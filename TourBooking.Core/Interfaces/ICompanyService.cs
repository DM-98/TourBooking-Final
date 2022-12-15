using TourBooking.Core.Domain;
using TourBooking.Core.DTOs.Inputs;
using TourBooking.Core.DTOs.Outputs;

namespace TourBooking.Core.Interfaces;

public interface ICompanyService : IBaseService<Company>
{
	Task<ResponseDTO<Company>> RegisterCompanyAsync(RegisterCompanyInputModel registerCompanyInputModel, CancellationToken cancellationToken = default);

	Task<ResponseDTO<CompanyThemeDTO>> LoadCompanyThemeAsync(string handle, CancellationToken cancellationToken = default);

	Task<ResponseDTO<Location>> CreateLocationAsync(CreateLocationInputModel createLocationInputModel, CancellationToken cancellationToken = default);

	Task<ResponseDTO<Package>> CreatePackageAsync(CreatePackageInputModel createPackageInputModel, CancellationToken cancellationToken = default);

	Task<ResponseDTO<Material>> CreateMaterialAsync(CreateMaterialInputModel createPackageInputModel, CancellationToken cancellationToken = default);

	Task<ResponseDTO<CompanyDetailsDTO>> GetCompanyDetailsAsync(string handle, CancellationToken cancellationToken = default);

	Task<ResponseDTO<IEnumerable<CompanyListDTO>>> GetCompanyListAsync(CancellationToken cancellationToken = default);

	Task<ResponseDTO<IEnumerable<CompanyPackageListDTO>>> GetCompanyPackageListAsync(string handle, CancellationToken cancellationToken = default);

	Task<ResponseDTO<IEnumerable<CompanyLocationListDTO>>> GetCompanyLocationListAsync(string handle, CancellationToken cancellationToken = default);

	Task<ResponseDTO<IEnumerable<CompanyMaterialListDTO>>> GetCompanyMaterialListAsync(string handle, CancellationToken cancellationToken = default);

	Task<ResponseDTO<IEnumerable<EmployeesEmailListDTO>>> GetAllEmployeesEmailListAsync(string handle, CancellationToken cancellationToken = default);
}