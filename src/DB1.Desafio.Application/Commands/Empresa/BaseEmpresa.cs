using DB1.Core.Messages;
using DB1.Desafio.Domain.Enums;

namespace DB1.Desafio.Application.Commands.Empresa
{
    public class BaseEmpresa : Command
    {
        public Guid Id { get; set; }
        public string? Nome { get; set; }
        public string? Cnpj { get; set; }
        public DateTime DataFundacao { get; set; }
        public StatusEmpresa Status { get; set; }

        public override void AddValidationResult()
        {
            ValidationResult = new BaseEmpresaValidation().Validate(this);
        }
    }
}
