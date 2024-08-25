using DB1.Core.Communication;

namespace DB1.Desafio.Application.Commands.Empresa.Criar
{
    public interface ICriarEmpresaUseCase
    {
        Task<ResponseResult<CriarEmpresaResponse>> ExecutarAsync(CriarEmpresaRequest request);
    }
}
