using DB1.Desafio.Domain.Entities;
using FluentValidation;

namespace DB1.Desafio.Domain.Validators
{
    public class CargoValidator : AbstractValidator<Cargo>
    {
        public static readonly int NOME_MAX_LENGTH = 150;

        public CargoValidator()
        {
            RuleFor(a => a.Nome)
                .NotEmpty().WithMessage("Informe o nome")
                .MaximumLength(NOME_MAX_LENGTH).WithMessage("Quantidade máxima de caracteres é de {MaxLength}");
        }
    }
}
