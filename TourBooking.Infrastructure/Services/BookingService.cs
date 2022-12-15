using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Web;
using TourBooking.Core.Domain;
using TourBooking.Core.DTOs.Inputs;
using TourBooking.Core.DTOs.Outputs;
using TourBooking.Core.Enums;
using TourBooking.Core.Interfaces;

namespace TourBooking.Infrastructure.Services;

public sealed class BookingService : BaseService<Booking>, IBookingService
{
    private readonly IRepository<Booking> bookingRepository;
    private readonly UserManager<ApplicationUser> userManager;
    private readonly RoleManager<IdentityRole<Guid>> roleManager;
    private readonly IRepository<Booker> bookerRepository;
    private readonly IRepository<Material> materialRepository;
    private readonly IRepository<Message> messageRepository;
    private readonly IEmailSender emailSender;

    public BookingService(IRepository<Booking> bookingRepository, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole<Guid>> roleManager, IRepository<Booker> bookerRepository, IRepository<Material> materialRepository, IRepository<Message> messageRepository, IEmailSender emailSender) : base(bookingRepository)
    {
        this.bookingRepository = bookingRepository;
        this.userManager = userManager;
        this.roleManager = roleManager;
        this.bookerRepository = bookerRepository;
        this.materialRepository = materialRepository;
        this.messageRepository = messageRepository;
        this.emailSender = emailSender;
    }

    public async Task<ResponseDTO<Booking>> CreateBookingAsync(string handle, CreateBookingInputModel createBookingInputModel, string confirmUrl, CancellationToken cancellationToken = default)
    {
        try
        {
            var bookingToCreate = new Booking
            {
                BookingStatus = BookingStatus.Active,
                PackageId = createBookingInputModel.PackageId,
                DateTime = createBookingInputModel.DateTime!.Value,
                AlternativeDateTime = createBookingInputModel.DateTime,
                Attendees = createBookingInputModel.Attendees!.Value,
                Remark = createBookingInputModel.Remark
            };

            var applicationUser = await userManager.FindByEmailAsync(createBookingInputModel.Email);

            if (applicationUser is null)
            {
                var applicationUserToCreate = new ApplicationUser
                {
                    UserName = createBookingInputModel.FirstName + " " + createBookingInputModel.LastName,
                    Email = createBookingInputModel.Email,
                    PhoneNumber = createBookingInputModel.PhoneNumber,
                    RoleType = RoleType.Booker,
                    IsEmailNotificationEnabled = true,
                    LockoutEnabled = true,
                    SecurityStamp = Guid.NewGuid().ToString()
                };

                var createApplicationUserResult = await userManager.CreateAsync(applicationUserToCreate);

                if (!createApplicationUserResult.Succeeded)
                {
                    if (createApplicationUserResult.Errors.FirstOrDefault()?.Code is "InvalidUserName")
                    {
                        return new ResponseDTO<Booking>(false, "Failed to create user: The name must only consist of letters, dashes and spaces.", CreateBookingErrorType.InvalidUserName);
                    }

                    return new ResponseDTO<Booking>(false, "Failed to create the new user with the specified inputs.", CreateBookingErrorType.CouldNotCreateApplicationUser);
                }

                applicationUser = await userManager.FindByEmailAsync(createBookingInputModel.Email);

                if (applicationUser is null)
                {
                    return new ResponseDTO<Booking>(false, "Failed to fetch user data from the database.", CreateBookingErrorType.CouldNotFindApplicationUser);
                }

                var roleExists = await roleManager.RoleExistsAsync(RoleType.Booker.ToString());

                if (!roleExists)
                {
                    await roleManager.CreateAsync(new IdentityRole<Guid>(RoleType.Booker.ToString()));
                }

                await userManager.AddToRoleAsync(applicationUser, RoleType.Booker.ToString());

                var bookerToCreate = new Booker
                {
                    ApplicationUserId = applicationUser.Id,
                    Organization = createBookingInputModel.Organization
                };

                var createdBooker = await bookerRepository.CreateAsync(bookerToCreate, cancellationToken);

                if (createdBooker is null)
                {
                    return new ResponseDTO<Booking>(false, "Failed to create booker.", CreateBookingErrorType.CouldNotCreateBookerNewUser);
                }

                bookingToCreate.BookerId = createdBooker.Id;
            }
            else
            {
                var booker = await bookerRepository.GetTable()
                    .Select(x => new Booker
                    {
                        Id = x.Id,
                        Organization = x.Organization
                    })
                    .FirstOrDefaultAsync(x => x.ApplicationUserId == applicationUser.Id, cancellationToken);

                if (booker is null)
                {
                    var bookerToCreate = new Booker
                    {
                        ApplicationUserId = applicationUser.Id,
                        Organization = createBookingInputModel.Organization
                    };

                    var createdBooker = await bookerRepository.CreateAsync(bookerToCreate, cancellationToken);

                    if (createdBooker is null)
                    {
                        return new ResponseDTO<Booking>(false, "Failed to create booker.", CreateBookingErrorType.CouldNotCreateBookerExistingUser);
                    }

                    bookingToCreate.BookerId = createdBooker.Id;
                }
                else
                {
                    bookingToCreate.BookerId = booker.Id;
                }
            }

            var createdBooking = await bookingRepository.CreateAsync(bookingToCreate, cancellationToken);

            if (createdBooking is null)
            {
                return new ResponseDTO<Booking>(false, "Failed to create booking.", CreateBookingErrorType.CouldNotCreateBooking);
            }

            if (createBookingInputModel.MaterialIds?.Any() ?? false)
            {
                foreach (var materialId in createBookingInputModel.MaterialIds)
                {
                    var rowsAffected = await materialRepository.GetTable().Where(x => x.Id == materialId).ExecuteUpdateAsync(x => x.SetProperty(y => y.BookingId, createdBooking.Id), cancellationToken);

                    if (rowsAffected <= 0)
                    {
                        return new ResponseDTO<Booking>(false, "Your booking was successfully created, but failed to borrow the requested item(s).", CreateBookingErrorType.CouldNotUpdateMaterial);
                    }
                }
            }

            if (!applicationUser.EmailConfirmed)
            {
                var localReturnUrl = handle + "/booking/" + createdBooking.Id;
                var confirmToken = HttpUtility.UrlEncode(await userManager.GenerateEmailConfirmationTokenAsync(applicationUser));

                var to = createBookingInputModel.Email;
                var subject = "Confirm your booking";
                var body = "Before we accept the booking, we require you to confirm your email address by clicking on this link: <br />" +
                    $"<hr /> <a href=\"{confirmUrl}?confirmToken={confirmToken}&appUID={applicationUser.Id}&localReturnUrl={localReturnUrl}\" target=\"_blank\">{confirmUrl}?confirmToken={confirmToken}&appUID={applicationUser.Id}&localReturnUrl={localReturnUrl}</a>";

                var isEmailSent = await emailSender.SendEmailAsync(to, subject, body);

                if (!isEmailSent)
                {
                    return new ResponseDTO<Booking>(false, "Failed to send email - contact a server administrator.", CreateBookingErrorType.CouldNotSendEmail);
                }

                return new ResponseDTO<Booking>(false, "The booking was created, but we require you to confirm your email address by following the instructions sent to you through email.", CreateBookingErrorType.EmailNotConfirmed);
            }

            return new ResponseDTO<Booking>(true, content: createdBooking);
        }
        catch (OperationCanceledException ex)
        {
            return new ResponseDTO<Booking>(false, "Your request was canceled.", GeneralErrorType.OperationWasCanceled, ex.Message, ex.InnerException?.Message);
        }
        catch (Exception ex)
        {
            return new ResponseDTO<Booking>(false, "A server error has occurred - contact a server administrator.", GeneralErrorType.Unhandled, ex.Message, ex.InnerException?.Message);
        }
    }

    public async Task<ResponseDTO<Message>> CreateMessageAsync(CreateMessageInputModel createMessageInputModel, Guid? userId = null, CancellationToken cancellationToken = default)
    {
        try
        {
            var booking = userId is not null
                ? await bookingRepository.GetTable()
                    .Where(x => x.Id == createMessageInputModel.BookingId && x.Booker.ApplicationUserId == userId)
                    .Select(x => new Booking
                    {
                        Id = x.Id,
                        BookingStatus = x.BookingStatus
                    })
                    .FirstOrDefaultAsync(cancellationToken)
                : await bookingRepository.GetTable()
                    .Where(x => x.Id == createMessageInputModel.BookingId)
                    .Select(x => new Booking
                    {
                        Id = x.Id,
                        BookingStatus = x.BookingStatus
                    })
                    .FirstOrDefaultAsync(cancellationToken);

            if (booking is null)
            {
                return new ResponseDTO<Message>(false, "Failed to create a new message.", CreateMessageErrorType.CouldNotFindBooking);
            }

            var messageToCreate = new Message
            {
                Content = createMessageInputModel.Content,
                BookingId = booking.Id,
                ApplicationUserId = createMessageInputModel.ApplicationUserId
            };

            var createdMessage = await messageRepository.CreateAsync(messageToCreate, cancellationToken);

            if (createdMessage is null)
            {
                return new ResponseDTO<Message>(false, "Failed to create a new message.", CreateBookingErrorType.CouldNotCreateMessage);
            }

            return new ResponseDTO<Message>(true, content: createdMessage);
        }
        catch (OperationCanceledException ex)
        {
            return new ResponseDTO<Message>(false, "Your request was canceled.", GeneralErrorType.OperationWasCanceled, ex.Message, ex.InnerException?.Message);
        }
        catch (Exception ex)
        {
            return new ResponseDTO<Message>(false, "A server error has occurred - contact a server administrator.", GeneralErrorType.Unhandled, ex.Message, ex.InnerException?.Message);
        }
    }

    public async Task<ResponseDTO<BookingDetailsDTO>> GetBookingDetailsAsync(Guid id, Guid? userId = default, CancellationToken cancellationToken = default)
    {
        try
        {
            var booking = userId is null
                ? await bookingRepository.GetTable()
                    .Where(x => x.Id == id)
                    .Select(x => new BookingDetailsDTO
                    {
                        Id = x.Id,
                        BookingStatus = x.BookingStatus,
                        Organization = x.Booker.Organization,
                        DateTime = x.DateTime,
                        AlternativeDateTime = x.AlternativeDateTime,
                        PackageName = x.Package.Name,
                        PackageDescription = x.Package.Description,
                        StreetName = x.Package.Location == null ? null : x.Package.Location.StreetName,
                        City = x.Package.Location == null ? null : x.Package.Location.City,
                        ZipCode = x.Package.Location == null ? null : x.Package.Location.ZipCode,
                        Attendees = x.Attendees,
                        BookersName = x.Booker.ApplicationUser.UserName!,
                        BookersEmail = x.Booker.ApplicationUser.Email!,
                        BookersPhoneNumber = x.Booker.ApplicationUser.PhoneNumber,
                        Remark = x.Remark,
                        CreatedDate = x.CreatedDate,
                        UpdatedDate = x.UpdatedDate,
                        Materials = x.Materials!.Select(y => new Material
                        {
                            Id = y.Id,
                            Name = y.Name
                        }).ToList(),
                        Messages = x.Messages!.Select(y => new Message
                        {
                            Id = y.Id,
                            Content = y.Content,
                            ApplicationUser = new ApplicationUser
                            {
                                Id = y.ApplicationUser.Id,
                                UserName = y.ApplicationUser.UserName,
                            },
                            CreatedDate = y.CreatedDate,
                        }).ToList()
                    })
                    .FirstOrDefaultAsync(cancellationToken)
                : await bookingRepository.GetTable()
                    .Where(x => x.Id == id && x.Booker.ApplicationUserId == userId)
                    .Select(x => new BookingDetailsDTO
                    {
                        Id = x.Id,
                        BookingStatus = x.BookingStatus,
                        Organization = x.Booker.Organization,
                        DateTime = x.DateTime,
                        AlternativeDateTime = x.AlternativeDateTime,
                        PackageName = x.Package.Name,
                        PackageDescription = x.Package.Description,
                        StreetName = x.Package.Location == null ? null : x.Package.Location.StreetName,
                        City = x.Package.Location == null ? null : x.Package.Location.City,
                        ZipCode = x.Package.Location == null ? null : x.Package.Location.ZipCode,
                        Attendees = x.Attendees,
                        BookersName = x.Booker.ApplicationUser.UserName!,
                        BookersEmail = x.Booker.ApplicationUser.Email!,
                        BookersPhoneNumber = x.Booker.ApplicationUser.PhoneNumber,
                        Remark = x.Remark,
                        CreatedDate = x.CreatedDate,
                        UpdatedDate = x.UpdatedDate,
                        Materials = x.Materials!.Select(y => new Material
                        {
                            Id = y.Id,
                            Name = y.Name
                        }).ToList(),
                        Messages = x.Messages!.Select(y => new Message
                        {
                            Id = y.Id,
                            Content = y.Content,
                            ApplicationUser = new ApplicationUser
                            {
                                Id = y.ApplicationUser.Id,
                                UserName = y.ApplicationUser.UserName,
                            },
                            CreatedDate = y.CreatedDate,
                        }).ToList()
                    })
                    .FirstOrDefaultAsync(cancellationToken);

            if (booking is null)
            {
                return new ResponseDTO<BookingDetailsDTO>(false, "Failed to fetch booking from the database.", GetBookingDetailsErrorType.CouldNotFindBooking);
            }

            return new ResponseDTO<BookingDetailsDTO>(true, content: booking);
        }
        catch (OperationCanceledException ex)
        {
            return new ResponseDTO<BookingDetailsDTO>(false, "Your request was canceled.", GeneralErrorType.OperationWasCanceled, ex.Message, ex.InnerException?.Message);
        }
        catch (Exception ex)
        {
            return new ResponseDTO<BookingDetailsDTO>(false, "A server error has occurred - contact a server administrator.", GeneralErrorType.Unhandled, ex.Message, ex.InnerException?.Message);
        }
    }

    public async Task<ResponseDTO<IEnumerable<BookingListDTO>>> GetBookingListAsync(string handle, Guid? userId = null, CancellationToken cancellationToken = default)
    {
        try
        {
            var bookings = userId is null
                ? await bookingRepository.GetTable()
                    .Where(x => x.Package.Company.Handle == handle)
                    .Select(x => new BookingListDTO
                    {
                        Id = x.Id,
                        BookingStatus = x.BookingStatus,
                        DateTime = x.DateTime,
                        UserName = x.Booker.ApplicationUser.UserName!,
                        Email = x.Booker.ApplicationUser.Email!,
                        PhoneNumber = x.Booker.ApplicationUser.PhoneNumber ?? "N/A",
                        Organization = x.Booker.Organization,
                        PackageName = x.Package.Name,
                        CreatedDate = x.CreatedDate
                    })
                    .ToListAsync(cancellationToken)
                : await bookingRepository.GetTable()
                    .Where(x => x.Package.Company.Handle == handle && x.Booker.ApplicationUserId == userId)
                    .Select(x => new BookingListDTO
                    {
                        Id = x.Id,
                        BookingStatus = x.BookingStatus,
                        DateTime = x.DateTime,
                        UserName = x.Booker.ApplicationUser.UserName!,
                        Email = x.Booker.ApplicationUser.Email!,
                        PhoneNumber = x.Booker.ApplicationUser.PhoneNumber ?? "N/A",
                        Organization = x.Booker.Organization,
                        PackageName = x.Package.Name,
                        CreatedDate = x.CreatedDate
                    })
                    .ToListAsync(cancellationToken);

            return new ResponseDTO<IEnumerable<BookingListDTO>>(true, content: bookings);
        }
        catch (OperationCanceledException ex)
        {
            return new ResponseDTO<IEnumerable<BookingListDTO>>(false, "Your request was canceled.", GeneralErrorType.OperationWasCanceled, ex.Message, ex.InnerException?.Message);
        }
        catch (Exception ex)
        {
            return new ResponseDTO<IEnumerable<BookingListDTO>>(false, "A server error has occurred - contact a server administrator.", GeneralErrorType.Unhandled, ex.Message, ex.InnerException?.Message);
        }
    }

    public async Task<ResponseDTO<BookingDetailsDTO>> ToggleBookingStatusAsync(Guid id, BookingStatus bookingStatus, Guid? userId = default, CancellationToken cancellationToken = default)
    {
        try
        {
            var rowsAffectedStatus = await bookingRepository.GetTable().Where(x => x.Id == id).ExecuteUpdateAsync(x => x.SetProperty(x => x.BookingStatus, bookingStatus), cancellationToken);
            var rowsAffectedUpdatedDate = await bookingRepository.GetTable().Where(x => x.Id == id).ExecuteUpdateAsync(x => x.SetProperty(x => x.UpdatedDate, DateTime.UtcNow), cancellationToken);

            if (rowsAffectedStatus <= 0)
            {
                return new ResponseDTO<BookingDetailsDTO>(false, "Failed to update the booking status.", ToggleBookingStatusErrorType.CouldNotUpdateBooking);
            }

            var updatedBooking = await GetBookingDetailsAsync(id, userId, cancellationToken);

            return updatedBooking;
        }
        catch (OperationCanceledException ex)
        {
            return new ResponseDTO<BookingDetailsDTO>(false, "Your request was canceled.", GeneralErrorType.OperationWasCanceled, ex.Message, ex.InnerException?.Message);
        }
        catch (Exception ex)
        {
            return new ResponseDTO<BookingDetailsDTO>(false, "A server error has occurred - contact a server administrator.", GeneralErrorType.Unhandled, ex.Message, ex.InnerException?.Message);
        }
    }
}