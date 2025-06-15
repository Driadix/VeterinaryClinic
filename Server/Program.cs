using BusinessLogic.Services;
using BusinessLogic.Strategies;
using Core.Interfaces;
using DataAccess;
using Microsoft.EntityFrameworkCore;

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

app.MapGet("/api/clients", async (ClinicService clinicService) =>
{
    try
    {
        var clients = await clinicService.GetAllClientsAsync();
        return Results.Ok(clients);
    }
    catch (Exception ex)
    {
        return Results.Problem("Произошла ошибка на сервере: " + ex.Message);
    }
});

app.Run();