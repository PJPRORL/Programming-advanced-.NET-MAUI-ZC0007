# 03_06

<b>Hoofdstuk 03</b> &nbsp;·&nbsp; Twee repositories, één contract &nbsp;·&nbsp; Concertzaal &nbsp;·&nbsp; niveau <span style="color:#f03e3e">●●●</span> &nbsp;·&nbsp; ±2 uur

## Leerdoel

Na deze oefening kan je één interface door twee verschillende klassen laten implementeren, en tussen die twee wisselen met één regel in Program.cs.

Je leert een repository bouwen die zijn gegevens in een JSON-bestand bewaart, zodat ze een herstart van je API overleven.

Daarnaast ervaar je dat controles die naar de bestaande gegevens kijken, zoals een bezette plaats, enkel kloppen als ze naar dezelfde opslag kijken als de rest van je API.

## 

## Opdracht

De ticket-API van De Notenbalk uit 02_06 verliest alles bij elke herstart. Dat is meer dan vervelend: na een herstart lijken alle plaatsen weer vrij, en kan dezelfde stoel twee keer verkocht worden. Een database komt er pas later. Tot dan wil de zaal de tickets in een bestand bewaren.

Jouw taak is om de TicketController te refactoren naar een repository, en daarna een tweede repository te schrijven die met een bestand werkt.

Via de API moet een medewerker, net als in 02_06:

1. de geldige tickets kunnen opvragen;
2. alle tickets kunnen opvragen, ook de geannuleerde;
3. één ticket kunnen opvragen;
4. een ticket kunnen verkopen, wijzigen en annuleren;
5. een geannuleerd ticket kunnen herstellen;
6. de opbrengst van de avond kunnen opvragen;
7. een ticket kunnen verhuizen naar een andere plaats;

en daarnaast:

8. erop kunnen rekenen dat dit alles een herstart van de API overleeft.

Implementeer onderstaande functionaliteiten.

### 1. Je vertrekpunt

Vertrek van je eigen oplossing van 02_06. Na deze oefening moeten de volgende endpoints nog precies hetzelfde antwoorden als vandaag:

| Verzoek | Antwoord |
|---------|----------|
| <span style="background:#1c7ed6;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>GET</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/ticket</code> | <code style="color:#37b24d;font-weight:600">200 OK</code> met de niet-geannuleerde tickets |
| <span style="background:#1c7ed6;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>GET</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/ticket/alles</code> | <code style="color:#37b24d;font-weight:600">200 OK</code> met alle tickets, geannuleerde inbegrepen |
| <span style="background:#1c7ed6;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>GET</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/ticket/<span style="color:#e64980"><b>{id}</b></span></code> | <code style="color:#37b24d;font-weight:600">200 OK</code> met het ticket, of <code style="color:#f08c00;font-weight:600">404 Not Found</code> met <code style="color:#845ef7">We vonden geen geldig ticket met Id <span style="color:#e64980"><b>{id}</b></span>.</code> |
| <span style="background:#2f9e44;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>POST</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/ticket</code> | <code style="color:#37b24d;font-weight:600">201 Created</code> met het nieuwe ticket en zijn adres, of <code style="color:#f08c00;font-weight:600">400 Bad Request</code> met de reden |
| <span style="background:#d9822b;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>PUT</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/ticket/<span style="color:#e64980"><b>{id}</b></span></code> | <code style="color:#37b24d;font-weight:600">204 No Content</code>, <code style="color:#f08c00;font-weight:600">404 Not Found</code>, of <code style="color:#f08c00;font-weight:600">400 Bad Request</code> met de reden |
| <span style="background:#e03131;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>DELETE</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/ticket/<span style="color:#e64980"><b>{id}</b></span></code> | <code style="color:#37b24d;font-weight:600">204 No Content</code>, of <code style="color:#f08c00;font-weight:600">404 Not Found</code> met <code style="color:#845ef7">Kan ticket met Id <span style="color:#e64980"><b>{id}</b></span> niet annuleren, omdat het niet gevonden is.</code> |
| <span style="background:#d9822b;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>PUT</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/ticket/herstel/<span style="color:#e64980"><b>{id}</b></span></code> | <code style="color:#37b24d;font-weight:600">204 No Content</code>, <code style="color:#f08c00;font-weight:600">404 Not Found</code>, of <code style="color:#f08c00;font-weight:600">400 Bad Request</code> met de reden |
| <span style="background:#1c7ed6;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>GET</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/ticket/opbrengst</code> | <code style="color:#37b24d;font-weight:600">200 OK</code> met de som van de prijzen van alle niet-geannuleerde tickets |
| <span style="background:#d9822b;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>PUT</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/ticket/verhuis/<span style="color:#e64980"><b>{id}</b></span>/<span style="color:#e64980"><b>{rij}</b></span>/<span style="color:#e64980"><b>{stoel}</b></span></code> | <code style="color:#37b24d;font-weight:600">204 No Content</code>, <code style="color:#f08c00;font-weight:600">404 Not Found</code> met <code style="color:#845ef7">We vonden geen geldig ticket met Id <span style="color:#e64980"><b>{id}</b></span>.</code>, of <code style="color:#f08c00;font-weight:600">400 Bad Request</code> met de reden |

De redenen voor een <code style="color:#f08c00;font-weight:600">400</code> blijven dezelfde als in 02_06:

- de naam is leeg: <code style="color:#845ef7">Een ticket moet op naam staan.</code>
- de rij is kleiner dan 1: <code style="color:#845ef7">De rij moet minstens 1 zijn.</code>
- de stoel is kleiner dan 1: <code style="color:#845ef7">De stoel moet minstens 1 zijn.</code>
- de prijs is 0 of lager: <code style="color:#845ef7">De prijs moet groter zijn dan 0.</code>
- de plaats is al bezet door een niet-geannuleerd ticket: <code style="color:#845ef7">Rij <span style="color:#e64980"><b>{rij}</b></span> stoel <span style="color:#e64980"><b>{stoel}</b></span> is al verkocht.</code>
- bij een herstel, het ticket is niet geannuleerd: <code style="color:#845ef7">Dit ticket is niet geannuleerd.</code>
- bij een herstel, de plaats is intussen weg: <code style="color:#845ef7">Rij <span style="color:#e64980"><b>{rij}</b></span> stoel <span style="color:#e64980"><b>{stoel}</b></span> is intussen aan iemand anders verkocht.</code>
- bij een verhuizing naar de eigen plaats: <code style="color:#845ef7">Dit ticket zit al op rij <span style="color:#e64980"><b>{rij}</b></span> stoel <span style="color:#e64980"><b>{stoel}</b></span>.</code>

Daarnaast blijven deze regels uit 02_06 gelden:

- een nieuw ticket is nooit geannuleerd, wat de client ook meestuurt;
- wijzigen en annuleren werken enkel op geldige tickets: een geannuleerd ticket geeft daar <code style="color:#f08c00;font-weight:600">404</code>;
- bij een wijziging mag een ticket zijn eigen plaats houden: dat is geen dubbele verkoop.

---

### 2. Het contract

Maak in de map Repositories een interface `ITicketRepository`.

Deze keer bedenk je zelf welke methodes erin horen. Vertrek van de tabel hierboven en vraag je per endpoint af welke gegevens de controller van de repository nodig heeft.

> [!IMPORTANT]
> Je interface bevat enkel methodes die gegevens ophalen of bewaren. Of een verzoek geldig is en welke statuscode erbij hoort, beslist de controller.

> [!TIP]
> Toets elke methode aan deze vraag: zou ze ook zinvol zijn voor een repository die zijn gegevens in een bestand bewaart, of later in een database? Is het antwoord nee, dan zit er waarschijnlijk iets in dat niet over gegevens gaat.

---

### 3. De eerste repository: in het geheugen

Maak een klasse `InMemoryTicketRepository` die `ITicketRepository` implementeert, en verhuis je lijst met tickets naar deze klasse.

Pas de TicketController aan, zodat hij een `ITicketRepository` en een `ILogger<TicketController>` binnenkrijgt via zijn constructor, en leg in Program.cs vast welke klasse hij krijgt.

Log in elk endpoint minstens één regel: elk verzoek op niveau Information, elke <code style="color:#f08c00;font-weight:600">400</code> en <code style="color:#f08c00;font-weight:600">404</code> op niveau Warning.

> [!NOTE]
> Annuleren blijft een soft delete. Een geannuleerd ticket verdwijnt nooit uit je gegevens.

Doe nu alle verzoeken uit punt 1 opnieuw. Alles moet antwoorden zoals in 02_06, ook wanneer je iets verkoopt en het bij een volgend verzoek terug opvraagt.

---

### 4. De tweede repository: in een bestand

Maak een tweede klasse `BestandTicketRepository` die dezelfde interface `ITicketRepository` implementeert. Deze repository bewaart alle tickets, ook de geannuleerde, in een JSON-bestand met de naam `tickets.json`.

De repository moet:

- bij de start de tickets uit het bestand lezen;
- bestaat het bestand nog niet, beginnen met dezelfde startlijst als je in-memory versie, en die meteen wegschrijven;
- na elke wijziging de nieuwe toestand in het bestand zetten.

Het bestand moet leesbare JSON bevatten. Open het na een paar wijzigingen en kijk of je je tickets terugvindt, met de juiste waarde voor `IsGeannuleerd`.

> [!NOTE]
> Bestanden lezen en schrijven, en een lijst omzetten naar JSON en terug, ken je uit de vorige modules. Hoofdstuk 30 van je leerboek zet het nog eens op een rij.

> [!WARNING]
> Kijk na waar je bestand op je schijf terechtkomt. Start je de API een keer vanuit Visual Studio en een keer vanuit de terminal, dan staat het misschien niet twee keer op dezelfde plaats, en lijkt het alsof je gegevens verdwenen zijn.

---

### 5. Wisselen van opslag

Wissel in Program.cs van de in-memory repository naar de bestandsrepository.

> [!IMPORTANT]
> Bij het wisselen verander je niets aan de TicketController. Moet je daar toch iets aanpassen, dan praat je controller ergens niet met de interface, maar met een concrete klasse.

---

### 6. Alles samen: een herstart overleven

<span style="background:#7048e8;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>SAMENGESTELDE OEFENING</b></span>

De vijf vorige punten geven je twee repositories die hetzelfde contract volgen. In dit laatste punt bewijs je dat dat contract klopt, en dat de plaatscontrole ook na een herstart nog weet welke stoelen bezet zijn.

Zet de bestandsrepository aan, verwijder `tickets.json` als het al bestaat, start je API en doorloop deze reeks:

1. Vraag de opbrengst op en noteer ze.
2. Verkoop een ticket op een vrije plaats, en annuleer een ander geldig ticket. Noteer van beide de rij en de stoel.
3. Stop je API en start hem opnieuw.
4. Vraag de opbrengst op. Ze is die van stap 1, plus de prijs van het nieuwe ticket, min de prijs van het geannuleerde. Vraag ook alle tickets op: het geannuleerde ticket staat er nog, als geannuleerd.
5. Verkoop een ticket op de plaats van het nieuwe ticket uit stap 2. Je krijgt <code style="color:#f08c00;font-weight:600">400</code>: die plaats is na de herstart nog altijd bezet.
6. Verkoop een ticket op de plaats van het geannuleerde ticket. Je krijgt <code style="color:#37b24d;font-weight:600">201</code>: die plaats is na de herstart nog altijd vrij. Het Id van dit ticket mag van geen enkel bestaand ticket zijn, ook niet van een geannuleerd.
7. Probeer het geannuleerde ticket te herstellen. Je krijgt <code style="color:#f08c00;font-weight:600">400</code>, want zijn plaats is intussen ingenomen.
8. Noteer de opbrengst en verhuis daarna het nieuwe ticket uit stap 2 naar een vrije plaats. Je krijgt <code style="color:#37b24d;font-weight:600">204</code>. Herstart, en vraag het ticket op: het staat op zijn nieuwe plaats, en de opbrengst is door de verhuizing niet veranderd.
9. Wissel in Program.cs terug naar de in-memory repository en doorloop stap 1 tot en met 3. Vraag daarna de opbrengst en alle tickets op: na de herstart staat alles weer zoals in je startlijst.

> [!NOTE]
> Stap 9 is geen fout, maar het bewijs: je controller weet niet met welke opslag hij werkt. Hij kreeg in beide gevallen een `ITicketRepository`, en meer moest hij niet weten.

> [!WARNING]
> Loopt stap 5 of 6 mis na de herstart, maar niet ervoor? Dan kijkt je plaatscontrole niet naar dezelfde gegevens als de rest van je API.
