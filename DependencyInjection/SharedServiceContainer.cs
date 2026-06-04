using EMC.BuildingBlocks.Application.AppBuilders;
using EMC.BuildingBlocks.Context;
using EMC.BuildingBlocks.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace EMC.BuildingBlocks.DependencyInjection
{
    public static class SharedServiceContainer
    {

        public static IServiceCollection AddSharedServices<TContext>(this IServiceCollection services, IConfiguration config
            , string fileName, bool relationalBD = true, string strConextion = "DefaultConnection") where TContext : DbContext
        {

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

            //TODO resolver
            Console.WriteLine("Fuentes de configuración cargadas:");
            foreach (var source in config.AsEnumerable())
            {
                Console.WriteLine($"Clave: {source.Key}, Valor: {source.Value}");
            }


            services.AddRedisInyection(config);

            return services;
        }



    }
}
