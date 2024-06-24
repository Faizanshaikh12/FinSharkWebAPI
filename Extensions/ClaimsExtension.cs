using System.Security.Claims;

namespace FinSharkWebAPI.Extensions;

public static class ClaimsExtension
{
    public static string GetUsername(this ClaimsPrincipal user)
    {
        var claims = user.Claims
            .SingleOrDefault(x => x.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/givenname");

        if (claims == null)
            throw new InvalidOperationException("Claim not found.");

        return claims.Value;
    }
}