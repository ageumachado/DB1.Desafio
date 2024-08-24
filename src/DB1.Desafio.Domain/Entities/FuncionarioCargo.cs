namespace DB1.Desafio.Domain.Entities
{
    public class FuncionarioCargo
    {
        public required Funcionario Funcionario { get; set; }
        public required Cargo Cargo { get; set; }
    }
}
