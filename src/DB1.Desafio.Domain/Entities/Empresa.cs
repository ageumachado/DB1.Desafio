using DB1.Core.DomainObjects;
using DB1.Core.ValueObjects;
using DB1.Desafio.Domain.Enums;
using DB1.Desafio.Domain.Validators;

namespace DB1.Desafio.Domain.Entities
{
    public class Empresa : Entity, IAggregateRoot
    {
        public string Nome { get; private set; }
        public Cnpj Cnpj { get; private set; }
        public DateTime DataFundacao { get; private set; }
        public StatusEmpresa Status { get; private set; }

        // EF
        protected Empresa() { }

        public Empresa(Guid id, string nome, Cnpj cnpj, DateTime dataFundacao, StatusEmpresa status = StatusEmpresa.Ativo)
        {
            Id = id;
            Nome = nome;
            Cnpj = cnpj;
            DataFundacao = dataFundacao;
            Status = status;

            Validate(this, new EmpresaValidator());
        }

        public ICollection<Funcionario> Funcionarios { get; private set; }

        public void Ativar() => Status = StatusEmpresa.Ativo;
        public void Inativar() => Status = StatusEmpresa.Inativo;
        public void Remover() => Status = StatusEmpresa.Removido;
    }
}
