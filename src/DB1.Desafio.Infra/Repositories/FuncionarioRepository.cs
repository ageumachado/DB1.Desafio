using DB1.Desafio.Domain.Entities;
using DB1.Desafio.Domain.Repositories;
using DB1.Desafio.Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace DB1.Desafio.Infra.Repositories
{
    internal class FuncionarioRepository(Db1DataContext context) : Repository<Funcionario>(context), IFuncionarioRepository
    {
        public async Task<bool> ExisteCargoAsync(Funcionario funcionario, Guid cargoId)
            => await Context.Set<FuncionarioCargo>()
                .AnyAsync(p => p.FuncionarioId == funcionario.Id && p.CargoId == cargoId);

        public void VincularCargo(Funcionario funcionario, Cargo cargo, DateTime dataVinculo)
        {
            Adicionar<FuncionarioCargo>(new FuncionarioCargo(funcionario, cargo, dataVinculo));
        }
    }
}
