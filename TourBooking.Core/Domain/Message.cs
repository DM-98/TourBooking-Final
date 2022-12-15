using System.ComponentModel.DataAnnotations.Schema;

namespace TourBooking.Core.Domain;

public sealed class Message : BaseEntity
{
	public required string Content { get; set; }

	public Guid BookingId { get; set; }

	[ForeignKey(nameof(BookingId))]
	public Booking Booking { get; set; } = null!;

	public Guid ApplicationUserId { get; set; }

	[ForeignKey(nameof(ApplicationUserId))]
	public ApplicationUser ApplicationUser { get; set; } = null!;
}