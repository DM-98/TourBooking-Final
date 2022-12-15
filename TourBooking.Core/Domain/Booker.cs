using System.ComponentModel.DataAnnotations.Schema;

namespace TourBooking.Core.Domain;

public sealed class Booker : BaseEntity
{
	public required string Organization { get; set; }

	public Guid ApplicationUserId { get; set; }

	[ForeignKey(nameof(ApplicationUserId))]
	public ApplicationUser ApplicationUser { get; set; } = null!;
}