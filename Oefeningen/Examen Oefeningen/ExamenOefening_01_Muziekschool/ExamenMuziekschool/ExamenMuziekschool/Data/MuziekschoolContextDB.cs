namespace ExamenMuziekschool.Data
{
    public class MuziekschoolContextDB : DbContext
    {
        // Toevoegen databases
        public DbSet<Leerling> Leerlingen { get; set; }
        public DbSet<Instrument> Instrumenten { get; set; }
        public DbSet<Lesgever> Lesgevers { get; set; }
        public DbSet<LesgeverInstrument> LesgeverInstrumenten { get; set; }

        // Constructor

        public MuziekschoolContextDB(DbContextOptions<MuziekschoolContextDB> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            GenerateTables(modelBuilder);
            SeedData(modelBuilder);
        }

        private void GenerateTables(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Leerling>(entity =>
            {
                entity.ToTable("Leerlingen");
                entity.Property(leerling => leerling.Naam).IsRequired().HasMaxLength(50);
                entity.Property(leerling => leerling.Voornaam).IsRequired().HasMaxLength(50);
                entity.Property(leerling => leerling.Email).IsRequired().HasMaxLength(100);
                entity.Property(leerling => leerling.Inschrijfdatum).IsRequired();

                entity.HasOne(leerling => leerling.Instrument)
                .WithMany(instrument => instrument.Leerlingen)
                .HasForeignKey(leerling => leerling.InstrumentId)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired();
            });

            modelBuilder.Entity<Instrument>(entity =>
            {
                entity.ToTable("Instrumenten");
                entity.Property(instrument => instrument.Naam).IsRequired().HasMaxLength(50);
                entity.Property(instrument => instrument.Categorie).IsRequired().HasMaxLength(50);
            });

            modelBuilder.Entity<Lesgever>(entity =>
            {
                entity.ToTable("Lesgevers");
                entity.Property(lesgever => lesgever.Naam).IsRequired().HasMaxLength(50);
                entity.Property(lesgever => lesgever.Voornaam).IsRequired().HasMaxLength(50);
                entity.Property(lesgever => lesgever.Email).IsRequired().HasMaxLength(100);
            });

            modelBuilder.Entity<LesgeverInstrument>(entity =>
            {
                entity.ToTable("LesgeverInstrumenten");

                entity.HasOne(li => li.Instrument)
                .WithMany(instrument => instrument.LesgeverInstrumenten)
                .HasForeignKey(li => li.InstrumentId)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();

                entity.HasOne(li => li.Lesgever)
                .WithMany(lesgever => lesgever.LesgeverInstrumenten)
                .HasForeignKey(li => li.LesgeverId)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();
            });
        }

        private void SeedData(ModelBuilder modelBuilder)
        {
            // 1. Lesgever Seeden
            modelBuilder.Entity<Lesgever>().HasData(
                new Lesgever { Id = 1, Naam = "Van Beethoven", Voornaam = "Ludwig", Email = "ludwig@muziekschool.be" },
                new Lesgever { Id = 2, Naam = "Peeters", Voornaam = "Clara", Email = "clara.peeters@muziekschool.be" },
                new Lesgever { Id = 3, Naam = "Janssens", Voornaam = "David", Email = "david.janssens@muziekschool.be" },
                new Lesgever { Id = 4, Naam = "Maes", Voornaam = "Hanne", Email = "hanne.maes@muziekschool.be" },
                new Lesgever { Id = 5, Naam = "Vermeulen", Voornaam = "Bram", Email = "bram.vermeulen@muziekschool.be" }
            );

            // 2. Leerling Seeden
            modelBuilder.Entity<Leerling>().HasData(
                new Leerling { Id = 1, Naam = "Jacobs", Voornaam = "Mila", Email = "mila.jacobs@mail.com", Inschrijfdatum = new DateTime(2025, 9, 1, 0, 0, 0, DateTimeKind.Utc), InstrumentId = 1 },
                new Leerling { Id = 2, Naam = "Peeters", Voornaam = "Noah", Email = "noah.peeters@mail.com", Inschrijfdatum = new DateTime(2025, 9, 3, 0, 0, 0, DateTimeKind.Utc), InstrumentId = 1 },
                new Leerling { Id = 3, Naam = "Willems", Voornaam = "Camille", Email = "camille.willems@mail.com", Inschrijfdatum = new DateTime(2025, 9, 5, 0, 0, 0, DateTimeKind.Utc), InstrumentId = 2 },
                new Leerling { Id = 4, Naam = "Claes", Voornaam = "Vince", Email = "vince.claes@mail.com", Inschrijfdatum = new DateTime(2025, 9, 10, 0, 0, 0, DateTimeKind.Utc), InstrumentId = 2 },
                new Leerling { Id = 5, Naam = "Peeters", Voornaam = "Marie", Email = "marie.peeters@mail.com", Inschrijfdatum = new DateTime(2025, 9, 12, 0, 0, 0, DateTimeKind.Utc), InstrumentId = 3 },
                new Leerling { Id = 6, Naam = "Dubois", Voornaam = "Lucas", Email = "lucas.dubois@mail.com", Inschrijfdatum = new DateTime(2025, 9, 15, 0, 0, 0, DateTimeKind.Utc), InstrumentId = 4 }
            );

            // 3. Instrument Seeden
            modelBuilder.Entity<Instrument>().HasData(
                new Instrument { Id = 1, Naam = "Piano", Categorie = "Toetsinstrumenten" },
                new Instrument { Id = 2, Naam = "Gitaar", Categorie = "Snaarinstrumenten" },
                new Instrument { Id = 3, Naam = "Viool", Categorie = "Strijkinstrumenten" },
                new Instrument { Id = 4, Naam = "Drums", Categorie = "Slagwerk" },
                new Instrument { Id = 5, Naam = "Dwarsfluit", Categorie = "Blaasinstrumenten" }
            );

            // 4. LesgeverInstrument Seeden
            modelBuilder.Entity<LesgeverInstrument>().HasData(
                new LesgeverInstrument { Id = 1, InstrumentId = 1, LesgeverId = 1 },
                new LesgeverInstrument { Id = 2, InstrumentId = 1, LesgeverId = 2 },
                new LesgeverInstrument { Id = 3, InstrumentId = 2, LesgeverId = 3 },
                new LesgeverInstrument { Id = 4, InstrumentId = 2, LesgeverId = 1 },
                new LesgeverInstrument { Id = 5, InstrumentId = 3, LesgeverId = 4 },
                new LesgeverInstrument { Id = 6, InstrumentId = 4, LesgeverId = 5 },
                new LesgeverInstrument { Id = 7, InstrumentId = 3, LesgeverId = 2 }
            );

        }
    }
}
