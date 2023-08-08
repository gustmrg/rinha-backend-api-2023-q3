namespace RinhaDeBackEnd.API.Models;

public class Person
{
    public Guid Id { get; set; }
    public string Nickname { get; set; }
    public string Name { get; set; }
    public DateOnly DateOfBirth  { get; set; }
    public string[] Stack { get; set; }
}