using AutoMapper;
using DB1.Core.Communication;
using DB1.Core.DomainObjects;
using DB1.Core.Extensions;
using DB1.Desafio.Domain.Repositories;

namespace DB1.Desafio.Application.Commands.Funcionario.ObterPorId
{
    public interface IObterPorIdFuncionarioUseCase
    {
        Task<ResponseResult<ObterPorIdFuncionarioResponse>> ExecutarAsync(Guid id);
    }

    public class ObterPorIdFuncionarioUseCase(
        IFuncionarioRepository funcionarioRepository,
        IMapper mapper) : CommandUseCase, IObterPorIdFuncionarioUseCase
    {
        public async Task<ResponseResult<ObterPorIdFuncionarioResponse>> ExecutarAsync(Guid id)
        {
            if (id == Guid.Empty) return ResponseResultError("Informe o ID corretamente");

            var entidade = await funcionarioRepository.ObterPorIdAsync(id);

            if (entidade is null) return ResponseResultError("Registro não encontrado");

            var result = mapper.Map<ObterPorIdFuncionarioResponse>(entidade);
            return result.ToResponseResult();
        }
    }

    public class ObterPorIdFuncionarioResponse : BaseFuncionario { }
}
