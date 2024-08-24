using System.Text.Json.Serialization;
using System.Text.Json;

namespace DB1.Desafio.Api.Extensions
{
    public static class ApiSetup
    {
        public static void AddApiConfig(this IServiceCollection services)
        {
            services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
                options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
                options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
                options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                options.JsonSerializerOptions.IgnoreReadOnlyProperties = true;
            });

            services.AddMemoryCache();
            services.AddRouting(options => options.LowercaseUrls = true);
        }
    }
}
