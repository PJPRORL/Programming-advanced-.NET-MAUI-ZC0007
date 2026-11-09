# 02_02

## Leerdoel

Na deze oefening kan je een ASP.NET Core API-controller maken met zowel GET- als POST-endpoints.

Je leert hoe je gegevens naar de server stuurt via de Request Body met Model Binding, hoe je een nieuw item toevoegt aan een in-memory lijst, en hoe je de correcte HTTP-statuscodes terugstuurt: **200 OK** voor leesacties, **201 Created** voor het aanmaken van nieuwe items, en **404 Not Found** wanneer een resource niet bestaat.

Je oefent ook het berekenen van een nieuwe ID op basis van de hoogste bestaande ID.

## Opdracht

De bibliotheek **Stadsbibliotheek Noord** wil haar catalogus-API uitbreiden zodat nieuwe boeken kunnen worden toegevoegd.

Jouw taak is om een `BoekController` te maken met GET- en POST-endpoints en een `Boek`-Model.

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

Statuscode: **200 OK**

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
GET /boeken/1
```

Statuscode: **200 OK**

```json
{
  "id": 1,
  "titel": "De Ontdekking van de Hemel",
  "auteur": "Harry Mulisch",
  "uitgeverij": "De Arbeiderspers",
  "jaartal": 1992
}
```

Als er geen boek bestaat met de gevraagde ID, geef dan HTTP-statuscode **404 Not Found** terug zonder body.

### 3. Een nieuw boek aanmaken

Voorzie een POST-endpoint op:

`/boeken`

De client stuurt een boek als JSON in de Request Body. ASP.NET Core zet deze automatisch om naar een `Boek`-object via Model Binding.

Het endpoint moet het volgende doen:

1. De nieuwe ID berekenen door de hoogste bestaande ID + 1 te nemen;
2. De ID toewijzen aan het nieuwe boek;
3. Het boek toevoegen aan de lijst;
4. Het volledige boek (inclusief de nieuwe ID) terugsturen met HTTP-statuscode **201 Created**.

Bijvoorbeeld:

```
POST /boeken
```

Request Body:

```json
{
  "id": 0,
  "titel": "De Kringloop van het Leven",
  "auteur": "Hella Haasse",
  "uitgeverij": "Contact",
  "jaartal": 1960
}
```

Statuscode: **201 Created**

Response Body:

```json
{
  "id": 4,
  "titel": "De Kringloop van het Leven",
  "auteur": "Hella Haasse",
  "uitgeverij": "Contact",
  "jaartal": 1960
}
```

De `id` in de request body mag worden genegeerd; je berekent de ID altijd zelf.
