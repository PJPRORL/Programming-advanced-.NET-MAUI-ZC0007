using Microsoft.EntityFrameworkCore;
using WebApi.Models;

namespace WebApi.Data;

public class LaptopCatalogusContext : DbContext
{
    public LaptopCatalogusContext(DbContextOptions<LaptopCatalogusContext> options)
        : base(options) { }

    public DbSet<Laptop> Laptops { get; set; }
}
