using EMC.BuildingBlocks.Cache;
using EMC.BuildingBlocks.Context;
using EMC.BuildingBlocks.Static.Extensions;
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
            Console.WriteLine("TOKEN: " + context.Request.Headers["Authorization"]);


            if (companyContext is CompanyExecutionContext ctx)
            {
                var companyIdHeader = context.Request.Headers["X-CompanyId"].FirstOrDefault();

                if (!Guid.TryParse(companyIdHeader, out var companyId))
                {
                    context.Response.StatusCode = 403;
                    await context.Response.WriteAsync("User/Company invalid");
                    return;
                }
                var tClaims = context.User.Claims;
                ctx.CompanyId = companyId;
                ctx.UserName = context.User.FindFirst(ClaimTypes.Email)?.Value;
                ctx.Roles = context.User.FindAll(ClaimTypes.Role).Select(r => r.Value).ToList();
                ctx.Claims = context.User.Claims
                            .GroupBy(c => c.Type)
                             .ToDictionary(g => g.Key, g => g.First().Value);

                var user = context.User;
                if (user != null && user.Identity.IsAuthenticated)
                {
                    var userName = context.User.GetUserName();
                    var userGuid = context.User.GetUserId();
                    ctx.UserId = userGuid ?? Guid.Empty; 
                }

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
