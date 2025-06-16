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

builder.Services.AddScoped<EfStrategy>();
builder.Services.AddScoped<SqlStrategy>();

builder.Services.AddHttpContextAccessor();

builder.Services.AddScoped<IDataAccessStrategy>(provider =>
{
    var httpContextAccessor = provider.GetRequiredService<IHttpContextAccessor>();
    var strategyHeader = httpContextAccessor.HttpContext?.Request.Headers["X-Data-Access-Strategy"].FirstOrDefault();

    if (strategyHeader == "RawSQL")
    {
        return provider.GetRequiredService<SqlStrategy>();
    }

    return provider.GetRequiredService<EfStrategy>();
});

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