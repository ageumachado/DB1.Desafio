using DB1.Core.Messages;
using DB1.Desafio.Domain.Enums;

namespace DB1.Desafio.Application.Commands.Cargo
{
    public class BaseCargo : Command
    {
        public string Nome { get; set; }
        public Status Status { get; set; } = Status.Ativo;

        public override void AddValidationResult()
        {
            ValidationResult = new BaseCargoValidation().Validate(this);
        }
    }
}
