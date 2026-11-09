using Examen_MVC.Data;
using Examen_MVC.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<ShopContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("LocalDBConnection")));

builder.Services.AddIdentity<CustomUser, IdentityRole>()
    .AddEntityFrameworkStores<ShopContext>();

builder.Services.AddScoped<UnitOfWork>();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddTransient<IdentitySeeding>();

builder.Services.ConfigureApplicationCookie(options =>
{

});

WebApplication app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
// Identity Seeding

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// Gebruik Identity
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Coffee}/{action=Index}/{id?}");

using (IServiceScope scope = app.Services.CreateScope())
{
    IdentitySeeding seeder = scope.ServiceProvider.GetRequiredService<IdentitySeeding>();
    RoleManager<IdentityRole> roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    UserManager<CustomUser> userManager = scope.ServiceProvider.GetRequiredService<UserManager<CustomUser>>();
    await seeder.IdentitySeedingAsync(userManager, roleManager);
}

app.Run();