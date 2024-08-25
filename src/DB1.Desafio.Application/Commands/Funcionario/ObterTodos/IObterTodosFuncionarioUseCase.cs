using DB1.Core.Communication;

namespace DB1.Desafio.Application.Commands.Funcionario.ObterTodos
{
    public interface IObterTodosFuncionarioUseCase
    {
        Task<ResponseResult<IEnumerable<ObterTodosFuncionarioResponse>>> ExecutarAsync();
    }
}
