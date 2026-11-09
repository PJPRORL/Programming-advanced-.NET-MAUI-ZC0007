# 05_06

<b>Hoofdstuk 05</b> &nbsp;·&nbsp; Bestand of database, asynchroon, in schoolstijl &nbsp;·&nbsp; Concertzaal &nbsp;·&nbsp; niveau <span style="color:#f03e3e">●●●</span> &nbsp;·&nbsp; ±2,5 uur

## Leerdoel

Na deze oefening kan je één asynchroon contract laten volgen door twee repositories die op een totaal andere manier werken: één met een bestand en één met een database.

Je leert de database laten optellen, tellen, zoeken en sorteren met `SumAsync`, `CountAsync`, `AnyAsync` en `AverageAsync`, en een Id berekenen die in een bestand en in een database hetzelfde uitkomt.

Daarnaast bewijs je dat twee repositories precies hetzelfde antwoorden, ook bij de plaatscontrole.

## 

## Opdracht

In 04_06 werkte je ticket-API rechtstreeks met de database, en bleef je bestandsrepository ongebruikt staan. De Notenbalk wil de databasecode achter een repository, maar wil de bestandsversie houden als reserve. Dan moet ze wel precies hetzelfde antwoorden, ook bij de vraag of een plaats nog vrij is.

Jouw taak is om één asynchroon contract op te stellen, een repository te schrijven die met de database werkt, je bestandsrepository mee asynchroon te maken, en te bewijzen dat beide hetzelfde antwoorden.

Via de API moet een medewerker, net als in 04_06:

1. de geldige en alle tickets kunnen opvragen, en één ticket;
2. een ticket kunnen verkopen, wijzigen, annuleren en herstellen;
3. de opbrengst van de avond kunnen opvragen;
4. een ticket kunnen verhuizen naar een andere plaats;

en daarnaast:

5. de geldige tickets van één rij kunnen opvragen en tellen;
6. de gemiddelde prijs van een geldig ticket kunnen opvragen;
7. de geldige tickets kunnen sorteren op prijs.

Implementeer onderstaande functionaliteiten.

### 1. Je vertrekpunt

Vertrek van je eigen oplossing van 04_06. Je model en je database veranderen in deze oefening niet. Alle endpoints uit 04_06 moeten nog precies hetzelfde antwoorden.

---

### 2. De interface

Stel in de map Repositories een asynchroon contract `ITicketRepository` op, met precies deze methodes:

| Methode | Parameters | Geeft terug |
|---------|------------|-------------|
| `GetGeldigeAsync` | geen | `Task<List<Ticket>>` |
| `GetAllesAsync` | geen | `Task<List<Ticket>>` |
| `GetByIdAsync` | `int id` | `Task<Ticket?>` |
| `CreateAsync` | `Ticket ticket` | `Task<Ticket>` |
| `UpdateAsync` | `int id, Ticket ticket` | `Task` |
| `AnnuleerAsync` | `int id` | `Task` |
| `HerstelAsync` | `int id` | `Task` |
| `VerhuisAsync` | `int id, int rij, int stoel` | `Task` |
| `IsPlaatsBezetAsync` | `int rij, int stoel, int behalveId` | `Task<bool>` |
| `GetOpbrengstAsync` | geen | `Task<double>` |
| `GetByRijAsync` | `int rij` | `Task<List<Ticket>>` |
| `CountByRijAsync` | `int rij` | `Task<int>` |
| `GetGemiddeldePrijsAsync` | geen | `Task<double>` |
| `GetSortedByPrijsAsync` | `bool descending` | `Task<List<Ticket>>` |

Wat elke methode doet:

- `GetByIdAsync` geeft het ticket met die Id, ook als het geannuleerd is, of `null`. Of een geannuleerd ticket een <code style="color:#f08c00;font-weight:600">404</code> geeft, beslist de controller.
- `CreateAsync` negeert de Id uit de body en berekent ze zelf, zoals in 04_06. Een nieuw ticket is nooit geannuleerd.
- `UpdateAsync`, `AnnuleerAsync`, `HerstelAsync` en `VerhuisAsync` doen niets als het ticket niet bestaat.
- `IsPlaatsBezetAsync` geeft `true` als er op die rij en die stoel een geldig ticket zit, met een andere Id dan `behalveId`. Bij een nieuw ticket geef je `0` mee.
- `GetOpbrengstAsync` telt de prijzen van de geldige tickets op. `GetGemiddeldePrijsAsync` geeft hun gemiddelde, of `0` als er geen geldige tickets zijn.
- `GetByRijAsync` geeft de geldige tickets van die rij, op stoel gesorteerd. `CountByRijAsync` telt ze.
- `GetSortedByPrijsAsync` geeft de geldige tickets op prijs: aflopend als `descending` `true` is, anders oplopend. Bij een gelijke prijs komt het kleinste Id eerst.
- De andere lijsten zijn op Id gesorteerd.

> [!IMPORTANT]
> Neem de namen, de parameters en de types letterlijk over. Pas je bestaande repositories en je controller daaraan aan.

> [!TIP]
> Met `behalveId` kan je controller dezelfde methode gebruiken bij een verkoop, bij een wijziging die op dezelfde plaats blijft, bij een herstel en bij een verhuizing. Denk na welke Id hij in elk van die gevallen meegeeft.

---

### 3. De bestandsrepository asynchroon

Maak je bestandsrepository uit 03_06 asynchroon, zodat ze het nieuwe contract volgt. Ze leest en schrijft `tickets.json` met de asynchrone methodes van .NET, en begint, als het bestand nog niet bestaat, met dezelfde zes tickets als je startgegevens in de database.

> [!NOTE]
> Les 05 toont de asynchrone methodes om een bestand te lezen en te schrijven niet. Ze hebben dezelfde naam als de synchrone, met `Async` erachter. Zoek ze op in de documentatie van .NET.

---

### 4. De databaserepository

Maak een klasse `TicketRepository` die het contract volgt. Ze krijgt je `ConcertzaalContext` via haar constructor, bewaart die in een `readonly` veld, en gebruikt overal de asynchrone methodes van Entity Framework Core.

> [!IMPORTANT]
> Laat de database het werk doen: de opbrengst met `SumAsync`, het gemiddelde met `AverageAsync`, de telling met `CountAsync`, en de plaatscontrole met `AnyAsync`. Je haalt nooit alle tickets op om ze in je API te overlopen.

> [!WARNING]
> `AverageAsync` geeft geen `0` als er niets te berekenen valt. Zorg dat je methode dan `0` teruggeeft, in je beide repositories.

Registreer met `AddScoped` welke repository je controller krijgt. Begin met de databaserepository.

> [!WARNING]
> Krijg je bij een verkoop een <code style="color:#f03e3e;font-weight:600">500</code> met *No route matches the supplied values*, terwijl het ticket wel bewaard is? Lees in je leerboek, hoofdstuk 38, wat ASP.NET Core met het achtervoegsel `Async` in de naam van een controlleractie doet.

---

### 5. De nieuwe endpoints

| Endpoint | Antwoord |
|----------|----------|
| <span style="background:#1c7ed6;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>GET</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">tickets/rij/<span style="color:#e64980"><b>{rij}</b></span></code> | <code style="color:#37b24d;font-weight:600">200 OK</code> met de geldige tickets van die rij, op stoel gesorteerd |
| <span style="background:#1c7ed6;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>GET</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">tickets/tellen/rij/<span style="color:#e64980"><b>{rij}</b></span></code> | <code style="color:#37b24d;font-weight:600">200 OK</code> met een object met een property `count`, een `int` |
| <span style="background:#1c7ed6;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>GET</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">tickets/prijs/gemiddelde</code> | <code style="color:#37b24d;font-weight:600">200 OK</code> met een object met een property `gemiddelde`, een `double` |
| <span style="background:#1c7ed6;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>GET</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">tickets/sorteren/prijs?descending=true</code> | <code style="color:#37b24d;font-weight:600">200 OK</code> met de geldige tickets op prijs, aflopend |

Een lege rij geeft `[]` en `{ "count": 0 }`, met <code style="color:#37b24d;font-weight:600">200 OK</code>. `descending` komt uit de querystring; ontbreekt hij, of is hij `false`, dan sorteer je oplopend. Log elk verzoek op niveau Information.

---

### 6. Alles samen: twee repositories, één antwoord

<span style="background:#7048e8;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>SAMENGESTELDE OEFENING</b></span>

Je hebt nu twee repositories die hetzelfde asynchrone contract volgen, maar elk op een totaal andere manier werken. In dit laatste punt doorloop je dezelfde reeks met allebei. Elk antwoord moet bij de twee hetzelfde zijn, tot op de Id's.

#### Testscenario

Begin met de databaserepository en een database die exact de startgegevens bevat. Bedragen vergelijk je als getal.

1. <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> tickets/opbrengst</code> geeft `{ "opbrengst": 175 }`, en <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> tickets/prijs/gemiddelde</code> geeft `{ "gemiddelde": 35 }`.
2. Verkoop dit ticket:

   ```json
   { "naam": "Jonas Wouters", "rij": 3, "stoel": 13, "prijs": 35 }
   ```

   Je krijgt <code style="color:#37b24d;font-weight:600">201</code>, en het nieuwe ticket heeft Id 7 en is niet geannuleerd.
3. Annuleer ticket 5. Je krijgt <code style="color:#37b24d;font-weight:600">204</code>.
4. De opbrengst is `185`. <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> tickets/rij/3</code> geeft de tickets 3 en 7, in die volgorde, en <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> tickets/tellen/rij/7</code> geeft `{ "count": 1 }`.
5. Stop je API en start hem opnieuw. De opbrengst is nog altijd `185`.
6. Herstel ticket 4. Je krijgt <code style="color:#f08c00;font-weight:600">400</code> met <code style="color:#845ef7">Rij 3 stoel 13 is intussen aan iemand anders verkocht.</code>
7. Herstel ticket 5. Je krijgt <code style="color:#37b24d;font-weight:600">204</code>, en de opbrengst is `210`.
8. Verhuis ticket 7 naar rij 7, stoel 3. Je krijgt <code style="color:#f08c00;font-weight:600">400</code> met <code style="color:#845ef7">Rij 7 stoel 3 is al verkocht.</code>
9. Verhuis ticket 7 naar rij 9, stoel 1. Je krijgt <code style="color:#37b24d;font-weight:600">204</code>. De opbrengst is nog altijd `210`, rij 3 bevat enkel nog ticket 3, en <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> tickets/tellen/rij/4</code> geeft `{ "count": 0 }`.
10. <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> tickets/sorteren/prijs?descending=true</code> geeft de tickets 1, 2, 3, 7, 5 en 6, in die volgorde. Zonder parameter krijg je 5, 6, 3, 7, 1 en 2.
11. <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> tickets/prijs/gemiddelde</code> geeft `{ "gemiddelde": 35 }`.
12. Wijzig ticket 3 met dezelfde gegevens, maar een prijs van 40. Je krijgt <code style="color:#37b24d;font-weight:600">204</code>: het ticket blijft op zijn eigen plaats, en die telt niet als bezet.
13. Annuleer ticket 99. Je krijgt <code style="color:#f08c00;font-weight:600">404</code>, en in je terminal staat een `Error`.

Wissel daarna in Program.cs naar de bestandsrepository, verwijder `tickets.json`, start je API opnieuw en doorloop dezelfde dertien stappen. Elk antwoord moet precies hetzelfde zijn als bij de database, ook de Id 7 in stap 2.

> [!WARNING]
> Geeft stap 12 een <code style="color:#f08c00;font-weight:600">400</code> met <code style="color:#845ef7">Rij 3 stoel 12 is al verkocht.</code>? Dan telt je plaatscontrole het ticket zelf mee. Kijk welke `behalveId` je controller meegeeft.

> [!NOTE]
> Wil je de reeks met de database herhalen, verwijder dan je database en werk ze opnieuw bij met je migratie.
