using Microsoft.EntityFrameworkCore;
using WebApi.Models;

namespace WebApi.Data;

public class KlankkastContext : DbContext
{
    public KlankkastContext(DbContextOptions<KlankkastContext> options)
        : base(options) { }

    public DbSet<Zaal> Zalen { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Zaal>(entity =>
        {
            entity.ToTable("Zalen");
            entity.Property(zaal => zaal.Naam).IsRequired().HasMaxLength(50);
            entity.Property(zaal => zaal.Capaciteit).IsRequired();
            entity.Property(zaal => zaal.)
        });

    }
}
