using BusinessLogic.Services;
using BusinessLogic.Strategies;
using Core.Interfaces;
using DataAccess;
using Microsoft.EntityFrameworkCore;
using Core.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IDataAccessStrategy, EfStrategy>();
builder.Services.AddScoped<ClinicService>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

var clientApi = app.MapGroup("/api/clients");

// GET /api/clients
clientApi.MapGet("/", async (ClinicService clinicService) => Results.Ok(await clinicService.GetAllClientsAsync()));

// GET /api/clients/{id}
clientApi.MapGet("/{id}", async (int id, ClinicService clinicService) =>
{
    var client = await clinicService.GetClientByIdAsync(id);
    return client != null ? Results.Ok(client) : Results.NotFound();
});

// POST /api/clients
clientApi.MapPost("/", async (Client newClient, ClinicService clinicService) =>
{
    await clinicService.AddClientAsync(newClient);
    return Results.Created($"/api/clients/{newClient.Id}", newClient);
});

// PUT /api/clients/{id}
clientApi.MapPut("/{id}", async (int id, Client updatedClient, ClinicService clinicService) =>
{
    if (id != updatedClient.Id) return Results.BadRequest("ID mismatch.");

    var success = await clinicService.UpdateClientAsync(updatedClient);

    return success ? Results.NoContent() : Results.NotFound();
});

// DELETE /api/clients/{id}
clientApi.MapDelete("/{id}", async (int id, ClinicService clinicService) =>
{
    var existingClient = await clinicService.GetClientByIdAsync(id);
    if (existingClient is null)
    {
        return Results.NotFound();
    }

    await clinicService.DeleteClientAsync(id);
    return Results.NoContent();
});

app.Run();