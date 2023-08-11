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

    [HttpPost]
    [Route("/pessoas")]
    public Person CreatePerson(Person person)
    {
        return _personRepository.CreatePerson(person);
    }
    
    [HttpGet]
    [Route("/pessoas")]
    public IEnumerable<Person> GetPersons()
    {
        return _personRepository.GetAllPersons();
    }

    [HttpGet]
    [Route("/pessoas/{id:guid}")]
    public Person GetPersonById(Guid id)
    {
        return _personRepository.GetPersonById(id);
    }
    
    [HttpGet]
    [Route("/contagem-pessoas")]
    public string GetPersonCount()
    {
        var count = _personRepository.PersonCount();
        return $"{count} pessoas cadastradas";
    }
}