# 04_06

<b>Hoofdstuk 04</b> &nbsp;·&nbsp; Een API op de database, migraties terugdraaien, in schoolstijl &nbsp;·&nbsp; Concertzaal &nbsp;·&nbsp; niveau <span style="color:#f03e3e">●●●</span> &nbsp;·&nbsp; ±2,5 uur

## Leerdoel

Na deze oefening kan je een API met negen endpoints rechtstreeks op een database laten werken, en een migratie die al op de database toegepast is weer volledig ongedaan maken.

Je leert wat er in een migratie staat, wat `Up` en `Down` doen, en waarom je een migratie niet zomaar mag weggooien zodra de database ze kent.

Daarnaast zet je je API om naar de stijl van de schoolopgaven, met een Id die je API zelf berekent en een kleine JSON voor een berekende waarde.

## 

## Opdracht

De ticket-API van De Notenbalk uit 03_06 kan wisselen tussen een repository in het geheugen en een repository in een JSON-bestand. De zaal wil een echte database, en het nieuwe kassasysteem verwacht de routes en de antwoorden in de stijl van de schoolopgaven, zoals in 04_04 en 04_05. Ondertussen twijfelt de zaal nog over een prijscategorie per ticket.

Jouw taak is om een database op te zetten voor de tickets, je controller rechtstreeks met die database te laten werken in de nieuwe stijl, en een migratie die achteraf een vergissing blijkt, netjes terug te draaien.

Via de API moet een medewerker, net als in 03_06:

1. de geldige en alle tickets kunnen opvragen, en één ticket;
2. een ticket kunnen verkopen, wijzigen, annuleren en herstellen;
3. de opbrengst van de avond kunnen opvragen;
4. een ticket kunnen verhuizen naar een andere plaats.

Implementeer onderstaande functionaliteiten.

### 1. Je vertrekpunt

Vertrek van je eigen oplossing van 03_06. Alle regels uit die oefening blijven gelden, met dezelfde redenen bij een <code style="color:#f08c00;font-weight:600">400</code>:

- de naam is leeg: <code style="color:#845ef7">Een ticket moet op naam staan.</code>
- de rij is kleiner dan 1: <code style="color:#845ef7">De rij moet minstens 1 zijn.</code>
- de stoel is kleiner dan 1: <code style="color:#845ef7">De stoel moet minstens 1 zijn.</code>
- de prijs is 0 of lager: <code style="color:#845ef7">De prijs moet groter zijn dan 0.</code>
- de plaats is al bezet door een niet-geannuleerd ticket: <code style="color:#845ef7">Rij <span style="color:#e64980"><b>{rij}</b></span> stoel <span style="color:#e64980"><b>{stoel}</b></span> is al verkocht.</code>
- bij een herstel, het ticket is niet geannuleerd: <code style="color:#845ef7">Dit ticket is niet geannuleerd.</code>
- bij een herstel, de plaats is intussen weg: <code style="color:#845ef7">Rij <span style="color:#e64980"><b>{rij}</b></span> stoel <span style="color:#e64980"><b>{stoel}</b></span> is intussen aan iemand anders verkocht.</code>
- bij een verhuizing naar de eigen plaats: <code style="color:#845ef7">Dit ticket zit al op rij <span style="color:#e64980"><b>{rij}</b></span> stoel <span style="color:#e64980"><b>{stoel}</b></span>.</code>

Annuleren blijft annuleren: een geannuleerd ticket verdwijnt niet, het telt enkel niet meer mee.

Vanaf nu ligt het model vast, met deze C#-types:

| Property      | C#-type  |
|---------------|----------|
| Id            | `int`    |
| Naam          | `string` |
| Rij           | `int`    |
| Stoel         | `int`    |
| Prijs         | `double` |
| IsGeannuleerd | `bool`   |

---

### 2. De database en de tabel

Gebruik je PostgreSQL-container uit les 04, of start er een. Deze oefening krijgt een eigen database met de naam `ConcertzaalDb`.

Installeer de packages uit les 04, zet in `appsettings.json` een connectiestring met de naam `PostgresConnection` die naar `ConcertzaalDb` wijst, en maak in de map Data een klasse `ConcertzaalContext`. Registreer ze in Program.cs.

Voeg een `DbSet<Ticket>` toe met de naam `Tickets`, en leg met de Fluent API vast:

| Property      | Kolom in de database |
|---------------|----------------------|
| Id            | de sleutel |
| Naam          | tekst van hoogstens 100 tekens, verplicht |
| Rij           | geheel getal, verplicht |
| Stoel         | geheel getal, verplicht |
| Prijs         | kommagetal, verplicht |
| IsGeannuleerd | ja of nee, verplicht |

De tabel heet `Tickets`. Leg met `HasData` deze zes tickets vast:

| Id | Naam             | Rij | Stoel | Prijs | IsGeannuleerd |
|----|------------------|-----|-------|-------|---------------|
| 1  | Sofie Peeters    | 1   | 5     | 45.00 | nee           |
| 2  | Tom Janssens     | 1   | 6     | 45.00 | nee           |
| 3  | Lisa Maes        | 3   | 12    | 35.00 | nee           |
| 4  | Karim El Idrissi | 3   | 13    | 35.00 | ja            |
| 5  | Emma Claes       | 7   | 2     | 25.00 | nee           |
| 6  | Noah Willems     | 7   | 3     | 25.00 | nee           |

Maak een migratie met de naam `InitialCreate` en werk de database bij. Controleer in pgAdmin of in VS Code dat elke kolom het juiste type heeft, en dat de zes tickets erin staan.

---

### 3. De controller op de database, in de nieuwe stijl

Pas je `TicketController` aan: hij krijgt via zijn constructor voortaan je `ConcertzaalContext` binnen, naast zijn `ILogger<TicketController>`, in plaats van een repository. Haal de registratie van je repository weg uit Program.cs. Je klassen voor het geheugen en voor het bestand mogen blijven staan; in 05_06 komt de bestandsversie terug.

| Endpoint | Antwoord |
|----------|----------|
| <span style="background:#1c7ed6;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>GET</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">tickets</code> | <code style="color:#37b24d;font-weight:600">200 OK</code> met de niet-geannuleerde tickets, op Id gesorteerd |
| <span style="background:#1c7ed6;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>GET</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">tickets/alles</code> | <code style="color:#37b24d;font-weight:600">200 OK</code> met alle tickets, geannuleerde inbegrepen, op Id gesorteerd |
| <span style="background:#1c7ed6;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>GET</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">tickets/<span style="color:#e64980"><b>{id}</b></span></code> | <code style="color:#37b24d;font-weight:600">200 OK</code> met het ticket, of <code style="color:#f08c00;font-weight:600">404 Not Found</code> zonder body als het niet bestaat of geannuleerd is |
| <span style="background:#2f9e44;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>POST</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">tickets</code> | <code style="color:#37b24d;font-weight:600">201 Created</code> met het nieuwe ticket en zijn adres, of <code style="color:#f08c00;font-weight:600">400 Bad Request</code> met de reden |
| <span style="background:#d9822b;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>PUT</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">tickets/<span style="color:#e64980"><b>{id}</b></span></code> | <code style="color:#37b24d;font-weight:600">204 No Content</code>, <code style="color:#f08c00;font-weight:600">404 Not Found</code> zonder body, of <code style="color:#f08c00;font-weight:600">400 Bad Request</code> met de reden |
| <span style="background:#e03131;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>DELETE</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">tickets/<span style="color:#e64980"><b>{id}</b></span></code> | <code style="color:#37b24d;font-weight:600">204 No Content</code> als het ticket geannuleerd is, of <code style="color:#f08c00;font-weight:600">404 Not Found</code> zonder body |
| <span style="background:#d9822b;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>PUT</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">tickets/herstel/<span style="color:#e64980"><b>{id}</b></span></code> | <code style="color:#37b24d;font-weight:600">204 No Content</code>, <code style="color:#f08c00;font-weight:600">404 Not Found</code> zonder body, of <code style="color:#f08c00;font-weight:600">400 Bad Request</code> met de reden |
| <span style="background:#1c7ed6;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>GET</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">tickets/opbrengst</code> | <code style="color:#37b24d;font-weight:600">200 OK</code> met een object met een property `opbrengst`, een `double` |
| <span style="background:#d9822b;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>PUT</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">tickets/verhuis/<span style="color:#e64980"><b>{id}</b></span>/<span style="color:#e64980"><b>{rij}</b></span>/<span style="color:#e64980"><b>{stoel}</b></span></code> | <code style="color:#37b24d;font-weight:600">204 No Content</code>, <code style="color:#f08c00;font-weight:600">404 Not Found</code> zonder body, of <code style="color:#f08c00;font-weight:600">400 Bad Request</code> met de reden |

De opbrengst is de som van de prijzen van alle niet-geannuleerde tickets. Ze komt niet meer als los getal, maar als klein object, zoals de schoolopgaven een berekende waarde teruggeven: `{ "opbrengst": 175 }`.

Bij een verkoop berekent je API zelf de Id: de hoogste Id in de tabel, plus één. Een Id die de client meestuurt, telt niet.

> [!WARNING]
> Je routes beginnen niet meer met `api/`, en ze staan in het meervoud. Met `[controller]` in je route krijg je `Ticket`, en niet `tickets`.

> [!TIP]
> Voor de opbrengst en voor de controle of een plaats bezet is, hoef je niet alle tickets naar je API te halen. Laat de database optellen en zoeken. Kijk in je terminal naar de SQL die er echt naartoe gaat.

Log in elk endpoint: elk verzoek op niveau `Information`, elke <code style="color:#f08c00;font-weight:600">404</code> en <code style="color:#f08c00;font-weight:600">400</code> op niveau `Warning`, bij een <code style="color:#f08c00;font-weight:600">400</code> met de reden, en een DELETE op een ticket dat niet bestaat op niveau `Error`.

Controleer daarna:

1. <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> tickets/opbrengst</code> geeft `{ "opbrengst": 175 }`.
2. Verkoop een ticket op naam van `Jonas Wouters`, op rij 3, stoel 13, voor 35. Je krijgt <code style="color:#37b24d;font-weight:600">201</code>, en het nieuwe ticket heeft Id 7: de plaats van het geannuleerde ticket 4 was vrij. De header `Location` eindigt op `/tickets/7`.
3. Annuleer ticket 5. Je krijgt <code style="color:#37b24d;font-weight:600">204</code>. De opbrengst is `185`.
4. Herstel ticket 4. Je krijgt <code style="color:#f08c00;font-weight:600">400</code> met <code style="color:#845ef7">Rij 3 stoel 13 is intussen aan iemand anders verkocht.</code>
5. Annuleer ticket 99. Je krijgt <code style="color:#f08c00;font-weight:600">404</code>, en in je terminal staat een `Error`.
6. Stop je API en start hem opnieuw. De opbrengst is nog altijd `185`, en <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> tickets/5</code> geeft <code style="color:#f08c00;font-weight:600">404</code>.

---

### 4. Dezelfde regels

Je database weigert een naam van meer dan 100 tekens. Voeg daarom aan de controles uit 03_06 deze toe, bij verkopen en wijzigen:

- de naam is langer dan 100 tekens: <code style="color:#f08c00;font-weight:600">400 Bad Request</code> met <code style="color:#845ef7">De naam mag hoogstens 100 tekens lang zijn.</code>

Test het met een naam van precies 100 tekens, die je wel moet kunnen verkopen, en een van 101 tekens, telkens met verder geldige gegevens en een vrije plaats. Annuleer het ticket van 100 tekens daarna weer.

---

### 5. Alles samen: een migratie terugdraaien

<span style="background:#7048e8;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>SAMENGESTELDE OEFENING</b></span>

Tot nu toe ging een migratie altijd één kant op: van je model naar de database. In dit laatste punt maak je een migratie, voer je ze uit, en draai je ze daarna volledig terug. Je database, je gegevens en je API moeten dat ongeschonden overleven.

#### De categorie erbij

De zaal wil per ticket een prijscategorie bewaren, zoals `parterre` of `balkon`.

1. Voeg aan het model een property `Categorie` toe, van het type `string`. In de database wordt het een verplichte tekst van hoogstens 20 tekens.
2. Maak een migratie met de naam `CategorieToegevoegd`. Open ze en zoek in `Up` en in `Down` wat er met de kolom gebeurt.
3. Werk de database bij. Controleer dat de kolom bestaat, welke waarde de bestaande tickets erin gekregen hebben, en dat ze er nog allemaal staan.

#### De categorie weer weg

Een week later beslist de zaal anders: de categorie volgt uit de rij, en hoeft dus niet apart bewaard te worden. Het veld moet weg, alsof het er nooit geweest is.

4. Breng de database terug naar de toestand van na `InitialCreate`.
5. Verwijder de migratie `CategorieToegevoegd` uit je project.
6. Haal de property `Categorie` weer uit je model.

> [!NOTE]
> Les 04 zegt dat je een migratie die de database al uitvoerde, niet zomaar mag verwijderen: je moet de database eerst terugrollen naar een eerdere versie. Hoe dat moet, toont de les niet. Zoek het op in de documentatie van Entity Framework Core.

> [!IMPORTANT]
> Pas de database niet met de hand aan, en verwijder het migratiebestand niet zelf uit je map Migrations. Houd de volgorde van stap 4, 5 en 6 aan. Op deze drie plaatsen mag geen spoor van de categorie overblijven: in de database, in je map Migrations en in je model.

#### Testscenario

Noteer voor je aan stap 1 begint wat <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> tickets/alles</code> en <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> tickets/opbrengst</code> teruggeven. Controleer na stap 6:

1. De geschiedenis van je migraties in de database bevat enkel nog `InitialCreate`.
2. De tabel `Tickets` heeft geen kolom `Categorie` meer, en bevat nog altijd dezelfde tickets als voor stap 1, ook ticket 7 uit punt 3.
3. In je map Migrations staat geen bestand meer met `Categorie` in de naam, en ook in het bestand met de momentopname van je model komt `Categorie` niet meer voor.
4. Je project compileert, en de opbrengst en de lijst met alle tickets geven precies hetzelfde antwoord als voor stap 1.
5. Maak een nieuwe migratie zonder iets aan je model te veranderen. Ze is leeg: `Up` en `Down` doen niets. Verwijder ze daarna weer.

> [!WARNING]
> Is de migratie uit stap 5 van het testscenario niet leeg, dan komen je model en de momentopname in je map Migrations niet overeen. Kijk dan na of je in stap 4 tot en met 6 een van de drie plaatsen vergeten bent.
