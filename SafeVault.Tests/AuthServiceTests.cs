using NUnit.Framework;
using SafeVault.Api.Data;
using SafeVault.Api.Security;

namespace SafeVault.Tests;

[TestFixture]
public class AuthServiceTests
{
    [Test]
    public void Login_WithValidAdminCredentials_ReturnsUser()
    {
        var repository = new InMemoryUserRepository();
        var authService = new AuthService(repository);

        var user = authService.Login("admin", "Admin123!");

        Assert.That(user, Is.Not.Null);
        Assert.That(user!.Role, Is.EqualTo("admin"));
    }

    [Test]
    public void Login_WithInvalidPassword_ReturnsNull()
    {
        var repository = new InMemoryUserRepository();
        var authService = new AuthService(repository);

        var user = authService.Login("admin", "PasswordIncorrecta123!");

        Assert.That(user, Is.Null);
    }

    [Test]
    public void GetUserByToken_WithValidToken_ReturnsUser()
    {
        var repository = new InMemoryUserRepository();
        var authService = new AuthService(repository);
        var user = authService.Login("usuario", "User1234!");
        string token = authService.CreateToken(user!);

        var result = authService.GetUserByToken($"Bearer {token}");

        Assert.That(result, Is.Not.Null);
        Assert.That(result!.Username, Is.EqualTo("usuario"));
    }
}
