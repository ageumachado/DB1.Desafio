using DB1.Core.Communication;

namespace DB1.Desafio.Application.Commands.Funcionario.ObterPorId
{
    public interface IObterPorIdFuncionarioUseCase
    {
        Task<ResponseResult<ObterPorIdFuncionarioResponse>> ExecutarAsync(Guid id);
    }
}
