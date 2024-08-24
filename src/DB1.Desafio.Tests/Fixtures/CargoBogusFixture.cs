using Bogus;
using DB1.Desafio.Domain.Entities;

namespace DB1.Desafio.Tests.Fixtures
{
    [CollectionDefinition(nameof(CargoBogusCollection))]
    public class CargoBogusCollection : ICollectionFixture<CargoBogusFixture>
    { }

    public class CargoBogusFixture : BaseFixture, IDisposable
    {
        public Cargo GerarCargoValido()
        {
            return GerarCargos(1, true).First();
        }

        public Cargo GerarCargoInvalido()
        {
            return GerarCargos(1, false).First();
        }

        private IEnumerable<Cargo> GerarCargos(int quantidade, bool dadosValido)
        {
            var entidades = new Faker<Cargo>("pt_BR")
                .CustomInstantiator(f =>
                {
                    if (!dadosValido)
                    {
                        return new Cargo("");
                    }
                    return new Cargo(f.Lorem.Word());
                });

            return entidades.Generate(quantidade);
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
