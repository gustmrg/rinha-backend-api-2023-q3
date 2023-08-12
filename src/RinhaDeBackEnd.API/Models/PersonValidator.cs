using FluentValidation;

namespace RinhaDeBackEnd.API.Models;

public class PersonValidator : AbstractValidator<Person>
{
    public PersonValidator()
    {
        RuleFor(p => p.Nickname)
            .NotEmpty().WithMessage("Obrigatório informar um apelido")
            .Length(1, 32).WithMessage("O campo apelido deve ter entre 1 e 32 caracteres");
        
        RuleFor(p => p.Name)
            .NotEmpty().WithMessage("Obrigatório informar um nome")
            .Length(1, 100).WithMessage("O campo nome deve ter entre 1 e 100 caracteres");

        RuleFor(p => p.DateOfBirth)
            .NotEmpty().Must(ValidDate).WithMessage("Obrigatório informar uma data de nascimento");
    }

    private static bool ValidDate(DateTime date)
    {
        return !date.Equals(default(DateTime));
    }
}