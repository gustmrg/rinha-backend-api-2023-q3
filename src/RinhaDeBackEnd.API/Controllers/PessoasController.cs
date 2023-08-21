using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using RinhaDeBackEnd.API.Interfaces;
using RinhaDeBackEnd.API.Models;
using PostgresException = Npgsql.PostgresException;

namespace RinhaDeBackEnd.API.Controllers;

[ApiController]
public class PessoasController : ControllerBase
{
    private readonly IValidator<Pessoa> _validator;
    private readonly IPessoaRepository _pessoaRepository;

    public PessoasController(IPessoaRepository pessoaRepository, IValidator<Pessoa> validator)
    {
        _pessoaRepository = pessoaRepository;
        _validator = validator;
    }
    
    [HttpGet("/pessoas")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IResult> GetPessoasBySearchTermAsync([FromQuery(Name = "t")] string? query)
    {
        return query == null ? 
            Results.BadRequest("É obrigatório informar um valor no parâmetro [t].") : 
            Results.Ok(await _pessoaRepository.FindByTerm(query));
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
        var result = await _validator.ValidateAsync(request);

        if (!result.IsValid)
            return Results.UnprocessableEntity(result.Errors);
        
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