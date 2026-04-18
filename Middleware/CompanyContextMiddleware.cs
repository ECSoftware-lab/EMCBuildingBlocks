using EMC.BuildingBlocks.Cache;
using EMC.BuildingBlocks.Context;
using EMC.BuildingBlocks.Static.Extensions;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using System.Text;
using System.Text.Json;

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
                    await EscribirErrorAsync(context, 403, "Header X-CompanyId inválido");
                    return;
                }

                ctx.CompanyId = companyId;

                if (context.User.Identity?.IsAuthenticated == true)
                {
                    var companyIdFromToken = context.User.FindFirst("CompanyId")?.Value;
                    if (!Guid.TryParse(companyIdFromToken, out var companyIdFromTokenGuid))
                    {
                        await EscribirErrorAsync(context, 403, "Token no contiene CompanyId válido");
                        return;
                    }

                    if (companyId != companyIdFromTokenGuid)
                    {
                        await EscribirErrorAsync(context, 403, "Inconsistencia entre token y header");
                        return;
                    }

                    ctx.UserName = context.User.FindFirst(ClaimTypes.Email)?.Value;
                    ctx.UserId = context.User.GetUserId() ?? Guid.Empty;
                    ctx.Roles = context.User.FindAll(ClaimTypes.Role).Select(r => r.Value).ToList();
                    ctx.Claims = NormalizeClaims(context.User.Claims);

                    var nEmployeFromToken = context.User.FindFirst("NEmploye")?.Value;
                    if (!int.TryParse(nEmployeFromToken, out var nEmploye))
                    {
                        await EscribirErrorAsync(context, 403, "Token no contiene NEmploye válido");
                        return;
                    }

                    ctx.KindId = nEmploye;
                    ctx.KindId= context.User.GetEmployeeNumber() ?? 0;
                    ctx.ActiveSubsidiaryId = context.User.FindFirst("ActiveSubsidiaryId")?.Value switch
                    {
                        null => null,
                        var val when int.TryParse(val, out var subId) => subId,
                        _ => null
                    };
                    /*var claimValue = context.User.FindFirst("ActiveSubsidiaryId")?.Value;
if (claimValue == null)
    ctx.ActiveSubsidiaryId = null;
else if (int.TryParse(claimValue, out var subId))
    ctx.ActiveSubsidiaryId = subId;
else
    ctx.ActiveSubsidiaryId = null;
*/
                }

                var config = await configCacheService.GetCompanyConfigAsync(companyId);
                if (config == null)
                {
                    await EscribirErrorAsync(context, 503, "No se encontró configuración para la compañía en cache");
                    return;
                }

                ctx.Configurations = config;
                ctx.ConfigPersonType = await configCacheService.GetCompanyConfigPersonTypeAsync(companyId);
            }

            await _next(context);
        }

        // ── Helper privado ────────────────────────────────────────────────────────
        private static async Task EscribirErrorAsync(HttpContext context, int statusCode, string mensaje)
        {
            context.Response.StatusCode = statusCode;
            context.Response.ContentType = "application/json; charset=utf-8";

            var body = JsonSerializer.Serialize(new
            {
                status = statusCode,
                message = mensaje
            });

            await context.Response.WriteAsync(body, Encoding.UTF8);
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
