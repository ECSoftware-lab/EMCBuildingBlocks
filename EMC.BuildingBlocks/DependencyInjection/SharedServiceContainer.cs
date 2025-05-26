using EMC.BuildingBlocks.Application.AppBuilders;
using EMC.BuildingBlocks.Context;
using EMC.BuildingBlocks.Interfaces;
using EMC.BuildingBlocks.Middleware;
using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace EMC.BuildingBlocks.DependencyInjection
{
    public static class SharedServiceContainer
    {
        public static IServiceCollection AddSharedServices<TContext>(this IServiceCollection services, IConfiguration config
            , string fileName, bool relationalBD = true) where TContext : DbContext
        {
            services.AddScoped<ICompanyExecutionContext, CompanyExecutionContext>();
            services.AddScoped<IAddressAppBuilder, AddressAppBuilder>();
            
            if (relationalBD)
            {
                var connectionString = config.GetConnectionString("DefaultConnection");

                if (string.IsNullOrWhiteSpace(connectionString))
                {
                    throw new InvalidOperationException("No se encontró la cadena de conexión 'DefaultConnection'. Verificá el appsettings.json del proyecto ProductApi.Presentation");
                }

                services.AddDbContext<TContext>(options =>
                    options.UseNpgsql(connectionString)
                );

                AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
            }


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

            //var autenthentication = config.GetValue<string>("Authentication:jwtKey");
            //if (!string.IsNullOrWhiteSpace(autenthentication))
            //    services.AddJWTAuthenticationScheme(config);

            services.AddRedisInyection(config);

            return services;
        }

        public static IApplicationBuilder UseSharedPolicies(this IApplicationBuilder app)
        {

           // app.UseMiddleware<CompanyContextMiddleware>();

            app.UseMiddleware<ExceptionMiddleware>();
            return app;
        }

        public static IServiceCollection AddValidatorsFromAssembly(this IServiceCollection services, Assembly assembly)
        {
            var validatorTypes = assembly
                .GetTypes()
                .Where(t => !t.IsAbstract && !t.IsInterface)
                .SelectMany(t =>
                    t.GetInterfaces()
                        .Where(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IValidator<>))
                        .Select(i => new { ValidatorType = t, InterfaceType = i }))
                .ToList();

            foreach (var v in validatorTypes)
            {
                services.AddScoped(v.InterfaceType, v.ValidatorType);
            }

            return services;
        }
    }
}
