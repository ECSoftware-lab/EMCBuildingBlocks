using EMC.BuildingBlocks.Cache;
using EMC.BuildingBlocks.Context;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace EMC.BuildingBlocks.Middleware
{
    public class CompanyContextMiddleware
    {
        private readonly RequestDelegate _next;

        public CompanyContextMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, ICompanyExecutionContext companyContext, ICompanyConfigurationCacheService ConfiCahe)
        {
            if (companyContext is CompanyExecutionContext ctx)
            {
                var companyIdHeader = context.Request.Headers["X-CompanyId"].FirstOrDefault();

                if (!int.TryParse(companyIdHeader, out var companyId))
                {
                    context.Response.StatusCode = 403;
                    await context.Response.WriteAsync("User/Company invalid");
                    return;
                }

                ctx.CompanyId = companyId;
                ctx.Email = context.User.FindFirst(ClaimTypes.Email)?.Value;
                ctx.Roles = context.User.FindAll(ClaimTypes.Role).Select(r => r.Value).ToList();
                ctx.Claims = context.User.Claims.ToDictionary(c => c.Type, c => c.Value);

                var config = await ConfiCahe.GetCompanyConfigAsync(companyId);
                if (config == null)
                {
                    context.Response.StatusCode = 503;
                    await context.Response.WriteAsync("No se encontró configuración para la compañía en cache");
                    return;
                }

                ctx.Configurations = config;

            }
            Console.WriteLine($"Middleware Instance: {companyContext.GetHashCode()}");

            await _next(context);

        }
    }

}
