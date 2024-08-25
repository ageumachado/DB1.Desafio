using DB1.Core.Communication;
using DB1.Core.DomainObjects;
using DB1.Core.Extensions;
using DB1.Desafio.Domain.Repositories;

namespace DB1.Desafio.Application.Commands.Cargo.Excluir
{
    public interface IExcluirCargoUseCase
    {
        Task<ResponseResult> ExecutarAsync(Guid id);
    }

    internal class ExcluirCargoUseCase(
        ICargoRepository cargoRepository) : CommandUseCase, IExcluirCargoUseCase
    {
        public async Task<ResponseResult> ExecutarAsync(Guid id)
        {
            if (id == Guid.Empty) return ResponseResultError("Informe o ID corretamente");

            var entidade = await cargoRepository.ObterPorIdAsync(id);

            if (entidade is null) return ResponseResultError("Registro não encontrado");

            cargoRepository.Remover(entidade);

            var result = await PersistirDados(cargoRepository.UnitOfWork);
            return result.ToResponseResult();
        }
    }
}
