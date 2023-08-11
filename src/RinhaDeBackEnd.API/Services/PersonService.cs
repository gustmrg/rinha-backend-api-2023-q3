using RinhaDeBackEnd.API.Interfaces;
using RinhaDeBackEnd.API.Models;
using RinhaDeBackEnd.API.Repositories;

namespace RinhaDeBackEnd.API.Services;

public class PersonService : IPersonService
{
    private readonly IPersonRepository _personRepository;

    public PersonService(IPersonRepository personRepository)
    {
        _personRepository = personRepository;
    }

    public Person Add(Person person)
    {
        return _personRepository.CreatePerson(person);
    }

    public IEnumerable<Person> GetAll()
    {
        return _personRepository.GetAllPersons();
    }

    public Person GetById(Guid id)
    {
        return _personRepository.GetPersonById(id);
    }

    public void Update()
    {
        throw new NotImplementedException();
    }

    public void Delete()
    {
        throw new NotImplementedException();
    }

    public int Count()
    {
        return _personRepository.PersonCount();
    }
}