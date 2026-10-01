# 02_04

## Leerdoel

Na deze oefening kan je een volledige CRUD-API bouwen met alle HTTP Verbs (GET, POST, PUT, DELETE) en het correct toepassen van HTTP-statuscodes.

Je leert een compleet resourcebeheer te implementeren: het opvragen van alle items en één item, het aanmaken van nieuwe items, het bijwerken van bestaande items, en het verwijderen van items. Je oefent ook het omgaan met randgevallen zoals dubbele IDs, niet-bestaande resources, en het genereren van unieke IDs.

Je leert hoe je de `CreatedAtAction`-methode gebruikt om na het aanmaken van een item een Location-header toe te voegen.

## Opdracht

De evenementenorganisatie **Festivaal Vlaanderen** wil een API bouwen voor het beheren van hun evenementenagenda.

Jouw taak is om een `EvenementController` te maken met volledige CRUD-functionaliteit en een `Evenement`-Model.

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

### Startgegevens

Voeg in je Controller een in-memory lijst met drie evenementen toe als startgegevens:

| Id | Naam | Locatie | Datum | MaxDeelnemers | GeregistreerdeDeelnemers |
| -- | ---- | ------- | ----- | ------------- | ---------------------- |
| 1 | Summer Music Festival | Antwerpen | 15-07-2026 | 5000 | 3200 |
| 2 | Culinaire Dagen | Brugge | 22-08-2026 | 200 | 145 |
| 3 | Tech Conference | Gent | 10-09-2026 | 300 | 300 |

### 1. Alle evenementen ophalen

Voorzie een GET-endpoint op:

`/evenementen`

Dit endpoint geeft alle evenementen terug als een JSON-lijst met HTTP-statuscode **200 OK**.

Bijvoorbeeld:

```
GET /evenementen
```

Statuscode: **200 OK**

```json
[
  {
    "id": 1,
    "naam": "Summer Music Festival",
    "locatie": "Antwerpen",
    "datum": "15-07-2026",
    "maxDeelnemers": 5000,
    "geregistreerdeDeelnemers": 3200
  },
  {
    "id": 2,
    "naam": "Culinaire Dagen",
    "locatie": "Brugge",
    "datum": "22-08-2026",
    "maxDeelnemers": 200,
    "geregistreerdeDeelnemers": 145
  },
  {
    "id": 3,
    "naam": "Tech Conference",
    "locatie": "Gent",
    "datum": "10-09-2026",
    "maxDeelnemers": 300,
    "geregistreerdeDeelnemers": 300
  }
]
```

### 2. Eén evenement ophalen op basis van de ID

Voorzie een GET-endpoint op:

`/evenementen/{id}`

De routeparameter `{id}` stelt de evenement-ID voor.

Vind het evenement met de gevraagde ID en geef het terug als JSON met HTTP-statuscode **200 OK**.

Bijvoorbeeld:

```
GET /evenementen/2
```

Statuscode: **200 OK**

```json
{
  "id": 2,
  "naam": "Culinaire Dagen",
  "locatie": "Brugge",
  "datum": "22-08-2026",
  "maxDeelnemers": 200,
  "geregistreerdeDeelnemers": 145
}
```

Als er geen evenement bestaat met de gevraagde ID, geef dan HTTP-statuscode **404 Not Found** terug zonder body.

### 3. Een nieuw evenement aanmaken

Voorzie een POST-endpoint op:

`/evenementen`

De client stuurt een evenement als JSON in de Request Body. ASP.NET Core zet deze automatisch om naar een `Evenement`-object via Model Binding.

Het endpoint moet het volgende doen:

1. De nieuwe ID berekenen door de hoogste bestaande ID + 1 te nemen;
2. De ID toewijzen aan het nieuwe evenement;
3. Het evenement toevoegen aan de lijst;
4. Het volledige evenement (inclusief de nieuwe ID) terugsturen met HTTP-statuscode **201 Created** en een Location-header naar het nieuwe item.

Bijvoorbeeld:

```
POST /evenementen
```

Request Body:

```json
{
  "id": 0,
  "naam": "Boekenfestival",
  "locatie": "Leuven",
  "datum": "05-10-2026",
  "maxDeelnemers": 150,
  "geregistreerdeDeelnemers": 0
}
```

Statuscode: **201 Created**

Response Body:

```json
{
  "id": 4,
  "naam": "Boekenfestival",
  "locatie": "Leuven",
  "datum": "05-10-2026",
  "maxDeelnemers": 150,
  "geregistreerdeDeelnemers": 0
}
```

De `id` in de request body mag worden genegeerd; je berekent de ID altijd zelf.

### 4. Een evenement bijwerken

Voorzie een PUT-endpoint op:

`/evenementen/{id}`

De routeparameter `{id}` stelt de evenement-ID voor. De client stuurt de nieuwe gegevens van het evenement als JSON in de Request Body via Model Binding.

Het endpoint moet het volgende doen:

1. Het evenement vinden met de gevraagde ID;
2. Als het evenement niet bestaat, HTTP-statuscode **404 Not Found** terugsturen zonder body;
3. Alle Properties van het evenement overschrijven met de nieuwe gegevens uit de Request Body;
4. HTTP-statuscode **204 No Content** terugsturen zonder body.

Bijvoorbeeld:

```
PUT /evenementen/2
```

Request Body:

```json
{
  "id": 2,
  "naam": "Culinaire Dagen Brugge",
  "locatie": "Brugge",
  "datum": "22-08-2026",
  "maxDeelnemers": 250,
  "geregistreerdeDeelnemers": 145
}
```

Statuscode: **204 No Content**

Na dit verzoek heeft het evenement met ID 2 de naam "Culinaire Dagen Brugge" en een nieuw maximum van 250 deelnemers.

### 5. Een evenement verwijderen

Voorzie een DELETE-endpoint op:

`/evenementen/{id}`

De routeparameter `{id}` stelt de evenement-ID voor.

Het endpoint moet het volgende doen:

1. Het evenement vinden met de gevraagde ID;
2. Als het evenement niet bestaat, HTTP-statuscode **404 Not Found** terugsturen zonder body;
3. Het evenement verwijderen uit de lijst;
4. HTTP-statuscode **204 No Content** terugsturen zonder body.

Bijvoorbeeld:

```
DELETE /evenementen/1
```

Statuscode: **204 No Content**

Na dit verzoek bestaat het evenement met ID 1 niet meer in de lijst.
