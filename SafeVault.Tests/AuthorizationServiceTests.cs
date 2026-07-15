using NUnit.Framework;
using SafeVault.Api.Models;
using SafeVault.Api.Security;

namespace SafeVault.Tests;

[TestFixture]
public class AuthorizationServiceTests
{
    [Test]
    public void CanAccessAdminPanel_WithAdminUser_ReturnsTrue()
    {
        var user = new User { Username = "admin", Role = Roles.Admin };

        bool result = AuthorizationService.CanAccessAdminPanel(user);

        Assert.That(result, Is.True);
    }

    [Test]
    public void CanAccessAdminPanel_WithNormalUser_ReturnsFalse()
    {
        var user = new User { Username = "usuario", Role = Roles.User };

        bool result = AuthorizationService.CanAccessAdminPanel(user);

        Assert.That(result, Is.False);
    }

    [Test]
    public void CanAccessAdminPanel_WithNullUser_ReturnsFalse()
    {
        bool result = AuthorizationService.CanAccessAdminPanel(null);

        Assert.That(result, Is.False);
    }
}
