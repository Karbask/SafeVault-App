using SafeVault.Api.Models;
using SafeVault.Api.Security;

namespace SafeVault.Api.Data;

public class InMemoryUserRepository : IUserRepository
{
    private readonly Dictionary<string, User> _users = new(StringComparer.OrdinalIgnoreCase);
    private readonly object _lock = new();

    public InMemoryUserRepository()
    {
        // Usuarios de demo para probar rápido desde request.http.
        Add(new User
        {
            Username = "admin",
            Email = "admin@safevault.local",
            PasswordHash = PasswordService.HashPassword("Admin123!"),
            Role = Roles.Admin
        });

        Add(new User
        {
            Username = "usuario",
            Email = "usuario@safevault.local",
            PasswordHash = PasswordService.HashPassword("User1234!"),
            Role = Roles.User
        });
    }

    public bool Add(User user)
    {
        lock (_lock)
        {
            if (_users.ContainsKey(user.Username))
                return false;

            _users[user.Username] = user;
            return true;
        }
    }

    public User? GetByUsername(string username)
    {
        lock (_lock)
        {
            return _users.TryGetValue(username, out User? user) ? user : null;
        }
    }

    public IReadOnlyCollection<User> GetAll()
    {
        lock (_lock)
        {
            return _users.Values.ToList().AsReadOnly();
        }
    }
}
