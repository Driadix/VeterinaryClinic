using BusinessLogic.Services;
using BusinessLogic.Strategies;
using Core.Interfaces;
using DataAccess;
using Microsoft.EntityFrameworkCore;
using Server.Endpoints;
using VeterinaryClinic.BusinessLogic.Strategies;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

const bool useEntityFramework = false;

if (useEntityFramework)
{
    builder.Services.AddScoped<IDataAccessStrategy, EfStrategy>();
}
else
{
    builder.Services.AddScoped<IDataAccessStrategy, SqlStrategy>();
}

builder.Services.AddScoped<ClinicService>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapClientEndpoints();
app.MapPetEndpoints();
app.MapAppointmentEndpoints();
app.MapUserEndpoints();

app.Run();