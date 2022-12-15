using System.ComponentModel.DataAnnotations.Schema;
using TourBooking.Core.Enums;
using TourBooking.Core.Interfaces;

namespace TourBooking.Core.Domain;

public sealed class Booking : BaseEntity, IAggregateRoot
{
	public DateTime DateTime { get; set; }

	public DateTime? AlternativeDateTime { get; set; }

	public int Attendees { get; set; }

	public string? Remark { get; set; }

	public required BookingStatus BookingStatus { get; set; }

	public Guid PackageId { get; set; }

	[ForeignKey(nameof(PackageId))]
	public Package Package { get; set; } = null!;

	public Guid BookerId { get; set; }

	[ForeignKey(nameof(BookerId))]
	public Booker Booker { get; set; } = null!;

	public ICollection<Material>? Materials { get; set; }

	public ICollection<Message>? Messages { get; set; }
}