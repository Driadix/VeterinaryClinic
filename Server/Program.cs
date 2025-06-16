using BusinessLogic.Services;
using BusinessLogic.Strategies;
using Core.Interfaces;
using DataAccess;
using Microsoft.EntityFrameworkCore;
using Server.Endpoints;

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

app.MapClientEndpoints();
app.MapPetEndpoints();

app.Run();