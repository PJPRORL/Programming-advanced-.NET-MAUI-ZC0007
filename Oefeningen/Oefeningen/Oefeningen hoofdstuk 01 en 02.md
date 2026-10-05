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

---

# 01_02

<b>Hoofdstuk 01</b> &nbsp;·&nbsp; Controller en routeparameters &nbsp;·&nbsp; PC-onderdelen &nbsp;·&nbsp; niveau <span style="color:#37b24d">●○○</span> &nbsp;·&nbsp; ±45 min

## Leerdoel

Na deze oefening kan je zelfstandig een API-controller opzetten en er meerdere GET-endpoints aan koppelen.

Je oefent opnieuw op routes en routeparameters, maar je gebruikt de waarde uit de URL nu ook om er iets mee te berekenen of te bewerken, in plaats van ze enkel door te geven.

Daarnaast merk je dat de naam van je C#-methode niets uitmaakt voor de gebruiker van je API. Enkel de route en het HTTP-verb bepalen wat er bereikbaar is.

## 

## Opdracht

De computerwinkel Bit & Byte wil aan de balie een kleine API waarmee medewerkers snel informatie over onderdelen kunnen opvragen.

Jouw taak is om een OnderdelenController te maken met verschillende GET-endpoints.

Via de API moeten medewerkers:

1. een algemeen openingsbericht kunnen opvragen;
2. uitleg over een soort onderdeel kunnen krijgen;
3. de voorraadstatus van een onderdeel kunnen opvragen;
4. een artikelcode kunnen laten nakijken.

Implementeer onderstaande functionaliteiten.

### 1. Openingsbericht

Voorzie een GET-endpoint op:

<span style="background:#1c7ed6;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>GET</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">/onderdelen</code>

Dit endpoint geeft volgende tekst terug:

<code style="color:#845ef7">Welkom bij de onderdelenbalie van Bit & Byte.</code>

---

### 2. Uitleg over een soort onderdeel

Voorzie een GET-endpoint op:

<span style="background:#1c7ed6;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>GET</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">/onderdelen/soort/<span style="color:#e64980"><b>{soort}</b></span></code>

De soort van het onderdeel wordt meegegeven via de URL.

De API moet drie soorten herkennen:

- moederbord
- processor
- videokaart

Geef voor iedere soort het bijbehorende bericht terug:

| Soort       | Bericht                                                     |
|-------------|-------------------------------------------------------------|
| moederbord  | <span style="color:#845ef7">Het moederbord verbindt alle onderdelen met elkaar.</span> |
| processor   | <span style="color:#845ef7">De processor voert alle berekeningen uit.</span> |
| videokaart  | <span style="color:#845ef7">De videokaart tekent het beeld voor je scherm.</span> |

Wordt een onbekende soort opgegeven, geef dan het bericht terug:

<code style="color:#845ef7">Sorry, dit soort onderdeel verkopen wij niet.</code>

---

### 3. Voorraadstatus

Voorzie een GET-endpoint op:

<span style="background:#1c7ed6;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>GET</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">/onderdelen/voorraad/<span style="color:#e64980"><b>{aantal}</b></span></code>

De routeparameter <code style="color:#e64980;font-weight:600">{aantal}</code> stelt het aantal stuks voor dat nog in het magazijn ligt.

Geef de volgende status:

- 0 stuks: <code style="color:#845ef7">Niet op voorraad, bestel bij de leverancier.</code>
- 1 tot en met 4 stuks: <code style="color:#845ef7">Beperkt op voorraad, hou dit in het oog.</code>
- Meer dan 4 stuks: <code style="color:#845ef7">Ruim op voorraad.</code>

Bijvoorbeeld:

<code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> /onderdelen/voorraad/0</code> geeft als resultaat: <code style="color:#845ef7">Niet op voorraad, bestel bij de leverancier.</code>
<code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> /onderdelen/voorraad/3</code> geeft als resultaat: <code style="color:#845ef7">Beperkt op voorraad, hou dit in het oog.</code>
<code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> /onderdelen/voorraad/12</code> geeft als resultaat: <code style="color:#845ef7">Ruim op voorraad.</code>

---

### 4. Artikelcode nakijken

Voorzie een GET-endpoint op:

<span style="background:#1c7ed6;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>GET</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">/onderdelen/code/<span style="color:#e64980"><b>{artikelcode}</b></span></code>

Het endpoint ontvangt een artikelcode via de route.

Geef een bericht terug dat de code in hoofdletters toont en vermeldt uit hoeveel tekens ze bestaat.

Bijvoorbeeld:

<code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> /onderdelen/code/mb-b650-01</code> geeft als resultaat: <code style="color:#845ef7">Artikelcode MB-B650-01 telt 10 tekens.</code>

> [!NOTE]
> De code moet afkomstig zijn uit de routeparameter. Je mag dus geen artikelcodes hardcoderen.

---

### 5. Alles samen: de onderdelenfiche

<span style="background:#7048e8;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>SAMENGESTELDE OEFENING</b></span>

De drie vorige endpoints staan los van elkaar. Je vraagt uitleg over een soort, maar de voorraad die je daarna opvraagt hoort bij niets, en de artikelcode al evenmin.

In dit laatste endpoint hangt alles aan één onderwerp: je geeft één soort onderdeel op, en je krijgt de uitleg, de voorraad én de artikelcode van dát onderdeel.

Voorzie een GET-endpoint op:

<span style="background:#1c7ed6;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>GET</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">/onderdelen/fiche/<span style="color:#e64980"><b>{soort}</b></span></code>

Je controller kent per soort de volgende gegevens:

| Soort       | Artikelcode  | Voorraad |
|-------------|--------------|----------|
| moederbord  | MB-B650-01   | 12       |
| processor   | CPU-7600X-01 | 3        |
| videokaart  | GPU-4070-01  | 0        |

Geef één antwoord terug dat de drie boodschappen na elkaar zet:

1. de uitleg over de soort, zoals in punt 2;
2. de voorraadstatus, zoals in punt 3, maar dan voor de voorraad uit de tabel hierboven;
3. de artikelcode in hoofdletters met het aantal tekens, zoals in punt 4, maar dan voor de code uit de tabel hierboven.

Bijvoorbeeld:

<code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> /onderdelen/fiche/processor</code> geeft als resultaat:

<code style="color:#845ef7">De processor voert alle berekeningen uit. Beperkt op voorraad, hou dit in het oog. Artikelcode CPU-7600X-01 telt 12 tekens.</code>

<code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> /onderdelen/fiche/videokaart</code> geeft als resultaat:

<code style="color:#845ef7">De videokaart tekent het beeld voor je scherm. Niet op voorraad, bestel bij de leverancier. Artikelcode GPU-4070-01 telt 11 tekens.</code>

Wordt een onbekende soort opgegeven, geef dan het bericht terug:

<code style="color:#845ef7">Sorry, dit soort onderdeel verkopen wij niet.</code>

> [!NOTE]
> De voorraad en de artikelcode staan niet meer in de URL. Ze komen uit je eigen code, op basis van de soort die de gebruiker opgeeft.

> [!TIP]
> Schrijf de drie boodschappen niet opnieuw uit per soort. De logica die je in punt 2, 3 en 4 al geschreven hebt, moet je hier kunnen hergebruiken. Merk je dat je aan het kopiëren bent, kijk dan eerst naar hoe je je code kan opsplitsen.
> 
---

# 01_03

<b>Hoofdstuk 01</b> &nbsp;·&nbsp; Twee routeparameters in één route &nbsp;·&nbsp; PC-onderdelen &nbsp;·&nbsp; niveau <span style="color:#f08c00">●●○</span> &nbsp;·&nbsp; ±60 min

## Leerdoel

Na deze oefening kan je een route opbouwen die meer dan één routeparameter bevat.

Je leert twee waarden uit dezelfde URL opvangen, ze met elkaar vergelijken en er berekeningen mee uitvoeren voor je een antwoord terugstuurt.

Daarnaast oefen je op het verschil tussen een vaste tekst in je route en een variabel stuk: <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">/compatibiliteit/socket/...</code> is vast, <code style="color:#e64980;font-weight:600">{socketProcessor}</code> niet.

## 

## Opdracht

Bit & Byte wil een tweede API die medewerkers helpt bij het nakijken of twee onderdelen bij elkaar passen.

Jouw taak is om een CompatibiliteitController te maken met verschillende GET-endpoints.

Via de API moeten medewerkers:

1. een algemeen bericht kunnen opvragen;
2. twee sockets met elkaar kunnen laten vergelijken;
3. kunnen nakijken of een voeding zwaar genoeg is;
4. kunnen nakijken hoeveel geheugensloten er vrij blijven.

Implementeer onderstaande functionaliteiten.

### 1. Algemeen bericht

Voorzie een GET-endpoint op:

<span style="background:#1c7ed6;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>GET</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">/compatibiliteit</code>

Dit endpoint geeft volgende tekst terug:

<code style="color:#845ef7">Compatibiliteitsbalie Bit & Byte. Geef twee onderdelen op om ze te vergelijken.</code>

---

### 2. Twee sockets vergelijken

Voorzie een GET-endpoint op:

<span style="background:#1c7ed6;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>GET</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">/compatibiliteit/socket/<span style="color:#e64980"><b>{socketProcessor}</b></span>/<span style="color:#e64980"><b>{socketMoederbord}</b></span></code>

Beide sockets worden via de URL meegegeven.

Komen ze overeen, geef dan terug:

<code style="color:#845ef7">De processor past op dit moederbord.</code>

Komen ze niet overeen, geef dan terug:

<code style="color:#845ef7">De processor past niet: <span style="color:#e64980"><b>{socketProcessor}</b></span> tegenover <span style="color:#e64980"><b>{socketMoederbord}</b></span>.</code>

Het verschil tussen hoofdletters en kleine letters mag geen rol spelen. `am5` en `AM5` moeten als dezelfde socket beschouwd worden.

Bijvoorbeeld:

<code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> /compatibiliteit/socket/AM5/am5</code> geeft als resultaat: <code style="color:#845ef7">De processor past op dit moederbord.</code>
<code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> /compatibiliteit/socket/AM5/LGA1700</code> geeft als resultaat: <code style="color:#845ef7">De processor past niet: AM5 tegenover LGA1700.</code>

---

### 3. Voeding nakijken

Voorzie een GET-endpoint op:

<span style="background:#1c7ed6;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>GET</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">/compatibiliteit/voeding/<span style="color:#e64980"><b>{wattage}</b></span>/<span style="color:#e64980"><b>{verbruik}</b></span></code>

De routeparameter <code style="color:#e64980;font-weight:600">{wattage}</code> is het vermogen van de voeding, <code style="color:#e64980;font-weight:600">{verbruik}</code> is het opgetelde verbruik van de volledige build. Beide in watt.

We rekenen met een marge van 100 watt.

Geef het volgende advies:

- Het wattage ligt lager dan het verbruik: <code style="color:#845ef7">De voeding is te zwak voor deze build.</code>
- Het wattage ligt minder dan 100 watt boven het verbruik: <code style="color:#845ef7">De voeding volstaat, maar de marge is krap.</code>
- Het wattage ligt 100 watt of meer boven het verbruik: <code style="color:#845ef7">De voeding is ruim voldoende.</code>

Bijvoorbeeld:

<code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> /compatibiliteit/voeding/550/620</code> geeft als resultaat: <code style="color:#845ef7">De voeding is te zwak voor deze build.</code>
<code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> /compatibiliteit/voeding/650/620</code> geeft als resultaat: <code style="color:#845ef7">De voeding volstaat, maar de marge is krap.</code>
<code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> /compatibiliteit/voeding/850/620</code> geeft als resultaat: <code style="color:#845ef7">De voeding is ruim voldoende.</code>

---

### 4. Vrije geheugensloten

Voorzie een GET-endpoint op:

<span style="background:#1c7ed6;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>GET</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">/compatibiliteit/sloten/<span style="color:#e64980"><b>{aantalSloten}</b></span>/<span style="color:#e64980"><b>{aantalModules}</b></span></code>

Geef het volgende terug:

- Er zijn meer modules dan sloten: <code style="color:#845ef7">Er passen maar <span style="color:#e64980"><b>{aantalSloten}</b></span> modules in dit moederbord.</code>
- Er zijn evenveel modules als sloten: <code style="color:#845ef7">Alle sloten worden gebruikt.</code>
- Er zijn minder modules dan sloten: <code style="color:#845ef7">Er blijven nog <span style="color:#e64980"><b>{aantal}</b></span> sloten vrij.</code>

De waarde <code style="color:#e64980;font-weight:600">{aantal}</code> in het laatste bericht moet je zelf berekenen. Ze staat niet in de URL.

Bijvoorbeeld:

<code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> /compatibiliteit/sloten/2/4</code> geeft als resultaat: <code style="color:#845ef7">Er passen maar 2 modules in dit moederbord.</code>
<code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> /compatibiliteit/sloten/4/4</code> geeft als resultaat: <code style="color:#845ef7">Alle sloten worden gebruikt.</code>
<code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> /compatibiliteit/sloten/4/2</code> geeft als resultaat: <code style="color:#845ef7">Er blijven nog 2 sloten vrij.</code>

---

### 5. Alles samen: het bouwrapport

<span style="background:#7048e8;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>SAMENGESTELDE OEFENING</b></span>

De drie vorige endpoints controleren elk iets anders, maar telkens op losse getallen die nergens bij horen. In dit laatste endpoint horen ze bij één build.

Voorzie een GET-endpoint op:

<span style="background:#1c7ed6;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>GET</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">/compatibiliteit/build/<span style="color:#e64980"><b>{build}</b></span>/<span style="color:#e64980"><b>{socketProcessor}</b></span>/<span style="color:#e64980"><b>{wattage}</b></span>/<span style="color:#e64980"><b>{aantalModules}</b></span></code>

Je controller kent drie standaardbuilds:

| Build    | Socket moederbord | Geheugensloten | Verbruik |
|----------|-------------------|----------------|----------|
| kantoor  | AM5               | 2              | 180      |
| gaming   | AM5               | 4              | 520      |
| montage  | LGA1700           | 4              | 610      |

De klant kiest een build en geeft daarbij op welke processor hij wil, welke voeding hij wil en hoeveel geheugenmodules hij wil. De socket van het moederbord, het aantal geheugensloten en het verbruik van de build komen uit de tabel, niet uit de URL.

Geef één antwoord terug dat de drie oordelen na elkaar zet:

1. de socketvergelijking, zoals in punt 2;
2. het voedingsadvies, zoals in punt 3;
3. het aantal vrije geheugensloten, zoals in punt 4.

Bijvoorbeeld:

<code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> /compatibiliteit/build/gaming/AM5/600/4</code> geeft als resultaat:

<code style="color:#845ef7">De processor past op dit moederbord. De voeding volstaat, maar de marge is krap. Alle sloten worden gebruikt.</code>

<code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> /compatibiliteit/build/kantoor/LGA1700/450/4</code> geeft als resultaat:

<code style="color:#845ef7">De processor past niet: LGA1700 tegenover AM5. De voeding is ruim voldoende. Er passen maar 2 modules in dit moederbord.</code>

Wordt een onbekende build opgegeven, geef dan enkel het bericht terug:

<code style="color:#845ef7">We kennen deze build niet.</code>

> [!IMPORTANT]
> Let op: de drie oordelen mogen elkaar niet beïnvloeden. Ook wanneer de processor niet past, moet je nog steeds iets over de voeding en de sloten zeggen. Een klant wil weten wat er allemaal mis is, niet enkel wat er als eerste misloopt.
> 
---

# 01_04

<b>Hoofdstuk 01</b> &nbsp;·&nbsp; Eigen route en geneste beslissingen &nbsp;·&nbsp; PC-onderdelen &nbsp;·&nbsp; niveau <span style="color:#f03e3e">●●●</span> &nbsp;·&nbsp; ±75 min

## Leerdoel

Na deze oefening kan je zelf bepalen op welke route een controller luistert, los van de naam die de klasse draagt.

Je leert dat `[controller]` in de route een plaatshouder is die je mag vervangen door een eigen naam, en dat de gebruiker van je API de klassenaam nooit te zien krijgt.

Daarnaast oefen je op geneste beslissingen: endpoints waar het antwoord van twee routeparameters samen afhangt, en waar de volgorde van je controles bepaalt of je het juiste antwoord geeft.

## 

## Opdracht

Bit & Byte wil klanten aan de ingang een adviespaneel geven. Marketing heeft beslist dat alle adressen met `bouwadvies` in de URL moeten werken, terwijl de ontwikkelaars de klasse liever `AdviesController` noemen.

Jouw taak is om een AdviesController te maken die luistert op de route <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/bouwadvies</code>.

Via de API moeten klanten:

1. een overzicht van het adviespaneel kunnen opvragen;
2. advies kunnen krijgen op basis van hun budget;
3. advies kunnen krijgen op basis van hun doel én hun budget;
4. advies kunnen krijgen over de koeling van hun processor.

Implementeer onderstaande functionaliteiten.

### 1. Overzicht

Voorzie een GET-endpoint op:

<span style="background:#1c7ed6;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>GET</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/bouwadvies</code>

Dit endpoint geeft volgende tekst terug:

<code style="color:#845ef7">Adviespaneel Bit & Byte. Vraag advies op over je budget, je doel of je koeling.</code>

> [!IMPORTANT]
> De klasse heet AdviesController, maar de route moet <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/bouwadvies</code> zijn. Een gebruiker die naar <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/advies</code> surft, mag niets terugkrijgen.

---

### 2. Advies op basis van budget

Voorzie een GET-endpoint op:

<span style="background:#1c7ed6;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>GET</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/bouwadvies/budget/<span style="color:#e64980"><b>{bedrag}</b></span></code>

De routeparameter <code style="color:#e64980;font-weight:600">{bedrag}</code> is het budget van de klant in euro.

Geef het volgende advies:

- Minder dan 600 euro: <code style="color:#845ef7">Met dit budget kies je best een tweedehandstoestel.</code>
- 600 tot en met 1199 euro: <code style="color:#845ef7">Met dit budget bouw je een degelijke kantoor-pc.</code>
- 1200 tot en met 2499 euro: <code style="color:#845ef7">Met dit budget bouw je een volwaardige gaming-pc.</code>
- 2500 euro of meer: <code style="color:#845ef7">Met dit budget kan je vrijwel alles bouwen.</code>

---

### 3. Advies op basis van doel en budget

Voorzie een GET-endpoint op:

<span style="background:#1c7ed6;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>GET</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/bouwadvies/doel/<span style="color:#e64980"><b>{doel}</b></span>/<span style="color:#e64980"><b>{bedrag}</b></span></code>

De API moet drie doelen herkennen, elk met een eigen minimumbudget:

| Doel      | Minimumbudget |
|-----------|---------------|
| kantoor   | 600           |
| gaming    | 1200          |
| montage   | 1500          |

Geef het volgende advies:

- Het doel is onbekend: <code style="color:#845ef7">We kennen dit doel niet.</code>
- Het bedrag ligt onder het minimum: <code style="color:#845ef7">Voor een <span style="color:#e64980"><b>{doel}</b></span>-pc reken je op minstens <span style="color:#e64980"><b>{minimum}</b></span> euro. Je komt <span style="color:#e64980"><b>{tekort}</b></span> euro te kort.</code>
- Het bedrag is gelijk aan of hoger dan het minimum: <code style="color:#845ef7">Een <span style="color:#e64980"><b>{doel}</b></span>-pc is haalbaar binnen dit budget.</code>

Het tekort moet je zelf berekenen.

Bijvoorbeeld:

<code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> api/bouwadvies/doel/gaming/900</code> geeft als resultaat: <code style="color:#845ef7">Voor een gaming-pc reken je op minstens 1200 euro. Je komt 300 euro te kort.</code>
<code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> api/bouwadvies/doel/kantoor/900</code> geeft als resultaat: <code style="color:#845ef7">Een kantoor-pc is haalbaar binnen dit budget.</code>
<code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> api/bouwadvies/doel/server/900</code> geeft als resultaat: <code style="color:#845ef7">We kennen dit doel niet.</code>

---

### 4. Advies over de koeling

Voorzie een GET-endpoint op:

<span style="background:#1c7ed6;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>GET</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/bouwadvies/koeling/<span style="color:#e64980"><b>{tdp}</b></span>/<span style="color:#e64980"><b>{koeling}</b></span></code>

De routeparameter <code style="color:#e64980;font-weight:600">{tdp}</code> is het warmtevermogen van de processor in watt. De routeparameter <code style="color:#e64980;font-weight:600">{koeling}</code> is `lucht` of `water`.

Geef het volgende advies:

Bij `lucht`:

- 95 watt of minder: <code style="color:#845ef7">Een gewone luchtkoeler volstaat.</code>
- 96 tot en met 150 watt: <code style="color:#845ef7">Kies een zware luchtkoeler.</code>
- Meer dan 150 watt: <code style="color:#845ef7">Luchtkoeling volstaat niet, ga voor waterkoeling.</code>

Bij `water`:

- 250 watt of minder: <code style="color:#845ef7">Een waterkoeling van 240 mm volstaat.</code>
- Meer dan 250 watt: <code style="color:#845ef7">Kies een radiator van 360 mm of groter.</code>

Bij een andere waarde dan `lucht` of `water`: <code style="color:#845ef7">We kennen dit soort koeling niet.</code>

> [!WARNING]
> Let goed op de volgorde waarin je je controles schrijft. Kijk je eerst naar het getal en pas daarna naar het soort koeling, dan geef je voor een onbekend soort koeling toch een antwoord.

---

### 5. Alles samen: het volledige advies

<span style="background:#7048e8;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>SAMENGESTELDE OEFENING</b></span>

De drie vorige endpoints geven elk een stukje advies over een pc die verder niets met de andere twee te maken heeft. In dit laatste endpoint gaat alles over dezelfde pc.

Voorzie een GET-endpoint op:

<span style="background:#1c7ed6;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>GET</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/bouwadvies/volledig/<span style="color:#e64980"><b>{doel}</b></span>/<span style="color:#e64980"><b>{bedrag}</b></span>/<span style="color:#e64980"><b>{tdp}</b></span>/<span style="color:#e64980"><b>{koeling}</b></span></code>

Je controller kent per doel niet alleen een minimumbudget, maar ook welke koeling erbij hoort:

| Doel      | Minimumbudget | Aangeraden koeling |
|-----------|---------------|--------------------|
| kantoor   | 600           | lucht              |
| gaming    | 1200          | lucht              |
| montage   | 1500          | water              |

Geef één antwoord terug dat de volgende boodschappen na elkaar zet:

1. het budgetadvies, zoals in punt 2;
2. het advies op basis van doel en budget, zoals in punt 3;
3. het koeladvies, zoals in punt 4;
4. en, enkel wanneer de gekozen koeling niet die van de tabel is: <code style="color:#845ef7">Voor een <span style="color:#e64980"><b>{doel}</b></span>-pc raden we <span style="color:#e64980"><b>{aangeradenKoeling}</b></span> aan.</code>

Bijvoorbeeld:

<code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> api/bouwadvies/volledig/gaming/1500/140/lucht</code> geeft als resultaat:

<code style="color:#845ef7">Met dit budget bouw je een volwaardige gaming-pc. Een gaming-pc is haalbaar binnen dit budget. Kies een zware luchtkoeler.</code>

<code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> api/bouwadvies/volledig/montage/1000/200/lucht</code> geeft als resultaat:

<code style="color:#845ef7">Met dit budget bouw je een degelijke kantoor-pc. Voor een montage-pc reken je op minstens 1500 euro. Je komt 500 euro te kort. Luchtkoeling volstaat niet, ga voor waterkoeling. Voor een montage-pc raden we water aan.</code>

Wordt een onbekend doel opgegeven, geef dan enkel het bericht terug:

<code style="color:#845ef7">We kennen dit doel niet.</code>

Wordt een onbekende koeling opgegeven, dan blijven de eerste twee boodschappen staan en vervang je de derde door: <code style="color:#845ef7">We kennen dit soort koeling niet.</code> De vierde boodschap valt dan weg.

> [!NOTE]
> Merk op dat de eerste twee boodschappen elkaar mogen tegenspreken. Bij het tweede voorbeeld zegt punt 2 dat je een kantoor-pc kan bouwen, terwijl punt 3 zegt dat een montage-pc te duur is. Dat is geen fout: punt 2 kijkt enkel naar het bedrag en weet niets van het doel. Twee regels die naar hetzelfde getal kijken maar een andere vraag beantwoorden, geven nu eenmaal een ander antwoord.
> 
---

# 01_05

<b>Hoofdstuk 01</b> &nbsp;·&nbsp; Controller en routeparameters &nbsp;·&nbsp; Bibliotheek &nbsp;·&nbsp; niveau <span style="color:#37b24d">●○○</span> &nbsp;·&nbsp; ±45 min

## Leerdoel

Na deze oefening kan je een API-controller maken met meerdere GET-endpoints en er routeparameters aan koppelen.

Je oefent op het omzetten van een waarde uit de URL naar een antwoord: soms via een vaste lijst van mogelijkheden, soms via een reeks getalgrenzen, soms door de tekst zelf te bewerken.

Daarnaast leer je dat een route uit meerdere vaste delen kan bestaan voor de parameter komt, zoals <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">/bibliotheek/afdeling/<span style="color:#e64980"><b>{afdeling}</b></span></code>.

## 

## Opdracht

De stadsbibliotheek De Leeszaal wil bezoekers via een kleine API wegwijs maken.

Jouw taak is om een BibliotheekController te maken met verschillende GET-endpoints.

Via de API moeten bezoekers:

1. een algemeen welkomstbericht kunnen opvragen;
2. informatie over een afdeling kunnen opvragen;
3. weten wat een te late teruggave kost;
4. de controlecode van hun lidkaart kunnen nakijken.

Implementeer onderstaande functionaliteiten.

### 1. Welkomstbericht

Voorzie een GET-endpoint op:

<span style="background:#1c7ed6;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>GET</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">/bibliotheek</code>

Dit endpoint geeft volgende tekst terug:

<code style="color:#845ef7">Welkom in De Leeszaal. Wij zijn open van dinsdag tot zaterdag.</code>

---

### 2. Informatie over een afdeling

Voorzie een GET-endpoint op:

<span style="background:#1c7ed6;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>GET</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">/bibliotheek/afdeling/<span style="color:#e64980"><b>{afdeling}</b></span></code>

De API moet drie afdelingen herkennen:

- jeugd
- strips
- studie

Geef voor iedere afdeling het bijbehorende bericht terug:

| Afdeling | Bericht                                                          |
|----------|------------------------------------------------------------------|
| jeugd    | <span style="color:#845ef7">De jeugdafdeling vind je op het gelijkvloers, achteraan links.</span> |
| strips   | <span style="color:#845ef7">De stripafdeling vind je op de eerste verdieping.</span> |
| studie   | <span style="color:#845ef7">De studiezaal vind je op de tweede verdieping en is stiltezone.</span> |

Wordt een onbekende afdeling opgegeven, geef dan het bericht terug:

<code style="color:#845ef7">Sorry, deze afdeling bestaat niet in onze bibliotheek.</code>

---

### 3. Boete bij te late teruggave

Voorzie een GET-endpoint op:

<span style="background:#1c7ed6;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>GET</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">/bibliotheek/boete/<span style="color:#e64980"><b>{dagen}</b></span></code>

De routeparameter <code style="color:#e64980;font-weight:600">{dagen}</code> stelt het aantal dagen voor dat een boek te laat is.

Geef het volgende bericht:

- 0 dagen: <code style="color:#845ef7">Je bent op tijd, er is geen boete.</code>
- 1 tot en met 7 dagen: <code style="color:#845ef7">Je betaalt een boete van 1 euro.</code>
- Meer dan 7 dagen: <code style="color:#845ef7">Je betaalt een boete van 5 euro en je lidkaart wordt geblokkeerd.</code>

Bijvoorbeeld:

<code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> /bibliotheek/boete/0</code> geeft als resultaat: <code style="color:#845ef7">Je bent op tijd, er is geen boete.</code>
<code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> /bibliotheek/boete/4</code> geeft als resultaat: <code style="color:#845ef7">Je betaalt een boete van 1 euro.</code>
<code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> /bibliotheek/boete/20</code> geeft als resultaat: <code style="color:#845ef7">Je betaalt een boete van 5 euro en je lidkaart wordt geblokkeerd.</code>

---

### 4. Controlecode van een lidkaart

Voorzie een GET-endpoint op:

<span style="background:#1c7ed6;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>GET</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">/bibliotheek/lidkaart/<span style="color:#e64980"><b>{nummer}</b></span></code>

Het endpoint ontvangt een lidkaartnummer via de route. De laatste vier tekens van dat nummer vormen de controlecode.

Geef een bericht terug met het volledige nummer en de controlecode.

Bijvoorbeeld:

<code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> /bibliotheek/lidkaart/LZ-2026-4471</code> geeft als resultaat: <code style="color:#845ef7">Lidkaart LZ-2026-4471 heeft controlecode 4471.</code>

> [!NOTE]
> Het nummer moet afkomstig zijn uit de routeparameter. Je mag dus geen nummers hardcoderen.

---

### 5. Alles samen: de lenersfiche

<span style="background:#7048e8;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>SAMENGESTELDE OEFENING</b></span>

De drie vorige endpoints staan los van elkaar. Je vraagt een afdeling op, daarna een boete die bij niemand hoort, daarna een lidkaartnummer dat verder nergens mee te maken heeft.

In dit laatste endpoint hangt alles aan één lener: je geeft één lidkaartnummer op, en je krijgt de controlecode, de afdeling waar zijn boek ligt én de boete die hij moet betalen.

Voorzie een GET-endpoint op:

<span style="background:#1c7ed6;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>GET</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">/bibliotheek/lener/<span style="color:#e64980"><b>{nummer}</b></span></code>

Je controller kent de volgende leners:

| Lidkaartnummer | Afdeling | Dagen te laat |
|----------------|----------|---------------|
| LZ-2026-4471   | jeugd    | 0             |
| LZ-2026-8820   | strips   | 4             |
| LZ-2025-1093   | studie   | 19            |

Geef één antwoord terug dat de drie boodschappen na elkaar zet:

1. het nummer met zijn controlecode, zoals in punt 4;
2. de informatie over de afdeling, zoals in punt 2;
3. de boete, zoals in punt 3.

Bijvoorbeeld:

<code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> /bibliotheek/lener/LZ-2026-8820</code> geeft als resultaat:

<code style="color:#845ef7">Lidkaart LZ-2026-8820 heeft controlecode 8820. De stripafdeling vind je op de eerste verdieping. Je betaalt een boete van 1 euro.</code>

<code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> /bibliotheek/lener/LZ-2026-4471</code> geeft als resultaat:

<code style="color:#845ef7">Lidkaart LZ-2026-4471 heeft controlecode 4471. De jeugdafdeling vind je op het gelijkvloers, achteraan links. Je bent op tijd, er is geen boete.</code>

Wordt een onbekend lidkaartnummer opgegeven, geef dan het bericht terug:

<code style="color:#845ef7">We kennen dit lidkaartnummer niet.</code>

> [!NOTE]
> De afdeling en het aantal dagen staan niet meer in de URL. Ze komen uit je eigen code, op basis van het nummer dat de gebruiker opgeeft.

> [!TIP]
> Schrijf de drie boodschappen niet opnieuw uit per lener. De logica uit punt 2, 3 en 4 moet je hier kunnen hergebruiken.
> 
---

# 01_06

<b>Hoofdstuk 01</b> &nbsp;·&nbsp; Twee routeparameters in één route &nbsp;·&nbsp; Zwembad &nbsp;·&nbsp; niveau <span style="color:#f08c00">●●○</span> &nbsp;·&nbsp; ±60 min

## Leerdoel

Na deze oefening kan je een route opbouwen die twee routeparameters bevat, en de twee waarden samen gebruiken om tot één antwoord te komen.

Je leert dat een tekstparameter en een getalparameter in dezelfde route kunnen voorkomen, en dat je daar een geneste beslissing op kan bouwen.

Daarnaast oefen je op rekenen met routeparameters: delen, een rest overhouden, en dat resultaat in je antwoord verwerken.

## 

## Opdracht

Zwembad De Waterval wil een API waarmee bezoekers hun bezoek kunnen voorbereiden.

Jouw taak is om een ZwembadController te maken met verschillende GET-endpoints.

Via de API moeten bezoekers:

1. de openingsuren kunnen opvragen;
2. hun tarief kunnen berekenen;
3. weten hoe warm het water is;
4. weten hoe druk het in de banen wordt.

Implementeer onderstaande functionaliteiten.

### 1. Openingsuren

Voorzie een GET-endpoint op:

<span style="background:#1c7ed6;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>GET</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">/zwembad</code>

Dit endpoint geeft volgende tekst terug:

<code style="color:#845ef7">Zwembad De Waterval is elke dag open van 7 tot 21 uur.</code>

---

### 2. Tarief berekenen

Voorzie een GET-endpoint op:

<span style="background:#1c7ed6;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>GET</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">/zwembad/tarief/<span style="color:#e64980"><b>{leeftijd}</b></span>/<span style="color:#e64980"><b>{dag}</b></span></code>

De routeparameter <code style="color:#e64980;font-weight:600">{dag}</code> is `week` of `weekend`.

Geef het volgende tarief:

Bij `week`:

- Jonger dan 3 jaar: `Gratis.`
- 3 tot en met 17 jaar: <code style="color:#845ef7">Je betaalt 2 euro.</code>
- 18 tot en met 64 jaar: <code style="color:#845ef7">Je betaalt 4 euro.</code>
- 65 jaar of ouder: <code style="color:#845ef7">Je betaalt 3 euro.</code>

Bij `weekend` gelden dezelfde leeftijdsgroepen, maar ligt elk tarief 1 euro hoger. Wie gratis binnen mag, blijft gratis binnen.

Bij een andere waarde dan `week` of `weekend`: <code style="color:#845ef7">We kennen deze dag niet.</code>

Bijvoorbeeld:

<code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> /zwembad/tarief/2/weekend</code> geeft als resultaat: `Gratis.`
<code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> /zwembad/tarief/30/week</code> geeft als resultaat: <code style="color:#845ef7">Je betaalt 4 euro.</code>
<code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> /zwembad/tarief/30/weekend</code> geeft als resultaat: <code style="color:#845ef7">Je betaalt 5 euro.</code>

---

### 3. Watertemperatuur

Voorzie een GET-endpoint op:

<span style="background:#1c7ed6;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>GET</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">/zwembad/temperatuur/<span style="color:#e64980"><b>{graden}</b></span></code>

Geef het volgende bericht:

- Minder dan 24 graden: <code style="color:#845ef7">Het water is fris, neem je tijd om in te stappen.</code>
- 24 tot en met 28 graden: <code style="color:#845ef7">Het water heeft een aangename temperatuur.</code>
- Meer dan 28 graden: <code style="color:#845ef7">Het water is warm, ideaal voor de kleinsten.</code>

---

### 4. Drukte in de banen

Voorzie een GET-endpoint op:

<span style="background:#1c7ed6;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>GET</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">/zwembad/banen/<span style="color:#e64980"><b>{aantalBanen}</b></span>/<span style="color:#e64980"><b>{aantalZwemmers}</b></span></code>

Geef het volgende bericht:

- Er zijn evenveel of minder zwemmers dan banen: <code style="color:#845ef7">Iedereen krijgt een eigen baan.</code>
- Er zijn meer zwemmers dan banen, en de verdeling gaat gelijk op: <code style="color:#845ef7">Er zwemmen <span style="color:#e64980"><b>{aantal}</b></span> zwemmers per baan.</code>
- Er zijn meer zwemmers dan banen, en er blijft een rest over: <code style="color:#845ef7">Er zwemmen <span style="color:#e64980"><b>{aantal}</b></span> zwemmers per baan, <span style="color:#e64980"><b>{rest}</b></span> banen krijgen er één extra.</code>

Zowel <code style="color:#e64980;font-weight:600">{aantal}</code> als <code style="color:#e64980;font-weight:600">{rest}</code> moet je zelf berekenen. Ze staan niet in de URL.

Bijvoorbeeld:

<code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> /zwembad/banen/4/3</code> geeft als resultaat: <code style="color:#845ef7">Iedereen krijgt een eigen baan.</code>
<code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> /zwembad/banen/4/8</code> geeft als resultaat: <code style="color:#845ef7">Er zwemmen 2 zwemmers per baan.</code>
<code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> /zwembad/banen/4/10</code> geeft als resultaat: <code style="color:#845ef7">Er zwemmen 2 zwemmers per baan, 2 banen krijgen er één extra.</code>

---

### 5. Alles samen: het bezoekrapport

<span style="background:#7048e8;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>SAMENGESTELDE OEFENING</b></span>

De drie vorige endpoints gaan elk over iets anders. In dit laatste endpoint gaan ze alle drie over hetzelfde bezoek.

Voorzie een GET-endpoint op:

<span style="background:#1c7ed6;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>GET</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">/zwembad/bezoek/<span style="color:#e64980"><b>{leeftijd}</b></span>/<span style="color:#e64980"><b>{dag}</b></span>/<span style="color:#e64980"><b>{graden}</b></span>/<span style="color:#e64980"><b>{aantalZwemmers}</b></span></code>

Het aantal banen staat niet in de URL. De Waterval heeft er zes, en dat weet je controller.

Geef één antwoord terug dat de drie boodschappen na elkaar zet:

1. het tarief, zoals in punt 2;
2. de watertemperatuur, zoals in punt 3;
3. de drukte in de banen, zoals in punt 4, gerekend met zes banen.

Bijvoorbeeld:

<code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> /zwembad/bezoek/30/weekend/26/14</code> geeft als resultaat:

<code style="color:#845ef7">Je betaalt 5 euro. Het water heeft een aangename temperatuur. Er zwemmen 2 zwemmers per baan, 2 banen krijgen er één extra.</code>

<code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> /zwembad/bezoek/2/week/22/4</code> geeft als resultaat:

<code style="color:#845ef7">Gratis. Het water is fris, neem je tijd om in te stappen. Iedereen krijgt een eigen baan.</code>

Wordt een andere dag dan `week` of `weekend` opgegeven, geef dan enkel het bericht terug:

<code style="color:#845ef7">We kennen deze dag niet.</code>

> [!NOTE]
> Het aantal banen zit nu vast in je controller in plaats van in de URL. Dat lijkt een detail, maar het is een eerste stap richting het volgende hoofdstuk: gegevens die bij het zwembad horen, horen bij het zwembad en niet bij het verzoek van de bezoeker.
> 
---

# 01_07

<b>Hoofdstuk 01</b> &nbsp;·&nbsp; Eigen route en geneste beslissingen &nbsp;·&nbsp; Festival &nbsp;·&nbsp; niveau <span style="color:#f03e3e">●●●</span> &nbsp;·&nbsp; ±75 min

## Leerdoel

Na deze oefening kan je zelf bepalen op welke route een controller luistert, los van de naam van de klasse.

Je leert dat `[controller]` in de route een plaatshouder is die je mag vervangen, en dat de gebruiker van je API nooit te zien krijgt hoe je klassen en methodes heten.

Daarnaast oefen je op geneste beslissingen waarbij de volgorde van je controles bepaalt of je antwoord klopt. Een regel die je te vroeg controleert, houdt de rest tegen.

## 

## Opdracht

Festival Klankdal wil bezoekers een infopaneel geven. De organisatie eist dat alle adressen met `festival` in de URL werken, terwijl de ontwikkelaars de klasse liever `PodiumController` noemen.

Jouw taak is om een PodiumController te maken die luistert op de route <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/festival</code>.

Via de API moeten bezoekers:

1. een overzicht van het festival kunnen opvragen;
2. weten wie er op een podium speelt;
3. de prijs van hun ticket kennen;
4. weten wat ze moeten meenemen.

Implementeer onderstaande functionaliteiten.

### 1. Overzicht

Voorzie een GET-endpoint op:

<span style="background:#1c7ed6;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>GET</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/festival</code>

Dit endpoint geeft volgende tekst terug:

<code style="color:#845ef7">Welkom op Klankdal. Drie podia, twee dagen, één weide.</code>

> [!IMPORTANT]
> De klasse heet PodiumController, maar de route moet <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/festival</code> zijn. Een bezoeker die naar <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/podium</code> surft, mag niets terugkrijgen.

---

### 2. Wie speelt er op een podium

Voorzie een GET-endpoint op:

<span style="background:#1c7ed6;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>GET</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/festival/podium/<span style="color:#e64980"><b>{podium}</b></span>/<span style="color:#e64980"><b>{uur}</b></span></code>

De routeparameter <code style="color:#e64980;font-weight:600">{uur}</code> is een getal van 0 tot en met 23.

De API moet drie podia herkennen:

Bij `hoofdpodium`:

- Voor 18 uur: <code style="color:#845ef7">Op het hoofdpodium spelen nu de opwarmers.</code>
- 18 tot en met 21 uur: <code style="color:#845ef7">Op het hoofdpodium speelt nu de hoofdact.</code>
- Na 21 uur: <code style="color:#845ef7">Het hoofdpodium is gesloten.</code>

Bij `tent`:

- Voor 22 uur: <code style="color:#845ef7">In de tent draaien de dj's van de dag.</code>
- Vanaf 22 uur: <code style="color:#845ef7">In de tent begint nu de nachtset.</code>

Bij `strand`:

- Op elk uur: <code style="color:#845ef7">Op het strandpodium speelt akoestische muziek.</code>

Bij een onbekend podium: <code style="color:#845ef7">Dit podium staat niet op het terrein.</code>

---

### 3. Prijs van een ticket

Voorzie een GET-endpoint op:

<span style="background:#1c7ed6;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>GET</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/festival/ticket/<span style="color:#e64980"><b>{type}</b></span>/<span style="color:#e64980"><b>{leeftijd}</b></span></code>

De API moet drie tickettypes herkennen, elk met een eigen basisprijs:

| Type  | Basisprijs |
|-------|------------|
| dag   | 65         |
| combi | 120        |
| vip   | 210        |

Bereken de prijs als volgt:

- Jonger dan 12 jaar: gratis, welk van de drie types ook gekozen wordt. Geef terug: <code style="color:#845ef7">Kinderen jonger dan 12 komen gratis binnen.</code>
- 12 tot en met 17 jaar: de helft van de basisprijs. Geef terug: <code style="color:#845ef7">Een <span style="color:#e64980"><b>{type}</b></span>ticket kost jou <span style="color:#e64980"><b>{prijs}</b></span> euro.</code>
- 18 jaar of ouder: de volle basisprijs. Geef terug: <code style="color:#845ef7">Een <span style="color:#e64980"><b>{type}</b></span>ticket kost jou <span style="color:#e64980"><b>{prijs}</b></span> euro.</code>

Bij een onbekend type: <code style="color:#845ef7">Dit tickettype bestaat niet.</code>

> [!WARNING]
> Let op de volgorde. Een kind van 8 jaar dat een onbekend tickettype opgeeft, moet <code style="color:#845ef7">Dit tickettype bestaat niet.</code> terugkrijgen en niet de gratis boodschap.

Bijvoorbeeld:

<code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> api/festival/ticket/combi/8</code> geeft als resultaat: <code style="color:#845ef7">Kinderen jonger dan 12 komen gratis binnen.</code>
<code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> api/festival/ticket/combi/15</code> geeft als resultaat: <code style="color:#845ef7">Een combiticket kost jou 60 euro.</code>
<code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> api/festival/ticket/vip/30</code> geeft als resultaat: <code style="color:#845ef7">Een vipticket kost jou 210 euro.</code>

---

### 4. Wat moet je meenemen

Voorzie een GET-endpoint op:

<span style="background:#1c7ed6;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>GET</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/festival/weer/<span style="color:#e64980"><b>{graden}</b></span>/<span style="color:#e64980"><b>{regenkans}</b></span></code>

De routeparameter <code style="color:#e64980;font-weight:600">{regenkans}</code> is een percentage van 0 tot en met 100.

Geef het volgende advies, in deze volgorde van belangrijkheid:

1. Is de regenkans groter dan 70: <code style="color:#845ef7">Neem een regenjas mee.</code>
2. Is het anders warmer dan 28 graden: <code style="color:#845ef7">Neem water en zonnecrème mee.</code>
3. Is het anders kouder dan 12 graden: <code style="color:#845ef7">Trek een warme trui aan.</code>
4. In alle andere gevallen: <code style="color:#845ef7">Ideaal festivalweer.</code>

Bijvoorbeeld:

<code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> api/festival/weer/31/85</code> geeft als resultaat: <code style="color:#845ef7">Neem een regenjas mee.</code>
<code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> api/festival/weer/31/20</code> geeft als resultaat: <code style="color:#845ef7">Neem water en zonnecrème mee.</code>
<code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> api/festival/weer/9/20</code> geeft als resultaat: <code style="color:#845ef7">Trek een warme trui aan.</code>
<code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> api/festival/weer/20/20</code> geeft als resultaat: <code style="color:#845ef7">Ideaal festivalweer.</code>

---

### 5. Alles samen: het bezoekersplan

<span style="background:#7048e8;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>SAMENGESTELDE OEFENING</b></span>

De drie vorige endpoints staan los van elkaar: je vraagt een ticketprijs op, daarna wie er ergens speelt, daarna wat het weer doet op een moment dat met dat podium niets te maken heeft.

In dit laatste endpoint hangt alles aan één bezoeker op één moment.

Voorzie een GET-endpoint op:

<span style="background:#1c7ed6;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>GET</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/festival/plan/<span style="color:#e64980"><b>{type}</b></span>/<span style="color:#e64980"><b>{leeftijd}</b></span>/<span style="color:#e64980"><b>{podium}</b></span>/<span style="color:#e64980"><b>{uur}</b></span></code>

De weersvoorspelling staat niet meer in de URL. Je controller kent ze per moment van de dag:

| Moment              | Graden | Regenkans |
|---------------------|--------|-----------|
| voor 12 uur         | 14     | 20        |
| 12 tot en met 17 uur| 29     | 10        |
| 18 tot en met 21 uur| 22     | 80        |
| vanaf 22 uur        | 11     | 30        |

Geef één antwoord terug dat de drie boodschappen na elkaar zet:

1. de ticketprijs, zoals in punt 3;
2. wie er op dat podium op dat uur speelt, zoals in punt 2;
3. wat de bezoeker moet meenemen, zoals in punt 4, gerekend met het weer uit de tabel hierboven.

Bijvoorbeeld:

<code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> api/festival/plan/combi/15/hoofdpodium/19</code> geeft als resultaat:

<code style="color:#845ef7">Een combiticket kost jou 60 euro. Op het hoofdpodium speelt nu de hoofdact. Neem een regenjas mee.</code>

<code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> api/festival/plan/dag/30/strand/14</code> geeft als resultaat:

<code style="color:#845ef7">Een dagticket kost jou 65 euro. Op het strandpodium speelt akoestische muziek. Neem water en zonnecrème mee.</code>

Wordt een onbekend tickettype opgegeven, geef dan enkel het bericht terug:

<code style="color:#845ef7">Dit tickettype bestaat niet.</code>

Wordt een onbekend podium opgegeven, dan blijven de eerste en de derde boodschap staan en vervang je de tweede door: <code style="color:#845ef7">Dit podium staat niet op het terrein.</code>

> [!NOTE]
> Het uur doet nu twee dingen tegelijk: het bepaalt wie er speelt én welk weer er staat. Eén routeparameter die twee verschillende beslissingen voedt, is precies wat het samenbrengen van deze oefening betekent.
> 
---

# 02_01

<b>Hoofdstuk 02</b> &nbsp;·&nbsp; Model, lijst en statuscodes &nbsp;·&nbsp; PC-onderdelen &nbsp;·&nbsp; niveau <span style="color:#37b24d">●○○</span> &nbsp;·&nbsp; ±60 min

## Leerdoel

Na deze oefening kan je een Model opstellen dat een object uit de echte wereld beschrijft, en het vullen met een tijdelijke lijst in het geheugen.

Je leert die gegevens via GET-endpoints beschikbaar stellen en je antwoord verpakken in `ActionResult<T>`, zodat je naast de data ook een HTTP-statuscode kan meesturen.

Daarnaast leer je wanneer een leeg resultaat een fout is (404) en wanneer het gewoon een geldig antwoord is (200). Dat is een keuze die je per endpoint bewust maakt.

## 

## Opdracht

Bit & Byte wil de voorraad van de winkel via een API ontsluiten, zodat de website later dezelfde gegevens kan tonen als de balie.

Jouw taak is om een Model en een ArtikelController te maken.

Via de API moet de website:

1. de volledige voorraad kunnen opvragen;
2. één artikel kunnen opvragen op basis van zijn Id;
3. alle artikelen van één soort kunnen opvragen;
4. enkel de artikelen kunnen opvragen die nog op voorraad liggen.

Implementeer onderstaande functionaliteiten.

### 1. Het model

Maak in de map Models een klasse `Artikel` met volgende gegevens:

| Gegeven          | Soort waarde |
|------------------|--------------|
| Id               | geheel getal |
| Naam             | tekst        |
| Merk             | tekst        |
| Soort            | tekst        |
| Prijs            | kommagetal   |
| AantalOpVoorraad | geheel getal |

---

### 2. De controller en de tijdelijke voorraad

Maak een ArtikelController die luistert op <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/artikel</code>.

Voorzie in de controller een lijst met minstens zes artikelen. Zorg dat er minstens twee soorten in voorkomen, en dat minstens één artikel een voorraad van 0 heeft.

---

### 3. Volledige voorraad opvragen

Voorzie een GET-endpoint op:

<span style="background:#1c7ed6;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>GET</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/artikel</code>

Dit endpoint geeft de volledige lijst terug met statuscode <code style="color:#37b24d;font-weight:600">200 OK</code>.

---

### 4. Eén artikel opvragen

Voorzie een GET-endpoint op:

<span style="background:#1c7ed6;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>GET</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/artikel/<span style="color:#e64980"><b>{id}</b></span></code>

Bestaat het artikel, geef het dan terug met statuscode <code style="color:#37b24d;font-weight:600">200 OK</code>.

Bestaat het artikel niet, geef dan statuscode <code style="color:#f08c00;font-weight:600">404 Not Found</code> met het bericht:

<code style="color:#845ef7">We vonden geen artikel met Id <span style="color:#e64980"><b>{id}</b></span>.</code>

---

### 5. Artikelen van één soort opvragen

Voorzie een GET-endpoint op:

<span style="background:#1c7ed6;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>GET</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/artikel/soort/<span style="color:#e64980"><b>{soort}</b></span></code>

Dit endpoint geeft alle artikelen van die soort terug met statuscode <code style="color:#37b24d;font-weight:600">200 OK</code>.

Zijn er geen artikelen van die soort, geef dan statuscode <code style="color:#f08c00;font-weight:600">404 Not Found</code> met het bericht:

<code style="color:#845ef7">We vonden geen artikelen van de soort <span style="color:#e64980"><b>{soort}</b></span>.</code>

> [!WARNING]
> Let op: deze route en de route uit punt 4 lijken op elkaar. Zorg dat een verzoek naar <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/artikel/soort/processor</code> niet per ongeluk bij het endpoint van punt 4 terechtkomt.

---

### 6. Enkel wat op voorraad is

Voorzie een GET-endpoint op:

<span style="background:#1c7ed6;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>GET</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/artikel/voorradig</code>

Dit endpoint geeft enkel de artikelen terug waarvan de voorraad groter is dan 0.

Is er niets op voorraad, geef dan een lege lijst terug met statuscode <code style="color:#37b24d;font-weight:600">200 OK</code>, en dus géén 404.

> [!NOTE]
> Een lege voorraad is namelijk geen fout: de vraag was geldig en het antwoord is "niets". Dat is een ander geval dan een artikel of een soort die niet bestaat.

---

### 7. Alles samen: de artikelfiche

<span style="background:#7048e8;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>SAMENGESTELDE OEFENING</b></span>

De vier vorige endpoints beantwoorden elk één vraag over de voorraad. Een medewerker die alles over één artikel wil weten, moet ze nu alle vier oproepen en de antwoorden zelf bij elkaar leggen.

In dit laatste endpoint doe je dat werk voor hem: één artikel, één antwoord, met alles erin.

Maak in de map Models een tweede klasse `ArtikelFiche` met volgende gegevens:

| Gegeven                | Soort waarde              |
|------------------------|---------------------------|
| Artikel                | het artikel zelf          |
| IsVoorradig            | ja of nee                 |
| AantalVanDezelfdeSoort | geheel getal              |
| IsGoedkoopsteVanDeSoort| ja of nee                 |

`AantalVanDezelfdeSoort` telt de andere artikelen van dezelfde soort. Het artikel zelf telt niet mee.

`IsGoedkoopsteVanDeSoort` zegt of geen enkel ander artikel van die soort goedkoper is.

Voorzie een GET-endpoint op:

<span style="background:#1c7ed6;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>GET</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/artikel/fiche/<span style="color:#e64980"><b>{id}</b></span></code>

Bestaat het artikel, geef dan de fiche terug met statuscode <code style="color:#37b24d;font-weight:600">200 OK</code>.

Bestaat het artikel niet, geef dan statuscode <code style="color:#f08c00;font-weight:600">404 Not Found</code> met het bericht:

<code style="color:#845ef7">We vonden geen artikel met Id <span style="color:#e64980"><b>{id}</b></span>.</code>

Test je endpoint en controleer:

- of `AantalVanDezelfdeSoort` op 0 staat bij een artikel dat als enige van zijn soort in de lijst zit;
- of `IsVoorradig` op nee staat bij het artikel waarvan je de voorraad op 0 hebt gezet;
- of `IsGoedkoopsteVanDeSoort` bij twee artikelen van dezelfde soort maar bij één van de twee op ja staat.

> [!NOTE]
> Dit is de eerste keer dat je een klasse maakt die geen object uit de echte wereld beschrijft. Een artikel bestaat, een artikelfiche niet: die bestaat alleen omdat een client hem handig vindt. Onthoud dat verschil, het komt later in de cursus terug.
> 
---

# 02_02

<b>Hoofdstuk 02</b> &nbsp;·&nbsp; Volledige CRUD &nbsp;·&nbsp; PC-onderdelen &nbsp;·&nbsp; niveau <span style="color:#f08c00">●●○</span> &nbsp;·&nbsp; ±90 min

## Leerdoel

Na deze oefening kan je de volledige CRUD-cyclus op een Model uitvoeren via HTTP: ophalen, toevoegen, bijwerken en verwijderen.

Je leert data ontvangen in de body van een verzoek in plaats van in de URL, en je leert per actie de juiste statuscode terugsturen: <code style="color:#37b24d;font-weight:600">201 Created</code> bij een POST, <code style="color:#37b24d;font-weight:600">204 No Content</code> bij een PUT en een DELETE.

Daarnaast leer je foutieve invoer afwijzen met <code style="color:#f08c00;font-weight:600">400 Bad Request</code>, zodat je API niet zomaar onmogelijke gegevens in zijn lijst laat binnensluipen.

## 

## Opdracht

Bit & Byte breidt het assortiment uit met ventilatoren. De inkoopafdeling wil ze via de API kunnen beheren zonder tussenkomst van een ontwikkelaar.

Jouw taak is om een Model en een VentilatorController te maken met een volledige CRUD.

Via de API moet de inkoopafdeling:

1. alle ventilatoren kunnen opvragen;
2. één ventilator kunnen opvragen;
3. een nieuwe ventilator kunnen toevoegen;
4. een bestaande ventilator kunnen bijwerken;
5. een ventilator kunnen verwijderen.

Implementeer onderstaande functionaliteiten.

### 1. Het model

Maak in de map Models een klasse `Ventilator` met volgende gegevens:

| Gegeven      | Soort waarde |
|--------------|--------------|
| Id           | geheel getal |
| Merk         | tekst        |
| AfmetingInMm | geheel getal |
| Verlichting  | tekst        |
| Prijs        | kommagetal   |

---

### 2. De controller en de tijdelijke lijst

Maak een VentilatorController die luistert op <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/ventilator</code>, met een lijst van minstens vier ventilatoren.

> [!WARNING]
> Zorg ervoor dat een ventilator die je toevoegt, er bij een volgend verzoek nog steeds staat. Test dit: voeg er een toe via POST en vraag daarna de volledige lijst op. Staat je nieuwe ventilator er niet meer bij, dan is er iets mis met de manier waarop je de lijst hebt gedeclareerd.

---

### 3. Alles ophalen en één ophalen

Voorzie een GET-endpoint op <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/ventilator</code> dat de volledige lijst teruggeeft met <code style="color:#37b24d;font-weight:600">200 OK</code>.

Voorzie een GET-endpoint op <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/ventilator/<span style="color:#e64980"><b>{id}</b></span></code> dat één ventilator teruggeeft met <code style="color:#37b24d;font-weight:600">200 OK</code>, of <code style="color:#f08c00;font-weight:600">404 Not Found</code> met het bericht:

<code style="color:#845ef7">We vonden geen ventilator met Id <span style="color:#e64980"><b>{id}</b></span>.</code>

---

### 4. Een ventilator toevoegen

Voorzie een POST-endpoint op:

<span style="background:#2f9e44;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>POST</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/ventilator</code>

De gegevens van de nieuwe ventilator komen in de body van het verzoek. Het Id wordt door de API zelf bepaald. Stuurt de client toch een Id mee, dan wordt dat overschreven.

Slaagt de toevoeging, geef dan statuscode <code style="color:#37b24d;font-weight:600">201 Created</code> terug, samen met de nieuwe ventilator én het adres waarop hij opgevraagd kan worden.

Weiger de toevoeging met statuscode <code style="color:#f08c00;font-weight:600">400 Bad Request</code> wanneer:

- het merk leeg is: <code style="color:#845ef7">Een ventilator moet een merk hebben.</code>
- de afmeting niet 120 of 140 is: <code style="color:#845ef7">We verkopen enkel ventilatoren van 120 of 140 mm.</code>
- de prijs 0 of lager is: <code style="color:#845ef7">De prijs moet groter zijn dan 0.</code>

---

### 5. Een ventilator bijwerken

Voorzie een PUT-endpoint op:

<span style="background:#d9822b;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>PUT</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/ventilator/<span style="color:#e64980"><b>{id}</b></span></code>

Het Id staat in de URL, de nieuwe gegevens staan in de body.

Bestaat de ventilator niet, geef dan <code style="color:#f08c00;font-weight:600">404 Not Found</code> met het bericht:

<code style="color:#845ef7">Kan geen ventilator bijwerken met Id <span style="color:#e64980"><b>{id}</b></span>, omdat deze niet bestaat.</code>

Dezelfde drie controles uit punt 4 gelden ook hier. Zijn de gegevens ongeldig, geef dan <code style="color:#f08c00;font-weight:600">400 Bad Request</code>.

Slaagt de wijziging, geef dan <code style="color:#37b24d;font-weight:600">204 No Content</code> terug.

---

### 6. Een ventilator verwijderen

Voorzie een DELETE-endpoint op:

<span style="background:#e03131;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>DELETE</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/ventilator/<span style="color:#e64980"><b>{id}</b></span></code>

Bestaat de ventilator niet, geef dan <code style="color:#f08c00;font-weight:600">404 Not Found</code> met het bericht:

<code style="color:#845ef7">Kan ventilator met Id <span style="color:#e64980"><b>{id}</b></span> niet verwijderen, omdat deze niet gevonden is.</code>

Slaagt de verwijdering, geef dan <code style="color:#37b24d;font-weight:600">204 No Content</code> terug.

---

### 7. Testen

Test elk endpoint via Scalar of Postman en noteer welke statuscode je terugkrijgt. Controleer in het bijzonder:

- of een POST met een afmeting van 92 mm effectief <code style="color:#f08c00;font-weight:600">400</code> geeft en niet in de lijst belandt;
- of een DELETE op een Id dat je net verwijderd hebt, de tweede keer <code style="color:#f08c00;font-weight:600">404</code> geeft.

---

### 8. Alles samen: een ventilator vervangen

<span style="background:#7048e8;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>SAMENGESTELDE OEFENING</b></span>

De inkoopafdeling doet vaak hetzelfde: een model uit het assortiment halen en er meteen een nieuw model voor in de plaats zetten. Vandaag zijn dat twee verzoeken, en tussen die twee in staat het assortiment even verkeerd.

In dit laatste endpoint doe je het in één keer.

Voorzie een PUT-endpoint op:

<span style="background:#d9822b;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>PUT</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/ventilator/vervang/<span style="color:#e64980"><b>{id}</b></span></code>

Het Id in de URL is de ventilator die uit het assortiment gaat. De gegevens van de nieuwe ventilator staan in de body. Het Id van de nieuwe ventilator bepaalt de API zelf.

Het endpoint moet:

- <code style="color:#f08c00;font-weight:600">404 Not Found</code> geven wanneer de te vervangen ventilator niet bestaat, met het bericht <code style="color:#845ef7">Kan ventilator met Id <span style="color:#e64980"><b>{id}</b></span> niet vervangen, omdat deze niet gevonden is.</code>
- <code style="color:#f08c00;font-weight:600">400 Bad Request</code> geven wanneer de nieuwe ventilator niet door de drie controles uit punt 4 geraakt;
- bij succes de oude ventilator verwijderen, de nieuwe toevoegen, en <code style="color:#37b24d;font-weight:600">201 Created</code> teruggeven met de nieuwe ventilator en het adres waarop hij opgevraagd kan worden.

> [!IMPORTANT]
> Er is één regel die je nergens in dit bestand eerder bent tegengekomen: **mislukt de vervanging, dan mag er niets veranderd zijn.** Een klant die een ongeldige nieuwe ventilator doorstuurt, mag zijn oude niet kwijt zijn.

> [!WARNING]
> Dat maakt de volgorde van je stappen belangrijk. Verwijder je eerst en controleer je daarna, dan is de oude ventilator al weg op het moment dat je <code style="color:#f08c00;font-weight:600">400</code> teruggeeft.

Test dit expliciet:

1. Vraag de volledige lijst op en onthoud welke ventilatoren erin staan.
2. Stuur een vervanging voor een bestaande ventilator, met een afmeting van 92 mm in de body.
3. Controleer dat je <code style="color:#f08c00;font-weight:600">400</code> krijgt.
4. Vraag de volledige lijst opnieuw op. De oude ventilator moet er nog steeds in staan.

---

# 02_03

<b>Hoofdstuk 02</b> &nbsp;·&nbsp; Soft delete en berekeningen &nbsp;·&nbsp; PC-onderdelen &nbsp;·&nbsp; niveau <span style="color:#f03e3e">●●●</span> &nbsp;·&nbsp; ±2 uur

## Leerdoel

Na deze oefening kan je een API bouwen waarin verwijderen niet betekent dat gegevens verdwijnen.

Je leert een soft delete toepassen: het item krijgt een label dat zegt dat het niet meer actief is, en je API filtert het weg zodat de client het niet meer ziet. De gegevens zelf blijven bewaard.

Daarnaast leer je dat dezelfde lijst verschillende antwoorden kan geven, afhankelijk van het endpoint, en dat je statuscodes moet kiezen op basis van wat de client mag weten.

## 

## Opdracht

Bit & Byte stelt bestellingen samen uit losse lijnen. Een medewerker moet een lijn kunnen schrappen, maar de boekhouding wil nadien nog kunnen zien wat er ooit op de bestelling stond.

Jouw taak is om een Model en een BestellijnController te maken.

Via de API moet een medewerker:

1. de actieve bestellijnen kunnen opvragen;
2. alle bestellijnen kunnen opvragen, ook de geschrapte;
3. één bestellijn kunnen opvragen;
4. een bestellijn kunnen toevoegen, bijwerken en schrappen;
5. een geschrapte bestellijn kunnen herstellen;
6. de totale waarde van de bestelling kunnen opvragen.

Implementeer onderstaande functionaliteiten.

### 1. Het model

Maak in de map Models een klasse `Bestellijn` met volgende gegevens:

| Gegeven      | Soort waarde |
|--------------|--------------|
| Id           | geheel getal |
| Omschrijving | tekst        |
| Aantal       | geheel getal |
| StukPrijs    | kommagetal   |
| IsActief     | ja of nee    |

---

### 2. De controller en de tijdelijke lijst

Maak een BestellijnController die luistert op <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/bestellijn</code>, met een lijst van minstens vijf bestellijnen. Zorg dat er minstens één lijn bij zit die al geschrapt is.

---

### 3. De actieve bestellijnen

Voorzie een GET-endpoint op:

<span style="background:#1c7ed6;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>GET</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/bestellijn</code>

Dit endpoint geeft enkel de actieve bestellijnen terug, met statuscode <code style="color:#37b24d;font-weight:600">200 OK</code>. Geschrapte lijnen mag de client hier niet zien.

---

### 4. Alle bestellijnen

Voorzie een GET-endpoint op:

<span style="background:#1c7ed6;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>GET</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/bestellijn/alles</code>

Dit endpoint geeft alle bestellijnen terug, geschrapte inbegrepen, met statuscode <code style="color:#37b24d;font-weight:600">200 OK</code>. Dit is het endpoint voor de boekhouding.

---

### 5. Eén bestellijn

Voorzie een GET-endpoint op:

<span style="background:#1c7ed6;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>GET</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/bestellijn/<span style="color:#e64980"><b>{id}</b></span></code>

Bestaat de lijn en is ze actief, geef ze dan terug met <code style="color:#37b24d;font-weight:600">200 OK</code>.

Bestaat de lijn niet, of is ze geschrapt, geef dan <code style="color:#f08c00;font-weight:600">404 Not Found</code> met het bericht:

<code style="color:#845ef7">We vonden geen actieve bestellijn met Id <span style="color:#e64980"><b>{id}</b></span>.</code>

> [!NOTE]
> Een geschrapte lijn bestaat nog in je lijst, maar voor deze client bestaat ze niet. Dat is precies wat een soft delete betekent.

---

### 6. Een bestellijn toevoegen

Voorzie een POST-endpoint op:

<span style="background:#2f9e44;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>POST</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/bestellijn</code>

Het Id bepaalt de API zelf. Stuurt de client toch een Id mee, dan wordt dat overschreven. Een nieuwe lijn is altijd actief, ook als de client iets anders meestuurt.

Slaagt de toevoeging, geef dan <code style="color:#37b24d;font-weight:600">201 Created</code> terug, samen met de nieuwe lijn en het adres waarop ze opgevraagd kan worden.

Weiger de toevoeging met <code style="color:#f08c00;font-weight:600">400 Bad Request</code> wanneer:

- de omschrijving leeg is: <code style="color:#845ef7">Een bestellijn moet een omschrijving hebben.</code>
- het aantal kleiner is dan 1: <code style="color:#845ef7">Het aantal moet minstens 1 zijn.</code>
- de stukprijs 0 of lager is: <code style="color:#845ef7">De stukprijs moet groter zijn dan 0.</code>

---

### 7. Een bestellijn bijwerken

Voorzie een PUT-endpoint op:

<span style="background:#d9822b;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>PUT</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/bestellijn/<span style="color:#e64980"><b>{id}</b></span></code>

Werkt enkel op actieve lijnen. Bestaat de lijn niet of is ze geschrapt, geef dan <code style="color:#f08c00;font-weight:600">404 Not Found</code>.

Dezelfde drie controles uit punt 6 gelden ook hier.

Slaagt de wijziging, geef dan <code style="color:#37b24d;font-weight:600">204 No Content</code> terug.

---

### 8. Een bestellijn schrappen

Voorzie een DELETE-endpoint op:

<span style="background:#e03131;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>DELETE</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/bestellijn/<span style="color:#e64980"><b>{id}</b></span></code>

Dit endpoint verwijdert de lijn niet uit je lijst. Het zet ze enkel op non-actief.

Slaagt het schrappen, geef dan <code style="color:#37b24d;font-weight:600">204 No Content</code> terug.

Bestaat de lijn niet, of is ze al geschrapt, geef dan <code style="color:#f08c00;font-weight:600">404 Not Found</code> met het bericht:

<code style="color:#845ef7">Kan bestellijn met Id <span style="color:#e64980"><b>{id}</b></span> niet schrappen, omdat deze niet gevonden is.</code>

> [!TIP]
> Roep dit endpoint twee keer na elkaar op met hetzelfde Id. De eerste keer krijg je <code style="color:#37b24d;font-weight:600">204</code>, de tweede keer <code style="color:#f08c00;font-weight:600">404</code>.

---

### 9. Een bestellijn herstellen

Voorzie een PUT-endpoint op:

<span style="background:#d9822b;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>PUT</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/bestellijn/herstel/<span style="color:#e64980"><b>{id}</b></span></code>

Dit endpoint zet een geschrapte lijn terug op actief.

- De lijn bestaat niet: <code style="color:#f08c00;font-weight:600">404 Not Found</code>
- De lijn is al actief: <code style="color:#f08c00;font-weight:600">400 Bad Request</code> met het bericht <code style="color:#845ef7">Deze bestellijn is niet geschrapt.</code>
- Het herstel is gelukt: <code style="color:#37b24d;font-weight:600">204 No Content</code>

---

### 10. De totale waarde

Voorzie een GET-endpoint op:

<span style="background:#1c7ed6;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>GET</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/bestellijn/totaal</code>

Dit endpoint geeft de totale waarde van de bestelling terug met <code style="color:#37b24d;font-weight:600">200 OK</code>. Per actieve lijn vermenigvuldig je het aantal met de stukprijs, en die bedragen tel je op.

Geschrapte lijnen tellen niet mee.

Schrap een lijn via punt 8 en vraag daarna dit endpoint opnieuw op. Het totaal moet gedaald zijn.

---

### 11. Alles samen: de bestelfiche en de bestelling schrappen

<span style="background:#7048e8;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>SAMENGESTELDE OEFENING</b></span>

De tien vorige punten bekijken de bestelling telkens door één venster: de actieve lijnen, of alle lijnen, of het totaal. Wie wil weten hoe de bestelling ervoor staat, moet drie endpoints oproepen en de antwoorden zelf bij elkaar leggen.

In dit laatste punt maak je één venster waar alles in past, en één actie die de hele bestelling in één keer schrapt.

#### De fiche

Maak in de map Models een tweede klasse `BestelFiche` met volgende gegevens:

| Gegeven               | Soort waarde         |
|-----------------------|----------------------|
| AantalActieveLijnen   | geheel getal         |
| AantalGeschrapteLijnen| geheel getal         |
| TotaalActief          | kommagetal           |
| TotaalGeschrapt       | kommagetal           |
| DuursteActieveLijn    | een bestellijn       |
| IsLeeg                | ja of nee            |

`TotaalActief` is de berekening uit punt 10. `TotaalGeschrapt` is diezelfde berekening, maar dan over de geschrapte lijnen.

`DuursteActieveLijn` is de actieve lijn met de hoogste waarde, dus aantal maal stukprijs. Zijn er geen actieve lijnen, dan blijft dit leeg.

`IsLeeg` staat op ja zodra er geen enkele actieve lijn meer is.

Voorzie een GET-endpoint op:

<span style="background:#1c7ed6;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>GET</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/bestellijn/fiche</code>

Dit endpoint geeft de fiche terug met statuscode <code style="color:#37b24d;font-weight:600">200 OK</code>.

#### De hele bestelling schrappen

Voorzie een DELETE-endpoint op:

<span style="background:#e03131;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>DELETE</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/bestellijn</code>

Let op het ontbrekende Id: dit endpoint werkt op de hele bestelling.

Het zet alle actieve lijnen op non-actief. Net als in punt 8 verdwijnt er niets uit je lijst.

- Waren er actieve lijnen, geef dan <code style="color:#37b24d;font-weight:600">200 OK</code> terug met de bijgewerkte fiche. Hier gebruik je dus geen <code style="color:#37b24d;font-weight:600">204</code>, want er is wél iets te tonen.
- Waren er geen actieve lijnen meer, geef dan <code style="color:#f08c00;font-weight:600">400 Bad Request</code> met het bericht <code style="color:#845ef7">Er zijn geen actieve bestellijnen om te schrappen.</code>

#### Testscenario

Doorloop deze reeks en controleer na elke stap de fiche:

1. Vraag de fiche op. Noteer `AantalActieveLijnen` en `TotaalActief`.
2. Voeg een lijn toe via punt 6. Het aantal actieve lijnen stijgt met één, het totaal stijgt met aantal maal stukprijs.
3. Schrap die lijn via punt 8. Het aantal actieve lijnen daalt weer, `AantalGeschrapteLijnen` stijgt, en `TotaalGeschrapt` stijgt met hetzelfde bedrag als waarmee `TotaalActief` daalde.
4. Herstel die lijn via punt 9. Alles staat weer zoals na stap 2.
5. Schrap de hele bestelling via het nieuwe DELETE-endpoint. `IsLeeg` staat op ja, `TotaalActief` op 0, en `DuursteActieveLijn` is leeg.
6. Roep het DELETE-endpoint nog eens op. Je krijgt <code style="color:#f08c00;font-weight:600">400</code>.

> [!WARNING]
> Verspringt er in stap 3 iets dat niet klopt, dan zit de fout niet in je fiche maar in je soft delete. De fiche is namelijk niets meer dan een spiegel van je lijst.
> 
---

# 02_04

<b>Hoofdstuk 02</b> &nbsp;·&nbsp; Model, lijst en statuscodes &nbsp;·&nbsp; Dierenasiel &nbsp;·&nbsp; niveau <span style="color:#37b24d">●○○</span> &nbsp;·&nbsp; ±60 min

## Leerdoel

Na deze oefening kan je een Model opstellen dat een object uit de echte wereld beschrijft, en het vullen met een tijdelijke lijst in het geheugen.

Je leert die gegevens via GET-endpoints beschikbaar stellen en je antwoord verpakken in `ActionResult<T>`, zodat je naast de data ook een HTTP-statuscode kan meesturen.

Daarnaast leer je wanneer een leeg resultaat een fout is (404) en wanneer het gewoon een geldig antwoord is (200). Dat is een keuze die je per endpoint bewust maakt.

## 

## Opdracht

Dierenasiel Knuffelhof wil zijn bewoners via een API tonen, zodat de website en de balie dezelfde gegevens gebruiken.

Jouw taak is om een Model en een DierController te maken.

Via de API moet de website:

1. alle dieren kunnen opvragen;
2. één dier kunnen opvragen op basis van zijn Id;
3. alle dieren van één soort kunnen opvragen;
4. enkel de jonge dieren kunnen opvragen.

Implementeer onderstaande functionaliteiten.

### 1. Het model

Maak in de map Models een klasse `Dier` met volgende gegevens:

| Gegeven          | Soort waarde |
|------------------|--------------|
| Id               | geheel getal |
| Naam             | tekst        |
| Soort            | tekst        |
| LeeftijdInJaren  | geheel getal |
| Verblijfsnummer  | geheel getal |

---

### 2. De controller en de tijdelijke lijst

Maak een DierController die luistert op <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/dier</code>.

Voorzie in de controller een lijst met minstens zes dieren. Zorg dat er minstens drie soorten in voorkomen, en dat er minstens twee dieren jonger zijn dan twee jaar.

---

### 3. Alle dieren opvragen

Voorzie een GET-endpoint op:

<span style="background:#1c7ed6;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>GET</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/dier</code>

Dit endpoint geeft de volledige lijst terug met statuscode <code style="color:#37b24d;font-weight:600">200 OK</code>.

---

### 4. Eén dier opvragen

Voorzie een GET-endpoint op:

<span style="background:#1c7ed6;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>GET</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/dier/<span style="color:#e64980"><b>{id}</b></span></code>

Bestaat het dier, geef het dan terug met statuscode <code style="color:#37b24d;font-weight:600">200 OK</code>.

Bestaat het dier niet, geef dan statuscode <code style="color:#f08c00;font-weight:600">404 Not Found</code> met het bericht:

<code style="color:#845ef7">We vonden geen dier met Id <span style="color:#e64980"><b>{id}</b></span>.</code>

---

### 5. Dieren van één soort opvragen

Voorzie een GET-endpoint op:

<span style="background:#1c7ed6;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>GET</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/dier/soort/<span style="color:#e64980"><b>{soort}</b></span></code>

Dit endpoint geeft alle dieren van die soort terug met statuscode <code style="color:#37b24d;font-weight:600">200 OK</code>.

Zijn er geen dieren van die soort, geef dan statuscode <code style="color:#f08c00;font-weight:600">404 Not Found</code> met het bericht:

<code style="color:#845ef7">We hebben op dit moment geen <span style="color:#e64980"><b>{soort}</b></span> in het asiel.</code>

> [!WARNING]
> Let op: deze route en de route uit punt 4 lijken op elkaar. Zorg dat een verzoek naar <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/dier/soort/kat</code> niet per ongeluk bij het endpoint van punt 4 terechtkomt.

---

### 6. Enkel de jonge dieren

Voorzie een GET-endpoint op:

<span style="background:#1c7ed6;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>GET</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/dier/jong</code>

Dit endpoint geeft enkel de dieren terug die jonger zijn dan twee jaar.

Zijn er geen jonge dieren, geef dan een lege lijst terug met statuscode <code style="color:#37b24d;font-weight:600">200 OK</code>, en dus géén 404.

> [!NOTE]
> Een leeg resultaat is hier namelijk geen fout: de vraag was geldig en het antwoord is "geen enkel". Dat is een ander geval dan een dier of een soort die niet bestaat.

---

### 7. Alles samen: de dierfiche

<span style="background:#7048e8;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>SAMENGESTELDE OEFENING</b></span>

De vier vorige endpoints beantwoorden elk één vraag over het asiel. Een bezoeker die alles over één dier wil weten, moet ze nu alle vier oproepen en de antwoorden zelf bij elkaar leggen.

In dit laatste endpoint doe je dat werk voor hem: één dier, één antwoord, met alles erin.

Maak in de map Models een tweede klasse `DierFiche` met volgende gegevens:

| Gegeven                | Soort waarde       |
|------------------------|--------------------|
| Dier                   | het dier zelf      |
| IsJong                 | ja of nee          |
| AantalVanDezelfdeSoort | geheel getal       |
| IsOudsteVanDeSoort     | ja of nee          |

`IsJong` volgt dezelfde regel als punt 6: jonger dan twee jaar.

`AantalVanDezelfdeSoort` telt de andere dieren van dezelfde soort. Het dier zelf telt niet mee.

`IsOudsteVanDeSoort` zegt of geen enkel ander dier van die soort ouder is.

Voorzie een GET-endpoint op:

<span style="background:#1c7ed6;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>GET</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/dier/fiche/<span style="color:#e64980"><b>{id}</b></span></code>

Bestaat het dier, geef dan de fiche terug met statuscode <code style="color:#37b24d;font-weight:600">200 OK</code>.

Bestaat het dier niet, geef dan statuscode <code style="color:#f08c00;font-weight:600">404 Not Found</code> met het bericht:

<code style="color:#845ef7">We vonden geen dier met Id <span style="color:#e64980"><b>{id}</b></span>.</code>

Test je endpoint en controleer:

- of `AantalVanDezelfdeSoort` op 0 staat bij een dier dat als enige van zijn soort in het asiel zit;
- of `IsJong` op ja staat bij de dieren die je jonger dan twee jaar hebt gemaakt;
- of er bij twee dieren van dezelfde soort maar bij één van de twee `IsOudsteVanDeSoort` op ja staat.

> [!NOTE]
> Dit is de eerste keer dat je een klasse maakt die geen object uit de echte wereld beschrijft. Een dier bestaat, een dierfiche niet: die bestaat alleen omdat een client hem handig vindt. Onthoud dat verschil, het komt later in de cursus terug.
> 
---

# 02_05

<b>Hoofdstuk 02</b> &nbsp;·&nbsp; Volledige CRUD &nbsp;·&nbsp; Fietsverhuur &nbsp;·&nbsp; niveau <span style="color:#f08c00">●●○</span> &nbsp;·&nbsp; ±90 min

## Leerdoel

Na deze oefening kan je de volledige CRUD-cyclus op een Model uitvoeren via HTTP: ophalen, toevoegen, bijwerken en verwijderen.

Je leert data ontvangen in de body van een verzoek in plaats van in de URL, en je leert per actie de juiste statuscode terugsturen: <code style="color:#37b24d;font-weight:600">201 Created</code> bij een POST, <code style="color:#37b24d;font-weight:600">204 No Content</code> bij een PUT en een DELETE.

Daarnaast leer je foutieve invoer afwijzen met <code style="color:#f08c00;font-weight:600">400 Bad Request</code>, zodat er geen onmogelijke gegevens in je lijst terechtkomen.

## 

## Opdracht

Fietsverhuur Trapdoor beheert zijn vloot nu nog in een rekenblad. Ze willen dat vervangen door een API, zodat de balie en de website met dezelfde gegevens werken.

Jouw taak is om een Model en een FietsController te maken met een volledige CRUD.

Via de API moet de balie:

1. alle fietsen kunnen opvragen;
2. één fiets kunnen opvragen;
3. een nieuwe fiets kunnen toevoegen;
4. een bestaande fiets kunnen bijwerken;
5. een fiets uit de vloot kunnen halen.

Implementeer onderstaande functionaliteiten.

### 1. Het model

Maak in de map Models een klasse `Fiets` met volgende gegevens:

| Gegeven        | Soort waarde |
|----------------|--------------|
| Id             | geheel getal |
| Type           | tekst        |
| Framemaat      | geheel getal |
| PrijsPerDag    | kommagetal   |
| IsBeschikbaar  | ja of nee    |

---

### 2. De controller en de tijdelijke lijst

Maak een FietsController die luistert op <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/fiets</code>, met een lijst van minstens vier fietsen.

> [!WARNING]
> Zorg ervoor dat een fiets die je toevoegt, er bij een volgend verzoek nog steeds staat. Test dit: voeg er een toe via POST en vraag daarna de volledige lijst op. Staat je nieuwe fiets er niet meer bij, dan is er iets mis met de manier waarop je de lijst hebt gedeclareerd.

---

### 3. Alles ophalen en één ophalen

Voorzie een GET-endpoint op <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/fiets</code> dat de volledige lijst teruggeeft met <code style="color:#37b24d;font-weight:600">200 OK</code>.

Voorzie een GET-endpoint op <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/fiets/<span style="color:#e64980"><b>{id}</b></span></code> dat één fiets teruggeeft met <code style="color:#37b24d;font-weight:600">200 OK</code>, of <code style="color:#f08c00;font-weight:600">404 Not Found</code> met het bericht:

<code style="color:#845ef7">We vonden geen fiets met Id <span style="color:#e64980"><b>{id}</b></span>.</code>

---

### 4. Een fiets toevoegen

Voorzie een POST-endpoint op:

<span style="background:#2f9e44;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>POST</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/fiets</code>

De gegevens van de nieuwe fiets komen in de body van het verzoek. Het Id wordt door de API zelf bepaald. Stuurt de client toch een Id mee, dan wordt dat overschreven.

Slaagt de toevoeging, geef dan statuscode <code style="color:#37b24d;font-weight:600">201 Created</code> terug, samen met de nieuwe fiets én het adres waarop ze opgevraagd kan worden.

Weiger de toevoeging met statuscode <code style="color:#f08c00;font-weight:600">400 Bad Request</code> wanneer:

- het type leeg is: <code style="color:#845ef7">Een fiets moet een type hebben.</code>
- de framemaat kleiner is dan 44 of groter dan 62: <code style="color:#845ef7">De framemaat moet tussen 44 en 62 liggen.</code>
- de prijs per dag 0 of lager is: <code style="color:#845ef7">De prijs per dag moet groter zijn dan 0.</code>

---

### 5. Een fiets bijwerken

Voorzie een PUT-endpoint op:

<span style="background:#d9822b;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>PUT</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/fiets/<span style="color:#e64980"><b>{id}</b></span></code>

Het Id staat in de URL, de nieuwe gegevens staan in de body.

Bestaat de fiets niet, geef dan <code style="color:#f08c00;font-weight:600">404 Not Found</code> met het bericht:

<code style="color:#845ef7">Kan geen fiets bijwerken met Id <span style="color:#e64980"><b>{id}</b></span>, omdat deze niet bestaat.</code>

Dezelfde drie controles uit punt 4 gelden ook hier. Zijn de gegevens ongeldig, geef dan <code style="color:#f08c00;font-weight:600">400 Bad Request</code>.

Slaagt de wijziging, geef dan <code style="color:#37b24d;font-weight:600">204 No Content</code> terug.

---

### 6. Een fiets verwijderen

Voorzie een DELETE-endpoint op:

<span style="background:#e03131;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>DELETE</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/fiets/<span style="color:#e64980"><b>{id}</b></span></code>

Bestaat de fiets niet, geef dan <code style="color:#f08c00;font-weight:600">404 Not Found</code> met het bericht:

<code style="color:#845ef7">Kan fiets met Id <span style="color:#e64980"><b>{id}</b></span> niet verwijderen, omdat deze niet gevonden is.</code>

Slaagt de verwijdering, geef dan <code style="color:#37b24d;font-weight:600">204 No Content</code> terug.

---

### 7. Testen

Test elk endpoint via Scalar of Postman en noteer welke statuscode je terugkrijgt. Controleer in het bijzonder:

- of een POST met framemaat 70 effectief <code style="color:#f08c00;font-weight:600">400</code> geeft en niet in de lijst belandt;
- of een DELETE op een Id dat je net verwijderd hebt, de tweede keer <code style="color:#f08c00;font-weight:600">404</code> geeft.

---

### 8. Alles samen: een fiets vervangen

<span style="background:#7048e8;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>SAMENGESTELDE OEFENING</b></span>

Trapdoor vervangt geregeld een versleten fiets door een nieuwe van hetzelfde type. Vandaag zijn dat twee verzoeken, en tussen die twee in klopt de vloot even niet.

In dit laatste endpoint doe je het in één keer.

Voorzie een PUT-endpoint op:

<span style="background:#d9822b;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>PUT</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/fiets/vervang/<span style="color:#e64980"><b>{id}</b></span></code>

Het Id in de URL is de fiets die uit de vloot gaat. De gegevens van de nieuwe fiets staan in de body. Het Id van de nieuwe fiets bepaalt de API zelf.

Het endpoint moet:

- <code style="color:#f08c00;font-weight:600">404 Not Found</code> geven wanneer de te vervangen fiets niet bestaat, met het bericht <code style="color:#845ef7">Kan fiets met Id <span style="color:#e64980"><b>{id}</b></span> niet vervangen, omdat deze niet gevonden is.</code>
- <code style="color:#f08c00;font-weight:600">400 Bad Request</code> geven wanneer de nieuwe fiets niet door de drie controles uit punt 4 geraakt;
- bij succes de oude fiets verwijderen, de nieuwe toevoegen, en <code style="color:#37b24d;font-weight:600">201 Created</code> teruggeven met de nieuwe fiets en het adres waarop ze opgevraagd kan worden.

> [!IMPORTANT]
> Er is één regel die je nergens in dit bestand eerder bent tegengekomen: **mislukt de vervanging, dan mag er niets veranderd zijn.** Wie een ongeldige nieuwe fiets doorstuurt, mag zijn oude niet kwijt zijn.

> [!WARNING]
> Dat maakt de volgorde van je stappen belangrijk. Verwijder je eerst en controleer je daarna, dan is de oude fiets al weg op het moment dat je <code style="color:#f08c00;font-weight:600">400</code> teruggeeft.

Test dit expliciet:

1. Vraag de volledige lijst op en onthoud welke fietsen erin staan.
2. Stuur een vervanging voor een bestaande fiets, met framemaat 70 in de body.
3. Controleer dat je <code style="color:#f08c00;font-weight:600">400</code> krijgt.
4. Vraag de volledige lijst opnieuw op. De oude fiets moet er nog steeds in staan.

---

# 02_06

<b>Hoofdstuk 02</b> &nbsp;·&nbsp; Soft delete en plaatscontrole &nbsp;·&nbsp; Concertzaal &nbsp;·&nbsp; niveau <span style="color:#f03e3e">●●●</span> &nbsp;·&nbsp; ±2 uur

## Leerdoel

Na deze oefening kan je een API bouwen waarin annuleren niet betekent dat gegevens verdwijnen.

Je leert een soft delete toepassen: het item krijgt een label dat zegt dat het geannuleerd is, en je API filtert het weg zodat de client het niet meer ziet. De gegevens zelf blijven bewaard.

Daarnaast leer je controles schrijven die niet alleen naar het binnenkomende object kijken, maar ook naar wat er al in je lijst staat. Twee bezoekers op dezelfde stoel is iets wat je API zelf moet tegenhouden.

## 

## Opdracht

Concertzaal De Notenbalk verkoopt genummerde plaatsen. Een medewerker moet een ticket kunnen annuleren, maar de boekhouding wil nadien nog kunnen zien wat er verkocht is geweest.

Jouw taak is om een Model en een TicketController te maken.

Via de API moet een medewerker:

1. de geldige tickets kunnen opvragen;
2. alle tickets kunnen opvragen, ook de geannuleerde;
3. één ticket kunnen opvragen;
4. een ticket kunnen verkopen, wijzigen en annuleren;
5. een geannuleerd ticket kunnen herstellen;
6. de opbrengst van de avond kunnen opvragen.

Implementeer onderstaande functionaliteiten.

### 1. Het model

Maak in de map Models een klasse `Ticket` met volgende gegevens:

| Gegeven        | Soort waarde |
|----------------|--------------|
| Id             | geheel getal |
| Naam           | tekst        |
| Rij            | geheel getal |
| Stoel          | geheel getal |
| Prijs          | kommagetal   |
| IsGeannuleerd  | ja of nee    |

---

### 2. De controller en de tijdelijke lijst

Maak een TicketController die luistert op <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/ticket</code>, met een lijst van minstens vijf tickets. Zorg dat er minstens één geannuleerd ticket bij zit.

---

### 3. De geldige tickets

Voorzie een GET-endpoint op:

<span style="background:#1c7ed6;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>GET</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/ticket</code>

Dit endpoint geeft enkel de niet-geannuleerde tickets terug, met statuscode <code style="color:#37b24d;font-weight:600">200 OK</code>.

---

### 4. Alle tickets

Voorzie een GET-endpoint op:

<span style="background:#1c7ed6;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>GET</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/ticket/alles</code>

Dit endpoint geeft alle tickets terug, geannuleerde inbegrepen, met statuscode <code style="color:#37b24d;font-weight:600">200 OK</code>. Dit is het endpoint voor de boekhouding.

---

### 5. Eén ticket

Voorzie een GET-endpoint op:

<span style="background:#1c7ed6;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>GET</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/ticket/<span style="color:#e64980"><b>{id}</b></span></code>

Bestaat het ticket en is het niet geannuleerd, geef het dan terug met <code style="color:#37b24d;font-weight:600">200 OK</code>.

Bestaat het ticket niet, of is het geannuleerd, geef dan <code style="color:#f08c00;font-weight:600">404 Not Found</code> met het bericht:

<code style="color:#845ef7">We vonden geen geldig ticket met Id <span style="color:#e64980"><b>{id}</b></span>.</code>

> [!NOTE]
> Een geannuleerd ticket bestaat nog in je lijst, maar voor deze client bestaat het niet. Dat is precies wat een soft delete betekent.

---

### 6. Een ticket verkopen

Voorzie een POST-endpoint op:

<span style="background:#2f9e44;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>POST</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/ticket</code>

Het Id bepaalt de API zelf. Stuurt de client toch een Id mee, dan wordt dat overschreven. Een nieuw ticket is nooit geannuleerd, ook als de client iets anders meestuurt.

Slaagt de verkoop, geef dan <code style="color:#37b24d;font-weight:600">201 Created</code> terug, samen met het nieuwe ticket en het adres waarop het opgevraagd kan worden.

Weiger de verkoop met <code style="color:#f08c00;font-weight:600">400 Bad Request</code> wanneer:

- de naam leeg is: <code style="color:#845ef7">Een ticket moet op naam staan.</code>
- de rij kleiner is dan 1: <code style="color:#845ef7">De rij moet minstens 1 zijn.</code>
- de stoel kleiner is dan 1: <code style="color:#845ef7">De stoel moet minstens 1 zijn.</code>
- de prijs 0 of lager is: <code style="color:#845ef7">De prijs moet groter zijn dan 0.</code>
- die rij en stoel al bezet zijn door een niet-geannuleerd ticket: <code style="color:#845ef7">Rij <span style="color:#e64980"><b>{rij}</b></span> stoel <span style="color:#e64980"><b>{stoel}</b></span> is al verkocht.</code>

> [!TIP]
> Die laatste controle kan je niet doen door enkel naar het binnenkomende ticket te kijken. Je moet je bestaande lijst doorzoeken.

---

### 7. Een ticket wijzigen

Voorzie een PUT-endpoint op:

<span style="background:#d9822b;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>PUT</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/ticket/<span style="color:#e64980"><b>{id}</b></span></code>

Werkt enkel op niet-geannuleerde tickets. Bestaat het ticket niet of is het geannuleerd, geef dan <code style="color:#f08c00;font-weight:600">404 Not Found</code>.

Dezelfde controles uit punt 6 gelden ook hier, met één verschil: een ticket mag zijn eigen plaats houden. Verhuis je ticket 3 van rij 2 stoel 5 naar rij 2 stoel 5, dan is dat geen dubbele verkoop.

Slaagt de wijziging, geef dan <code style="color:#37b24d;font-weight:600">204 No Content</code> terug.

---

### 8. Een ticket annuleren

Voorzie een DELETE-endpoint op:

<span style="background:#e03131;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>DELETE</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/ticket/<span style="color:#e64980"><b>{id}</b></span></code>

Dit endpoint verwijdert het ticket niet uit je lijst. Het zet het enkel op geannuleerd, waardoor de plaats weer vrijkomt.

Slaagt de annulering, geef dan <code style="color:#37b24d;font-weight:600">204 No Content</code> terug.

Bestaat het ticket niet, of is het al geannuleerd, geef dan <code style="color:#f08c00;font-weight:600">404 Not Found</code> met het bericht:

<code style="color:#845ef7">Kan ticket met Id <span style="color:#e64980"><b>{id}</b></span> niet annuleren, omdat het niet gevonden is.</code>

> [!TIP]
> Roep dit endpoint twee keer na elkaar op met hetzelfde Id. De eerste keer krijg je <code style="color:#37b24d;font-weight:600">204</code>, de tweede keer <code style="color:#f08c00;font-weight:600">404</code>.

---

### 9. Een ticket herstellen

Voorzie een PUT-endpoint op:

<span style="background:#d9822b;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>PUT</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/ticket/herstel/<span style="color:#e64980"><b>{id}</b></span></code>

Dit endpoint maakt een geannuleerd ticket weer geldig.

- Het ticket bestaat niet: <code style="color:#f08c00;font-weight:600">404 Not Found</code>
- Het ticket is niet geannuleerd: <code style="color:#f08c00;font-weight:600">400 Bad Request</code> met het bericht <code style="color:#845ef7">Dit ticket is niet geannuleerd.</code>
- De plaats is intussen aan iemand anders verkocht: <code style="color:#f08c00;font-weight:600">400 Bad Request</code> met het bericht <code style="color:#845ef7">Rij <span style="color:#e64980"><b>{rij}</b></span> stoel <span style="color:#e64980"><b>{stoel}</b></span> is intussen aan iemand anders verkocht.</code>
- Het herstel is gelukt: <code style="color:#37b24d;font-weight:600">204 No Content</code>

Test dit scenario volledig: annuleer een ticket, verkoop diezelfde plaats aan iemand anders, en probeer daarna het eerste ticket te herstellen.

---

### 10. De opbrengst

Voorzie een GET-endpoint op:

<span style="background:#1c7ed6;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>GET</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/ticket/opbrengst</code>

Dit endpoint geeft de totale opbrengst van de avond terug met <code style="color:#37b24d;font-weight:600">200 OK</code>: de prijzen van alle niet-geannuleerde tickets opgeteld.

Annuleer een ticket via punt 8 en vraag daarna dit endpoint opnieuw op. De opbrengst moet gedaald zijn.

---

### 11. Alles samen: een ticket verhuizen

<span style="background:#7048e8;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>SAMENGESTELDE OEFENING</b></span>

De tien vorige punten behandelen elk één stukje van de zaal: een plaats verkopen, een plaats vrijgeven, nakijken of een plaats al bezet is, de opbrengst tellen. Een medewerker die iemand naar een andere stoel wil zetten, moet vandaag annuleren en opnieuw verkopen, en tussen die twee handelingen in klopt de zaal even niet.

In dit laatste punt doe je het in één keer, en alle regels uit dit bestand komen er samen.

Voorzie een PUT-endpoint op:

<span style="background:#d9822b;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>PUT</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/ticket/verhuis/<span style="color:#e64980"><b>{id}</b></span>/<span style="color:#e64980"><b>{rij}</b></span>/<span style="color:#e64980"><b>{stoel}</b></span></code>

Het endpoint verplaatst een bestaand ticket naar een andere plaats. De oude plaats komt daarbij vrij.

Het endpoint moet:

- <code style="color:#f08c00;font-weight:600">404 Not Found</code> geven wanneer het ticket niet bestaat of geannuleerd is, met het bericht <code style="color:#845ef7">We vonden geen geldig ticket met Id <span style="color:#e64980"><b>{id}</b></span>.</code>
- <code style="color:#f08c00;font-weight:600">400 Bad Request</code> geven wanneer de rij of de stoel kleiner is dan 1, met de berichten uit punt 6;
- <code style="color:#f08c00;font-weight:600">400 Bad Request</code> geven wanneer de bezoeker al op die plaats zit, met het bericht <code style="color:#845ef7">Dit ticket zit al op rij <span style="color:#e64980"><b>{rij}</b></span> stoel <span style="color:#e64980"><b>{stoel}</b></span>.</code>
- <code style="color:#f08c00;font-weight:600">400 Bad Request</code> geven wanneer die plaats al bezet is door een ánder niet-geannuleerd ticket, met het bericht <code style="color:#845ef7">Rij <span style="color:#e64980"><b>{rij}</b></span> stoel <span style="color:#e64980"><b>{stoel}</b></span> is al verkocht.</code>
- bij succes de rij en de stoel aanpassen en <code style="color:#37b24d;font-weight:600">204 No Content</code> teruggeven.

> [!WARNING]
> Let op het verschil tussen de derde en de vierde regel. Beide gaan over een bezette plaats, maar in het ene geval is het je eigen plaats en in het andere die van iemand anders. Die twee gevallen scheiden is precies waar je in punt 7 al op gebotst bent.

De prijs verandert niet. Een verhuizing mag de opbrengst dus niet beïnvloeden.

#### Testscenario

Doorloop deze reeks en controleer na elke stap wat er gebeurt:

1. Vraag de opbrengst op via punt 10 en noteer ze.
2. Verhuis een ticket naar een vrije plaats. Je krijgt <code style="color:#37b24d;font-weight:600">204</code>. Vraag de opbrengst opnieuw op: ze is niet veranderd.
3. Verhuis datzelfde ticket naar precies dezelfde plaats. Je krijgt <code style="color:#f08c00;font-weight:600">400</code>.
4. Verhuis het naar de plaats van een ander geldig ticket. Je krijgt <code style="color:#f08c00;font-weight:600">400</code>.
5. Annuleer dat andere ticket via punt 8. Verhuis nu opnieuw naar die plaats. Nu krijg je <code style="color:#37b24d;font-weight:600">204</code>, want de plaats is vrijgekomen.
6. Probeer het geannuleerde ticket te herstellen via punt 9. Je krijgt <code style="color:#f08c00;font-weight:600">400</code>, want die plaats is intussen ingenomen.
7. Vraag de opbrengst een laatste keer op. Ze is gedaald met de prijs van het geannuleerde ticket, en met niets meer.

> [!WARNING]
> Loopt stap 5 mis, dan geeft je annulering de plaats niet vrij. Loopt stap 6 mis, dan kijkt je herstel niet naar wat er intussen gebeurd is. Beide fouten zijn onzichtbaar zolang je de endpoints apart test. Dat is de reden waarom deze oefening bestaat.
> 