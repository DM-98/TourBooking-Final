using TourBooking.Core.Enums;

namespace TourBooking.Core.DTOs.Outputs;

public sealed record BookingListDTO
{
	public Guid Id { get; set; }

	public BookingStatus BookingStatus { get; set; }

	public required string Organization { get; set; }

	public DateTime DateTime { get; set; }

	public required string PackageName { get; set; }

	public required string UserName { get; set; }

	public required string Email { get; set; }

	public string? PhoneNumber { get; set; }

	public DateTime CreatedDate { get; set; }
}