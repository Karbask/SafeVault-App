namespace SafeVault.Api.Models;

public record LoginRequest(
    string Username,
    string Password
);
