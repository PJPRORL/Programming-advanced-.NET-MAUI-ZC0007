using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;
using WebApi.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();

// Haal de connection string op uit appsettings.json
string connectionString = builder.Configuration.GetConnectionString("PostgresConnection")!;

// Registreer de DbContext met de PostgreSQL provider (Npgsql)
builder.Services.AddDbContext<KlankkastContext>(options =>
    options.UseNpgsql(connectionString));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
