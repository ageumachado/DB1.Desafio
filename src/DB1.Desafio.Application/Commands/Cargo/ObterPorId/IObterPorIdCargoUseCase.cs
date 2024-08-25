using DB1.Core.Communication;

namespace DB1.Desafio.Application.Commands.Cargo.ObterPorId
{
    public interface IObterPorIdCargoUseCase
    {
        Task<ResponseResult<ObterPorIdCargoResponse>> ExecutarAsync(Guid id);
    }
}
