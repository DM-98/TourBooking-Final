using System.ComponentModel.DataAnnotations.Schema;

namespace TourBooking.Core.Domain;

public sealed class Location : BaseEntity
{
	public required string StreetName { get; set; }

	public required string City { get; set; }

	public int ZipCode { get; set; }

	public bool IsHeadquarter { get; set; }

	public Guid CompanyId { get; set; }

	[ForeignKey(nameof(CompanyId))]
	public Company Company { get; set; } = null!;

	public ICollection<Package>? Packages { get; set; }
}