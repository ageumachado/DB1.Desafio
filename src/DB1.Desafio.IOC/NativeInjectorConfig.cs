using DB1.Desafio.Application;
using DB1.Desafio.Infra;
using DB1.Desafio.Infra.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DB1.Desafio.IOC
{
    public static class NativeInjectorConfig
    {
        public static void RegisterServices(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddDbContext(configuration)
                .AddInfraDependencyInjection()
                .AddApplicationDependencyInjection();
        }
    }
}
