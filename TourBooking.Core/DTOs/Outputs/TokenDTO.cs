using System.Diagnostics.CodeAnalysis;

namespace TourBooking.Core.DTOs.Outputs;

public sealed record TokenDTO
{
	public required string AccessToken { get; init; }

	public required string RefreshToken { get; init; }

	[SetsRequiredMembers]
	public TokenDTO(string accessToken, string refreshToken)
	{
		AccessToken = accessToken;
		RefreshToken = refreshToken;
	}
}