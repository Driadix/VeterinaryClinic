using BusinessLogic.Services;
using Core.Models;

namespace Server.Endpoints;

public static class ClientEndpoints
{
    public static IEndpointRouteBuilder MapClientEndpoints(this IEndpointRouteBuilder app)
    {
        var clientApi = app.MapGroup("/api/clients");

        clientApi.MapGet("/", async (ClinicService clinicService) =>
            Results.Ok(await clinicService.GetAllClientsAsync()));

        clientApi.MapGet("/{id}", async (int id, ClinicService clinicService) =>
            await clinicService.GetClientByIdAsync(id) is Client client ? Results.Ok(client) : Results.NotFound());

        clientApi.MapPost("/", async (Client newClient, ClinicService clinicService) =>
        {
            await clinicService.AddClientAsync(newClient);
            return Results.Created($"/api/clients/{newClient.Id}", newClient);
        });

        clientApi.MapPut("/{id}", async (int id, Client updatedClient, ClinicService clinicService) =>
        {
            if (id != updatedClient.Id) return Results.BadRequest("ID mismatch.");
            var success = await clinicService.UpdateClientAsync(updatedClient);
            return success ? Results.NoContent() : Results.NotFound();
        });

        clientApi.MapDelete("/{id}", async (int id, ClinicService clinicService) =>
            await clinicService.DeleteClientAsync(id) ? Results.NoContent() : Results.NotFound());

        return app;
    }
}