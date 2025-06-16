using BusinessLogic.Services;

namespace Server.Endpoints;

public record AuthRequest(string Username, string Password);

public static class AuthEndpoints
{
    public static IEndpointRouteBuilder MapAuthEndpoints(this IEndpointRouteBuilder app)
    {
        var authApi = app.MapGroup("/api/auth");

        authApi.MapPost("/register", async (AuthRequest request, ClinicService clinicService) =>
        {
            var user = await clinicService.RegisterUserAsync(request.Username, request.Password);
            return user != null ? Results.Ok(user) : Results.BadRequest("Username already exists.");
        });

        authApi.MapPost("/login", async (AuthRequest request, ClinicService clinicService) =>
        {
            var user = await clinicService.LoginAsync(request.Username, request.Password);
            return user != null ? Results.Ok(user) : Results.Unauthorized();
        });

        return app;
    }
}