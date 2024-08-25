using DB1.Core.Communication;

namespace DB1.Desafio.Application.Commands.Empresa.Editar
{
    public interface IEditarEmpresaUseCase
    {
        Task<ResponseResult<EditarEmpresaResponse>> ExecutarAsync(Guid id, EditarEmpresaRequest request); 
    }
         
}
