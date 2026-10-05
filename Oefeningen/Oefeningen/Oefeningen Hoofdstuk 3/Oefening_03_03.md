# 03_03

<b>Hoofdstuk 03</b> &nbsp;·&nbsp; Twee repositories, één contract &nbsp;·&nbsp; PC-onderdelen &nbsp;·&nbsp; niveau <span style="color:#f03e3e">●●●</span> &nbsp;·&nbsp; ±2 uur

## Leerdoel

Na deze oefening kan je één interface door twee verschillende klassen laten implementeren, en tussen die twee wisselen met één regel in Program.cs.

Je leert een repository bouwen die zijn gegevens in een JSON-bestand bewaart, zodat ze een herstart van je API overleven.

Daarnaast ervaar je waarom je controller enkel met de interface mag praten: wanneer je van opslag wisselt, verander je in de controller geen letter.

## 

## Opdracht

De bestellijnen-API uit 02_03 verliest alles bij elke herstart. De boekhouding vindt dat onaanvaardbaar: wat er ooit op een bestelling stond, moet ook na een herstart nog op te vragen zijn. Een database komt er pas later. Tot dan wil Bit & Byte de gegevens in een bestand bewaren.

Jouw taak is om de BestellijnController te refactoren naar een repository, en daarna een tweede repository te schrijven die met een bestand werkt.

Via de API moet een medewerker, net als in 02_03:

1. de actieve bestellijnen kunnen opvragen;
2. alle bestellijnen kunnen opvragen, ook de geschrapte;
3. één bestellijn kunnen opvragen;
4. een bestellijn kunnen toevoegen, bijwerken en schrappen;
5. een geschrapte bestellijn kunnen herstellen;
6. de totale waarde en de bestelfiche kunnen opvragen;
7. de hele bestelling in één keer kunnen schrappen;

en daarnaast:

8. erop kunnen rekenen dat dit alles een herstart van de API overleeft.

Implementeer onderstaande functionaliteiten.

### 1. Je vertrekpunt

Vertrek van je eigen oplossing van 02_03. Na deze oefening moeten de volgende endpoints nog precies hetzelfde antwoorden als vandaag:

| Verzoek | Antwoord |
|---------|----------|
| <span style="background:#1c7ed6;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>GET</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/bestellijn</code> | <code style="color:#37b24d;font-weight:600">200 OK</code> met de actieve bestellijnen |
| <span style="background:#1c7ed6;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>GET</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/bestellijn/alles</code> | <code style="color:#37b24d;font-weight:600">200 OK</code> met alle bestellijnen, geschrapte inbegrepen |
| <span style="background:#1c7ed6;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>GET</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/bestellijn/<span style="color:#e64980"><b>{id}</b></span></code> | <code style="color:#37b24d;font-weight:600">200 OK</code> met de lijn, of <code style="color:#f08c00;font-weight:600">404 Not Found</code> met <code style="color:#845ef7">We vonden geen actieve bestellijn met Id <span style="color:#e64980"><b>{id}</b></span>.</code> |
| <span style="background:#2f9e44;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>POST</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/bestellijn</code> | <code style="color:#37b24d;font-weight:600">201 Created</code> met de nieuwe lijn en haar adres, of <code style="color:#f08c00;font-weight:600">400 Bad Request</code> met de reden |
| <span style="background:#d9822b;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>PUT</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/bestellijn/<span style="color:#e64980"><b>{id}</b></span></code> | <code style="color:#37b24d;font-weight:600">204 No Content</code>, <code style="color:#f08c00;font-weight:600">404 Not Found</code>, of <code style="color:#f08c00;font-weight:600">400 Bad Request</code> met de reden |
| <span style="background:#e03131;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>DELETE</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/bestellijn/<span style="color:#e64980"><b>{id}</b></span></code> | <code style="color:#37b24d;font-weight:600">204 No Content</code>, of <code style="color:#f08c00;font-weight:600">404 Not Found</code> met <code style="color:#845ef7">Kan bestellijn met Id <span style="color:#e64980"><b>{id}</b></span> niet schrappen, omdat deze niet gevonden is.</code> |
| <span style="background:#d9822b;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>PUT</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/bestellijn/herstel/<span style="color:#e64980"><b>{id}</b></span></code> | <code style="color:#37b24d;font-weight:600">204 No Content</code>, <code style="color:#f08c00;font-weight:600">404 Not Found</code>, of <code style="color:#f08c00;font-weight:600">400 Bad Request</code> met <code style="color:#845ef7">Deze bestellijn is niet geschrapt.</code> |
| <span style="background:#1c7ed6;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>GET</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/bestellijn/totaal</code> | <code style="color:#37b24d;font-weight:600">200 OK</code> met de totale waarde van de actieve lijnen |
| <span style="background:#1c7ed6;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>GET</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/bestellijn/fiche</code> | <code style="color:#37b24d;font-weight:600">200 OK</code> met de `BestelFiche`, met dezelfde zes gegevens als in 02_03 |
| <span style="background:#e03131;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>DELETE</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/bestellijn</code> | <code style="color:#37b24d;font-weight:600">200 OK</code> met de bijgewerkte fiche, of <code style="color:#f08c00;font-weight:600">400 Bad Request</code> met <code style="color:#845ef7">Er zijn geen actieve bestellijnen om te schrappen.</code> |

De drie redenen voor een <code style="color:#f08c00;font-weight:600">400</code> bij toevoegen en bijwerken blijven dezelfde als in 02_03:

- de omschrijving is leeg: <code style="color:#845ef7">Een bestellijn moet een omschrijving hebben.</code>
- het aantal is kleiner dan 1: <code style="color:#845ef7">Het aantal moet minstens 1 zijn.</code>
- de stukprijs is 0 of lager: <code style="color:#845ef7">De stukprijs moet groter zijn dan 0.</code>

Daarnaast blijven deze regels uit 02_03 gelden:

- een nieuwe lijn is altijd actief, wat de client ook meestuurt;
- bijwerken en schrappen werken enkel op actieve lijnen: een geschrapte lijn geeft daar <code style="color:#f08c00;font-weight:600">404</code>.

---

### 2. Het contract

Maak in de map Repositories een interface `IBestellijnRepository`.

Deze keer bedenk je zelf welke methodes erin horen. Vertrek van de tabel hierboven en vraag je per endpoint af welke gegevens de controller van de repository nodig heeft.

> [!IMPORTANT]
> Je interface bevat enkel methodes die gegevens ophalen of bewaren. Of een verzoek geldig is en welke statuscode erbij hoort, beslist de controller.

> [!TIP]
> Toets elke methode aan deze vraag: zou ze ook zinvol zijn voor een repository die zijn gegevens in een bestand bewaart, of later in een database? Is het antwoord nee, dan zit er waarschijnlijk iets in dat niet over gegevens gaat.

---

### 3. De eerste repository: in het geheugen

Maak een klasse `InMemoryBestellijnRepository` die `IBestellijnRepository` implementeert, en verhuis je lijst met bestellijnen naar deze klasse.

Pas de BestellijnController aan, zodat hij een `IBestellijnRepository` en een `ILogger<BestellijnController>` binnenkrijgt via zijn constructor, en leg in Program.cs vast welke klasse hij krijgt.

Log in elk endpoint minstens één regel: elk verzoek op niveau Information, elke <code style="color:#f08c00;font-weight:600">400</code> en <code style="color:#f08c00;font-weight:600">404</code> op niveau Warning.

> [!NOTE]
> Het schrappen blijft een soft delete. Een geschrapte lijn verdwijnt nooit uit je gegevens.

Doe nu alle verzoeken uit punt 1 opnieuw. Alles moet antwoorden zoals in 02_03, ook wanneer je iets toevoegt en het bij een volgend verzoek terug opvraagt.

---

### 4. De tweede repository: in een bestand

Maak een tweede klasse `BestandBestellijnRepository` die dezelfde interface `IBestellijnRepository` implementeert. Deze repository bewaart alle bestellijnen, ook de geschrapte, in een JSON-bestand met de naam `bestellijnen.json`.

De repository moet:

- bij de start de bestellijnen uit het bestand lezen;
- bestaat het bestand nog niet, beginnen met dezelfde startlijst als je in-memory versie, en die meteen wegschrijven;
- na elke wijziging de nieuwe toestand in het bestand zetten.

Het bestand moet leesbare JSON bevatten. Open het na een paar wijzigingen en kijk of je je bestellijnen terugvindt, met de juiste waarde voor `IsActief`.

> [!NOTE]
> Bestanden lezen en schrijven, en een lijst omzetten naar JSON en terug, ken je uit de vorige modules. Hoofdstuk 30 van je leerboek zet het nog eens op een rij.

> [!WARNING]
> Kijk na waar je bestand op je schijf terechtkomt. Start je de API een keer vanuit Visual Studio en een keer vanuit de terminal, dan staat het misschien niet twee keer op dezelfde plaats, en lijkt het alsof je gegevens verdwenen zijn.

---

### 5. Wisselen van opslag

Wissel in Program.cs van de in-memory repository naar de bestandsrepository.

> [!IMPORTANT]
> Bij het wisselen verander je niets aan de BestellijnController. Moet je daar toch iets aanpassen, dan praat je controller ergens niet met de interface, maar met een concrete klasse.

---

### 6. Alles samen: een herstart overleven

<span style="background:#7048e8;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>SAMENGESTELDE OEFENING</b></span>

De vijf vorige punten geven je twee repositories die hetzelfde contract volgen. In dit laatste punt bewijs je dat dat contract klopt: dezelfde controller moet met allebei werken, en met de bestandsrepository mag een herstart niets meer uitmaken.

Zet de bestandsrepository aan, verwijder `bestellijnen.json` als het al bestaat, start je API en doorloop deze reeks:

1. Vraag de fiche op. Noteer `AantalActieveLijnen`, `AantalGeschrapteLijnen`, `TotaalActief` en `TotaalGeschrapt`.
2. Voeg een geldige lijn toe en schrap een andere actieve lijn.
3. Vraag de fiche opnieuw op. `AantalActieveLijnen` is gelijk gebleven, `AantalGeschrapteLijnen` is met één gestegen, `TotaalActief` is gestegen met de waarde van de nieuwe lijn en gedaald met die van de geschrapte, en `TotaalGeschrapt` is gestegen met de waarde van de geschrapte lijn.
4. Stop je API en start hem opnieuw.
5. Vraag de fiche op. Ze is exact gelijk aan die van stap 3. Vraag ook alle bestellijnen op: de geschrapte lijn staat er nog, als geschrapt.
6. Voeg nog een lijn toe. Haar Id mag van geen enkele bestaande lijn zijn, ook niet van een geschrapte.
7. Herstel de lijn die je in stap 2 schrapte, en schrap daarna de hele bestelling in één keer. `IsLeeg` staat op ja.
8. Herstart opnieuw en vraag de fiche op. `IsLeeg` staat nog altijd op ja, en `AantalGeschrapteLijnen` telt al je lijnen.
9. Wissel in Program.cs terug naar de in-memory repository en doorloop stap 1 tot en met 4. Vraag daarna de fiche en alle bestellijnen op: na de herstart staat alles weer zoals in je startlijst.

> [!NOTE]
> Stap 9 is geen fout, maar het bewijs: je controller weet niet met welke opslag hij werkt. Hij kreeg in beide gevallen een `IBestellijnRepository`, en meer moest hij niet weten.

> [!WARNING]
> Gaat stap 6 mis, kijk dan hoe je een nieuw Id bepaalt. Na een herstart weet je repository enkel wat er in het bestand staat.
