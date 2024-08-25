using AutoMapper;
using AutoMapper.QueryableExtensions;
using DB1.Core.Communication;
using DB1.Core.DomainObjects;
using DB1.Core.Extensions;
using DB1.Desafio.Domain.Repositories;

namespace DB1.Desafio.Application.Commands.Cargo.ObterTodos
{
    internal class ObterTodosCargoUseCase(
        ICargoRepository cargoRepository,
        IMapper mapper) : CommandUseCase, IObterTodosCargoUseCase
    {
        public async Task<ResponseResult<IEnumerable<ObterComFiltroCargoResponse>>> ExecutarAsync()
        {
            var response = await cargoRepository
                .ObterListaQueryableAsync(p =>
                    p.ProjectTo<ObterComFiltroCargoResponse>(mapper.ConfigurationProvider));
            return response.ToResponseResult();
        }
    }
}
