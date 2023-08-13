namespace RinhaDeBackEnd.API.Models;

public class Pessoa
{
    public Guid Id { get; set; }
    public string Apelido { get; set; } = null!;
    public string Nome { get; set; } = null!;
    public DateTime Nascimento  { get; set; }
    public string[]? Stack { get; set; }
}