using AutoMapper;
using DB1.Core.Communication;
using DB1.Core.DomainObjects;
using DB1.Core.Extensions;
using DB1.Desafio.Domain.Repositories;

namespace DB1.Desafio.Application.Commands.Cargo.Criar
{
    internal class CriarCargoUseCase(
        ICargoRepository cargoRepository,
        IMapper mapper) : CommandUseCase, ICriarCargoUseCase
    {
        public async Task<ResponseResult<CriarCargoResponse>> ExecutarAsync(CriarCargoRequest request)
        {
            if (request is null) return ResponseResultError("Requisição não recebida");
            
            if (request.EhInvalido()) return request.ToResponseResultError();

            if (await cargoRepository
                .ExisteAsync(p => p.Nome == request.Nome))
                return ResponseResultError("Nome já existe");

            var entidade = mapper.Map<Domain.Entities.Cargo>(request);

            if (entidade.Invalid)
                return entidade.ValidationResult.ToResponseResult();

            cargoRepository.Adicionar(entidade);

            var result = await PersistirDados(cargoRepository.UnitOfWork);
            var response = mapper.Map<CriarCargoResponse>(entidade);
            return result.ToResponseResult(response);
        }
    }
}
