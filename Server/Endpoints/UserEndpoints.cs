using BusinessLogic.Services;

namespace Server.Endpoints;

public static class UserEndpoints
{
    public static IEndpointRouteBuilder MapUserEndpoints(this IEndpointRouteBuilder app)
    {
        var userApi = app.MapGroup("/api/users");

        userApi.MapGet("/", async (ClinicService clinicService) =>
            Results.Ok(await clinicService.GetAllUsersAsync()));

        return app;
    }
}