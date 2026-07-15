using SafeVault.Api.Models;

namespace SafeVault.Api.Security;

public static class AuthorizationService
{
    public static bool IsAuthenticated(User? user)
    {
        return user != null;
    }

    public static bool IsAdmin(User? user)
    {
        if (user == null)
            return false;

        return user.Role.Equals(Roles.Admin, StringComparison.OrdinalIgnoreCase);
    }

    public static bool CanAccessAdminPanel(User? user)
    {
        return IsAuthenticated(user) && IsAdmin(user);
    }
}
