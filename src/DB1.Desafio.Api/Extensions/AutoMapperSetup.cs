namespace DB1.Desafio.Api.Extensions
{
    public static class AutoMapperSetup
    {
        public static IServiceCollection AddAutoMapperSetup(this IServiceCollection services)
        {
            services.AddAutoMapper((cfg) =>
            {
                //fg.ShouldMapProperty = p => !p.SetMethod?.IsPrivate ?? true;
                //cfg.ShouldUseConstructor = ci => !ci.IsPrivate;

            },
            typeof(Application.ApplicationDependencyInjection).Assembly/* Assembly.Load("Acsm.SPX.Application")*/);
            return services;
        }
    }
}
