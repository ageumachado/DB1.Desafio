using DB1.Core.Communication;

namespace DB1.Desafio.Application.Commands.Funcionario.Excluir
{
    public interface IExcluirFuncionarioUseCase
    {
        Task<ResponseResult> ExecutarAsync(Guid id);
    }
}
