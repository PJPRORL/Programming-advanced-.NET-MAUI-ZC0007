# 05_01

## Leerdoel

Na deze oefening kan je een asynchrone `Repository` implementeren die communiceert met een `DbContext` via Entity Framework Core.

Je leert hoe je de `ILaptopRepository`-interface omzet naar een concrete asynchrone implementatie, hoe je `async` en `await` correct gebruikt, en hoe je de asynchrone methodes van EF Core aanroept om database-operaties uit te voeren.

## Opdracht

De bibliotheek **Stadsbibliotheek Noord** wil haar catalogus-API migreren van een in-memory lijst naar een echte PostgreSQL-database met Entity Framework Core. Men heeft reeds de `DbContext` en de `ILaptopRepository`-interface klaar, maar de concrete implementatie van de asynchrone repository ontbreekt nog.

Jouw taak is om de asynchrone `LaptopRepository`-klasse te implementeren die de `ILaptopRepository`-interface implementeert.

### Het Laptop Model

Gebruik het bestaande `Laptop`-Model in de map `Models`:

| Property | Type | Beschrijving |
| -------- | ---- | ------------ |
| Id | int | Unieke identificatie van de laptop |
| Merk | string | Het merk van de laptop |
| Processor | string | Het type processor |
| RamInGB | int | De hoeveelheid RAM in GB |
| Prijs | double | De prijs van de laptop |
| GPU | string | Het type GPU |

### De DbContext

De context is reeds aangemaakt en geregistreerd. Gebruik de volgende context:

```csharp
using Microsoft.EntityFrameworkCore;
using WebApi.Models;

namespace WebApi.Data;

public class LaptopCatalogusContext : DbContext
{
    public LaptopCatalogusContext(DbContextOptions<LaptopCatalogusContext> options)
        : base(options) { }

    public DbSet<Laptop> Laptops { get; set; }
}
```

### De Interface

Je moet de `ILaptopRepository`-interface implementeren. De interface is als volgt gedefinieerd:

```csharp
using WebApi.Models;

namespace WebApi.Repositories;

public interface ILaptopRepository
{
    Task<Laptop> CreateAsync(Laptop laptop);
    Task DeleteAsync(int id);
    Task<List<Laptop>> GetAllAsync();
    Task<Laptop?> GetByIdAsync(int id);
    Task UpdateAsync(int id, Laptop laptop);
}
```

### De asynchrone Repository implementeren

Maak een nieuwe klasse genaamd **LaptopRepository** in de map **Repositories** die de `ILaptopRepository`-interface implementeert.

De repository moet:
- de `DbContext` ontvangen via constructor injection (geen `new` in de klasse);
- alle CRUD-operaties asynchroon uitvoeren met de juiste EF Core async methodes;
- de `-Async` suffix gebruiken voor de namen van alle asynchrone methodes;
- correct omgaan met niet-bestaande laptops bij `UpdateAsync` en `DeleteAsync` (niets doen als de laptop niet bestaat);
- de `readonly` modifier gebruiken voor de `_context` dependency.

De interface vereist de volgende methodes:

#### 1. Alle laptops ophalen

```csharp
Task<List<Laptop>> GetAllAsync();
```

Haal alle laptops op uit de database en geef ze terug gesorteerd op merk (oplopend).

#### 2. Eén laptop ophalen op basis van de ID

```csharp
Task<Laptop?> GetByIdAsync(int id);
```

Zoek de laptop met de gevraagde ID en geef hem terug. Als er geen laptop bestaat met die ID, geef dan `null` terug.

#### 3. Een nieuwe laptop aanmaken

```csharp
Task<Laptop> CreateAsync(Laptop laptop);
```

Voeg de nieuwe laptop toe aan de database en geef het volledige laptop-object terug (inclusief de gegenereerde ID).

#### 4. Een laptop bijwerken

```csharp
Task UpdateAsync(int id, Laptop laptop);
```

Zoek de bestaande laptop met de gevraagde ID en werk alle properties bij met de nieuwe gegevens. Als de laptop niet bestaat, doe dan niets.

#### 5. Een laptop verwijderen

```csharp
Task DeleteAsync(int id);
```

Verwijder de laptop met de gevraagde ID uit de database. Als de laptop niet bestaat, doe dan niets.

### Dependency Injection

Registreer de repository in `Program.cs` met `AddScoped`:

```csharp
builder.Services.AddScoped<ILaptopRepository, LaptopRepository>();
```
