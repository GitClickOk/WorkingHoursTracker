using Microsoft.EntityFrameworkCore;

namespace WorkingHoursTracker.Models;

public class WorkingHoursDbContext : DbContext
{
    public WorkingHoursDbContext (DbContextOptions<WorkingHoursDbContext> options)
        : base(options)
    {
        Database.EnsureDeleted();
        Database.EnsureCreated();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .Entity<AppUser>()
            .HasData(
                new AppUser { AppUserId = 1, FirstName = "John", LastName = "Doe", PassCode = "12345", WorkingStatus = WorkingStatus.Working },
                new AppUser { AppUserId = 2, FirstName = "Patrick", PassCode = "12345", LastName = "Jackson" },
                new AppUser { AppUserId = 3, FirstName = "Jack", PassCode = "12345", LastName = "Samuels" }
                );

        modelBuilder
            .Entity<WorkingHistory>()
            .HasData(
                new WorkingHistory { AppUserId = 1, WorkingHistoryId = 1, Start = DateTime.Now.AddHours(-2.5) }
                );

    }

    public DbSet<AppUser> AppUsers => Set<AppUser>();

    public DbSet<WorkingHistory> WorkingHistory => Set<WorkingHistory>();
}