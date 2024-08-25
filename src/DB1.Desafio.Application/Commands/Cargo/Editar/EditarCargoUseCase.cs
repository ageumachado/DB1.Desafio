using AutoMapper;
using DB1.Core.Communication;
using DB1.Core.DomainObjects;
using DB1.Core.Extensions;
using DB1.Desafio.Domain.Repositories;

namespace DB1.Desafio.Application.Commands.Cargo.Editar
{
    internal class EditarCargoUseCase(
        ICargoRepository cargoRepository,
        IMapper mapper) : CommandUseCase, IEditarCargoUseCase
    {
        public async Task<ResponseResult<EditarCargoResponse>> ExecutarAsync(Guid id, EditarCargoRequest request)
        {
            if (request is null) return ResponseResultError("Requisição não recebida");

            if (request.EhInvalido()) return request.ToResponseResultError();

            if (await cargoRepository
                .ExisteAsync(p => p.Id != id && p.Nome == request.Nome!))
                return ResponseResultError("Nome já informado anteriormente");

            var entidade = await cargoRepository.ObterPorIdAsync(id);

            if (entidade is null) return ResponseResultError("Registro não encontrado");

            mapper.Map(request, entidade);

            cargoRepository.Editar(entidade);

            var result = await PersistirDados(cargoRepository.UnitOfWork);
            var response = mapper.Map<EditarCargoResponse>(entidade);
            return result.ToResponseResult(response);
        }
    }
}
