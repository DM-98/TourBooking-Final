using System.ComponentModel.DataAnnotations;

namespace TourBooking.Core.DTOs.Inputs;

public sealed class CreateLocationInputModel
{
	public Guid CompanyId { get; set; }

	[Required(ErrorMessage = "This field is required.")]
	[StringLength(50, ErrorMessage = "{0} can only consist of {1} characters.")]
	[Display(Name = "Street name")]
	public string StreetName { get; set; } = null!;

	[Required(ErrorMessage = "This field is required.")]
	[StringLength(50, ErrorMessage = "{0} can only consist of {1} characters.")]
	[Display(Name = "City")]
	public string City { get; set; } = null!;

	[Required(ErrorMessage = "This field is required.")]
	[Display(Name = "ZIP code")]
	public int? ZipCode { get; set; }
}