using Microsoft.AspNetCore.Mvc;
using TourBooking.Core.Interfaces;

namespace TourBooking.Web.Pages.Home;

public class HomeModel : BasePageModel
{
	public HomeModel(ICompanyService companyService) : base(companyService)
	{
	}

	public async Task<IActionResult> OnGetAsync(string handle)
	{
		if (!await LoadCompanyThemeAsync(handle))
		{
			return LocalRedirect(LocalErrorPage);
		}

		return Page();
	}
}