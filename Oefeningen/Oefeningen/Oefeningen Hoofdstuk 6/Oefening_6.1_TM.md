# 06_01

## Leerdoel

Na deze oefening kan je een één-op-veel (1:N) relatie configureren tussen twee modellen met de Fluent API in Entity Framework Core.

Je leert hoe je Navigation Properties gebruikt om relaties tussen tabellen te leggen, hoe je Foreign Keys definieert, en hoe je `OnModelCreating` overschrijft om de relatie expliciet te configureren met `HasOne`, `WithMany`, `HasForeignKey` en `OnDelete`.

## Opdracht

De bibliotheek **Stadsbibliotheek Noord** wil haar catalogus-API migreren van een in-memory lijst naar een echte PostgreSQL-database met Entity Framework Core. Men wil de relatie tussen **Klanten** en **Bestellingen** configureren zodat elke klant meerdere bestellingen heeft, maar elke bestelling bij precies één klant hoort.

Jouw taak is om de Fluent API-configuratie voor de 1:N-relatie tussen Klant en Bestelling te implementeren in de `DbContext`.

### Het Klant Model

Maak een Modelklasse `Klant` in de map `Models` met volgende Properties:

| Property | Type | Beschrijving |
| -------- | ---- | ------------ |
| Id | int | Unieke identificatie van de klant |
| Voornaam | string | De voornaam van de klant |
| Naam | string | De achternaam van de klant |
| AangemaaktDatum | DateTime | De datum waarop de klant is aangemaakt |

```csharp
namespace WebApi.Models;

public class Klant
{
    public int Id { get; set; }
    public string Voornaam { get; set; } = default!;
    public string Naam { get; set; } = default!;
    public DateTime AangemaaktDatum { get; set; }
}
```

### Het Bestelling Model

Maak een Modelklasse `Bestelling` in de map `Models` met volgende Properties:

| Property | Type | Beschrijving |
| -------- | ---- | ------------ |
| Id | int | Unieke identificatie van de bestelling |
| KlantId | int | Foreign Key naar de klant die de bestelling heeft geplaatst |
| Klant | Klant? | Navigation Property naar de klant (optioneel) |
| ProductNaam | string | De naam van het bestelde product |
| Aantal | int | Het aantal bestelde stuks |
| Besteldatum | DateTime | De datum waarop de bestelling is geplaatst |

```csharp
namespace WebApi.Models;

public class Bestelling
{
    public int Id { get; set; }
    public int KlantId { get; set; }
    public Klant? Klant { get; set; } = default!;
    public string ProductNaam { get; set; } = default!;
    public int Aantal { get; set; }
    public DateTime Besteldatum { get; set; }
}
```

### De DbContext

Maak een nieuwe map genaamd **Data**. Voeg een klasse genaamd **BibliotheekContext** toe die overerft van `DbContext`.

De context moet:
- een constructor hebben die `DbContextOptions<BibliotheekContext>` accepteert en doorgeeft aan de basisklasse;
- een `DbSet<Klant>` property genaamd **Klanten** bevatten;
- een `DbSet<Bestelling>` property genaamd **Bestellingen** bevatten;
- de Fluent API (`OnModelCreating`) overschrijven om de volgende configuraties toe te passen:
  - de tabelnaam voor **Klant** expliciet instellen op **Klant**;
  - de tabelnaam voor **Bestelling** expliciet instellen op **Bestelling**;
  - de 1:N-relatie tussen Klant en Bestelling configureren met de Fluent API:
    - `HasOne(p => p.Klant)` — een bestelling heeft één specifieke klant;
    - `WithMany(x => x.Bestellingen)` — die ene klant kan meerdere bestellingen hebben (voeg deze Navigation Property toe aan het Klant model);
    - `HasForeignKey(y => y.KlantId)` — de Foreign Key is KlantId;
    - `OnDelete(DeleteBehavior.Restrict)` — blokkeer het verwijderen van een klant zolang deze nog bestellingen heeft;
    - `.IsRequired()` — de relatie is verplicht (een bestelling moet bij een klant horen).

```csharp
using Microsoft.EntityFrameworkCore;
using WebApi.Models;

namespace WebApi.Data;

public class BibliotheekContext : DbContext
{
    public BibliotheekContext(DbContextOptions<BibliotheekContext> options)
        : base(options) { }

    public DbSet<Klant> Klanten { get; set; }
    public DbSet<Bestelling> Bestellingen { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Klant>(entity =>
        {
            entity.ToTable("Klant");
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

### Klant Model aanpassen

Voeg de volgende Navigation Property toe aan het Klant model:

```csharp
public List<Bestelling>? Bestellingen { get; set; } = default!;
```

### Seed data toevoegen

Voeg de volgende seed data toe in de `OnModelCreating`-methode van de context:

| Id | Voornaam | Naam | AangemaaktDatum |
| -- | -------- | ---- | --------------- |
| 1 | Leon | Van Der Neffe | 2022-10-01 |
| 2 | Firmin | Van De Kasseinen | 2022-10-02 |
| 3 | Marcel | Kiekeboe | 2022-10-03 |

### Dependency Injection

Registreer de DbContext in `Program.cs` met `AddDbContext`:

```csharp
builder.Services.AddDbContext<BibliotheekContext>(options =>
    options.UseNpgsql(connectionString));
```
