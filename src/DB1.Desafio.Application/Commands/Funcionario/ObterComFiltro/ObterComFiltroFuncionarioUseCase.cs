using AutoMapper;
using AutoMapper.QueryableExtensions;
using DB1.Core.Communication;
using DB1.Core.DomainObjects;
using DB1.Core.Extensions;
using DB1.Desafio.Application.Filtros;
using DB1.Desafio.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DB1.Desafio.Application.Commands.Funcionario.ObterComFiltro
{
    public class ObterComFiltroFuncionarioUseCase(
        IFuncionarioRepository funcionarioRepository,
        IMapper mapper) : CommandUseCase, IObterComFiltroFuncionarioUseCase
    {
        public async Task<ResponseResult<IEnumerable<ObterComFiltroFuncionarioResponse>>> ExecutarAsync(FuncionarioFiltro filtro)
        {
            var cpf = filtro.Cpf?.ApenasNumeros();

            var response = await funcionarioRepository
                .ObterListaQueryableAsync(p =>
                    p.Where(x =>
                        (string.IsNullOrEmpty(filtro.Nome) || EF.Functions.Like(x.Nome, $"%{filtro.Nome}%"))
                        && (string.IsNullOrEmpty(cpf) || x.Cpf.Numero.Equals(cpf))
                        && (!filtro.DataContratacaoInicial.HasValue || x.DataContratacao.Date >= filtro.DataContratacaoInicial.Value.Date)
                        && (!filtro.DataContratacaoFinal.HasValue || x.DataContratacao.Date <= filtro.DataContratacaoFinal.Value.Date))
                    .ProjectTo<ObterComFiltroFuncionarioResponse>(mapper.ConfigurationProvider));
            return response.ToResponseResult();
        }
    }
}
