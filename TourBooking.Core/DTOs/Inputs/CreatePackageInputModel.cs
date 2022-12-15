using System.ComponentModel.DataAnnotations;

namespace TourBooking.Core.DTOs.Inputs;

public sealed class CreatePackageInputModel
{
	public Guid CompanyId { get; set; }

	[Required(ErrorMessage = "This field is required.")]
	[StringLength(75, ErrorMessage = "{0} can only consist of {1} characters.")]
	[Display(Name = "Package name")]
	public string Name { get; set; } = null!;

	[Required(ErrorMessage = "This field is required.")]
	[StringLength(255, ErrorMessage = "{0} can only consist of {1} characters.")]
	[Display(Name = "Package description")]
	public string Description { get; set; } = null!;

	[Display(Name = "Choose a location for the package")]
	public Guid LocationId { get; set; }
}