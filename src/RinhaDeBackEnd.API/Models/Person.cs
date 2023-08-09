using System.ComponentModel.DataAnnotations;

namespace RinhaDeBackEnd.API.Models;

public class Person
{
    [Key]
    public Guid Id { get; set; }

    [Required] 
    [StringLength(32, ErrorMessage = "O tamanho máximo é de {0} caracteres"), MinLength(2)]
    public string Nickname { get; set; } = null!;

    [Required] 
    [StringLength(100, ErrorMessage = "O tamanho máximo é de {0} caracteres"), MinLength(2)]
    public string Name { get; set; } = null!;
    
    [Required]
    [DisplayFormat(DataFormatString = "YYYY/MM/DD")]
    public DateOnly DateOfBirth  { get; set; }
    
    public string[]? Stack { get; set; } 
}