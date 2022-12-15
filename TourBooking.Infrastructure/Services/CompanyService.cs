using Microsoft.EntityFrameworkCore;
using TourBooking.Core.Domain;
using TourBooking.Core.DTOs.Inputs;
using TourBooking.Core.DTOs.Outputs;
using TourBooking.Core.Enums;
using TourBooking.Core.Interfaces;

namespace TourBooking.Infrastructure.Services;

public sealed class CompanyService : BaseService<Company>, ICompanyService
{
    private readonly IRepository<Company> companyRepository;
    private readonly IRepository<Location> locationRepository;
    private readonly IRepository<Theme> themeRepository;
    private readonly IRepository<Package> packageRepository;
    private readonly IRepository<Material> materialRepository;

    public CompanyService(IRepository<Company> companyRepository, IRepository<Location> locationRepository, IRepository<Theme> themeRepository, IRepository<Package> packageRepository, IRepository<Material> materialRepository) : base(companyRepository)
    {
        this.companyRepository = companyRepository;
        this.locationRepository = locationRepository;
        this.themeRepository = themeRepository;
        this.packageRepository = packageRepository;
        this.materialRepository = materialRepository;
    }

    public async Task<ResponseDTO<Location>> CreateLocationAsync(CreateLocationInputModel createLocationInputModel, CancellationToken cancellationToken = default)
    {
        try
        {
            var locationToCreate = new Location
            {
                CompanyId = createLocationInputModel.CompanyId,
                StreetName = createLocationInputModel.StreetName,
                City = createLocationInputModel.City,
                ZipCode = createLocationInputModel.ZipCode!.Value
            };

            var createdLocation = await locationRepository.CreateAsync(locationToCreate, cancellationToken);

            if (createdLocation is null)
            {
                return new ResponseDTO<Location>(false, "Failed to create location.", CreateLocationErrorType.CouldNotCreateLocation);
            }

            return new ResponseDTO<Location>(true, content: createdLocation);
        }
        catch (OperationCanceledException ex)
        {
            return new ResponseDTO<Location>(false, "Your request was canceled.", GeneralErrorType.OperationWasCanceled, ex.Message, ex.InnerException?.Message);
        }
        catch (Exception ex)
        {
            return new ResponseDTO<Location>(false, "A server error has occurred - contact a server administrator.", GeneralErrorType.Unhandled, ex.Message, ex.InnerException?.Message);
        }
    }

    public async Task<ResponseDTO<Material>> CreateMaterialAsync(CreateMaterialInputModel createPackageInputModel, CancellationToken cancellationToken = default)
    {
        try
        {
            var materialToCreate = new Material
            {
                CompanyId = createPackageInputModel.CompanyId,
                Name = createPackageInputModel.Name
            };

            var createdMaterial = await materialRepository.CreateAsync(materialToCreate, cancellationToken);

            if (createdMaterial is null)
            {
                return new ResponseDTO<Material>(false, "Failed to create material.", CreateMaterialErrorType.CouldNotCreateMaterial);
            }

            return new ResponseDTO<Material>(true, content: createdMaterial);
        }
        catch (OperationCanceledException ex)
        {
            return new ResponseDTO<Material>(false, "Your request was canceled.", GeneralErrorType.OperationWasCanceled, ex.Message, ex.InnerException?.Message);
        }
        catch (Exception ex)
        {
            return new ResponseDTO<Material>(false, "A server error has occurred - contact a server administrator.", GeneralErrorType.Unhandled, ex.Message, ex.InnerException?.Message);
        }
    }

    public async Task<ResponseDTO<Package>> CreatePackageAsync(CreatePackageInputModel createPackageInputModel, CancellationToken cancellationToken = default)
    {
        try
        {
            var packageToCreate = new Package
            {
                CompanyId = createPackageInputModel.CompanyId,
                LocationId = createPackageInputModel.LocationId,
                Name = createPackageInputModel.Name,
                Description = createPackageInputModel.Description
            };

            var createdPackage = await packageRepository.CreateAsync(packageToCreate, cancellationToken);

            if (createdPackage is null)
            {
                return new ResponseDTO<Package>(false, "Failed to create package.", CreatePackageErrorType.CouldNotCreatePackage);
            }

            return new ResponseDTO<Package>(true, content: createdPackage);
        }
        catch (OperationCanceledException ex)
        {
            return new ResponseDTO<Package>(false, "Your request was canceled.", GeneralErrorType.OperationWasCanceled, ex.Message, ex.InnerException?.Message);
        }
        catch (Exception ex)
        {
            return new ResponseDTO<Package>(false, "A server error has occurred - contact a server administrator.", GeneralErrorType.Unhandled, ex.Message, ex.InnerException?.Message);
        }
    }

    public async Task<ResponseDTO<IEnumerable<EmployeesEmailListDTO>>> GetAllEmployeesEmailListAsync(string handle, CancellationToken cancellationToken = default)
    {
        try
        {
            var employeesEmailList = await companyRepository.GetTable()
                .Where(x => x.Handle == handle)
                .SelectMany(x => x.Employees!
                    .Select(y => new EmployeesEmailListDTO
                    {
                        Email = y.ApplicationUser.Email!,
                        IsEmailNotificationEnabled = y.ApplicationUser.IsEmailNotificationEnabled
                    }).ToList())
                .ToListAsync(cancellationToken);

            return new ResponseDTO<IEnumerable<EmployeesEmailListDTO>>(true, content: employeesEmailList);
        }
        catch (OperationCanceledException ex)
        {
            return new ResponseDTO<IEnumerable<EmployeesEmailListDTO>>(false, "Your request was canceled.", GeneralErrorType.OperationWasCanceled, ex.Message, ex.InnerException?.Message);
        }
        catch (Exception ex)
        {
            return new ResponseDTO<IEnumerable<EmployeesEmailListDTO>>(false, "A server error has occurred - contact a server administrator.", GeneralErrorType.Unhandled, ex.Message, ex.InnerException?.Message);
        }
    }

    public async Task<ResponseDTO<CompanyDetailsDTO>> GetCompanyDetailsAsync(string handle, CancellationToken cancellationToken = default)
    {
        try
        {
            var company = await companyRepository.GetTable()
                .Where(x => x.Handle == handle)
                .Select(x => new CompanyDetailsDTO
                {
                    Id = x.Id,
                    Name = x.Name,
                    Handle = x.Handle,
                    PhoneNumber = x.PhoneNumber,
                    LogoUrl = x.LogoUrl,
                    Website = x.Website,
                    HeadquarterStreetName = x.Locations!.First(x => x.IsHeadquarter).StreetName,
                    HeadquarterCity = x.Locations!.First(x => x.IsHeadquarter).City,
                    HeadquarterZipCode = x.Locations!.First(x => x.IsHeadquarter).ZipCode,
                    PrimaryBackgroundColor = x.Themes!.First(x => x.IsPrimary).BackgroundColor,
                    PrimaryContainerColor = x.Themes!.First(x => x.IsPrimary).ContainerColor,
                    PrimaryTextColor = x.Themes!.First(x => x.IsPrimary).TextColor,
                    PrimaryNavigationBackgroundColor = x.Themes!.First(x => x.IsPrimary).NavigationBackgroundColor,
                    PrimaryNavigationButtonTextColor = x.Themes!.First(x => x.IsPrimary).NavigationButtonTextColor,
                    PrimaryButtonColor = x.Themes!.First(x => x.IsPrimary).ButtonColor,
                    PrimaryButtonTextColor = x.Themes!.First(x => x.IsPrimary).ButtonTextColor,
                    BookingsCount = x.Packages == null ? 0 : x.Packages.Count(y => y.Bookings!.Any()),
                    Employees = x.Employees!.Select(y => new Employee
                    {
                        Id = y.Id,
                        ApplicationUser = new ApplicationUser
                        {
                            Id = y.Id,
                            UserName = y.ApplicationUser.UserName
                        }
                    }).ToList(),
                    Locations = x.Locations!.Select(y => new Location
                    {
                        Id = y.Id,
                        City = y.City,
                        StreetName = y.StreetName,
                        ZipCode = y.ZipCode,
                        IsHeadquarter = y.IsHeadquarter
                    }).ToList(),
                    Materials = x.Materials!.Select(y => new Material
                    {
                        Id = y.Id,
                        Name = y.Name,
                        BookingId = y.BookingId,
                        Booking = y.Booking != null ? new Booking
                        {
                            Id = y.Id,
                            BookingStatus = y.Booking!.BookingStatus
                        } : null,
                    }).ToList(),
                    Packages = x.Packages!.Select(y => new Package
                    {
                        Id = y.Id,
                        Name = y.Name,
                        Description = y.Description
                    }).ToList()
                })
                .FirstOrDefaultAsync(cancellationToken);

            if (company is null)
            {
                return new ResponseDTO<CompanyDetailsDTO>(false, "Failed to find company from the handle.", GetCompanyDetailsErrorType.CouldNotFindCompany);
            }

            return new ResponseDTO<CompanyDetailsDTO>(true, content: company);
        }
        catch (OperationCanceledException ex)
        {
            return new ResponseDTO<CompanyDetailsDTO>(false, "Your request was canceled.", GeneralErrorType.OperationWasCanceled, ex.Message, ex.InnerException?.Message);
        }
        catch (Exception ex)
        {
            return new ResponseDTO<CompanyDetailsDTO>(false, "A server error has occurred - contact a server administrator.", GeneralErrorType.Unhandled, ex.Message, ex.InnerException?.Message);
        }
    }

    public async Task<ResponseDTO<IEnumerable<CompanyListDTO>>> GetCompanyListAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            var companies = await companyRepository.GetTable()
                .Select(x => new CompanyListDTO
                {
                    Id = x.Id,
                    Name = x.Name,
                    Handle = x.Handle,
                    CreatedDate = x.CreatedDate,
                    HeadquarterStreetName = x.Locations!.First(x => x.IsHeadquarter).StreetName,
                    HeadquarterCity = x.Locations!.First(x => x.IsHeadquarter).City,
                    HeadquarterZipCode = x.Locations!.First(x => x.IsHeadquarter).ZipCode,
                    BookingsCount = x.Packages!.Select(x => x.Bookings).Count(),
                    EmployeesCount = x.Employees!.Count,
                    LocationsCount = x.Locations!.Count,
                    MaterialsCount = x.Materials!.Count,
                    PackagesCount = x.Packages!.Count
                })
                .ToListAsync(cancellationToken);

            return new ResponseDTO<IEnumerable<CompanyListDTO>>(true, content: companies);
        }
        catch (OperationCanceledException ex)
        {
            return new ResponseDTO<IEnumerable<CompanyListDTO>>(false, "Your request was canceled.", GeneralErrorType.OperationWasCanceled, ex.Message, ex.InnerException?.Message);
        }
        catch (Exception ex)
        {
            return new ResponseDTO<IEnumerable<CompanyListDTO>>(false, "A server error has occurred - contact a server administrator.", GeneralErrorType.Unhandled, ex.Message, ex.InnerException?.Message);
        }
    }

    public async Task<ResponseDTO<IEnumerable<CompanyLocationListDTO>>> GetCompanyLocationListAsync(string handle, CancellationToken cancellationToken = default)
    {
        try
        {
            var locations = await locationRepository.GetTable()
                .Where(x => x.Company.Handle == handle)
                .Select(x => new CompanyLocationListDTO
                {
                    Id = x.Id,
                    IsHeadquarter = x.IsHeadquarter,
                    StreetName = x.StreetName,
                    City = x.City,
                    ZipCode = x.ZipCode,
                    CreatedDate = x.CreatedDate,
                    PackagesCount = x.Packages!.Count()
                })
                .ToListAsync(cancellationToken);

            return new ResponseDTO<IEnumerable<CompanyLocationListDTO>>(true, content: locations);
        }
        catch (OperationCanceledException ex)
        {
            return new ResponseDTO<IEnumerable<CompanyLocationListDTO>>(false, "Your request was canceled.", GeneralErrorType.OperationWasCanceled, ex.Message, ex.InnerException?.Message);
        }
        catch (Exception ex)
        {
            return new ResponseDTO<IEnumerable<CompanyLocationListDTO>>(false, "A server error has occurred - contact a server administrator.", GeneralErrorType.Unhandled, ex.Message, ex.InnerException?.Message);
        }
    }

    public async Task<ResponseDTO<IEnumerable<CompanyMaterialListDTO>>> GetCompanyMaterialListAsync(string handle, CancellationToken cancellationToken = default)
    {
        try
        {
            var materials = await materialRepository.GetTable()
                .Where(x => x.Company.Handle == handle && x.BookingId == null)
                .Select(x => new CompanyMaterialListDTO
                {
                    Id = x.Id,
                    Name = x.Name,
                    BookingId = x.BookingId,
                    BookerName = x.Booking == null ? null : x.Booking.Booker.ApplicationUser.UserName
                })
                .ToListAsync(cancellationToken);

            return new ResponseDTO<IEnumerable<CompanyMaterialListDTO>>(true, content: materials);
        }
        catch (OperationCanceledException ex)
        {
            return new ResponseDTO<IEnumerable<CompanyMaterialListDTO>>(false, "Your request was canceled.", GeneralErrorType.OperationWasCanceled, ex.Message, ex.InnerException?.Message);
        }
        catch (Exception ex)
        {
            return new ResponseDTO<IEnumerable<CompanyMaterialListDTO>>(false, "A server error has occurred - contact a server administrator.", GeneralErrorType.Unhandled, ex.Message, ex.InnerException?.Message);
        }
    }

    public async Task<ResponseDTO<IEnumerable<CompanyPackageListDTO>>> GetCompanyPackageListAsync(string handle, CancellationToken cancellationToken = default)
    {
        try
        {
            var packages = await packageRepository.GetTable()
                .Where(x => x.Company.Handle == handle)
                .Select(x => new CompanyPackageListDTO
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description,
                    StreetName = x.Location == null ? null : x.Location.StreetName,
                    City = x.Location == null ? null : x.Location.City,
                    ZipCode = x.Location == null ? null : x.Location.ZipCode,
                    CreatedDate = x.CreatedDate,
                    BookingsCount = x.Bookings!.Count
                })
                .ToListAsync(cancellationToken);

            return new ResponseDTO<IEnumerable<CompanyPackageListDTO>>(true, content: packages);
        }
        catch (OperationCanceledException ex)
        {
            return new ResponseDTO<IEnumerable<CompanyPackageListDTO>>(false, "Your request was canceled.", GeneralErrorType.OperationWasCanceled, ex.Message, ex.InnerException?.Message);
        }
        catch (Exception ex)
        {
            return new ResponseDTO<IEnumerable<CompanyPackageListDTO>>(false, "A server error has occurred - contact a server administrator.", GeneralErrorType.Unhandled, ex.Message, ex.InnerException?.Message);
        }
    }

    public async Task<ResponseDTO<CompanyThemeDTO>> LoadCompanyThemeAsync(string handle, CancellationToken cancellationToken = default)
    {
        try
        {
            var company = await companyRepository.GetTable()
                .Where(x => x.Handle == handle)
                .Select(x => new CompanyThemeDTO
                {
                    Id = x.Id,
                    Name = x.Name,
                    Handle = x.Handle,
                    Website = x.Website,
                    LogoUrl = x.LogoUrl,
                    NavigationBackgroundColor = x.Themes!.First(x => x.IsPrimary).NavigationBackgroundColor,
                    NavigationButtonTextColor = x.Themes!.First(x => x.IsPrimary).NavigationButtonTextColor,
                    BackgroundColor = x.Themes!.First(x => x.IsPrimary).BackgroundColor,
                    ContainerColor = x.Themes!.First(x => x.IsPrimary).ContainerColor,
                    TextColor = x.Themes!.First(x => x.IsPrimary).TextColor,
                    ButtonColor = x.Themes!.First(x => x.IsPrimary).ButtonColor,
                    ButtonTextColor = x.Themes!.First(x => x.IsPrimary).ButtonTextColor
                })
                .FirstOrDefaultAsync(cancellationToken);

            if (company is null)
            {
                return new ResponseDTO<CompanyThemeDTO>(false, "Invalid URL.", LoadCompanyThemeErrorType.CouldNotFindCompany);
            }

            return new ResponseDTO<CompanyThemeDTO>(true, content: company);
        }
        catch (OperationCanceledException ex)
        {
            return new ResponseDTO<CompanyThemeDTO>(false, "Your request was canceled.", GeneralErrorType.OperationWasCanceled, ex.Message, ex.InnerException?.Message);
        }
        catch (Exception ex)
        {
            return new ResponseDTO<CompanyThemeDTO>(false, "A server error has occurred - contact a server administrator.", GeneralErrorType.Unhandled, ex.Message, ex.InnerException?.Message);
        }
    }

    public async Task<ResponseDTO<Company>> RegisterCompanyAsync(RegisterCompanyInputModel registerCompanyInputModel, CancellationToken cancellationToken = default)
    {
        try
        {
            var companyToCreate = new Company
            {
                Name = registerCompanyInputModel.Name,
                Handle = registerCompanyInputModel.Handle,
                LogoUrl = registerCompanyInputModel.LogoUrl,
                Website = registerCompanyInputModel.Website,
                PhoneNumber = registerCompanyInputModel.PhoneNumber
            };

            var createdCompany = await companyRepository.CreateAsync(companyToCreate, cancellationToken);

            if (createdCompany is null)
            {
                return new ResponseDTO<Company>(false, "Failed to register the company.", RegisterCompanyErrorType.CouldNotCreateCompany);
            }

            var headquarterToCreate = new Location
            {
                IsHeadquarter = true,
                StreetName = registerCompanyInputModel.StreetName,
                City = registerCompanyInputModel.City,
                ZipCode = registerCompanyInputModel.ZipCode!.Value,
                CompanyId = createdCompany.Id
            };

            var createdHeadquarter = await locationRepository.CreateAsync(headquarterToCreate, cancellationToken);

            if (createdHeadquarter is null)
            {
                return new ResponseDTO<Company>(false, "Failed to register the company's headquarter.", RegisterCompanyErrorType.CouldNotCreateHeadquarter);
            }

            var packageToCreate = new Package
            {
                Name = registerCompanyInputModel.PackageName,
                Description = registerCompanyInputModel.PackageDescription,
                CompanyId = createdCompany.Id,
                LocationId = headquarterToCreate.Id,
            };

            var createdPackage = await packageRepository.CreateAsync(packageToCreate, cancellationToken);

            if (createdPackage is null)
            {
                return new ResponseDTO<Company>(false, "Company created, but failed to create package.", RegisterCompanyErrorType.CouldNotCreatePackage);
            }

            var primaryThemeToCreate = new Theme
            {
                IsPrimary = true,
                TextColor = registerCompanyInputModel.PrimaryTextColor,
                BackgroundColor = registerCompanyInputModel.PrimaryBackgroundColor,
                ContainerColor = registerCompanyInputModel.PrimaryContainerColor,
                ButtonColor = registerCompanyInputModel.PrimaryButtonColor,
                ButtonTextColor = registerCompanyInputModel.PrimaryButtonTextColor,
                NavigationBackgroundColor = registerCompanyInputModel.PrimaryNavigationBackgroundColor,
                NavigationButtonTextColor = registerCompanyInputModel.PrimaryNavigationButtonTextColor,
                CompanyId = createdCompany.Id,
            };

            var createdPrimaryTheme = await themeRepository.CreateAsync(primaryThemeToCreate, cancellationToken);

            if (createdPrimaryTheme is null)
            {
                return new ResponseDTO<Company>(false, "Failed to create the company's primary theme.", RegisterCompanyErrorType.CouldNotCreatePrimaryTheme);
            }

            return new ResponseDTO<Company>(true, content: createdCompany);
        }
        catch (OperationCanceledException ex)
        {
            return new ResponseDTO<Company>(false, "Your request was canceled.", GeneralErrorType.OperationWasCanceled, ex.Message, ex.InnerException?.Message);
        }
        catch (Exception ex)
        {
            return new ResponseDTO<Company>(false, "A server error has occurred - contact a server administrator.", GeneralErrorType.Unhandled, ex.Message, ex.InnerException?.Message);
        }
    }
}