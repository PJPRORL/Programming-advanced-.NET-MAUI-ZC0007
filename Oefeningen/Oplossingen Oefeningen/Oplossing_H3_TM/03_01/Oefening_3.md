# 03_01

## Leerdoel

Na deze oefening kan je een Interface en een Repository Pattern gebruiken om data te beheren in een ASP.NET Core API.

Je leert hoe je een contract definieert met een Interface, hoe je die implementeert met een `InMemoryRepository`, en hoe je Dependency Injection gebruikt om de repository te injecteren in een Controller.

## Opdracht

De fictieve bibliotheek **Stadsbibliotheek Noord** wil een eenvoudige catalogus-API bouwen. In plaats van de data rechtstreeks in de Controller te beheren, wil men het Repository Pattern toepassen.

Jouw taak is om een `BoekController` te maken met een `IBoekRepository`-Interface en een `InMemoryBoekRepository`-Implementatie.

### Het Boek Model

Maak een Modelklasse `Boek` in de map `Models` met volgende Properties:

| Property | Type | Beschrijving |
| -------- | ---- | ------------ |
| Id | int | Unieke identificatie van het boek |
| Titel | string | De titel van het boek |
| Auteur | string | De auteur van het boek |
| Uitgeverij | string | De uitgeverij |
| Jaartal | int | Het publicatiejaar |

### De Repository Interface

Maak een nieuwe map genaamd **Repositories**. Voeg een Interface toe genaamd **IBoekRepository.cs** met volgende methoden:

| Methode | Return type | Beschrijving |
| ------- | ----------- | ------------ |
| GetAll | `List<Boek>` | Alle boeken ophalen |
| GetById | `Boek?` | Eén boek ophalen op basis van de ID (null als niet gevonden) |
| Create | `Boek` | Een nieuw boek aanmaken |

De Interface moet de volgende code bevatten:

```csharp
using WebApi.Models;

namespace WebApi.Repositories;

public interface IBoekRepository
{
    List<Boek> GetAll();
    Boek? GetById(int id);
    Boek Create(Boek boek);
}
```

### De InMemory Repository Implementatie

Maak in dezelfde map **Repositories** een klasse `InMemoryBoekRepository` die `IBoekRepository` implementeert.

Deze klasse moet:

- een privé `List<Boek>` veld bevatten met drie startboeken als seed data;
- de `GetAll()`-methode laten teruggeven van de lijst;
- de `GetById()`-methode laten zoeken met `FirstOrDefault`;
- de `Create()`-methode de hoogste bestaande ID + 1 berekenen en toewijzen, dan het boek toevoegen.

Seed data:

| Id | Titel | Auteur | Uitgeverij | Jaartal |
| -- | ----- | ------ | ---------- | ------- |
| 1 | De Ontdekking van de Hemel | Harry Mulisch | De Arbeiderspers | 1992 |
| 2 | Het Dagboek van Anne Frank | Anne Frank | Contact | 1947 |
| 3 | De Avonturen van Pi | Yann Martel | De Bezige Bij | 2001 |

### De Controller

Maak een `BoekController` met volgende endpoints:

#### 1. Alle boeken ophalen

Route: `GET /boeken`

Geef alle boeken terug met HTTP-statuscode **200 OK**.

#### 2. Eén boek ophalen op basis van de ID

Route: `GET /boeken/{id}`

Vind het boek met de gevraagde ID en geef het terug als JSON met HTTP-statuscode **200 OK**.

Als er geen boek bestaat met de gevraagde ID, geef dan HTTP-statuscode **404 Not Found** terug zonder body.

### Dependency Injection

Registreer de `IBoekRepository` met `InMemoryBoekRepository` in `Program.cs` met `AddScoped`.

De Controller moet de repository ontvangen via de constructor (geen `new` in de Controller).
