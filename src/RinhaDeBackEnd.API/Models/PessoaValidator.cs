using FluentValidation;

namespace RinhaDeBackEnd.API.Models;

public class PessoaValidator : AbstractValidator<Pessoa>
{
    public PessoaValidator()
    {
        RuleFor(p => p.Apelido)
            .NotEmpty().WithMessage("Obrigatório informar um apelido")
            .Length(1, 32).WithMessage("O campo apelido deve ter entre 1 e 32 caracteres");
        
        RuleFor(p => p.Nome)
            .NotEmpty().WithMessage("Obrigatório informar um nome")
            .Length(1, 100).WithMessage("O campo nome deve ter entre 1 e 100 caracteres");

        RuleFor(p => p.Nascimento)
            .NotEmpty().Must(ValidDate).WithMessage("Obrigatório informar uma data de nascimento");
    }

    private static bool ValidDate(DateTime date)
    {
        return !date.Equals(default(DateTime));
    }
}