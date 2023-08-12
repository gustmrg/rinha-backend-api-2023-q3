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
    
    public Pessoa Add(Pessoa pessoa)
    {
        pessoa.Id = Guid.NewGuid();
 
        string sqlCommand = "INSERT INTO pessoas VALUES (@Id, @Apelido, @Nome, @Nascimento)";
        
        _dbConnection.Execute(sqlCommand, new
        {
            id = pessoa.Id,
            apelido = pessoa.Apelido,
            nome = pessoa.Nome,
            nascimento = pessoa.Nascimento
        });
        
        _dbConnection.Dispose();

        return pessoa;
    }
    
    public IEnumerable<Pessoa> Get()
    {
        string sqlCommand = "SELECT Id, Apelido, Nome, Nascimento FROM pessoas";

        var pessoas = _dbConnection.Query<Pessoa>(sqlCommand);  
        
        _dbConnection.Dispose();
        
        return pessoas;
    }
    
    public Pessoa GetById(Guid id)
    {
        var pessoa = _dbConnection.QuerySingleOrDefault<Pessoa>(
            @"SELECT Id, Apelido, Nome, Nascimento FROM pessoas WHERE id = @Id", new { Id = id });
        
        _dbConnection.Dispose();
        
        return pessoa;
    }

    public void UpdatePerson()
    {
        throw new NotImplementedException(); 
    }

    public void DeletePerson()
    {
        throw new NotImplementedException();
    }

    public int Count()
    {
        string sqlCommand = "SELECT COUNT(*) FROM pessoas";

        var count = _dbConnection.ExecuteScalar<int>(sqlCommand);

        return count;
    }
}