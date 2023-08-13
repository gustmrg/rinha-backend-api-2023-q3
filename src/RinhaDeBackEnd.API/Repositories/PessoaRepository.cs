using System.Data;
using Dapper;
using Npgsql;
using RinhaDeBackEnd.API.Helpers;
using RinhaDeBackEnd.API.Interfaces;
using RinhaDeBackEnd.API.Models;

namespace RinhaDeBackEnd.API.Repositories;

public class PessoaRepository : IPessoaRepository
{
    private readonly DataContext _context;

    public PessoaRepository(DataContext context)
    {
        _context = context;
    }
    
    public async Task<Pessoa> Add(Pessoa pessoa)
    {
        pessoa.Id = Guid.NewGuid();
        
        using var connection = _context.CreateConnection();
        
        await connection.ExecuteAsync("INSERT INTO pessoas VALUES (@Id, @Apelido, @Nome, @Nascimento, @Stack)",
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
        using var connection = _context.CreateConnection();
        
        var pessoas = connection.Query<Pessoa>(
            "SELECT Id, Apelido, Nome, Nascimento, Stack FROM pessoas");
        
        return pessoas;
    }

    public async Task<Pessoa> GetById(Guid id)
    {
        using var connection = _context.CreateConnection();
        
        var pessoa = await connection.QuerySingleOrDefaultAsync<Pessoa>(
            @"SELECT Id, Apelido, Nome, Nascimento, Stack FROM pessoas WHERE Id = @Id", 
            new { Id = id });
        
        return pessoa;
    }
    
    public async Task<IEnumerable<Pessoa>> FindByTerm(string term)
    {
        using var connection = _context.CreateConnection();
        
        var pessoas = await connection.QueryAsync<Pessoa>(
            @"SELECT *
                FROM pessoas
                WHERE Nome ILIKE '%' || @Term || '%'
	                OR Apelido ILIKE '%' || @Term || '%'
	                OR @Term ILIKE SOME(Stack)
                LIMIT 50;", 
            new { Term = term } );

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
        using var connection = _context.CreateConnection();
        
        var count = await connection.ExecuteScalarAsync<int>("SELECT COUNT(*) FROM pessoas");

        return count;
    }
}