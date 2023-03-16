using Microsoft.EntityFrameworkCore;
using ResortMan.Entities;

namespace ResortMan.Data;

public class ResortDbContext : DbContext
{
    public ResortDbContext(DbContextOptions<ResortDbContext> options) : base(options)
    {
    }

    public DbSet<Booking> Bookings { get; set; } = null!;

    public DbSet<Accomodation> Accomodations { get; set; } = null!;

    public DbSet<AccomodationPackage> AccomodationsPackages { get; set; } = null!;

    public DbSet<AccomodationType> AccomodationTypes { get; set; } = null!;
}
