using System.ComponentModel.DataAnnotations.Schema;

namespace TourBooking.Core.Domain;

public sealed class Material : BaseEntity
{
	public required string Name { get; set; }

	public Guid CompanyId { get; set; }

	[ForeignKey(nameof(CompanyId))]
	public Company Company { get; set; } = null!;

	public Guid? BookingId { get; set; }

	[ForeignKey(nameof(BookingId))]
	public Booking? Booking { get; set; }
}