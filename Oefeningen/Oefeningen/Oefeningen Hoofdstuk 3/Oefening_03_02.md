# 03_02

<b>Hoofdstuk 03</b> &nbsp;·&nbsp; CRUD via een repository &nbsp;·&nbsp; PC-onderdelen &nbsp;·&nbsp; niveau <span style="color:#f08c00">●●○</span> &nbsp;·&nbsp; ±2 uur

## Leerdoel

Na deze oefening kan je een volledige CRUD-controller laten werken via een repository.

Je leert dat het toevoegen, wijzigen en verwijderen zelf in de repository gebeurt, terwijl de controller blijft beslissen of een verzoek geldig is en welke statuscode erbij hoort.

Daarnaast merk je wat er met je gegevens gebeurt tussen twee verzoeken, nu je lijst niet meer in de controller leeft, en geef je één controller meer dan één eigen repository.

## 

## Opdracht

Bit & Byte wil van de ventilator-API uit 02_02 hetzelfde maken als van de artikel-API: de lijst uit de controller, achter een interface. De inkoopafdeling wil daarnaast kunnen nalezen wat er allemaal aan het assortiment veranderd is.

Jouw taak is om de VentilatorController te refactoren naar een repository, en er een wijzigingshistoriek aan toe te voegen.

Via de API moet de inkoopafdeling, net als in 02_02:

1. alle ventilatoren kunnen opvragen;
2. één ventilator kunnen opvragen;
3. een nieuwe ventilator kunnen toevoegen;
4. een bestaande ventilator kunnen bijwerken;
5. een ventilator kunnen verwijderen;
6. een ventilator kunnen vervangen;

en daarnaast:

7. kunnen nalezen welke wijzigingen er gebeurd zijn.

Implementeer onderstaande functionaliteiten.

### 1. Je vertrekpunt

Vertrek van je eigen oplossing van 02_02. Na deze oefening moeten de volgende endpoints nog precies hetzelfde antwoorden als vandaag:

| Verzoek | Antwoord |
|---------|----------|
| <span style="background:#1c7ed6;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>GET</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/ventilator</code> | <code style="color:#37b24d;font-weight:600">200 OK</code> met de volledige lijst |
| <span style="background:#1c7ed6;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>GET</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/ventilator/<span style="color:#e64980"><b>{id}</b></span></code> | <code style="color:#37b24d;font-weight:600">200 OK</code> met de ventilator, of <code style="color:#f08c00;font-weight:600">404 Not Found</code> met <code style="color:#845ef7">We vonden geen ventilator met Id <span style="color:#e64980"><b>{id}</b></span>.</code> |
| <span style="background:#2f9e44;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>POST</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/ventilator</code> | <code style="color:#37b24d;font-weight:600">201 Created</code> met de nieuwe ventilator en zijn adres, of <code style="color:#f08c00;font-weight:600">400 Bad Request</code> met de reden |
| <span style="background:#d9822b;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>PUT</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/ventilator/<span style="color:#e64980"><b>{id}</b></span></code> | <code style="color:#37b24d;font-weight:600">204 No Content</code>, <code style="color:#f08c00;font-weight:600">404 Not Found</code> met <code style="color:#845ef7">Kan geen ventilator bijwerken met Id <span style="color:#e64980"><b>{id}</b></span>, omdat deze niet bestaat.</code>, of <code style="color:#f08c00;font-weight:600">400 Bad Request</code> met de reden |
| <span style="background:#e03131;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>DELETE</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/ventilator/<span style="color:#e64980"><b>{id}</b></span></code> | <code style="color:#37b24d;font-weight:600">204 No Content</code>, of <code style="color:#f08c00;font-weight:600">404 Not Found</code> met <code style="color:#845ef7">Kan ventilator met Id <span style="color:#e64980"><b>{id}</b></span> niet verwijderen, omdat deze niet gevonden is.</code> |
| <span style="background:#d9822b;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>PUT</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/ventilator/vervang/<span style="color:#e64980"><b>{id}</b></span></code> | <code style="color:#37b24d;font-weight:600">201 Created</code> met de nieuwe ventilator en zijn adres, <code style="color:#f08c00;font-weight:600">404 Not Found</code> met <code style="color:#845ef7">Kan ventilator met Id <span style="color:#e64980"><b>{id}</b></span> niet vervangen, omdat deze niet gevonden is.</code>, of <code style="color:#f08c00;font-weight:600">400 Bad Request</code> met de reden |

De drie redenen voor een <code style="color:#f08c00;font-weight:600">400</code> blijven dezelfde als in 02_02, bij toevoegen, bijwerken en vervangen:

- het merk is leeg: <code style="color:#845ef7">Een ventilator moet een merk hebben.</code>
- de afmeting is niet 120 of 140: <code style="color:#845ef7">We verkopen enkel ventilatoren van 120 of 140 mm.</code>
- de prijs is 0 of lager: <code style="color:#845ef7">De prijs moet groter zijn dan 0.</code>

Bij een vervanging kijk je eerst of de oude ventilator bestaat, en pas daarna of de nieuwe door de drie controles geraakt.

> [!NOTE]
> Vervangen komt uit 02_02, punt 8. Had je het daar nog niet gemaakt, dan bouw je het hier, meteen via de repository. Punt 4 legt het stap voor stap uit.

> [!TIP]
> Doe al deze verzoeken één keer vóór je begint, en noteer wat je terugkrijgt. Na je refactoring doe je exact dezelfde verzoeken opnieuw.

---

### 2. Het contract

Maak in de map Repositories een interface `IVentilatorRepository`.

De interface beschrijft wat je repository moet kunnen:

- alle ventilatoren teruggeven;
- één ventilator teruggeven op basis van zijn Id, of niets als hij niet bestaat;
- een nieuwe ventilator toevoegen en hem teruggeven, met het Id dat hij gekregen heeft;
- een bestaande ventilator bijwerken;
- een ventilator verwijderen.

> [!NOTE]
> Vervangen staat niet in dit lijstje. Dat is geen vergissing: punt 4 legt uit waarom.

---

### 3. De repository

Maak een klasse `InMemoryVentilatorRepository` die `IVentilatorRepository` implementeert, en verhuis je lijst met ventilatoren naar deze klasse.

Het Id van een nieuwe ventilator bepaalt vanaf nu de repository. Stuurt de client toch een Id mee, dan wordt dat nog altijd overschreven.

---

### 4. Wie beslist wat

> [!IMPORTANT]
> De drie controles uit 02_02, en de controle of een ventilator bestaat, blijven in de controller. De repository voert uit wat de controller hem vraagt en beslist zelf niets: hij weigert geen invoer en kiest geen statuscode.

Pas de VentilatorController aan, zodat hij een `IVentilatorRepository` en een `ILogger<VentilatorController>` binnenkrijgt via zijn constructor. Leg in Program.cs vast welke klasse hij krijgt.

Log in elk endpoint minstens één regel:

- elk binnenkomend verzoek op niveau Information, als eerste regel van het endpoint, zodat ook een verzoek dat geweigerd wordt in je log staat;
- elke <code style="color:#f08c00;font-weight:600">404</code> op niveau Warning;
- elke <code style="color:#f08c00;font-weight:600">400</code> op niveau Warning, met de reden in het bericht.

#### Vervangen: één beslissing, twee acties

In 02_02 stond de lijst in de controller, en kon je ze daar rechtstreeks aanpassen. Nu kan de controller enkel nog iets vragen aan de repository. Vervangen is het duidelijkste voorbeeld van wie wat beslist: de controller doet alle controles, en laat de repository daarna twee dingen doen die ze al kan.

> [!IMPORTANT]
> Vervangen krijgt geen eigen methode in `IVentilatorRepository`. De controller bouwt het met de methodes uit punt 2.

Voorzie een PUT-endpoint op:

<span style="background:#d9822b;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>PUT</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/ventilator/vervang/<span style="color:#e64980"><b>{id}</b></span></code>

Een vervanging krijgt twee dingen binnen:

| Waar     | Wat |
|----------|-----|
| de URL   | het Id van de ventilator die uit het assortiment gaat |
| de body  | de nieuwe ventilator. Een Id in de body telt niet: dat kiest de repository, zoals bij een POST |

Het endpoint doet, in deze volgorde:

1. Het logt het binnenkomende verzoek op niveau Information.
2. Bestaat de ventilator uit de URL niet, dan geeft het <code style="color:#f08c00;font-weight:600">404 Not Found</code> met <code style="color:#845ef7">Kan ventilator met Id <span style="color:#e64980"><b>{id}</b></span> niet vervangen, omdat deze niet gevonden is.</code>, en logt het een Warning.
3. Raakt de nieuwe ventilator niet door een van de drie controles, dan geeft het <code style="color:#f08c00;font-weight:600">400 Bad Request</code> met de reden, en logt het een Warning met die reden.
4. Pas nu verandert er iets: de oude ventilator wordt verwijderd en de nieuwe wordt toegevoegd, allebei via de repository.
5. Het geeft <code style="color:#37b24d;font-weight:600">201 Created</code> terug, met de nieuwe ventilator en het adres waarop hij opgevraagd kan worden, net als bij een POST.

> [!IMPORTANT]
> Mislukt de vervanging, dan mag er niets veranderd zijn. Een klant die een ongeldige nieuwe ventilator doorstuurt, mag zijn oude niet kwijt zijn. Daarom verandert er pas iets in stap 4, als alle controles voorbij zijn.

> [!WARNING]
> Begint de route van je endpoint met een `/`, dan negeert ASP.NET Core de route van je controller. Je endpoint bestaat dan wel, maar niet op <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/ventilator/vervang/<span style="color:#e64980"><b>{id}</b></span></code>. Je leerboek legt het uit in hoofdstuk 25, bij de veelgemaakte fouten.

Test het vervangen. Elke stap kijkt met een tweede verzoek na wat er veranderd is. Dat lukt enkel als je lijst tussen twee verzoeken blijft bestaan; lukt het niet, lees dan eerst punt 5.

1. Herstart je API, vraag de volledige lijst op en onthoud welke ventilatoren erin staan.
2. <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>PUT</b> api/ventilator/vervang/2</code> met deze body:

   ```json
   { "merk": "Arctic", "afmetingInMm": 92, "verlichting": "ARGB", "prijs": 15.99 }
   ```

   Je krijgt <code style="color:#f08c00;font-weight:600">400</code> met <code style="color:#845ef7">We verkopen enkel ventilatoren van 120 of 140 mm.</code> Vraag de lijst opnieuw op: ventilator 2 staat er nog, onveranderd, en er is niets bijgekomen.
3. <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>PUT</b> api/ventilator/vervang/99</code> met dezelfde body. Je krijgt <code style="color:#f08c00;font-weight:600">404</code>, geen <code style="color:#f08c00;font-weight:600">400</code>: eerst kijk je of de oude ventilator bestaat.
4. <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>PUT</b> api/ventilator/vervang/2</code>, nu met een afmeting van 120. Je krijgt <code style="color:#37b24d;font-weight:600">201</code> met de Arctic en een nieuw Id. De header `Location` eindigt op `/api/ventilator/`, gevolgd door dat nieuwe Id; hoofdletters tellen daarbij niet.
5. <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> api/ventilator/2</code> geeft nu <code style="color:#f08c00;font-weight:600">404</code>, en de volledige lijst bevat de Arctic.

> [!NOTE]
> Bepaalt je repository een nieuw Id als het hoogste Id plus één, en vervang je de ventilator met het hoogste Id, dan kan de nieuwe datzelfde Id krijgen. Dat hangt af van de volgorde waarin je verwijdert en toevoegt. Beide volgordes mogen.

---

### 5. Gegevens tussen twee verzoeken

Voeg een ventilator toe via POST en vraag daarna de volledige lijst op.

> [!WARNING]
> Staat je nieuwe ventilator er niet meer bij, terwijl dat in 02_02 wel zo was? Dan leeft je lijst niet lang genoeg. Zoek uit hoe vaak ASP.NET Core een nieuwe repository aanmaakt, en wat er dan gebeurt met de lijst die erin zit. In les 02 had je hetzelfde probleem al eens, toen de lijst nog in de controller stond.

Test daarna elk endpoint opnieuw en controleer in het bijzonder:

- of een POST met een afmeting van 92 mm <code style="color:#f08c00;font-weight:600">400</code> geeft, niet in de lijst belandt, en een Warning met de reden in je log zet;
- of een DELETE op een Id dat je net verwijderd hebt, de tweede keer <code style="color:#f08c00;font-weight:600">404</code> geeft;
- of een ventilator die je bijwerkt, bij een volgend verzoek nog steeds de nieuwe gegevens heeft.

> [!NOTE]
> Deze drie controles gaan over toevoegen, verwijderen en bijwerken. Ze lijken op de test van het vervangen in punt 4, maar ze testen iets anders: niet wát een endpoint antwoordt, maar of je lijst onthoudt wat er gebeurde.

---

### 6. Alles samen: de wijzigingshistoriek

<span style="background:#7048e8;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>SAMENGESTELDE OEFENING</b></span>

De inkoopafdeling wil weten wat er met het assortiment gebeurd is: welke ventilatoren erbij kwamen, welke gewijzigd werden en welke verdwenen. Daarvoor bouw je een tweede repository, die je naast de eerste in dezelfde controller gebruikt.

Dit punt bestaat uit vijf delen. Bouw ze in deze volgorde, en laat je project na elk deel bouwen.

| Deel | Wat je maakt | Waar |
|------|--------------|------|
| 6a | het model `Wijziging` | Models |
| 6b | het contract en de repository | Repositories |
| 6c | de repository in je controller | Program.cs en de constructor van de controller |
| 6d | een wijziging bewaren na elke geslaagde actie | de endpoints die iets veranderen |
| 6e | de historiek opvragen | een nieuw endpoint in de controller |

#### 6a. Het model

Maak in de map Models een klasse `Wijziging` met volgende gegevens:

| Gegeven      | Soort waarde |
|--------------|--------------|
| Id           | geheel getal |
| Actie        | tekst        |
| VentilatorId | geheel getal |

#### 6b. Het contract en de repository

Maak een interface `IWijzigingRepository` en een klasse `InMemoryWijzigingRepository` die ze implementeert. Het contract bevat twee methodes. De namen kies je zelf.

| De methode moet               | Krijgt binnen | Geeft terug |
|-------------------------------|---------------|-------------|
| een wijziging bewaren         | een wijziging met een actie en een VentilatorId, maar zonder Id | niets |
| alle wijzigingen teruggeven   | niets         | een lijst met alle wijzigingen, de oudste eerst |

Voor de klasse gelden drie regels:

- De lijst met wijzigingen begint leeg. Ze moet blijven bestaan tussen twee verzoeken, net als je lijst met ventilatoren.
- Bij het bewaren kiest de repository het Id: 1 voor de allereerste wijziging, en daarna telkens één meer dan de vorige.
- De oudste eerst betekent: in de volgorde waarin ze bewaard werden.

> [!WARNING]
> Je historiek begint leeg, je ventilatoren niet. Neem je de manier om een Id te bepalen over uit je ventilatorrepository, kijk dan wat die doet met een lege lijst. Je leerboek legt het uit in hoofdstuk 11.

> [!TIP]
> Een `List` zet wat je toevoegt altijd achteraan. Moet je dan nog sorteren om de oudste eerst te krijgen?

#### 6c. De repository in je controller

- Leg in Program.cs vast welke klasse ASP.NET Core aanlevert wanneer iemand om een `IWijzigingRepository` vraagt, op dezelfde manier als voor je ventilatoren.
- Laat de VentilatorController ook deze repository binnenkrijgen via zijn constructor, en bewaar ze in een `private readonly` veld.

Start je API en vraag <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> api/ventilator</code> op. Je krijgt nog altijd <code style="color:#37b24d;font-weight:600">200</code> met je ventilatoren.

> [!WARNING]
> Krijgt nu élk verzoek naar je controller een <code style="color:#f03e3e;font-weight:600">500</code>, met in je terminal *Unable to resolve service for type*, gevolgd door de naam van je interface? Dan vraagt je constructor iets wat Program.cs niet kent.

#### 6d. Een wijziging bewaren na elke geslaagde actie

Na elke geslaagde actie bewaart de controller een wijziging:

| Geslaagde actie | Actie                        | VentilatorId                       |
|-----------------|------------------------------|------------------------------------|
| POST            | `toegevoegd`                 | het Id van de nieuwe ventilator, zoals de repository hem teruggaf |
| PUT (bijwerken) | `bijgewerkt`                 | het Id uit de URL                  |
| DELETE          | `verwijderd`                 | het Id uit de URL                  |
| PUT (vervangen) | eerst `verwijderd`, dan `toegevoegd` | eerst het Id uit de URL, dan het Id van de nieuwe ventilator |

De acties schrijf je letterlijk zoals in de tabel, in kleine letters.

> [!IMPORTANT]
> Een actie die mislukt, met een <code style="color:#f08c00;font-weight:600">400</code> of een <code style="color:#f08c00;font-weight:600">404</code>, laat geen enkel spoor na in de historiek.

> [!TIP]
> Daarvoor hoef je niets extra te controleren. Bewaar de wijziging na de actie zelf, vlak voor het antwoord. Een <code style="color:#f08c00;font-weight:600">400</code> of een <code style="color:#f08c00;font-weight:600">404</code> verlaat je endpoint al eerder.

#### 6e. De historiek opvragen

Voorzie een GET-endpoint op:

<span style="background:#1c7ed6;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>GET</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/ventilator/historiek</code>

Dit endpoint logt het verzoek op niveau Information, en geeft alle wijzigingen terug, de oudste eerst, met statuscode <code style="color:#37b24d;font-weight:600">200 OK</code>. Is er nog niets gewijzigd, dan krijg je een lege lijst `[]`, ook met <code style="color:#37b24d;font-weight:600">200 OK</code>: een lege historiek is geen fout.

> [!NOTE]
> Vraag je <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> api/ventilator/historiek</code> op vóór dit endpoint bestaat, dan krijg je <code style="color:#f08c00;font-weight:600">400</code>. Het verzoek komt dan bij <span style="background:#1c7ed6;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>GET</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/ventilator/<span style="color:#e64980"><b>{id}</b></span></code> terecht, en *historiek* is geen getal. Je leerboek legt dat uit in hoofdstuk 25, bij "Voorspel de uitkomst". Zodra je endpoint bestaat, kiest ASP.NET Core het zelf: een vast stuk tekst in een route gaat voor op een parameter.

#### Testscenario

Herstart je API, zodat je met een lege historiek begint, en doorloop deze reeks. Vraag na elke stap de historiek op.

1. De historiek is leeg: `[]`.
2. Voeg deze ventilator toe:

   ```json
   { "merk": "Noctua", "afmetingInMm": 140, "verlichting": "geen", "prijs": 27.5 }
   ```

   Je krijgt <code style="color:#37b24d;font-weight:600">201</code>. De historiek bevat één wijziging: `toegevoegd`, met het Id van je nieuwe ventilator.
3. Voeg dezelfde ventilator toe, maar met een afmeting van 92 mm. Je krijgt <code style="color:#f08c00;font-weight:600">400</code>. De historiek is niet veranderd.
4. Werk de ventilator uit stap 2 bij, met verlichting `RGB` en een prijs van 32. Je krijgt <code style="color:#37b24d;font-weight:600">204</code>. De historiek bevat twee wijzigingen.
5. Vervang ventilator 1 door deze ventilator:

   ```json
   { "merk": "Arctic", "afmetingInMm": 120, "verlichting": "ARGB", "prijs": 15.99 }
   ```

   Je krijgt <code style="color:#37b24d;font-weight:600">201</code>. De historiek bevat vier wijzigingen; de laatste twee zijn `verwijderd` met Id 1 en `toegevoegd` met het Id van de Arctic.
6. Verwijder een Id dat niet bestaat, zoals 999. Je krijgt <code style="color:#f08c00;font-weight:600">404</code>. De historiek bevat nog altijd vier wijzigingen.
7. Vraag de volledige lijst met ventilatoren op. Ze moet kloppen met wat de historiek vertelt: de Noctua uit stap 2 met de gegevens uit stap 4, de Arctic, en geen ventilator 1 meer.

Heeft je startlijst de Id's 1 tot en met 4, dan geeft <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> api/ventilator/historiek</code> na stap 6 als resultaat:

```json
[
  { "id": 1, "actie": "toegevoegd", "ventilatorId": 5 },
  { "id": 2, "actie": "bijgewerkt", "ventilatorId": 5 },
  { "id": 3, "actie": "verwijderd", "ventilatorId": 1 },
  { "id": 4, "actie": "toegevoegd", "ventilatorId": 6 }
]
```

> [!WARNING]
> Blijven je ventilatoren bewaard tussen twee verzoeken, maar is je historiek telkens weer leeg? Dan heb je de vraag uit punt 5 voor één van je twee repositories opgelost, maar niet voor de andere.
