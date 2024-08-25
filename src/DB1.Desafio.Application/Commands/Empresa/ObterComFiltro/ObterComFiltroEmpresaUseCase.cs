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
}
