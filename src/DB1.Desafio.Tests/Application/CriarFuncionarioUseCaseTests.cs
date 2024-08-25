using AutoMapper;
using DB1.Core.Data;
using DB1.Desafio.Application.Commands.Funcionario.Criar;
using DB1.Desafio.Application.Mappings;
using DB1.Desafio.Domain.Entities;
using DB1.Desafio.Domain.Repositories;
using DB1.Desafio.Tests.Fixtures;
using DB1.Desafio.Tests.Utils;
using Moq;

namespace DB1.Desafio.Tests.Application
{
    [Collection(nameof(FuncionarioBogusCollection))]
    public class CriarFuncionarioUseCaseTests
    {
        private readonly FuncionarioBogusFixture funcionarioBogusFixture;
        private readonly IMapper mapper;
        private readonly Mock<IFuncionarioRepository> funcionarioRepoMock;
        private readonly Mock<ICargoRepository> cargoRepoMock;

        public CriarFuncionarioUseCaseTests(FuncionarioBogusFixture funcionarioBogusFixture)
        {
            this.funcionarioBogusFixture = funcionarioBogusFixture;
            mapper = MapperGenerater.ObterMapper(new AutomapperProfile());

            funcionarioRepoMock = new Mock<IFuncionarioRepository>();
            cargoRepoMock = new Mock<ICargoRepository>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();

            unitOfWorkMock.Setup(p => p.Commit()).ReturnsAsync(true);
            funcionarioRepoMock.Setup(p => p.UnitOfWork).Returns(unitOfWorkMock.Object);
        }

        [Fact(DisplayName = "Novo funcionario válido")]
        [Trait("Funcionario", "UseCase - Criar")]
        public async Task FuncionarioUseCase_NovoFuncionario_DeveEstarValido()
        {
            // Arrange
            var funcionarioUseCase = new CriarFuncionarioUseCase(funcionarioRepoMock.Object, cargoRepoMock.Object, mapper);
            var funcionario = funcionarioBogusFixture.GerarFuncionarioValido();
            funcionario.Id = Guid.Empty;
            var funcionarioRequest = mapper.Map<CriarFuncionarioRequest>(funcionario);

            // Act
            var response = await funcionarioUseCase.ExecutarAsync(funcionarioRequest);

            // Assert
            Assert.True(response.EhValido());
            Assert.NotNull(response.Data);
            Assert.IsType<CriarFuncionarioResponse>(response.Data);
            Assert.Equal(funcionario.Id, response.Data.Id);
            funcionarioRepoMock.Verify(r => r.Adicionar(funcionario), Times.Once);
        }

        [Fact(DisplayName = "Novo funcionario sem empresa e com cargo, não vincular cargo")]
        [Trait("Funcionario", "UseCase - Criar")]
        public async Task FuncionarioUseCase_NovoFuncionarioSemEmpresaComCargo_NaoVincularCargo()
        {
            // Arrange
            var funcionarioUseCase = new CriarFuncionarioUseCase(funcionarioRepoMock.Object, cargoRepoMock.Object, mapper);
            var funcionario = funcionarioBogusFixture.GerarFuncionarioValido();
            funcionario.Id = Guid.Empty;
            
            var funcionarioRequest = mapper.Map<CriarFuncionarioRequest>(funcionario);
            funcionarioRequest.EmpresaId = null;
            funcionarioRequest.CargoId = Guid.NewGuid();

            // Act
            var response = await funcionarioUseCase.ExecutarAsync(funcionarioRequest);

            // Assert
            Assert.True(response.EhValido());
            Assert.NotNull(response.Data);
            cargoRepoMock.Verify(p => p.ObterPorIdAsync(new[] { It.IsAny<Guid>() }), Times.Never);
            funcionarioRepoMock.Verify(r => r.VincularCargo(funcionario, It.IsAny<Cargo>(), DateTime.Now), Times.Never);
            funcionarioRepoMock.Verify(r => r.Adicionar(funcionario), Times.Once);
        }

        [Fact(DisplayName = "Novo funcionario com empresa e com cargo, não vincular cargo")]
        [Trait("Funcionario", "UseCase - Criar")]
        public async Task FuncionarioUseCase_NovoFuncionarioComEmpresaComCargo_VincularCargo()
        {
            // Arrange
            var cargo = new Cargo("Teste")
            {
                Id = Guid.NewGuid()
            };
            cargoRepoMock.Setup(p => p.ObterPorIdAsync(cargo.Id)).ReturnsAsync(cargo);
            var funcionarioUseCase = new CriarFuncionarioUseCase(funcionarioRepoMock.Object, cargoRepoMock.Object, mapper);
            var funcionario = funcionarioBogusFixture.GerarFuncionarioValido();
            funcionario.Id = Guid.Empty;
            funcionarioRepoMock.Setup(p => p.VincularCargo(funcionario, cargo, It.IsAny<DateTime>()));
            var funcionarioRequest = mapper.Map<CriarFuncionarioRequest>(funcionario);
            funcionarioRequest.EmpresaId = Guid.NewGuid();
            funcionarioRequest.CargoId = cargo.Id;            

            // Act
            var response = await funcionarioUseCase.ExecutarAsync(funcionarioRequest);

            // Assert
            Assert.True(response.EhValido());
            Assert.NotNull(response.Data);
            cargoRepoMock.Verify(p => p.ObterPorIdAsync(cargo.Id), Times.Once);
            funcionarioRepoMock.Verify(r => r.VincularCargo(funcionario, cargo, It.IsAny<DateTime>()), Times.Once);
            funcionarioRepoMock.Verify(r => r.Adicionar(funcionario), Times.Once);
        }
    }
}
