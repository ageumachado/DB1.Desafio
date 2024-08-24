using DB1.Desafio.Domain.Entities;
using DB1.Desafio.Domain.Repositories;
using DB1.Desafio.Infra.Data;

namespace DB1.Desafio.Infra.Repositories
{
    internal class EmpresaRepository(Db1DataContext context) : Repository<Empresa>(context), IEmpresaRepository
    {
    }
}
