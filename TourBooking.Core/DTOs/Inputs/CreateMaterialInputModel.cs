using System.ComponentModel.DataAnnotations;

namespace TourBooking.Core.DTOs.Inputs;

public sealed class CreateMaterialInputModel
{
	public Guid CompanyId { get; set; }

	[Required(ErrorMessage = "This field is required.")]
	[StringLength(50, ErrorMessage = "{0} can only consist of {1} characters.")]
	[Display(Name = "The name of the material")]
	public string Name { get; set; } = null!;
}