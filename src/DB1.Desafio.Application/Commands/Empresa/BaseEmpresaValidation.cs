using FluentValidation;
using DB1.Core.Extensions;
using DB1.Desafio.Domain.Validators;

namespace DB1.Desafio.Application.Commands.Empresa
{
    public class BaseEmpresaValidation : AbstractValidator<BaseEmpresa>
    {
        public BaseEmpresaValidation()
        {
            RuleFor(a => a.Nome)
                .NotEmpty().WithMessage("Informe o nome")
                .MaximumLength(EmpresaValidator.NOME_MAX_LENGTH).WithMessage("Quantidade máxima de caracteres é de {MaxLength}");

            RuleFor(a => a.Cnpj)
                .NotNull().WithMessage("Informe o CNPJ")
                .Length(EmpresaValidator.CNPJ_LENGTH).WithMessage($"Tamanho deve ser {EmpresaValidator.CNPJ_LENGTH}")
                .IsCnpj().WithMessage("CNPJ inválido");

            RuleFor(a => a.DataFundacao)
                .NotNull().WithMessage("Informe a data da fundação")
                .Must(data => data > DateTime.MinValue).WithMessage("Data precisa ser maior que a data mínima");
        }
    }
}
