using SafeVault.Api.Data;
using SafeVault.Api.Models;
using SafeVault.Api.Security;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<IUserRepository, InMemoryUserRepository>();
builder.Services.AddSingleton<AuthService>();

var app = builder.Build();

app.MapGet("/health", () => Results.Ok(new
{
    status = "SafeVault API funcionando",
    timestamp = DateTimeOffset.UtcNow
}));

app.MapPost("/api/users/register", (RegisterUserRequest request, IUserRepository userRepository) =>
{
    try
    {
        string username = InputValidator.ValidateUsername(request.Username);
        string email = InputValidator.ValidateEmail(request.Email);
        string passwordHash = PasswordService.HashPassword(request.Password);

        var user = new User
        {
            Username = username,
            Email = email,
            PasswordHash = passwordHash,
            Role = Roles.User // Por seguridad, el registro público siempre crea usuarios normales.
        };

        if (!userRepository.Add(user))
            return Results.Conflict(new { error = "Ya existe un usuario con ese nombre." });

        return Results.Created($"/api/users/{username}", new
        {
            user.UserId,
            Username = OutputEncoder.EncodeForHtml(user.Username),
            Email = OutputEncoder.EncodeForHtml(user.Email),
            user.Role
        });
    }
    catch (ArgumentException ex)
    {
        return Results.BadRequest(new { error = ex.Message });
    }
});

app.MapPost("/api/auth/login", (LoginRequest request, AuthService authService) =>
{
    User? user = authService.Login(request.Username, request.Password);

    if (user == null)
        return Results.Unauthorized();

    string token = authService.CreateToken(user);

    return Results.Ok(new
    {
        token,
        Username = OutputEncoder.EncodeForHtml(user.Username),
        user.Role
    });
});

app.MapGet("/api/users/search", (string username, IUserRepository userRepository) =>
{
    try
    {
        string safeUsername = InputValidator.ValidateUsername(username);
        User? user = userRepository.GetByUsername(safeUsername);

        if (user == null)
            return Results.NotFound(new { error = "Usuario no encontrado." });

        return Results.Ok(new
        {
            user.UserId,
            Username = OutputEncoder.EncodeForHtml(user.Username),
            Email = OutputEncoder.EncodeForHtml(user.Email),
            user.Role
        });
    }
    catch (ArgumentException ex)
    {
        return Results.BadRequest(new { error = ex.Message });
    }
});

app.MapGet("/api/users", (HttpRequest httpRequest, AuthService authService, IUserRepository userRepository) =>
{
    User? currentUser = authService.GetUserByToken(httpRequest.Headers.Authorization.ToString());

    if (!AuthorizationService.IsAuthenticated(currentUser))
        return Results.Unauthorized();

    if (!AuthorizationService.CanAccessAdminPanel(currentUser))
        return Results.StatusCode(StatusCodes.Status403Forbidden);

    var users = userRepository.GetAll().Select(user => new
    {
        user.UserId,
        Username = OutputEncoder.EncodeForHtml(user.Username),
        Email = OutputEncoder.EncodeForHtml(user.Email),
        user.Role
    });

    return Results.Ok(users);
});

app.MapGet("/api/admin/dashboard", (HttpRequest httpRequest, AuthService authService) =>
{
    User? currentUser = authService.GetUserByToken(httpRequest.Headers.Authorization.ToString());

    if (!AuthorizationService.IsAuthenticated(currentUser))
        return Results.Unauthorized();

    if (!AuthorizationService.CanAccessAdminPanel(currentUser))
        return Results.StatusCode(StatusCodes.Status403Forbidden);

    return Results.Ok(new
    {
        message = "Acceso permitido al panel de administración.",
        Username = OutputEncoder.EncodeForHtml(currentUser!.Username),
        currentUser.Role
    });
});

app.MapPost("/api/security/echo-safe", (EchoRequest request) =>
{
    string safeText = OutputEncoder.EncodeForHtml(request.Text);

    return Results.Ok(new
    {
        originalValue = request.Text,
        safeHtml = $"<p>{safeText}</p>",
        note = "safeHtml está codificado para que el navegador no ejecute scripts."
    });
});

app.Run();
