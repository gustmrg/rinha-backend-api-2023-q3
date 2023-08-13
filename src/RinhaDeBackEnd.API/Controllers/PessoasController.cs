using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Npgsql;
using RinhaDeBackEnd.API.Interfaces;
using RinhaDeBackEnd.API.Models;
using PostgresException = Npgsql.PostgresException;

namespace RinhaDeBackEnd.API.Controllers;

[ApiController]
public class PessoasController : ControllerBase
{
    private readonly IPessoaRepository _pessoaRepository;

    public PessoasController(IPessoaRepository pessoaRepository)
    {
        _pessoaRepository = pessoaRepository;
    }

    // TODO: Alterar para aceitar termo de busca
    [HttpGet("/pessoas")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IEnumerable<Pessoa> GetPessoasBySearchTermAsync([FromQuery] string term)
    {
        return _pessoaRepository.Get();
    }

    [HttpGet("/pessoas/{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IResult> GetPessoaByIdAsync(Guid id)
    {
        try
        {
            var pessoa = await _pessoaRepository.GetById(id);
        
            return pessoa == null ?
                Results.NotFound() : Results.Ok(pessoa);
        }
        catch (Exception)
        {
            return Results.NotFound();
        }
    }

    [HttpPost("/pessoas")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
    public async Task<IResult> CreatePersonAsync(Pessoa request)
    {

        if (!ModelState.IsValid)
            return Results.UnprocessableEntity();

        try
        {
            var pessoa = await _pessoaRepository.Add(request);
            return Results.Created($"/pessoas/{pessoa.Id}", pessoa);
        }
        catch (PostgresException ex)
        {
            if (ex.SqlState == "23505")
                return Results.UnprocessableEntity("Já existe uma pessoa criada com este apelido.");
            throw ex;
        }
        catch (Exception)
        {
            return Results.BadRequest();
        }
    }
    
    [HttpGet("/contagem-pessoas")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IResult> GetPersonCountAsync()
    {
        var count = await _pessoaRepository.Count();
        return Results.Ok($"{count} pessoas cadastradas!");
    }
}