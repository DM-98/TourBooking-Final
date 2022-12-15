using TourBooking.Core.Constants;
using TourBooking.Core.Enums;

namespace TourBooking.Core.Helpers;

public static class ExtensionMethods
{
    public static string EnumToString(this CompanyHandleList companyHandleList)
    {
        return companyHandleList switch
        {
            CompanyHandleList.Axla => CompanyHandleList.Axla.ToString().ToLowerInvariant(),
            CompanyHandleList.Nomi4s => CompanyHandleList.Nomi4s.ToString().ToLowerInvariant(),
            _ => "No specified company found.",
        };
    }

    public static string EnumToString(this BookingStatus bookingStatus)
    {
        return bookingStatus switch
        {
            BookingStatus.Active => BookingStatus.Active.ToString(),
            BookingStatus.Closed => BookingStatus.Closed.ToString(),
            _ => "Current booking status was invalid.",
        };
    }

    public static string FormatTime(this DateTime? dateTime)
    {
        return dateTime.HasValue ? dateTime.Value.ToLocalTime().ToString("d. MMM yyyy kl. HH:mm") : "N/A";
    }

    public static string FormatTime(this DateTime dateTime)
    {
        return dateTime.ToLocalTime().ToString("d. MMM yyyy kl. HH:mm");
    }

    public static string FormatReturnUrl(this string? returnUrl, string? fallbackPage = default)
    {
        if (string.IsNullOrWhiteSpace(returnUrl) || returnUrl.EndsWith("fejl") || returnUrl.EndsWith("uautoriseret"))
        {
            returnUrl = fallbackPage ?? Globals.DefaultCompanyRouteName;
        }

        return "~/" + returnUrl;
    }
}