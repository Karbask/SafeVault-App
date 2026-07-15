namespace SafeVault.Api.Models;

public record RegisterUserRequest(
    string Username,
    string Email,
    string Password
);
