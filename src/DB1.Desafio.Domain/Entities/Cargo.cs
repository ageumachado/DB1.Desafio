using DB1.Core.DomainObjects;
using DB1.Desafio.Domain.Enums;
using FluentValidation;

namespace DB1.Desafio.Domain.Entities
{
    public class Cargo : Entity, IAggregateRoot
    {
        public string Nome { get; private set; }
        public Status Status { get; private set; }

        public Cargo(string nome)
        {
            Nome = nome;
            Ativar();

            Validate(this, new CargoValidator());
        }

        public ICollection<Funcionario>? Funcionarios { get; set; }

        public void Ativar() => Status = Status.Ativo;
        public void Inativar() => Status = Status.Inativo;
        public void Remover() => Status = Status.Removido;
    }

    public class CargoValidator : AbstractValidator<Cargo>
    {
        public static readonly int NOME_MAX_LENGTH = 150;

        public CargoValidator()
        {
            RuleFor(a => a.Nome)
                .NotEmpty().WithMessage("Informe o nome")
                .MaximumLength(NOME_MAX_LENGTH).WithMessage("Quantidade máxima de caracteres é de {MaxLength}");
        }
    }
}
