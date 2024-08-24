using DB1.Desafio.Domain.Repositories;
using DB1.Desafio.Infra.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace DB1.Desafio.Infra
{
    public static class InfraDependencyInjection
    {
        public static IServiceCollection AddInfraDependencyInjection(this IServiceCollection services)
        {
            services
                .AddScoped<IEmpresaRepository, EmpresaRepository>()
                .AddScoped<IFuncionarioRepository, FuncionarioRepository>()
                .AddScoped<ICargoRepository, CargoRepository>();

            return services;
        }
    }
}
