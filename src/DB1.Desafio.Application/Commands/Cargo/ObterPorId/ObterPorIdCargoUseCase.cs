using AutoMapper;
using DB1.Core.Communication;
using DB1.Core.DomainObjects;
using DB1.Core.Extensions;
using DB1.Desafio.Domain.Repositories;

namespace DB1.Desafio.Application.Commands.Cargo.ObterPorId
{
    internal class ObterPorIdCargoUseCase(
        ICargoRepository cargoRepository,
        IMapper mapper) : CommandUseCase, IObterPorIdCargoUseCase
    {
        public async Task<ResponseResult<ObterPorIdCargoResponse>> ExecutarAsync(Guid id)
        {
            if (id == Guid.Empty) return ResponseResultError("Informe o ID corretamente");

            var entidade = await cargoRepository.ObterPorIdAsync(id);

            if (entidade is null) return ResponseResultError("Registro não encontrado");

            var result = mapper.Map<ObterPorIdCargoResponse>(entidade);
            return result.ToResponseResult();
        }
    }
}
