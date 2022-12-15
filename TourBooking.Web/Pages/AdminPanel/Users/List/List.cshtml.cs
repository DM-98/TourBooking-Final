using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TourBooking.Core.DTOs.Outputs;
using TourBooking.Core.Interfaces;

namespace TourBooking.Web.Pages.AdminPanel.Users.List;

[Authorize(Roles = "Admin")]
public class ListModel : BasePageModel
{
    private readonly IUserService userService;

    public IEnumerable<EmployeeListDTO>? Employees { get; set; }

    public IEnumerable<AdminListDTO>? Admins { get; set; }

    public IEnumerable<BookerListDTO>? Bookers { get; set; }

    public ListModel(IUserService userService)
    {
        this.userService = userService;
    }

    public async Task<IActionResult> OnGetAsync()
    {
        var adminsResult = await userService.GetAdminListAsync();

        if (adminsResult.IsSuccess)
        {
            Admins = adminsResult.Content!;
        }
        else
        {
            DisplayError(adminsResult);
        }

        var bookersResult = await userService.GetBookerListAsync();

        if (bookersResult.IsSuccess)
        {
            Bookers = bookersResult.Content!;
        }
        else
        {
            DisplayError(bookersResult);
        }

        var employeesResult = await userService.GetEmployeeListAsync();

        if (employeesResult.IsSuccess)
        {
            Employees = employeesResult.Content!;
        }
        else
        {
            DisplayError(employeesResult);
        }

        return Page();
    }
}