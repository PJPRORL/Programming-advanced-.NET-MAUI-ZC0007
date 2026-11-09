# 06_04

<b>Hoofdstuk 06</b> &nbsp;·&nbsp; Een één-op-veel-relatie, in schoolstijl &nbsp;·&nbsp; Dierenasiel &nbsp;·&nbsp; niveau <span style="color:#37b24d">●○○</span> &nbsp;·&nbsp; ±2 uur

## Leerdoel

Na deze oefening kan je twee modellen met elkaar verbinden in een één-op-veel-relatie, met een vreemde sleutel en navigation properties.

Je leert de relatie volledig vastleggen met de Fluent API, kiezen wat er gebeurt als iemand de "één"-kant wil verwijderen, en startgegevens voor twee tabellen meegeven, ook met een datum.

Daarnaast merk je dat de database een relatie zelf bewaakt: wat de relatie verbiedt, moet je API vooraf netjes weigeren.

## 

## Opdracht

Knuffelhof hield tot nu toe enkel een nummer bij voor het verblijf van elk dier. Wat achter dat nummer zat, wist de API niet. Een verblijf wordt nu een eigen model: elk dier woont in precies één verblijf, en één verblijf kan meerdere dieren huisvesten. Knuffelhof wil ook weten sinds wanneer elk verblijf in gebruik is.

Jouw taak is om een model `Verblijf` toe te voegen, het te verbinden met `Dier`, en de verblijven via de API beschikbaar te maken, in de stijl van de schoolopgaven.

Via de API moet de website, net als in 05_04, alle dieren kunnen opvragen, filteren, zoeken, tellen en de fiche tonen. Daarnaast moet ze:

1. alle verblijven en één verblijf kunnen opvragen;
2. alle dieren van één verblijf kunnen opvragen, en tellen;
3. een verblijf kunnen toevoegen;
4. een verblijf kunnen verwijderen, zolang het leeg is.

Implementeer onderstaande functionaliteiten.

### 1. Je vertrekpunt

Vertrek van je eigen oplossing van 05_04. Alle endpoints uit die oefening blijven werken. Elk dier verandert wel een beetje, en dat zie je ook in de JSON.

> [!NOTE]
> In deze oefening bouw je je database opnieuw op. Verwijder ze, maak de nieuwe migratie uit punt 4, en laat je database daarna volledig opbouwen door al je migraties.

---

### 2. De modellen

Maak in de map Models een klasse `Verblijf`:

| Property      | C#-type       | Kolom in de database |
|---------------|---------------|----------------------|
| Id            | `int`         | de sleutel |
| Naam          | `string`      | tekst van hoogstens 30 tekens, verplicht |
| Capaciteit    | `int`         | geheel getal, verplicht |
| InGebruikSinds | `DateTime`   | datum en uur, verplicht |
| Dieren        | `List<Dier>?` | navigation property, geen kolom |

Pas `Dier` aan: de property `Verblijfsnummer` verdwijnt, en in de plaats komen:

| Property   | C#-type     | Kolom in de database |
|------------|-------------|----------------------|
| VerblijfId | `int`       | vreemde sleutel naar `Verblijven`, verplicht |
| Verblijf   | `Verblijf?` | navigation property, geen kolom |

De tabel heet `Verblijven`. Voeg ze toe aan je `AsielContext`.

> [!NOTE]
> Let op het vraagteken bij de navigation properties: de client stuurt ze nooit mee. Scalar vult ze in zijn voorbeeld wel in. Haal ze daar weg voor je een verzoek verstuurt.

---

### 3. De relatie

Leg in `OnModelCreating` met de Fluent API de relatie volledig vast, zoals de schoolopgaven van hoofdstuk 6 het vragen: `HasOne`, `WithMany`, `HasForeignKey`, `OnDelete` en `IsRequired`.

- Een dier heeft één verblijf, en een verblijf heeft veel dieren.
- `VerblijfId` is de vreemde sleutel.
- Een verblijf waarin nog dieren wonen, mag niet verwijderd kunnen worden, ook niet rechtstreeks in de database.
- De relatie is verplicht.

> [!WARNING]
> Lees in les 06 wat Entity Framework Core uit zichzelf doet als je een verblijf verwijdert, en wat er dan met zijn dieren gebeurt. Dat is niet wat Knuffelhof wil.

---

### 4. De startgegevens

Leg met `HasData` deze verblijven vast:

| Id | Naam        | Capaciteit | InGebruikSinds |
|----|-------------|------------|----------------|
| 1  | Kattenkamer | 6          | 2012-04-01     |
| 2  | Hondenren   | 4          | 2012-04-01     |
| 3  | Konijnenhok | 5          | 2016-05-20     |
| 4  | Vogelkooi   | 10         | 2020-02-14     |
| 5  | Quarantaine | 2          | 2024-09-01     |

Geef elk dier in je startgegevens zijn verblijf:

| Dier | VerblijfId |
|------|------------|
| 1, 2 | 1          |
| 3, 4 | 2          |
| 5    | 3          |
| 6    | 4          |

Verblijf 5 is leeg. Maak een migratie met de naam `Verblijven`, verwijder je database, en bouw ze opnieuw op met al je migraties.

> [!WARNING]
> Een datum in je startgegevens kan PostgreSQL weigeren, met een melding over *Kind=Unspecified* en *only UTC is supported*. Lees in je leerboek, hoofdstuk 39, bij "Let op bij de lescode", wat je dan meegeeft. Hoofdstuk 3 legt uit waarom.

> [!TIP]
> Les 06 verdeelt `OnModelCreating` in twee methodes: één die de tabellen opbouwt en één die de startgegevens toevoegt. Doe dat ook.

---

### 5. De repository

Maak een interface `IVerblijfRepository` en een klasse `VerblijfRepository`, asynchroon, met je context in een `readonly` veld. Registreer ze met `AddScoped`.

| Methode | Parameters | Geeft terug |
|---------|------------|-------------|
| `GetAllAsync` | geen | `Task<List<Verblijf>>` |
| `GetByIdAsync` | `int id` | `Task<Verblijf?>` |
| `CreateAsync` | `Verblijf verblijf` | `Task<Verblijf>` |
| `DeleteAsync` | `int id` | `Task` |
| `GetDierenAsync` | `int verblijfId` | `Task<List<Dier>>` |
| `CountDierenAsync` | `int verblijfId` | `Task<int>` |
| `HeeftDierenAsync` | `int verblijfId` | `Task<bool>` |

- `GetAllAsync` en `GetDierenAsync` zijn op Id gesorteerd. `GetDierenAsync` geeft een lege lijst als het verblijf leeg is of niet bestaat.
- `CreateAsync` negeert de Id uit de body en berekent ze zelf: de hoogste Id plus één, of 1 in een lege tabel.
- `DeleteAsync` doet niets als het verblijf niet bestaat.
- Tellen doe je met `CountAsync`, nagaan of er iets is met `AnyAsync`.

---

### 6. De endpoints

Maak een `VerblijfController` die luistert op <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">verblijven</code>:

| Endpoint | Antwoord |
|----------|----------|
| <span style="background:#1c7ed6;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>GET</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">verblijven</code> | <code style="color:#37b24d;font-weight:600">200 OK</code> met alle verblijven, op Id gesorteerd |
| <span style="background:#1c7ed6;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>GET</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">verblijven/<span style="color:#e64980"><b>{id}</b></span></code> | <code style="color:#37b24d;font-weight:600">200 OK</code> met het verblijf, of <code style="color:#f08c00;font-weight:600">404 Not Found</code> zonder body |
| <span style="background:#1c7ed6;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>GET</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">verblijven/<span style="color:#e64980"><b>{id}</b></span>/dieren</code> | <code style="color:#37b24d;font-weight:600">200 OK</code> met de dieren in dat verblijf, op Id gesorteerd |
| <span style="background:#1c7ed6;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>GET</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">verblijven/<span style="color:#e64980"><b>{id}</b></span>/aantal</code> | <code style="color:#37b24d;font-weight:600">200 OK</code> met een object met een property `count`, een `int` |
| <span style="background:#2f9e44;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>POST</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">verblijven</code> | <code style="color:#37b24d;font-weight:600">201 Created</code> met het nieuwe verblijf en zijn adres, of <code style="color:#f08c00;font-weight:600">400 Bad Request</code> met <code style="color:#845ef7">Een verblijf moet een naam hebben.</code> |

Een leeg verblijf, of een verblijf dat niet bestaat, geeft bij het derde endpoint `[]` en bij het vierde `{ "count": 0 }`, telkens met <code style="color:#37b24d;font-weight:600">200 OK</code>, zoals de schoolopgaven het vragen. Log elk verzoek op niveau `Information`, en elke <code style="color:#f08c00;font-weight:600">400</code> en <code style="color:#f08c00;font-weight:600">404</code> op niveau `Warning`.

<code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> verblijven/2</code> geeft als resultaat:

```json
{
  "id": 2,
  "naam": "Hondenren",
  "capaciteit": 4,
  "inGebruikSinds": "2012-04-01T00:00:00Z",
  "dieren": null
}
```

<code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> dieren/4</code> geeft nu als resultaat:

```json
{
  "id": 4,
  "naam": "Bella",
  "soort": "hond",
  "leeftijdInJaren": 1,
  "isGechipt": true,
  "verblijfId": 2,
  "verblijf": null
}
```

> [!NOTE]
> De navigation properties zijn leeg in je JSON. Entity Framework Core haalt gerelateerde gegevens niet uit zichzelf op. Hoe je dat wél doet, zie je in les 08.

> [!WARNING]
> Krijg je bij <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> verblijven/2/dieren</code> een <code style="color:#f03e3e;font-weight:600">500</code> met een melding over een *object cycle*? Dan kent elk dier zijn verblijf, en kent dat verblijf zijn dieren weer, zonder einde. Les 08 legt uit waarom. Haal in hetzelfde verzoek niet meer objecten op dan het endpoint teruggeeft.

---

### 7. Alles samen: een verblijf verwijderen

<span style="background:#7048e8;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>SAMENGESTELDE OEFENING</b></span>

De relatie, de keuze bij het verwijderen, de startgegevens en de repository komen samen in één endpoint: een verblijf verwijderen. Het mag enkel lukken als er geen dier meer in woont.

<span style="background:#e03131;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>DELETE</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">verblijven/<span style="color:#e64980"><b>{id}</b></span></code>

- Het verblijf bestaat niet: <code style="color:#f08c00;font-weight:600">404 Not Found</code> zonder body, gelogd op niveau `Error`.
- Er wonen nog dieren in: <code style="color:#f08c00;font-weight:600">400 Bad Request</code> met <code style="color:#845ef7">Dit verblijf is niet leeg.</code>
- Anders: verwijder het verblijf en geef <code style="color:#37b24d;font-weight:600">204 No Content</code> terug.

> [!IMPORTANT]
> De database weigert zelf ook een verblijf met dieren, dankzij je keuze in punt 3. Maar laat het niet zover komen: dan krijgt de website een <code style="color:#f03e3e;font-weight:600">500</code>. Je API kijkt eerst, en antwoordt netjes.

#### Testscenario

Bouw je database opnieuw op, zodat ze exact de startgegevens bevat, en doorloop deze reeks:

1. Verwijder verblijf 1. Je krijgt <code style="color:#f08c00;font-weight:600">400</code> met het bericht hierboven. <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> dieren/soort/kat</code> geeft nog altijd de dieren 1 en 2.
2. Probeer in pgAdmin of in VS Code verblijf 1 rechtstreeks uit de tabel te verwijderen. De database weigert, met een melding over de vreemde sleutel.
3. <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> verblijven/1/aantal</code> geeft `{ "count": 2 }`, en <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> verblijven/99/dieren</code> geeft `[]`.
4. Verwijder verblijf 5. Je krijgt <code style="color:#37b24d;font-weight:600">204</code>. Doe het nog eens: nu krijg je <code style="color:#f08c00;font-weight:600">404</code>, en in je terminal staat een `Error`.
5. Voeg dit verblijf toe:

   ```json
   { "id": 0, "naam": "Opvangkamer", "capaciteit": 3, "inGebruikSinds": "2026-11-07T00:00:00Z" }
   ```

   Je krijgt <code style="color:#37b24d;font-weight:600">201</code>, en het nieuwe verblijf heeft Id 5. De header `Location` eindigt op `/verblijven/5`; hoofdletters tellen daarbij niet.
6. <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> verblijven</code> geeft de verblijven 1 tot en met 5, in die volgorde. Verblijf 5 heet nu `Opvangkamer`.
7. <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> verblijven/5/dieren</code> geeft `[]`.
8. Controleer in pgAdmin of in VS Code dat de kolom `InGebruikSinds` een datum met tijdzone is, en dat er in de tabel `Dieren` geen kolom `Verblijfsnummer` meer staat.

> [!NOTE]
> In stap 5 krijgt het nieuwe verblijf Id 5, het nummer van het verblijf dat je net verwijderde. Dat is het gevolg van "de hoogste Id plus één". In de reeks _01 tot en met _03 kiest de database de Id, en komt een nummer nooit terug.
