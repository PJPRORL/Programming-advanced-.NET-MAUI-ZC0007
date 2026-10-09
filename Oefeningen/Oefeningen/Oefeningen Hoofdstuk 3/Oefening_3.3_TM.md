# 03_03

## Leerdoel

Na deze oefening kan je een Interface met meerdere query-methoden gebruiken om gefilterde data te ophalen uit een Repository.

Je leert hoe je een Interface uitbreidt met extra zoekmethodes, hoe je die methodes implementeert in de Repository, en hoe je ze gebruikt in een Controller met Dependency Injection.

## Opdracht

De fictieve bioscoop **Cinema Paradiso** wil een API bouwen voor het beheren van hun films. In plaats van slechts één zoekmethode (GetById), wil men meerdere query-methodes om films op verschillende manieren te kunnen zoeken.

Jouw taak is om een `FilmController` te maken met een `IFilmRepository`-Interface die meerdere query-methodes bevat, een `InMemoryFilmRepository`-Implementatie en een Controller die Dependency Injection gebruikt.

### Het Film Model

Maak een Modelklasse `Film` in de map `Models` met volgende Properties:

| Property | Type | Beschrijving |
| -------- | ---- | ------------ |
| Id | int | Unieke identificatie van de film |
| Titel | string | De titel van de film |
| Regisseur | string | De regisseur van de film |
| Genre | string | Het genre van de film |
| Speelduur | int | De speelduur in minuten |

### De Repository Interface

Maak een nieuwe map genaamd **Repositories**. Voeg een Interface toe genaamd **IFilmRepository.cs** met volgende methoden:

| Methode | Return type | Beschrijving |
| ------- | ----------- | ------------ |
| GetAll | `List<Film>` | Alle films ophalen |
| GetById | `Film?` | Eén film ophalen op basis van de ID (null als niet gevonden) |
| GetByGenre | `List<Film>?` | Films ophalen op basis van het genre (null of lege lijst als geen gevonden) |

De Interface moet de volgende code bevatten:

```csharp
using WebApi.Models;

namespace WebApi.Repositories;

public interface IFilmRepository
{
    List<Film> GetAll();
    Film? GetById(int id);
    List<Film>? GetByGenre(string genre);
}
```

### De InMemory Repository Implementatie

Maak in dezelfde map **Repositories** een klasse `InMemoryFilmRepository` die `IFilmRepository` implementeert.

Deze klasse moet:

- een privé `List<Film>` veld bevatten met vijf startfilms als seed data;
- de `GetAll()`-methode laten teruggeven van de lijst;
- de `GetById()`-methode laten zoeken met `FirstOrDefault`;
- de `GetByGenre()`-methode laten filteren op genre met `Where`, ongevoelig voor hoofdletters/kleine letters;
- de `GetByGenre()`-methode teruggeven als `null` of lege lijst als er geen films gevonden worden.

Seed data:

| Id | Titel | Regisseur | Genre | Speelduur |
| -- | ----- | --------- | ----- | --------- |
| 1 | The Shawshank Redemption | Frank Darabont | Drama | 142 |
| 2 | Inception | Christopher Nolan | Sci-Fi | 148 |
| 3 | De Ontdekking van de Hemel | Jeroen Krabbé | Drama | 165 |
| 4 | Interstellar | Christopher Nolan | Sci-Fi | 169 |
| 5 | De Avonturen van Pi | Ang Lee | Avontuur | 127 |

### De Controller

Maak een `FilmController` met volgende endpoints:

#### 1. Alle films ophalen

Route: `GET /films`

Geef alle films terug als JSON met HTTP-statuscode **200 OK**.

#### 2. Eén film ophalen op basis van de ID

Route: `GET /films/{id}`

Vind de film met de gevraagde ID en geef het terug als JSON met HTTP-statuscode **200 OK**.

Als er geen film bestaat met de gevraagde ID, geef dan HTTP-statuscode **404 Not Found** terug zonder body.

#### 3. Films ophalen op basis van het genre

Route: `GET /films/genre/{genre}`

De routeparameter `{genre}` stelt het genre voor.

Zoek films met het gevraagde genre en geef ze terug als JSON met HTTP-statuscode **200 OK**.

Wordt er geen film gevonden voor dat genre, geef dan een lege JSON-lijst `[]` terug met HTTP-statuscode **200 OK** (geen 404 voor lege resultaten).

### Dependency Injection

Registreer de `IFilmRepository` met `InMemoryFilmRepository` in `Program.cs` met `AddScoped`.

De Controller moet de repository ontvangen via de constructor (geen `new` in de Controller).
