using DB1.Desafio.Domain.Entities;
using DB1.Desafio.Domain.Enums;
using DB1.Desafio.Tests.Fixtures;
using DB1.Desafio.Tests.Utils;

namespace DB1.Desafio.Tests.Domain
{
    [Collection(nameof(FuncionarioBogusCollection))]
    public class FuncionarioTests(FuncionarioBogusFixture funcionarioBogusFixture)
    {
        private const string NOME = "Nome";
        private static readonly string CPF = Geradores.GerarCpf();
        private static readonly DateTime DATA_CONTRATACAO = DateTime.Now.AddDays(-20);

        [Fact(DisplayName = "Funcionario Dados Corretos, deve estar válida")]
        [Trait("Categoria", "Funcionario Trait Testes")]
        public void Funcionario_DadosCorretos_DeveEstarValida()
        {
            // Arrange
            var funcionario = funcionarioBogusFixture.GerarFuncionarioValido();

            // Act
            var result = funcionario.Valid;

            // Assert 
            Assert.True(result);
            Assert.Empty(funcionario.ValidationResult.Errors);
        }

        [Fact(DisplayName = "Funcionario Dados incorretos, deve estar inválida")]
        [Trait("Categoria", "Funcionario Trait Testes")]
        public void Funcionario_DadosErrados_DeveEstarInvalida()
        {
            // Arrange
            var funcionario = funcionarioBogusFixture.GerarFuncionarioInvalido();

            // Act
            var result = funcionario.Invalid;

            // Assert 
            Assert.True(result);
            Assert.NotEmpty(funcionario.ValidationResult.Errors);
        }

        [Fact(DisplayName = "Nome quantidade caracteres acima, deve gerar error")]
        [Trait("Categoria", "Funcionario Trait Testes")]
        public void Funcionario_Nome_QuantidadeCaracteresAcima_DeveGerarError()
        {
            // Arrange
            var nomeTamanhoAcima = FuncionarioValidator.NOME_MAX_LENGTH + 1;
            var funcionario = new Funcionario(Geradores.CaracteresAleatorio(nomeTamanhoAcima), 
                CPF, DATA_CONTRATACAO);

            // Act
            var result = funcionario.Invalid;

            // Assert 
            Assert.True(result);
            Assert.Equal(1, funcionario.ValidationResult.Errors.Count);
        }

        [Fact(DisplayName = "Data contratação igual data mínima, deve gerar error")]
        [Trait("Categoria", "Funcionario Trait Testes")]
        public void Funcionario_DataContratacao_IgualDataMinima_DeveGerarError()
        {
            // Arrange
            var funcionario = new Funcionario(NOME, CPF, DateTime.MinValue);

            // Act
            var result = funcionario.Invalid;

            // Assert 
            Assert.True(result);
            Assert.Equal(1, funcionario.ValidationResult.Errors.Count);
        }

        [Fact(DisplayName = "Data contratação maior que data mínima, deve gerar sucesso")]
        [Trait("Categoria", "Funcionario Trait Testes")]
        public void Funcionario_DataContratacao_MaiorDataMinima_DeveGerarSucesso()
        {
            // Arrange
            var funcionario = new Funcionario(NOME, CPF, DateTime.MinValue.AddDays(1));

            // Act
            var result = funcionario.Valid;

            // Assert 
            Assert.True(result);
            Assert.Empty(funcionario.ValidationResult.Errors);
        }

        [Fact(DisplayName = "Cpf número acima do padrão, deve gerar error")]
        [Trait("Categoria", "Funcionario Trait Testes")]
        public void Funcionario_Cpf_AcimaNumeroPadrao_DeveGerarError()
        {
            // Arrange
            var funcionario = new Funcionario(NOME, string.Concat(CPF, "0"), DateTime.Now.AddDays(-100));

            // Act
            var result = funcionario.Invalid;

            // Assert 
            Assert.True(result);
            Assert.Contains($"Tamanho deve ser {FuncionarioValidator.CPF_LENGTH}", funcionario.ValidationResult.Errors.Select(s => s.ErrorMessage));
        }

        [Fact(DisplayName = "Cpf número abaixo do padrão, deve gerar error")]
        [Trait("Categoria", "Funcionario Trait Testes")]
        public void Funcionario_Cpf_AbaixoNumeroPadrao_DeveGerarError()
        {
            // Arrange
            var funcionario = new Funcionario(NOME, CPF.Substring(0, CPF.Length - 1), DateTime.Now.AddDays(-100));

            // Act
            var result = funcionario.Invalid;

            // Assert 
            Assert.True(result);
            Assert.Contains($"Tamanho deve ser {FuncionarioValidator.CPF_LENGTH}", funcionario.ValidationResult.Errors.Select(s => s.ErrorMessage));
        }

        [Fact(DisplayName = "Funcionario Valores Corretos")]
        [Trait("Categoria", "Funcionario Trait Testes")]
        public void Funcionario_NovaFuncionario_DeveValoresCorretos()
        {
            // Arrange
            var funcionario = new Funcionario(NOME, CPF, DATA_CONTRATACAO);

            // Act & Assert
            Assert.Equal(NOME, funcionario.Nome);
            Assert.Equal(CPF, funcionario.Cpf);
            Assert.Equal(DATA_CONTRATACAO, funcionario.DataContratacao);
        }

        [Fact(DisplayName = "Funcionario inativar deve retornar status inativo")]
        [Trait("Categoria", "Funcionario Trait Testes")]
        public void Funcionario_Inativar_DeveRetornarStatusInativo()
        {
            // Arrange
            var funcionario = funcionarioBogusFixture.GerarFuncionarioValido();

            // Act
            funcionario.Inativar();

            // Assert
            Assert.Equal(Status.Inativo, funcionario.Status);
        }

        [Fact(DisplayName = "Funcionario ativar deve retornar status ativo")]
        [Trait("Categoria", "Funcionario Trait Testes")]
        public void Funcionario_Ativar_DeveRetornarStatusAtivo()
        {
            // Arrange
            var funcionario = funcionarioBogusFixture.GerarFuncionarioValido();

            // Act
            funcionario.Ativar();

            // Assert
            Assert.Equal(Status.Ativo, funcionario.Status);
        }

        [Fact(DisplayName = "Funcionario remover deve retornar status removido")]
        [Trait("Categoria", "Funcionario Trait Testes")]
        public void Funcionario_Remover_DeveRetornarStatusRemovido()
        {
            // Arrange
            var funcionario = funcionarioBogusFixture.GerarFuncionarioValido();

            // Act
            funcionario.Remover();

            // Assert
            Assert.Equal(Status.Removido, funcionario.Status);
        }
    }
}
