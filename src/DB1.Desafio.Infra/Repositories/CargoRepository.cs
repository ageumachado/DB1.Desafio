using DB1.Desafio.Domain.Entities;
using DB1.Desafio.Domain.Repositories;
using DB1.Desafio.Infra.Data;

namespace DB1.Desafio.Infra.Repositories
{
    internal class CargoRepository(Db1DataContext context) : Repository<Cargo>(context), ICargoRepository
    {
    }
}
