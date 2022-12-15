namespace TourBooking.Core.DTOs.Outputs;

public sealed record CompanyListDTO
{
	public Guid Id { get; set; }

	public required string Name { get; set; }

	public required string Handle { get; set; }

	public required string HeadquarterStreetName { get; set; }

	public required string HeadquarterCity { get; set; }

	public int HeadquarterZipCode { get; set; }

	public int BookingsCount { get; set; }

	public int EmployeesCount { get; set; }

	public int LocationsCount { get; set; }

	public int PackagesCount { get; set; }

	public int MaterialsCount { get; set; }

	public DateTime CreatedDate { get; set; }
}