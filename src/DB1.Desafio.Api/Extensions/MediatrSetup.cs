namespace DB1.Desafio.Api.Extensions
{
    public static class MediatrSetup
    {
        public static void AddMediatRApi(this IServiceCollection services)
        {
            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssemblies(
                    typeof(Application.ApplicationDependencyInjection).Assembly);
            });
        }
    }
}
