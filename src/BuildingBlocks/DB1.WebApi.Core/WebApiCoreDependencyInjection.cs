using Microsoft.Extensions.DependencyInjection;

namespace DB1.WebApi.Core
{
    public static class WebApiCoreDependencyInjection
    {
        public static IServiceCollection AddWebApiCoreDIConfiguration(this IServiceCollection services)
        {
            services.AddHttpContextAccessor();
            return services;
        }
    }
}
