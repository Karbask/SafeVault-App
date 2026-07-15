namespace SafeVault.Api.Data;

public static class SafeSqlExamples
{
    // Este archivo deja documentado cómo se consultaría una base de datos real.
    // La API de demo usa memoria para que se pueda ejecutar fácil en VS Code,
    // pero en una base de datos real los parámetros evitan SQL Injection.

    public const string UnsafeQueryExample =
        "SELECT UserID, Username, Email FROM Users WHERE Username = '" + " + username + " + "'";

    public const string SafeParameterizedQuery = @"
        SELECT UserID, Username, Email, PasswordHash, Role
        FROM Users
        WHERE Username = @Username;
    ";
}
