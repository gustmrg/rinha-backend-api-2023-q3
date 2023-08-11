using RinhaDeBackEnd.API.Models;

namespace RinhaDeBackEnd.API.Interfaces;

public interface IPersonRepository
{
    public IEnumerable<Person> GetAllPersons();
    public Person GetPersonById(Guid id);
    public Person CreatePerson(Person person);
    public void UpdatePerson();
    public void DeletePerson();
    public int PersonCount();
}