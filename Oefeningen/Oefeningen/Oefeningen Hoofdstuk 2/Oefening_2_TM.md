# 02_01

## Leerdoel

Na deze oefening kan je een eenvoudige ASP.NET Core API-controller maken met een Model en meerdere GET-endpoints.

Je leert een Modelklasse aanmaken met Properties, data opslaan in een in-memory lijst, en het retourtype `ActionResult<T>` gebruiken om HTTP-statuscodes terug te sturen.

Je oefent ook het gebruik van routeparameters om een specifiek item uit de lijst op te halen, en het correct afhandelen van niet-bestaande items met een `404 Not Found`-status.

## Opdracht

De bibliotheek **Stadsbibliotheek Noord** wil een eenvoudige catalogus-API bouwen waarmee bezoekers boeken kunnen opzoeken.

Jouw taak is om een `BoekController` te maken met verschillende GET-endpoints en een `Boek`-Model.

### Het Boek Model

Maak een Modelklasse `Boek` in de map `Models` met volgende Properties:

| Property | Type | Beschrijving |
| -------- | ---- | ------------ |
| Id | int | Unieke identificatie van het boek |
| Titel | string | De titel van het boek |
| Auteur | string | De auteur van het boek |
| Uitgeverij | string | De uitgeverij |
| Jaartal | int | Het publicatiejaar |

### Startgegevens

Voeg in je Controller een in-memory lijst met drie boeken toe als startgegevens:

| Id | Titel | Auteur | Uitgeverij | Jaartal |
| -- | ----- | ------ | ---------- | ------- |
| 1 | De Ontdekking van de Hemel | Harry Mulisch | De Arbeiderspers | 1992 |
| 2 | Het Dagboek van Anne Frank | Anne Frank | Contact | 1947 |
| 3 | De Avonturen van Pi | Yann Martel | De Bezige Bij | 2001 |

### 1. Alle boeken ophalen

Voorzie een GET-endpoint op:

`/boeken`

Dit endpoint geeft alle boeken terug als een JSON-lijst met HTTP-statuscode **200 OK**.

Bijvoorbeeld:

```
GET /boeken
```

```json
[
  {
    "id": 1,
    "titel": "De Ontdekking van de Hemel",
    "auteur": "Harry Mulisch",
    "uitgeverij": "De Arbeiderspers",
    "jaartal": 1992
  },
  {
    "id": 2,
    "titel": "Het Dagboek van Anne Frank",
    "auteur": "Anne Frank",
    "uitgeverij": "Contact",
    "jaartal": 1947
  },
  {
    "id": 3,
    "titel": "De Avonturen van Pi",
    "auteur": "Yann Martel",
    "uitgeverij": "De Bezige Bij",
    "jaartal": 2001
  }
]
```

### 2. Eén boek ophalen op basis van de ID

Voorzie een GET-endpoint op:

`/boeken/{id}`

De routeparameter `{id}` stelt de boeken-ID voor.

Vind het boek met de gevraagde ID en geef het terug als JSON met HTTP-statuscode **200 OK**.

Bijvoorbeeld:

```
GET /boeken/2
```

```json
{
  "id": 2,
  "titel": "Het Dagboek van Anne Frank",
  "auteur": "Anne Frank",
  "uitgeverij": "Contact",
  "jaartal": 1947
}
```

Als er geen boek bestaat met de gevraagde ID, geef dan HTTP-statuscode **404 Not Found** terug.

Bijvoorbeeld:

```
GET /boeken/99
```

De response heeft statuscode **404 Not Found** zonder body.
