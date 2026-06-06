using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EMC.BuildingBlocks.DependencyInjection
{
    public static class CorsExtensions
    {
        public static IServiceCollection AddCustomCors(this IServiceCollection services, IConfiguration configuration)
        {
            var origins = configuration.GetSection("Cors:AllowedOrigins").Get<string[]>() ?? [];

            services.AddCors(options =>
            {
                options.AddPolicy("AllowFrontends", policy =>
                {
                    policy.WithOrigins(origins)
                          .SetIsOriginAllowed(origin =>
                              origin.EndsWith("turneroweb.ar", StringComparison.OrdinalIgnoreCase) ||
                              origin.Contains("localhost", StringComparison.OrdinalIgnoreCase))
                          .AllowAnyMethod()
                          .AllowAnyHeader()
                          .AllowCredentials();
                });
            });

            return services;
        }
    }
}
