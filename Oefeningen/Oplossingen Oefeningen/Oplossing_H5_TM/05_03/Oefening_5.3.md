# 05_03

## Leerdoel

Na deze oefening kan je een asynchrone `Repository` implementeren met query-methodes die complexe LINQ-operaties combineren, zoals projectie met `Select`, aggregatie met `Sum` en `Max`, en conditionele zoekopdrachten met `AnyAsync`.

Je leert hoe je asynchrone methodes van EF Core combineert met LINQ-aggregatiefuncties, hoe je data projecteert naar specifieke eigenschappen met `Select`, en hoe je conditionele queries uitvoert die meerdere criteria combineren.

## Opdracht

De boekhandel **Boekwinkel De Gouden Pagina** wil haar catalogus-API migreren van een in-memory lijst naar een echte PostgreSQL-database met Entity Framework Core. Na de basis-CRUD-operaties wil men geavanceerde zoek- en rapportagemogelijkheden toevoegen.

Jouw taak is om de asynchrone `BoekRepository`-klasse te implementeren die een interface met zowel CRUD- als complexe query-methodes implementeert.

### Het Boek Model

Maak een Modelklasse `Boek` in de map `Models` met volgende Properties:

| Property | Type | Beschrijving |
| -------- | ---- | ------------ |
| Id | int | Unieke identificatie van het boek |
| Titel | string | De titel van het boek |
| Auteur | string | De auteur van het boek |
| Genre | string | Het genre van het boek (bijv. "Romance", "Sci-Fi", "Thriller", "Fantasy") |
| Prijs | double | De prijs van het boek |
| Uitgeverij | string | De uitgeverij |
| Voorraad | int | Het aantal beschikbare stuks op voorraad |

### De DbContext

De context is reeds aangemaakt en geregistreerd. Gebruik de volgende context:

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

### De Interface

Je moet de `IBoekRepository`-interface implementeren. De interface is als volgt gedefinieerd:

```csharp
using WebApi.Models;

namespace WebApi.Repositories;

public interface IBoekRepository
{
    // CRUD-methodes
    Task<Boek> CreateAsync(Boek boek);
    Task DeleteAsync(int id);
    Task<List<Boek>> GetAllAsync();
    Task<Boek?> GetByIdAsync(int id);
    Task UpdateAsync(int id, Boek boek);

    // Query-methodes
    Task<List<string>> GetUniqueGenresAsync();
    Task<List<string>> GetAuthorsByGenreAsync(string genre);
    Task<double> GetAveragePriceAsync();
    Task<double> GetMinPriceAsync();
    Task<double> GetMaxPriceAsync();
    Task<List<Boek>> GetBooksByAuthorAsync(string auteur);
    Task<List<Boek>> GetBooksWithVoorraadGreaterThanAsync(int minVoorraad);
    Task<bool> HasAnyBookInGenreAsync(string genre);
    Task<List<string>> GetPublisherNamesAsync();
}
```

### De asynchrone Repository implementeren

Maak een nieuwe klasse genaamd **BoekRepository** in de map **Repositories** die de `IBoekRepository`-interface implementeert.

De repository moet:
- de `DbContext` ontvangen via constructor injection (geen `new` in de klasse);
- alle CRUD-operaties asynchroon uitvoeren met de juiste EF Core async methodes;
- alle query-methodes asynchroon uitvoeren met de juiste EF Core async methodes;
- de `-Async` suffix gebruiken voor de namen van alle asynchrone methodes;
- correct omgaan met niet-bestaande boeken bij `UpdateAsync` en `DeleteAsync` (niets doen als het boek niet bestaat);
- de `readonly` modifier gebruiken voor de `_context` dependency.

### CRUD-methodes

#### 1. Alle boeken ophalen

```csharp
Task<List<Boek>> GetAllAsync();
```

Haal alle boeken op uit de database en geef ze terug gesorteerd op auteur (oplopend).

#### 2. Eén boek ophalen op basis van de ID

```csharp
Task<Boek?> GetByIdAsync(int id);
```

Zoek het boek met de gevraagde ID en geef het terug. Als er geen boek bestaat met die ID, geef dan `null` terug.

#### 3. Een nieuw boek aanmaken

```csharp
Task<Boek> CreateAsync(Boek boek);
```

Voeg het nieuwe boek toe aan de database en geef het volledige boek-object terug (inclusief de gegenereerde ID).

#### 4. Een boek bijwerken

```csharp
Task UpdateAsync(int id, Boek boek);
```

Zoek het bestaande boek met de gevraagde ID en werk alle properties bij met de nieuwe gegevens. Als het boek niet bestaat, doe dan niets.

#### 5. Een boek verwijderen

```csharp
Task DeleteAsync(int id);
```

Verwijder het boek met de gevraagde ID uit de database. Als het boek niet bestaat, doe dan niets.

### Query-methodes

#### 6. Unieke genres ophalen

```csharp
Task<List<string>> GetUniqueGenresAsync();
```

Haal alle unieke genres op uit de database en geef ze terug gesorteerd in alfabetische volgorde (oplopend). Gebruik `Select` om alleen de genre-eigenschappen te projecteren en `Distinct` om dubbele genres te verwijderen.

#### 7. Auteurs per genre ophalen

```csharp
Task<List<string>> GetAuthorsByGenreAsync(string genre);
```

Haal alle auteurs op van boeken met het gevraagde genre en geef ze terug gesorteerd in alfabetische volgorde (oplopend). De vergelijking moet ongevoelig zijn voor hoofdletters en kleine letters. Gebruik `Select` om alleen de auteur-eigenschappen te projecteren en `Distinct` om dubbele auteurs te verwijderen. Wordt er geen boek gevonden met dat genre, geef dan een lege lijst terug.

#### 8. Gemiddelde prijs berekenen

```csharp
Task<double> GetAveragePriceAsync();
```

Bereken het gemiddelde van alle boekprijzen. Gebruik de `AverageAsync()` methode. Wordt er geen boek gevonden, geef dan `0` terug.

#### 9. Laagste prijs vinden

```csharp
Task<double> GetMinPriceAsync();
```

Vind de laagste prijs onder alle boeken. Gebruik de `MinAsync()` methode. Wordt er geen boek gevonden, geef dan `0` terug.

#### 10. Hoogste prijs vinden

```csharp
Task<double> GetMaxPriceAsync();
```

Vind de hoogste prijs onder alle boeken. Gebruik de `MaxAsync()` methode. Wordt er geen boek gevonden, geef dan `0` terug.

#### 11. Boeken per auteur ophalen

```csharp
Task<List<Boek>> GetBooksByAuthorAsync(string auteur);
```

Zoek alle boeken van de gevraagde auteur en geef ze terug gesorteerd op prijs (oplopend). De vergelijking moet ongevoelig zijn voor hoofdletters en kleine letters. Wordt er geen boek gevonden voor die auteur, geef dan een lege lijst terug.

#### 12. Boeken met minimum voorraad ophalen

```csharp
Task<List<Boek>> GetBooksWithVoorraadGreaterThanAsync(int minVoorraad);
```

Zoek alle boeken waarvan de voorraad groter is dan de opgegeven waarde en geef ze terug gesorteerd op prijs (oplopend). Wordt er geen boek gevonden, geef dan een lege lijst terug.

#### 13. Controleren of een genre bestaat

```csharp
Task<bool> HasAnyBookInGenreAsync(string genre);
```

Controleer of er minstens één boek bestaat met het gevraagde genre. De vergelijking moet ongevoelig zijn voor hoofdletters en kleine letters. Gebruik `AnyAsync()` voor deze controle. Geef `true` terug als er minstens één boek bestaat met dat genre, anders `false`.

#### 14. Uitgeverijnamen ophalen

```csharp
Task<List<string>> GetPublisherNamesAsync();
```

Haal alle unieke uitgeverijnamen op uit de database en geef ze terug gesorteerd in alfabetische volgorde (oplopend). Gebruik `Select` om alleen de uitgeverij-eigenschappen te projecteren en `Distinct` om dubbele uitgeverijen te verwijderen.

### Seed data toevoegen

Voeg de volgende seed data toe in de `OnModelCreating`-methode van de `DbContext`:

| Id | Titel | Auteur | Genre | Prijs | Uitgeverij | Voorraad |
| -- | ----- | ------ | ----- | ----- | ---------- | -------- |
| 1 | De Kameleon | Jeroen Olyslagers | Thriller | 18.99 | Lannoo | 15 |
| 2 | De Avonturen van Pi | Yann Martel | Avontuur | 14.50 | Ambo|Anthos | 8 |
| 3 | Harry Potter en de Steen der Wijzen | J.K. Rowling | Fantasy | 19.99 | Uitgeverij Contact | 25 |
| 4 | Dune | Frank Herbert | Sci-Fi | 22.00 | Uitgeverij Lannoo | 12 |
| 5 | Het diner | Herman Koch | Thriller | 16.75 | Uitgeverij Prometheus | 6 |
| 6 | De Ontdekking van de Hemel | Harry Mulisch | Romantiek | 20.50 | Uitgeverij Contact | 4 |
| 7 | De Eenzaamheid van de Sierlijke | Amélie Nothomb | Romantiek | 15.00 | Uitgeverij Lannoo | 10 |
