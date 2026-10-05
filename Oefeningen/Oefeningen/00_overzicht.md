# Extra oefeningen — hoofdstuk 01 en 02

<b>Programming Advanced</b> &nbsp;·&nbsp; twaalf oefeningen &nbsp;·&nbsp; opgesteld op basis van `01 - Introductie` en `02 - HTTP Verbs`

Zelfde opmaak als oefening 01_01 van de leerkracht. Twee reeksen die elkaar
spiegelen: dezelfde techniek, ander domein. Doe je ze allebei, dan oefen je elk
concept twee keer zonder twee keer hetzelfde te lezen.

Elk bestand sluit af met een <span style="background:#7048e8;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>SAMENGESTELDE OEFENING</b></span> : één endpoint waarin alle
voorgaande punten van dat bestand samenkomen rond één onderwerp. Dat is telkens
het laatste genummerde punt.

---

## De kleuren in deze bestanden

De HTTP-verbs dragen de kleuren van Scalar, zodat je ze herkent uit je eigen
testomgeving. De statuscodes volgen de kleurcode die je cursus zelf gebruikt in
de samenvatting van hoofdstuk 02. De kleuren zijn gekozen om zowel op een licht
als op een donker thema leesbaar te blijven.

| Kleur | Waarvoor |
|---|---|
| <span style="background:#1c7ed6;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>GET</b></span> | gegevens ophalen |
| <span style="background:#2f9e44;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>POST</b></span> | gegevens toevoegen |
| <span style="background:#d9822b;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>PUT</b></span> | gegevens bijwerken |
| <span style="background:#e03131;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>DELETE</b></span> | gegevens verwijderen |
| <code style="color:#37b24d;font-weight:600">200</code> <code style="color:#37b24d;font-weight:600">201</code> <code style="color:#37b24d;font-weight:600">204</code> | gelukt |
| <code style="color:#f08c00;font-weight:600">400</code> <code style="color:#f08c00;font-weight:600">404</code> | de client maakte een fout |
| <code style="color:#f03e3e;font-weight:600">500</code> | jouw code maakte een fout |
| <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">/onderdelen/soort/<span style="color:#e64980"><b>{soort}</b></span></code> | een route; het vaste deel blauwgroen |
| <code style="color:#e64980;font-weight:600">{soort}</code> | een routeparameter: wat de gebruiker zelf invult |
| <code style="color:#845ef7">De processor voert alle berekeningen uit.</code> | tekst die je API letterlijk moet teruggeven |
| `Ventilator` `AfmetingInMm` `weekend` | klassenamen, eigenschappen en invoerwaarden blijven neutraal |

De kaders in de oefeningen betekenen het volgende:

> [!NOTE]
> Iets om te onthouden, meestal een keuze die verderop in de cursus terugkomt.

> [!TIP]
> Een aanwijzing die je werk korter maakt.

> [!IMPORTANT]
> Een eis waaraan je oplossing moet voldoen.

> [!WARNING]
> Een valkuil. Hier lopen de meeste oplossingen stuk.

De kleuren en de kaders komen volledig tot hun recht in de preview van VS Code.
Open een bestand en druk op `Ctrl+Shift+V`.

---

## Hoofdstuk 01 — controller, routes, routeparameters

| Oefening | Niveau | Domein | Wat je oefent | Samengestelde oefening |
|----------|--------|--------|----------------|------------------------|
| 01_02 | <span style="color:#37b24d">●○○</span> | PC-onderdelen | Controller opzetten, vier GET-endpoints, tekst uit de route bewerken | De onderdelenfiche |
| 01_03 | <span style="color:#f08c00">●●○</span> | PC-onderdelen | Twee routeparameters in één route, vergelijken en rekenen | Het bouwrapport |
| 01_04 | <span style="color:#f03e3e">●●●</span> | PC-onderdelen | Eigen routenaam los van de klassenaam, geneste beslissingen | Het volledige advies |
| 01_05 | <span style="color:#37b24d">●○○</span> | Bibliotheek | Zelfde niveau als 01_02 | De lenersfiche |
| 01_06 | <span style="color:#f08c00">●●○</span> | Zwembad | Zelfde niveau als 01_03, met deling en rest | Het bezoekrapport |
| 01_07 | <span style="color:#f03e3e">●●●</span> | Festival | Zelfde niveau als 01_04, met volgorde van controles als valkuil | Het bezoekersplan |

## Hoofdstuk 02 — model, in-memory lijst, CRUD, statuscodes

| Oefening | Niveau | Domein | Wat je oefent | Samengestelde oefening |
|----------|--------|--------|----------------|------------------------|
| 02_01 | <span style="color:#37b24d">●○○</span> | PC-onderdelen | Model, lijst, GET-endpoints, 200 tegenover 404 | De artikelfiche |
| 02_02 | <span style="color:#f08c00">●●○</span> | PC-onderdelen | Volledige CRUD, 201 en 204, invoer weigeren met 400 | Een ventilator vervangen |
| 02_03 | <span style="color:#f03e3e">●●●</span> | PC-onderdelen | Soft delete, herstellen, berekening over de lijst | De bestelfiche en alles schrappen |
| 02_04 | <span style="color:#37b24d">●○○</span> | Dierenasiel | Zelfde niveau als 02_01 | De dierfiche |
| 02_05 | <span style="color:#f08c00">●●○</span> | Fietsverhuur | Zelfde niveau als 02_02 | Een fiets vervangen |
| 02_06 | <span style="color:#f03e3e">●●●</span> | Concertzaal | Zelfde niveau als 02_03, met een controle tegen de bestaande lijst | Een ticket verhuizen |

---

## Over de samengestelde oefeningen

In de gewone punten staat elk endpoint op zichzelf. Je vraagt uitleg over een
soort onderdeel op, daarna een voorraad die bij niets hoort, daarna een
artikelcode die bij niets hoort. Handig om één techniek te oefenen, maar het
lijkt niet op een echte API.

De samengestelde oefening zet daar één onderwerp tegenover. Je geeft één ding op
en krijgt alles wat je API daarover weet. Dat doet drie dingen met je code:

- **Gegevens verhuizen van de URL naar je controller.** De voorraad van een
  moederbord hoort bij dat moederbord, niet bij het verzoek van de gebruiker.
  In hoofdstuk 01 zet je die gegevens nog met de hand in je code; in hoofdstuk 02
  zit dat al in je Model en je lijst.
- **Je logica moet herbruikbaar worden.** Merk je dat je dezelfde boodschappen
  een tweede keer zit uit te schrijven, dan is dat het signaal dat je code nog
  niet opgesplitst is zoals het hoort.
- **Fouten die je apart niet ziet, komen naar boven.** In 02_02 en 02_05 mag een
  mislukte vervanging niets veranderd hebben. In 02_06 moet een annulering de
  plaats echt vrijgeven. Zulke fouten blijven onzichtbaar zolang je elk endpoint
  apart test.

---

## Volgorde

Werk per hoofdstuk van laag naar hoog: eerst 01_02, dan 01_03, dan 01_04. Doe de
samengestelde oefening pas als de punten ervoor werken — ze bouwt er letterlijk
op verder.

De neutrale reeks kan je nadien gebruiken als herhaling, of meteen als je merkt
dat een concept nog niet zit.

> [!IMPORTANT]
> Begin niet aan hoofdstuk 02 voor 01_04 volledig lukt zonder terug te bladeren.

---

## Buiten bereik

Deze oefeningen blijven binnen wat in hoofdstuk 01 en 02 staat. Je hebt geen
database, geen Entity Framework, geen repositories, geen async en geen DTO's
nodig.

> [!WARNING]
> Heb je het gevoel dat je die wel nodig hebt, dan los je de oefening
> waarschijnlijk te ingewikkeld op.

Twee elementen komen terug die in de cursus niet worden voorgedaan.
<code style="color:#f08c00;font-weight:600">400 Bad Request</code> staat enkel in de statuscodetabel; vanaf 02_02 schrijf je
die zelf. En in 02_01, 02_03, 02_04 en 02_06 maak je een tweede klasse die geen
object uit de echte wereld beschrijft, maar enkel bestaat omdat een client hem
handig vindt. Dat is bewust: je hebt alles wat je nodig hebt om het zelf te
bedenken, en het bereidt je voor op wat later in de cursus komt.
