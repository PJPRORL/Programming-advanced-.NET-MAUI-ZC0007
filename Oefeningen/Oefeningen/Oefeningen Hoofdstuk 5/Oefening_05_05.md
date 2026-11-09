# 05_05

<b>Hoofdstuk 05</b> &nbsp;·&nbsp; Twee asynchrone repositories en de cijfers, in schoolstijl &nbsp;·&nbsp; Fietsverhuur &nbsp;·&nbsp; niveau <span style="color:#f08c00">●●○</span> &nbsp;·&nbsp; ±2,5 uur

## Leerdoel

Na deze oefening kan je twee asynchrone repositories op één DbContext laten werken, elk achter een interface die je letterlijk volgt, met CRUD-methodes en query-methodes.

Je leert een Id in de repository berekenen, ook in een lege tabel, en de database laten filteren, sorteren, tellen en rekenen met de asynchrone methodes van Entity Framework Core.

Daarnaast maak je de tabel volledig leeg, en zie je of elke methode daar tegen kan.

## 

## Opdracht

In 04_05 bewaarde je fietsen-API alles in de database, met een controller die zelf met de context werkte. Trapdoor wil die code achter repositories, en wil aan de balie meteen kunnen zien hoeveel fietsen er klaarstaan, welke types er zijn, en wat een dag huren kost.

Jouw taak is om twee asynchrone repositories op één context te schrijven, je controller erop te laten overstappen, en de nieuwe query's als endpoints beschikbaar te maken.

Via de API moet de balie, net als in 04_05, fietsen beheren, verhuren, terugbrengen en de verhuringen nalezen. Daarnaast moet ze:

1. enkel de beschikbare fietsen kunnen opvragen, en ze kunnen tellen;
2. de lijst van alle types kunnen opvragen;
3. de fietsen kunnen sorteren op prijs per dag, oplopend of aflopend;
4. de gemiddelde, de laagste en de hoogste prijs per dag kunnen opvragen;
5. kunnen tellen hoe vaak één fiets in de verhuringen voorkomt.

Implementeer onderstaande functionaliteiten.

### 1. Je vertrekpunt

Vertrek van je eigen oplossing van 04_05. Je model en je database veranderen in deze oefening niet. Alle endpoints uit 04_05 moeten nog precies hetzelfde antwoorden.

---

### 2. De interfaces

Vervang je oude interfaces uit les 03 door deze twee, in de map Repositories.

`IFietsRepository`:

| Methode | Parameters | Geeft terug |
|---------|------------|-------------|
| `CreateAsync` | `Fiets fiets` | `Task<Fiets>` |
| `DeleteAsync` | `int id` | `Task` |
| `GetAllAsync` | geen | `Task<List<Fiets>>` |
| `GetByIdAsync` | `int id` | `Task<Fiets?>` |
| `UpdateAsync` | `int id, Fiets fiets` | `Task` |
| `SetBeschikbaarAsync` | `int id, bool isBeschikbaar` | `Task` |
| `GetBeschikbaarAsync` | geen | `Task<List<Fiets>>` |
| `GetTypesAsync` | geen | `Task<List<string>>` |
| `GetSortedByPrijsAsync` | `bool descending` | `Task<List<Fiets>>` |
| `CountBeschikbaarAsync` | geen | `Task<int>` |
| `GetGemiddeldePrijsAsync` | geen | `Task<double>` |
| `GetLaagstePrijsAsync` | geen | `Task<double>` |
| `GetHoogstePrijsAsync` | geen | `Task<double>` |

`IVerhuringRepository`:

| Methode | Parameters | Geeft terug |
|---------|------------|-------------|
| `CreateAsync` | `Verhuring verhuring` | `Task<Verhuring>` |
| `GetAllAsync` | geen | `Task<List<Verhuring>>` |
| `CountByFietsAsync` | `int fietsId` | `Task<int>` |

> [!IMPORTANT]
> Neem de namen, de parameters en de types letterlijk over. Op het examen krijg je de interfaces zo.

---

### 3. Twee repositories op één context

Maak de klassen `FietsRepository` en `VerhuringRepository`. Beide krijgen je `FietsverhuurContext` via hun constructor, bewaren die in een `readonly` veld, en gebruiken overal de asynchrone methodes van Entity Framework Core. Registreer ze met `AddScoped`.

Wat de methodes doen:

- `CreateAsync` negeert de Id uit de body, berekent ze zelf zoals in 04_05, de hoogste Id plus één of 1 in een lege tabel, en geeft het bewaarde item terug. Een nieuwe fiets is altijd beschikbaar.
- `UpdateAsync` overschrijft het type, de framemaat en de prijs per dag. De beschikbaarheid verandert enkel via `SetBeschikbaarAsync`, bij verhuren en terugbrengen.
- `UpdateAsync`, `DeleteAsync` en `SetBeschikbaarAsync` doen niets als de fiets niet bestaat.
- De lijsten van fietsen zijn op Id gesorteerd, de verhuringen de oudste eerst.
- `GetTypesAsync` geeft elk type één keer, alfabetisch gesorteerd. Gebruik `Select` en `Distinct`.
- `GetSortedByPrijsAsync` sorteert op prijs per dag: aflopend als `descending` `true` is, anders oplopend. Bij een gelijke prijs komt het kleinste Id eerst.
- De gemiddelde, de laagste en de hoogste prijs berekent de database, met `AverageAsync`, `MinAsync` en `MaxAsync`. Tellen doe je met `CountAsync`.

Elke methode die een getal berekent, geeft `0` als er niets te berekenen valt.

> [!WARNING]
> `AverageAsync`, `MinAsync` en `MaxAsync` geven geen `0` als ze niets vinden. In het testscenario maak je je tabel helemaal leeg. Zorg dat je methodes dan `0` teruggeven, en geen <code style="color:#f03e3e;font-weight:600">500</code> veroorzaken.

---

### 4. De controller op de repositories

Pas je `FietsController` aan: hij krijgt via zijn constructor een `IFietsRepository` en een `IVerhuringRepository` binnen in plaats van de context, naast zijn logger, en elk endpoint werkt asynchroon. De controles, de berichten en de logging blijven zoals in 04_05. De controller kijkt zelf of een fiets bestaat, en kiest de statuscode.

> [!WARNING]
> Krijg je bij een POST of een vervanging een <code style="color:#f03e3e;font-weight:600">500</code> met *No route matches the supplied values*, terwijl de fiets wel in je database staat? Lees in je leerboek, hoofdstuk 38, wat ASP.NET Core met het achtervoegsel `Async` in de naam van een controlleractie doet.

---

### 5. De nieuwe endpoints

| Endpoint | Antwoord |
|----------|----------|
| <span style="background:#1c7ed6;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>GET</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">fietsen/beschikbaar</code> | <code style="color:#37b24d;font-weight:600">200 OK</code> met de beschikbare fietsen |
| <span style="background:#1c7ed6;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>GET</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">fietsen/types</code> | <code style="color:#37b24d;font-weight:600">200 OK</code> met de lijst van alle types |
| <span style="background:#1c7ed6;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>GET</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">fietsen/sorteren/prijs?descending=true</code> | <code style="color:#37b24d;font-weight:600">200 OK</code> met alle fietsen op prijs per dag, aflopend |
| <span style="background:#1c7ed6;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>GET</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">fietsen/tellen/beschikbaar</code> | <code style="color:#37b24d;font-weight:600">200 OK</code> met een object met een property `count`, een `int` |
| <span style="background:#1c7ed6;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>GET</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">fietsen/prijs/gemiddelde</code> | <code style="color:#37b24d;font-weight:600">200 OK</code> met een object met een property `gemiddelde`, een `double` |
| <span style="background:#1c7ed6;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>GET</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">fietsen/prijs/grenzen</code> | <code style="color:#37b24d;font-weight:600">200 OK</code> met een object met de properties `laagste` en `hoogste`, twee `double`'s |
| <span style="background:#1c7ed6;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>GET</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">fietsen/<span style="color:#e64980"><b>{id}</b></span>/verhuringen/tellen</code> | <code style="color:#37b24d;font-weight:600">200 OK</code> met een object met een property `count`, een `int` |

`descending` is een parameter uit de querystring. Ontbreekt hij, of is hij `false`, dan sorteer je oplopend. Elk endpoint antwoordt met <code style="color:#37b24d;font-weight:600">200 OK</code>, ook als het niets vindt, ook voor een fiets die niet bestaat. Log elk verzoek op niveau Information.

---

### 6. Alles samen: van startgegevens tot lege tabel

<span style="background:#7048e8;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>SAMENGESTELDE OEFENING</b></span>

De repositories, de controller en de nieuwe endpoints komen samen in één reeks. Ze begint bij de startgegevens, verhuurt en brengt terug, en eindigt met een lege tabel. Juist daar blijkt of elke methode haar `0` teruggeeft.

#### Testscenario

Begin met een database die exact de startgegevens bevat: verwijder ze, en werk ze opnieuw bij met je migraties. Bedragen vergelijk je als getal.

1. <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> fietsen/beschikbaar</code> geeft de fietsen 1, 2 en 4, en <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> fietsen/tellen/beschikbaar</code> geeft `{ "count": 3 }`.
2. <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> fietsen/types</code> geeft `["bakfiets", "e-bike", "mountainbike", "stadsfiets"]`.
3. <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> fietsen/prijs/gemiddelde</code> geeft `{ "gemiddelde": 21.375 }`, en <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> fietsen/prijs/grenzen</code> geeft `{ "laagste": 12.5, "hoogste": 30 }`.
4. Voeg een fiets toe van het type `stadsfiets`, framemaat 50, voor 12.5 per dag. Je krijgt <code style="color:#37b24d;font-weight:600">201</code>, met Id 5.
5. <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> fietsen/types</code> geeft nog altijd vier types. <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> fietsen/sorteren/prijs</code> geeft de fietsen 1, 5, 2, 3 en 4, en met `?descending=true` krijg je 4, 3, 2, 1 en 5.
6. Verhuur fiets 5, breng ze terug, en verhuur ze opnieuw. Je krijgt telkens <code style="color:#37b24d;font-weight:600">204</code>. <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> fietsen/5/verhuringen/tellen</code> geeft `{ "count": 3 }`, en <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> fietsen/99/verhuringen/tellen</code> geeft `{ "count": 0 }`.
7. Breng fiets 3 terug. Je krijgt <code style="color:#37b24d;font-weight:600">204</code>. <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> fietsen/tellen/beschikbaar</code> geeft `{ "count": 4 }`: alle fietsen behalve fiets 5.
8. Werk fiets 99 bij, en verwijder fiets 99. Je krijgt telkens <code style="color:#f08c00;font-weight:600">404</code>. In je terminal staan een `Warning` en een `Error`.
9. Breng fiets 5 terug, en verwijder daarna de fietsen 1 tot en met 5, één voor één. Je krijgt telkens <code style="color:#37b24d;font-weight:600">204</code>.
10. Met een lege tabel geeft <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> fietsen</code> `[]`, het gemiddelde `0`, de grenzen `0` en `0`, de types `[]`, en de telling van de beschikbare fietsen `0`. Geen enkel endpoint geeft een <code style="color:#f03e3e;font-weight:600">500</code>.
11. Voeg de fiets uit stap 4 opnieuw toe. Ze krijgt Id 1.
12. <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> fietsen/verhuringen</code> geeft vijf verhuringen, met Id 1 tot en met 5: `verhuurd`, `teruggebracht` en `verhuurd` voor fiets 5, dan `teruggebracht` voor fiets 3, en als laatste `teruggebracht` voor fiets 5.

> [!NOTE]
> De nieuwe fiets uit stap 11 krijgt Id 1, het nummer van een fiets die je net verwijderde. Had die oude fiets verhuringen gehad, dan zou <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> fietsen/1/verhuringen/tellen</code> ze nu bij de nieuwe fiets tellen. Dat is een gevolg van "de hoogste Id plus één", en van een tabel zonder relatie. Les 06 lost het tweede op.
