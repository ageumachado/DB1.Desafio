using AutoMapper;
using DB1.Core.Data;
using DB1.Desafio.Application.Commands.Funcionario.Editar;
using DB1.Desafio.Application.Mappings;
using DB1.Desafio.Domain.Entities;
using DB1.Desafio.Domain.Repositories;
using DB1.Desafio.Tests.Fixtures;
using DB1.Desafio.Tests.Utils;
using Moq;

namespace DB1.Desafio.Tests.Application
{
    [Collection(nameof(FuncionarioBogusCollection))]
    public class EditarFuncionarioUseCaseTests
    {
        private readonly FuncionarioBogusFixture funcionarioBogusFixture;
        private readonly IMapper mapper;
        private readonly Mock<IFuncionarioRepository> funcionarioRepoMock;
        private readonly Mock<ICargoRepository> cargoRepoMock;

        public EditarFuncionarioUseCaseTests(FuncionarioBogusFixture funcionarioBogusFixture)
        {
            this.funcionarioBogusFixture = funcionarioBogusFixture;
            mapper = MapperGenerater.ObterMapper(new AutomapperProfile());

            funcionarioRepoMock = new Mock<IFuncionarioRepository>();
            cargoRepoMock = new Mock<ICargoRepository>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();

            unitOfWorkMock.Setup(p => p.Commit()).ReturnsAsync(true);
            funcionarioRepoMock.Setup(p => p.UnitOfWork).Returns(unitOfWorkMock.Object);
        }

        [Fact(DisplayName = "Editar funcionario válido")]
        [Trait("Funcionario", "UseCase - Editar")]
        public async Task FuncionarioUseCase_EditarFuncionario_DeveEstarValido()
        {
            // Arrange
            var funcionarioUseCase = new EditarFuncionarioUseCase(funcionarioRepoMock.Object, cargoRepoMock.Object, mapper);
            var funcionario = funcionarioBogusFixture.GerarFuncionarioValido();
            var funcionarioRequest = mapper.Map<EditarFuncionarioRequest>(funcionario);
            funcionarioRepoMock.Setup(p => p.ObterPorIdAsync(It.IsAny<Guid>())).ReturnsAsync(funcionario);

            // Act
            var response = await funcionarioUseCase.ExecutarAsync(funcionario.Id, funcionarioRequest);

            // Assert
            Assert.True(response.EhValido());
            Assert.NotNull(response.Data);
            Assert.IsType<EditarFuncionarioResponse>(response.Data);
            Assert.Equal(funcionario.Id, response.Data.Id);
            funcionarioRepoMock.Verify(r => r.Editar(funcionario), Times.Once);
        }

        [Fact(DisplayName = "Editar funcionario sem empresa e com cargo, não vincular cargo")]
        [Trait("Funcionario", "UseCase - Editar")]
        public async Task FuncionarioUseCase_EditarFuncionarioSemEmpresaComCargo_NaoVincularCargo()
        {
            // Arrange
            var funcionarioUseCase = new EditarFuncionarioUseCase(funcionarioRepoMock.Object, cargoRepoMock.Object, mapper);
            var funcionario = funcionarioBogusFixture.GerarFuncionarioValido();
            funcionarioRepoMock.Setup(p => p.ObterPorIdAsync(It.IsAny<Guid>())).ReturnsAsync(funcionario);

            var funcionarioRequest = mapper.Map<EditarFuncionarioRequest>(funcionario);
            funcionarioRequest.EmpresaId = null;
            funcionarioRequest.CargoId = Guid.NewGuid();

            // Act
            var response = await funcionarioUseCase.ExecutarAsync(funcionario.Id, funcionarioRequest);

            // Assert
            Assert.True(response.EhValido());
            Assert.NotNull(response.Data);
            cargoRepoMock.Verify(p => p.ObterPorIdAsync(new[] { It.IsAny<Guid>() }), Times.Never);
            funcionarioRepoMock.Verify(r => r.VincularCargo(funcionario, It.IsAny<Cargo>(), DateTime.Now), Times.Never);
            funcionarioRepoMock.Verify(r => r.Editar(funcionario), Times.Once);
        }

        [Fact(DisplayName = "Editar funcionario com empresa e com cargo, vincular cargo")]
        [Trait("Funcionario", "UseCase - Editar")]
        public async Task FuncionarioUseCase_EditarFuncionarioComEmpresaComCargo_VincularCargo()
        {
            // Arrange
            var cargo = new Cargo("Teste")
            {
                Id = Guid.NewGuid()
            };
            cargoRepoMock.Setup(p => p.ObterPorIdAsync(cargo.Id)).ReturnsAsync(cargo);
            var funcionarioUseCase = new EditarFuncionarioUseCase(funcionarioRepoMock.Object, cargoRepoMock.Object, mapper);
            var funcionario = funcionarioBogusFixture.GerarFuncionarioValido();
            funcionarioRepoMock.Setup(p => p.ObterPorIdAsync(It.IsAny<Guid>())).ReturnsAsync(funcionario);
            funcionarioRepoMock.Setup(p => p.VincularCargo(funcionario, cargo, It.IsAny<DateTime>()));
            var funcionarioRequest = mapper.Map<EditarFuncionarioRequest>(funcionario);
            funcionarioRequest.EmpresaId = Guid.NewGuid();
            funcionarioRequest.CargoId = cargo.Id;

            // Act
            var response = await funcionarioUseCase.ExecutarAsync(funcionario.Id, funcionarioRequest);

            // Assert
            Assert.True(response.EhValido());
            Assert.NotNull(response.Data);
            cargoRepoMock.Verify(p => p.ObterPorIdAsync(cargo.Id), Times.Once);
            funcionarioRepoMock.Verify(r => r.VincularCargo(funcionario, cargo, It.IsAny<DateTime>()), Times.Once);
            funcionarioRepoMock.Verify(r => r.Editar(funcionario), Times.Once);
        }

        [Fact(DisplayName = "Editar funcionario com empresa e com cargo já vinculado, não vincular cargo")]
        [Trait("Funcionario", "UseCase - Editar")]
        public async Task FuncionarioUseCase_EditarFuncionarioComEmpresaComCargoJaVinculado_NaoVincularCargo()
        {
            // Arrange
            var cargo = new Cargo("Teste")
            {
                Id = Guid.NewGuid()
            };
            cargoRepoMock.Setup(p => p.ObterPorIdAsync(cargo.Id)).ReturnsAsync(cargo);
            var funcionarioUseCase = new EditarFuncionarioUseCase(funcionarioRepoMock.Object, cargoRepoMock.Object, mapper);
            var funcionario = funcionarioBogusFixture.GerarFuncionarioValido();
            funcionarioRepoMock.Setup(p => p.ObterPorIdAsync(It.IsAny<Guid>())).ReturnsAsync(funcionario);
            funcionarioRepoMock.Setup(p => p.ExisteCargoAsync(funcionario, It.IsAny<Guid>())).ReturnsAsync(true);
            funcionarioRepoMock.Setup(p => p.VincularCargo(funcionario, cargo, It.IsAny<DateTime>()));
            var funcionarioRequest = mapper.Map<EditarFuncionarioRequest>(funcionario);
            funcionarioRequest.EmpresaId = Guid.NewGuid();
            funcionarioRequest.CargoId = cargo.Id;

            // Act
            var response = await funcionarioUseCase.ExecutarAsync(funcionario.Id, funcionarioRequest);

            // Assert
            Assert.True(response.EhValido());
            Assert.NotNull(response.Data);
            cargoRepoMock.Verify(p => p.ObterPorIdAsync(cargo.Id), Times.Never);
            funcionarioRepoMock.Verify(r => r.VincularCargo(funcionario, cargo, It.IsAny<DateTime>()), Times.Never);
            funcionarioRepoMock.Verify(r => r.Editar(funcionario), Times.Once);
        }
    }
}
