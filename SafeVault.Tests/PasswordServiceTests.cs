using NUnit.Framework;
using SafeVault.Api.Security;

namespace SafeVault.Tests;

[TestFixture]
public class PasswordServiceTests
{
    [Test]
    public void HashPassword_DoesNotReturnPlainTextPassword()
    {
        string password = "Password123!";
        string hash = PasswordService.HashPassword(password);

        Assert.That(hash, Is.Not.EqualTo(password));
    }

    [Test]
    public void VerifyPassword_WithCorrectPassword_ReturnsTrue()
    {
        string password = "Password123!";
        string hash = PasswordService.HashPassword(password);

        bool result = PasswordService.VerifyPassword(password, hash);

        Assert.That(result, Is.True);
    }

    [Test]
    public void VerifyPassword_WithWrongPassword_ReturnsFalse()
    {
        string hash = PasswordService.HashPassword("Password123!");

        bool result = PasswordService.VerifyPassword("WrongPassword123!", hash);

        Assert.That(result, Is.False);
    }
}
