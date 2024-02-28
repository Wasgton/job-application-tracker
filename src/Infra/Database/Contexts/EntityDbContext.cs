using JobApplicationTracker.Domain.Entity.Job;
using Microsoft.EntityFrameworkCore;

namespace JobApplicationTracker.Infra.Database.Contexts;

public class EntityDbContext : Context
{
    public DbSet<Job>? Jobs { get; set; }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        string connectionString = GetConnectionString();
        optionsBuilder.UseSqlServer(connectionString,options=>options.EnableRetryOnFailure());
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Job>().ToTable("Job");
    }
    
} 