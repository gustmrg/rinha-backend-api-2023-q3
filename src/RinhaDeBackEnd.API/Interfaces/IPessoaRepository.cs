using RinhaDeBackEnd.API.Models;

namespace RinhaDeBackEnd.API.Interfaces;

public interface IPessoaRepository
{
    public Pessoa Add(Pessoa person);
    public IEnumerable<Pessoa> Get();
    public Task<Pessoa> GetById(Guid id);
    public void UpdatePerson();
    public void DeletePerson();
    public int Count();
}