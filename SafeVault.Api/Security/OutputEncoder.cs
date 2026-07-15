using System.Net;

namespace SafeVault.Api.Security;

public static class OutputEncoder
{
    public static string EncodeForHtml(string? value)
    {
        if (string.IsNullOrEmpty(value))
            return string.Empty;

        return WebUtility.HtmlEncode(value);
    }
}
