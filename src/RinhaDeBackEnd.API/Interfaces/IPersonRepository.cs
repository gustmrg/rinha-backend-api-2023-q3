using RinhaDeBackEnd.API.Models;

namespace RinhaDeBackEnd.API.Interfaces;

public interface IPersonRepository
{
    public Person Add(Person person);
    public IEnumerable<Person> Get();
    public Person GetById(Guid id);
    public void UpdatePerson();
    public void DeletePerson();
    public int Count();
}