namespace TourBooking.Core.DTOs.Outputs;

public sealed record CompanyMaterialListDTO
{
	public Guid Id { get; set; }

	public required string Name { get; set; }

	public Guid? BookingId { get; set; }

	public string? BookerName { get; set; }
}