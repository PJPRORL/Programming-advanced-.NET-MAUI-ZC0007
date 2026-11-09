# 06_07

<b>Hoofdstuk 06</b> &nbsp;·&nbsp; Relaties en een tussentabel &nbsp;·&nbsp; Herstelplaats &nbsp;·&nbsp; examenstijl &nbsp;·&nbsp; niveau <span style="color:#f03e3e">●●●</span> &nbsp;·&nbsp; ±2,5 uur

## Leerdoel

Na deze oefening kan je vier modellen met elkaar verbinden: een één-op-veel-relatie, en een veel-op-veel-relatie via een tussentabel met een eigen gegeven.

Je leert alle relaties vastleggen met de Fluent API, startgegevens voor vier tabellen meegeven met `HasData`, ook met een datum, en in een query via een navigation property rekenen met gegevens uit een andere tabel.

Daarnaast merk je dat een `decimal` en een `double` niet zomaar samen rekenen, en dat een relatie met `Restrict` een verwijdering kan tegenhouden die je API vooraf moet opvangen.

## 

## Opdracht

Tot nu toe stond een herstelling op zichzelf. De herstelplaats wil weten welke technieker aan welke herstelling werkt, en welke onderdelen er in elk toestel gaan. Een technieker werkt aan meerdere herstellingen. Een herstelling kan meerdere onderdelen nodig hebben, en hetzelfde onderdeel komt in meerdere herstellingen voor, telkens in een bepaald aantal.

Jouw taak is om de modellen te verbinden, je database opnieuw op te bouwen met alle startgegevens, en je repository en je controller uit te breiden met query's die over de relaties gaan.

Via de API moet een medewerker, net als in 05_07, de herstellingen beheren, opvragen en de cijfers zien. Daarnaast moet hij:

1. de herstellingen van één technieker kunnen opvragen;
2. de onderdelen van één herstelling kunnen opvragen;
3. de prijs van de onderdelen van één herstelling kunnen opvragen;
4. bij elke herstelling een technieker kunnen kiezen, maar enkel een die bestaat;
5. een herstelling enkel kunnen verwijderen als er geen onderdelen meer aan hangen.

Implementeer onderstaande functionaliteiten.

### 1. Je vertrekpunt

Vertrek van je eigen oplossing van 05_07. Alle endpoints uit die oefening blijven werken. Elke herstelling verandert wel een beetje, en dat zie je ook in de JSON.

> [!NOTE]
> In deze oefening bouw je je database opnieuw op. Verwijder je database `HerstelplaatsDb` en je map Migrations. Op het einde van punt 4 maak je één nieuwe migratie `InitialCreate`, zoals in de schoolopgaven.

---

### 2. De modellen

Pas `Technieker` aan. Er komen twee properties bij:

| Property      | C#-type               | Kolom in de database |
|---------------|-----------------------|----------------------|
| InDienstSinds | `DateTime`            | datum en uur, verplicht |
| Herstellingen | `List<Herstelling>?`  | navigation property, geen kolom |

Pas `Herstelling` aan. Er komen drie properties bij:

| Property          | C#-type                   | Kolom in de database |
|-------------------|---------------------------|----------------------|
| TechniekerId      | `int`                     | vreemde sleutel naar `Techniekers`, verplicht |
| Technieker        | `Technieker?`             | navigation property, geen kolom |
| HerstelOnderdelen | `List<HerstelOnderdeel>?` | navigation property, geen kolom |

Maak een klasse `Onderdeel`:

| Property          | C#-type                   | Kolom in de database |
|-------------------|---------------------------|----------------------|
| Id                | `int`                     | de sleutel |
| Naam              | `string`                  | tekst van hoogstens 100 tekens, verplicht |
| Beschrijving      | `string?`                 | tekst van hoogstens 200 tekens, niet verplicht |
| Prijs             | `decimal`                 | getal met 8 cijfers, waarvan 2 na de komma, verplicht |
| HerstelOnderdelen | `List<HerstelOnderdeel>?` | navigation property, geen kolom |

En de tussentabel, een klasse `HerstelOnderdeel`:

| Property      | C#-type        | Kolom in de database |
|---------------|----------------|----------------------|
| Id            | `int`          | de sleutel |
| HerstellingId | `int`          | vreemde sleutel naar `Herstellingen`, verplicht |
| Herstelling   | `Herstelling?` | navigation property, geen kolom |
| OnderdeelId   | `int`          | vreemde sleutel naar `Onderdelen`, verplicht |
| Onderdeel     | `Onderdeel?`   | navigation property, geen kolom |
| Aantal        | `double`       | kommagetal, verplicht |

Voeg aan je `HerstelplaatsContext` twee `DbSet`'s toe: `Onderdelen` en `HerstelOnderdelen`. De tabellen heten ook zo.

> [!NOTE]
> De prijs van een onderdeel is hier een `decimal`, het aantal een `double`. Dat is geen vergissing: de schoolopgaven van hoofdstuk 6 doen precies hetzelfde, met de prijs van een product en het aantal op een orderlijn. Het aantal is een `double` omdat je ook een halve tube koelpasta kan gebruiken.

> [!NOTE]
> Let op het vraagteken bij de navigation properties: de client stuurt ze nooit mee. Scalar vult ze in zijn voorbeeld wel in. Haal ze daar weg voor je een verzoek verstuurt.

---

### 3. De relaties

Leg in `OnModelCreating` met de Fluent API drie relaties vast. Elke relatie is verplicht, en elke "één"-kant mag niet verwijderd kunnen worden zolang er nog iets aan hangt. Ook niet rechtstreeks in de database.

| Relatie | Vreemde sleutel |
|---------|-----------------|
| een technieker heeft veel herstellingen, een herstelling heeft één technieker | `TechniekerId` in `Herstellingen` |
| een herstelling heeft veel herstelonderdelen, een herstelonderdeel hoort bij één herstelling | `HerstellingId` in `HerstelOnderdelen` |
| een onderdeel komt in veel herstelonderdelen voor, een herstelonderdeel heeft één onderdeel | `OnderdeelId` in `HerstelOnderdelen` |

De laatste twee samen vormen de veel-op-veel-relatie tussen herstellingen en onderdelen.

> [!WARNING]
> Lees in les 06 wat Entity Framework Core uit zichzelf doet als je de "één"-kant van een relatie verwijdert. Dat is niet wat de herstelplaats wil. Kijk na het maken van je migratie bij elke vreemde sleutel welke `onDelete` er staat.

---

### 4. De startgegevens

Leg met `HasData` deze startgegevens vast. De herstellingen zijn die uit 04_07, nu met een technieker.

De techniekers:

| Id | Naam          | Specialisatie  | InDienstSinds |
|----|---------------|----------------|---------------|
| 1  | Sven Mertens  | Laptops        | 2019-03-01    |
| 2  | Nora Aerts    | Desktops       | 2021-09-15    |
| 3  | Bram De Wilde | Randapparatuur | 2024-01-08    |
| 4  | Lotte Vos     | Netwerken      | 2025-06-30    |

De herstellingen:

| Id | KlantNaam | Toestel    | Probleem                | Status         | Kostprijs | TechniekerId |
|----|-----------|------------|-------------------------|----------------|-----------|--------------|
| 1  | Peeters   | Laptop     | Scherm gebarsten        | In behandeling | 189.50    | 1            |
| 2  | Janssens  | Desktop    | Start niet op           | Ontvangen      | 75.00     | 2            |
| 3  | Maes      | Laptop     | Batterij laadt niet op  | Klaar          | 129.75    | 1            |
| 4  | Claes     | Printer    | Papier loopt vast       | Afgehaald      | 45.25     | 3            |
| 5  | Wouters   | Smartphone | Laadpoort defect        | Klaar          | 59.50     | 3            |
| 6  | Willems   | Desktop    | Ventilator maakt lawaai | In behandeling | 35.50     | 2            |

De onderdelen:

| Id | Naam                   | Beschrijving     | Prijs  |
|----|------------------------|------------------|--------|
| 1  | Laptopscherm 15,6 inch | Full HD, mat     | 119.00 |
| 2  | Laptopbatterij         | 4 cellen, 56 Wh  | 69.50  |
| 3  | Voeding 500 W          | geen             | 54.75  |
| 4  | Ventilator 120 mm      | Stil, 3 pinnen   | 12.25  |
| 5  | Thermische pasta       | geen             | 7.50   |
| 6  | Laadpoort USB-C        | Voor smartphones | 18.00  |

"geen" betekent: geen beschrijving, dus `null`.

De herstelonderdelen:

| Id | HerstellingId | OnderdeelId | Aantal |
|----|---------------|-------------|--------|
| 1  | 1             | 1           | 1      |
| 2  | 1             | 5           | 0.5    |
| 3  | 3             | 2           | 1      |
| 4  | 6             | 5           | 0.5    |
| 5  | 6             | 4           | 2      |
| 6  | 2             | 3           | 1      |
| 7  | 5             | 6           | 1      |

Herstelling 4 heeft geen onderdelen, en technieker 4 heeft nog geen herstellingen.

Maak een migratie met de naam `InitialCreate` en bouw daarmee je database op.

> [!WARNING]
> Een datum in je startgegevens kan PostgreSQL weigeren, met een melding over *Kind=Unspecified* en *only UTC is supported*. Lees in je leerboek, hoofdstuk 39, bij "Let op bij de lescode", wat je dan meegeeft. Hoofdstuk 3 legt uit waarom.

> [!TIP]
> Nu staat er wel een `decimal` in je startgegevens. Lees in je leerboek, hoofdstuk 39, bij de regels van `HasData`, wat je dan achter het getal zet.

---

### 5. De interface en de repository uitbreiden

Breid `IHerstellingRepository` uit met deze methodes, en implementeer ze in `HerstellingRepository`:

| Methode | Parameters | Geeft terug |
|---------|------------|-------------|
| `GetByTechniekerIdAsync` | `int techniekerId` | `Task<List<Herstelling>>` |
| `GetHerstelOnderdelenAsync` | `int herstellingId` | `Task<List<HerstelOnderdeel>>` |
| `GetTotaalPrijsAsync` | `int herstellingId` | `Task<double>` |
| `TechniekerBestaatAsync` | `int techniekerId` | `Task<bool>` |
| `HeeftOnderdelenAsync` | `int herstellingId` | `Task<bool>` |

| Methode | Wat ze teruggeeft |
|---------|-------------------|
| `GetByTechniekerIdAsync` | de herstellingen van die technieker, op Id gesorteerd; een lege lijst als er geen zijn |
| `GetHerstelOnderdelenAsync` | de herstelonderdelen van die herstelling, gesorteerd op `OnderdeelId`; een lege lijst als er geen zijn |
| `GetTotaalPrijsAsync` | de prijs van alle onderdelen van die herstelling: voor elk herstelonderdeel de prijs van het onderdeel maal het aantal, alles opgeteld, met `SumAsync`; `0` als er geen zijn |
| `TechniekerBestaatAsync` | `true` als er een technieker met die Id is, anders `false` |
| `HeeftOnderdelenAsync` | `true` als er minstens één herstelonderdeel aan die herstelling hangt, anders `false` |

Alle methodes uit 05_07 blijven, met hetzelfde gedrag.

> [!TIP]
> Voor de totaalprijs hoef je geen onderdeel op te halen. In een query mag je via de navigation property van een herstelonderdeel naar de prijs van zijn onderdeel. Entity Framework Core maakt daar zelf een join van.

> [!WARNING]
> C# vermenigvuldigt een `decimal` en een `double` niet zomaar met elkaar: je project bouwt dan niet. Kijk welk type je methode moet teruggeven, en zet het andere getal om naar dat type.

---

### 6. De nieuwe endpoints

| Endpoint | Antwoord |
|----------|----------|
| <span style="background:#1c7ed6;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>GET</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">herstellingen/technieker/<span style="color:#e64980"><b>{techniekerId}</b></span></code> | <code style="color:#37b24d;font-weight:600">200 OK</code> met de herstellingen van die technieker |
| <span style="background:#1c7ed6;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>GET</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">herstellingen/<span style="color:#e64980"><b>{id}</b></span>/onderdelen</code> | <code style="color:#37b24d;font-weight:600">200 OK</code> met de herstelonderdelen van die herstelling |
| <span style="background:#1c7ed6;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>GET</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">herstellingen/<span style="color:#e64980"><b>{id}</b></span>/prijs</code> | <code style="color:#37b24d;font-weight:600">200 OK</code> met een object met een property `totaalPrijs`, een `double` |

Vinden ze niets, ook omdat de technieker of de herstelling niet bestaat, dan geven ze `[]` of `{ "totaalPrijs": 0 }`, met <code style="color:#37b24d;font-weight:600">200 OK</code>. Geen <code style="color:#f08c00;font-weight:600">404</code>: zo vraagt de schoolopgave het. Elk endpoint schrijft bij elk verzoek een logbericht van niveau `Information`.

De `totaalPrijs` gaat enkel over de onderdelen. Het werk, de `Kostprijs`, betaalt de klant apart.

<code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> herstellingen/6/onderdelen</code> geeft als resultaat:

```json
[
  {
    "id": 5,
    "herstellingId": 6,
    "herstelling": null,
    "onderdeelId": 4,
    "onderdeel": null,
    "aantal": 2
  },
  {
    "id": 4,
    "herstellingId": 6,
    "herstelling": null,
    "onderdeelId": 5,
    "onderdeel": null,
    "aantal": 0.5
  }
]
```

<code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> herstellingen/1/prijs</code> geeft als resultaat:

```json
{
  "totaalPrijs": 122.75
}
```

<code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> herstellingen/1</code> geeft nu als resultaat:

```json
{
  "id": 1,
  "klantNaam": "Peeters",
  "toestel": "Laptop",
  "probleem": "Scherm gebarsten",
  "status": "In behandeling",
  "kostprijs": 189.5,
  "techniekerId": 1,
  "technieker": null,
  "herstelOnderdelen": null
}
```

> [!NOTE]
> De navigation properties zijn leeg in je JSON. Entity Framework Core haalt gerelateerde gegevens niet uit zichzelf op. Hoe je dat wél doet, zie je in les 08.

> [!WARNING]
> Krijg je een <code style="color:#f03e3e;font-weight:600">500</code> met een melding over een *object cycle*? Dan kent een herstelonderdeel zijn herstelling, en kent die herstelling haar herstelonderdelen weer, zonder einde. Dat gebeurt als je in hetzelfde verzoek meer objecten ophaalt dan je nodig hebt. Haal enkel op wat het endpoint teruggeeft.

---

### 7. Alles samen: een technieker kiezen en een herstelling verwijderen

<span style="background:#7048e8;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>SAMENGESTELDE OEFENING</b></span>

De relaties, de startgegevens en de repository komen samen in de endpoints die een herstelling toevoegen, bijwerken en verwijderen. Ze werken zoals in 05_07, met twee regels erbij:

- Bij <span style="background:#2f9e44;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>POST</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">herstellingen</code> en <span style="background:#d9822b;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>PUT</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">herstellingen/<span style="color:#e64980"><b>{id}</b></span></code>: bestaat de technieker niet, geef dan <code style="color:#f08c00;font-weight:600">400 Bad Request</code> zonder body, en bewaar niets.
- Bij <span style="background:#e03131;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>DELETE</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">herstellingen/<span style="color:#e64980"><b>{id}</b></span></code>: hangen er nog onderdelen aan de herstelling, geef dan <code style="color:#f08c00;font-weight:600">400 Bad Request</code> zonder body, en verwijder niets.

Bij een <code style="color:#f08c00;font-weight:600">400</code> schrijf je een logbericht van niveau `Warning`. Bestaat de herstelling bij een PUT of een DELETE niet, dan blijft het <code style="color:#f08c00;font-weight:600">404</code>, zoals in 05_07.

> [!IMPORTANT]
> De schoolopgave van hoofdstuk 6 vraagt deze twee regels niet. Zonder die regels weigert de database zelf, dankzij je relaties, en krijgt de client een <code style="color:#f03e3e;font-weight:600">500</code>. Laat het niet zover komen: je API kijkt eerst, en antwoordt netjes.

#### Testscenario

Begin met een database die exact de startgegevens bevat, en doorloop deze reeks. Bedragen vergelijk je als getal.

1. <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> techniekers</code> geeft de vier techniekers. Bij technieker 1 staat `"inDienstSinds": "2019-03-01T00:00:00Z"`.
2. <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> herstellingen/technieker/1</code> geeft de herstellingen 1 en 3. <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> herstellingen/technieker/4</code> en <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> herstellingen/technieker/99</code> geven allebei `[]`.
3. <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> herstellingen/1/onderdelen</code> geeft de herstelonderdelen 1 en 2, in die volgorde. <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> herstellingen/4/onderdelen</code> en <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> herstellingen/99/onderdelen</code> geven allebei `[]`.
4. De `totaalPrijs` is `122.75` voor herstelling 1, `28.25` voor herstelling 6, `0` voor herstelling 4, en `0` voor herstelling 99.
5. Voeg deze herstelling toe:

   ```json
   { "klantNaam": "Hermans", "toestel": "Tablet", "probleem": "Scherm reageert niet", "status": "Ontvangen", "kostprijs": 64.5, "techniekerId": 4 }
   ```

   Je krijgt <code style="color:#37b24d;font-weight:600">201</code>, met Id 7. <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> herstellingen/technieker/4</code> geeft nu herstelling 7.
6. Voeg dezelfde herstelling toe, maar met `"techniekerId": 99`. Je krijgt <code style="color:#f08c00;font-weight:600">400</code>. <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> herstellingen</code> geeft nog altijd zeven herstellingen.
7. Werk herstelling 5 bij:

   ```json
   { "klantNaam": "Wouters", "toestel": "Smartphone", "probleem": "Laadpoort defect", "status": "Klaar", "kostprijs": 59.5, "techniekerId": 2 }
   ```

   Je krijgt <code style="color:#37b24d;font-weight:600">204</code>. <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> herstellingen/technieker/2</code> geeft 2, 5 en 6, en <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> herstellingen/technieker/3</code> enkel nog 4.
8. Werk herstelling 5 bij met dezelfde body, maar met `"techniekerId": 99`. Je krijgt <code style="color:#f08c00;font-weight:600">400</code>, en herstelling 5 hoort nog altijd bij technieker 2.
9. Verwijder herstelling 6. Je krijgt <code style="color:#f08c00;font-weight:600">400</code>. Herstelling 6 bestaat nog, en haar `totaalPrijs` is nog altijd `28.25`.
10. Verwijder herstelling 4 en herstelling 7. Je krijgt telkens <code style="color:#37b24d;font-weight:600">204</code>.
11. Probeer in pgAdmin of in VS Code technieker 1 en onderdeel 5 rechtstreeks uit hun tabel te verwijderen. De database weigert, telkens met een melding over een vreemde sleutel.
12. De cijfers uit 05_07 kloppen nog: de omzet is `489.25`, <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> herstellingen/tellen/status/afgehaald</code> geeft `0`, en <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> herstellingen/exists/status/afgehaald</code> geeft `false`.

Controleer tot slot in pgAdmin of in VS Code:

- of de tabellen `Techniekers`, `Herstellingen`, `Onderdelen` en `HerstelOnderdelen` bestaan, met hun startgegevens;
- of de kolom `Prijs` van `Onderdelen` twee cijfers na de komma bewaart, en `InDienstSinds` een datum met tijdzone is;
- of de geschiedenis van je migraties precies één migratie bevat: `InitialCreate`.
