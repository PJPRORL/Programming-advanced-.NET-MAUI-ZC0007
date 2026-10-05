# Extra oefeningen — hoofdstuk 03 tot en met 09

<b>Programming Advanced</b> &nbsp;·&nbsp; zes oefeningen per hoofdstuk &nbsp;·&nbsp; opgesteld op basis van de lessen `03 - Repositories` tot en met `09 - DTO's`

Zelfde opmaak en zelfde opbouw als de oefeningen van hoofdstuk 01 en 02. Twee reeksen die elkaar spiegelen: dezelfde techniek, ander domein. Doe je ze allebei, dan oefen je elk concept twee keer zonder twee keer hetzelfde te lezen.

Elk bestand sluit af met een <span style="background:#7048e8;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>SAMENGESTELDE OEFENING</b></span> : één endpoint of één reeks verzoeken waarin alle voorgaande punten van dat bestand samenkomen. Dat is telkens het laatste genummerde punt.

> [!NOTE]
> Dit overzicht groeit mee. Op dit moment staat hoofdstuk 03 erin. De andere hoofdstukken volgen.

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

## Hoofdstuk 03 — repository, interface, dependency injection, logging

| Oefening | Niveau | Domein | Wat je oefent | Samengestelde oefening |
|----------|--------|--------|----------------|------------------------|
| 03_01 | <span style="color:#37b24d">●○○</span> | PC-onderdelen | 02_01 refactoren: interface, repository, constructor injection, logging, zoeken op merk | De artikelfiche via de repository |
| 03_02 | <span style="color:#f08c00">●●○</span> | PC-onderdelen | 02_02 refactoren: CRUD via de repository, wie beslist wat, gegevens tussen twee verzoeken | De wijzigingshistoriek |
| 03_03 | <span style="color:#f03e3e">●●●</span> | PC-onderdelen | 02_03 refactoren: zelf het contract ontwerpen, een tweede repository in een JSON-bestand, wisselen in Program.cs | Een herstart overleven |
| 03_04 | <span style="color:#37b24d">●○○</span> | Dierenasiel | Zelfde niveau als 03_01, met zoeken op een stukje van de naam | De dierfiche via de repository |
| 03_05 | <span style="color:#f08c00">●●○</span> | Fietsverhuur | Zelfde niveau als 03_02, met regels die bestaande endpoints veranderen | Verhuren en terugbrengen |
| 03_06 | <span style="color:#f03e3e">●●●</span> | Concertzaal | Zelfde niveau als 03_03, met een plaatscontrole die een herstart moet overleven | Een herstart overleven |

### Hoe hoofdstuk 03 op hoofdstuk 02 bouwt

Les 03 is een les in refactoren: de binnenkant van je API verandert grondig, de buitenkant blijft precies dezelfde. Daarom vertrekt elke oefening van hoofdstuk 03 van jouw oplossing van de oefening uit hoofdstuk 02 met hetzelfde nummer: 03_01 van 02_01, 03_02 van 02_02, enzovoort.

Elke oefening begint met een tabel van de endpoints die na je refactoring nog precies hetzelfde moeten antwoorden. Heb je de oefening uit hoofdstuk 02 niet meer, maak die dan eerst opnieuw vanuit haar eigen opgave. De tabel is een geheugensteun, geen volledige opgave.

---

## Over de samengestelde oefeningen

In hoofdstuk 03 doet de samengestelde oefening telkens één van drie dingen:

- **Ze laat iets uit hoofdstuk 02 opnieuw werken, nu de lijst weg is uit de controller.** In 03_01 en 03_04 moet de fiche dezelfde gegevens tonen als vroeger, terwijl de controller alles via de repository moet vragen. Daarbij beslis je zelf wie de fiche samenstelt.
- **Ze zet een tweede repository naast de eerste.** In 03_02 en 03_05 krijgt je controller een tweede repository, die bijhoudt wat er met de eerste gebeurde. Wat daar misloopt, zie je pas als je de endpoints na elkaar test: een actie die mislukt, mag in geen van beide repositories een spoor nalaten.
- **Ze wisselt de opslag onder je controller weg.** In 03_03 en 03_06 werkt dezelfde controller eerst met een lijst in het geheugen en daarna met een bestand. Moet je daarvoor de controller aanpassen, dan was je refactoring niet af.

---

## Volgorde

Werk per hoofdstuk van laag naar hoog: eerst 03_01, dan 03_02, dan 03_03. Doe de samengestelde oefening pas als de punten ervoor werken — ze bouwt er letterlijk op verder.

De spiegelreeks, 03_04 tot en met 03_06, kan je nadien gebruiken als herhaling, of meteen als je merkt dat een concept nog niet zit.

> [!IMPORTANT]
> Begin niet aan hoofdstuk 04 voor 03_03 volledig lukt zonder terug te bladeren.

---

## Buiten bereik

De oefeningen van hoofdstuk 03 blijven binnen wat in les 01 tot en met 03 staat. Je hebt geen database, geen Entity Framework, geen async, geen eigen generics en geen DTO's nodig.

> [!WARNING]
> Heb je het gevoel dat je die wel nodig hebt, dan los je de oefening waarschijnlijk te ingewikkeld op.

Twee elementen komen voor die de les niet voordoet:

- Een repository die zijn gegevens in een bestand bewaart, in 03_03 en 03_06. De les noemt het in de inleiding, maar werkt het niet uit. Het lezen en schrijven van bestanden ken je uit de vorige modules.
- Een tweede model dat geen voorwerp uit de echte wereld beschrijft, maar bijhoudt wat er gebeurde: `Wijziging` in 03_02 en `Verhuring` in 03_05.
