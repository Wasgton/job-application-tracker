using System.Data;
using Microsoft.Data.SqlClient;

namespace JobApplicationTracker.Infra.Database.Contexts;

public class DapperDbContext : Context
{
    public readonly IDbConnection Connection;

    public DapperDbContext()
    {
        string connectionString = GetConnectionString();
        Connection = new SqlConnection(connectionString);
    }

}