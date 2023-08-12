using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using RinhaDeBackEnd.API.Interfaces;
using RinhaDeBackEnd.API.Models;

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
    public IEnumerable<Pessoa> GetPessoas()
    {
        return _pessoaRepository.Get();
    }

    [HttpGet("/pessoas/{id:guid}")]
    [Consumes(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IResult> GetPessoaById(Guid id)
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
    public IResult CreatePerson(Pessoa model)
    {

        if (!ModelState.IsValid)
            return Results.UnprocessableEntity();

        try
        {
            var pessoa = _pessoaRepository.Add(model);
            return Results.Created($"/pessoas/{pessoa.Id}", pessoa);
        }
        catch (Exception e)
        {
            throw new Exception();
        }
    }
    
    [HttpGet("/contagem-pessoas")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IResult> GetPersonCount()
    {
        var count = await _pessoaRepository.Count();
        return Results.Ok($"{count} pessoas cadastradas");
    }
}