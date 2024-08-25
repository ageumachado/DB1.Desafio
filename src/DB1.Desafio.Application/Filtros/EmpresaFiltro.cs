using AspNetCore.IQueryable.Extensions;

namespace DB1.Desafio.Application.Filtros
{
    public class EmpresaFiltro : ICustomQueryable
    {
        public string? Nome { get; set; }
        public string? Cnpj { get; set; }
        public DateTime? DataFundacaoInicial { get; set; }
        public DateTime? DataFundacaoFinal { get; set; }
    }
}
