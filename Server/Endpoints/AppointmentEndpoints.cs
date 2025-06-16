using BusinessLogic.Services;
using Core.Models;

namespace Server.Endpoints;

public static class AppointmentEndpoints
{
    public static IEndpointRouteBuilder MapAppointmentEndpoints(this IEndpointRouteBuilder app)
    {
        var appointmentApi = app.MapGroup("/api/appointments");

        appointmentApi.MapGet("/", async (ClinicService clinicService) =>
            Results.Ok(await clinicService.GetAllAppointmentsAsync()));

        appointmentApi.MapPost("/", async (Appointment newAppointment, ClinicService clinicService) =>
        {
            await clinicService.AddAppointmentAsync(newAppointment);
            return Results.Created($"/api/appointments/{newAppointment.Id}", newAppointment);
        });

        appointmentApi.MapPut("/{id}", async (int id, Appointment updatedAppointment, ClinicService clinicService) =>
        {
            if (id != updatedAppointment.Id) return Results.BadRequest("ID mismatch.");
            var success = await clinicService.UpdateAppointmentAsync(updatedAppointment);
            return success ? Results.NoContent() : Results.NotFound();
        });

        appointmentApi.MapDelete("/{id}", async (int id, ClinicService clinicService) =>
            await clinicService.DeleteAppointmentAsync(id) ? Results.NoContent() : Results.NotFound());

        return app;
    }
}