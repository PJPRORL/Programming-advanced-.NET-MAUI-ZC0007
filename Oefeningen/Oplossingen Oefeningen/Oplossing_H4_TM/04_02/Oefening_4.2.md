# 04_02

## Leerdoel

Na deze oefening kan je een `DbContext` configureren met de Fluent API (`OnModelCreating`) om databasetabellen te finetunen, en een volledige CRUD-API bouwen met Entity Framework Core.

Je leert hoe je string-lengtes en vereiste velden configureert met de Fluent API, hoe je de databasecontext registreert in DI, en hoe je alle CRUD-operaties (Create, Read, Update, Delete) implementeert met correcte HTTP-statuscodes.

## Opdracht

De restaurant **De Gouden Oesters** wil haar bestellingen-API migreren van een in-memory lijst naar een echte PostgreSQL-database met Entity Framework Core.

Jouw taak is om een `DbContext` aan te maken met Fluent API-configuratie, alle CRUD-endpoints te implementeren, en de database te synchroniseren via migraties.

### Het Bestelling Model

Maak een Modelklasse `Bestelling` in de map `Models` met volgende Properties:

| Property | Type | Beschrijving |
| -------- | ---- | ------------ |
| Id | int | Unieke identificatie van de bestelling |
| Naam | string | De naam van de klant |
| Tafelnummer | int | Het tafelnnummer |
| Gerechten | string | De bestelde gerechten, gescheiden door koppeltekens (bijv. "Lasagne-Salade") |
| Status | string | De status van de bestelling |

### De DbContext met Fluent API

Maak een nieuwe map genaamd **Data**. Voeg een klasse genaamd **BestellingContext** toe die overerft van `DbContext`.

De context moet:
- een constructor hebben die `DbContextOptions<BestellingContext>` accepteert en doorgeeft aan de basisklasse;
- een `DbSet<Bestelling>` property genaamd **Bestellingen** bevatten;
- de Fluent API (`OnModelCreating`) overschrijven om de volgende configuraties toe te passen:
  - de tabelnaam expliciet instellen op **Bestellingen**;
  - de string-velden `Naam`, `Gerechten` en `Status` verplicht maken (`IsRequired()`) met een maximale lengte van **100** karakters;
- de namespace **WebApi.Data** gebruiken.

```csharp
using Microsoft.EntityFrameworkCore;
using WebApi.Models;

namespace WebApi.Data;

public class BestellingContext : DbContext
{
    public BestellingContext(DbContextOptions<BestellingContext> options)
        : base(options) { }

    public DbSet<Bestelling> Bestellingen { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Bestelling>(entity =>
        {
            entity.ToTable("Bestellingen");

            entity.Property(p => p.Naam).IsRequired().HasMaxLength(100);
            entity.Property(p => p.Gerechten).IsRequired().HasMaxLength(100);
            entity.Property(p => p.Status).IsRequired().HasMaxLength(100);
        });
    }
}
```

### De Database registreren in Program.cs

Registreer de `DbContext` in `Program.cs` met `AddDbContext` en koppel deze aan de PostgreSQL-provider (Npgsql). Gebruik de connection string met naam **PostgresConnection** uit `appsettings.json`.

### Migratie aanmaken en uitvoeren

Maak een migratie aan met de naam **InitialCreate** en voer deze uit om de database en tabellen te genereren.

### De Controller

Maak een `BestellingController` met volgende endpoints:

#### 1. Alle bestellingen ophalen

Route: `GET /bestellingen`

Geef alle bestellingen terug als JSON met HTTP-statuscode **200 OK**.

#### 2. Eén bestelling ophalen op basis van de ID

Route: `GET /bestellingen/{id}`

Vind de bestelling met de gevraagde ID en geef het terug als JSON met HTTP-statuscode **200 OK**.

Als er geen bestelling bestaat met de gevraagde ID, geef dan HTTP-statuscode **404 Not Found** terug zonder body.

#### 3. Een nieuwe bestelling aanmaken

Route: `POST /bestellingen`

De client stuurt een bestelling als JSON in de Request Body. ASP.NET Core zet deze automatisch om naar een `Bestelling`-object via Model Binding.

Het endpoint moet:
1. De nieuwe bestelling toevoegen aan de database;
2. De volledige bestelling (inclusief de gegenereerde ID) terugsturen met HTTP-statuscode **201 Created**.

De `id` in de request body mag worden genegeerd; Entity Framework genereert de ID automatisch.

#### 4. Een bestelling bijwerken

Route: `PUT /bestellingen/{id}`

De routeparameter `{id}` stelt de bestel-ID voor. De client stuurt de nieuwe gegevens van de bestelling als JSON in de Request Body via Model Binding.

Het endpoint moet:
1. De bestelling vinden met de gevraagde ID;
2. Als de bestelling niet bestaat, HTTP-statuscode **404 Not Found** terugsturen zonder body;
3. Alle Properties van de bestelling overschrijven met de nieuwe gegevens uit de Request Body;
4. HTTP-statuscode **204 No Content** terugsturen zonder body.

#### 5. Een bestelling verwijderen

Route: `DELETE /bestellingen/{id}`

De routeparameter `{id}` stelt de bestel-ID voor.

Het endpoint moet:
1. De bestelling vinden met de gevraagde ID;
2. Als de bestelling niet bestaat, HTTP-statuscode **404 Not Found** terugsturen zonder body;
3. De bestelling verwijderen uit de database;
4. HTTP-statuscode **204 No Content** terugsturen zonder body.

De controller moet de `DbContext` ontvangen via de constructor (geen `new` in de Controller).
