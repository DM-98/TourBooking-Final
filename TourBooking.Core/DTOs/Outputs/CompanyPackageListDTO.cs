using TourBooking.Core.Domain;

namespace TourBooking.Core.DTOs.Outputs;

public sealed record CompanyPackageListDTO
{
	public Guid Id { get; set; }

	public required string Name { get; set; }

	public required string Description { get; set; }

	public string? StreetName { get; set; }

	public string? City { get; set; }

	public int? ZipCode { get; set; }

	public DateTime CreatedDate { get; set; }

	public int BookingsCount { get; set; }
}