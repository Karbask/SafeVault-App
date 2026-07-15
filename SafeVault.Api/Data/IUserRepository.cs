using SafeVault.Api.Models;

namespace SafeVault.Api.Data;

public interface IUserRepository
{
    bool Add(User user);
    User? GetByUsername(string username);
    IReadOnlyCollection<User> GetAll();
}
