namespace TourBooking.Core.DTOs.Outputs;

public sealed record CompanyLocationListDTO
{
	public Guid Id { get; set; }

	public bool IsHeadquarter { get; set; }

	public required string StreetName { get; set; }

	public required string City { get; set; }

	public int ZipCode { get; set; }

	public DateTime CreatedDate { get; set; }

	public int PackagesCount { get; set; }
}