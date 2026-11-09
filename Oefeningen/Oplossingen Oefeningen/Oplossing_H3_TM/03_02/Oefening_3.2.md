# 03_02

## Leerdoel

Na deze oefening kan je een volledige CRUD-API bouwen met het Repository Pattern, Dependency Injection en Logging.

Je leert hoe je alle CRUD-operaties (Create, Read, Update, Delete) implementeert met een Interface en Repository, hoe je Dependency Injection gebruikt om de repository te injecteren in een Controller, en hoe je ILogger gebruikt om logberichten toe te voegen aan elke endpoint.

## Opdracht

De restaurant **De Gouden Oesters** wil een API bouwen voor het beheren van bestellingen. Men wil het Repository Pattern toepassen voor alle datatoegang, Dependency Injection gebruiken en Logging invoegen in elke endpoint.

Jouw taak is om een `BestellingController` te maken met een `IBestellingRepository`-Interface, een `InMemoryBestellingRepository`-Implementatie en een Controller die Dependency Injection en Logging gebruikt.

### Het Bestelling Model

Maak een Modelklasse `Bestelling` in de map `Models` met volgende Properties:

| Property | Type | Beschrijving |
| -------- | ---- | ------------ |
| Id | int | Unieke identificatie van de bestelling |
| Naam | string | De naam van de klant |
| Tafelnummer | int | Het tafelnnummer |
| Gerechten | string | De bestelde gerechten, gescheiden door koppeltekens (bijv. "Lasagne-Salade") |
| Status | string | De status van de bestelling |

### De Repository Interface

Maak een nieuwe map genaamd **Repositories**. Voeg een Interface toe genaamd **IBestellingRepository.cs** met volgende methoden:

| Methode | Return type | Beschrijving |
| ------- | ----------- | ------------ |
| GetAll | `List<Bestelling>` | Alle bestellingen ophalen |
| GetById | `Bestelling?` | Eén bestelling ophalen op basis van de ID (null als niet gevonden) |
| Create | `Bestelling` | Een nieuwe bestelling aanmaken |
| Update | `void` | Een bestelling bijwerken (geen return waarde) |
| Delete | `void` | Een bestelling verwijderen (geen return waarde) |

De Interface moet de volgende code bevatten:

```csharp
using WebApi.Models;

namespace WebApi.Repositories;

public interface IBestellingRepository
{
    List<Bestelling> GetAll();
    Bestelling? GetById(int id);
    Bestelling Create(Bestelling bestelling);
    void Update(int id, Bestelling bestelling);
    void Delete(int id);
}
```

### De InMemory Repository Implementatie

Maak in dezelfde map **Repositories** een klasse `InMemoryBestellingRepository` die `IBestellingRepository` implementeert.

Deze klasse moet:

- een privé `List<Bestelling>` veld bevatten met drie startbestellingen als seed data;
- de `GetAll()`-methode laten teruggeven van de lijst;
- de `GetById()`-methode laten zoeken met `FirstOrDefault`;
- de `Create()`-methode de hoogste bestaande ID + 1 berekenen en toewijzen, dan de bestelling toevoegen;
- de `Update()`-methode de bestelling vinden en alle Properties overschrijven (inclusief Id);
- de `Delete()`-methode de bestelling vinden en verwijderen uit de lijst.

Seed data:

| Id | Naam | Tafelnummer | Gerechten | Status |
| -- | ---- | ----------- | --------- | ------ |
| 1 | Anna Jansen | 4 | Lasagne-Salade | Gereed |
| 2 | Youssef Benali | 7 | Risotto-Gegrilde Groenten | Bereiden |
| 3 | Maria De Smet | 2 | Pasta Carbonara | Gereed |

### De Controller

Maak een `BestellingController` met volgende endpoints. Elke endpoint moet logging bevatten:

#### 1. Alle bestellingen ophalen

Route: `GET /bestellingen`

Geef alle bestellingen terug als JSON met HTTP-statuscode **200 OK**.

Voeg een logbericht van niveau `Information` toe bij het ontvangen van de request.

#### 2. Eén bestelling ophalen op basis van de ID

Route: `GET /bestellingen/{id}`

Vind de bestelling met de gevraagde ID en geef het terug als JSON met HTTP-statuscode **200 OK**.

Als er geen bestelling bestaat met de gevraagde ID, geef dan HTTP-statuscode **404 Not Found** terug zonder body.

Voeg een logbericht van niveau `Information` toe bij het ontvangen van de request en een logbericht van niveau `Warning` als de bestelling niet gevonden wordt.

#### 3. Een nieuwe bestelling aanmaken

Route: `POST /bestellingen`

De client stuurt een bestelling als JSON in de Request Body. ASP.NET Core zet deze automatisch om naar een `Bestelling`-object via Model Binding.

Het endpoint moet:

1. De nieuwe ID berekenen door de hoogste bestaande ID + 1 te nemen;
2. De ID toewijzen aan de nieuwe bestelling;
3. De bestelling toevoegen;
4. De volledige bestelling (inclusief de nieuwe ID) terugsturen met HTTP-statuscode **201 Created**.

De `id` in de request body mag worden genegeerd; je berekent de ID altijd zelf.

Voeg een logbericht van niveau `Information` toe bij het aanmaken van een bestelling.

#### 4. Een bestelling bijwerken

Route: `PUT /bestellingen/{id}`

De routeparameter `{id}` stelt de bestel-ID voor. De client stuurt de nieuwe gegevens van de bestelling als JSON in de Request Body via Model Binding.

Het endpoint moet:

1. De bestelling vinden met de gevraagde ID;
2. Als de bestelling niet bestaat, HTTP-statuscode **404 Not Found** terugsturen zonder body;
3. Alle Properties van de bestelling overschrijven met de nieuwe gegevens uit de Request Body;
4. HTTP-statuscode **204 No Content** terugsturen zonder body.

Voeg een logbericht van niveau `Information` toe bij het ontvangen van de request en een logbericht van niveau `Warning` als de bestelling niet gevonden wordt.

#### 5. Een bestelling verwijderen

Route: `DELETE /bestellingen/{id}`

De routeparameter `{id}` stelt de bestel-ID voor.

Het endpoint moet:

1. De bestelling vinden met de gevraagde ID;
2. Als de bestelling niet bestaat, HTTP-statuscode **404 Not Found** terugsturen zonder body;
3. De bestelling verwijderen uit de lijst;
4. HTTP-statuscode **204 No Content** terugsturen zonder body.

Voeg een logbericht van niveau `Information` toe bij het ontvangen van de request en een logbericht van niveau `Error` als de bestelling niet gevonden wordt.

### Dependency Injection

Registreer de `IBestellingRepository` met `InMemoryBestellingRepository` in `Program.cs` met `AddScoped`.

De Controller moet de repository ontvangen via de constructor (geen `new` in de Controller).

De Controller moet ook `ILogger<BestellingController>` ontvangen via de constructor voor logging.
