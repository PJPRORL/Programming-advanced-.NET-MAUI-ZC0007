# 04_01

## Leerdoel

Na deze oefening kan je een `DbContext` aanmaken met `DbSet<T>` voor een model, de context registreren in de Dependency Injection container, en een migratie aanmaken en uitvoeren voor een PostgreSQL-database.

Je leert hoe je een Code-First database opzet vanuit C#-modellen, hoe je de `DbContext` configureert in `Program.cs`, en hoe je migraties gebruikt om de database te synchroniseren met je modellen.

## Opdracht

De bibliotheek **Stadsbibliotheek Noord** wil haar catalogus-API migreren van een in-memory lijst naar een echte PostgreSQL-database met Entity Framework Core.

Jouw taak is om een `DbContext` aan te maken, een migratie toe te passen, en een eenvoudige GET-endpoint te maken die alle boeken uit de database ophaalt.

### Het Boek Model

Gebruik het bestaande `Boek`-Model in de map `Models`:

| Property | Type | Beschrijving |
| -------- | ---- | ------------ |
| Id | int | Unieke identificatie van het boek |
| Titel | string | De titel van het boek |
| Auteur | string | De auteur van het boek |
| Uitgeverij | string | De uitgeverij |
| Jaartal | int | Het publicatiejaar |

### De DbContext

Maak een nieuwe map genaamd **Data**. Voeg een klasse genaamd **BoekCatalogusContext** toe die overerft van `DbContext`.

De context moet:
- een constructor hebben die `DbContextOptions<BoekCatalogusContext>` accepteert en doorgeeft aan de basisklasse;
- een `DbSet<Boek>` property genaamd **Boeken** bevatten;
- de namespace **WebApi.Data** gebruiken.

```csharp
using Microsoft.EntityFrameworkCore;
using WebApi.Models;

namespace WebApi.Data;

public class BoekCatalogusContext : DbContext
{
    public BoekCatalogusContext(DbContextOptions<BoekCatalogusContext> options)
        : base(options) { }

    public DbSet<Boek> Boeken { get; set; }
}
```

### De Database registreren in Program.cs

Registreer de `DbContext` in `Program.cs` met `AddDbContext` en koppel deze aan de PostgreSQL-provider (Npgsql). Gebruik de connection string met naam **PostgresConnection** uit `appsettings.json`.

```csharp
var connectionString = builder.Configuration.GetConnectionString("PostgresConnection");
builder.Services.AddDbContext<BoekCatalogusContext>(options =>
    options.UseNpgsql(connectionString));
```

### Migratie aanmaken en uitvoeren

Maak een migratie aan met de naam **InitialCreate** en voer deze uit om de database en tabellen te genereren.

### De Controller

Maak een `BoekController` met volgende endpoint:

#### 1. Alle boeken ophalen

Route: `GET /boeken`

Geef alle boeken terug als JSON met HTTP-statuscode **200 OK**.

De controller moet de `DbContext` ontvangen via de constructor (geen `new` in de Controller).
