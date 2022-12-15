using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Web;
using TourBooking.Core.Domain;
using TourBooking.Core.DTOs.Inputs;
using TourBooking.Core.DTOs.Outputs;
using TourBooking.Core.Enums;
using TourBooking.Core.Interfaces;

namespace TourBooking.Infrastructure.Services;

public sealed class UserService : IUserService
{
    private readonly UserManager<ApplicationUser> userManager;
    private readonly RoleManager<IdentityRole<Guid>> roleManager;
    private readonly IRepository<Admin> adminRepository;
    private readonly IRepository<Employee> employeeRepository;
    private readonly IRepository<Booker> bookerRepository;
    private readonly IRepository<Company> companyRepository;

    public UserService(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole<Guid>> roleManager, IRepository<Admin> adminRepository, IRepository<Employee> employeeRepository, IRepository<Booker> bookerRepository, IRepository<Company> companyRepository)
    {
        this.userManager = userManager;
        this.roleManager = roleManager;
        this.adminRepository = adminRepository;
        this.employeeRepository = employeeRepository;
        this.bookerRepository = bookerRepository;
        this.companyRepository = companyRepository;
    }

    public async Task<ResponseDTO<Booker>> ConfirmEmailAsync(Guid id, string confirmToken, CancellationToken cancellationToken = default)
    {
        try
        {
            var applicationUser = await userManager.FindByIdAsync(id.ToString());

            if (applicationUser is null)
            {
                return new ResponseDTO<Booker>(false, "Failed to confirm email.", ConfirmEmailErrorType.CouldNotFindApplicationUser);
            }

            if (applicationUser.EmailConfirmed)
            {
                return new ResponseDTO<Booker>(false, "Your email is already confirmed.", ConfirmEmailErrorType.EmailAlreadyConfirmed);
            }

            var confirmResult = await userManager.ConfirmEmailAsync(applicationUser, confirmToken);

            if (!confirmResult.Succeeded)
            {
                return new ResponseDTO<Booker>(false, "Failed to confirm email.", ConfirmEmailErrorType.CouldNotConfirmEmail, confirmResult.Errors.FirstOrDefault()?.Description);
            }

            return new ResponseDTO<Booker>(true);
        }
        catch (OperationCanceledException ex)
        {
            return new ResponseDTO<Booker>(false, "Your request was canceled.", GeneralErrorType.OperationWasCanceled, ex.Message, ex.InnerException?.Message);
        }
        catch (Exception ex)
        {
            return new ResponseDTO<Booker>(false, "A server error has occurred - contact a server administrator.", GeneralErrorType.Unhandled, ex.Message, ex.InnerException?.Message);
        }
    }

    public async Task<ResponseDTO<IEnumerable<AdminListDTO>>> GetAdminListAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            var admins = await adminRepository.GetTable()
                .Select(x => new AdminListDTO
                {
                    Id = x.Id,
                    Name = x.ApplicationUser.UserName!,
                    Email = x.ApplicationUser.Email!,
                    PhoneNumber = x.ApplicationUser.PhoneNumber ?? "N/A",
                    CreatedDate = x.CreatedDate,
                })
                .ToListAsync(cancellationToken);

            return new ResponseDTO<IEnumerable<AdminListDTO>>(true, content: admins);
        }
        catch (OperationCanceledException ex)
        {
            return new ResponseDTO<IEnumerable<AdminListDTO>>(false, "Your request was canceled.", GeneralErrorType.OperationWasCanceled, ex.Message, ex.InnerException?.Message);
        }
        catch (Exception ex)
        {
            return new ResponseDTO<IEnumerable<AdminListDTO>>(false, "A server error has occurred - contact a server administrator.", GeneralErrorType.Unhandled, ex.Message, ex.InnerException?.Message);
        }
    }

    public async Task<ResponseDTO<IEnumerable<BookerListDTO>>> GetBookerListAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            var bookers = await bookerRepository.GetTable()
                .Select(x => new BookerListDTO
                {
                    Id = x.Id,
                    Name = x.ApplicationUser.UserName!,
                    Email = x.ApplicationUser.Email!,
                    PhoneNumber = x.ApplicationUser.PhoneNumber ?? "N/A",
                    Organization = x.Organization,
                    CreatedDate = x.CreatedDate,
                })
                .ToListAsync(cancellationToken);

            return new ResponseDTO<IEnumerable<BookerListDTO>>(true, content: bookers);
        }
        catch (OperationCanceledException ex)
        {
            return new ResponseDTO<IEnumerable<BookerListDTO>>(false, "Your request was canceled.", GeneralErrorType.OperationWasCanceled, ex.Message, ex.InnerException?.Message);
        }
        catch (Exception ex)
        {
            return new ResponseDTO<IEnumerable<BookerListDTO>>(false, "A server error has occurred - contact a server administrator.", GeneralErrorType.Unhandled, ex.Message, ex.InnerException?.Message);
        }
    }

    public async Task<ResponseDTO<IEnumerable<EmployeeListDTO>>> GetEmployeeListAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            var employees = await employeeRepository.GetTable()
                .Select(x => new EmployeeListDTO
                {
                    Id = x.Id,
                    Name = x.ApplicationUser.UserName!,
                    Email = x.ApplicationUser.Email!,
                    PhoneNumber = x.ApplicationUser.PhoneNumber ?? "N/A",
                    CreatedDate = x.CreatedDate,
                    CompanyName = x.Company.Name
                })
                .ToListAsync(cancellationToken);

            return new ResponseDTO<IEnumerable<EmployeeListDTO>>(true, content: employees);
        }
        catch (OperationCanceledException ex)
        {
            return new ResponseDTO<IEnumerable<EmployeeListDTO>>(false, "Your request was canceled.", GeneralErrorType.OperationWasCanceled, ex.Message, ex.InnerException?.Message);
        }
        catch (Exception ex)
        {
            return new ResponseDTO<IEnumerable<EmployeeListDTO>>(false, "A server error has occurred - contact a server administrator.", GeneralErrorType.Unhandled, ex.Message, ex.InnerException?.Message);
        }
    }

    public async Task<ResponseDTO<Admin>> RegisterAdminAsync(RegisterAdminInputModel registerAdminInputModel, CancellationToken cancellationToken = default)
    {
        var applicationUserExists = await userManager.FindByEmailAsync(registerAdminInputModel.Email) is not null;

        if (applicationUserExists)
        {
            return new ResponseDTO<Admin>(false, $"A user with the email ({registerAdminInputModel.Email}) already exists.", RegisterAdminErrorType.EmailAlreadyExists);
        }

        var applicationUserToCreate = new ApplicationUser
        {
            UserName = registerAdminInputModel.FirstName + " " + registerAdminInputModel.LastName,
            Email = registerAdminInputModel.Email,
            PhoneNumber = registerAdminInputModel.PhoneNumber,
            IsEmailNotificationEnabled = true,
            RoleType = RoleType.Admin,
            LockoutEnabled = true,
            SecurityStamp = Guid.NewGuid().ToString()
        };

        var createApplicationUserResult = await userManager.CreateAsync(applicationUserToCreate, registerAdminInputModel.Password);

        if (!createApplicationUserResult.Succeeded)
        {
            if (createApplicationUserResult.Errors.FirstOrDefault()?.Code is "InvalidUserName")
            {
                return new ResponseDTO<Admin>(false, "Failed to create user: The name must only consist of letters, dashes and spaces.", RegisterAdminErrorType.InvalidUserName);
            }

            return new ResponseDTO<Admin>(false, "Failed to create user.", RegisterAdminErrorType.CouldNotCreateApplicationUser, createApplicationUserResult.Errors.FirstOrDefault()?.Description);
        }

        var applicationUser = await userManager.FindByEmailAsync(registerAdminInputModel.Email);

        if (applicationUser is null)
        {
            return new ResponseDTO<Admin>(false, "Failed to fetch user.", RegisterAdminErrorType.CouldNotFindApplicationUser);
        }

        var roleExists = await roleManager.RoleExistsAsync(RoleType.Admin.ToString());

        if (!roleExists)
        {
            await roleManager.CreateAsync(new IdentityRole<Guid>(RoleType.Admin.ToString()));
        }

        await userManager.AddToRoleAsync(applicationUser, RoleType.Admin.ToString());

        var adminToCreate = new Admin
        {
            ApplicationUserId = applicationUser.Id
        };

        var createdAdmin = await adminRepository.CreateAsync(adminToCreate, cancellationToken);

        if (createdAdmin is null)
        {
            return new ResponseDTO<Admin>(false, "Server error. Unable to register administrator.", RegisterAdminErrorType.CouldNotCreateAdmin);
        }

        return new ResponseDTO<Admin>(true, content: createdAdmin);
    }

    public async Task<ResponseDTO<Booker>> RegisterBookerAsync(RegisterBookerInputModel registerInputModel, CancellationToken cancellationToken = default)
    {
        try
        {
            var applicationUser = await userManager.FindByEmailAsync(registerInputModel.Email);

            if (applicationUser is not null)
            {
                if (!string.IsNullOrWhiteSpace(applicationUser.PasswordHash))
                {
                    return new ResponseDTO<Booker>(false, $"Bruger med e-mail: {registerInputModel.Email} eksisterer allerede.", RegisterBookerErrorType.EmailAlreadyExists);
                }
                else
                {
                    var addPasswordResult = await userManager.AddPasswordAsync(applicationUser, registerInputModel.Password);
                }
            }
            else
            {
                var applicationUserToCreate = new ApplicationUser
                {
                    Email = registerInputModel.Email,
                    UserName = registerInputModel.FirstName + " " + registerInputModel.LastName,
                    PhoneNumber = registerInputModel.PhoneNumber,
                    IsEmailNotificationEnabled = true,
                    LockoutEnabled = true,
                    SecurityStamp = Guid.NewGuid().ToString()
                };

                var createApplicationUserResult = await userManager.CreateAsync(applicationUserToCreate, registerInputModel.Password);

                if (!createApplicationUserResult.Succeeded)
                {
                    return createApplicationUserResult.Errors.FirstOrDefault()?.Code is "InvalidUserName"
                        ? new ResponseDTO<Booker>(false, "Navnet må kun bestå af bogstaver, bindestreger og mellemrum.", RegisterBookerErrorType.InvalidUserName)
                        : new ResponseDTO<Booker>(false, "Fejl ved oprettelse af bruger.", RegisterBookerErrorType.CouldNotCreateApplicationUser, createApplicationUserResult.Errors.FirstOrDefault()?.Description);
                }
            }

            if (applicationUser is null)
            {
                return new ResponseDTO<Booker>(false, "Fejl ved oprettelse af bruger.", RegisterBookerErrorType.CouldNotGetApplicationUser);
            }

            var bookerRoleExists = await roleManager.RoleExistsAsync(RoleType.Booker.ToString());

            if (!bookerRoleExists)
            {
                await roleManager.CreateAsync(new IdentityRole<Guid>(RoleType.Booker.ToString()));
            }

            var bookserIsInBookerRole = await userManager.IsInRoleAsync(applicationUser, RoleType.Booker.ToString());

            if (!bookserIsInBookerRole)
            {
                await userManager.AddToRoleAsync(applicationUser, RoleType.Booker.ToString());
            }

            var booker = await bookerRepository.GetTable().FirstOrDefaultAsync(x => x.ApplicationUserId == applicationUser.Id, cancellationToken);

            if (booker is null)
            {
                var bookerToCreate = new Booker()
                {
                    ApplicationUserId = applicationUser.Id,
                    Organization = registerInputModel.Organization
                };

                booker = await bookerRepository.CreateAsync(bookerToCreate, cancellationToken);

                if (booker is null)
                {
                    return new ResponseDTO<Booker>(false, "Fejl ved oprettelse af bruger.", RegisterBookerErrorType.CouldNotCreateBooker);
                }
            }

            return new ResponseDTO<Booker>(true, content: booker);
        }
        catch (OperationCanceledException ex)
        {
            return new ResponseDTO<Booker>(false, "Your request was canceled.", GeneralErrorType.OperationWasCanceled, ex.Message, ex.InnerException?.Message);
        }
        catch (Exception ex)
        {
            return new ResponseDTO<Booker>(false, "A server error has occurred - contact a server administrator.", GeneralErrorType.Unhandled, ex.Message, ex.InnerException?.Message);
        }
    }

    public async Task<ResponseDTO<Employee>> RegisterEmployeeAsync(RegisterEmployeeInputModel registerEmployeeInputModel, CancellationToken cancellationToken = default)
    {
        var company = await companyRepository.GetTable().Select(x => new Company { Id = x.Id, Handle = x.Handle, LogoUrl = x.LogoUrl, Name = x.Name, PhoneNumber = x.PhoneNumber }).FirstOrDefaultAsync(x => x.Id == registerEmployeeInputModel.CompanyId, cancellationToken);

        if (company is null)
        {
            return new ResponseDTO<Employee>(false, "Failed to fetch company.", RegisterEmployeeErrorType.CouldNotFindCompany);
        }

        var applicationUserExists = await userManager.FindByEmailAsync(registerEmployeeInputModel.Email) is not null;

        if (applicationUserExists)
        {
            return new ResponseDTO<Employee>(false, $"A user with the email ({registerEmployeeInputModel.Email}) already exists.", RegisterEmployeeErrorType.EmailAlreadyExists);
        }

        var applicationUserToCreate = new ApplicationUser
        {
            UserName = registerEmployeeInputModel.FirstName + " " + registerEmployeeInputModel.LastName,
            Email = registerEmployeeInputModel.Email,
            PhoneNumber = registerEmployeeInputModel.PhoneNumber,
            IsEmailNotificationEnabled = true,
            RoleType = RoleType.Employee,
            LockoutEnabled = true,
            SecurityStamp = Guid.NewGuid().ToString(),
        };

        var createApplicationUserResult = await userManager.CreateAsync(applicationUserToCreate, registerEmployeeInputModel.Password);

        if (!createApplicationUserResult.Succeeded)
        {
            if (createApplicationUserResult.Errors.FirstOrDefault()?.Code is "InvalidUserName")
            {
                return new ResponseDTO<Employee>(false, "Failed to create user: The name must only consist of letters, dashes and spaces.", RegisterEmployeeErrorType.InvalidUserName);
            }

            return new ResponseDTO<Employee>(false, "Failed to create user.", RegisterEmployeeErrorType.CouldNotCreateApplicationUser, createApplicationUserResult.Errors.FirstOrDefault()?.Description);
        }

        var applicationUser = await userManager.FindByEmailAsync(registerEmployeeInputModel.Email);

        if (applicationUser is null)
        {
            return new ResponseDTO<Employee>(false, "Failed to fetch user.", RegisterEmployeeErrorType.CouldNotFindApplicationUser);
        }

        var roleExists = await roleManager.RoleExistsAsync(RoleType.Employee.ToString());

        if (!roleExists)
        {
            await roleManager.CreateAsync(new IdentityRole<Guid>(RoleType.Employee.ToString()));
        }

        await userManager.AddToRoleAsync(applicationUser, RoleType.Employee.ToString());

        var companyRoleExists = await roleManager.RoleExistsAsync(company.Handle);

        if (!companyRoleExists)
        {
            await roleManager.CreateAsync(new IdentityRole<Guid>(company.Handle));
        }

        await userManager.AddToRoleAsync(applicationUser, company.Handle);

        var employeeToCreate = new Employee
        {
            ApplicationUserId = applicationUser.Id,
            CompanyId = registerEmployeeInputModel.CompanyId
        };

        var createdEmployee = await employeeRepository.CreateAsync(employeeToCreate, cancellationToken);

        if (createdEmployee is null)
        {
            return new ResponseDTO<Employee>(false, "Server error. Unable to register employee.", RegisterEmployeeErrorType.CouldNotCreateEmployee);
        }

        return new ResponseDTO<Employee>(true, content: createdEmployee);
    }
}