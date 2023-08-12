using Microsoft.AspNetCore.Mvc;
using RinhaDeBackEnd.API.Interfaces;
using RinhaDeBackEnd.API.Models;

namespace RinhaDeBackEnd.API.Controllers;

[ApiController]
public class PersonController : ControllerBase
{
    private readonly IPersonRepository _personRepository;

    public PersonController(IPersonRepository personRepository)
    {
        _personRepository = personRepository;
    }

    // TODO: Alterar para aceitar termo de busca
    [HttpGet("/pessoas")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IEnumerable<Person> GetPersons()
    {
        return _personRepository.Get();
    }

    [HttpGet("/pessoas/{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<Person> GetPersonById(Guid id)
    {
        return Ok(_personRepository.GetById(id));
    }

    [HttpPost("/pessoas")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
    public IResult CreatePerson(Person model)
    {

        if (!ModelState.IsValid)
            return Results.UnprocessableEntity();

        try
        {
            var person = _personRepository.Add(model);
            return Results.Created($"/pessoas/{person.Id}", person);
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
        var count = _personRepository.Count();
        return Ok($"{count} pessoas cadastradas");
    }
}