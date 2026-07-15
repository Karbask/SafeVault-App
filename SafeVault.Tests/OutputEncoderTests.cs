using NUnit.Framework;
using SafeVault.Api.Security;

namespace SafeVault.Tests;

[TestFixture]
public class OutputEncoderTests
{
    [Test]
    public void EncodeForHtml_WithScript_EncodesTags()
    {
        string result = OutputEncoder.EncodeForHtml("<script>alert('XSS')</script>");

        Assert.That(result, Does.Contain("&lt;script&gt;"));
        Assert.That(result, Does.Contain("&lt;/script&gt;"));
    }

    [Test]
    public void EncodeForHtml_WithNormalText_KeepsTextReadable()
    {
        string result = OutputEncoder.EncodeForHtml("usuario_seguro");

        Assert.That(result, Is.EqualTo("usuario_seguro"));
    }
}
