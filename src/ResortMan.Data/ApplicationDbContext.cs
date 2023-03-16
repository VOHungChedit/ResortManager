using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ResortMan.Entities;

namespace ResortMan.Data;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Booking> Bookings { get; set; } = null!;

    public DbSet<Accomodation> Accomodations { get; set; } = null!;

    public DbSet<AccomodationPackage> AccomodationsPackages { get; set; } = null!;

    public DbSet<AccomodationType> AccomodationTypes { get; set; } = null!;
}