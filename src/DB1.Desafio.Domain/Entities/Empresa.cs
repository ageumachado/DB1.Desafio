using DB1.Core.DomainObjects;
using DB1.Desafio.Domain.Enums;
using FluentValidation;

namespace DB1.Desafio.Domain.Entities
{
    public class Empresa : Entity, IAggregateRoot
    {
        public string Nome { get; private set; }
        public string Cnpj { get; private set; }
        public DateTime DataFundacao { get; private set; }
        public StatusEmpresa Status { get; private set; }

        public Empresa(string nome, string cnpj, DateTime dataFundacao)
        {
            Nome = nome;
            Cnpj = cnpj;
            DataFundacao = dataFundacao;
            Ativar();

            Validate(this, new EmpresaValidator());
        }

        public ICollection<Funcionario> Funcionarios { get; private set; }

        public void Ativar() => Status = StatusEmpresa.Ativo;
        public void Inativar() => Status = StatusEmpresa.Inativo;
        public void Remover() => Status = StatusEmpresa.Removido;
    }

    public class EmpresaValidator : AbstractValidator<Empresa>
    {
        public static readonly int NOME_MAX_LENGTH = 150;
        public static readonly int CNPJ_LENGTH = 14;

        public EmpresaValidator()
        {
            RuleFor(a => a.Nome)
                .NotEmpty().WithMessage("Informe o nome")
                .MaximumLength(NOME_MAX_LENGTH).WithMessage("Quantidade máxima de caracteres é de {MaxLength}");

            RuleFor(a => a.Cnpj)
                .NotNull().WithMessage("Informe o CNPJ")
                .Length(CNPJ_LENGTH).WithMessage($"Tamanho deve ser {CNPJ_LENGTH}");
                //.IsCnpj().WithMessage("CNPJ inválido");

            RuleFor(a => a.DataFundacao)
                .NotNull().WithMessage("Informe a data da fundação")
                .Must(data => data > DateTime.MinValue).WithMessage("Data precisa ser maior que a data mínima");
        }
    }
}
