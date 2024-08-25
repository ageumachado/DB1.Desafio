using DB1.Core.Communication;
using DB1.Desafio.Application.Filtros;

namespace DB1.Desafio.Application.Commands.Funcionario.ObterComFiltro
{
    public interface IObterComFiltroFuncionarioUseCase
    {
        Task<ResponseResult<IEnumerable<ObterComFiltroFuncionarioResponse>>> ExecutarAsync(FuncionarioFiltro filtro);
    }
}
