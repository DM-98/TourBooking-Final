using TourBooking.Core.Interfaces;

namespace TourBooking.Core.Domain;

public sealed class Company : BaseEntity, IAggregateRoot
{
	public required string Name { get; set; }

	public required string Handle { get; set; }

	public required string LogoUrl { get; set; }

	public required string PhoneNumber { get; set; }

	public string? Website { get; set; }

	public ICollection<Employee>? Employees { get; set; }

	public ICollection<Theme>? Themes { get; set; }

	public ICollection<Location>? Locations { get; set; }

	public ICollection<Package>? Packages { get; set; }

	public ICollection<Material>? Materials { get; set; }
}