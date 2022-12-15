using Microsoft.AspNetCore.Identity;
using TourBooking.Core.Enums;

namespace TourBooking.Core.Domain;

public sealed class ApplicationUser : IdentityUser<Guid>
{
	public bool IsEmailNotificationEnabled { get; set; }

	public string? RefreshToken { get; set; }

	public DateTime? RefreshTokenExpiry { get; set; }

	public DateTime? DeletionRequestedDate { get; set; }

	public RoleType RoleType { get; set; }
}