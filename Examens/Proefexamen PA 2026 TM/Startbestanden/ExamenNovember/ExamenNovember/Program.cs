var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// Repositories
builder.Services.AddScoped<IDocentRepository, DocentRepository>();
builder.Services.AddScoped<IKlasRepository, KlasRepository>();
builder.Services.AddScoped<IStudentRepository, StudentRepository>();

// Database setup
string connectionString = builder.Configuration.GetConnectionString("PostgresConnection");
builder.Services.AddDbContext<ExamenNovemberContext>(options => options.UseNpgsql(connectionString));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
