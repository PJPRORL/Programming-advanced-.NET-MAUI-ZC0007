# 06_04

## Leerdoel

Na deze oefening kan je alle concepten uit dit hoofdstuk combineren: drie modellen met een 1:N-relatie en een N:M-relatie via een tussentabel, Fluent API-configuratie, data seeding, asynchrone Repository-implementatie, en een volledige CRUD-API met correcte HTTP-statuscodes in één coherent project.

Je leert hoe je een complete webshop-dataarchitectuur bouwt met Entity Framework Core, hoe je alle relaties configureert met de Fluent API, hoe je seed data toevoegt met `HasData`, en hoe je een asynchrone repository en controller implementeert die via de database-relaties navigeert.

## Opdracht

De webshop **WebShop Vlaanderen** wil haar bestellingen-API migreren van een in-memory lijst naar een echte PostgreSQL-database met Entity Framework Core. Men wil de volledige webshop-dataarchitectuur configureren met alle relaties tussen tabellen, seed data toevoegen, en een asynchrone repository bouwen voor het beheren van bestellingen inclusief hun gerelateerde producten.

Jouw taak is om de Fluent API-configuratie voor alle relaties te implementeren, seed data toe te voegen, de asynchrone `BestellingRepository` te bouwen, en een volledige CRUD-API te bouwen met correcte HTTP-statuscodes.

### De modellen

De webshop heeft de volgende modellen:

**Klant:**

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

**Product:**

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

**Bestelling:**

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

**OrderLijn (tussentabel voor N:M-relatie tussen Bestelling en Product):**

| Property | Type | Beschrijving |
| -------- | ---- | ------------ |
| Id | int | Unieke identificatie van de orderlijn |
| BestellingId | int | Foreign Key naar de bestelling |
| Bestelling | Bestelling? | Navigation Property naar de bestelling |
| ProductId | int | Foreign Key naar het product |
| Product | Product? | Navigation Property naar het product |
| Aantal | double | Het aantal bestelde stuks van dit product |

```csharp
namespace WebApi.Models;

public class OrderLijn
{
    public int Id { get; set; }
    public int BestellingId { get; set; }
    public Bestelling? Bestelling { get; set; } = default!;
    public int ProductId { get; set; }
    public Product? Product { get; set; } = default!;
    public double Aantal { get; set; }
}
```

### De DbContext met Fluent API-configuratie

Maak een nieuwe map genaamd **Data**. Voeg een klasse genaamd **WebshopContext** toe die overerft van `DbContext`.

De context moet:
- een constructor hebben die `DbContextOptions<WebshopContext>` accepteert en doorgeeft aan de basisklasse;
- een `DbSet<Klant>` property genaamd **Klanten** bevatten;
- een `DbSet<Product>` property genaamd **Producten** bevatten;
- een `DbSet<Bestelling>` property genaamd **Bestellingen** bevatten;
- een `DbSet<OrderLijn>` property genaamd **OrderLijnen** bevatten;
- de Fluent API (`OnModelCreating`) overschrijven om de volgende configuraties toe te passen:
  - de tabelnamen expliciet instellen op **Klant**, **Product**, **Bestelling** en **OrderLijn**;
  - de 1:N-relatie tussen Klant en Bestelling configureren:
    - `HasOne(p => p.Klant)` — een bestelling heeft één specifieke klant;
    - `WithMany(x => x.Bestellingen)` — die ene klant kan meerdere bestellingen hebben;
    - `HasForeignKey(y => y.KlantId)` — de Foreign Key is KlantId;
    - `OnDelete(DeleteBehavior.Restrict)` — blokkeer het verwijderen van een klant zolang deze nog bestellingen heeft;
    - `.IsRequired()` — de relatie is verplicht.
  - de twee 1:N-relaties voor de OrderLijn tussentabel configureren (samen vormen ze de N:M-relatie):
    - `HasOne(p => p.Bestelling)` — een orderlijn hoort bij één bestelling;
    - `WithMany(x => x.OrderLijnen)` — die ene bestelling kan meerdere orderlijnen hebben;
    - `HasForeignKey(y => y.BestellingId)` — de Foreign Key is BestellingId;
    - `OnDelete(DeleteBehavior.Restrict)` — blokkeer het verwijderen van een bestelling zolang deze nog orderlijnen heeft;
    - `.IsRequired()` — de relatie is verplicht.
    - `HasOne(p => p.Product)` — een orderlijn hoort bij één product;
    - `WithMany(x => x.OrderLijnen)` — dat ene product kan in meerdere orderlijnen voorkomen;
    - `HasForeignKey(y => y.ProductId)` — de Foreign Key is ProductId;
    - `OnDelete(DeleteBehavior.Restrict)` — blokkeer het verwijderen van een product zolang dit in orderlijnen voorkomt;
    - `.IsRequired()` — de relatie is verplicht.
- de seed data toevoegen via `HasData`:

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

**OrderLijnen:**

| Id | Aantal | BestellingId | ProductId |
| -- | ------ | ------------ | --------- |
| 1 | 3 | 1 | 1 |
| 2 | 7 | 1 | 2 |
| 3 | 4 | 2 | 1 |
| 4 | 1 | 2 | 2 |
| 5 | 2 | 3 | 1 |
| 6 | 3 | 4 | 1 |
| 7 | 1 | 4 | 3 |
| 8 | 2 | 5 | 1 |
| 9 | 6 | 5 | 2 |
| 10 | 10 | 5 | 3 |

### De Interface

Maak een nieuwe map genaamd **Repositories**. Voeg een interface genaamd **IBestellingRepository** toe:

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
    Task<List<Bestelling>> GetByKlantIdAsync(int klantId);
    Task<List<OrderLijn>> GetOrderLijnenAsync(int bestellingId);
    Task<double> GetTotaalPrijsAsync(int bestellingId);
}
```

### De asynchrone Repository implementeren

Maak een nieuwe klasse genaamd **BestellingRepository** in de map **Repositories** die de `IBestellingRepository`-interface implementeert.

De repository moet:
- de `DbContext` ontvangen via constructor injection (geen `new` in de klasse);
- alle CRUD-operaties asynchroon uitvoeren met de juiste EF Core async methodes;
- de `-Async` suffix gebruiken voor de namen van alle asynchrone methodes;
- correct omgaan met niet-bestaande bestellingen bij `UpdateAsync` en `DeleteAsync` (niets doen als de bestelling niet bestaat);
- de `readonly` modifier gebruiken voor de `_context` dependency;
- de `id` in de request body negeren bij `CreateAsync` en zelf de hoogste ID + 1 berekenen.

#### CRUD-methodes

**1. Alle bestellingen ophalen**

```csharp
Task<List<Bestelling>> GetAllAsync();
```

Haal alle bestellingen op uit de database en geef ze terug gesorteerd op KlantId (oplopend).

**2. Eén bestelling ophalen op basis van de ID**

```csharp
Task<Bestelling?> GetByIdAsync(int id);
```

Zoek de bestelling met de gevraagde ID en geef het terug. Als er geen bestelling bestaat met die ID, geef dan `null` terug.

**3. Een nieuwe bestelling aanmaken**

```csharp
Task<Bestelling> CreateAsync(Bestelling bestelling);
```

Bereken de nieuwe ID door de hoogste bestaande ID + 1 te nemen. Voeg de nieuwe bestelling toe aan de database en geef het volledige bestelling-object terug (inclusief de gegenereerde ID).

**4. Een bestelling bijwerken**

```csharp
Task UpdateAsync(int id, Bestelling bestelling);
```

Zoek de bestaande bestelling met de gevraagde ID en werk alle properties bij met de nieuwe gegevens. Als de bestelling niet bestaat, doe dan niets. De `id` in de request body mag worden genegeerd; gebruik de routeparameter `{id}` om de bestelling te vinden.

**5. Een bestelling verwijderen**

```csharp
Task DeleteAsync(int id);
```

Verwijder de bestelling met de gevraagde ID uit de database. Als de bestelling niet bestaat, doe dan niets.

#### Query-methodes

**6. Bestellingen ophalen op basis van de klant**

```csharp
Task<List<Bestelling>> GetByKlantIdAsync(int klantId);
```

Zoek alle bestellingen voor de gevraagde klant en geef ze terug gesorteerd op Id (oplopend). Wordt er geen bestelling gevonden voor die klant, geef dan een lege lijst terug.

**7. Orderlijnen ophalen van een bestelling**

```csharp
Task<List<OrderLijn>> GetOrderLijnenAsync(int bestellingId);
```

Zoek alle orderlijnen voor de gevraagde bestelling en geef ze terug gesorteerd op ProductId (oplopend). Wordt er geen bestelling gevonden voor die ID, geef dan een lege lijst terug.

**8. Totale prijs van een bestelling berekenen**

```csharp
Task<double> GetTotaalPrijsAsync(int bestellingId);
```

Bereken de totale prijs van de bestelling door de prijs van elk product in de orderlijnen te vermenigvuldigen met het aantal en deze op te tellen. Gebruik `SumAsync()` met een LINQ-query die voor elke orderlijn `Product.Prijs * Aantal` berekent. Wordt er geen bestelling gevonden, geef dan `0` terug.

### De Controller

Maak een `BestellingController` met volgende endpoints. Elke endpoint moet de `IBestellingRepository` ontvangen via de constructor (geen `new` in de Controller).

#### 1. Alle bestellingen ophalen

Route: `GET /bestellingen`

Geef alle bestellingen terug als JSON met HTTP-statuscode **200 OK**.

#### 2. Eén bestelling ophalen op basis van de ID

Route: `GET /bestellingen/{id}`

Vind de bestelling met de gevraagde ID en geef het terug als JSON met HTTP-statuscode **200 OK**.

Als er geen bestelling bestaat met de gevraagde ID, geef dan HTTP-statuscode **404 Not Found** terug zonder body.

#### 3. Bestellingen ophalen op basis van de klant

Route: `GET /bestellingen/klant/{klantId}`

De routeparameter `{klantId}` stelt de klant-ID voor.

Zoek bestellingen voor de gevraagde klant en geef ze terug als JSON met HTTP-statuscode **200 OK**.

Wordt er geen bestelling gevonden voor die klant, geef dan een lege JSON-lijst `[]` terug met HTTP-statuscode **200 OK** (geen 404 voor lege resultaten).

#### 4. Orderlijnen ophalen van een bestelling

Route: `GET /bestellingen/{id}/orderlijnen`

De routeparameter `{id}` stelt de bestel-ID voor.

Geef alle orderlijnen van de bestelling terug als JSON met HTTP-statuscode **200 OK**.

Wordt er geen bestelling gevonden voor die ID, geef dan een lege JSON-lijst `[]` terug met HTTP-statuscode **200 OK** (geen 404 voor lege resultaten).

#### 5. Totale prijs van een bestelling ophalen

Route: `GET /bestellingen/{id}/prijs`

De routeparameter `{id}` stelt de bestel-ID voor.

Geef de totale prijs van de bestelling terug als JSON met HTTP-statuscode **200 OK**. De response moet een object zijn met een property `totaalPrijs` van type `double`.

Wordt er geen bestelling gevonden voor die ID, geef dan `{"totaalPrijs": 0}` terug met HTTP-statuscode **200 OK** (geen 404 voor lege resultaten).

#### 6. Een nieuwe bestelling aanmaken

Route: `POST /bestellingen`

De client stuurt een bestelling als JSON in de Request Body. ASP.NET Core zet deze automatisch om naar een `Bestelling`-object via Model Binding.

Het endpoint moet:
1. De nieuwe ID berekenen door de hoogste bestaande ID + 1 te nemen;
2. De ID toewijzen aan het nieuwe object;
3. De bestelling toevoegen aan de database;
4. Het volledige object (inclusief de nieuwe ID) terugsturen met HTTP-statuscode **201 Created**.

De `id` in de request body mag worden genegeerd; je berekent de ID altijd zelf.

#### 7. Een bestelling bijwerken

Route: `PUT /bestellingen/{id}`

De routeparameter `{id}` stelt de bestel-ID voor. De client stuurt de nieuwe gegevens van de bestelling als JSON in de Request Body via Model Binding.

Het endpoint moet:
1. De bestelling vinden met de gevraagde ID;
2. Als de bestelling niet bestaat, HTTP-statuscode **404 Not Found** terugsturen zonder body;
3. Alle Properties van de bestelling overschrijven met de nieuwe gegevens uit de Request Body;
4. HTTP-statuscode **204 No Content** terugsturen zonder body.

De `id` in de request body mag worden genegeerd; gebruik de routeparameter `{id}` om de bestelling te vinden.

#### 8. Een bestelling verwijderen

Route: `DELETE /bestellingen/{id}`

De routeparameter `{id}` stelt de bestel-ID voor.

Het endpoint moet:
1. De bestelling vinden met de gevraagde ID;
2. Als de bestelling niet bestaat, HTTP-statuscode **404 Not Found** terugsturen zonder body;
3. De bestelling verwijderen uit de database;
4. HTTP-statuscode **204 No Content** terugsturen zonder body.

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

**OrderLijnen:**

| Id | Aantal | BestellingId | ProductId |
| -- | ------ | ------------ | --------- |
| 1 | 3 | 1 | 1 |
| 2 | 7 | 1 | 2 |
| 3 | 4 | 2 | 1 |
| 4 | 1 | 2 | 2 |
| 5 | 2 | 3 | 1 |
| 6 | 3 | 4 | 1 |
| 7 | 1 | 4 | 3 |
| 8 | 2 | 5 | 1 |
| 9 | 6 | 5 | 2 |
| 10 | 10 | 5 | 3 |

### Dependency Injection

Registreer de repository in `Program.cs` met `AddScoped`:

```csharp
builder.Services.AddScoped<IBestellingRepository, BestellingRepository>();
```
