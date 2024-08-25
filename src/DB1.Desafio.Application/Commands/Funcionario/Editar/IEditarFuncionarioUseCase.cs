using DB1.Core.Communication;

namespace DB1.Desafio.Application.Commands.Funcionario.Editar
{
    public interface IEditarFuncionarioUseCase
    {
        Task<ResponseResult<EditarFuncionarioResponse>> ExecutarAsync(Guid id, EditarFuncionarioRequest request);
    }
}
