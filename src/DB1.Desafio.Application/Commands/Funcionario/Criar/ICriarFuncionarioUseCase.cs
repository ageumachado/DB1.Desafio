using DB1.Core.Communication;

namespace DB1.Desafio.Application.Commands.Funcionario.Criar
{
    public interface ICriarFuncionarioUseCase
    {
        Task<ResponseResult<CriarFuncionarioResponse>> ExecutarAsync(CriarFuncionarioRequest request);
    }
}
