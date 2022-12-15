namespace TourBooking.Core.DTOs.Outputs;

public sealed record EmployeesEmailListDTO
{
	public bool IsEmailNotificationEnabled { get; set; }

	public required string Email { get; set; }
}