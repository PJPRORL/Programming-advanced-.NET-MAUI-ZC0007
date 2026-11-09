using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;
using WebApi.Data;
using WebApi.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();

builder.Services.AddScoped<IBestellingRepository, BestellingRepository>();

builder.Services.AddDbContext<BestellingContext>(options =>
    options.UseSqlite("Data Source=bestellingen.db"));

builder.Services.AddLogging();

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
