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
    }

}
