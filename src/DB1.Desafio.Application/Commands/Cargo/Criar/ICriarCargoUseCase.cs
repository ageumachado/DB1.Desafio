using DB1.Core.Communication;

namespace DB1.Desafio.Application.Commands.Cargo.Criar
{
    public interface ICriarCargoUseCase
    {
        Task<ResponseResult<CriarCargoResponse>> ExecutarAsync(CriarCargoRequest request);
    }
}
