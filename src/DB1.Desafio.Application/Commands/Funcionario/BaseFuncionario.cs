using DB1.Core.Messages;
using DB1.Desafio.Domain.Enums;

namespace DB1.Desafio.Application.Commands.Funcionario
{
    public class BaseFuncionario : Command
    {
        public string? Nome { get; set; }
        public string? Cpf { get; set; }
        public DateTime DataContratacao { get; set; }
        public Status Status { get; set; } = Status.Ativo;
        public Guid? EmpresaId { get; set; }
        public Guid? CargoId { get; set; }

        public override void AddValidationResult()
        {
            ValidationResult = new BaseFuncionarioValidator().Validate(this);
        }
    }
}
