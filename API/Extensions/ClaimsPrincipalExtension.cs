using System.Security.Claims;

namespace API.Extensions;

public static class ClaimsPrincipalExtension
{
    public static string RetriveEmail(this ClaimsPrincipal principal)
    {
        return principal.FindFirstValue(ClaimTypes.Email);
    }

}
