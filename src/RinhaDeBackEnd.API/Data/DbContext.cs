using RinhaDeBackEnd.API.Interfaces;

namespace RinhaDeBackEnd.API.Data;

public class DbContext : IDbContext
{
    public string ConnectionString { get; }

    public DbContext()
    {
        this.ConnectionString = Environment.GetEnvironmentVariable("DB_CONNECTION_STRING") ?? "Connection string not found";
    }
}