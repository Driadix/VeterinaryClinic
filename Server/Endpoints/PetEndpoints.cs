using BusinessLogic.Services;
using Core.Models;

namespace Server.Endpoints;

public static class PetEndpoints
{
    public static IEndpointRouteBuilder MapPetEndpoints(this IEndpointRouteBuilder app)
    {
        var petApi = app.MapGroup("/api/pets");

        petApi.MapGet("/client/{clientId}", async (int clientId, ClinicService clinicService) =>
            Results.Ok(await clinicService.GetPetsForClientAsync(clientId)));

        petApi.MapPost("/", async (Pet newPet, ClinicService clinicService) =>
        {
            await clinicService.AddPetAsync(newPet);
            return Results.Created($"/api/pets/{newPet.Id}", newPet);
        });

        petApi.MapPut("/{id}", async (int id, Pet updatedPet, ClinicService clinicService) =>
        {
            if (id != updatedPet.Id) return Results.BadRequest("ID mismatch.");
            var success = await clinicService.UpdatePetAsync(updatedPet);
            return success ? Results.NoContent() : Results.NotFound();
        });

        petApi.MapDelete("/{id}", async (int id, ClinicService clinicService) =>
            await clinicService.DeletePetAsync(id) ? Results.NoContent() : Results.NotFound());

        return app;
    }
}