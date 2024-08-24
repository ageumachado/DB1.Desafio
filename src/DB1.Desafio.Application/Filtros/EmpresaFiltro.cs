using AspNetCore.IQueryable.Extensions;
using AspNetCore.IQueryable.Extensions.Attributes;
using AspNetCore.IQueryable.Extensions.Filter;

namespace DB1.Desafio.Application.Filtros
{
    public class EmpresaFiltro : ICustomQueryable
    {
        [QueryOperator(Operator = WhereOperator.Contains)]
        public string? Nome { get; set; }

        [QueryOperator(Operator = WhereOperator.Equals)]
        public string? Cnpj { get; set; }

        [QueryOperator(Operator = WhereOperator.GreaterThanOrEqualWhenNullable)]
        public DateTime? DataFundacaoInicial { get; set; }

        [QueryOperator(Operator = WhereOperator.LessThanOrEqualWhenNullable)]
        public DateTime? DataFundacaoFinal { get; set; }
    }
}
