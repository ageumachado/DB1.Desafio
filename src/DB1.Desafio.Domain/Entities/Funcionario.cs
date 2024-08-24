using DB1.Core.DomainObjects;
using DB1.Desafio.Domain.Enums;
using FluentValidation;

namespace DB1.Desafio.Domain.Entities
{
    public class Funcionario : Entity, IAggregateRoot
    {
        public string Nome { get; private set; }
        public string Cpf { get; private set; }
        public DateTime DataContratacao { get; private set; }
        public Status Status { get; private set; }

        public Funcionario(string nome, string cpf, DateTime dataContratacao)
        {
            Nome = nome;
            Cpf = cpf;
            DataContratacao = dataContratacao;
            Ativar();

            Validate(this, new FuncionarioValidator());
        }


        public Guid EmpresaId { get; private set; }
        public Empresa Empresa { get; private set; }

        private List<Cargo>? _cargos;
        public IReadOnlyCollection<Cargo> Cargos => _cargos ?? [];

        public void AdicionarCargo(params Cargo[] cargos)
        {
            _cargos ??= [];
            _cargos.AddRange(cargos);
        }
        public void LimparCargos() => _cargos?.Clear();


        public void Ativar() => Status = Status.Ativo;
        public void Inativar() => Status = Status.Inativo;
        public void Remover() => Status = Status.Removido;
    }

    public class FuncionarioValidator : AbstractValidator<Funcionario>
    {
        public static readonly int NOME_MAX_LENGTH = 150;
        public static readonly int CPF_LENGTH = 11;

        public FuncionarioValidator()
        {
            RuleFor(a => a.Nome)
                .NotEmpty().WithMessage("Informe o nome")
                .MaximumLength(NOME_MAX_LENGTH).WithMessage("Quantidade máxima de caracteres é de {MaxLength}");

            RuleFor(a => a.Cpf)
                .NotNull().WithMessage("Informe o CPF")
                .Length(CPF_LENGTH).WithMessage($"Tamanho deve ser {CPF_LENGTH}");

            RuleFor(a => a.DataContratacao)
                .NotNull().WithMessage("Informe a data de contratação")
                .Must(data => data > DateTime.MinValue).WithMessage("Data precisa ser maior que a data mínima");
        }
    }
}
