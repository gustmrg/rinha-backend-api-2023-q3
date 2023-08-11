using Microsoft.AspNetCore.Mvc;
using RinhaDeBackEnd.API.Interfaces;
using RinhaDeBackEnd.API.Models;

namespace RinhaDeBackEnd.API.Controllers;

[ApiController]
public class PersonController : ControllerBase
{
    private readonly IPersonService _personService;

    public PersonController(IPersonService personService)
    {
        _personService = personService;
    }

    // TODO: Alterar para aceitar termo de busca
    [HttpGet("/pessoas")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IEnumerable<Person> GetPersons()
    {
        return _personService.GetAll();
    }

    [HttpGet("/pessoas/{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<Person> GetPersonById(Guid id)
    {
        return Ok(_personService.GetById(id));
    }

    [HttpPost("/pessoas")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
    public Person CreatePerson(Person person)
    {
        return _personService.Add(person);
    }
    
    [HttpGet("/contagem-pessoas")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public ActionResult<string> GetPersonCount()
    {
        var count = _personService.Count();
        return Ok($"{count} pessoas cadastradas");
    }
}