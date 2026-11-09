# 04_04

<b>Hoofdstuk 04</b> &nbsp;·&nbsp; Een API op de database, in schoolstijl &nbsp;·&nbsp; Dierenasiel &nbsp;·&nbsp; niveau <span style="color:#37b24d">●○○</span> &nbsp;·&nbsp; ±90 min

## Leerdoel

Na deze oefening kan je een bestaande API laten overstappen naar een PostgreSQL-database, met een DbContext die je controller rechtstreeks binnenkrijgt.

Je leert een DbContext opstellen, met de Fluent API vastleggen hoe elke kolom eruitziet, startgegevens meegeven met `HasData`, en je model via een migratie naar de database brengen.

Daarnaast schakel je over naar de stijl van de schoolopgaven: routes zonder `api/`, een <code style="color:#f08c00;font-weight:600">404</code> zonder body, en een lege lijst als een filter niets vindt.

## 

## Opdracht

De dieren-API van Knuffelhof uit 03_04 werkt met een repository, maar de dieren leven nog altijd in het geheugen. Knuffelhof wil een echte database. De website die de API gebruikt, wordt tegelijk vernieuwd, en de nieuwe website verwacht de routes en de antwoorden zoals de schoolopgaven ze vragen.

Jouw taak is om een database op te zetten, ze via een migratie op te bouwen met haar startgegevens, je controller zijn gegevens rechtstreeks uit die database te laten halen, en je routes en antwoorden om te zetten naar de nieuwe stijl.

Via de API moet de website, net als in 03_04:

1. alle dieren en één dier kunnen opvragen;
2. alle dieren van één soort kunnen opvragen;
3. enkel de jonge dieren kunnen opvragen;
4. dieren kunnen zoeken op een stukje van hun naam;
5. de dierfiche kunnen opvragen.

Implementeer onderstaande functionaliteiten.

### 1. Je vertrekpunt

Vertrek van je eigen oplossing van 03_04. Het model ligt vanaf nu vast, met deze C#-types:

| Property        | C#-type  |
|-----------------|----------|
| Id              | `int`    |
| Naam            | `string` |
| Soort           | `string` |
| LeeftijdInJaren | `int`    |
| Verblijfsnummer | `int`    |

> [!IMPORTANT]
> Vanaf deze oefening volgt de reeks _04 tot en met _06 de stijl van de schoolopgaven. De reeks _01 tot en met _03 blijft de stijl van de les volgen. Zo oefen je beide, en leer je de opgave letterlijk te lezen: op het examen bepaalt de opgave welke stijl je gebruikt.

---

### 2. De database in Docker

Start een PostgreSQL-database in Docker, zoals in les 04. Heb je de container uit de les nog, dan mag je die hergebruiken. Deze oefening krijgt een eigen database: `AsielDb`.

Installeer de packages uit les 04 en zet in `appsettings.json` een connectiestring met de naam `PostgresConnection`, die naar `AsielDb` wijst.

---

### 3. De DbContext

Maak een map Data, met daarin een klasse `AsielContext` die erft van `DbContext`. Ze heeft een constructor die de opties doorgeeft aan de basisklasse, en een `DbSet<Dier>` met de naam `Dieren`.

Leg in `OnModelCreating` met de Fluent API vast hoe de tabel eruitziet:

| Property        | Kolom in de database |
|-----------------|----------------------|
| Id              | de sleutel |
| Naam            | tekst van hoogstens 50 tekens, verplicht |
| Soort           | tekst van hoogstens 30 tekens, verplicht |
| LeeftijdInJaren | geheel getal, verplicht |
| Verblijfsnummer | geheel getal, verplicht |

De tabel heet `Dieren`. Registreer je `AsielContext` in Program.cs met de PostgreSQL-provider en de connectiestring uit punt 2.

---

### 4. De startgegevens en de migratie

Leg met `HasData` deze zes dieren vast:

| Id | Naam    | Soort   | LeeftijdInJaren | Verblijfsnummer |
|----|---------|---------|-----------------|-----------------|
| 1  | Mimi    | kat     | 3               | 2               |
| 2  | Tommie  | kat     | 1               | 2               |
| 3  | Rex     | hond    | 7               | 5               |
| 4  | Bella   | hond    | 1               | 5               |
| 5  | Flappie | konijn  | 2               | 8               |
| 6  | Kiki    | parkiet | 4               | 9               |

Maak een migratie met de naam `InitialCreate` en werk daarmee de database bij. Controleer in pgAdmin of in VS Code of de tabel `Dieren` bestaat, met de lengtes uit punt 3 en de zes dieren.

> [!NOTE]
> Les 04 toont `HasData` niet, de schoolopgaven van hoofdstuk 4 gebruiken het wel. Je leerboek legt het uit in hoofdstuk 39: lees daar de regels voor je begint.

> [!TIP]
> Open de migratie voor je de database bijwerkt. Zie je de lengtes, de verplichte kolommen en onderaan je zes dieren terug? Zo niet, dan staan ze ook niet in je database.

---

### 5. De controller op de database, in de nieuwe stijl

Pas je `DierController` aan: hij krijgt via zijn constructor voortaan je `AsielContext` binnen in plaats van een `IDierRepository`, naast zijn `ILogger<DierController>`. Haal de registratie van je repository weg uit Program.cs.

De routes en de antwoorden veranderen zo:

| Endpoint | Antwoord |
|----------|----------|
| <span style="background:#1c7ed6;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>GET</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">dieren</code> | <code style="color:#37b24d;font-weight:600">200 OK</code> met alle dieren, op Id gesorteerd |
| <span style="background:#1c7ed6;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>GET</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">dieren/<span style="color:#e64980"><b>{id}</b></span></code> | <code style="color:#37b24d;font-weight:600">200 OK</code> met het dier, of <code style="color:#f08c00;font-weight:600">404 Not Found</code> zonder body |
| <span style="background:#1c7ed6;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>GET</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">dieren/soort/<span style="color:#e64980"><b>{soort}</b></span></code> | <code style="color:#37b24d;font-weight:600">200 OK</code> met de dieren van die soort, op Id gesorteerd |
| <span style="background:#1c7ed6;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>GET</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">dieren/jong</code> | <code style="color:#37b24d;font-weight:600">200 OK</code> met de dieren jonger dan twee jaar, op Id gesorteerd |
| <span style="background:#1c7ed6;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>GET</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">dieren/zoek/<span style="color:#e64980"><b>{tekst}</b></span></code> | <code style="color:#37b24d;font-weight:600">200 OK</code> met de dieren waarvan de naam dat stukje bevat, op Id gesorteerd |
| <span style="background:#1c7ed6;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>GET</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">dieren/fiche/<span style="color:#e64980"><b>{id}</b></span></code> | <code style="color:#37b24d;font-weight:600">200 OK</code> met de `DierFiche`, of <code style="color:#f08c00;font-weight:600">404 Not Found</code> zonder body |

Vindt een filter niets, dan geeft het een lege lijst `[]` met <code style="color:#37b24d;font-weight:600">200 OK</code>, en geen <code style="color:#f08c00;font-weight:600">404</code> meer. Bij de soort en bij het zoeken spelen hoofdletters geen rol, zoals in 03_04.

> [!WARNING]
> Je routes beginnen niet meer met `api/`, en ze staan in het meervoud. Zet de route letterlijk in je controller. Met `[controller]` in je route krijg je de naam van je klasse, `Dier`, en niet `dieren`.

> [!NOTE]
> "Zonder body" betekent in de schoolopgaven dat jij er geen bericht in zet. Met `[ApiController]` vult ASP.NET Core de body van een <code style="color:#f08c00;font-weight:600">404</code> zelf met een klein foutobject. Dat is in orde.

> [!WARNING]
> Niet elke C#-methode kan Entity Framework Core naar SQL vertalen. Krijg je bij de soort of bij het zoeken een <code style="color:#f03e3e;font-weight:600">500</code> met *could not be translated*, kijk dan welke methode je in je vergelijking gebruikt, en zoek een vorm die de database begrijpt.

Log elk verzoek op niveau Information, en elke <code style="color:#f08c00;font-weight:600">404</code> op niveau Warning.

---

### 6. Alles samen: een kolom erbij

<span style="background:#7048e8;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>SAMENGESTELDE OEFENING</b></span>

De vorige punten brachten je model, je startgegevens en je API één keer naar de database, in een nieuwe stijl. In dit laatste punt doorloop je de cyclus van les 04 opnieuw, en moeten je model, je database, je startgegevens en je API daarna weer op één lijn liggen.

Knuffelhof wil bij elk dier tonen of het al een chip heeft.

1. Voeg aan het model een property `IsGechipt` toe, van het type `bool`. In de database wordt het een verplichte kolom met ja of nee.
2. Vul de waarde in bij je startgegevens: de dieren 1, 3 en 4 zijn gechipt, de dieren 2, 5 en 6 niet.
3. Maak een migratie met de naam `ChipToegevoegd`. Open ze en zoek in `Up` de regel die de kolom toevoegt en de regels die de waarde van je dieren invullen, en in `Down` wat er weer weggaat.
4. Werk de database bij.

> [!IMPORTANT]
> Pas de database nooit met de hand aan. De kolom `IsGechipt` en haar waarden komen er via je migratie.

#### Testscenario

Herstart je API en doe de volgende verzoeken.

| Verzoek | Statuscode | Antwoord |
|---------|------------|----------|
| <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> dieren</code> | <code style="color:#37b24d;font-weight:600">200</code> | de zes dieren, op Id gesorteerd |
| <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> dieren/99</code> | <code style="color:#f08c00;font-weight:600">404</code> | geen bericht van jou |
| <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> dieren/soort/HOND</code> | <code style="color:#37b24d;font-weight:600">200</code> | de dieren met Id 3 en 4 |
| <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> dieren/soort/cavia</code> | <code style="color:#37b24d;font-weight:600">200</code> | `[]` |
| <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> dieren/jong</code> | <code style="color:#37b24d;font-weight:600">200</code> | de dieren met Id 2 en 4 |
| <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> dieren/zoek/MI</code> | <code style="color:#37b24d;font-weight:600">200</code> | de dieren met Id 1 en 2 |
| <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> dieren/zoek/xyz</code> | <code style="color:#37b24d;font-weight:600">200</code> | `[]` |
| <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> dieren/fiche/99</code> | <code style="color:#f08c00;font-weight:600">404</code> | geen bericht van jou |
| <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> api/dier</code> | <code style="color:#f08c00;font-weight:600">404</code> | de oude route bestaat niet meer |

<code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> dieren/4</code> geeft als resultaat:

```json
{
  "id": 4,
  "naam": "Bella",
  "soort": "hond",
  "leeftijdInJaren": 1,
  "verblijfsnummer": 5,
  "isGechipt": true
}
```

<code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> dieren/fiche/3</code> geeft als resultaat:

```json
{
  "dier": {
    "id": 3,
    "naam": "Rex",
    "soort": "hond",
    "leeftijdInJaren": 7,
    "verblijfsnummer": 5,
    "isGechipt": true
  },
  "isJong": false,
  "aantalVanDezelfdeSoort": 1,
  "isOudsteVanDeSoort": true
}
```

Controleer tot slot:

- of de geschiedenis van je migraties precies twee migraties bevat: `InitialCreate` en `ChipToegevoegd`;
- of er in je terminal bij het verzoek naar dier 99 en naar fiche 99 telkens een `Warning` staat.
