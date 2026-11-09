# 05_04

## Leerdoel

Na deze oefening kan je alle concepten uit dit hoofdstuk combineren: een asynchrone `Repository` met volledige CRUD-operaties, complexe LINQ-query's, Dependency Injection, Logging en correcte HTTP-statuscodes in één coherent API-project.

Je leert hoe je een complete applicatie bouwt die alle CRUD-operaties ondersteunt, gefilterde en gesorteerde queries toestaat, logging gebruikt in elke endpoint, aggregatiefuncties combineert met asynchrone methodes, en correct omgaat met niet-bestaande resources — allemaal met een echte database via Entity Framework Core.

## Opdracht

De restaurant **De Gouden Oesters** wil haar bestellingen-API migreren van een in-memory lijst naar een echte PostgreSQL-database met Entity Framework Core. Men wil alle datatoegang laten verlopen via de Repository pattern met asynchrone methodes, Logging invoegen in elke endpoint, en geavanceerde zoek- en rapportagemogelijkheden toevoegen.

Jouw taak is om een `DbContext` aan te maken met Fluent API-configuratie, een asynchrone `BestellingRepository` te implementeren, en een volledige CRUD-API te bouwen met gefilterde queries, logging en correcte HTTP-statuscodes.

### Het Bestelling Model

Maak een Modelklasse `Bestelling` in de map `Models` met volgende Properties:

| Property | Type | Beschrijving |
| -------- | ---- | ------------ |
| Id | int | Unieke identificatie van de bestelling |
| KlantNaam | string | De naam van de klant |
| Tafelnummer | int | Het tafelnnummer |
| Gerechten | string | De bestelde gerechten, gescheiden door koppeltekens (bijv. "Lasagne-Salade") |
| Status | string | De status van de bestelling ("In bereiding", "Klaar", "Afgehaald", "Betaald") |
| TotaalPrijs | double | Het totaalbedrag van de bestelling |

### De DbContext met Fluent API

Maak een nieuwe map genaamd **Data**. Voeg een klasse genaamd **BestellingContext** toe die overerft van `DbContext`.

De context moet:
- een constructor hebben die `DbContextOptions<BestellingContext>` accepteert en doorgeeft aan de basisklasse;
- een `DbSet<Bestelling>` property genaamd **Bestellingen** bevatten;
- de Fluent API (`OnModelCreating`) overschrijven om de volgende configuraties toe te passen:
  - de tabelnaam expliciet instellen op **Bestellingen**;
  - de string-velden `KlantNaam`, `Gerechten` en `Status` verplicht maken (`IsRequired()`) met een maximale lengte van **200** karakters;
  - de `Tafelnummer` verplicht maken (`IsRequired()`);
  - de `TotaalPrijs` verplicht maken (`IsRequired()`) met precisie van 18 decimalen en 2 cijfers na de komma (`HasPrecision(18, 2)`);
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

            entity.Property(p => p.KlantNaam).IsRequired().HasMaxLength(200);
            entity.Property(p => p.Gerechten).IsRequired().HasMaxLength(200);
            entity.Property(p => p.Status).IsRequired().HasMaxLength(50);
            entity.Property(p => p.Tafelnummer).IsRequired();
            entity.Property(p => p.TotaalPrijs).IsRequired().HasPrecision(18, 2);
        });
    }
}
```

### De Interface

Je moet de `IBestellingRepository`-interface implementeren. De interface is als volgt gedefinieerd:

```csharp
using WebApi.Models;

namespace WebApi.Repositories;

public interface IBestellingRepository
{
    // CRUD-methodes
    Task<Bestelling> CreateAsync(Bestelling bestelling);
    Task DeleteAsync(int id);
    Task<List<Bestelling>> GetAllAsync();
    Task<Bestelling?> GetByIdAsync(int id);
    Task UpdateAsync(int id, Bestelling bestelling);

    // Query-methodes
    Task<List<Bestelling>> GetByStatusAsync(string status);
    Task<List<Bestelling>> GetByTafelAsync(int tafelnummer);
    Task<List<Bestelling>> GetSortedByTotaalPrijsAsync(bool descending);
    Task<double> GetTotalRevenueAsync();
    Task<double> GetAverageOrderValueAsync();
    Task<int> CountByStatusAsync(string status);
    Task<bool> HasAnyOrderWithStatusAsync(string status);
}
```

### De asynchrone Repository implementeren

Maak een nieuwe klasse genaamd **BestellingRepository** in de map **Repositories** die de `IBestellingRepository`-interface implementeert.

De repository moet:
- de `DbContext` ontvangen via constructor injection (geen `new` in de klasse);
- alle CRUD-operaties asynchroon uitvoeren met de juiste EF Core async methodes;
- alle query-methodes asynchroon uitvoeren met de juiste EF Core async methodes;
- de `-Async` suffix gebruiken voor de namen van alle asynchrone methodes;
- correct omgaan met niet-bestaande bestellingen bij `UpdateAsync` en `DeleteAsync` (niets doen als de bestelling niet bestaat);
- de `readonly` modifier gebruiken voor de `_context` dependency;
- de `id` in de request body negeren bij `CreateAsync` en zelf de hoogste ID + 1 berekenen.

### CRUD-methodes

#### 1. Alle bestellingen ophalen

```csharp
Task<List<Bestelling>> GetAllAsync();
```

Haal alle bestellingen op uit de database en geef ze terug gesorteerd op Tafelnummer (oplopend).

#### 2. Eén bestelling ophalen op basis van de ID

```csharp
Task<Bestelling?> GetByIdAsync(int id);
```

Zoek de bestelling met de gevraagde ID en geef het terug. Als er geen bestelling bestaat met die ID, geef dan `null` terug.

#### 3. Een nieuwe bestelling aanmaken

```csharp
Task<Bestelling> CreateAsync(Bestelling bestelling);
```

Bereken de nieuwe ID door de hoogste bestaande ID + 1 te nemen. Voeg de nieuwe bestelling toe aan de database en geef het volledige bestelling-object terug (inclusief de gegenereerde ID). De `id` in de request body mag worden genegeerd.

#### 4. Een bestelling bijwerken

```csharp
Task UpdateAsync(int id, Bestelling bestelling);
```

Zoek de bestaande bestelling met de gevraagde ID en werk alle properties bij met de nieuwe gegevens. Als de bestelling niet bestaat, doe dan niets. De `id` in de request body mag worden genegeerd; gebruik de routeparameter `{id}` om de bestelling te vinden.

#### 5. Een bestelling verwijderen

```csharp
Task DeleteAsync(int id);
```

Verwijder de bestelling met de gevraagde ID uit de database. Als de bestelling niet bestaat, doe dan niets.

### Query-methodes

#### 6. Bestellingen ophalen op basis van de status

```csharp
Task<List<Bestelling>> GetByStatusAsync(string status);
```

Zoek alle bestellingen met de gevraagde status en geef ze terug gesorteerd op Tafelnummer (oplopend). De vergelijking moet ongevoelig zijn voor hoofdletters en kleine letters. Wordt er geen bestelling gevonden, geef dan een lege lijst terug.

#### 7. Bestellingen ophalen op basis van het tafelnnummer

```csharp
Task<List<Bestelling>> GetByTafelAsync(int tafelnummer);
```

Zoek alle bestellingen voor het gevraagde tafelnnummer en geef ze terug gesorteerd op TotaalPrijs (oplopend). Wordt er geen bestelling gevonden voor dat tafelnnummer, geef dan een lege lijst terug.

#### 8. Bestellingen sorteren op totaalprijs

```csharp
Task<List<Bestelling>> GetSortedByTotaalPrijsAsync(bool descending);
```

Geef alle bestellingen terug, gesorteerd op TotaalPrijs. Als `descending` true is, geef ze terug in aflopende volgorde (hoogste bedrag eerst). Als `descending` false is, geef ze terug in oplopende volgorde (laagste bedrag eerst).

#### 9. Totale omzet berekenen

```csharp
Task<double> GetTotalRevenueAsync();
```

Bereken de totale omzet door alle TotaalPrijs-waarden op te tellen. Gebruik de `SumAsync()` methode. Wordt er geen bestelling gevonden, geef dan `0` terug.

#### 10. Gemiddelde bestelwaarde berekenen

```csharp
Task<double> GetAverageOrderValueAsync();
```

Bereken het gemiddelde van alle TotaalPrijs-waarden. Gebruik de `AverageAsync()` methode. Wordt er geen bestelling gevonden, geef dan `0` terug.

#### 11. Aantal bestellingen per status tellen

```csharp
Task<int> CountByStatusAsync(string status);
```

Tel het aantal bestellingen met de gevraagde status. De vergelijking moet ongevoelig zijn voor hoofdletters en kleine letters. Wordt er geen bestelling gevonden, geef dan `0` terug.

#### 12. Controleren of een status bestaat

```csharp
Task<bool> HasAnyOrderWithStatusAsync(string status);
```

Controleer of er minstens één bestelling bestaat met de gevraagde status. De vergelijking moet ongevoelig zijn voor hoofdletters en kleine letters. Gebruik `AnyAsync()` voor deze controle. Geef `true` terug als er minstens één bestelling bestaat met die status, anders `false`.

### Seed data toevoegen

Voeg de volgende seed data toe in de `OnModelCreating`-methode van de context:

| Id | KlantNaam | Tafelnummer | Gerechten | Status | TotaalPrijs |
| -- | --------- | ----------- | --------- | ------ | ----------- |
| 1 | Jansen | 5 | Lasagne-Salade | Klaar | 24.50 |
| 2 | De Smet | 12 | Risotto-Wijn | In bereiding | 32.00 |
| 3 | Peeters | 3 | Carpaccio-Koffie | Afgehaald | 18.75 |
| 4 | Willems | 8 | Gegrilde Kabeljauw-Dessert | Betaald | 45.00 |
| 5 | Claes | 1 | Stoofvlees-Bier | In bereiding | 28.50 |
| 6 | Dubois | 15 | Moules-Frites | Klaar | 22.00 |

### De Controller

Maak een `BestellingController` met volgende endpoints. Elke endpoint moet logging bevatten en de `IBestellingRepository` ontvangen via de constructor (geen `new` in de Controller).

#### 1. Alle bestellingen ophalen

Route: `GET /bestellingen`

Geef alle bestellingen terug als JSON met HTTP-statuscode **200 OK**.

Voeg een logbericht van niveau `Information` toe bij het ontvangen van de request.

#### 2. Eén bestelling ophalen op basis van de ID

Route: `GET /bestellingen/{id}`

Vind de bestelling met de gevraagde ID en geef het terug als JSON met HTTP-statuscode **200 OK**.

Als er geen bestelling bestaat met de gevraagde ID, geef dan HTTP-statuscode **404 Not Found** terug zonder body.

Voeg een logbericht van niveau `Information` toe bij het ontvangen van de request en een logbericht van niveau `Warning` als de bestelling niet gevonden wordt.

#### 3. Bestellingen ophalen op basis van de status

Route: `GET /bestellingen/status/{status}`

De routeparameter `{status}` stelt de status voor.

Zoek bestellingen met de gevraagde status en geef ze terug als JSON met HTTP-statuscode **200 OK**.

Wordt er geen bestelling gevonden voor die status, geef dan een lege JSON-lijst `[]` terug met HTTP-statuscode **200 OK** (geen 404 voor lege resultaten).

Voeg een logbericht van niveau `Information` toe bij het ontvangen van de request.

#### 4. Bestellingen ophalen op basis van het tafelnnummer

Route: `GET /bestellingen/tafel/{tafelnummer}`

De routeparameter `{tafelnummer}` stelt het tafelnnummer voor.

Zoek bestellingen voor het gevraagde tafelnnummer en geef ze terug als JSON met HTTP-statuscode **200 OK**.

Wordt er geen bestelling gevonden voor dat tafelnnummer, geef dan een lege JSON-lijst `[]` terug met HTTP-statuscode **200 OK** (geen 404 voor lege resultaten).

Voeg een logbericht van niveau `Information` toe bij het ontvangen van de request.

#### 5. Bestellingen sorteren op totaalprijs

Route: `GET /bestellingen/sorteren/oprijs?descending=true`

De query parameter `descending` bepaalt de sorteervolgorde. Als `descending` true is, geef ze terug in aflopende volgorde (hoogste bedrag eerst). Als `descending` false of niet opgegeven, geef ze terug in oplopende volgorde (laagste bedrag eerst).

Geef alle bestellingen terug als JSON met HTTP-statuscode **200 OK**.

Voeg een logbericht van niveau `Information` toe bij het ontvangen van de request.

#### 6. Totale omzet ophalen

Route: `GET /bestellingen/omzet`

Geef de totale omzet terug als JSON met HTTP-statuscode **200 OK**. De response moet een object zijn met een property `totalRevenue` van type `double`.

Voeg een logbericht van niveau `Information` toe bij het ontvangen van de request.

#### 7. Gemiddelde bestelwaarde ophalen

Route: `GET /bestellingen/gemiddelde`

Geef de gemiddelde bestelwaarde terug als JSON met HTTP-statuscode **200 OK**. De response moet een object zijn met een property `averageOrderValue` van type `double`.

Voeg een logbericht van niveau `Information` toe bij het ontvangen van de request.

#### 8. Aantal bestellingen per status tellen

Route: `GET /bestellingen/tellen/status/{status}`

De routeparameter `{status}` stelt de status voor.

Geef het aantal bestellingen met die status terug als JSON met HTTP-statuscode **200 OK**. De response moet een object zijn met een property `count` van type `int`.

Wordt er geen bestelling gevonden voor die status, geef dan `{"count": 0}` terug met HTTP-statuscode **200 OK** (geen 404 voor lege resultaten).

Voeg een logbericht van niveau `Information` toe bij het ontvangen van de request.

#### 9. Controleren of een status bestaat

Route: `GET /bestellingen/exists/status/{status}`

De routeparameter `{status}` stelt de status voor.

Controleer of er minstens één bestelling bestaat met die status en geef het resultaat terug als JSON met HTTP-statuscode **200 OK**. De response moet een object zijn met een property `exists` van type `bool`.

Voeg een logbericht van niveau `Information` toe bij het ontvangen van de request.

#### 10. Een nieuwe bestelling aanmaken

Route: `POST /bestellingen`

De client stuurt een bestelling als JSON in de Request Body. ASP.NET Core zet deze automatisch om naar een `Bestelling`-object via Model Binding.

Het endpoint moet:
1. De nieuwe ID berekenen door de hoogste bestaande ID + 1 te nemen;
2. De ID toewijzen aan het nieuwe object;
3. De bestelling toevoegen aan de database;
4. Het volledige object (inclusief de nieuwe ID) terugsturen met HTTP-statuscode **201 Created**.

De `id` in de request body mag worden genegeerd; je berekent de ID altijd zelf.

Voeg een logbericht van niveau `Information` toe bij het aanmaken van een bestelling.

#### 11. Een bestelling bijwerken

Route: `PUT /bestellingen/{id}`

De routeparameter `{id}` stelt de bestel-ID voor. De client stuurt de nieuwe gegevens van de bestelling als JSON in de Request Body via Model Binding.

Het endpoint moet:
1. De bestelling vinden met de gevraagde ID;
2. Als de bestelling niet bestaat, HTTP-statuscode **404 Not Found** terugsturen zonder body;
3. Alle Properties van de bestelling overschrijven met de nieuwe gegevens uit de Request Body;
4. HTTP-statuscode **204 No Content** terugsturen zonder body.

De `id` in de request body mag worden genegeerd; gebruik de routeparameter `{id}` om de bestelling te vinden.

Voeg een logbericht van niveau `Information` toe bij het ontvangen van de request en een logbericht van niveau `Warning` als de bestelling niet gevonden wordt.

#### 12. Een bestelling verwijderen

Route: `DELETE /bestellingen/{id}`

De routeparameter `{id}` stelt de bestel-ID voor.

Het endpoint moet:
1. De bestelling vinden met de gevraagde ID;
2. Als de bestelling niet bestaat, HTTP-statuscode **404 Not Found** terugsturen zonder body;
3. De bestelling verwijderen uit de database;
4. HTTP-statuscode **204 No Content** terugsturen zonder body.

Voeg een logbericht van niveau `Information` toe bij het ontvangen van de request en een logbericht van niveau `Error` als de bestelling niet gevonden wordt.

### Dependency Injection

Registreer de repository in `Program.cs` met `AddScoped`:

```csharp
builder.Services.AddScoped<IBestellingRepository, BestellingRepository>();
```
