using DB1.Core.Communication;

namespace DB1.Desafio.Application.Commands.Cargo.Editar
{
    public interface IEditarCargoUseCase
    {
        Task<ResponseResult<EditarCargoResponse>> ExecutarAsync(Guid id, EditarCargoRequest request);
    }
}
