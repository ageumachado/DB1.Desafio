using DB1.Desafio.Domain.Entities;
using FluentValidation;

namespace DB1.Desafio.Domain.Validators
{
    public class EmpresaValidator : AbstractValidator<Empresa>
    {
        public static readonly int NOME_MAX_LENGTH = 150;
        public static readonly int CNPJ_LENGTH = 14;

        public EmpresaValidator()
        {
            RuleFor(a => a.Nome)
                .NotEmpty().WithMessage("Informe o nome")
                .MaximumLength(NOME_MAX_LENGTH).WithMessage("Quantidade máxima de caracteres é de {MaxLength}");

            RuleFor(a => a.Cnpj.Numero)
                .NotNull().WithMessage("Informe o CNPJ")
                .Length(CNPJ_LENGTH).WithMessage($"Tamanho deve ser {CNPJ_LENGTH}");

            RuleFor(a => a.DataFundacao)
                .NotNull().WithMessage("Informe a data da fundação")
                .Must(data => data > DateTime.MinValue).WithMessage("Data precisa ser maior que a data mínima");
        }
    }
}
