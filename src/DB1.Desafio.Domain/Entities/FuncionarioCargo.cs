using System.Diagnostics.CodeAnalysis;

namespace DB1.Desafio.Domain.Entities
{
    public class FuncionarioCargo
    {
        public Guid? FuncionarioId { get; set; }
        public required Funcionario Funcionario { get; set; }

        public required Guid CargoId { get; set; }
        public Cargo? Cargo { get; set; }

        public required DateTime DataVinculo { get; set; }

        // EF
        protected FuncionarioCargo() { }

        [SetsRequiredMembers]
        public FuncionarioCargo(Funcionario funcionario, Guid cargoId, DateTime dataVinculo)
        {
            Funcionario = funcionario;
            CargoId = cargoId;
            DataVinculo = dataVinculo;
        }
    }
}
