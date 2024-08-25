using DB1.Core.Communication;

namespace DB1.Desafio.Application.Commands.Empresa.Excluir
{
    public interface IExcluirEmpresaUseCase
    {
        Task<ResponseResult> ExecutarAsync(Guid id);
    }
}
