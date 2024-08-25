using DB1.Core.Messages;
using DB1.Desafio.Application.Commands.Empresa;
using DB1.Desafio.Domain.Entities;
using DB1.Desafio.Domain.Enums;
using FluentValidation;

namespace DB1.Desafio.Application.Commands.Cargo
{
    public class BaseCargo : Command
    {
        public string Nome { get; set; }
        public Status Status { get; set; }

        public override void AddValidationResult()
        {
            ValidationResult = new BaseCargoValidation().Validate(this);
        }
    }

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
