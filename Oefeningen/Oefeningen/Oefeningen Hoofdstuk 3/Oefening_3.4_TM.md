# 03_04

## Leerdoel

Na deze oefening kan je alle concepten uit dit hoofdstuk combineren: het Repository Pattern met meerdere query-methodes, Dependency Injection en Logging in een volledige CRUD-API.

Je leert hoe je een complete applicatie bouwt die alle CRUD-operaties ondersteunt, gefilterde queries toestaat, logging gebruikt in elke endpoint, en correct omgaat met niet-bestaande resources.

## Opdracht

De evenementenorganisatie **Festivaal Vlaanderen** wil een API bouwen voor het beheren van hun evenementenagenda. Men wil het Repository Pattern toepassen voor alle datatoegang, Dependency Injection gebruiken en Logging invoegen in elke endpoint.

Jouw taak is om een `EvenementController` te maken met een `IEvenementRepository`-Interface met meerdere query-methodes, een `InMemoryEvenementRepository`-Implementatie en een Controller die Dependency Injection en Logging gebruikt.

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

### De Repository Interface

Maak een nieuwe map genaamd **Repositories**. Voeg een Interface toe genaamd **IEvenementRepository.cs** met volgende methoden:

| Methode | Return type | Beschrijving |
| ------- | ----------- | ------------ |
| GetAll | `List<Evenement>` | Alle evenementen ophalen |
| GetById | `Evenement?` | Eén evenement ophalen op basis van de ID (null als niet gevonden) |
| GetByLocatie | `List<Evenement>?` | Evenementen ophalen op basis van de locatie (null of lege lijst als geen gevonden) |
| Create | `Evenement` | Een nieuw evenement aanmaken |
| Update | `void` | Een evenement bijwerken (geen return waarde) |
| Delete | `void` | Een evenement verwijderen (geen return waarde) |

De Interface moet de volgende code bevatten:

```csharp
using WebApi.Models;

namespace WebApi.Repositories;

public interface IEvenementRepository
{
    List<Evenement> GetAll();
    Evenement? GetById(int id);
    List<Evenement>? GetByLocatie(string locatie);
    Evenement Create(Evenement evenement);
    void Update(int id, Evenement evenement);
    void Delete(int id);
}
```

### De InMemory Repository Implementatie

Maak in dezelfde map **Repositories** een klasse `InMemoryEvenementRepository` die `IEvenementRepository` implementeert.

Deze klasse moet:

- een privé `List<Evenement>` veld bevatten met drie startevenementen als seed data;
- de `GetAll()`-methode laten teruggeven van de lijst;
- de `GetById()`-methode laten zoeken met `FirstOrDefault`;
- de `GetByLocatie()`-methode laten filteren op locatie met `Where`, ongevoelig voor hoofdletters/kleine letters;
- de `Create()`-methode de hoogste bestaande ID + 1 berekenen en toewijzen, dan het evenement toevoegen;
- de `Update()`-methode het evenement vinden en alle Properties overschrijven (inclusief Id);
- de `Delete()`-methode het evenement vinden en verwijderen uit de lijst.

Seed data:

| Id | Naam | Locatie | Datum | MaxDeelnemers | GeregistreerdeDeelnemers |
| -- | ---- | ------- | ----- | ------------- | ---------------------- |
| 1 | Summer Music Festival | Antwerpen | 15-07-2026 | 5000 | 3200 |
| 2 | Culinaire Dagen | Brugge | 22-08-2026 | 200 | 145 |
| 3 | Tech Conference | Gent | 10-09-2026 | 300 | 300 |

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

Voeg een logbericht van niveau `Information` toe bij het ontvangen van de request.

#### 4. Een nieuw evenement aanmaken

Route: `POST /evenementen`

De client stuurt een evenement als JSON in de Request Body. ASP.NET Core zet deze automatisch om naar een `Evenement`-object via Model Binding.

Het endpoint moet:

1. De nieuwe ID berekenen door de hoogste bestaande ID + 1 te nemen;
2. De ID toewijzen aan het nieuwe evenement;
3. Het evenement toevoegen;
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
3. Het evenement verwijderen uit de lijst;
4. HTTP-statuscode **204 No Content** terugsturen zonder body.

Voeg een logbericht van niveau `Information` toe bij het ontvangen van de request en een logbericht van niveau `Error` als het evenement niet gevonden wordt.

### Dependency Injection

Registreer de `IEvenementRepository` met `InMemoryEvenementRepository` in `Program.cs` met `AddScoped`.

De Controller moet de repository ontvangen via de constructor (geen `new` in de Controller).

De Controller moet ook `ILogger<EvenementController>` ontvangen via de constructor voor logging.
