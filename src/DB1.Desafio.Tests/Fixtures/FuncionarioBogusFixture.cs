using Bogus;
using DB1.Desafio.Domain.Entities;
using DB1.Desafio.Tests.Utils;

namespace DB1.Desafio.Tests.Fixtures
{
    [CollectionDefinition(nameof(FuncionarioBogusCollection))]
    public class FuncionarioBogusCollection : ICollectionFixture<FuncionarioBogusFixture>
    { }

    public class FuncionarioBogusFixture : BaseFixture, IDisposable
    {
        public Funcionario GerarFuncionarioValido()
        {
            return GerarFuncionarios(1, true).First();
        }

        public Funcionario GerarFuncionarioInvalido()
        {
            return GerarFuncionarios(1, false).First();
        }

        private IEnumerable<Funcionario> GerarFuncionarios(int quantidade, bool dadosValido)
        {
            var entidades = new Faker<Funcionario>("pt_BR")
                .CustomInstantiator(f =>
                {
                    if (!dadosValido)
                    {
                        return new Funcionario(
                            "",
                            "",
                            DateTime.MinValue);
                    }
                    return new Funcionario(
                    f.Name.FullName(),
                    Geradores.GerarCpf(),
                    f.Date.Recent(20).Date);
                });

            return entidades.Generate(quantidade);
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
