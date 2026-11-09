using GameLibraryAPI.Data;
using GameLibraryAPI.Data.UnitOfWork;
using GameLibraryAPI.Helper;
using GameLibraryAPI.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(swagger =>
{
    //This is to generate the Default UI of Swagger Documentation    
    swagger.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Web API",
        Description = "Authentication and Authorization in ASP.NET with JWT and Swagger"
    });
    // To Enable authorization using Swagger (JWT)    
    swagger.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        //Type = SecuritySchemeType.ApiKey,
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Enter ZONDER Bearer. Dus gewoon je TOKEN: \r\n\r\nExample: \"eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9\"",
    });
    swagger.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});

//Service voor identity en dbcontext
builder.Services.AddIdentity<Gebruiker, IdentityRole>()
.AddEntityFrameworkStores<GLContext>()
    .AddDefaultTokenProviders();

builder.Services.AddDbContext<GLContext>(options => options
    .UseSqlServer(builder.Configuration.GetConnectionString("LocalDBConnection")));

// Service voor UnitOfWork
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// Service voor Automapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

Token.Settings = new Settings
{
    Secret = (builder.Configuration["Settings:Secret"] ?? "XR17").ToCharArray(),
    ValidIssuer = builder.Configuration["Settings:ValidIssuer"] ?? "ExamenAPI",
    ValidAudience = builder.Configuration["Settings:ValidAudience"] ?? "ExamenAPI"
};

builder.Configuration.GetRequiredSection(nameof(Settings)).Bind(Token.Settings);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
    {
        options.IncludeErrorDetails = true;
        options.SaveToken = true;
        options.RequireHttpsMetadata = false;
        options.UseSecurityTokenValidators = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = Token.Settings.ValidIssuer, // Pas aan naar jouw issuer
            ValidAudience = Token.Settings.ValidAudience, // Pas aan naar jouw audience
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Token.Settings.Secret)) // Gebruik een veilige sleutel
        };
        // Customize the JWT Bearer events
        options.Events = new JwtBearerEvents
        {
            OnChallenge = context =>
            {
                // Customize unauthorized response
                context.Response.StatusCode = 401; // Unauthorized
                context.Response.ContentType = "application/json";
                var result = "{\"error\": \"Niet geauthenticeerd. Token is ongeldig of ontbreekt.\"}";
                return context.Response.WriteAsync(result);
            },
            OnForbidden = context =>
            {
                // Customize forbidden response
                context.Response.StatusCode = 403; // Forbidden
                context.Response.ContentType = "application/json";
                var result = "{\"error\": \"Toegang geweigerd. U heeft niet de juiste rechten voor deze actie.\"}";
                return context.Response.WriteAsync(result);
            }
        };
    });


builder.Services.Configure<IdentityOptions>(options =>
{
    // Password settings. 
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequireUppercase = true;
    options.Password.RequiredLength = 6;
    options.Password.RequiredUniqueChars = 1;

    // Lockout settings.
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
    options.Lockout.MaxFailedAccessAttempts = 5;
    options.Lockout.AllowedForNewUsers = true;

    // User settings. 
    options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
    options.User.RequireUniqueEmail = false;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
