# Formaat van een nakijkbestand (versie 1)

Eén JSON-bestand per oefening, UTF-8 zonder BOM, naam `Nakijk_<oefening>.json`
(bijvoorbeeld `Nakijk_01_03.json`, `Nakijk_TM_02_01.json`). Het nakijkscript leest het met
`ConvertFrom-Json` (PowerShell 5.1), dus: geen commentaar, geen komma na het laatste element.

Een nakijkbestand bevat **enkel wat de opgave zelf vraagt**: de verzoeken, voorbeelden en regels
die in de tekst van de oefening staan, met de antwoorden die de tekst belooft. Nooit iets wat de
opgave niet vraagt, en nooit code.

---

## 1. Bovenste niveau

| Sleutel | Type | Verplicht | Betekenis |
|---|---|---|---|
| `formaat` | getal | ja | altijd `1` |
| `oefening` | tekst | ja | `01_03`, `06_01`, `TM_02_01` |
| `titel` | tekst | ja | de ondertitel uit de kop van de oefening |
| `hoofdstuk` | getal | ja | 1 tot 9 |
| `opgave` | tekst | ja | bestandsnaam van de opgave, bv. `Oefening_01_03.md` |
| `bouwtOpVan` | tekst | nee | de oefening waarvan de student vertrekt, bv. `05_01` |
| `soort` | tekst | ja | `geheugen` (alles in een lijst), `bestand` (JSON-bestand), `database` |
| `routePrefix` | object | ja | `{ "standaard": "", "probeer": ["api/"] }`: het script probeert eerst `standaard`, daarna elk van `probeer`. Vanaf hoofdstuk 03 is het `{ "standaard": "api/", "probeer": [] }` omdat de opgave `api/` zelf vraagt |
| `database` | object | bij `database` | `{ "naam": "ArtikelDb", "opbouwen": "migraties" }` of `"sqlscript"` (zie §4) |
| `bestanden` | lijst tekst | nee | bestanden in de projectmap die het script wist bij de start van elk scenario met `herstart` of `opbouwen`, bv. `["tickets.json"]`. Een `actie: herstart` midden in een scenario wist ze níet: zo test je of iets een herstart overleeft |
| `scenarios` | lijst | ja | zie §2 |
| `handmatig` | lijst | nee | `{ "punt": "7", "tekst": "..." }`: wat het script niet zelf kan nakijken (pgAdmin openen, een migratie lezen); komt als checklist in het verslag |
| `valkuilen` | object | nee | `{ "extra": ["CODE"], "niet": ["CODE"] }`: afwijkingen op de valkuilen die standaard bij het hoofdstuk horen (de catalogus legt per valkuil vast vanaf welk hoofdstuk ze geldt) |

## 2. Scenario

| Sleutel | Type | Verplicht | Betekenis |
|---|---|---|---|
| `id` | tekst | ja | uniek in het bestand, bv. `S1` |
| `titel` | tekst | ja | bv. `Voorbeelden uit punt 2 tot 5` of `Testscenario punt 7` |
| `start` | tekst | ja | `herstart`: API stoppen en opnieuw starten (lijst in het geheugen begint opnieuw); `opbouwen`: API stoppen, database opnieuw opbouwen (§4), API starten; `doorgaan`: verder met de toestand van het vorige scenario |
| `stappen` | lijst | ja | zie §3, in de volgorde waarin ze uitgevoerd worden |

## 3. Stap

Elke stap heeft `id` (uniek in het bestand), `punt` (het nummer van het punt in de opgave waar de
regel staat, als tekst), `uitleg` (korte zin voor het verslag) en precies één van `verzoek`,
`actie` of `sql`. Optioneel: `valkuilen` (lijst codes die het script als vermoedelijke oorzaak
toont als de stap faalt).

### 3a. `verzoek` (HTTP)

```json
{
  "id": "7.4", "punt": "7", "uitleg": "Voeg Kabelkoning toe",
  "verzoek": { "methode": "POST", "pad": "leverancier", "body": { "naam": "Kabelkoning", "land": "België" } },
  "verwacht": { "status": 201, "json": { "id": 5, "naam": "Kabelkoning", "land": "België", "artikelen": null },
                "location": { "eindigtOp": "/api/leverancier/5" } },
  "bewaar": { "kabelkoning": "id" }
}
```

- `pad`: zonder prefix en zonder `/` vooraan. Een spatie mag (het script codeert de URL).
  Variabelen: `{{naam}}`.
- `body`: een JSON-waarde. `bodyRuw`: een letterlijke tekst als body (bv. ongeldige JSON).
  Zonder body stuurt het script niets mee.
- `verwacht` (alles optioneel, maar minstens één):
  - `status`: getal. Ontbreekt het, dan kijkt het script de statuscode niet na.
  - `tekst`: de body moet exact deze tekst zijn (na trimmen; een JSON-string wordt eerst ontdaan van
    zijn aanhalingstekens).
  - `json`: de body als JSON, vergeleken volgens §5.
  - `geenBody`: `true` → geen eigen inhoud: een lege body, of de standaard-`ProblemDetails` die ASP.NET bij
    `NotFound()` of `BadRequest()` zonder argument meegeeft (een object met `status` en `title`).
  - `location`: `{ "eindigtOp": "/api/leverancier/5" }` → de header `Location` eindigt hierop,
    hoofdletters tellen niet.
  - `queries`: getal → precies zoveel regels `Executed DbCommand` in de uitvoer van de API tijdens dit verzoek.
- `bewaar`: `{ "variabele": "pad" }` → onthoudt een waarde uit het antwoord. Pad: `id`,
  `lijnen[0].id`, `[0].id` (eerste element van een lijst als body), `""` (de hele body, bv. een kaal
  getal), of `header:Location`. Het script bewaart eerst en controleert daarna: een variabele mag dus
  ook in `verwacht` van dezelfde stap staan (bv. `location: { "eindigtOp": "/api/fiets/{{nieuw}}" }`).

### 3b. `actie`

- `{ "actie": "herstart" }`: API stoppen en opnieuw starten, zonder iets te wissen.
- `{ "actie": "verwijderBestand", "pad": "tickets.json" }`: bestand in de projectmap verwijderen (API eerst stoppen).
- `{ "actie": "opbouwen" }`: database opnieuw opbouwen zoals in §4.

### 3c. `sql` (enkel bij `soort: database`)

```json
{ "id": "4.2", "punt": "4", "uitleg": "De tabel Artikelen bevat de zes startartikelen",
  "sql": "SELECT \"Id\", \"Naam\" FROM \"Artikelen\" ORDER BY \"Id\"",
  "verwacht": { "rijen": [["1", "ROG Strix B650-A"], ["2", "MAG B650 Tomahawk"]], "volgorde": "vast" } }
```

Het script voert de query uit met `psql` in de Docker-container van PostgreSQL. Alle waarden worden
als tekst vergeleken, zoals `psql -A -t` ze toont (`t`/`f` voor booleans, `NULL` wordt een lege tekst).
`volgorde`: `vast` of `vrij`. In plaats van `rijen` mag ook `aantal` (aantal rijen).

`verwacht` mag ook `{ "fout": "foreign key" }` zijn: de query moet dan mislukken met een melding die
dit stuk tekst bevat. Zo'n query voert het script altijd uit tussen `BEGIN` en `ROLLBACK`, zodat een
query die tóch lukt, niets verandert aan de database.

## 4. Database opnieuw opbouwen

- `migraties`: `dotnet ef database drop --force`, daarna `dotnet ef database update`
  (hoofdstuk 06 en later: de startgegevens komen met `HasData`).
- `sqlscript`: zoals `migraties`, daarna de SQL-scripts van de student uitvoeren met de startgegevens
  (hoofdstuk 04 en 05). Het script voert alle `.sql`-bestanden uit de projectmap uit, in alfabetische
  volgorde, tenzij de student ze zelf opgeeft met `-SqlScript` (dan in die volgorde).

## 5. JSON vergelijken

- Objecten: standaard **exact** dezelfde sleutels (niet meer, niet minder), volgorde maakt niet uit.
  Waarden recursief vergeleken.
- Getallen: als getal (`389.00` = `389`), met een tolerantie van 0,005 (een `double` in de API mag een
  afrondingsverschil geven). Het script rekent zelf in `decimal`. Teksten: exact. `true`/`false`/`null`: exact.
- Lijsten: standaard **op volgorde**, element per element.
- Matchers: een object met precies één sleutel die met `$` begint:

| Matcher | Betekenis |
|---|---|
| `{ "$elk": true }` | elke waarde is goed, als de sleutel bestaat |
| `{ "$ontbreekt": true }` | de sleutel mag **niet** bestaan (enkel als waarde van een sleutel) |
| `{ "$bevat": { ... } }` | object met minstens deze sleutels en waarden; andere sleutels mogen |
| `{ "$ids": [1, 5] }` | lijst van objecten waarvan de `id`'s precies deze zijn, volgorde vrij |
| `{ "$idsInVolgorde": [1, 2, 3] }` | idem, in deze volgorde |
| `{ "$verzameling": [ ... ] }` | lijst met deze elementen (elk mag zelf een matcher zijn), volgorde vrij |
| `{ "$aantal": 6 }` | lijst met precies zoveel elementen |
| `{ "$tekstBevat": "..." }` | tekst die dit stuk bevat |
| `{ "$elkElement": <matcher> }` | elk element van de lijst voldoet hieraan |
| `{ "$heeftElement": <matcher> }` | minstens één element van de lijst voldoet hieraan |
| `{ "$geenElement": <matcher> }` | geen enkel element van de lijst voldoet hieraan (bv. "belandt niet in de lijst") |
| `{ "$reken": "{{totaal}} + 35.00 - 12.50" }` | getal: de som, met bewaarde variabelen en getallen, enkel `+` en `-` |

Variabelen (`"{{id}}"`) mogen overal in teksten staan; staat een variabele alleen in een tekst en is
de echte waarde een getal, dan vergelijkt het script als getal.

## 6. Keuzes bij het opstellen

- Volg de volgorde van het testscenario in de opgave. Regels uit de opgave die het testscenario niet
  test (een 404-bericht, een 400-bericht), komen achteraan, of in een eigen scenario als ze de
  toestand zouden verstoren.
- Toont de opgave het volledige JSON-antwoord, vergelijk dan exact. Noemt ze maar enkele velden,
  gebruik `$bevat`. Lijsten die de opgave "op Id gesorteerd" noemt: `$idsInVolgorde`.
- Zegt de opgave "een geldige ..." zonder body, kies dan zelf een body die aan alle regels voldoet en
  vermeld hem niet apart: hij staat in de stap.
- Vaste Id's mogen letterlijk in een pad staan: elk scenario met `opbouwen` of `herstart` begint bij de
  startgegevens. Gebruik `bewaar` enkel als de opgave het Id niet vastlegt.

## 7. Wat níet in een nakijkbestand hoort

- Verzoeken die de opgave niet vraagt, of combinaties waarvan de opgave het antwoord niet vastlegt
  (bv. de volgorde 404/400 bij een PUT op een onbestaand Id met ongeldige gegevens).
- Volgorde binnen een lijst die met `Include` meekomt: altijd `$ids` of `$verzameling`.
- Een ontbrekend veld in een body: lege velden altijd als `""` sturen (een ontbrekend non-nullable
  veld geeft de automatische 400 van ASP.NET, niet het bericht van de opgave).
