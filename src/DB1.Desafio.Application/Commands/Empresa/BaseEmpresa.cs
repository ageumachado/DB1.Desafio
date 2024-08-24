using DB1.Core.Messages;
using DB1.Desafio.Domain.Entities;
using FluentValidation;
using DB1.Core.Extensions;

namespace DB1.Desafio.Application.Commands.Empresa
{
    public class BaseEmpresa : Command
    {
        public Guid Id { get; set; }
        public string? Nome { get; set; }
        public string? Cnpj { get; set; }
        public DateTime DataFundacao { get; set; }
        public bool Ativo { get; set; }

        public override void AddValidationResult()
        {
            ValidationResult = new BaseEmpresaValidation().Validate(this);
        }
    }

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
