using System.ComponentModel.DataAnnotations.Schema;

namespace TourBooking.Core.Domain;

public sealed class Log : BaseEntity
{
	public required string Action { get; set; }

	public required string Page { get; set; }

	public Guid ApplicationUserId { get; set; }

	[ForeignKey(nameof(ApplicationUserId))]
	public ApplicationUser ApplicationUser { get; set; } = null!;
}