using AutoMapper;
using AutoMapper.QueryableExtensions;
using DB1.Core.Communication;
using DB1.Core.DomainObjects;
using DB1.Core.Extensions;
using DB1.Desafio.Domain.Repositories;

namespace DB1.Desafio.Application.Commands.Cargo.ObterTodos
{
    public interface IObterTodosCargoUseCase
    {
        Task<ResponseResult<IEnumerable<ObterComFiltroCargoResponse>>> ExecutarAsync();
    }

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

    public class ObterComFiltroCargoResponse
    {
        public Guid Id { get; set; }
        public string? Nome { get; set; }
        public string Status { get; set; }
    }
}
