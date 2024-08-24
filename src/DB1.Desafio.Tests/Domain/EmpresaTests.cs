using DB1.Desafio.Domain.Entities;
using DB1.Desafio.Domain.Enums;
using DB1.Desafio.Tests.Fixtures;
using DB1.Desafio.Tests.Utils;

namespace DB1.Desafio.Tests.Domain
{
    [Collection(nameof(EmpresaBogusCollection))]
    public class EmpresaTests(EmpresaBogusFixture empresaBogusFixture)
    {
        private const string NOME = "Nome";
        private static readonly string CNPJ = Geradores.GerarCnpj();
        private static readonly DateTime DATA_FUNDACAO = DateTime.Now.AddDays(-100);

        [Fact(DisplayName = "Empresa Dados Corretos, deve estar válida")]
        [Trait("Categoria", "Empresa Trait Testes")]
        public void Empresa_DadosCorretos_DeveEstarValida()
        {
            // Arrange
            var empresa = empresaBogusFixture.GerarEmpresaValida();

            // Act
            var result = empresa.Valid;

            // Assert 
            Assert.True(result);
            Assert.Empty(empresa.ValidationResult.Errors);
        }

        [Fact(DisplayName = "Empresa Dados incorretos, deve estar inválida")]
        [Trait("Categoria", "Empresa Trait Testes")]
        public void Empresa_DadosErrados_DeveEstarInvalida()
        {
            // Arrange
            var empresa = empresaBogusFixture.GerarEmpresaInvalida();

            // Act
            var result = empresa.Invalid;

            // Assert 
            Assert.True(result);
            Assert.NotEmpty(empresa.ValidationResult.Errors);
        }

        [Fact(DisplayName = "Nome quantidade caracteres acima, deve gerar error")]
        [Trait("Categoria", "Empresa Trait Testes")]
        public void Empresa_Nome_QuantidadeCaracteresAcima_DeveGerarError()
        {
            // Arrange
            var nomeTamanhoAcima = EmpresaValidator.NOME_MAX_LENGTH + 1;
            var empresa = new Empresa(Guid.NewGuid(), Geradores.CaracteresAleatorio(nomeTamanhoAcima), CNPJ, DATA_FUNDACAO); ;

            // Act
            var result = empresa.Invalid;

            // Assert 
            Assert.True(result);
            Assert.Equal(1, empresa.ValidationResult.Errors.Count);
        }

        [Fact(DisplayName = "Data fundação igual data mínima, deve gerar error")]
        [Trait("Categoria", "Empresa Trait Testes")]
        public void Empresa_DataFundacao_IgualDataMinima_DeveGerarError()
        {
            // Arrange
            var empresa = new Empresa(Guid.NewGuid(), NOME, CNPJ, DateTime.MinValue);

            // Act
            var result = empresa.Invalid;

            // Assert 
            Assert.True(result);
            Assert.Equal(1, empresa.ValidationResult.Errors.Count);
        }

        [Fact(DisplayName = "Data fundação maior que data mínima, deve gerar sucesso")]
        [Trait("Categoria", "Empresa Trait Testes")]
        public void Empresa_DataFundacao_MaiorDataMinima_DeveGerarSucesso()
        {
            // Arrange
            var empresa = new Empresa(Guid.NewGuid(), NOME, CNPJ, DateTime.MinValue.AddDays(1));

            // Act
            var result = empresa.Valid;

            // Assert 
            Assert.True(result);
            Assert.Empty(empresa.ValidationResult.Errors);
        }

        [Fact(DisplayName = "Cnpj número acima do padrão, deve gerar error")]
        [Trait("Categoria", "Empresa Trait Testes")]
        public void Empresa_Cnpj_AcimaNumeroPadrao_DeveGerarError()
        {
            // Arrange
            var empresa = new Empresa(Guid.NewGuid(), NOME, new Core.ValueObjects.Cnpj(string.Concat(CNPJ, "0")), DateTime.Now.AddDays(-100));

            // Act
            var result = empresa.Invalid;

            // Assert 
            Assert.True(result);
            Assert.Contains($"Tamanho deve ser {EmpresaValidator.CNPJ_LENGTH}", empresa.ValidationResult.Errors.Select(s => s.ErrorMessage));
        }

        [Fact(DisplayName = "Cnpj número abaixo do padrão, deve gerar error")]
        [Trait("Categoria", "Empresa Trait Testes")]
        public void Empresa_Cnpj_AbaixoNumeroPadrao_DeveGerarError()
        {
            // Arrange
            var empresa = new Empresa(Guid.NewGuid(), NOME, new Core.ValueObjects.Cnpj(CNPJ.Substring(0, CNPJ.Length - 1)), DateTime.Now.AddDays(-100));

            // Act
            var result = empresa.Invalid;

            // Assert 
            Assert.True(result);
            Assert.Contains($"Tamanho deve ser {EmpresaValidator.CNPJ_LENGTH}", empresa.ValidationResult.Errors.Select(s => s.ErrorMessage));
        }

        [Fact(DisplayName = "Empresa Valores Corretos")]
        [Trait("Categoria", "Empresa Trait Testes")]
        public void Empresa_NovaEmpresa_DeveValoresCorretos()
        {
            // Arrange
            var empresa = new Empresa(Guid.NewGuid(), NOME, CNPJ, DATA_FUNDACAO);

            // Act & Assert
            Assert.Equal(NOME, empresa.Nome);
            Assert.Equal(CNPJ, empresa.Cnpj);
            Assert.Equal(DATA_FUNDACAO, empresa.DataFundacao);
        }

        [Fact(DisplayName = "Empresa inativar deve retornar status inativo")]
        [Trait("Categoria", "Empresa Trait Testes")]
        public void Empresa_Inativar_DeveRetornarStatusInativo()
        {
            // Arrange
            var empresa = empresaBogusFixture.GerarEmpresaValida();

            // Act
            empresa.Inativar();

            // Assert
            Assert.Equal(StatusEmpresa.Inativo, empresa.Status);
        }

        [Fact(DisplayName = "Empresa ativar deve retornar status ativo")]
        [Trait("Categoria", "Empresa Trait Testes")]
        public void Empresa_Ativar_DeveRetornarStatusAtivo()
        {
            // Arrange
            var empresa = empresaBogusFixture.GerarEmpresaValida();

            // Act
            empresa.Ativar();

            // Assert
            Assert.Equal(StatusEmpresa.Ativo, empresa.Status);
        }

        [Fact(DisplayName = "Empresa remover deve retornar status removido")]
        [Trait("Categoria", "Empresa Trait Testes")]
        public void Empresa_Remover_DeveRetornarStatusRemovido()
        {
            // Arrange
            var empresa = empresaBogusFixture.GerarEmpresaValida();

            // Act
            empresa.Remover();

            // Assert
            Assert.Equal(StatusEmpresa.Removido, empresa.Status);
        }
    }
}
