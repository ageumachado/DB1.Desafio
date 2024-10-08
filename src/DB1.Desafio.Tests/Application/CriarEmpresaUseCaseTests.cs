﻿using AutoMapper;
using DB1.Core.Data;
using DB1.Desafio.Application.Commands.Empresa.Criar;
using DB1.Desafio.Application.Mappings;
using DB1.Desafio.Domain.Repositories;
using DB1.Desafio.Tests.Fixtures;
using DB1.Desafio.Tests.Utils;
using Moq;

namespace DB1.Desafio.Tests.Application
{
    [Collection(nameof(EmpresaBogusCollection))]
    public class CriarEmpresaUseCaseTests
    {
        private readonly EmpresaBogusFixture empresaBogusFixture;
        private readonly IMapper mapper;
        private readonly Mock<IEmpresaRepository> empresaRepoMock;

        public CriarEmpresaUseCaseTests(EmpresaBogusFixture empresaBogusFixture)
        {
            this.empresaBogusFixture = empresaBogusFixture;
            mapper = MapperGenerater.ObterMapper(new AutomapperProfile());

            empresaRepoMock = new Mock<IEmpresaRepository>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();

            unitOfWorkMock.Setup(p => p.Commit()).ReturnsAsync(true);
            empresaRepoMock.Setup(p => p.UnitOfWork).Returns(unitOfWorkMock.Object);
        }

        [Fact(DisplayName = "Nova empresa válida")]
        [Trait("Empresa", "UseCase - Criar")]
        public async Task EmpresaUseCase_NovaEmpresa_DeveEstarValido()
        {
            // Arrange
            var empresaUseCase = new CriarEmpresaUseCase(empresaRepoMock.Object, mapper);
            var empresa = empresaBogusFixture.GerarEmpresaValida();
            empresa.Id = Guid.Empty;
            var empresaRequest = mapper.Map<CriarEmpresaRequest>(empresa);

            // Act
            var response = await empresaUseCase.ExecutarAsync(empresaRequest);

            // Assert
            Assert.True(response.EhValido());
            Assert.NotNull(response.Data);
            Assert.IsType<CriarEmpresaResponse>(response.Data);
            Assert.Equal(empresa.Id, response.Data.Id);
            empresaRepoMock.Verify(r => r.Adicionar(empresa), Times.Once);
        }

        [Fact(DisplayName = "Nova empresa inválida")]
        [Trait("Empresa", "UseCase - Criar")]
        public async Task EmpresaUseCase_NovaEmpresa_DeveEstarInvalido()
        {
            // Arrange
            var empresaUseCase = new CriarEmpresaUseCase(empresaRepoMock.Object, mapper);
            var empresa = empresaBogusFixture.GerarEmpresaInvalida();
            var empresaRequest = mapper.Map<CriarEmpresaRequest>(empresa);

            // Act
            var response = await empresaUseCase.ExecutarAsync(empresaRequest);

            // Assert
            Assert.False(empresaRequest.EhValido());
            Assert.False(response.EhValido());
            Assert.Null(response.Data);
            empresaRepoMock.Verify(r => r.Adicionar(empresa), Times.Never);
        }
    }
}
