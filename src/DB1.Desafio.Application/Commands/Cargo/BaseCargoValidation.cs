using DB1.Desafio.Domain.Validators;
using FluentValidation;

namespace DB1.Desafio.Application.Commands.Cargo
{
    public class BaseCargoValidation : AbstractValidator<BaseCargo>
    {
        public BaseCargoValidation()
        {
            RuleFor(a => a.Nome)
                .NotEmpty().WithMessage("Informe o nome")
                .MaximumLength(CargoValidator.NOME_MAX_LENGTH).WithMessage("Quantidade máxima de caracteres é de {MaxLength}");
        }
    }
}
