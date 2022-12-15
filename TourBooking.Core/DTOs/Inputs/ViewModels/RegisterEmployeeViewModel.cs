using TourBooking.Core.Domain;

namespace TourBooking.Core.DTOs.Inputs.ViewModels;

public sealed class RegisterEmployeeViewModel
{
    public RegisterEmployeeInputModel RegisterEmployeeInputModel { get; set; }

    public IEnumerable<Company>? Companies { get; set; }

    public RegisterEmployeeViewModel(RegisterEmployeeInputModel registerEmployeeInputModel, IEnumerable<Company>? companies)
    {
        Companies = companies;
        RegisterEmployeeInputModel = registerEmployeeInputModel;
    }
}