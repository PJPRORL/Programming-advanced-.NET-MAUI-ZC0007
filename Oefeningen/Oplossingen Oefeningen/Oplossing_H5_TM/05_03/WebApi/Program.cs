using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;
using WebApi.Data;
using WebApi.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();

builder.Services.AddScoped<IBoekRepository, BoekRepository>();

builder.Services.AddDbContext<BoekCatalogusContext>(options =>
    options.UseSqlite("Data Source=boeken.db"));

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
