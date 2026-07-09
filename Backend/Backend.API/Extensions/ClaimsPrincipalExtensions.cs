using System.Security.Claims;


namespace Backend.API.Extensions;

public static class ClaimsPrincipalExtensions
{
    public static int? GetUserId(this ClaimsPrincipal user)
    {
        string? value = user
            .FindFirst(ClaimTypes.NameIdentifier)?
            .Value;

        if (!int.TryParse(value, out int userId))
            return null;

        return userId;
    }
}