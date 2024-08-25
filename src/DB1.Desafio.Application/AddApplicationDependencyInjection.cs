using DB1.Core.Mediator;
using DB1.Desafio.Application.Commands.Cargo.Criar;
using DB1.Desafio.Application.Commands.Cargo.Editar;
using DB1.Desafio.Application.Commands.Cargo.Excluir;
using DB1.Desafio.Application.Commands.Cargo.ObterPorId;
using DB1.Desafio.Application.Commands.Cargo.ObterTodos;
using DB1.Desafio.Application.Commands.Empresa.Criar;
using DB1.Desafio.Application.Commands.Empresa.Editar;
using DB1.Desafio.Application.Commands.Empresa.Excluir;
using DB1.Desafio.Application.Commands.Empresa.ObterComFiltro;
using DB1.Desafio.Application.Commands.Empresa.ObterPorId;
using DB1.Desafio.Application.Commands.Empresa.ObterTodos;
using DB1.Desafio.Application.Commands.Funcionario.Criar;
using DB1.Desafio.Application.Commands.Funcionario.Editar;
using DB1.Desafio.Application.Commands.Funcionario.Excluir;
using DB1.Desafio.Application.Commands.Funcionario.ObterComFiltro;
using DB1.Desafio.Application.Commands.Funcionario.ObterPorId;
using DB1.Desafio.Application.Commands.Funcionario.ObterTodos;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace DB1.Desafio.Application
{
    public static class ApplicationDependencyInjection
    {
        public static IServiceCollection AddApplicationDependencyInjection(this IServiceCollection services)
        {
            #region Eventos
            services
                .TryAddScoped<IMediatorHandler, MediatorHandler>();
            #endregion

            services

            #region Cargo
                 .AddScoped<ICriarCargoUseCase, CriarCargoUseCase>()
                 .AddScoped<IEditarCargoUseCase, EditarCargoUseCase>()
                 .AddScoped<IExcluirCargoUseCase, ExcluirCargoUseCase>()
                 .AddScoped<IObterPorIdCargoUseCase, ObterPorIdCargoUseCase>()
                 .AddScoped<IObterTodosCargoUseCase, ObterTodosCargoUseCase>()
            #endregion

            #region Funcionario
                .AddScoped<ICriarFuncionarioUseCase, CriarFuncionarioUseCase>()
                .AddScoped<IEditarFuncionarioUseCase, EditarFuncionarioUseCase>()
                .AddScoped<IObterPorIdFuncionarioUseCase, ObterPorIdFuncionarioUseCase>()
                .AddScoped<IObterTodosFuncionarioUseCase, ObterTodosFuncionarioUseCase>()
                .AddScoped<IObterComFiltroFuncionarioUseCase, ObterComFiltroFuncionarioUseCase>()
                .AddScoped<IExcluirFuncionarioUseCase, ExcluirFuncionarioUseCase>()
            #endregion

            #region Empresa
                .AddScoped<ICriarEmpresaUseCase, CriarEmpresaUseCase>()
                .AddScoped<IEditarEmpresaUseCase, EditarEmpresaUseCase>()
                .AddScoped<IObterPorIdEmpresaUseCase, ObterPorIdEmpresaUseCase>()
                .AddScoped<IObterTodosEmpresaUseCase, ObterTodosEmpresaUseCase>()
                .AddScoped<IObterComFiltroEmpresaUseCase, ObterComFiltroEmpresaUseCase>()
                .AddScoped<IExcluirEmpresaUseCase, ExcluirEmpresaUseCase>();
            #endregion

            return services;
        }
    }
}
