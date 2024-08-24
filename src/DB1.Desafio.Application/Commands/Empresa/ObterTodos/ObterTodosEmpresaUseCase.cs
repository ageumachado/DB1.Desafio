using AspNetCore.IQueryable.Extensions;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using DB1.Core.Communication;
using DB1.Core.DomainObjects;
using DB1.Core.Extensions;
using DB1.Desafio.Application.Filtros;
using DB1.Desafio.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB1.Desafio.Application.Commands.Empresa.ObterTodos
{
    public interface IObterTodosEmpresaUseCase
    {
        Task<ResponseResult<IEnumerable<ObterTodosEmpresaResponse>>> ExecutarAsync(EmpresaFiltro filtro);
    }

    public class ObterTodosEmpresaUseCase(
        IEmpresaRepository empresaRepository,
        IMapper mapper) : CommandUseCase, IObterTodosEmpresaUseCase
    {
        public async Task<ResponseResult<IEnumerable<ObterTodosEmpresaResponse>>> ExecutarAsync(EmpresaFiltro filtro)
        {
            var response = await empresaRepository
                .ObterListaQueryableAsync(p =>
                    p.Apply(filtro)
                    .ProjectTo<ObterTodosEmpresaResponse>(mapper.ConfigurationProvider));
            return response.ToResponseResult();
        }
    }

    public class ObterTodosEmpresaResponse : BaseEmpresa;


}
