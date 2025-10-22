using EMC.BuildingBlocks.Application.AppBuilders;
using EMC.BuildingBlocks.Context;
using EMC.BuildingBlocks.Exceptions;
using EMC.BuildingBlocks.Interfaces;
using EMC.BuildingBlocks.Middleware;
using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Reflection;

namespace EMC.BuildingBlocks.DependencyInjection
{
    public static class SharedServiceContainer
    {
        public static IServiceCollection AddSharedServices<TContext>(this IServiceCollection services, IConfiguration config
            , string fileName, bool relationalBD = true,string strConextion = "DefaultConnection") where TContext : DbContext
        {
            //TODO resolver
            //Console.WriteLine("Fuentes de configuración cargadas:");
            //foreach (var source in config.AsEnumerable())
            //{
            //    Console.WriteLine($"Clave: {source.Key}, Valor: {source.Value}");
            //}

            services.AddScoped<ICompanyExecutionContext, CompanyExecutionContext>();
            services.AddScoped<IAddressAppBuilder, AddressAppBuilder>();
            
            if (relationalBD)
            {
                var connectionString = config.GetConnectionString(strConextion);
               
                if (string.IsNullOrWhiteSpace(connectionString))
                {
                    throw new InvalidOperationException("No se encontró la cadena de conexión 'DefaultConnection'. Verificá el appsettings.json del proyecto ProductApi.Presentation");
                }
                services.AddDbContextFactory<TContext>(options =>
                {
                    options.UseNpgsql(connectionString);
                    options.EnableSensitiveDataLogging(false);
                    options.LogTo(Console.WriteLine, LogLevel.Information);
                }); 


                AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
            }
           


            services.AddRedisInyection(config);

            return services;
        }

        public static IApplicationBuilder UseSharedPolicies(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionMiddleware>();
            app.UseMiddleware<ErrorHandlingMiddleware>();
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
 