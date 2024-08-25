using DB1.Core.Data;
using DB1.Desafio.Domain.Entities;

namespace DB1.Desafio.Domain.Repositories
{
    public interface IFuncionarioRepository : IRepository<Funcionario> 
    {
        void VincularCargo(Funcionario funcionario, Cargo cargo, DateTime dataVinculo);
        Task<bool> ExisteCargoAsync(Funcionario funcionario, Guid cargoId);
    }
}
