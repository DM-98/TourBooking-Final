using System.ComponentModel.DataAnnotations.Schema;

namespace TourBooking.Core.Domain;

public sealed class Employee : BaseEntity
{
	public Guid CompanyId { get; set; }

	[ForeignKey(nameof(CompanyId))]
	public Company Company { get; set; } = null!;

	public Guid ApplicationUserId { get; set; }

	[ForeignKey(nameof(ApplicationUserId))]
	public ApplicationUser ApplicationUser { get; set; } = null!;
}