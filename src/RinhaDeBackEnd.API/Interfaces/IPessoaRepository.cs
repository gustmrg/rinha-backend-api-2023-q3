using RinhaDeBackEnd.API.Models;

namespace RinhaDeBackEnd.API.Interfaces;

public interface IPessoaRepository
{
    public Task<Pessoa> Add(Pessoa pessoa);
    public IEnumerable<Pessoa> Get();
    public Task<IEnumerable<Pessoa>> FindByTerm(string term);
    public Task<Pessoa> GetById(Guid id);
    public void UpdatePerson();
    public void DeletePerson();
    public Task<int> Count();
}