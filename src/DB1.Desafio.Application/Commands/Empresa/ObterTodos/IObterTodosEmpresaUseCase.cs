using DB1.Core.Communication;

namespace DB1.Desafio.Application.Commands.Empresa.ObterTodos
{
    public interface IObterTodosEmpresaUseCase
    {
        Task<ResponseResult<IEnumerable<ObterTodosEmpresaResponse>>> ExecutarAsync();
    }


}
