namespace DB1.Desafio.Application.Commands.Funcionario.ObterComFiltro
{
    public class ObterComFiltroFuncionarioResponse
    {
        public Guid Id { get; set; }
        public string? Nome { get; set; }
        public string? Cpf { get; set; }
        public DateTime DataContratacao { get; set; }
        public string Status { get; set; }
        public string EmpresaNome { get; set; }
        public string? CargoNome { get; set; }
    }
}
