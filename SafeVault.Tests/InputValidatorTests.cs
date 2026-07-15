using NUnit.Framework;
using SafeVault.Api.Security;

namespace SafeVault.Tests;

[TestFixture]
public class InputValidatorTests
{
    [Test]
    public void ValidateUsername_WithValidUsername_ReturnsCleanValue()
    {
        string result = InputValidator.ValidateUsername("usuario_123");

        Assert.That(result, Is.EqualTo("usuario_123"));
    }

    [Test]
    public void ValidateUsername_WithSqlInjection_ThrowsException()
    {
        Assert.Throws<ArgumentException>(() =>
            InputValidator.ValidateUsername("admin' OR '1'='1"));
    }

    [Test]
    public void ValidateUsername_WithXssScript_ThrowsException()
    {
        Assert.Throws<ArgumentException>(() =>
            InputValidator.ValidateUsername("<script>alert('XSS')</script>"));
    }

    [Test]
    public void ValidateEmail_WithValidEmail_ReturnsCleanValue()
    {
        string result = InputValidator.ValidateEmail("usuario@test.com");

        Assert.That(result, Is.EqualTo("usuario@test.com"));
    }

    [Test]
    public void ValidateEmail_WithInvalidEmail_ThrowsException()
    {
        Assert.Throws<ArgumentException>(() =>
            InputValidator.ValidateEmail("correo-no-valido"));
    }

    [Test]
    public void ValidatePassword_WithWeakPassword_ThrowsException()
    {
        Assert.Throws<ArgumentException>(() =>
            InputValidator.ValidatePassword("123"));
    }
}
