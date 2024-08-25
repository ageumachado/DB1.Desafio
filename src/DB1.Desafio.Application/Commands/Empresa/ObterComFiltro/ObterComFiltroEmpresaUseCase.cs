using AspNetCore.IQueryable.Extensions;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using DB1.Core.Communication;
using DB1.Core.DomainObjects;
using DB1.Core.Extensions;
using DB1.Desafio.Application.Filtros;
using DB1.Desafio.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DB1.Desafio.Application.Commands.Empresa.ObterComFiltro
{
    public interface IObterComFiltroEmpresaUseCase
    {
        Task<ResponseResult<IEnumerable<ObterComFiltroEmpresaResponse>>> ExecutarAsync(EmpresaFiltro filtro);
    }

    public class ObterComFiltroEmpresaUseCase(
        IEmpresaRepository empresaRepository,
        IMapper mapper) : CommandUseCase, IObterComFiltroEmpresaUseCase
    {
        public async Task<ResponseResult<IEnumerable<ObterComFiltroEmpresaResponse>>> ExecutarAsync(EmpresaFiltro filtro)
        {
            var cnpj = filtro.Cnpj?.ApenasNumeros();

            var response = await empresaRepository
                .ObterListaQueryableAsync(p =>
                    p.Where(x => 
                        (string.IsNullOrEmpty(filtro.Nome) || EF.Functions.Like(x.Nome, $"%{filtro.Nome}%"))
                        && (string.IsNullOrEmpty(cnpj) || x.Cnpj.Numero.Equals(cnpj))
                        && (!filtro.DataFundacaoInicial.HasValue || x.DataFundacao.Date >= filtro.DataFundacaoInicial.Value.Date)
                        && (!filtro.DataFundacaoFinal.HasValue || x.DataFundacao.Date <= filtro.DataFundacaoFinal.Value.Date))
                    .ProjectTo<ObterComFiltroEmpresaResponse>(mapper.ConfigurationProvider));
            return response.ToResponseResult();
        }
    }

    public class ObterComFiltroEmpresaResponse
    {
        public Guid Id { get; set; }
        public string? Nome { get; set; }
        public string? Cnpj { get; set; }
        public DateTime DataFundacao { get; set; }
        public string Status { get; set; }
    }
}
