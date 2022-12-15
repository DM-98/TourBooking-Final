namespace TourBooking.Core.DTOs.Outputs;

public sealed record BookerListDTO
{
	public Guid Id { get; set; }

	public required string Name { get; set; }

	public required string Email { get; set; }

	public string? PhoneNumber { get; set; }

	public required string Organization { get; set; }

	public DateTime CreatedDate { get; set; }
}