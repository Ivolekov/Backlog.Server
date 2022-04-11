namespace Backlog.Server.Infrastructure
{
    using System.Linq;
    using System.Security.Claims;
    public static class IdentityExtensions
    {
        public static string GetId(this ClaimsPrincipal user) 
            => user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
        public static string GetUsername(this ClaimsPrincipal user)
            => user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;
    }
}
