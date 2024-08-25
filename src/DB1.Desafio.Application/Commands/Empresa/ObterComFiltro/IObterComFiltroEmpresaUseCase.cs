using DB1.Core.Communication;
using DB1.Desafio.Application.Filtros;

namespace DB1.Desafio.Application.Commands.Empresa.ObterComFiltro
{
    public interface IObterComFiltroEmpresaUseCase
    {
        Task<ResponseResult<IEnumerable<ObterComFiltroEmpresaResponse>>> ExecutarAsync(EmpresaFiltro filtro);
    }
}
