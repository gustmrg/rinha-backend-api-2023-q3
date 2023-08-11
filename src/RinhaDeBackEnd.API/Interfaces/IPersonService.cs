using RinhaDeBackEnd.API.Models;

namespace RinhaDeBackEnd.API.Interfaces;

public interface IPersonService
{
    public Person Add(Person person);
    public IEnumerable<Person> GetAll();
    public Person GetById(Guid id);
    public void Update();
    public void Delete();
    public int Count();
}