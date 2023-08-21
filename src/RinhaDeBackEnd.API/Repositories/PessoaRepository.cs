using System.Data;
using Dapper;
using RinhaDeBackEnd.API.Interfaces;
using RinhaDeBackEnd.API.Models;

namespace RinhaDeBackEnd.API.Repositories;

public class PessoaRepository : IPessoaRepository
{
    private readonly IDbConnection _dbConnection;

    public PessoaRepository(IDbConnection dbConnection)
    {
        _dbConnection = dbConnection;
    }

    public async Task<Pessoa> Add(Pessoa pessoa)
    {
        pessoa.Id = Guid.NewGuid();

        using (_dbConnection)
        {
            using var tran = _dbConnection.BeginTransaction();
            try
            {
                await _dbConnection.ExecuteAsync("INSERT INTO pessoas VALUES (@Id, @Apelido, @Nome, @Nascimento, @Stack)",
                    new
                    {
                        id = pessoa.Id,
                        apelido = pessoa.Apelido,
                        nome = pessoa.Nome,
                        nascimento = pessoa.Nascimento,
                        stack = pessoa.Stack
                    }, transaction: tran);
                tran.Commit();
                return pessoa;
            }
            catch (Exception)
            {
                tran.Rollback();
                throw;
            }
        }
    }

    public IEnumerable<Pessoa> Get()
    {
        using (_dbConnection)
        {
            var pessoas = _dbConnection.Query<Pessoa>(
                "SELECT Id, Apelido, Nome, Nascimento, Stack FROM pessoas");

            return pessoas;
        }
    }

    public async Task<Pessoa> GetById(Guid id)
    {
        using (_dbConnection)
        {
            var pessoa = await _dbConnection.QuerySingleOrDefaultAsync<Pessoa>(
                @"SELECT Id, Apelido, Nome, Nascimento, Stack FROM pessoas WHERE Id = @Id",
                new { Id = id });

            return pessoa;
        }
    }

    public async Task<IEnumerable<Pessoa>> FindByTerm(string term)
    {
        using (_dbConnection)
        {
            var pessoas = await _dbConnection.QueryAsync<Pessoa>(
                @"SELECT *
                    FROM pessoas
                    WHERE Nome ILIKE '%' || @Term || '%'
	                    OR Apelido ILIKE '%' || @Term || '%'
	                    OR @Term ILIKE SOME(Stack)
                    LIMIT 50;",
                new { Term = term });

            return pessoas;
        }
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
        using (_dbConnection)
        {
            var count = await _dbConnection.ExecuteScalarAsync<int>("SELECT COUNT(*) FROM pessoas");

            return count;
        }
    }
}