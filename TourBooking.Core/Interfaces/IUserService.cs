using TourBooking.Core.Domain;
using TourBooking.Core.DTOs.Inputs;
using TourBooking.Core.DTOs.Outputs;

namespace TourBooking.Core.Interfaces;

public interface IUserService
{
    Task<ResponseDTO<Admin>> RegisterAdminAsync(RegisterAdminInputModel registerAdminInputModel, CancellationToken cancellationToken = default);

    Task<ResponseDTO<Employee>> RegisterEmployeeAsync(RegisterEmployeeInputModel registerEmployeeInputModel, CancellationToken cancellationToken = default);

    Task<ResponseDTO<IEnumerable<EmployeeListDTO>>> GetEmployeeListAsync(CancellationToken cancellationToken = default);

    Task<ResponseDTO<IEnumerable<AdminListDTO>>> GetAdminListAsync(CancellationToken cancellationToken = default);

    Task<ResponseDTO<IEnumerable<BookerListDTO>>> GetBookerListAsync(CancellationToken cancellationToken = default);

    Task<ResponseDTO<Booker>> ConfirmEmailAsync(Guid id, string confirmToken, CancellationToken cancellationToken = default);

    Task<ResponseDTO<Booker>> RegisterBookerAsync(RegisterBookerInputModel registerInputModel, CancellationToken cancellationToken = default);
}