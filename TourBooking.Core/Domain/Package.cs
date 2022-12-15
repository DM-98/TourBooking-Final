using System.ComponentModel.DataAnnotations.Schema;

namespace TourBooking.Core.Domain;

public sealed class Package : BaseEntity
{
	public required string Name { get; set; }

	public required string Description { get; set; }

	public Guid? LocationId { get; set; }

	[ForeignKey(nameof(LocationId))]
	public Location? Location { get; set; }

	public Guid CompanyId { get; set; }

	[ForeignKey(nameof(CompanyId))]
	public Company Company { get; set; } = null!;

	public ICollection<Booking>? Bookings { get; set; }
}