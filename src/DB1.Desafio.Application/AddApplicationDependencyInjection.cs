using DB1.Core.Mediator;
using DB1.Desafio.Application.Commands.Empresa.Criar;
using DB1.Desafio.Application.Commands.Empresa.Editar;
using DB1.Desafio.Application.Commands.Empresa.ObterPorId;
using DB1.Desafio.Application.Commands.Empresa.ObterTodos;
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
            #region Empresa
                .AddScoped<ICriarEmpresaUseCase, CriarEmpresaUseCase>()
                .AddScoped<IEditarEmpresaUseCase, EditarEmpresaUseCase>()
                .AddScoped<IObterPorIdEmpresaUseCase, ObterPorIdEmpresaUseCase>()
                .AddScoped<IObterTodosEmpresaUseCase, ObterTodosEmpresaUseCase>();
            #endregion

            return services;
        }
    }
}
