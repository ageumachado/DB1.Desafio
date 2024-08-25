using AutoMapper;
using AutoMapper.QueryableExtensions;
using DB1.Core.Communication;
using DB1.Core.DomainObjects;
using DB1.Core.Extensions;
using DB1.Desafio.Domain.Repositories;

namespace DB1.Desafio.Application.Commands.Empresa.ObterTodos
{
    public class ObterTodosEmpresaUseCase(
        IEmpresaRepository empresaRepository,
        IMapper mapper) : CommandUseCase, IObterTodosEmpresaUseCase
    {
        public async Task<ResponseResult<IEnumerable<ObterTodosEmpresaResponse>>> ExecutarAsync()
        {
            var response = await empresaRepository
                .ObterListaQueryableAsync(p =>
                    p.ProjectTo<ObterTodosEmpresaResponse>(mapper.ConfigurationProvider));
            return response.ToResponseResult();
        }
    }
}
