# 04_07

<b>Hoofdstuk 04</b> &nbsp;·&nbsp; Een API rechtstreeks op de DbContext &nbsp;·&nbsp; Herstelplaats &nbsp;·&nbsp; examenstijl &nbsp;·&nbsp; niveau <span style="color:#f08c00">●●○</span> &nbsp;·&nbsp; ±2 uur

## Leerdoel

Na deze oefening kan je een volledige API bouwen op een PostgreSQL-database, met een DbContext die je controller rechtstreeks binnenkrijgt, zoals de schoolopgaven van hoofdstuk 4 het vragen.

Je leert een opgave in de stijl van het examen letterlijk volgen: vaste namen voor je klassen en namespaces, routes zonder `api/`, startgegevens met `HasData`, een Id die je API zelf berekent, en in elk endpoint een logbericht met een vast niveau.

Daarnaast doorloop je een volledige opgave in de vorm van het examen, van een leeg project tot een werkende API, zonder de tussenstappen van de andere reeksen.

## 

## Opdracht

Bit & Byte opent naast de winkel een herstelplaats. Klanten brengen er een laptop, een desktop, een printer of een smartphone binnen, en een technieker zoekt uit wat er mis is. De herstelplaats wil van bij het begin een API op een echte database.

Jouw taak is om een nieuw project op te zetten, de database met haar startgegevens via één migratie te laten maken, en een controller te bouwen die rechtstreeks met de DbContext werkt.

Via de API moet een medewerker:

1. alle herstellingen en één herstelling kunnen opvragen;
2. de herstellingen met een bepaalde status kunnen opvragen;
3. een herstelling kunnen toevoegen, bijwerken en verwijderen;
4. alle techniekers kunnen opvragen.

> [!NOTE]
> De reeks _07 volgt de opbouw van de schoolopgaven, tot in de namen. Ze bouwt niet verder op een vorige oefening: je begint met een leeg project. In 05_07 en 06_07 werk je in hetzelfde project verder.

Implementeer onderstaande functionaliteiten.

### 1. Het project

Maak een nieuw ASP.NET Core Web API-project met controllers. Noem het `WebApi`, en zet het in een eigen map `Herstelplaats`, los van je project van de andere reeksen.

Gebruik de namespaces van de schoolopgaven:

| Map         | Namespace            |
|-------------|----------------------|
| Models      | `WebApi.Models`      |
| Data        | `WebApi.Data`        |
| Controllers | `WebApi.Controllers` |

Zet Scalar op zoals in les 02, zodat je je endpoints kan testen.

> [!IMPORTANT]
> Op het examen liggen de namen van je klassen, je properties, je namespaces en je routes vast. De testen bij een opgave zoeken ze letterlijk. Een andere naam is voor een test een fout, ook als je API werkt.

---

### 2. De modellen

Maak in de map Models een klasse `Herstelling`:

| Property  | C#-type  | Betekenis |
|-----------|----------|-----------|
| Id        | `int`    | de unieke Id van de herstelling |
| KlantNaam | `string` | de naam van de klant |
| Toestel   | `string` | het soort toestel, zoals `Laptop` of `Printer` |
| Probleem  | `string` | wat de klant meldt |
| Status    | `string` | `Ontvangen`, `In behandeling`, `Klaar` of `Afgehaald` |
| Kostprijs | `double` | de prijs voor het werk aan het toestel |

En een klasse `Technieker`:

| Property      | C#-type  | Betekenis |
|---------------|----------|-----------|
| Id            | `int`    | de unieke Id van de technieker |
| Naam          | `string` | de volledige naam |
| Specialisatie | `string` | de toestellen waarin de technieker het sterkst is |

> [!TIP]
> Een bedrag is hier een `double`, zoals in bijna elke schoolopgave. Een `double` onthoudt geen nullen achter de komma: `189.50` in je startgegevens wordt `189.5` in je JSON.

---

### 3. De database

Start PostgreSQL in Docker, zoals in les 04. Je container uit de les of uit de andere reeksen mag je hergebruiken. Deze oefening krijgt wel een eigen database: `HerstelplaatsDb`.

Installeer de packages uit les 04 en zet in `appsettings.json` een connectiestring met de naam `PostgresConnection`, die naar `HerstelplaatsDb` wijst.

---

### 4. De DbContext

Maak in de map Data een klasse `HerstelplaatsContext` die erft van `DbContext`. Ze heeft een constructor die de opties doorgeeft aan de basisklasse, en twee `DbSet`'s: `Herstellingen` en `Techniekers`.

Leg in `OnModelCreating` met de Fluent API vast hoe de tabellen eruitzien.

De tabel `Herstellingen`:

| Property  | Kolom in de database |
|-----------|----------------------|
| Id        | de sleutel |
| KlantNaam | tekst van hoogstens 100 tekens, verplicht |
| Toestel   | tekst van hoogstens 50 tekens, verplicht |
| Probleem  | tekst van hoogstens 200 tekens, verplicht |
| Status    | tekst van hoogstens 30 tekens, verplicht |
| Kostprijs | kommagetal, verplicht |

De tabel `Techniekers`:

| Property      | Kolom in de database |
|---------------|----------------------|
| Id            | de sleutel |
| Naam          | tekst van hoogstens 100 tekens, verplicht |
| Specialisatie | tekst van hoogstens 50 tekens, verplicht |

Registreer je `HerstelplaatsContext` in Program.cs met de PostgreSQL-provider en de connectiestring uit punt 3.

> [!NOTE]
> In de schoolopgaven zie je soms `HasPrecision(18, 2)` bij een `double`, zoals bij de bestellingen in 05_04 van school. Vraagt een opgave het, voeg het dan toe zoals gevraagd, en kijk in je migratie welk kolomtype er komt. Je leerboek bespreekt het in hoofdstuk 37.

---

### 5. De startgegevens

Leg deze startgegevens vast in `OnModelCreating`, met `HasData`, zodat ze met je migratie in de database komen.

De herstellingen:

| Id | KlantNaam | Toestel    | Probleem                | Status         | Kostprijs |
|----|-----------|------------|-------------------------|----------------|-----------|
| 1  | Peeters   | Laptop     | Scherm gebarsten        | In behandeling | 189.50    |
| 2  | Janssens  | Desktop    | Start niet op           | Ontvangen      | 75.00     |
| 3  | Maes      | Laptop     | Batterij laadt niet op  | Klaar          | 129.75    |
| 4  | Claes     | Printer    | Papier loopt vast       | Afgehaald      | 45.25     |
| 5  | Wouters   | Smartphone | Laadpoort defect        | Klaar          | 59.50     |
| 6  | Willems   | Desktop    | Ventilator maakt lawaai | In behandeling | 35.50     |

De techniekers:

| Id | Naam          | Specialisatie  |
|----|---------------|----------------|
| 1  | Sven Mertens  | Laptops        |
| 2  | Nora Aerts    | Desktops       |
| 3  | Bram De Wilde | Randapparatuur |
| 4  | Lotte Vos     | Netwerken      |

> [!NOTE]
> Les 04 toont `HasData` niet. De schoolopgaven van hoofdstuk 4 gebruiken het wel. Je leerboek legt het uit in hoofdstuk 39: lees daar de regels voor je begint.

> [!TIP]
> Je leerboek zet in zijn voorbeelden een `m` achter een bedrag. Die `m` hoort bij een `decimal`. Een `double` schrijf je zonder letter.

---

### 6. De migratie

Maak een migratie met de naam `InitialCreate` en werk daarmee de database bij.

Controleer daarna in pgAdmin of in de PostgreSQL-extensie van VS Code:

- of de tabellen `Herstellingen` en `Techniekers` bestaan, met de lengtes uit punt 4;
- of ze de startgegevens uit punt 5 bevatten;
- of de geschiedenis van je migraties precies één migratie bevat: `InitialCreate`.

> [!TIP]
> Open de migratie voor je de database bijwerkt. Onderaan `Up` zie je je startgegevens terug. Staan ze er niet, dan komen ze ook niet in je database.

---

### 7. De techniekers

Maak een `TechniekerController` die luistert op <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">techniekers</code> en je `HerstelplaatsContext` via zijn constructor binnenkrijgt.

<span style="background:#1c7ed6;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>GET</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">techniekers</code> geeft alle techniekers, op Id gesorteerd, met <code style="color:#37b24d;font-weight:600">200 OK</code>.

> [!WARNING]
> Je routes beginnen hier niet met `api/`, en ze staan in het meervoud. Zet de route letterlijk in je controller. Met `[controller]` in je route krijg je de naam van je klasse, `Technieker`, en niet `techniekers`.

---

### 8. De herstellingen opvragen

Maak een `HerstellingController` die luistert op <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">herstellingen</code>. Hij krijgt via zijn constructor je `HerstelplaatsContext` en een `ILogger<HerstellingController>`.

Er komt in deze oefening geen repository tussen: de controller werkt rechtstreeks met de context, zoals de schoolopgaven van hoofdstuk 4 het vragen.

| Endpoint | Antwoord |
|----------|----------|
| <span style="background:#1c7ed6;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>GET</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">herstellingen</code> | <code style="color:#37b24d;font-weight:600">200 OK</code> met alle herstellingen, op Id gesorteerd |
| <span style="background:#1c7ed6;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>GET</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">herstellingen/<span style="color:#e64980"><b>{id}</b></span></code> | <code style="color:#37b24d;font-weight:600">200 OK</code> met de herstelling, of <code style="color:#f08c00;font-weight:600">404 Not Found</code> zonder body |
| <span style="background:#1c7ed6;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>GET</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">herstellingen/status/<span style="color:#e64980"><b>{status}</b></span></code> | <code style="color:#37b24d;font-weight:600">200 OK</code> met de herstellingen met die status, op Id gesorteerd |

Vindt het derde endpoint geen enkele herstelling, dan geeft het een lege lijst `[]` met <code style="color:#37b24d;font-weight:600">200 OK</code>, en geen <code style="color:#f08c00;font-weight:600">404</code>. De status vergelijk je zonder op hoofdletters te letten: `klaar`, `Klaar` en `KLAAR` geven hetzelfde antwoord.

<code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> herstellingen/3</code> geeft als resultaat:

```json
{
  "id": 3,
  "klantNaam": "Maes",
  "toestel": "Laptop",
  "probleem": "Batterij laadt niet op",
  "status": "Klaar",
  "kostprijs": 129.75
}
```

> [!NOTE]
> "Zonder body" betekent in de schoolopgaven dat jij er geen bericht in zet. Met `[ApiController]` vult ASP.NET Core de body van een <code style="color:#f08c00;font-weight:600">404</code> zelf met een klein foutobject. Dat is in orde.

> [!WARNING]
> Vergelijk zonder hoofdletters op een manier die de database zelf kan uitvoeren. Kijk in je terminal naar de SQL die Entity Framework Core verstuurt: je vergelijking moet erin staan. Krijg je een <code style="color:#f03e3e;font-weight:600">500</code> met *could not be translated*, kies dan een andere vorm.

---

### 9. Toevoegen, bijwerken en verwijderen

| Endpoint | Antwoord |
|----------|----------|
| <span style="background:#2f9e44;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>POST</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">herstellingen</code> | <code style="color:#37b24d;font-weight:600">201 Created</code> met de nieuwe herstelling |
| <span style="background:#d9822b;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>PUT</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">herstellingen/<span style="color:#e64980"><b>{id}</b></span></code> | <code style="color:#37b24d;font-weight:600">204 No Content</code>, of <code style="color:#f08c00;font-weight:600">404 Not Found</code> zonder body |
| <span style="background:#e03131;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>DELETE</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">herstellingen/<span style="color:#e64980"><b>{id}</b></span></code> | <code style="color:#37b24d;font-weight:600">204 No Content</code>, of <code style="color:#f08c00;font-weight:600">404 Not Found</code> zonder body |

Bij een toevoeging berekent je API zelf de Id: de hoogste Id in de tabel, plus één. Een Id in de body negeer je altijd. Geef ook het adres mee waarop de nieuwe herstelling opgevraagd kan worden.

Bij het bijwerken overschrijf je alle properties met de gegevens uit de body, behalve de Id. Welke herstelling je bijwerkt, bepaalt de route, niet de body.

> [!IMPORTANT]
> Ook in een lege tabel moet een toevoeging lukken: de eerste herstelling krijgt dan Id 1. Vraag je af wat de database antwoordt als je de hoogste Id vraagt van een tabel zonder rijen, en laat je API daar niet op vastlopen.

> [!NOTE]
> Omdat je API de Id zelf berekent, speelt de teller van PostgreSQL hier geen rol. In de reeks _01 tot en met _03 kiest de database de Id. Beide komen voor: volg wat de opgave vraagt.

---

### 10. Alles samen: logging in elk endpoint

<span style="background:#7048e8;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>SAMENGESTELDE OEFENING</b></span>

De vorige punten gaven je een werkende API. De schoolopgaven vragen bovendien in elk endpoint een logbericht met een vast niveau. In dit laatste punt voeg je die logging toe, en doorloop je de hele API in één reeks.

| Endpoint | Bij elk verzoek | Als de herstelling niet bestaat |
|----------|-----------------|---------------------------------|
| <span style="background:#1c7ed6;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>GET</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">herstellingen</code> | `Information` | — |
| <span style="background:#1c7ed6;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>GET</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">herstellingen/<span style="color:#e64980"><b>{id}</b></span></code> | `Information` | `Warning` |
| <span style="background:#1c7ed6;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>GET</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">herstellingen/status/<span style="color:#e64980"><b>{status}</b></span></code> | `Information` | — |
| <span style="background:#2f9e44;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>POST</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">herstellingen</code> | `Information`, bij het aanmaken | — |
| <span style="background:#d9822b;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>PUT</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">herstellingen/<span style="color:#e64980"><b>{id}</b></span></code> | `Information` | `Warning` |
| <span style="background:#e03131;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>DELETE</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">herstellingen/<span style="color:#e64980"><b>{id}</b></span></code> | `Information` | `Error` |

De tekst van elk bericht kies je zelf. Zet er de Id of de status in, zodat je in je terminal ziet over welk verzoek het gaat.

> [!WARNING]
> Kijk goed naar het niveau bij een DELETE die niets vindt: dat is `Error`, niet `Warning`. Het is het enige endpoint waar de schoolopgave een ander niveau vraagt.

#### Testscenario

Begin met een database die exact de startgegevens bevat, en doorloop deze reeks. Wil je ze later herhalen, verwijder dan je database en werk ze opnieuw bij met je migratie.

1. <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> herstellingen</code> geeft de zes herstellingen, met Id 1 tot en met 6, in die volgorde.
2. <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> herstellingen/99</code> geeft <code style="color:#f08c00;font-weight:600">404</code>. In je terminal staat een `Warning`.
3. <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> herstellingen/status/klaar</code> geeft de herstellingen 3 en 5. <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> herstellingen/status/IN BEHANDELING</code> geeft 1 en 6; Scalar zet de spatie zelf om. <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> herstellingen/status/geannuleerd</code> geeft `[]`, met <code style="color:#37b24d;font-weight:600">200</code>.
4. <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> techniekers</code> geeft de vier techniekers, op Id gesorteerd.
5. Voeg deze herstelling toe:

   ```json
   { "id": 50, "klantNaam": "Hermans", "toestel": "Tablet", "probleem": "Scherm reageert niet", "status": "Ontvangen", "kostprijs": 64.5 }
   ```

   Je krijgt <code style="color:#37b24d;font-weight:600">201</code>, met Id 7. De header `Location` eindigt op `/herstellingen/7`; hoofdletters tellen daarbij niet.
6. Werk herstelling 2 bij:

   ```json
   { "klantNaam": "Janssens", "toestel": "Desktop", "probleem": "Start niet op", "status": "Klaar", "kostprijs": 80.25 }
   ```

   Je krijgt <code style="color:#37b24d;font-weight:600">204</code>. <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> herstellingen/status/Klaar</code> geeft nu 2, 3 en 5, en herstelling 2 kost `80.25`.
7. Werk herstelling 99 bij met dezelfde body. Je krijgt <code style="color:#f08c00;font-weight:600">404</code>, en in je terminal een `Warning`. <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> herstellingen</code> geeft nog altijd zeven herstellingen.
8. Verwijder herstelling 7. Je krijgt <code style="color:#37b24d;font-weight:600">204</code>. Verwijder ze nog eens: nu krijg je <code style="color:#f08c00;font-weight:600">404</code>, en in je terminal een `Error`.
9. Voeg de herstelling uit stap 5 opnieuw toe. Ze krijgt opnieuw Id 7.
10. Bekijk je terminal. Bij elk verzoek uit deze reeks staat een `Information`, en daarnaast precies twee keer een `Warning` en één keer een `Error`.

> [!NOTE]
> Stap 9 toont een gevolg van "de hoogste Id plus één": een Id die vrijkwam, kan terugkomen. Een database met een eigen teller doet dat niet. Op het examen volg je de opgave, maar het is goed dat je het verschil kent.
