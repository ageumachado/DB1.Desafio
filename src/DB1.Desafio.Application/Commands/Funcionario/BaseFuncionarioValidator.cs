using DB1.Core.Extensions;
using DB1.Desafio.Domain.Validators;
using FluentValidation;

namespace DB1.Desafio.Application.Commands.Funcionario
{
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
