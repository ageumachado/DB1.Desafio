using AutoMapper;
using DB1.Core.Communication;
using DB1.Core.DomainObjects;
using DB1.Core.Extensions;
using DB1.Desafio.Domain.Repositories;

namespace DB1.Desafio.Application.Commands.Empresa.ObterPorId
{
    public class ObterPorIdEmpresaUseCase(
        IEmpresaRepository empresaRepository,
        IMapper mapper) : CommandUseCase, IObterPorIdEmpresaUseCase
    {
        public async Task<ResponseResult<ObterPorIdEmpresaResponse>> ExecutarAsync(Guid id)
        {
            if (id == Guid.Empty) return ResponseResultError("Informe o ID corretamente");

            var entidade = await empresaRepository.ObterPorIdAsync(id);

            if (entidade is null) return ResponseResultError("Registro não encontrado");

            var result = mapper.Map<ObterPorIdEmpresaResponse>(entidade);
            return result.ToResponseResult();
        }
    }
}
