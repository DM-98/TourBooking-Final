using Microsoft.AspNetCore.Identity;
using TourBooking.Core.Domain;
using TourBooking.Core.Interfaces;
using TourBooking.Infrastructure.Context;
using TourBooking.Infrastructure.EmailSenders;
using TourBooking.Infrastructure.Repositories;
using TourBooking.Infrastructure.Services;

namespace TourBooking.Web.CompositionRoot;

public static class ConfigureServices
{
    public static IServiceCollection ConfigureTourBookingServices(this IServiceCollection services)
    {
        services.AddScoped<IRepository<Admin>, EFRepository<Admin, TourBookingDbContext>>();
        services.AddScoped<IRepository<Employee>, EFRepository<Employee, TourBookingDbContext>>();
        services.AddScoped<IRepository<Booker>, EFRepository<Booker, TourBookingDbContext>>();
        services.AddScoped<IUserService, UserService>();

        services.AddScoped<IAuthService, AuthService>();

        services.AddScoped<IRepository<Company>, EFRepository<Company, TourBookingDbContext>>();
        services.AddScoped<IRepository<Location>, EFRepository<Location, TourBookingDbContext>>();
        services.AddScoped<IRepository<Theme>, EFRepository<Theme, TourBookingDbContext>>();
        services.AddScoped<IRepository<Package>, EFRepository<Package, TourBookingDbContext>>();
        services.AddScoped<IRepository<Material>, EFRepository<Material, TourBookingDbContext>>();
        services.AddTransient<ICompanyService, CompanyService>();

        services.AddScoped<IRepository<Booking>, EFRepository<Booking, TourBookingDbContext>>();
        services.AddScoped<IRepository<Message>, EFRepository<Message, TourBookingDbContext>>();
        services.AddTransient<IBookingService, BookingService>();

        services.AddScoped<IEmailSender, SMTPEmailSender>();

        return services;
    }

    public static IdentityBuilder AddPasswordlessLoginTotpTokenProvider(this IdentityBuilder builder)
    {
        var userType = builder.UserType;
        var totpProvider = typeof(PasswordlessLoginTotpTokenProvider<>).MakeGenericType(userType);

        return builder.AddTokenProvider("PasswordlessLoginTotpProvider", totpProvider);
    }
}