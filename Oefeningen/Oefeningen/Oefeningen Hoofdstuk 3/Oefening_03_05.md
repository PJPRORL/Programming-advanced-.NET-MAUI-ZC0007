# 03_05

<b>Hoofdstuk 03</b> &nbsp;·&nbsp; CRUD via een repository &nbsp;·&nbsp; Fietsverhuur &nbsp;·&nbsp; niveau <span style="color:#f08c00">●●○</span> &nbsp;·&nbsp; ±90 min

## Leerdoel

Na deze oefening kan je een volledige CRUD-controller laten werken via een repository.

Je leert dat het toevoegen, wijzigen en verwijderen zelf in de repository gebeurt, terwijl de controller blijft beslissen of een verzoek geldig is en welke statuscode erbij hoort.

Daarnaast merk je wat er met je gegevens gebeurt tussen twee verzoeken, nu je lijst niet meer in de controller leeft, en geef je één controller meer dan één eigen repository.

## 

## Opdracht

Trapdoor wil van de fietsen-API uit 02_05 een API maken die klaar is voor een database: de lijst uit de controller, achter een interface. De balie wil daarnaast fietsen kunnen verhuren en terugbrengen, en nadien kunnen nalezen in welke volgorde welke fiets vertrok en terugkwam.

Jouw taak is om de FietsController te refactoren naar een repository, en er het verhuren en terugbrengen aan toe te voegen.

Via de API moet de balie, net als in 02_05:

1. alle fietsen kunnen opvragen;
2. één fiets kunnen opvragen;
3. een nieuwe fiets kunnen toevoegen;
4. een bestaande fiets kunnen bijwerken;
5. een fiets uit de vloot kunnen halen;
6. een fiets kunnen vervangen;

en daarnaast:

7. een fiets kunnen verhuren en terugbrengen, en die verhuringen kunnen nalezen.

Implementeer onderstaande functionaliteiten.

### 1. Je vertrekpunt

Vertrek van je eigen oplossing van 02_05. Tot en met punt 5 moeten de volgende endpoints nog precies hetzelfde antwoorden als vandaag. In punt 6 verander je er bewust een paar regels aan.

| Verzoek | Antwoord |
|---------|----------|
| <span style="background:#1c7ed6;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>GET</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/fiets</code> | <code style="color:#37b24d;font-weight:600">200 OK</code> met de volledige lijst |
| <span style="background:#1c7ed6;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>GET</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/fiets/<span style="color:#e64980"><b>{id}</b></span></code> | <code style="color:#37b24d;font-weight:600">200 OK</code> met de fiets, of <code style="color:#f08c00;font-weight:600">404 Not Found</code> met <code style="color:#845ef7">We vonden geen fiets met Id <span style="color:#e64980"><b>{id}</b></span>.</code> |
| <span style="background:#2f9e44;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>POST</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/fiets</code> | <code style="color:#37b24d;font-weight:600">201 Created</code> met de nieuwe fiets en haar adres, of <code style="color:#f08c00;font-weight:600">400 Bad Request</code> met de reden |
| <span style="background:#d9822b;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>PUT</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/fiets/<span style="color:#e64980"><b>{id}</b></span></code> | <code style="color:#37b24d;font-weight:600">204 No Content</code>, <code style="color:#f08c00;font-weight:600">404 Not Found</code> met <code style="color:#845ef7">Kan geen fiets bijwerken met Id <span style="color:#e64980"><b>{id}</b></span>, omdat deze niet bestaat.</code>, of <code style="color:#f08c00;font-weight:600">400 Bad Request</code> met de reden |
| <span style="background:#e03131;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>DELETE</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/fiets/<span style="color:#e64980"><b>{id}</b></span></code> | <code style="color:#37b24d;font-weight:600">204 No Content</code>, of <code style="color:#f08c00;font-weight:600">404 Not Found</code> met <code style="color:#845ef7">Kan fiets met Id <span style="color:#e64980"><b>{id}</b></span> niet verwijderen, omdat deze niet gevonden is.</code> |
| <span style="background:#d9822b;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>PUT</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/fiets/vervang/<span style="color:#e64980"><b>{id}</b></span></code> | <code style="color:#37b24d;font-weight:600">201 Created</code> met de nieuwe fiets en haar adres, <code style="color:#f08c00;font-weight:600">404 Not Found</code> met <code style="color:#845ef7">Kan fiets met Id <span style="color:#e64980"><b>{id}</b></span> niet vervangen, omdat deze niet gevonden is.</code>, of <code style="color:#f08c00;font-weight:600">400 Bad Request</code> met de reden |

De drie redenen voor een <code style="color:#f08c00;font-weight:600">400</code> blijven dezelfde als in 02_05:

- het type is leeg: <code style="color:#845ef7">Een fiets moet een type hebben.</code>
- de framemaat is kleiner dan 44 of groter dan 62: <code style="color:#845ef7">De framemaat moet tussen 44 en 62 liggen.</code>
- de prijs per dag is 0 of lager: <code style="color:#845ef7">De prijs per dag moet groter zijn dan 0.</code>

Bij een vervanging kijk je eerst of de oude fiets bestaat, en pas daarna of de nieuwe door de drie controles geraakt.

> [!TIP]
> Doe al deze verzoeken één keer vóór je begint, en noteer wat je terugkrijgt. Na je refactoring doe je exact dezelfde verzoeken opnieuw.

---

### 2. Het contract

Maak in de map Repositories een interface `IFietsRepository`.

De interface beschrijft wat je repository moet kunnen:

- alle fietsen teruggeven;
- één fiets teruggeven op basis van haar Id, of niets als ze niet bestaat;
- een nieuwe fiets toevoegen en haar teruggeven, met het Id dat ze gekregen heeft;
- een bestaande fiets bijwerken;
- een fiets verwijderen.

---

### 3. De repository

Maak een klasse `InMemoryFietsRepository` die `IFietsRepository` implementeert, en verhuis je lijst met fietsen naar deze klasse.

Het Id van een nieuwe fiets bepaalt vanaf nu de repository. Stuurt de client toch een Id mee, dan wordt dat nog altijd overschreven.

---

### 4. Wie beslist wat

> [!IMPORTANT]
> De drie controles uit 02_05, en de controle of een fiets bestaat, blijven in de controller. De repository voert uit wat de controller hem vraagt en beslist zelf niets: hij weigert geen invoer en kiest geen statuscode.

Pas de FietsController aan, zodat hij een `IFietsRepository` en een `ILogger<FietsController>` binnenkrijgt via zijn constructor. Leg in Program.cs vast welke klasse hij krijgt.

Log in elk endpoint minstens één regel:

- elk binnenkomend verzoek op niveau Information;
- elke <code style="color:#f08c00;font-weight:600">404</code> op niveau Warning;
- elke <code style="color:#f08c00;font-weight:600">400</code> op niveau Warning, met de reden in het bericht.

---

### 5. Gegevens tussen twee verzoeken

Voeg een fiets toe via POST en vraag daarna de volledige lijst op.

> [!WARNING]
> Staat je nieuwe fiets er niet meer bij, terwijl dat in 02_05 wel zo was? Dan leeft je lijst niet lang genoeg. Zoek uit hoe vaak ASP.NET Core een nieuwe repository aanmaakt, en wat er dan gebeurt met de lijst die erin zit. In les 02 had je hetzelfde probleem al eens, toen de lijst nog in de controller stond.

Test daarna elk endpoint opnieuw en controleer in het bijzonder:

- of een POST met framemaat 70 <code style="color:#f08c00;font-weight:600">400</code> geeft, niet in de lijst belandt, en een Warning met de reden in je log zet;
- of een DELETE op een Id dat je net verwijderd hebt, de tweede keer <code style="color:#f08c00;font-weight:600">404</code> geeft;
- of een fiets die je bijwerkt, bij een volgend verzoek nog steeds de nieuwe gegevens heeft.

---

### 6. Alles samen: verhuren en terugbrengen

<span style="background:#7048e8;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>SAMENGESTELDE OEFENING</b></span>

Tot nu toe zegt `IsBeschikbaar` enkel iets als de client het zelf invult. In dit laatste punt krijgt het een betekenis: een fiets die niet beschikbaar is, is verhuurd, en omgekeerd. Staat er in je startlijst een fiets die niet beschikbaar is, dan geldt die dus als verhuurd. Elke verhuring en elke teruggave wordt bijgehouden in een tweede repository, die je naast de eerste in dezelfde controller gebruikt.

#### Het model

Maak in de map Models een klasse `Verhuring` met volgende gegevens:

| Gegeven | Soort waarde |
|---------|--------------|
| Id      | geheel getal |
| Actie   | tekst        |
| FietsId | geheel getal |

#### De tweede repository

Maak een interface `IVerhuringRepository` en een klasse `InMemoryVerhuringRepository` die ze implementeert. Deze repository moet:

- een verhuring kunnen bewaren, waarbij hij zelf het Id bepaalt;
- alle verhuringen kunnen teruggeven, de oudste eerst.

De FietsController krijgt ook deze repository via zijn constructor.

Vind je dat `IFietsRepository` er een methode bij nodig heeft, dan mag je die toevoegen, zolang ze enkel gegevens ophaalt of bewaart.

> [!WARNING]
> Je verhuringen beginnen leeg, je fietsen niet. Kijk na of de manier waarop je een nieuw Id bepaalt, ook werkt voor de allereerste verhuring.

#### Verhuren

Voorzie een PUT-endpoint op:

<span style="background:#d9822b;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>PUT</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/fiets/verhuur/<span style="color:#e64980"><b>{id}</b></span></code>

- De fiets bestaat niet: <code style="color:#f08c00;font-weight:600">404 Not Found</code> met <code style="color:#845ef7">We vonden geen fiets met Id <span style="color:#e64980"><b>{id}</b></span>.</code>
- De fiets is al verhuurd: <code style="color:#f08c00;font-weight:600">400 Bad Request</code> met <code style="color:#845ef7">Deze fiets is al verhuurd.</code>
- Anders zet je de fiets op niet beschikbaar, bewaar je een verhuring met als actie `verhuurd`, en geef je <code style="color:#37b24d;font-weight:600">204 No Content</code> terug.

#### Terugbrengen

Voorzie een PUT-endpoint op:

<span style="background:#d9822b;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>PUT</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/fiets/terug/<span style="color:#e64980"><b>{id}</b></span></code>

- De fiets bestaat niet: <code style="color:#f08c00;font-weight:600">404 Not Found</code> met <code style="color:#845ef7">We vonden geen fiets met Id <span style="color:#e64980"><b>{id}</b></span>.</code>
- De fiets is niet verhuurd: <code style="color:#f08c00;font-weight:600">400 Bad Request</code> met <code style="color:#845ef7">Deze fiets is niet verhuurd.</code>
- Anders zet je de fiets weer op beschikbaar, bewaar je een verhuring met als actie `teruggebracht`, en geef je <code style="color:#37b24d;font-weight:600">204 No Content</code> terug.

#### De verhuringen opvragen

Voorzie een GET-endpoint op:

<span style="background:#1c7ed6;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>GET</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/fiets/verhuringen</code>

Dit endpoint geeft alle verhuringen terug, de oudste eerst, met statuscode <code style="color:#37b24d;font-weight:600">200 OK</code>. Zijn er nog geen, dan krijg je een lege lijst, ook met <code style="color:#37b24d;font-weight:600">200 OK</code>.

#### Wat er verandert aan de bestaande endpoints

- Een fiets die binnenkomt via POST of als vervanging, is altijd beschikbaar, wat de client ook meestuurt.
- Bijwerken verandert nooit of een fiets beschikbaar is. Dat kan enkel via verhuren en terugbrengen.
- Een verhuurde fiets kan je niet verwijderen en niet vervangen. Geef dan <code style="color:#f08c00;font-weight:600">400 Bad Request</code> met <code style="color:#845ef7">Een verhuurde fiets kan je niet uit de vloot halen.</code>

Bij een vervanging geldt deze volgorde: eerst of de oude fiets bestaat, dan of ze verhuurd is, en pas daarna de drie controles op de nieuwe fiets.

> [!IMPORTANT]
> Een actie die mislukt, met een <code style="color:#f08c00;font-weight:600">400</code> of een <code style="color:#f08c00;font-weight:600">404</code>, verandert niets aan de fiets en laat geen enkel spoor na in de verhuringen.

#### Testscenario

Herstart je API, zodat je met een lege lijst verhuringen begint, en doorloop deze reeks:

1. Vraag de verhuringen op. De lijst is leeg.
2. Verhuur een beschikbare fiets. Je krijgt <code style="color:#37b24d;font-weight:600">204</code>. Vraag de fiets op: ze is niet meer beschikbaar. De verhuringen bevatten één regel: `verhuurd`.
3. Verhuur dezelfde fiets nog eens. Je krijgt <code style="color:#f08c00;font-weight:600">400</code>. De verhuringen zijn niet veranderd.
4. Probeer die fiets te verwijderen, en daarna te vervangen door een geldige nieuwe fiets. Je krijgt twee keer <code style="color:#f08c00;font-weight:600">400</code>. De fiets staat nog in de lijst.
5. Werk die fiets bij met een andere prijs per dag, en zet in de body `IsBeschikbaar` op ja. Je krijgt <code style="color:#37b24d;font-weight:600">204</code>. De prijs is aangepast, maar de fiets is nog altijd verhuurd.
6. Breng de fiets terug. Je krijgt <code style="color:#37b24d;font-weight:600">204</code>. Ze is weer beschikbaar, en de verhuringen bevatten twee regels.
7. Breng ze nog eens terug. Je krijgt <code style="color:#f08c00;font-weight:600">400</code>.
8. Verhuur een Id dat niet bestaat. Je krijgt <code style="color:#f08c00;font-weight:600">404</code>. De verhuringen bevatten nog altijd twee regels.
9. Verwijder nu de teruggebrachte fiets. Je krijgt <code style="color:#37b24d;font-weight:600">204</code>.

> [!WARNING]
> Blijven je fietsen bewaard tussen twee verzoeken, maar zijn je verhuringen telkens weer verdwenen? Dan heb je de vraag uit punt 5 voor één van je twee repositories opgelost, maar niet voor de andere.
