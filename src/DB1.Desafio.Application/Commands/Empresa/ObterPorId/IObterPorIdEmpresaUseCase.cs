using DB1.Core.Communication;

namespace DB1.Desafio.Application.Commands.Empresa.ObterPorId
{
    public interface IObterPorIdEmpresaUseCase
    {
        Task<ResponseResult<ObterPorIdEmpresaResponse>> ExecutarAsync(Guid id);
    }
}
