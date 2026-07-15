using SafeVault.Api.Data;
using SafeVault.Api.Models;

namespace SafeVault.Api.Security;

public class AuthService
{
    private readonly IUserRepository _userRepository;
    private readonly Dictionary<string, string> _tokensByUsername = new(StringComparer.OrdinalIgnoreCase);
    private readonly object _lock = new();

    public AuthService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public User? Login(string username, string password)
    {
        string safeUsername;

        try
        {
            safeUsername = InputValidator.ValidateUsername(username);
        }
        catch
        {
            return null;
        }

        User? user = _userRepository.GetByUsername(safeUsername);

        if (user == null)
            return null;

        bool validPassword = PasswordService.VerifyPassword(password, user.PasswordHash);

        return validPassword ? user : null;
    }

    public string CreateToken(User user)
    {
        string token = Convert.ToBase64String(Guid.NewGuid().ToByteArray())
            .Replace("+", "")
            .Replace("/", "")
            .Replace("=", "");

        lock (_lock)
        {
            _tokensByUsername[token] = user.Username;
        }

        return token;
    }

    public User? GetUserByToken(string? authorizationHeader)
    {
        if (string.IsNullOrWhiteSpace(authorizationHeader))
            return null;

        const string bearerPrefix = "Bearer ";
        if (!authorizationHeader.StartsWith(bearerPrefix, StringComparison.OrdinalIgnoreCase))
            return null;

        string token = authorizationHeader[bearerPrefix.Length..].Trim();

        lock (_lock)
        {
            if (!_tokensByUsername.TryGetValue(token, out string? username))
                return null;

            return _userRepository.GetByUsername(username);
        }
    }
}
