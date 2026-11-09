# 06_01

<b>Hoofdstuk 06</b> &nbsp;·&nbsp; Een één-op-veel-relatie &nbsp;·&nbsp; PC-onderdelen &nbsp;·&nbsp; niveau <span style="color:#37b24d">●○○</span> &nbsp;·&nbsp; ±2 uur

## Leerdoel

Na deze oefening kan je twee modellen met elkaar verbinden in een één-op-veel-relatie, met een vreemde sleutel en navigation properties.

Je leert de relatie volledig vastleggen met de Fluent API, zoals de schoolopgaven het vragen, kiezen wat er gebeurt als iemand de "één"-kant wil verwijderen, en startgegevens meegeven voor twee tabellen, ook met een datum.

Daarnaast merk je dat de database een relatie zelf bewaakt: wat de relatie verbiedt, moet je API vooraf netjes weigeren.

## 

## Opdracht

Bit & Byte koopt zijn artikelen bij een handvol leveranciers. Tot nu toe stond nergens bij wie. Elk artikel komt voortaan van precies één leverancier, en één leverancier levert meerdere artikelen. De inkoop wil ook weten sinds wanneer een leverancier partner is.

Jouw taak is om een model `Leverancier` toe te voegen, het te verbinden met `Artikel`, en de leveranciers via de API beschikbaar te maken.

Via de API moet de website, net als in 05_01, alle artikelen kunnen opvragen, filteren, tellen en de fiche tonen. Daarnaast moet ze:

1. alle leveranciers en één leverancier kunnen opvragen;
2. alle artikelen van één leverancier kunnen opvragen, en tellen;
3. een leverancier kunnen toevoegen;
4. een leverancier kunnen verwijderen, zolang die geen artikelen meer levert.

Implementeer onderstaande functionaliteiten.

### 1. Je vertrekpunt

Vertrek van je eigen oplossing van 05_01. Alle endpoints uit die oefening blijven werken. Elk artikel verandert wel een beetje, en dat zie je ook in de JSON.

> [!NOTE]
> In deze oefening bouw je je database opnieuw op. Verwijder ze, maak de nieuwe migratie uit punt 4, en laat je database daarna volledig opbouwen door al je migraties.

---

### 2. De modellen

Maak in de map Models een klasse `Leverancier`:

| Property     | C#-type          | Kolom in de database |
|--------------|------------------|----------------------|
| Id           | `int`            | de sleutel, de database telt zelf op |
| Naam         | `string`         | tekst van hoogstens 50 tekens, verplicht |
| Land         | `string`         | tekst van hoogstens 30 tekens, verplicht |
| PartnerSinds | `DateTime`       | datum en uur, verplicht |
| Artikelen    | `List<Artikel>?` | navigation property, geen kolom |

Voeg aan `Artikel` toe:

| Property      | C#-type        | Kolom in de database |
|---------------|----------------|----------------------|
| LeverancierId | `int`          | vreemde sleutel naar `Leveranciers`, verplicht |
| Leverancier   | `Leverancier?` | navigation property, geen kolom |

De tabel heet `Leveranciers`. Voeg ze toe aan je `ArtikelContext`.

> [!NOTE]
> Let op het vraagteken bij de navigation properties: de client stuurt ze nooit mee. Scalar vult ze in zijn voorbeeld wel in. Haal ze daar weg voor je een verzoek verstuurt.

---

### 3. De relatie

Leg in `OnModelCreating` met de Fluent API de relatie volledig vast, zoals de schoolopgaven van hoofdstuk 6 het vragen:

- een artikel heeft één leverancier, en een leverancier heeft veel artikelen;
- `LeverancierId` is de vreemde sleutel;
- een leverancier die nog artikelen levert, mag niet verwijderd kunnen worden, ook niet rechtstreeks in de database;
- de relatie is verplicht.

> [!TIP]
> In de schoolopgaven zie je de vijf stappen van zo'n relatie telkens apart opgesomd: `HasOne`, `WithMany`, `HasForeignKey`, `OnDelete` en `IsRequired`. Zoek bij elke stap uit welk stuk van de vier zinnen hierboven ze vastlegt.

> [!WARNING]
> Lees in les 06 wat Entity Framework Core uit zichzelf doet als je een leverancier verwijdert. Dat is niet wat Bit & Byte wil. Kijk na het maken van je migratie welke `onDelete` er bij de vreemde sleutel staat.

---

### 4. De startgegevens

Leg met `HasData` deze leveranciers vast:

| Id | Naam            | Land      | PartnerSinds |
|----|-----------------|-----------|--------------|
| 1  | TechDistri      | België    | 2015-03-01   |
| 2  | Componentenhuis | Nederland | 2019-06-15   |
| 3  | ChipCentraal    | Duitsland | 2018-09-01   |
| 4  | Koelwerk        | België    | 2023-01-10   |

Geef elk artikel in je startgegevens zijn leverancier:

| Artikel | LeverancierId |
|---------|---------------|
| 1, 5    | 1             |
| 2, 6    | 2             |
| 3, 4    | 3             |

Leverancier 4 levert nog niets. Maak een migratie met de naam `Leveranciers`, verwijder je database, en bouw ze opnieuw op met al je migraties.

> [!WARNING]
> Een datum in je startgegevens kan PostgreSQL weigeren, met een melding over *Kind=Unspecified* en *only UTC is supported*. Lees in je leerboek, hoofdstuk 39, bij "Let op bij de lescode", wat je dan meegeeft. Hoofdstuk 3 legt uit waarom.

> [!WARNING]
> Startgegevens met een vaste Id vertellen de teller van PostgreSQL niet welke Id's al gebruikt zijn, zoals in 04_02. Na het opbouwen van je database moet de eerste leverancier die je via de API toevoegt, Id 5 krijgen.

> [!TIP]
> Open de migratie voor je de database bijwerkt. Zoek de regels die de kolom `LeverancierId` toevoegen, die elk artikel zijn leverancier geven, en die de vreemde sleutel maken. Staat de vreemde sleutel na de startgegevens? Dan krijgt elk artikel eerst zijn leverancier, en pas daarna controleert de database de relatie.

---

### 5. De repository

Maak een interface `ILeverancierRepository` en een klasse `LeverancierRepository`, asynchroon zoals in les 05, met je context in een `readonly` veld. Registreer ze met `AddScoped`.

| Methode | Parameters | Geeft terug |
|---------|------------|-------------|
| `GetAllAsync` | geen | `Task<List<Leverancier>>` |
| `GetByIdAsync` | `int id` | `Task<Leverancier?>` |
| `CreateAsync` | `Leverancier leverancier` | `Task<Leverancier>` |
| `DeleteAsync` | `int id` | `Task` |
| `GetArtikelenAsync` | `int leverancierId` | `Task<List<Artikel>>` |
| `CountArtikelenAsync` | `int leverancierId` | `Task<int>` |
| `HeeftArtikelenAsync` | `int leverancierId` | `Task<bool>` |

De lijsten zijn op Id gesorteerd. `GetArtikelenAsync` geeft een lege lijst als de leverancier niets levert. `DeleteAsync` doet niets als de leverancier niet bestaat. Tellen doe je met `CountAsync`, nagaan of er iets is met `AnyAsync`.

---

### 6. De endpoints

Maak een `LeverancierController` die luistert op <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/leverancier</code>, met een `ILeverancierRepository` en een logger.

| Verzoek | Antwoord |
|---------|----------|
| <span style="background:#1c7ed6;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>GET</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/leverancier</code> | <code style="color:#37b24d;font-weight:600">200 OK</code> met alle leveranciers, op Id gesorteerd |
| <span style="background:#1c7ed6;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>GET</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/leverancier/<span style="color:#e64980"><b>{id}</b></span></code> | <code style="color:#37b24d;font-weight:600">200 OK</code> met de leverancier, of <code style="color:#f08c00;font-weight:600">404 Not Found</code> met <code style="color:#845ef7">We vonden geen leverancier met Id <span style="color:#e64980"><b>{id}</b></span>.</code> |
| <span style="background:#1c7ed6;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>GET</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/leverancier/<span style="color:#e64980"><b>{id}</b></span>/artikelen</code> | <code style="color:#37b24d;font-weight:600">200 OK</code> met de artikelen van die leverancier, op Id gesorteerd, of <code style="color:#f08c00;font-weight:600">404 Not Found</code> met hetzelfde bericht als hierboven |
| <span style="background:#1c7ed6;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>GET</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/leverancier/<span style="color:#e64980"><b>{id}</b></span>/artikelen/tellen</code> | <code style="color:#37b24d;font-weight:600">200 OK</code> met een object met een property `count`, of <code style="color:#f08c00;font-weight:600">404 Not Found</code> met hetzelfde bericht als hierboven |
| <span style="background:#2f9e44;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>POST</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/leverancier</code> | <code style="color:#37b24d;font-weight:600">201 Created</code> met de nieuwe leverancier en haar adres, of <code style="color:#f08c00;font-weight:600">400 Bad Request</code> met <code style="color:#845ef7">Een leverancier moet een naam hebben.</code> |

Een leverancier zonder artikelen geeft bij het derde endpoint een lege lijst, en bij het vierde `{ "count": 0 }`, telkens met <code style="color:#37b24d;font-weight:600">200 OK</code>. Log zoals in 05_01: elk verzoek op niveau Information, elke <code style="color:#f08c00;font-weight:600">400</code> en <code style="color:#f08c00;font-weight:600">404</code> op niveau Warning.

<code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> api/leverancier/3</code> geeft als resultaat:

```json
{
  "id": 3,
  "naam": "ChipCentraal",
  "land": "Duitsland",
  "partnerSinds": "2018-09-01T00:00:00Z",
  "artikelen": null
}
```

<code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> api/artikel/4</code> geeft nu als resultaat:

```json
{
  "id": 4,
  "naam": "Ryzen 7 7800X3D",
  "merk": "AMD",
  "soort": "processor",
  "prijs": 389,
  "aantalOpVoorraad": 0,
  "garantiejaren": 3,
  "leverancierId": 3,
  "leverancier": null
}
```

> [!NOTE]
> De navigation properties zijn leeg in je JSON. Entity Framework Core haalt gerelateerde gegevens niet uit zichzelf op. Hoe je dat wél doet, zie je in les 08.

> [!NOTE]
> De `Z` achter de datum betekent UTC. Stuur je zelf een datum mee, schrijf hem dan ook zo: `2026-11-07T00:00:00Z`. Zonder `Z` weet de database niet welke tijdzone je bedoelt.

> [!WARNING]
> Krijg je bij <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> api/leverancier/3/artikelen</code> een <code style="color:#f03e3e;font-weight:600">500</code> met een melding over een *object cycle*? Dan kent elk artikel zijn leverancier, en kent die leverancier haar artikelen weer, zonder einde. Les 08 legt uit waarom. Haal in hetzelfde verzoek niet meer objecten op dan het endpoint teruggeeft.

---

### 7. Alles samen: een leverancier verwijderen

<span style="background:#7048e8;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>SAMENGESTELDE OEFENING</b></span>

De relatie, de keuze bij het verwijderen, de startgegevens en de repository komen samen in één endpoint: een leverancier verwijderen. Het mag enkel lukken als ze geen artikelen meer levert.

<span style="background:#e03131;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>DELETE</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/leverancier/<span style="color:#e64980"><b>{id}</b></span></code>

- De leverancier bestaat niet: <code style="color:#f08c00;font-weight:600">404 Not Found</code> met <code style="color:#845ef7">We vonden geen leverancier met Id <span style="color:#e64980"><b>{id}</b></span>.</code>, gelogd op niveau `Error`, zoals de schoolopgaven dat bij een DELETE vragen.
- De leverancier levert nog artikelen: <code style="color:#f08c00;font-weight:600">400 Bad Request</code> met <code style="color:#845ef7">Deze leverancier levert nog artikelen.</code>
- Anders: verwijder de leverancier en geef <code style="color:#37b24d;font-weight:600">204 No Content</code> terug.

> [!IMPORTANT]
> De database weigert zelf ook een leverancier met artikelen, dankzij je keuze in punt 3. Maar laat het niet zover komen: dan krijgt de website een <code style="color:#f03e3e;font-weight:600">500</code>. Je API kijkt eerst, en antwoordt netjes.

#### Testscenario

Bouw je database opnieuw op, zodat ze exact de startgegevens bevat, en doorloop deze reeks:

1. Verwijder leverancier 3. Je krijgt <code style="color:#f08c00;font-weight:600">400</code> met het bericht hierboven. <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> api/artikel/soort/processor</code> geeft nog altijd de artikelen 3 en 4.
2. Probeer in pgAdmin of in VS Code leverancier 3 rechtstreeks uit de tabel te verwijderen. De database weigert, met een melding over de vreemde sleutel.
3. Verwijder leverancier 4. Je krijgt <code style="color:#37b24d;font-weight:600">204</code>. Doe het nog eens: nu krijg je <code style="color:#f08c00;font-weight:600">404</code>, en in je terminal staat een `Error`.
4. Voeg deze leverancier toe:

   ```json
   { "naam": "Kabelkoning", "land": "België", "partnerSinds": "2026-11-07T00:00:00Z" }
   ```

   Je krijgt <code style="color:#37b24d;font-weight:600">201</code>, en de nieuwe leverancier heeft Id 5. De header `Location` eindigt op `/api/leverancier/5`; hoofdletters tellen daarbij niet.
5. <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> api/leverancier</code> geeft de leveranciers 1, 2, 3 en 5, in die volgorde. Leverancier 5 is partner sinds `2026-11-07T00:00:00Z`.
6. <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> api/leverancier/5/artikelen</code> geeft `[]`, en <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> api/leverancier/5/artikelen/tellen</code> geeft `{ "count": 0 }`. Voor leverancier 1 is dat `{ "count": 2 }`.
7. <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> api/leverancier/99/artikelen</code> geeft <code style="color:#f08c00;font-weight:600">404</code> met <code style="color:#845ef7">We vonden geen leverancier met Id 99.</code>
8. Controleer in pgAdmin of in VS Code dat de kolom `PartnerSinds` een datum met tijdzone is, en dat de geschiedenis van je migraties eindigt met `Leveranciers`.
