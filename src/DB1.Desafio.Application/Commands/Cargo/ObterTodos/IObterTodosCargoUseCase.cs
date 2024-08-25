using DB1.Core.Communication;

namespace DB1.Desafio.Application.Commands.Cargo.ObterTodos
{
    public interface IObterTodosCargoUseCase
    {
        Task<ResponseResult<IEnumerable<ObterComFiltroCargoResponse>>> ExecutarAsync();
    }
}
