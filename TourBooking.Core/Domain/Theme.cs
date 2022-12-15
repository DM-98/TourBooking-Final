using System.ComponentModel.DataAnnotations.Schema;

namespace TourBooking.Core.Domain;

public sealed class Theme : BaseEntity
{
	public bool IsPrimary { get; set; }

	public required string TextColor { get; set; }

	public required string BackgroundColor { get; set; }

	public required string ContainerColor { get; set; }

	public required string ButtonColor { get; set; }

	public required string ButtonTextColor { get; set; }

	public required string NavigationBackgroundColor { get; set; }

	public required string NavigationButtonTextColor { get; set; }

	public Guid CompanyId { get; set; }

	[ForeignKey(nameof(CompanyId))]
	public Company ComapnyId { get; set; } = null!;
}