using System.Security.Claims;

public static class ClaimsPrincipalExtensions
{
    public static string Id(this ClaimsPrincipal principal)
    {
        if (principal == null)
        {
            throw new ArgumentNullException(nameof(principal));
        }

        var id = principal.FindFirstValue(ClaimTypes.NameIdentifier);

        if (id == null)
        {
            throw new ArgumentNullException(nameof(id));
        }

        return id;
    }

    public static string Email(this ClaimsPrincipal principal)
    {
        if (principal == null)
        {
            throw new ArgumentNullException(nameof(principal));
        }

        var email = principal.FindFirstValue(ClaimTypes.Email);

        if (email == null)
        {
            throw new ArgumentNullException(nameof(email));
        }

        return email;
    }
}