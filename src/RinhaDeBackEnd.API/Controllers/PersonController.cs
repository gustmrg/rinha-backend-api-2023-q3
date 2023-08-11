using Microsoft.AspNetCore.Mvc;
using RinhaDeBackEnd.API.Models;
using RinhaDeBackEnd.API.Repositories;

namespace RinhaDeBackEnd.API.Controllers;

[ApiController]
public class PersonController : ControllerBase
{
    private readonly PersonRepository _personRepository;

    public PersonController(PersonRepository personRepository)
    {
        _personRepository = personRepository;
    }
    
    // TODO: Alterar para aceitar termo de busca
    [HttpGet("/pessoas")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IEnumerable<Person> GetPersons()
    {
        return _personRepository.GetAllPersons();
    }

    [HttpGet("/pessoas/{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<Person> GetPersonById(Guid id)
    {
        return Ok(_personRepository.GetPersonById(id));
    }

    [HttpPost("/pessoas")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
    public Person CreatePerson(Person person)
    {
        return _personRepository.CreatePerson(person);
    }
    
    [HttpGet("/contagem-pessoas")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public ActionResult<string> GetPersonCount()
    {
        var count = _personRepository.PersonCount();
        return Ok($"{count} pessoas cadastradas");
    }
}