using System.ComponentModel.DataAnnotations;

namespace TourBooking.Core.DTOs.Inputs;

public sealed class CreateMessageInputModel
{
	[Required(ErrorMessage = "This field is required.")]
	[StringLength(255, ErrorMessage = "{0} can only consist of {1} characters.")]
	[DataType(DataType.MultilineText)]
	[Display(Name = "Message")]
	public string Content { get; set; } = null!;

	public Guid BookingId { get; set; }

	public Guid ApplicationUserId { get; set; }
}