# 04_03

## Leerdoel

Na deze oefening kan je een `DbContext` gebruiken om gefilterde query's uit te voeren met LINQ en Entity Framework Core.

Je leert hoe je meerdere `DbSet<T>`-properties configureert in een context, hoe je LINQ-query's schrijft die door EF Core worden vertaald naar SQL, en hoe je case-insensitive vergelijkingen uitvoert in een database-query.

## Opdracht

De fictieve bioscoop **Cinema Paradiso** wil haar filmcatalogus-API migreren van een in-memory lijst naar een echte PostgreSQL-database met Entity Framework Core. In plaats van slechts één zoekmethode (`GetById`), wil men meerdere query-methodes om films op verschillende manieren te kunnen zoeken.

Jouw taak is om een `DbContext` aan te maken met meerdere `DbSet<T>`-properties, een migratie toe te passen, en endpoints te implementeren die gefilterde query's uitvoeren met LINQ.

### Het Film Model

Maak een Modelklasse `Film` in de map `Models` met volgende Properties:

| Property | Type | Beschrijving |
| -------- | ---- | ------------ |
| Id | int | Unieke identificatie van de film |
| Titel | string | De titel van de film |
| Regisseur | string | De regisseur van de film |
| Genre | string | Het genre van de film |
| Speelduur | int | De speelduur in minuten |

### De DbContext

Maak een nieuwe map genaamd **Data**. Voeg een klasse genaamd **FilmCatalogusContext** toe die overerft van `DbContext`.

De context moet:
- een constructor hebben die `DbContextOptions<FilmCatalogusContext>` accepteert en doorgeeft aan de basisklasse;
- twee `DbSet<T>`-properties bevatten:
  - **Films** voor `Film`;
  - **Regisseurs** voor `Regisseur`;
- de Fluent API (`OnModelCreating`) overschrijven om:
  - de tabelnaam van `Film` expliciet in te stellen op **Films**;
  - de string-velden `Titel`, `Regisseur` en `Genre` verplicht maken (`IsRequired()`) met een maximale lengte van **150** karakters;
  - de tabelnaam van `Regisseur` expliciet in te stellen op **Regisseurs**;
- de namespace **WebApi.Data** gebruiken.

```csharp
using Microsoft.EntityFrameworkCore;
using WebApi.Models;

namespace WebApi.Data;

public class FilmCatalogusContext : DbContext
{
    public FilmCatalogusContext(DbContextOptions<FilmCatalogusContext> options)
        : base(options) { }

    public DbSet<Film> Films { get; set; }
    public DbSet<Regisseur> Regisseurs { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Film>(entity =>
        {
            entity.ToTable("Films");

            entity.Property(p => p.Titel).IsRequired().HasMaxLength(150);
            entity.Property(p => p.Regisseur).IsRequired().HasMaxLength(150);
            entity.Property(p => p.Genre).IsRequired().HasMaxLength(50);
        });

        modelBuilder.Entity<Regisseur>(entity =>
        {
            entity.ToTable("Regisseurs");
        });
    }
}
```

### De Database registreren in Program.cs

Registreer de `DbContext` in `Program.cs` met `AddDbContext` en koppel deze aan de PostgreSQL-provider (Npgsql). Gebruik de connection string met naam **PostgresConnection** uit `appsettings.json`.

### Migratie aanmaken en uitvoeren

Maak een migratie aan met de naam **InitialCreate** en voer deze uit om de database en tabellen te genereren.

### Seed data toevoegen

Voeg de volgende seed data toe in de `OnModelCreating`-methode van de context:

| Id | Titel | Regisseur | Genre | Speelduur |
| -- | ----- | --------- | ----- | --------- |
| 1 | The Shawshank Redemption | Frank Darabont | Drama | 142 |
| 2 | Inception | Christopher Nolan | Sci-Fi | 148 |
| 3 | De Ontdekking van de Hemel | Jeroen Krabbé | Drama | 165 |
| 4 | Interstellar | Christopher Nolan | Sci-Fi | 169 |
| 5 | De Avonturen van Pi | Ang Lee | Avontuur | 127 |

```csharp
modelBuilder.Entity<Film>().HasData(
    new Film { Id = 1, Titel = "The Shawshank Redemption", Regisseur = "Frank Darabont", Genre = "Drama", Speelduur = 142 },
    new Film { Id = 2, Titel = "Inception", Regisseur = "Christopher Nolan", Genre = "Sci-Fi", Speelduur = 148 },
    new Film { Id = 3, Titel = "De Ontdekking van de Hemel", Regisseur = "Jeroen Krabbé", Genre = "Drama", Speelduur = 165 },
    new Film { Id = 4, Titel = "Interstellar", Regisseur = "Christopher Nolan", Genre = "Sci-Fi", Speelduur = 169 },
    new Film { Id = 5, Titel = "De Avonturen van Pi", Regisseur = "Ang Lee", Genre = "Avontuur", Speelduur = 127 }
);
```

### De Controller

Maak een `FilmController` met volgende endpoints:

#### 1. Alle films ophalen

Route: `GET /films`

Geef alle films terug als JSON met HTTP-statuscode **200 OK**.

#### 2. Eén film ophalen op basis van de ID

Route: `GET /films/{id}`

Vind de film met de gevraagde ID en geef het terug als JSON met HTTP-statuscode **200 OK**.

Als er geen film bestaat met de gevraagde ID, geef dan HTTP-statuscode **404 Not Found** terug zonder body.

#### 3. Films ophalen op basis van het genre

Route: `GET /films/genre/{genre}`

De routeparameter `{genre}` stelt het genre voor.

Zoek films met het gevraagde genre en geef ze terug als JSON met HTTP-statuscode **200 OK**.

Wordt er geen film gevonden voor dat genre, geef dan een lege JSON-lijst `[]` terug met HTTP-statuscode **200 OK** (geen 404 voor lege resultaten).

De query moet ongevoelig zijn voor hoofdletters en kleine letters bij het vergelijken van het genre.

De controller moet de `DbContext` ontvangen via de constructor (geen `new` in de Controller).
