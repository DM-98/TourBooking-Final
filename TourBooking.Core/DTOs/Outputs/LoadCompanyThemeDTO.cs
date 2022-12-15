namespace TourBooking.Core.DTOs.Outputs;

public sealed record CompanyThemeDTO
{
	public Guid Id { get; set; }

	public required string Name { get; set; }

	public required string Handle { get; set; }

	public string? Website { get; set; }

	public required string LogoUrl { get; set; }

	public required string NavigationBackgroundColor { get; set; }

	public required string NavigationButtonTextColor { get; set; }

	public required string BackgroundColor { get; set; }

	public required string ContainerColor { get; set; }

	public required string TextColor { get; set; }

	public required string ButtonColor { get; set; }

	public required string ButtonTextColor { get; set; }
}