# 06_02

## Leerdoel

Na deze oefening kan je twee één-op-veel (1:N) relaties configureren tussen drie modellen en seed data toevoegen via de Fluent API in Entity Framework Core.

Je leert hoe je meerdere 1:N-relaties onafhankelijk configureert, hoe je `HasData` gebruikt om seed data te definiëren, en hoe je Foreign Keys correct koppelt aan de juiste Navigation Properties bij het seeden van data.

## Opdracht

De fietsenwinkel **Fietsspecialist Gent** wil haar inventory-API migreren van een in-memory lijst naar een echte PostgreSQL-database met Entity Framework Core. Men wil de relaties tussen **Klanten**, **Bestellingen** en **Producten** configureren zodat elke klant meerdere bestellingen heeft en elke bestelling meerdere producten bevat via een tussentabel.

Jouw taak is om de Fluent API-configuratie voor de 1:N-relaties tussen Klant, Bestelling en Product te implementeren en seed data toe te voegen.

### Het Klant Model

Maak een Modelklasse `Klant` in de map `Models` met volgende Properties:

| Property | Type | Beschrijving |
| -------- | ---- | ------------ |
| Id | int | Unieke identificatie van de klant |
| Voornaam | string | De voornaam van de klant |
| Naam | string | De achternaam van de klant |
| AangemaaktDatum | DateTime | De datum waarop de klant is aangemaakt |
| Bestellingen | List<Bestelling>? | Navigation Property naar de bestellingen van de klant |

```csharp
namespace WebApi.Models;

public class Klant
{
    public int Id { get; set; }
    public string Voornaam { get; set; } = default!;
    public string Naam { get; set; } = default!;
    public DateTime AangemaaktDatum { get; set; }
    public List<Bestelling>? Bestellingen { get; set; } = default!;
}
```

### Het Product Model

Maak een Modelklasse `Product` in de map `Models` met volgende Properties:

| Property | Type | Beschrijving |
| -------- | ---- | ------------ |
| Id | int | Unieke identificatie van het product |
| Naam | string | De naam van het product |
| Beschrijving | string? | De beschrijving van het product (optioneel) |
| Prijs | decimal | De prijs van het product |
| OrderLijnen | List<OrderLijn> | Navigation Property naar de orderlijnen die dit product bevatten |

```csharp
namespace WebApi.Models;

public class Product
{
    public int Id { get; set; }
    public string Naam { get; set; } = default!;
    public string? Beschrijving { get; set; }
    public decimal Prijs { get; set; }
    public List<OrderLijn> OrderLijnen { get; set; } = default!;
}
```

### Het Bestelling Model

Maak een Modelklasse `Bestelling` in de map `Models` met volgende Properties:

| Property | Type | Beschrijving |
| -------- | ---- | ------------ |
| Id | int | Unieke identificatie van de bestelling |
| KlantId | int | Foreign Key naar de klant die de bestelling heeft geplaatst |
| Klant | Klant? | Navigation Property naar de klant (optioneel) |
| OrderLijnen | List<OrderLijn> | Navigation Property naar de orderlijnen van deze bestelling |

```csharp
namespace WebApi.Models;

public class Bestelling
{
    public int Id { get; set; }
    public int KlantId { get; set; }
    public Klant? Klant { get; set; } = default!;
    public List<OrderLijn> OrderLijnen { get; set; } = default!;
}
```

### De DbContext

Maak een nieuwe map genaamd **Data**. Voeg een klasse genaamd **FietsContext** toe die overerft van `DbContext`.

De context moet:
- een constructor hebben die `DbContextOptions<FietsContext>` accepteert en doorgeeft aan de basisklasse;
- een `DbSet<Klant>` property genaamd **Klanten** bevatten;
- een `DbSet<Product>` property genaamd **Producten** bevatten;
- een `DbSet<Bestelling>` property genaamd **Bestellingen** bevatten;
- de Fluent API (`OnModelCreating`) overschrijven om de volgende configuraties toe te passen:
  - de tabelnaam voor **Klant** expliciet instellen op **Klant**;
  - de tabelnaam voor **Product** expliciet instellen op **Product**;
  - de tabelnaam voor **Bestelling** expliciet instellen op **Bestelling**;
  - de 1:N-relatie tussen Klant en Bestelling configureren met de Fluent API:
    - `HasOne(p => p.Klant)` — een bestelling heeft één specifieke klant;
    - `WithMany(x => x.Bestellingen)` — die ene klant kan meerdere bestellingen hebben;
    - `HasForeignKey(y => y.KlantId)` — de Foreign Key is KlantId;
    - `OnDelete(DeleteBehavior.Restrict)` — blokkeer het verwijderen van een klant zolang deze nog bestellingen heeft;
    - `.IsRequired()` — de relatie is verplicht.

```csharp
using Microsoft.EntityFrameworkCore;
using WebApi.Models;

namespace WebApi.Data;

public class FietsContext : DbContext
{
    public FietsContext(DbContextOptions<FietsContext> options)
        : base(options) { }

    public DbSet<Klant> Klanten { get; set; }
    public DbSet<Product> Producten { get; set; }
    public DbSet<Bestelling> Bestellingen { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Klant>(entity =>
        {
            entity.ToTable("Klant");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.ToTable("Product");
        });

        modelBuilder.Entity<Bestelling>(entity =>
        {
            entity.ToTable("Bestelling");

            entity.HasOne(p => p.Klant)
                .WithMany(x => x.Bestellingen)
                .HasForeignKey(y => y.KlantId)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired();
        });
    }
}
```

### Seed data toevoegen

Voeg de volgende seed data toe in de `OnModelCreating`-methode van de context:

**Klanten:**

| Id | Voornaam | Naam | AangemaaktDatum |
| -- | -------- | ---- | --------------- |
| 1 | Leon | Van Der Neffe | 2022-10-01 |
| 2 | Firmin | Van De Kasseinen | 2022-10-02 |
| 3 | Marcel | Kiekeboe | 2022-10-03 |

**Producten:**

| Id | Naam | Beschrijving | Prijs |
| -- | ---- | ---------- | ----- |
| 1 | fiets | Dit is een fiets | 100.00 |
| 2 | koersfiets | Dit is een mooie koersfiets | 200.00 |
| 3 | auto | Dit is een auto | 2000.00 |

**Bestellingen:**

| Id | KlantId |
| -- | ------- |
| 1 | 1 |
| 2 | 2 |
| 3 | 3 |
| 4 | 2 |
| 5 | 3 |

### Dependency Injection

Registreer de DbContext in `Program.cs` met `AddDbContext`:

```csharp
builder.Services.AddDbContext<FietsContext>(options =>
    options.UseNpgsql(connectionString));
```
