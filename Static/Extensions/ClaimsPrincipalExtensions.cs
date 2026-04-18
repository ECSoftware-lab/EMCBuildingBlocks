using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace EMC.BuildingBlocks.Static.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static string? GetUserName(this ClaimsPrincipal user) =>
            user.FindFirst(ClaimTypes.Name)?.Value;

        public static Guid? GetUserId(this ClaimsPrincipal user)
        {
            var value = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return Guid.TryParse(value, out var id) ? id : (Guid?)null;
        }

        public static int?  GetEmployeeNumber(this ClaimsPrincipal user)
        {
            var nEmployeFromToken = user.FindFirst("NEmploye")?.Value;
           var n= int.TryParse(nEmployeFromToken, out var nEmploye)? nEmploye : (int?)null;
             return n;
        }
        public static IEnumerable<string> GetRoles(this ClaimsPrincipal user) =>
            user.FindAll(ClaimTypes.Role).Select(r => r.Value);
    }

}
