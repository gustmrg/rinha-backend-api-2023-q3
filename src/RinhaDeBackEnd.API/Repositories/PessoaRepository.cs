using System.Data;
using Dapper;
using Npgsql;
using RinhaDeBackEnd.API.Interfaces;
using RinhaDeBackEnd.API.Models;

namespace RinhaDeBackEnd.API.Repositories;

public class PessoaRepository : IPessoaRepository
{
    private readonly IConfiguration _configuration;
    private readonly IDbConnection _dbConnection;

    public PessoaRepository(IConfiguration configuration)
    {
        _configuration = configuration;
        _dbConnection = new NpgsqlConnection(_configuration.GetConnectionString("DockerConnection"));
    }
    
    public async Task<Pessoa> Add(Pessoa pessoa)
    {
        pessoa.Id = Guid.NewGuid();
        
        await _dbConnection.ExecuteAsync("INSERT INTO pessoas VALUES (@Id, @Apelido, @Nome, @Nascimento, @Stack)",
            new {
            id = pessoa.Id,
            apelido = pessoa.Apelido,
            nome = pessoa.Nome,
            nascimento = pessoa.Nascimento,
            stack = pessoa.Stack
            });

        return pessoa;
    }
    
    public IEnumerable<Pessoa> Get()
    {
        var pessoas = _dbConnection.Query<Pessoa>(
            "SELECT Id, Apelido, Nome, Nascimento, Stack FROM pessoas");
        
        return pessoas;
    }
    
    public async Task<Pessoa> GetById(Guid id)
    {
        var pessoa = await _dbConnection.QuerySingleOrDefaultAsync<Pessoa>(
            @"SELECT Id, Apelido, Nome, Nascimento, Stack FROM pessoas WHERE Id = @Id", 
            new { Id = id });
        
        return pessoa;
    }
    
    public async Task<IEnumerable<Pessoa>> GetByFilter(string text)
    {
        var pessoas = await _dbConnection.QueryAsync<Pessoa>(
            "GetByFilter", 
            text, 
            commandType: CommandType.StoredProcedure);

        return pessoas;
    }


    public void UpdatePerson()
    {
        throw new NotImplementedException(); 
    }

    public void DeletePerson()
    {
        throw new NotImplementedException();
    }

    public async Task<int> Count()
    {
        var count = await _dbConnection.ExecuteScalarAsync<int>("SELECT COUNT(*) FROM pessoas");

        return count;
    }
}