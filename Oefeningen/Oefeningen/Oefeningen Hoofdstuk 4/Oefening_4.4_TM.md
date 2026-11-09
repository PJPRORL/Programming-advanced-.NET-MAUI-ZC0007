# 04_04

## Leerdoel

Na deze oefening kan je alle concepten uit dit hoofdstuk combineren: een `DbContext` met Fluent API-configuratie, volledige CRUD-operaties, gefilterde LINQ-query's, Dependency Injection en Logging in één coherent API-project.

Je leert hoe je een complete applicatie bouwt die alle CRUD-operaties ondersteunt, gefilterde queries toestaat, logging gebruikt in elke endpoint, en correct omgaat met niet-bestaande resources — allemaal met een echte database via Entity Framework Core.

## Opdracht

De evenementenorganisatie **Festivaal Vlaanderen** wil een API bouwen voor het beheren van hun evenementenagenda. Men wil alle datatoegang laten verlopen via Entity Framework Core met een echte PostgreSQL-database in plaats van een in-memory lijst. Men wil ook Logging invoegen in elke endpoint.

Jouw taak is om een `DbContext` aan te maken met Fluent API-configuratie, een migratie toe te passen, en een volledige CRUD-API te bouwen met gefilterde queries en logging.

### Het Evenement Model

Maak een Modelklasse `Evenement` in de map `Models` met volgende Properties:

| Property | Type | Beschrijving |
| -------- | ---- | ------------ |
| Id | int | Unieke identificatie van het evenement |
| Naam | string | De naam van het evenement |
| Locatie | string | De locatie van het evenement |
| Datum | string | De datum van het evenement (indeling: "dd-MM-yyyy") |
| MaxDeelnemers | int | Het maximum aantal deelnemers |
| GeregistreerdeDeelnemers | int | Het aantal geregistreerde deelnemers |

### De DbContext met Fluent API

Maak een nieuwe map genaamd **Data**. Voeg een klasse genaamd **EvenementContext** toe die overerft van `DbContext`.

De context moet:
- een constructor hebben die `DbContextOptions<EvenementContext>` accepteert en doorgeeft aan de basisklasse;
- een `DbSet<Evenement>` property genaamd **Evenementen** bevatten;
- de Fluent API (`OnModelCreating`) overschrijven om de volgende configuraties toe te passen:
  - de tabelnaam expliciet instellen op **Evenementen**;
  - de string-velden `Naam`, `Locatie` en `Datum` verplicht maken (`IsRequired()`) met een maximale lengte van **200** karakters;
- de namespace **WebApi.Data** gebruiken.

```csharp
using Microsoft.EntityFrameworkCore;
using WebApi.Models;

namespace WebApi.Data;

public class EvenementContext : DbContext
{
    public EvenementContext(DbContextOptions<EvenementContext> options)
        : base(options) { }

    public DbSet<Evenement> Evenementen { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Evenement>(entity =>
        {
            entity.ToTable("Evenementen");

            entity.Property(p => p.Naam).IsRequired().HasMaxLength(200);
            entity.Property(p => p.Locatie).IsRequired().HasMaxLength(200);
            entity.Property(p => p.Datum).IsRequired().HasMaxLength(10);
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

| Id | Naam | Locatie | Datum | MaxDeelnemers | GeregistreerdeDeelnemers |
| -- | ---- | ------- | ----- | ------------- | ---------------------- |
| 1 | Summer Music Festival | Antwerpen | 15-07-2026 | 5000 | 3200 |
| 2 | Culinaire Dagen | Brugge | 22-08-2026 | 200 | 145 |
| 3 | Tech Conference | Gent | 10-09-2026 | 300 | 300 |

```csharp
modelBuilder.Entity<Evenement>().HasData(
    new Evenement { Id = 1, Naam = "Summer Music Festival", Locatie = "Antwerpen", Datum = "15-07-2026", MaxDeelnemers = 5000, GeregistreerdeDeelnemers = 3200 },
    new Evenement { Id = 2, Naam = "Culinaire Dagen", Locatie = "Brugge", Datum = "22-08-2026", MaxDeelnemers = 200, GeregistreerdeDeelnemers = 145 },
    new Evenement { Id = 3, Naam = "Tech Conference", Locatie = "Gent", Datum = "10-09-2026", MaxDeelnemers = 300, GeregistreerdeDeelnemers = 300 }
);
```

### De Controller

Maak een `EvenementController` met volgende endpoints. Elke endpoint moet logging bevatten:

#### 1. Alle evenementen ophalen

Route: `GET /evenementen`

Geef alle evenementen terug als JSON met HTTP-statuscode **200 OK**.

Voeg een logbericht van niveau `Information` toe bij het ontvangen van de request.

#### 2. Eén evenement ophalen op basis van de ID

Route: `GET /evenementen/{id}`

Vind het evenement met de gevraagde ID en geef het terug als JSON met HTTP-statuscode **200 OK**.

Als er geen evenement bestaat met de gevraagde ID, geef dan HTTP-statuscode **404 Not Found** terug zonder body.

Voeg een logbericht van niveau `Information` toe bij het ontvangen van de request en een logbericht van niveau `Warning` als het evenement niet gevonden wordt.

#### 3. Evenementen ophalen op basis van de locatie

Route: `GET /evenementen/locatie/{locatie}`

De routeparameter `{locatie}` stelt de locatie voor.

Zoek evenementen met de gevraagde locatie en geef ze terug als JSON met HTTP-statuscode **200 OK**.

Wordt er geen evenement gevonden voor die locatie, geef dan een lege JSON-lijst `[]` terug met HTTP-statuscode **200 OK** (geen 404 voor lege resultaten).

De query moet ongevoelig zijn voor hoofdletters en kleine letters bij het vergelijken van de locatie.

Voeg een logbericht van niveau `Information` toe bij het ontvangen van de request.

#### 4. Een nieuw evenement aanmaken

Route: `POST /evenementen`

De client stuurt een evenement als JSON in de Request Body. ASP.NET Core zet deze automatisch om naar een `Evenement`-object via Model Binding.

Het endpoint moet:
1. De nieuwe ID berekenen door de hoogste bestaande ID + 1 te nemen;
2. De ID toewijzen aan het nieuwe evenement;
3. Het evenement toevoegen aan de database;
4. Het volledige evenement (inclusief de nieuwe ID) terugsturen met HTTP-statuscode **201 Created**.

De `id` in de request body mag worden genegeerd; je berekent de ID altijd zelf.

Voeg een logbericht van niveau `Information` toe bij het aanmaken van een evenement.

#### 5. Een evenement bijwerken

Route: `PUT /evenementen/{id}`

De routeparameter `{id}` stelt de evenement-ID voor. De client stuurt de nieuwe gegevens van het evenement als JSON in de Request Body via Model Binding.

Het endpoint moet:
1. Het evenement vinden met de gevraagde ID;
2. Als het evenement niet bestaat, HTTP-statuscode **404 Not Found** terugsturen zonder body;
3. Alle Properties van het evenement overschrijven met de nieuwe gegevens uit de Request Body;
4. HTTP-statuscode **204 No Content** terugsturen zonder body.

Voeg een logbericht van niveau `Information` toe bij het ontvangen van de request en een logbericht van niveau `Warning` als het evenement niet gevonden wordt.

#### 6. Een evenement verwijderen

Route: `DELETE /evenementen/{id}`

De routeparameter `{id}` stelt de evenement-ID voor.

Het endpoint moet:
1. Het evenement vinden met de gevraagde ID;
2. Als het evenement niet bestaat, HTTP-statuscode **404 Not Found** terugsturen zonder body;
3. Het evenement verwijderen uit de database;
4. HTTP-statuscode **204 No Content** terugsturen zonder body.

Voeg een logbericht van niveau `Information` toe bij het ontvangen van de request en een logbericht van niveau `Error` als het evenement niet gevonden wordt.

### Dependency Injection

De Controller moet de `DbContext` ontvangen via de constructor (geen `new` in de Controller).

De Controller moet ook `ILogger<EvenementController>` ontvangen via de constructor voor logging.
