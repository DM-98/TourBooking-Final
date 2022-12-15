using TourBooking.Core.Domain;

namespace TourBooking.Core.DTOs.Outputs;

public sealed record CompanyDetailsDTO
{
	public Guid Id { get; set; }

	public required string Name { get; set; }

	public required string Handle { get; set; }

	public required string PhoneNumber { get; set; }

	public required string LogoUrl { get; set; }

	public string? Website { get; set; }

	public required string HeadquarterStreetName { get; set; }

	public required string HeadquarterCity { get; set; }

	public required int HeadquarterZipCode { get; set; }

	public required string PrimaryBackgroundColor { get; set; }

	public required string PrimaryContainerColor { get; set; }

	public required string PrimaryTextColor { get; set; }

	public required string PrimaryNavigationBackgroundColor { get; set; }

	public required string PrimaryNavigationButtonTextColor { get; set; }

	public required string PrimaryButtonColor { get; set; }

	public required string PrimaryButtonTextColor { get; set; }

	public int BookingsCount { get; set; }

	public IEnumerable<Employee>? Employees { get; set; }

	public IEnumerable<Package>? Packages { get; set; }

	public IEnumerable<Material>? Materials { get; set; }

	public IEnumerable<Location>? Locations { get; set; }
}