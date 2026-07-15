namespace SafeVault.Api.Models;

public static class Roles
{
    public const string Admin = "admin";
    public const string User = "user";

    public static bool IsValid(string role)
    {
        return role.Equals(Admin, StringComparison.OrdinalIgnoreCase)
            || role.Equals(User, StringComparison.OrdinalIgnoreCase);
    }
}
