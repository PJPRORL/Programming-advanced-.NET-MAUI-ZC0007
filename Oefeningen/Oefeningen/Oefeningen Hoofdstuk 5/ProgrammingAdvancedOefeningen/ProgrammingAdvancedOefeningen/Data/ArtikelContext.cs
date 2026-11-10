namespace ProgrammingAdvancedOefeningen.Data
{
    public class ArtikelContext : DbContext
    {
        public ArtikelContext(DbContextOptions<ArtikelContext> options) : base(options) { }

        public DbSet<Artikel> Artikelen { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Artikel>(entity =>
            {
                entity.ToTable("Artikelen");
                entity.Property(artikel => artikel.Naam).IsRequired().HasMaxLength(100);
                entity.Property(artikel => artikel.Merk).IsRequired().HasMaxLength(50);
                entity.Property(artikel => artikel.Soort).IsRequired().HasMaxLength(30);
                entity.Property(artikel => artikel.Prijs).IsRequired().HasPrecision(18, 2);
                entity.Property(artikel => artikel.AantalOpVoorraad).IsRequired();
            });
        }
    }
}
