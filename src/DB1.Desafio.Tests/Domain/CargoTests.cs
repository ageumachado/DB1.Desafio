using DB1.Desafio.Domain.Entities;
using DB1.Desafio.Domain.Enums;
using DB1.Desafio.Domain.Validators;
using DB1.Desafio.Tests.Fixtures;
using DB1.Desafio.Tests.Utils;

namespace DB1.Desafio.Tests.Domain
{
    [Collection(nameof(CargoBogusCollection))]
    public class CargoTests(CargoBogusFixture cargoBogusFixture)
    {
        private const string NOME = "Nome";

        [Fact(DisplayName = "Cargo Dados Corretos, deve estar válida")]
        [Trait("Categoria", "Cargo Trait Testes")]
        public void Cargo_DadosCorretos_DeveEstarValida()
        {
            // Arrange
            var cargo = cargoBogusFixture.GerarCargoValido();

            // Act
            var result = cargo.Valid;

            // Assert 
            Assert.True(result);
            Assert.Empty(cargo.ValidationResult.Errors);
        }

        [Fact(DisplayName = "Cargo Dados incorretos, deve estar inválida")]
        [Trait("Categoria", "Cargo Trait Testes")]
        public void Cargo_DadosErrados_DeveEstarInvalida()
        {
            // Arrange
            var cargo = cargoBogusFixture.GerarCargoInvalido();

            // Act
            var result = cargo.Invalid;

            // Assert 
            Assert.True(result);
            Assert.NotEmpty(cargo.ValidationResult.Errors);
        }

        [Fact(DisplayName = "Nome quantidade caracteres acima, deve gerar error")]
        [Trait("Categoria", "Cargo Trait Testes")]
        public void Cargo_Nome_QuantidadeCaracteresAcima_DeveGerarError()
        {
            // Arrange
            var nomeTamanhoAcima = CargoValidator.NOME_MAX_LENGTH + 1;
            var cargo = new Cargo(Geradores.CaracteresAleatorio(nomeTamanhoAcima));

            // Act
            var result = cargo.Invalid;

            // Assert 
            Assert.True(result);
            Assert.Equal(1, cargo.ValidationResult.Errors.Count);
        }

        [Fact(DisplayName = "Cargo Valores Corretos")]
        [Trait("Categoria", "Cargo Trait Testes")]
        public void Funcionario_NovaFuncionario_DeveValoresCorretos()
        {
            // Arrange
            var cargo = new Cargo(NOME);

            // Act & Assert
            Assert.Equal(NOME, cargo.Nome);
        }

        [Fact(DisplayName = "Cargo inativar deve retornar status inativo")]
        [Trait("Categoria", "Cargo Trait Testes")]
        public void Cargo_Inativar_DeveRetornarStatusInativo()
        {
            // Arrange
            var cargo = cargoBogusFixture.GerarCargoValido();

            // Act
            cargo.Inativar();

            // Assert
            Assert.Equal(Status.Inativo, cargo.Status);
        }

        [Fact(DisplayName = "Cargo ativar deve retornar status ativo")]
        [Trait("Categoria", "Cargo Trait Testes")]
        public void Cargo_Ativar_DeveRetornarStatusAtivo()
        {
            // Arrange
            var cargo = cargoBogusFixture.GerarCargoValido();

            // Act
            cargo.Ativar();

            // Assert
            Assert.Equal(Status.Ativo, cargo.Status);
        }

        [Fact(DisplayName = "Cargo remover deve retornar status removido")]
        [Trait("Categoria", "Cargo Trait Testes")]
        public void Cargo_Remover_DeveRetornarStatusRemovido()
        {
            // Arrange
            var cargo = cargoBogusFixture.GerarCargoValido();

            // Act
            cargo.Remover();

            // Assert
            Assert.Equal(Status.Removido, cargo.Status);
        }
    }
}
