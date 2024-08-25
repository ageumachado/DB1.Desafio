namespace DB1.Desafio.Application.Commands.Empresa.ObterComFiltro
{
    public class ObterComFiltroEmpresaResponse
    {
        public Guid Id { get; set; }
        public string? Nome { get; set; }
        public string? Cnpj { get; set; }
        public DateTime DataFundacao { get; set; }
        public string Status { get; set; }
    }
}
