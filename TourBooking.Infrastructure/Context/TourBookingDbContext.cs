using Microsoft.AspNetCore.DataProtection.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TourBooking.Core.Domain;
using TourBooking.Core.Domain.Nomi4s;

namespace TourBooking.Infrastructure.Context;

public sealed class TourBookingDbContext : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>, IDataProtectionKeyContext
{
    public DbSet<Admin> Admin { get; set; }

    public DbSet<Employee> Employee { get; set; }

    public DbSet<Booker> Booker { get; set; }

    public DbSet<Booking> Booking { get; set; }

    public DbSet<Nomi4sBooking> Nomi4sBooking { get; set; }

    public DbSet<Company> Company { get; set; }

    public DbSet<Location> Location { get; set; }

    public DbSet<Log> Log { get; set; }

    public DbSet<Material> Material { get; set; }

    public DbSet<Message> Message { get; set; }

    public DbSet<Package> Package { get; set; }

    public DbSet<Theme> Theme { get; set; }

    public DbSet<DataProtectionKey> DataProtectionKeys { get; set; }

    public TourBookingDbContext(DbContextOptions<TourBookingDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<DataProtectionKey>().ToTable("DataProtectionKey");
        builder.Entity<ApplicationUser>().ToTable("User");
        builder.Entity<IdentityUserRole<Guid>>().ToTable("UsersRoles");
        builder.Entity<IdentityUserClaim<Guid>>().ToTable("UsersClaims");
        builder.Entity<IdentityUserLogin<Guid>>().ToTable("UsersLogins");
        builder.Entity<IdentityUserToken<Guid>>().ToTable("UsersTokens");
        builder.Entity<IdentityRole<Guid>>().ToTable("Role");
        builder.Entity<IdentityRoleClaim<Guid>>().ToTable("RolesClaims");
    }
}