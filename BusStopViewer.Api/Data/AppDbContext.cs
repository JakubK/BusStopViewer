using BusStopViewer.Api.Data.Configuration;
using Microsoft.EntityFrameworkCore;

namespace BusStopViewer.Api.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UserConfiguration());
        modelBuilder.ApplyConfiguration(new UserStopConfiguration());
    }

    public virtual DbSet<User> Users { get; set; }
    public virtual DbSet<UserStop> UserStops { get; set; }

}