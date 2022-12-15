using System.ComponentModel.DataAnnotations.Schema;

namespace TourBooking.Core.Domain;

public sealed class Admin : BaseEntity
{
	public Guid ApplicationUserId { get; set; }

	[ForeignKey(nameof(ApplicationUserId))]
	public ApplicationUser ApplicationUser { get; set; } = null!;
}