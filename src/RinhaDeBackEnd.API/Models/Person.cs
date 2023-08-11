using System.ComponentModel.DataAnnotations;

namespace RinhaDeBackEnd.API.Models;

public class Person
{
    public Guid Id { get; set; }
    
    public string Nickname { get; set; } = null!;
    
    public string Name { get; set; } = null!;
    
    public DateOnly DateOfBirth  { get; set; }
    
    public string[]? Stack { get; set; }
}