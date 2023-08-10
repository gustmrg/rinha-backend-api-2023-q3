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
    public void CreatePerson()
    {
        throw new NotImplementedException(); 
    }
    
    [HttpGet]
    [Route("/pessoas")]
    public IEnumerable<Person> GetPersons()
    {
        return _personRepository.GetAllPersons();
    }

    [HttpGet]
    [Route("/pessoas/{id:guid}")]
    public void GetPersonById(Guid id)
    {
        throw new NotImplementedException();
    }
}