# 04_01

<b>Hoofdstuk 04</b> &nbsp;·&nbsp; Een API op de database &nbsp;·&nbsp; PC-onderdelen &nbsp;·&nbsp; niveau <span style="color:#37b24d">●○○</span> &nbsp;·&nbsp; ±90 min

## Leerdoel

Na deze oefening kan je een bestaande API laten overstappen naar een PostgreSQL-database, met Entity Framework Core en de code-first werkwijze.

Je leert een DbContext opstellen, met de Fluent API vastleggen hoe elke kolom eruitziet, startgegevens meegeven met `HasData`, en je model via een migratie naar de database brengen.

Daarnaast laat je je controller rechtstreeks met de DbContext werken, zoals in de schoolopgaven van hoofdstuk 4, en doorloop je de vaste cyclus van les 04 een tweede keer: model aanpassen, migratie maken, nakijken, database bijwerken.

## 

## Opdracht

De artikel-API van Bit & Byte uit 03_01 werkt met een repository, maar de artikelen leven nog altijd in het geheugen. Na een herstart is alles terug zoals in de startlijst. Bit & Byte wil een echte database.

Jouw taak is om een database op te zetten, ze via een migratie op te bouwen met haar startgegevens, en je controller zijn gegevens voortaan rechtstreeks uit die database te laten halen.

Via de API moet de website, net als in 03_01:

1. de volledige voorraad kunnen opvragen;
2. één artikel kunnen opvragen op basis van zijn Id;
3. alle artikelen van één soort kunnen opvragen;
4. enkel de artikelen kunnen opvragen die nog op voorraad liggen;
5. alle artikelen van één merk kunnen opvragen;
6. de artikelfiche kunnen opvragen.

Implementeer onderstaande functionaliteiten.

### 1. Je vertrekpunt

Vertrek van je eigen oplossing van 03_01. Na deze oefening moeten deze endpoints nog precies hetzelfde antwoorden: dezelfde statuscodes, dezelfde berichten en dezelfde JSON.

| Verzoek | Antwoord |
|---------|----------|
| <span style="background:#1c7ed6;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>GET</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/artikel</code> | <code style="color:#37b24d;font-weight:600">200 OK</code> met de volledige lijst, op Id gesorteerd |
| <span style="background:#1c7ed6;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>GET</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/artikel/<span style="color:#e64980"><b>{id}</b></span></code> | <code style="color:#37b24d;font-weight:600">200 OK</code> met het artikel, of <code style="color:#f08c00;font-weight:600">404 Not Found</code> met <code style="color:#845ef7">We vonden geen artikel met Id <span style="color:#e64980"><b>{id}</b></span>.</code> |
| <span style="background:#1c7ed6;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>GET</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/artikel/soort/<span style="color:#e64980"><b>{soort}</b></span></code> | <code style="color:#37b24d;font-weight:600">200 OK</code> met de artikelen van die soort, of <code style="color:#f08c00;font-weight:600">404 Not Found</code> met <code style="color:#845ef7">We vonden geen artikelen van de soort <span style="color:#e64980"><b>{soort}</b></span>.</code> |
| <span style="background:#1c7ed6;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>GET</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/artikel/voorradig</code> | <code style="color:#37b24d;font-weight:600">200 OK</code> met de artikelen die op voorraad liggen, ook als dat een lege lijst is |
| <span style="background:#1c7ed6;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>GET</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/artikel/merk/<span style="color:#e64980"><b>{merk}</b></span></code> | <code style="color:#37b24d;font-weight:600">200 OK</code> met de artikelen van dat merk, of <code style="color:#f08c00;font-weight:600">404 Not Found</code> met <code style="color:#845ef7">We vonden geen artikelen van het merk <span style="color:#e64980"><b>{merk}</b></span>.</code> |
| <span style="background:#1c7ed6;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>GET</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/artikel/fiche/<span style="color:#e64980"><b>{id}</b></span></code> | <code style="color:#37b24d;font-weight:600">200 OK</code> met de `ArtikelFiche`, of <code style="color:#f08c00;font-weight:600">404 Not Found</code> met <code style="color:#845ef7">We vonden geen artikel met Id <span style="color:#e64980"><b>{id}</b></span>.</code> |

Vanaf nu ligt het model vast, met deze C#-types:

| Property         | C#-type  |
|------------------|----------|
| Id               | `int`    |
| Naam             | `string` |
| Merk             | `string` |
| Soort            | `string` |
| Prijs            | `double` |
| AantalOpVoorraad | `int`    |

> [!TIP]
> Een bedrag is in deze reeks een `double`. Op school en op het examen krijg je de types in een tabel, en daar is een bedrag bijna altijd een `double`. Volg die tabel letterlijk: de testen bij een opgave verwachten precies dat type. Staat er wel `decimal`, dan volg je dat. Het verschil tussen de twee lees je in je leerboek, hoofdstuk 2.

---

### 2. De database in Docker

Start een PostgreSQL-database in Docker, zoals in les 04. Heb je de container uit de les nog, dan mag je die hergebruiken.

Elke oefening krijgt wel haar eigen database. Deze database heet `ArtikelDb`.

---

### 3. Je project koppelen

Installeer de packages uit les 04 en zet in `appsettings.json` een connectiestring met de naam `PostgresConnection`, die naar de database `ArtikelDb` wijst.

> [!NOTE]
> `PostgresConnection` is ook de naam die de schoolopgaven gebruiken. Op het examen zijn de packages, de connectiestring en Program.cs vaak al gegeven. Kijk dan goed of de naam in `appsettings.json` dezelfde is als de naam waarmee Program.cs ze opvraagt.

---

### 4. De DbContext

Maak een map Data, met daarin een klasse `ArtikelContext` die erft van `DbContext`. Ze heeft een constructor die de opties doorgeeft aan de basisklasse, en een `DbSet<Artikel>` met de naam `Artikelen`.

Leg in `OnModelCreating` met de Fluent API vast hoe de tabel eruitziet:

| Property         | Kolom in de database |
|------------------|----------------------|
| Id               | de sleutel, de database telt zelf op |
| Naam             | tekst van hoogstens 100 tekens, verplicht |
| Merk             | tekst van hoogstens 50 tekens, verplicht |
| Soort            | tekst van hoogstens 30 tekens, verplicht |
| Prijs            | kommagetal, verplicht |
| AantalOpVoorraad | geheel getal, verplicht |

De tabel heet `Artikelen`.

Registreer je `ArtikelContext` in Program.cs met de PostgreSQL-provider en de connectiestring uit punt 3.

---

### 5. De startgegevens

Leg in `OnModelCreating` met `HasData` deze zes artikelen vast, zodat ze met je migratie in de database komen:

| Id | Naam                | Merk | Soort      | Prijs  | AantalOpVoorraad |
|----|---------------------|------|------------|--------|------------------|
| 1  | ROG Strix B650-A    | Asus | moederbord | 249.99 | 12               |
| 2  | MAG B650 Tomahawk   | MSI  | moederbord | 219.00 | 5                |
| 3  | Ryzen 5 7600X       | AMD  | processor  | 229.50 | 3                |
| 4  | Ryzen 7 7800X3D     | AMD  | processor  | 389.00 | 0                |
| 5  | TUF Gaming RTX 4070 | Asus | videokaart | 649.00 | 2                |
| 6  | Ventus 2X RTX 4060  | MSI  | videokaart | 299.99 | 0                |

> [!NOTE]
> Les 04 toont `HasData` niet, de schoolopgaven van hoofdstuk 4 gebruiken het wel. Je leerboek legt het uit in hoofdstuk 39: lees daar de regels voor je begint.

> [!TIP]
> Je leerboek zet in zijn voorbeelden een `m` achter een bedrag. Die `m` hoort bij een `decimal`. Een `double` schrijf je zonder letter. Een `double` onthoudt ook geen nullen achter de komma: `389.00` in je startgegevens wordt `389` in je JSON.

---

### 6. De eerste migratie

Maak een migratie met de naam `InitialCreate` en werk daarmee de database bij.

Controleer daarna in pgAdmin of in de PostgreSQL-extensie van VS Code:

- of de database `ArtikelDb` bestaat, met een tabel `Artikelen`;
- of elke kolom het type en de lengte heeft uit de tabel van punt 4, en geen enkele kolom leeg mag blijven;
- of de zes artikelen uit punt 5 in de tabel staan.

> [!TIP]
> Open de migratie voor je de database bijwerkt. Zie je de lengtes en de verplichte kolommen uit je Fluent API terug, en onderaan je zes startgegevens? Zo niet, dan staan ze ook niet in je database.

---

### 7. De controller op de database

Pas je `ArtikelController` aan: hij krijgt via zijn constructor voortaan je `ArtikelContext` binnen in plaats van een `IArtikelRepository`, naast zijn `ILogger<ArtikelController>`. Elk endpoint haalt zijn gegevens rechtstreeks uit de tabel `Artikelen`.

Haal de registratie van je repository weg uit Program.cs. De klassen `IArtikelRepository` en `InMemoryArtikelRepository` mogen blijven staan; je controller gebruikt ze niet meer.

> [!NOTE]
> Dat de controller rechtstreeks met de context werkt, is precies wat de schoolopgaven van hoofdstuk 4 vragen. Les 05 zet er weer een repository tussen, dan wel een die met de database praat. Dat doe je in 05_01.

> [!WARNING]
> Een database belooft geen volgorde, tenzij je erom vraagt. Elke lijst die je API teruggeeft, is op Id gesorteerd, zoals ze dat in je in-memory versie altijd was.

> [!WARNING]
> Niet elke C#-methode kan Entity Framework Core naar SQL vertalen. Krijg je bij het zoeken op soort of merk een <code style="color:#f03e3e;font-weight:600">500</code> met een melding dat een expressie *could not be translated*, kijk dan welke methode je in je vergelijking gebruikt, en zoek een vorm die de database begrijpt.

Je logging blijft zoals in 03_01: elk verzoek op niveau Information, elke <code style="color:#f08c00;font-weight:600">404</code> op niveau Warning.

---

### 8. Alles samen: een kolom erbij

<span style="background:#7048e8;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>SAMENGESTELDE OEFENING</b></span>

De vorige punten brachten je model, je startgegevens en je API één keer naar de database. Maar een model blijft niet stilstaan. In dit laatste punt doorloop je de hele cyclus opnieuw, en moeten je model, je database, je startgegevens en je API daarna weer op één lijn liggen.

Bit & Byte wil bij elk artikel tonen hoeveel jaar garantie de klant krijgt.

1. Voeg aan het model een property `Garantiejaren` toe, van het type `int`. In de database wordt het een verplicht geheel getal.
2. Vul de garantie in bij je startgegevens:

   | Id | Garantiejaren |
   |----|---------------|
   | 1  | 3             |
   | 2  | 2             |
   | 3  | 3             |
   | 4  | 3             |
   | 5  | 3             |
   | 6  | 2             |

3. Maak een migratie met de naam `GarantieToegevoegd`. Open ze en zoek in `Up` de regel die de kolom toevoegt, en de regels die de garantie van je zes artikelen invullen. Zoek in `Down` wat er weer weggaat.
4. Werk de database bij.

> [!IMPORTANT]
> Pas de database nooit met de hand aan. De kolom `Garantiejaren` en haar waarden komen er via je migratie, niet via SQL in pgAdmin.

#### Testscenario

Herstart je API en doe de volgende verzoeken. Elk antwoord moet letter voor letter kloppen.

| Verzoek | Statuscode | Antwoord |
|---------|------------|----------|
| <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> api/artikel</code> | <code style="color:#37b24d;font-weight:600">200</code> | de zes artikelen, op Id gesorteerd |
| <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> api/artikel/99</code> | <code style="color:#f08c00;font-weight:600">404</code> | <code style="color:#845ef7">We vonden geen artikel met Id 99.</code> |
| <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> api/artikel/soort/PROCESSOR</code> | <code style="color:#37b24d;font-weight:600">200</code> | de artikelen met Id 3 en 4 |
| <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> api/artikel/soort/koeler</code> | <code style="color:#f08c00;font-weight:600">404</code> | <code style="color:#845ef7">We vonden geen artikelen van de soort koeler.</code> |
| <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> api/artikel/voorradig</code> | <code style="color:#37b24d;font-weight:600">200</code> | de artikelen met Id 1, 2, 3 en 5 |
| <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> api/artikel/merk/asus</code> | <code style="color:#37b24d;font-weight:600">200</code> | de artikelen met Id 1 en 5 |
| <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> api/artikel/merk/corsair</code> | <code style="color:#f08c00;font-weight:600">404</code> | <code style="color:#845ef7">We vonden geen artikelen van het merk corsair.</code> |

<code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> api/artikel/4</code> geeft als resultaat:

```json
{
  "id": 4,
  "naam": "Ryzen 7 7800X3D",
  "merk": "AMD",
  "soort": "processor",
  "prijs": 389,
  "aantalOpVoorraad": 0,
  "garantiejaren": 3
}
```

<code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> api/artikel/fiche/6</code> geeft als resultaat:

```json
{
  "artikel": {
    "id": 6,
    "naam": "Ventus 2X RTX 4060",
    "merk": "MSI",
    "soort": "videokaart",
    "prijs": 299.99,
    "aantalOpVoorraad": 0,
    "garantiejaren": 2
  },
  "isVoorradig": false,
  "aantalVanDezelfdeSoort": 1,
  "isGoedkoopsteVanDeSoort": true
}
```

Controleer tot slot:

- of de geschiedenis van je migraties in de database precies twee migraties bevat: `InitialCreate` en `GarantieToegevoegd`;
- of je in je terminal bij elk verzoek de SQL ziet die Entity Framework Core naar de database stuurt. Kijk bij het zoeken op merk of de vergelijking zonder hoofdletters in die SQL staat;
- of je logberichten er nog staan, met dezelfde niveaus als in 03_01.
