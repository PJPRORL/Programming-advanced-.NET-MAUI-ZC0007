# 05_02

## Leerdoel

Na deze oefening kan je een asynchrone `Repository` implementeren met extra query-methodes die gefilterde en gesorteerde resultaten teruggeven.

Je leert hoe je LINQ-query's combineert met asynchrone EF Core methodes, hoe je `Where`, `OrderBy`, `OrderByDescending`, `AnyAsync` en `CountAsync` gebruikt in een repository, en hoe je case-insensitive vergelijkingen uitvoert in database-query's.

## Opdracht

De fietsenwinkel **Fietsspecialist Gent** wil haar inventory-API migreren van een in-memory lijst naar een echte PostgreSQL-database met Entity Framework Core. Na de basis-CRUD-operaties wil men ook gefilterde en gesorteerde zoekopdrachten ondersteunen.

Jouw taak is om de asynchrone `FietsRepository`-klasse te implementeren die een interface met zowel CRUD- als query-methodes implementeert.

### Het Fiets Model

Maak een Modelklasse `Fiets` in de map `Models` met volgende Properties:

| Property | Type | Beschrijving |
| -------- | ---- | ------------ |
| Id | int | Unieke identificatie van de fiets |
| Merk | string | Het merk van de fiets |
| Type | string | Het type fiets (bijv. "Mountainbike", "Racefiets", "Stadsfiets") |
| Kleur | string | De kleur van de fiets |
| Prijs | double | De prijs van de fiets |
| Aantal | int | Het aantal beschikbare stuks op voorraad |

### De DbContext

De context is reeds aangemaakt en geregistreerd. Gebruik de volgende context:

```csharp
using Microsoft.EntityFrameworkCore;
using WebApi.Models;

namespace WebApi.Data;

public class FietsCatalogusContext : DbContext
{
    public FietsCatalogusContext(DbContextOptions<FietsCatalogusContext> options)
        : base(options) { }

    public DbSet<Fiets> Fietsen { get; set; }
}
```

### De Interface

Je moet de `IFietsRepository`-interface implementeren. De interface is als volgt gedefinieerd:

```csharp
using WebApi.Models;

namespace WebApi.Repositories;

public interface IFietsRepository
{
    // CRUD-methodes
    Task<Fiets> CreateAsync(Fiets fiets);
    Task DeleteAsync(int id);
    Task<List<Fiets>> GetAllAsync();
    Task<Fiets?> GetByIdAsync(int id);
    Task UpdateAsync(int id, Fiets fiets);

    // Query-methodes
    Task<List<Fiets>> GetByMerkAsync(string merk);
    Task<List<Fiets>> GetByTypeAsync(string type);
    Task<List<Fiets>> GetByKleurAsync(string kleur);
    Task<List<Fiets>> GetInStockAsync();
    Task<List<Fiets>> GetSortedByPrijsAsync(bool descending);
    Task<bool> ExistsByMerkAsync(string merk);
    Task<int> CountByTypeAsync(string type);
}
```

### De asynchrone Repository implementeren

Maak een nieuwe klasse genaamd **FietsRepository** in de map **Repositories** die de `IFietsRepository`-interface implementeert.

De repository moet:
- de `DbContext` ontvangen via constructor injection (geen `new` in de klasse);
- alle CRUD-operaties asynchroon uitvoeren met de juiste EF Core async methodes;
- alle query-methodes asynchroon uitvoeren met de juiste EF Core async methodes;
- de `-Async` suffix gebruiken voor de namen van alle asynchrone methodes;
- correct omgaan met niet-bestaande fietsen bij `UpdateAsync` en `DeleteAsync` (niets doen als de fiets niet bestaat);
- de `readonly` modifier gebruiken voor de `_context` dependency.

### CRUD-methodes

#### 1. Alle fietsen ophalen

```csharp
Task<List<Fiets>> GetAllAsync();
```

Haal alle fietsen op uit de database en geef ze terug gesorteerd op merk (oplopend).

#### 2. Eén fiets ophalen op basis van de ID

```csharp
Task<Fiets?> GetByIdAsync(int id);
```

Zoek de fiets met de gevraagde ID en geef hem terug. Als er geen fiets bestaat met die ID, geef dan `null` terug.

#### 3. Een nieuwe fiets aanmaken

```csharp
Task<Fiets> CreateAsync(Fiets fiets);
```

Voeg de nieuwe fiets toe aan de database en geef het volledige fiets-object terug (inclusief de gegenereerde ID).

#### 4. Een fiets bijwerken

```csharp
Task UpdateAsync(int id, Fiets fiets);
```

Zoek de bestaande fiets met de gevraagde ID en werk alle properties bij met de nieuwe gegevens. Als de fiets niet bestaat, doe dan niets.

#### 5. Een fiets verwijderen

```csharp
Task DeleteAsync(int id);
```

Verwijder de fiets met de gevraagde ID uit de database. Als de fiets niet bestaat, doe dan niets.

### Query-methodes

#### 6. Fietsen ophalen op basis van het merk

```csharp
Task<List<Fiets>> GetByMerkAsync(string merk);
```

Zoek alle fietsen met het gevraagde merk en geef ze terug gesorteerd op prijs (oplopend). De vergelijking moet ongevoelig zijn voor hoofdletters en kleine letters. Wordt er geen fiets gevonden, geef dan een lege lijst terug.

#### 7. Fietsen ophalen op basis van het type

```csharp
Task<List<Fiets>> GetByTypeAsync(string type);
```

Zoek alle fietsen met het gevraagde type en geef ze terug gesorteerd op prijs (oplopend). De vergelijking moet ongevoelig zijn voor hoofdletters en kleine letters. Wordt er geen fiets gevonden, geef dan een lege lijst terug.

#### 8. Fietsen ophalen op basis van de kleur

```csharp
Task<List<Fiets>> GetByKleurAsync(string kleur);
```

Zoek alle fietsen met de gevraagde kleur en geef ze terug gesorteerd op prijs (oplopend). De vergelijking moet ongevoelig zijn voor hoofdletters en kleine letters. Wordt er geen fiets gevonden, geef dan een lege lijst terug.

#### 9. Fietsen met voorraad ophalen

```csharp
Task<List<Fiets>> GetInStockAsync();
```

Zoek alle fietsen die op voorraad zijn (Aantal > 0) en geef ze terug gesorteerd op prijs (oplopend). Wordt er geen fiets gevonden, geef dan een lege lijst terug.

#### 10. Fietsen sorteren op prijs

```csharp
Task<List<Fiets>> GetSortedByPrijsAsync(bool descending);
```

Geef alle fietsen terug, gesorteerd op prijs. Als `descending` true is, geef ze terug in aflopende volgorde (hoogste prijs eerst). Als `descending` false is, geef ze terug in oplopende volgorde (laagste prijs eerst).

#### 11. Controleren of een merk bestaat

```csharp
Task<bool> ExistsByMerkAsync(string merk);
```

Controleer of er minstens één fiets bestaat met het gevraagde merk. De vergelijking moet ongevoelig zijn voor hoofdletters en kleine letters. Geef `true` terug als er minstens één fiets bestaat met dat merk, anders `false`.

#### 12. Aantal fietsen per type tellen

```csharp
Task<int> CountByTypeAsync(string type);
```

Tel het aantal fietsen met het gevraagde type. De vergelijking moet ongevoelig zijn voor hoofdletters en kleine letters. Wordt er geen fiets gevonden, geef dan `0` terug.

### Seed data toevoegen

Voeg de volgende seed data toe in de `OnModelCreating`-methode van de `DbContext`:

| Id | Merk | Type | Kleur | Prijs | Aantal |
| -- | ---- | ---- | ----- | ----- | ------ |
| 1 | Trek | Mountainbike | Zwart | 1200.00 | 5 |
| 2 | Specialized | Racefiets | Rood | 2500.00 | 3 |
| 3 | Gazelle | Stadsfiets | Blauw | 800.00 | 12 |
| 4 | Trek | Racefiets | Groen | 1800.00 | 0 |
| 5 | Giant | Mountainbike | Zwart | 1500.00 | 7 |
| 6 | Koga | Stadsfiets | Wit | 950.00 | 8 |

### Dependency Injection

Registreer de repository in `Program.cs` met `AddScoped`:

```csharp
builder.Services.AddScoped<IFietsRepository, FietsRepository>();
```
