using DB1.Core.DomainObjects;
using DB1.Core.ValueObjects;
using DB1.Desafio.Domain.Enums;
using DB1.Desafio.Domain.Validators;

namespace DB1.Desafio.Domain.Entities
{
    public class Funcionario : Entity, IAggregateRoot
    {
        public string Nome { get; private set; }
        public Cpf Cpf { get; private set; }
        public DateTime DataContratacao { get; private set; }
        public Status Status { get; private set; }

        // EF
        protected Funcionario() { }

        public Funcionario(string nome, Cpf cpf, DateTime dataContratacao)
        {
            Nome = nome;
            Cpf = cpf;
            DataContratacao = dataContratacao;
            Ativar();

            Validate(this, new FuncionarioValidator());
        }

        public Guid? EmpresaId { get; private set; }
        public Empresa? Empresa { get; private set; }

        public void AdicionarEmpresa(Guid? empresaId) => EmpresaId = empresaId;

        private List<FuncionarioCargo>? _funcionarioCargos;
        public IReadOnlyCollection<FuncionarioCargo> FuncionarioCargos => _funcionarioCargos ?? [];

        public void Ativar() => Status = Status.Ativo;
        public void Inativar() => Status = Status.Inativo;
        public void Remover() => Status = Status.Removido;
    }
}
