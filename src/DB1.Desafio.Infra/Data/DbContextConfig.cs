using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DB1.Desafio.Infra.Data
{
    public static class DbContextConfig
    {
        public static IServiceCollection AddDbContext(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDbContext<Db1DataContext>(m =>
                    m.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                        e => e.MigrationsAssembly(typeof(Db1DataContext).Assembly.FullName))
#if DEBUG
                        .EnableSensitiveDataLogging()
                        .EnableDetailedErrors()
#endif
                        );

            return services;
        }
    }
}
