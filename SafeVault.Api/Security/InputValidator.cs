using System.Net.Mail;
using System.Text.RegularExpressions;

namespace SafeVault.Api.Security;

public static class InputValidator
{
    private static readonly Regex UsernameRegex = new("^[a-zA-Z0-9_-]{3,50}$", RegexOptions.Compiled);

    public static string ValidateUsername(string username)
    {
        if (string.IsNullOrWhiteSpace(username))
            throw new ArgumentException("El nombre de usuario no puede estar vacío.");

        username = username.Trim();

        if (!UsernameRegex.IsMatch(username))
            throw new ArgumentException("El nombre de usuario solo puede contener letras, números, guion bajo o guion medio, y debe tener entre 3 y 50 caracteres.");

        return username;
    }

    public static string ValidateEmail(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
            throw new ArgumentException("El correo electrónico no puede estar vacío.");

        email = email.Trim();

        if (email.Length > 100)
            throw new ArgumentException("El correo electrónico no puede superar los 100 caracteres.");

        try
        {
            var mailAddress = new MailAddress(email);

            if (!mailAddress.Address.Equals(email, StringComparison.OrdinalIgnoreCase))
                throw new ArgumentException("El formato del correo electrónico no es válido.");

            return email;
        }
        catch
        {
            throw new ArgumentException("El formato del correo electrónico no es válido.");
        }
    }

    public static string ValidatePassword(string password)
    {
        if (string.IsNullOrWhiteSpace(password))
            throw new ArgumentException("La contraseña no puede estar vacía.");

        if (password.Length < 8)
            throw new ArgumentException("La contraseña debe tener al menos 8 caracteres.");

        if (!password.Any(char.IsUpper) || !password.Any(char.IsLower) || !password.Any(char.IsDigit))
            throw new ArgumentException("La contraseña debe incluir mayúsculas, minúsculas y números.");

        return password;
    }
}
