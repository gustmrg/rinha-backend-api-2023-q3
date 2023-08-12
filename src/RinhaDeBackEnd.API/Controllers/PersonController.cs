using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using RinhaDeBackEnd.API.Interfaces;
using RinhaDeBackEnd.API.Models;

namespace RinhaDeBackEnd.API.Controllers;

[ApiController]
public class PersonController : ControllerBase
{
    private readonly IPessoaRepository _pessoaRepository;

    public PersonController(IPessoaRepository pessoaRepository)
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
    public IResult GetPessoaById(Guid id)
    {
        var pessoa = _pessoaRepository.GetById(id);
        
        if (pessoa is null)
            return Results.NotFound();
        
        return Results.Ok(pessoa);
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
    public ActionResult<string> GetPersonCount()
    {
        var count = _pessoaRepository.Count();
        return Ok($"{count} pessoas cadastradas");
    }
}