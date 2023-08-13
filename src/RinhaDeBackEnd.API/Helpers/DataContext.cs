using System.Data;
using Npgsql;

namespace RinhaDeBackEnd.API.Helpers;

public class DataContext
{
    private readonly IConfiguration _configuration;

    public DataContext(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public IDbConnection CreateConnection()
    {
        var connectionString = _configuration.GetConnectionString("DockerConnection");
        return new NpgsqlConnection(connectionString);
    }
}