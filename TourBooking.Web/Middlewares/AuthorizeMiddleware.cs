using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Http.Extensions;
using System.Security.Cryptography;
using TourBooking.Core.DTOs.Outputs;
using TourBooking.Core.Interfaces;
using TourBooking.Infrastructure.Helpers;

namespace TourBooking.Web.Middlewares;

public sealed class AuthorizeMiddleware
{
    private readonly RequestDelegate next;

    public AuthorizeMiddleware(RequestDelegate next)
    {
        this.next = next;
    }

    public async Task InvokeAsync(HttpContext context, IAuthService authService, IDataProtectionProvider dataProtectionProvider, IConfiguration configuration)
    {
        if (!context.Response.HasStarted)
        {
            var encryptedAccessToken = context.Request.Cookies["AccessToken"];
            var encryptedRefreshToken = context.Request.Cookies["RefreshToken"];

            if (!string.IsNullOrWhiteSpace(encryptedAccessToken))
            {
                try
                {
                    var dataProtector = dataProtectionProvider.CreateProtector("ProtectCookieValues");
                    var accessToken = dataProtector.Unprotect(encryptedAccessToken);

                    var claims = AuthHelper.GetClaims(accessToken);

                    if (claims?.Any() ?? false)
                    {
                        var accessTokenExpiry = DateTimeOffset.FromUnixTimeSeconds(long.Parse(claims.First(x => x.Type is "exp").Value));

                        if (accessTokenExpiry < DateTime.UtcNow.AddMinutes(1))
                        {
                            if (!string.IsNullOrWhiteSpace(encryptedRefreshToken))
                            {
                                var refreshToken = dataProtector.Unprotect(encryptedRefreshToken);

                                var newAccessToken = await RefreshTokenAsync(context, authService, dataProtector, configuration, accessToken, refreshToken);

                                if (!string.IsNullOrWhiteSpace(newAccessToken))
                                {
                                    accessToken = newAccessToken;
                                }
                            }
                        }
                    }

                    context.Request.Headers.Add("Authorization", "Bearer " + accessToken);
                }
                catch (CryptographicException)
                {
                    context.Response.Cookies.Delete("RememberMe", new CookieOptions() { Secure = true });
                    context.Response.Cookies.Delete("AccessToken", new CookieOptions() { Secure = true });
                    context.Response.Cookies.Delete("RefreshToken", new CookieOptions() { Secure = true });

                    context.Response.Redirect("/" + context.Request.Path.Value?.Split('/', '/')[1] + "/uautoriseret" + "?returnUrl=" + context.Request.GetEncodedPathAndQuery());

                    await next(context);
                }
            }

            await next(context);
        }

        if ((context.Response.StatusCode is StatusCodes.Status401Unauthorized || context.Response.StatusCode is StatusCodes.Status403Forbidden) && !context.Request.Path.StartsWithSegments("/api"))
        {
            context.Response.Redirect("/" + context.Request.Path.Value?.Split('/', '/')[1] + "/uautoriseret" + "?returnUrl=" + context.Request.GetEncodedPathAndQuery());
        }

        if (context.Response.StatusCode == StatusCodes.Status404NotFound && !context.Request.Path.StartsWithSegments("/api"))
        {
            context.Response.Redirect("/error");
        }
    }

    public async Task<string?> RefreshTokenAsync(HttpContext context, IAuthService authService, IDataProtector dataProtector, IConfiguration configuration, string accessToken, string refreshToken)
    {
        var refreshTokenResult = await authService.RefreshTokensAsync(new TokenDTO(accessToken, refreshToken));

        if (refreshTokenResult.IsSuccess)
        {
            var rememberMe = !string.IsNullOrWhiteSpace(context.Request.Cookies["RememberMe"]) ? dataProtector.Unprotect(context.Request.Cookies["RememberMe"]!) : string.Empty;

            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                SameSite = SameSiteMode.Lax,
                Expires = rememberMe is "true" ? DateTimeOffset.UtcNow.AddDays(Convert.ToDouble(configuration["JWT:RefreshTokenExpiryInDays"])) : null,
            };

            context.Response.Cookies.Append("RememberMe", dataProtector.Protect(rememberMe), cookieOptions);
            context.Response.Cookies.Append("AccessToken", dataProtector.Protect(refreshTokenResult.Content!.AccessToken), cookieOptions);
            context.Response.Cookies.Append("RefreshToken", dataProtector.Protect(refreshTokenResult.Content!.RefreshToken), cookieOptions);

            return refreshTokenResult.Content!.AccessToken;
        }

        return null;
    }
}

public static class AuthorizeMiddlewareExtensions
{
    public static IApplicationBuilder UseCookieJWTAuthorize(this IApplicationBuilder app)
    {
        return app.UseMiddleware<AuthorizeMiddleware>();
    }
}