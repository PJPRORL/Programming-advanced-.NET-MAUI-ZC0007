# Extra oefeningen — hoofdstuk 03 tot en met 09

<b>Programming Advanced</b> &nbsp;·&nbsp; zes oefeningen per hoofdstuk, en in hoofdstuk 04 tot en met 06 een zevende in examenstijl &nbsp;·&nbsp; opgesteld op basis van de lessen `03 - Repositories` tot en met `09 - DTO's`

Zelfde opmaak en zelfde opbouw als de oefeningen van hoofdstuk 01 en 02. Twee reeksen die elkaar spiegelen: dezelfde techniek, ander domein. Doe je ze allebei, dan oefen je elk concept twee keer zonder twee keer hetzelfde te lezen.

Elk bestand sluit af met een <span style="background:#7048e8;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>SAMENGESTELDE OEFENING</b></span> : één endpoint, één reeks verzoeken of één volledige cyclus waarin alle voorgaande punten van dat bestand samenkomen. Dat is telkens het laatste genummerde punt.

> [!NOTE]
> Dit overzicht groeit mee. Op dit moment staan hoofdstuk 03 tot en met 09 erin.
>
> Hoofdstuk 04 tot en met 06 zijn in november 2026 herschreven: elke oefening combineert nu de leerdoelen van de lessen met die van de schoolopgaven. De vorige versies staan in elke hoofdstukmap in de submap `Vorige versie`.
>
> Oefening 03_02 is in november 2026 verduidelijkt: vervangen staat stap voor stap in punt 4, en de historiek in punt 6 is opgedeeld in vijf delen. Wat je API moet antwoorden, bleef hetzelfde. De vorige versie staat in `Oefeningen Hoofdstuk 3/Vorige versie`.

---

## De kleuren in deze bestanden

De HTTP-verbs dragen de kleuren van Scalar, zodat je ze herkent uit je eigen testomgeving. De statuscodes volgen de kleurcode die je cursus zelf gebruikt in de samenvatting van hoofdstuk 02. De kleuren zijn gekozen om zowel op een licht als op een donker thema leesbaar te blijven.

| Kleur | Waarvoor |
|---|---|
| <span style="background:#1c7ed6;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>GET</b></span> | gegevens ophalen |
| <span style="background:#2f9e44;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>POST</b></span> | gegevens toevoegen |
| <span style="background:#d9822b;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>PUT</b></span> | gegevens bijwerken |
| <span style="background:#e03131;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>DELETE</b></span> | gegevens verwijderen |
| <code style="color:#37b24d;font-weight:600">200</code> <code style="color:#37b24d;font-weight:600">201</code> <code style="color:#37b24d;font-weight:600">204</code> | gelukt |
| <code style="color:#f08c00;font-weight:600">400</code> <code style="color:#f08c00;font-weight:600">404</code> | de client maakte een fout |
| <code style="color:#f03e3e;font-weight:600">500</code> | jouw code maakte een fout |
| <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/artikel/merk/<span style="color:#e64980"><b>{merk}</b></span></code> | een route; het vaste deel blauwgroen |
| <code style="color:#e64980;font-weight:600">{merk}</code> | een routeparameter: wat de gebruiker zelf invult |
| <code style="color:#845ef7">We vonden geen artikelen van het merk asus.</code> | tekst die je API letterlijk moet teruggeven |
| `IArtikelRepository` `Wijziging` `bestellijnen.json` | klassenamen, eigenschappen, bestanden en invoerwaarden blijven neutraal |

De kaders in de oefeningen betekenen het volgende:

> [!NOTE]
> Iets om te onthouden, meestal een keuze die verderop in de cursus terugkomt.

> [!TIP]
> Een aanwijzing die je werk korter maakt.

> [!IMPORTANT]
> Een eis waaraan je oplossing moet voldoen.

> [!WARNING]
> Een valkuil. Hier lopen de meeste oplossingen stuk.

De kleuren en de kaders komen volledig tot hun recht in de preview van VS Code. Open een bestand en druk op `Ctrl+Shift+V`.

---

## Zes reeksen die doorlopen

Elke oefening bouwt verder op de oefening met hetzelfde nummer uit het hoofdstuk ervoor: 03_01 op 02_01, 04_01 op 03_01, enzovoort. Zo groeit elke reeks uit tot één project, net zoals WebAPIDemo in de lessen.

| Reeks | Niveau | Domein | Vertrekt van |
|-------|--------|--------|--------------|
| _01 | <span style="color:#37b24d">●○○</span> | PC-onderdelen: artikelen | 02_01 |
| _02 | <span style="color:#f08c00">●●○</span> | PC-onderdelen: ventilatoren | 02_02 |
| _03 | <span style="color:#f03e3e">●●●</span> | PC-onderdelen: bestellijnen | 02_03 |
| _04 | <span style="color:#37b24d">●○○</span> | Dierenasiel | 02_04 |
| _05 | <span style="color:#f08c00">●●○</span> | Fietsverhuur | 02_05 |
| _06 | <span style="color:#f03e3e">●●●</span> | Concertzaal | 02_06 |
| _07 | <span style="color:#f08c00">●●○</span> | Herstelplaats, in examenstijl | een nieuw project, in 04_07 |

Elke oefening begint met een punt **Je vertrekpunt**: wat er uit de vorige oefening moet blijven werken. Heb je die oefening niet meer, maak ze dan eerst opnieuw vanuit haar eigen opgave. Het vertrekpunt is een geheugensteun, geen volledige opgave.

**De reeks _07** volgt de schoolopgaven van hoofdstuk 4 tot en met 6, tot in de namen: een project `WebApi`, routes zonder `api/`, een <code style="color:#f08c00;font-weight:600">404</code> zonder body, een lege lijst `[]` met <code style="color:#37b24d;font-weight:600">200</code> als een filter niets vindt, een Id die je API zelf berekent, en een interface die je letterlijk overneemt. Ze begint in 04_07 met een leeg project en loopt door tot 06_07. Waar de andere reeksen tussenstappen nemen, gaat de reeks _07 meteen naar de vorm van het examen: één opgave per hoofdstuk, volledig in de stijl van de schoolopgaven.

Vanaf hoofdstuk 04 krijgt elke oefening ook **vaste startgegevens** en een model met **C#-types**, zoals in de oefeningen van school. Daardoor kan elk voorbeeld het volledige antwoord tonen, en weet je precies wat je API moet teruggeven. In de JSON-voorbeelden mag de volgorde van de velden verschillen; de waarden niet. Bedragen vergelijk je als getal.

Een bedrag is een `double`, zoals in de schoolopgaven. Een `double` onthoudt geen nullen achter de komma: `389.00` in je startgegevens is `389` in je JSON. Enkel waar een opgave uitdrukkelijk een `decimal` vraagt, zoals bij de accessoires in 06_05 en de onderdelen in 06_07, gebruik je een `decimal`.

---

## Hoofdstuk 03 — repository, interface, dependency injection, logging

| Oefening | Niveau | Domein | Wat je oefent | Samengestelde oefening |
|----------|--------|--------|----------------|------------------------|
| 03_01 | <span style="color:#37b24d">●○○</span> | PC-onderdelen | 02_01 refactoren: interface, repository, constructor injection, logging, zoeken op merk | De artikelfiche via de repository |
| 03_02 | <span style="color:#f08c00">●●○</span> | PC-onderdelen | 02_02 refactoren: CRUD via de repository, wie beslist wat, gegevens tussen twee verzoeken | De wijzigingshistoriek |
| 03_03 | <span style="color:#f03e3e">●●●</span> | PC-onderdelen | 02_03 refactoren: zelf het contract ontwerpen, een tweede repository in een JSON-bestand, wisselen in Program.cs | Een herstart overleven |
| 03_04 | <span style="color:#37b24d">●○○</span> | Dierenasiel | Zelfde niveau als 03_01, met zoeken op een stukje van de naam | De dierfiche via de repository |
| 03_05 | <span style="color:#f08c00">●●○</span> | Fietsverhuur | Zelfde niveau als 03_02, met regels die bestaande endpoints veranderen | Verhuren en terugbrengen |
| 03_06 | <span style="color:#f03e3e">●●●</span> | Concertzaal | Zelfde niveau als 03_03, met een plaatscontrole die een herstart moet overleven | Een herstart overleven |

Les 03 is een les in refactoren: de binnenkant van je API verandert grondig, de buitenkant blijft precies dezelfde.

**De samengestelde oefeningen** doen telkens één van drie dingen:

- **Ze laten iets uit hoofdstuk 02 opnieuw werken, nu de lijst weg is uit de controller.** In 03_01 en 03_04 moet de fiche dezelfde gegevens tonen als vroeger, terwijl de controller alles via de repository moet vragen. Daarbij beslis je zelf wie de fiche samenstelt.
- **Ze zetten een tweede repository naast de eerste.** In 03_02 en 03_05 krijgt je controller een tweede repository, die bijhoudt wat er met de eerste gebeurde. Wat daar misloopt, zie je pas als je de endpoints na elkaar test: een actie die mislukt, mag in geen van beide repositories een spoor nalaten.
- **Ze wisselen de opslag onder je controller weg.** In 03_03 en 03_06 werkt dezelfde controller eerst met een lijst in het geheugen en daarna met een bestand. Moet je daarvoor de controller aanpassen, dan was je refactoring niet af.

**Buiten bereik.** Je hebt geen database, geen Entity Framework, geen async, geen eigen generics en geen DTO's nodig. Twee elementen komen voor die de les niet voordoet:

- Een repository die zijn gegevens in een bestand bewaart, in 03_03 en 03_06. De les noemt het in de inleiding, maar werkt het niet uit. Het lezen en schrijven van bestanden ken je uit de vorige modules.
- Een tweede model dat geen voorwerp uit de echte wereld beschrijft, maar bijhoudt wat er gebeurde: `Wijziging` in 03_02 en `Verhuring` in 03_05.

---

## Hoofdstuk 04 — Docker, DbContext, Fluent API, migraties, HasData

| Oefening | Niveau | Domein | Wat je oefent | Samengestelde oefening |
|----------|--------|--------|----------------|------------------------|
| 04_01 | <span style="color:#37b24d">●○○</span> | PC-onderdelen | Database in Docker, DbContext, Fluent API, `HasData`, migratie `InitialCreate`, de controller rechtstreeks op de DbContext | Een kolom erbij |
| 04_02 | <span style="color:#f08c00">●●○</span> | PC-onderdelen | CRUD via de DbContext, twee tabellen en twee migraties, de teller na `HasData`, dezelfde regels in API en database, `Error` bij een DELETE die niets vindt | Niets verdwijnt nog |
| 04_03 | <span style="color:#f03e3e">●●●</span> | PC-onderdelen | Tien endpoints op de database, tellen en optellen in SQL, sommen afronden, `Up` en `Down`, een toegepaste migratie terugdraaien | Een migratie terugdraaien |
| 04_04 | <span style="color:#37b24d">●○○</span> | Dierenasiel | Zelfde niveau als 04_01, en de overstap naar de stijl van de schoolopgaven | Een kolom erbij |
| 04_05 | <span style="color:#f08c00">●●○</span> | Fietsverhuur | Zelfde niveau als 04_02, met de Id als hoogste plus één, ook in een lege tabel | Niets verdwijnt nog |
| 04_06 | <span style="color:#f03e3e">●●●</span> | Concertzaal | Zelfde niveau als 04_03, met de opbrengst als klein JSON-object | Een migratie terugdraaien |
| 04_07 | <span style="color:#f08c00">●●○</span> | Herstelplaats | Examenstijl: een nieuw project, `HasData`, één migratie `InitialCreate`, een controller rechtstreeks op de DbContext, routes zonder `api/`, een Id die je API zelf berekent | Logging in elk endpoint |

In hoofdstuk 04 stapt elke API over naar de database. De controller krijgt de DbContext rechtstreeks binnen, zoals in de schoolopgaven van hoofdstuk 4; in hoofdstuk 05 komt er weer een repository tussen. De startgegevens komen met `HasData` mee in je migraties. Elke oefening krijgt een eigen database in dezelfde PostgreSQL-container.

**Twee stijlen.** De reeks _01 tot en met _03 houdt de stijl van de les: routes met `api/`, een <code style="color:#f08c00;font-weight:600">404</code> met bericht, en de database kiest de Id. De reeks _04 tot en met _06 schakelt in 04_04 over naar de stijl van de schoolopgaven: routes zonder `api/`, in het meervoud, een <code style="color:#f08c00;font-weight:600">404</code> zonder body, een lege lijst `[]` als een filter niets vindt, en een Id die je API zelf berekent. Een <code style="color:#f08c00;font-weight:600">400</code> houdt in beide reeksen zijn reden. Zo oefen je beide, en leer je de opgave letterlijk te lezen.

**De samengestelde oefeningen:**

- **Een kolom erbij**, in 04_01 en 04_04: het model groeit, en je migratie voegt de kolom toe én vult de startgegevens aan.
- **Niets verdwijnt nog**, in 04_02 en 04_05: een tweede tabel, en na een herstart vertellen je API en je database nog hetzelfde verhaal.
- **Een migratie terugdraaien**, in 04_03 en 04_06: een migratie die de database al kent, moet weg zonder een spoor na te laten.

**Buiten bereik.** Je hebt nog geen repository op de database, geen async, geen relaties en geen DTO's nodig. Drie elementen komen voor die de les niet voordoet:

- `HasData`: les 06 toont het, de schoolopgaven van hoofdstuk 4 gebruiken het al. Je leerboek legt het uit in hoofdstuk 39;
- de teller van PostgreSQL na startgegevens met een vaste Id, in 04_02 en 04_03. Je leerboek legt het uit in hoofdstuk 31 en 39;
- het commando om de database terug te brengen naar een eerdere migratie, in 04_03 en 04_06. De les zegt dat het moet, maar niet hoe.

---

## Hoofdstuk 05 — asynchrone repository, LINQ in de database

| Oefening | Niveau | Domein | Wat je oefent | Samengestelde oefening |
|----------|--------|--------|----------------|------------------------|
| 05_01 | <span style="color:#37b24d">●○○</span> | PC-onderdelen | Een asynchrone repository met een gegeven interface, `readonly` en `AddScoped`, `Select` met `Distinct`, `?descending=` uit de querystring, `CountAsync` en `AnyAsync` | Dezelfde antwoorden, en meer |
| 05_02 | <span style="color:#f08c00">●●○</span> | PC-onderdelen | Twee asynchrone repositories op één context, `AverageAsync`, `MinAsync` en `MaxAsync` met `0` als er niets is, het achtervoegsel `Async` en `CreatedAtAction` | Cijfers die kloppen |
| 05_03 | <span style="color:#f03e3e">●●●</span> | PC-onderdelen | Een bestands- en een databaserepository achter één contract, `SumAsync`, `AverageAsync` en `AnyAsync`, een drempel in SQL | Twee repositories, één antwoord |
| 05_04 | <span style="color:#37b24d">●○○</span> | Dierenasiel | Zelfde niveau als 05_01, met de gemiddelde, de jongste en de oudste leeftijd | Dezelfde antwoorden, en de cijfers |
| 05_05 | <span style="color:#f08c00">●●○</span> | Fietsverhuur | Zelfde niveau als 05_02, met CRUD in de repository en een tabel die helemaal leeg raakt | Van startgegevens tot lege tabel |
| 05_06 | <span style="color:#f03e3e">●●●</span> | Concertzaal | Zelfde niveau als 05_03, met een plaatscontrole via `AnyAsync` | Twee repositories, één antwoord |
| 05_07 | <span style="color:#f08c00">●●○</span> | Herstelplaats | Examenstijl: een gegeven interface letterlijk volgen, `AnyAsync`, `CountAsync`, `SumAsync`, `AverageAsync`, `MinAsync` en `MaxAsync`, `Select` met `Distinct`, een parameter uit de querystring | Van startgegevens tot lege tabel |

In les 05 komt er weer een repository tussen je controller en de database, nu asynchroon. De interfaces staan in de opgave zoals op het examen: neem de namen, de parameters en de types letterlijk over. Elke methode die rekent, geeft `0` als er niets te berekenen valt.

**Buiten bereik.** Je hebt geen relaties, geen navigation properties, geen generieke repository, geen Unit of Work, geen `Include` en geen DTO's nodig. Twee elementen komen voor die de les niet voordoet: de asynchrone methodes om een bestand te lezen en te schrijven, in 05_03 en 05_06, en een parameter uit de querystring, die de schoolopgaven wel vragen.

---

## Hoofdstuk 06 — relaties, Fluent API, startgegevens met HasData

| Oefening | Niveau | Domein | Wat je oefent | Samengestelde oefening |
|----------|--------|--------|----------------|------------------------|
| 06_01 | <span style="color:#37b24d">●○○</span> | PC-onderdelen | Eén op veel: leveranciers en artikelen, de relatie van `HasOne` tot `IsRequired`, `Restrict`, `HasData` met een datum in UTC | Een leverancier verwijderen |
| 06_02 | <span style="color:#f08c00">●●○</span> | PC-onderdelen | Veel op veel met een tussentabel, een som via een navigation property | Een ventilator vervangen in alle behuizingen |
| 06_03 | <span style="color:#f03e3e">●●●</span> | PC-onderdelen | Bestellijnen onder een bestelling, `OnModelCreating` opsplitsen, berekeningen per bestelling, filteren via een navigation property | De fiche per bestelling |
| 06_04 | <span style="color:#37b24d">●○○</span> | Dierenasiel | Zelfde niveau als 06_01, met verblijven | Een verblijf verwijderen |
| 06_05 | <span style="color:#f08c00">●●○</span> | Fietsverhuur | Zelfde niveau als 06_02, met een `decimal` maal een `double` | Een fiets vervangen met haar uitrusting |
| 06_06 | <span style="color:#f03e3e">●●●</span> | Concertzaal | Zelfde niveau als 06_03, met een `DateTime` en een plaatscontrole per concert | Een plaats per concert |
| 06_07 | <span style="color:#f03e3e">●●●</span> | Herstelplaats | Examenstijl: één op veel en veel op veel met een tussentabel, `HasData` voor vier tabellen met een datum, rekenen via een navigation property, een `decimal` maal een `double` | Een technieker kiezen en een herstelling verwijderen |

In les 06 bouw je je database telkens opnieuw op, en komen alle startgegevens via de Fluent API. Je database verwijderen hoort niet bij de leerstof: dat mag in pgAdmin, of met `dotnet ef database drop`. Navigation properties blijven leeg in je JSON: gerelateerde gegevens ophalen zie je in les 08.

**Buiten bereik.** Je hebt geen generieke repository, geen Unit of Work, geen `Include`, geen `IgnoreCycles`, geen DTO's en geen Mapster nodig.

---

## Hoofdstuk 07 — generieke repository, Unit of Work

| Oefening | Niveau | Domein | Wat je oefent | Samengestelde oefening |
|----------|--------|--------|----------------|------------------------|
| 07_01 | <span style="color:#37b24d">●○○</span> | PC-onderdelen | Een generieke repository, een specifieke repository die ervan overerft, een Unit of Work, bijwerken met de generieke repository | Een artikel naar een andere leverancier |
| 07_02 | <span style="color:#f08c00">●●○</span> | PC-onderdelen | Vier modellen achter één Unit of Work, twee tabellen in één keer bewaren, en zien waar dat niet lukt | Een behuizing uit het gamma halen |
| 07_03 | <span style="color:#f03e3e">●●●</span> | PC-onderdelen | De ombouw zonder dat de client iets merkt, bijwerken zonder te veel te overschrijven, alles controleren voor je bewaart | Een bestelling in één keer plaatsen |
| 07_04 | <span style="color:#37b24d">●○○</span> | Dierenasiel | Zelfde niveau als 07_01, met een capaciteitscontrole | Een dier verhuizen |
| 07_05 | <span style="color:#f08c00">●●○</span> | Fietsverhuur | Zelfde niveau als 07_02, met verhuren in één keer | Een accessoire uit het assortiment halen |
| 07_06 | <span style="color:#f03e3e">●●●</span> | Concertzaal | Zelfde niveau als 07_03, met een plaatscontrole binnen de groep | Een groep in één keer verkopen |

In les 07 veranderen je modellen en je database niet: je herschikt je code achter één generieke repository en één Unit of Work. De testscenario's van hoofdstuk 06 moeten daarna nog precies hetzelfde antwoorden. De samengestelde oefeningen van niveau 2 en 3 tonen waar de Unit of Work verschil maakt: wijzigingen die samen slagen of samen mislukken.

**Buiten bereik.** Je hebt geen `Include`, geen `IgnoreCycles`, geen DTO's, geen Mapster en geen eigen transacties nodig. Eén `SaveChangesAsync` per verzoek volstaat, behalve waar de oefening zelf zegt dat het niet kan. Een paar elementen gaan verder dan de les: bijwerken zonder de fout uit de laptopcontroller, lijsten gesorteerd houden met een generieke repository, en in 07_03 een navigation property die de client wél meestuurt. De waarschuwing over een *object cycle* uit hoofdstuk 06 blijft gelden.

---

## Hoofdstuk 08 — Include en ThenInclude

| Oefening | Niveau | Domein | Wat je oefent | Samengestelde oefening |
|----------|--------|--------|----------------|------------------------|
| 08_01 | <span style="color:#37b24d">●○○</span> | PC-onderdelen | `Include` in beide richtingen van een één-op-veel-relatie, een ERD, `IgnoreCycles`, één query per verzoek | Alle leveranciers met hun artikelen |
| 08_02 | <span style="color:#f08c00">●●○</span> | PC-onderdelen | `Include` en `ThenInclude` door een tussentabel heen, in beide richtingen | Een behuizing met haar ventilatoren |
| 08_03 | <span style="color:#f03e3e">●●●</span> | PC-onderdelen | `Include` voor één object en voor een gefilterde lijst, een omweg uit 07_03 opruimen | Alle bestellingen van één klant |
| 08_04 | <span style="color:#37b24d">●○○</span> | Dierenasiel | Zelfde niveau als 08_01 | Alle verblijven met hun dieren |
| 08_05 | <span style="color:#f08c00">●●○</span> | Fietsverhuur | Zelfde niveau als 08_02 | Een fiets met haar uitrusting |
| 08_06 | <span style="color:#f03e3e">●●●</span> | Concertzaal | Zelfde niveau als 08_03, met een filter op de datum | Alle concerten van één jaar |

In les 08 veranderen je modellen en je database niet. De bestaande endpoints blijven hun navigation properties `null` tonen, op één uitzondering na: <span style="background:#2f9e44;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>POST</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/bestelling/volledig</code> in 08_03. Enkel de nieuwe endpoints vullen ze, telkens met één query. Je terminal toont hoeveel query's een verzoek kost: kijk er bij elk nieuw endpoint naar.

**Buiten bereik.** Je hebt geen DTO's, geen Mapster, geen Dapper en geen gefilterde of gesorteerde `Include` nodig. Daarom ligt de volgorde binnen een meegeladen lijst niet vast. `IgnoreCycles` gebruik je zoals de les: tijdelijk, tot hoofdstuk 09.

---

## Hoofdstuk 09 — DTO's en Mapster

| Oefening | Niveau | Domein | Wat je oefent | Samengestelde oefening |
|----------|--------|--------|----------------|------------------------|
| 09_01 | <span style="color:#37b24d">●○○</span> | PC-onderdelen | Read- en write-DTO's, Mapster, `MapperProfile`, `IgnoreCycles` weg, een platte details-DTO | Het overzicht van alle leveranciers |
| 09_02 | <span style="color:#f08c00">●●○</span> | PC-onderdelen | Vier modellen achter DTO's, een tussentabel platslaan met een eigen mapping | Een behuizing zoals de balie ze wil zien |
| 09_03 | <span style="color:#f03e3e">●●●</span> | PC-onderdelen | Aparte DTO's om toe te voegen en bij te werken, lijsten over twee niveaus platslaan | Het overzicht van een klant |
| 09_04 | <span style="color:#37b24d">●○○</span> | Dierenasiel | Zelfde niveau als 09_01 | Het overzicht van alle verblijven |
| 09_05 | <span style="color:#f08c00">●●○</span> | Fietsverhuur | Zelfde niveau als 09_02, met een update-DTO die minder bevat dan het model | Een fiets zoals de balie ze wil zien |
| 09_06 | <span style="color:#f03e3e">●●●</span> | Concertzaal | Zelfde niveau als 09_03 | Het overzicht van een jaar |

In les 09 veranderen je modellen en je database niet, maar de JSON van elk endpoint wel: een read-DTO toont het model zonder navigation properties, de details-endpoints krijgen een eigen, platte vorm, en in 09_03 en 09_06 geeft het overzicht per klant of per jaar voortaan één object terug in plaats van een lijst. De routes, de controles en de berichten blijven dezelfde.

**Buiten bereik.** Je hebt geen nieuwe tabellen, geen `IgnoreCycles` en geen data annotations nodig. Eén element gaat verder dan de les: les 09 toont hoe je een model naar een DTO omzet, maar niet de omgekeerde richting, en ook niet hoe je met een write-DTO een bestaand model bijwerkt.

---

## Volgorde

Werk per hoofdstuk van laag naar hoog: eerst _01, dan _02, dan _03. Doe de samengestelde oefening pas als de punten ervoor werken — ze bouwt er letterlijk op verder.

De spiegelreeks, _04 tot en met _06, kan je nadien gebruiken als herhaling, of meteen als je merkt dat een concept nog niet zit.

De reeks _07 doe je na de _03 van hetzelfde hoofdstuk, of als laatste herhaling voor je examen. Ze staat los van de andere reeksen: 04_07 begint met een leeg project.

> [!IMPORTANT]
> Begin niet aan een nieuw hoofdstuk voor de _03 van het vorige volledig lukt zonder terug te bladeren.

> [!WARNING]
> Heb je het gevoel dat je iets nodig hebt dat bij een hoofdstuk onder "buiten bereik" staat, dan los je de oefening waarschijnlijk te ingewikkeld op.
