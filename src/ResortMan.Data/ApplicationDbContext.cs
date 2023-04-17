using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ResortMan.Entities;

namespace ResortMan.Data;

public class ApplicationDbContext : IdentityDbContext<
    ApplicationUser, IdentityRole, string,
    IdentityUserClaim<string>, ApplicationUserRole, IdentityUserLogin<string>,
    IdentityRoleClaim<string>, IdentityUserToken<string>>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Booking> Bookings { get; set; } = null!;

    public DbSet<Accomodation> Accomodations { get; set; } = null!;

    public DbSet<AccomodationPackage> AccomodationsPackages { get; set; } = null!;

    public DbSet<AccomodationType> AccomodationTypes { get; set; } = null!;

    public DbSet<AccomodationPackagePicture> AccomodationPackagePictures { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<ApplicationUser>()
            .HasMany(e => e.UserRoles)
            .WithOne(ur => ur.User)
            .HasForeignKey(ur => ur.UserId)
            .IsRequired();

        builder.Entity<IdentityRole>()
            .HasMany<ApplicationUserRole>()
            .WithOne(e => e.Role)
            .HasForeignKey(ur => ur.RoleId)
            .IsRequired();
    }
}