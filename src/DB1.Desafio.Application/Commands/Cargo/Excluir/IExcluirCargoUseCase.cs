using DB1.Core.Communication;

namespace DB1.Desafio.Application.Commands.Cargo.Excluir
{
    public interface IExcluirCargoUseCase
    {
        Task<ResponseResult> ExecutarAsync(Guid id);
    }
}
