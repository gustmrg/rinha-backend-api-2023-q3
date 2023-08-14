using System.Data;
using Dapper;
using Npgsql;
using RinhaDeBackEnd.API.Interfaces;

namespace RinhaDeBackEnd.API.Helpers;

public class DataContext
{
    private readonly IDbContext _dbContext;

    public DataContext(IDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public IDbConnection CreateConnection()
    {
        var connectionString = _dbContext.ConnectionString;
        return new NpgsqlConnection(connectionString);
    }

    public async Task Init()
    {
        await CreateDatabase();
        await CreateTables();
        await CreateIndexes();
    }

    private async Task CreateDatabase()
    {
        var connectionString = "Server=localhost;Port=5432;Database=postgres;User Id=postgres;Password=postgrespw";
        using var connection = new NpgsqlConnection(connectionString);

        // Check if database already exists
        var sqlDbCount = $"SELECT COUNT(*) FROM pg_database WHERE datname = 'rinha';";
        var dbCount = await connection.ExecuteScalarAsync<int>(sqlDbCount);
        if (dbCount == 0)
        {
            var sql = "CREATE DATABASE rinha";
            await connection.ExecuteAsync(sql);
        }
    }

    private async Task CreateTables()
    {
        using var connection = CreateConnection();
        var sql = @"CREATE TABLE IF NOT EXISTS pessoas (
                        id UUID PRIMARY KEY,
                        apelido VARCHAR(32) UNIQUE NOT NULL,
                        nome VARCHAR(100) NOT NULL,
                        nascimento DATE NOT NULL,
                        stack VARCHAR[] NULL
                        );";

        await connection.ExecuteAsync(sql);
    }

    private async Task CreateIndexes()
    {
        using var connection = CreateConnection();

        var sql = @"BEGIN;
	                    CREATE INDEX IF NOT EXISTS idx_pessoas_nome ON pessoas USING btree(nome);
	                    CREATE INDEX IF NOT EXISTS idx_pessoas_apelido ON pessoas USING btree(apelido);
	                    CREATE INDEX IF NOT EXISTS idx_pessoas_stack ON pessoas USING gin(stack);
                    COMMIT;";

        await connection.ExecuteAsync(sql);
    }
}