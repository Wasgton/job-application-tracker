
using JobApplicationTracker.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace JobApplicationTracker.Infra.Database.Contexts;

public class EntityDbContext : Context
{
    public DbSet<Job>? Jobs { get; set; }
    public DbSet<User>? Users { get; set; }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        string connectionString = GetConnectionString();
        optionsBuilder
            .UseSqlServer(connectionString,options=>options.EnableRetryOnFailure())
            .EnableSensitiveDataLogging()
            .LogTo(Console.WriteLine);
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Job>().ToTable("job");
        modelBuilder.Entity<User>().ToTable("user");
        base.OnModelCreating(modelBuilder);
    }

} 