# Extra oefeningen — hoofdstuk 03 tot en met 09

<b>Programming Advanced</b> &nbsp;·&nbsp; zes oefeningen per hoofdstuk &nbsp;·&nbsp; opgesteld op basis van de lessen `03 - Repositories` tot en met `09 - DTO's`

Zelfde opmaak en zelfde opbouw als de oefeningen van hoofdstuk 01 en 02. Twee reeksen die elkaar spiegelen: dezelfde techniek, ander domein. Doe je ze allebei, dan oefen je elk concept twee keer zonder twee keer hetzelfde te lezen.

Elk bestand sluit af met een <span style="background:#7048e8;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>SAMENGESTELDE OEFENING</b></span> : één endpoint, één reeks verzoeken of één volledige cyclus waarin alle voorgaande punten van dat bestand samenkomen. Dat is telkens het laatste genummerde punt.

> [!NOTE]
> Dit overzicht groeit mee. Op dit moment staan hoofdstuk 03 tot en met 09 erin.

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

Elke oefening begint met een punt **Je vertrekpunt**: wat er uit de vorige oefening moet blijven werken. Heb je die oefening niet meer, maak ze dan eerst opnieuw vanuit haar eigen opgave. Het vertrekpunt is een geheugensteun, geen volledige opgave.

Vanaf hoofdstuk 04 krijgt elke oefening ook **vaste startgegevens** en een model met **C#-types**, zoals in de oefeningen van school. Daardoor kan elk voorbeeld het volledige antwoord tonen, en weet je precies wat je API moet teruggeven. In de JSON-voorbeelden mag de volgorde van de velden verschillen; de waarden niet. Bedragen vergelijk je als getal.

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

## Hoofdstuk 04 — Docker, DbContext, Fluent API, migraties

| Oefening | Niveau | Domein | Wat je oefent | Samengestelde oefening |
|----------|--------|--------|----------------|------------------------|
| 04_01 | <span style="color:#37b24d">●○○</span> | PC-onderdelen | Database in Docker, DbContext, Fluent API, eerste migratie, startgegevens met SQL | Een kolom erbij |
| 04_02 | <span style="color:#f08c00">●●○</span> | PC-onderdelen | Twee tabellen in één context, twee migraties, dezelfde regels in API en database | Twee bronnen, één verhaal |
| 04_03 | <span style="color:#f03e3e">●●●</span> | PC-onderdelen | Dezelfde startgegevens in database, geheugen en bestand, `Up` en `Down`, een toegepaste migratie terugdraaien | Een migratie terugdraaien |
| 04_04 | <span style="color:#37b24d">●○○</span> | Dierenasiel | Zelfde niveau als 04_01 | Een kolom erbij |
| 04_05 | <span style="color:#f08c00">●●○</span> | Fietsverhuur | Zelfde niveau als 04_02 | Twee bronnen, één verhaal |
| 04_06 | <span style="color:#f03e3e">●●●</span> | Concertzaal | Zelfde niveau als 04_03 | Een migratie terugdraaien |

Les 04 maakt een database, maar gebruikt ze nog in geen enkel endpoint: dat gebeurt pas in les 05. Daarom blijft je API in dit hoofdstuk werken met de repositories uit hoofdstuk 03, en bouw je de database ernaast op. Aan het einde van elke oefening moeten je API en je database precies dezelfde gegevens tonen, zodat de overstap in les 05 voor de client niets verandert.

Elke oefening krijgt een eigen database in dezelfde PostgreSQL-container. Je mag de container uit les 04 dus voor alle zes gebruiken.

**De samengestelde oefeningen** laten je de cyclus van les 04 opnieuw doorlopen, telkens met iets meer op het spel:

- **Een kolom erbij**, in 04_01 en 04_04: het model groeit, en model, database, startgegevens en API moeten daarna weer gelijklopen.
- **Twee bronnen, één verhaal**, in 04_02 en 04_05: het geheugen van je API en je database moeten dezelfde gegevens tonen en dezelfde regels volgen.
- **Een migratie terugdraaien**, in 04_03 en 04_06: een migratie die de database al kent, moet weg zonder een spoor na te laten.

**Buiten bereik.** Je hebt nog geen repository nodig die met de database praat, geen async, geen relaties tussen tabellen, geen `HasData` en geen DTO's. Drie elementen komen voor die de les niet voordoet:

- het commando om de database terug te brengen naar een eerdere migratie, in 04_03 en 04_06. De les zegt dat het moet, maar niet hoe. Ook het commando om een migratie te verwijderen toont de les enkel voor Visual Studio; je leerboek geeft de vorm voor de terminal;
- de tabel waarin de database bijhoudt welke migraties ze al uitvoerde. De les noemt ze niet, maar je ziet ze in pgAdmin staan, naast je eigen tabellen;
- de teller van een tabel bijstellen met SQL. Dat zie je pas in het SQL-script van les 06.

---

## Hoofdstuk 05 — EF-repository, async en await

| Oefening | Niveau | Domein | Wat je oefent | Samengestelde oefening |
|----------|--------|--------|----------------|------------------------|
| 05_01 | <span style="color:#37b24d">●○○</span> | PC-onderdelen | Een repository op de database, het contract en de controller asynchroon, wisselen in Program.cs, sorteren en Distinct in SQL | Dezelfde antwoorden, nu uit de database |
| 05_02 | <span style="color:#f08c00">●●○</span> | PC-onderdelen | Twee repositories op één context, wijzigingen die echt bewaard worden, de teller en het adres van een nieuw item | Een historiek die blijft |
| 05_03 | <span style="color:#f03e3e">●●●</span> | PC-onderdelen | Een bestandsrepository én een databaserepository achter één asynchroon contract, filteren en sorteren in SQL | Twee repositories, één antwoord |
| 05_04 | <span style="color:#37b24d">●○○</span> | Dierenasiel | Zelfde niveau als 05_01 | Dezelfde antwoorden, nu uit de database |
| 05_05 | <span style="color:#f08c00">●●○</span> | Fietsverhuur | Zelfde niveau als 05_02 | Verhuringen die blijven |
| 05_06 | <span style="color:#f03e3e">●●●</span> | Concertzaal | Zelfde niveau als 05_03, met een plaatscontrole op de database | Twee repositories, één antwoord |

In les 05 neemt de database het over. De samengestelde oefeningen tonen telkens met volledige antwoorden wat je API moet teruggeven, zodat je kan nagaan dat de client niets van de overstap merkt.

**Buiten bereik.** Je hebt geen relaties, geen navigation properties, geen `HasData`, geen generieke repository, geen Unit of Work, geen `Include`, geen DTO's en geen Mapster nodig. Twee elementen komen voor die de les niet voordoet: de asynchrone methodes om een bestand te lezen en te schrijven, in 05_03 en 05_06, en opnieuw de teller van een tabel bijstellen met SQL, wanneer je naar je startgegevens terug wil.

---

## Hoofdstuk 06 — relaties, Fluent API, startgegevens met HasData

| Oefening | Niveau | Domein | Wat je oefent | Samengestelde oefening |
|----------|--------|--------|----------------|------------------------|
| 06_01 | <span style="color:#37b24d">●○○</span> | PC-onderdelen | Eén op veel: leveranciers en artikelen, vreemde sleutel, wat er gebeurt bij verwijderen, startgegevens via de Fluent API | Een leverancier verwijderen |
| 06_02 | <span style="color:#f08c00">●●○</span> | PC-onderdelen | Veel op veel met een tussentabel: welke ventilator past in welke behuizing, en hoeveel keer | Een ventilator vervangen in alle behuizingen |
| 06_03 | <span style="color:#f03e3e">●●●</span> | PC-onderdelen | Bestellijnen onder een bestelling brengen, `OnModelCreating` opsplitsen, berekeningen per bestelling | De fiche per bestelling |
| 06_04 | <span style="color:#37b24d">●○○</span> | Dierenasiel | Zelfde niveau als 06_01, met verblijven | Een verblijf verwijderen |
| 06_05 | <span style="color:#f08c00">●●○</span> | Fietsverhuur | Zelfde niveau als 06_02, met accessoires per fiets | Een fiets vervangen met haar uitrusting |
| 06_06 | <span style="color:#f03e3e">●●●</span> | Concertzaal | Zelfde niveau als 06_03, met een plaatscontrole per concert | Een plaats per concert |

In les 06 bouw je je database telkens opnieuw op, en komen alle startgegevens via de Fluent API. Je database verwijderen hoort niet bij de leerstof: dat mag in pgAdmin, of met `dotnet ef database drop`. Navigation properties horen in dit hoofdstuk leeg te blijven in je JSON: gerelateerde gegevens ophalen is voor les 08. Krijg je toch een *object cycle*, lees dan de waarschuwing in de oefening.

**Buiten bereik.** Je hebt geen generieke repository, geen Unit of Work, geen `Include`, geen `IgnoreCycles`, geen DTO's en geen Mapster nodig. Eén element gaat verder dan de les: de teller van PostgreSQL in orde brengen na startgegevens met `HasData`. Je leerboek waarschuwt ervoor bij de startgegevens.

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

> [!IMPORTANT]
> Begin niet aan een nieuw hoofdstuk voor de _03 van het vorige volledig lukt zonder terug te bladeren.

> [!WARNING]
> Heb je het gevoel dat je iets nodig hebt dat bij een hoofdstuk onder "buiten bereik" staat, dan los je de oefening waarschijnlijk te ingewikkeld op.
