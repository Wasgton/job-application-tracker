using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace JobApplicationTracker.Infra.Database.Contexts;

public abstract class Context : DbContext
{
    protected ConfigurationBuilder ConfigurationBuilder = new();
    
    protected string GetConnectionString()
    {
        var section = ConfigurationBuilder
            .AddJsonFile("appsettings.json")
            .Build()
            .GetSection("Settings");
        
        return $"Server={section["Server"]},{section["Port"]};" +
               $"Database={section["Database"]};" +
               $"User ID={section["User"]};" +
               $"Password={section["Password"]};" +
               $"TrustServerCertificate=true;" +
               $"Trusted_Connection=false;";
    }
}