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


        public async Task Invoke(HttpContext context, ICompanyExecutionContext companyContext, ICompanyConfigurationCacheService configCacheService)
        {
            if (companyContext is CompanyExecutionContext ctx)
            {
                var companyIdHeader = context.Request.Headers["X-CompanyId"].FirstOrDefault();

                if (!Guid.TryParse(companyIdHeader, out var companyId))
                {
                    context.Response.StatusCode = 403;
                    await context.Response.WriteAsync("Header X-CompanyId inválido");
                    return;
                }

                ctx.CompanyId = companyId;

                if (context.User.Identity?.IsAuthenticated == true)
                {
                    // ✅ Solo si está autenticado, validar el token
                    var companyIdFromToken = context.User.FindFirst("CompanyId")?.Value;

                    if (!Guid.TryParse(companyIdFromToken, out var companyIdFromTokenGuid))
                    {
                        context.Response.StatusCode = 403;
                        await context.Response.WriteAsync("Token no contiene CompanyId válido");
                        return;
                    }

                    if (companyId != companyIdFromTokenGuid)
                    {
                        context.Response.StatusCode = 403;
                        await context.Response.WriteAsync("Inconsistencia entre token y header");
                        return;
                    }

                    ctx.UserName = context.User.FindFirst(ClaimTypes.Email)?.Value;
                    ctx.UserId = context.User.GetUserId() ?? Guid.Empty;
                    ctx.Roles = context.User.FindAll(ClaimTypes.Role).Select(r => r.Value).ToList();
                    //ctx.Claims = context.User.Claims.GroupBy(c => c.Type).ToDictionary(g => g.Key, g => g.First().Value);

                    ctx.Claims = NormalizeClaims(context.User.Claims);
                    var NEmployeFromToken = context.User.FindFirst("NEmploye")?.Value;

                    if (!int.TryParse(NEmployeFromToken, out var NEmploye ))
                    {
                        context.Response.StatusCode = 403;
                        await context.Response.WriteAsync("Token no contiene NEmploye válido");
                        return;
                    }
                    ctx.KindId = NEmploye;

                }

                // ✅ Siempre buscar config, incluso si es request anónima (si querés evitarlo, podés poner otra condición)
                var config = await configCacheService.GetCompanyConfigAsync(companyId);
                if (config == null)
                {
                    context.Response.StatusCode = 503;
                    await context.Response.WriteAsync("No se encontró configuración para la compañía en cache");
                    return;
                }
                ctx.Configurations = config;
                var configPersonType = await configCacheService.GetCompanyConfigPersonTypeAsync(companyId);
                ctx.ConfigPersonType = configPersonType;
            }

            await _next(context);
        }

        private Dictionary<string, string> NormalizeClaims(IEnumerable<System.Security.Claims.Claim> claims)
        {
            var dict = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

            foreach (var claim in claims)
            {
                // Si la clave es un URI, me quedo con el último segmento
                var key = claim.Type.Contains('/')
                    ? claim.Type.Split('/').Last()
                    : claim.Type;

                dict[key] = claim.Value;
            }

            return dict;
        }


        public static Guid GetNameIdentifier(IDictionary<string, string> claims)
        {
            var kv = claims.FirstOrDefault(c => c.Key.EndsWith("nameidentifier", StringComparison.OrdinalIgnoreCase));
            return Guid.TryParse(kv.Value, out var id) ? id : Guid.Empty;
        }

    }

}
