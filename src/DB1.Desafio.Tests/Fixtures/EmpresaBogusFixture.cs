using Bogus;
using DB1.Desafio.Domain.Entities;
using DB1.Desafio.Tests.Utils;

namespace DB1.Desafio.Tests.Fixtures
{
    [CollectionDefinition(nameof(EmpresaBogusCollection))]
    public class EmpresaBogusCollection : ICollectionFixture<EmpresaBogusFixture>
    { }

    public class EmpresaBogusFixture : BaseFixture, IDisposable
    {
        public Empresa GerarEmpresaValida()
        {
            return GerarEmpresas(1, true).First();
        }

        public Empresa GerarEmpresaInvalida()
        {
            return GerarEmpresas(1, false).First();
        }

        private IEnumerable<Empresa> GerarEmpresas(int quantidade, bool dadosValido)
        {
            var entidades = new Faker<Empresa>("pt_BR")
                .CustomInstantiator(f =>
                {
                    if (!dadosValido)
                    {
                        return new Empresa(
                            "",
                            "",
                            DateTime.MinValue);
                    }
                    return new Empresa(
                    f.Company.CompanyName(),
                    Geradores.GerarCnpj(),
                    f.Date.Recent(100).Date);
                });

            return entidades.Generate(quantidade);
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
