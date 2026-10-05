# 02_03

## Leerdoel

Na deze oefening kan je een ASP.NET Core API-controller maken met GET-, PUT- en DELETE-endpoints.

Je leert hoe je bestaande data bijwerkt via een PUT-verzoek met HTTP-statuscode **204 No Content**, hoe je data verwijdert via een DELETE-verzoek met HTTP-statuscode **204 No Content**, en hoe je correct omgaat met niet-bestaande resources met HTTP-statuscode **404 Not Found**.

Je oefent ook het overschrijven van alle Properties van een bestaand object in de lijst.

## Opdracht

De restaurant **De Gouden Oesters** wil een eenvoudige API bouwen voor het beheren van bestellingen.

Jouw taak is om een `BestellingController` te maken met GET-, PUT- en DELETE-endpoints en een `Bestelling`-Model.

### Het Bestelling Model

Maak een Modelklasse `Bestelling` in de map `Models` met volgende Properties:

| Property | Type | Beschrijving |
| -------- | ---- | ------------ |
| Id | int | Unieke identificatie van de bestelling |
| Naam | string | De naam van de klant |
| Tafelnummer | int | Het tafelnnummer |
| Gerechten | string | De bestelde gerechten, gescheiden door koppeltekens (bijv. "Lasagne-Salade") |
| Status | string | De status van de bestelling |

### Startgegevens

Voeg in je Controller een in-memory lijst met drie bestellingen toe als startgegevens:

| Id | Naam | Tafelnummer | Gerechten | Status |
| -- | ---- | ----------- | --------- | ------ |
| 1 | Anna Jansen | 4 | Lasagne-Salade | Gereed |
| 2 | Youssef Benali | 7 | Risotto-Gegrilde Groenten | Bereiden |
| 3 | Maria De Smet | 2 | Pasta Carbonara | Gereed |

### 1. Alle bestellingen ophalen

Voorzie een GET-endpoint op:

`/bestellingen`

Dit endpoint geeft alle bestellingen terug als een JSON-lijst met HTTP-statuscode **200 OK**.

Bijvoorbeeld:

```
GET /bestellingen
```

Statuscode: **200 OK**

```json
[
  {
    "id": 1,
    "naam": "Anna Jansen",
    "tafelnummer": 4,
    "gerechten": "Lasagne-Salade",
    "status": "Gereed"
  },
  {
    "id": 2,
    "naam": "Youssef Benali",
    "tafelnummer": 7,
    "gerechten": "Risotto-Gegrilde Groenten",
    "status": "Bereiden"
  },
  {
    "id": 3,
    "naam": "Maria De Smet",
    "tafelnummer": 2,
    "gerechten": "Pasta Carbonara",
    "status": "Gereed"
  }
]
```

### 2. Eén bestelling ophalen op basis van de ID

Voorzie een GET-endpoint op:

`/bestellingen/{id}`

De routeparameter `{id}` stelt de bestel-ID voor.

Vind de bestelling met de gevraagde ID en geef het terug als JSON met HTTP-statuscode **200 OK**.

Bijvoorbeeld:

```
GET /bestellingen/2
```

Statuscode: **200 OK**

```json
{
  "id": 2,
  "naam": "Youssef Benali",
  "tafelnummer": 7,
  "gerechten": "Risotto-Gegrilde Groenten",
  "status": "Bereiden"
}
```

Als er geen bestelling bestaat met de gevraagde ID, geef dan HTTP-statuscode **404 Not Found** terug zonder body.

### 3. Een bestelling bijwerken

Voorzie een PUT-endpoint op:

`/bestellingen/{id}`

De routeparameter `{id}` stelt de bestel-ID voor. De client stuurt de nieuwe gegevens van de bestelling als JSON in de Request Body via Model Binding.

Het endpoint moet het volgende doen:

1. De bestelling vinden met de gevraagde ID;
2. Als de bestelling niet bestaat, HTTP-statuscode **404 Not Found** terugsturen zonder body;
3. Alle Properties van de bestelling overschrijven met de nieuwe gegevens uit de Request Body;
4. HTTP-statuscode **204 No Content** terugsturen zonder body.

Bijvoorbeeld:

```
PUT /bestellingen/2
```

Request Body:

```json
{
  "id": 2,
  "naam": "Youssef Benali",
  "tafelnummer": 7,
  "gerechten": "Risotto-Gegrilde Groenten",
  "status": "Gereed"
}
```

Statuscode: **204 No Content**

Na dit verzoek heeft de bestelling met ID 2 de status "Gereed".

### 4. Een bestelling verwijderen

Voorzie een DELETE-endpoint op:

`/bestellingen/{id}`

De routeparameter `{id}` stelt de bestel-ID voor.

Het endpoint moet het volgende doen:

1. De bestelling vinden met de gevraagde ID;
2. Als de bestelling niet bestaat, HTTP-statuscode **404 Not Found** terugsturen zonder body;
3. De bestelling verwijderen uit de lijst;
4. HTTP-statuscode **204 No Content** terugsturen zonder body.

Bijvoorbeeld:

```
DELETE /bestellingen/1
```

Statuscode: **204 No Content**

Na dit verzoek bestaat de bestelling met ID 1 niet meer in de lijst.
