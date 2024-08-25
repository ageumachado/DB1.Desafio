using DB1.Core.Extensions;
using DB1.Core.Messages;
using DB1.Core.ValueObjects;
using DB1.Desafio.Domain.Entities;
using DB1.Desafio.Domain.Enums;
using FluentValidation;

namespace DB1.Desafio.Application.Commands.Funcionario
{
    public class BaseFuncionario : Command
    {
        public string? Nome { get; set; }
        public string? Cpf { get; set; }
        public DateTime DataContratacao { get; set; }
        public Status Status { get; set; }
        public Guid? EmpresaId { get; set; }
        public Guid? CargoId { get; set; }

        public override void AddValidationResult()
        {
            ValidationResult = new BaseFuncionarioValidator().Validate(this);
        }
    }

    public class BaseFuncionarioValidator : AbstractValidator<BaseFuncionario>
    {
        public BaseFuncionarioValidator()
        {
            RuleFor(a => a.Nome)
                .NotEmpty().WithMessage("Informe o nome")
                .MaximumLength(FuncionarioValidator.NOME_MAX_LENGTH).WithMessage("Quantidade máxima de caracteres é de {MaxLength}");

            RuleFor(a => a.Cpf)
                .NotNull().WithMessage("Informe o CPF")
                .Length(FuncionarioValidator.CPF_LENGTH).WithMessage($"Tamanho deve ser {FuncionarioValidator.CPF_LENGTH}")
                .IsCpf().WithMessage("CPF inválido");

            RuleFor(a => a.DataContratacao)
                .NotNull().WithMessage("Informe a data de contratação")
                .Must(data => data > DateTime.MinValue).WithMessage("Data precisa ser maior que a data mínima");
        }
    }
}
