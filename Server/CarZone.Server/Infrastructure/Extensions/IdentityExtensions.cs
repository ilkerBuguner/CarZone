namespace CarZone.Server.Infrastructure.Extensions
{
    using System.Linq;
    using System.Security.Claims;

    public static class IdentityExtensions
    {
        public static string GetId(this ClaimsPrincipal user)
        {
            return user
                .Claims
                .FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)
                ?.Value;
        }
    }
}
