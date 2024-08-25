using DB1.Desafio.Domain.Entities;
using FluentValidation;

namespace DB1.Desafio.Domain.Validators
{
    public class FuncionarioValidator : AbstractValidator<Funcionario>
    {
        public static readonly int NOME_MAX_LENGTH = 150;
        public static readonly int CPF_LENGTH = 11;

        public FuncionarioValidator()
        {
            RuleFor(a => a.Nome)
                .NotEmpty().WithMessage("Informe o nome")
                .MaximumLength(NOME_MAX_LENGTH).WithMessage("Quantidade máxima de caracteres é de {MaxLength}");

            RuleFor(a => a.Cpf.Numero)
                .NotNull().WithMessage("Informe o CPF")
                .Length(CPF_LENGTH).WithMessage($"Tamanho deve ser {CPF_LENGTH}");

            RuleFor(a => a.DataContratacao)
                .NotNull().WithMessage("Informe a data de contratação")
                .Must(data => data > DateTime.MinValue).WithMessage("Data precisa ser maior que a data mínima");
        }
    }
}
