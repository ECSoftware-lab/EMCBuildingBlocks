using EMC.BuildingBlocks.Context;
using EMC.BuildingBlocks.Middleware;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EMC.BuildingBlocks.DependencyInjection
{
    public static class SharedServiceContainer
    {
        public static IServiceCollection AddSharedServices<TContext>(this IServiceCollection services, IConfiguration config, string fileName) where TContext : DbContext
        {
            services.AddScoped<ICompanyExecutionContext, CompanyExecutionContext>();


            var connectionString = config.GetConnectionString("DefaultConnection");

            if (string.IsNullOrWhiteSpace(connectionString))
            {
                throw new InvalidOperationException("No se encontró la cadena de conexión 'DefaultConnection'. Verificá el appsettings.json del proyecto ProductApi.Presentation");
            }

            services.AddDbContext<TContext>(options =>
                options.UseNpgsql(connectionString)
            );

            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

            // services.AddDbContextFactory<TContext>(options =>
            //options.UseNpgsql(config.GetConnectionString("DefaultConnection")), ServiceLifetime.Scoped
            // );


            //Log.Logger = new LoggerConfiguration()
            //    .MinimumLevel.Information()
            //    .WriteTo.Debug()
            //    .WriteTo.Console()
            //    .WriteTo.File(path: $"{fileName}-.text",
            //    restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Information,
            //    outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.ff zzz} [{Level:u3}] {message:lj}{NewLine}{Exception}",
            //    rollingInterval: RollingInterval.Day)
            //    .CreateLogger();

            services.AddJWTAuthenticationScheme(config);
            services.AddRedisInyection(config);

            return services;
        }

        public static IApplicationBuilder UseSharedPolicies(this IApplicationBuilder app)
        {

            app.UseMiddleware<CompanyContextMiddleware>();

            app.UseMiddleware<ExceptionMiddleware>();
            return app;
        }
    }
}
