# 05_07

<b>Hoofdstuk 05</b> &nbsp;·&nbsp; Een asynchrone repository met query's &nbsp;·&nbsp; Herstelplaats &nbsp;·&nbsp; examenstijl &nbsp;·&nbsp; niveau <span style="color:#f08c00">●●○</span> &nbsp;·&nbsp; ±2,5 uur

## Leerdoel

Na deze oefening kan je een asynchrone repository schrijven die een gegeven interface letterlijk volgt, met CRUD-methodes en met query-methodes die filteren, sorteren, tellen, controleren en rekenen.

Je leert de asynchrone methodes van Entity Framework Core inzetten waarvoor ze bedoeld zijn: `AnyAsync`, `CountAsync`, `SumAsync`, `AverageAsync`, `MinAsync` en `MaxAsync`, en een projectie maken met `Select` en `Distinct`.

Daarnaast merk je wat elk van die methodes doet met een lege tabel, en lees je een parameter uit de querystring.

## 

## Opdracht

De herstelplaats draait. De zaakvoerder wil nu cijfers: hoeveel herstellingen er klaarstaan, wat het werk gemiddeld kost, welke toestellen er binnenkomen. En de controller mag zijn gegevens niet langer zelf uit de context halen: daar komt een repository voor.

Jouw taak is om een repository te schrijven die een gegeven interface volgt, je controller erop te laten overstappen, en de nieuwe query's als endpoints beschikbaar te maken.

Via de API moet een medewerker, net als in 04_07, de herstellingen kunnen opvragen, toevoegen, bijwerken en verwijderen. Daarnaast moet hij:

1. de herstellingen kunnen sorteren op kostprijs, oplopend of aflopend;
2. de herstellingen boven een bedrag kunnen opvragen;
3. de lijst van alle toestellen kunnen opvragen;
4. de omzet, de gemiddelde kostprijs en de laagste en de hoogste kostprijs kunnen opvragen;
5. per status kunnen tellen, nagaan of ze voorkomt, en het gemiddelde opvragen.

Implementeer onderstaande functionaliteiten.

### 1. Je vertrekpunt

Vertrek van je eigen oplossing van 04_07. Je model en je database veranderen in deze oefening niet.

Begin met een database die exact de startgegevens uit 04_07 bevat: verwijder ze, en werk ze opnieuw bij met je migratie.

| Id | KlantNaam | Toestel    | Status         | Kostprijs |
|----|-----------|------------|----------------|-----------|
| 1  | Peeters   | Laptop     | In behandeling | 189.50    |
| 2  | Janssens  | Desktop    | Ontvangen      | 75.00     |
| 3  | Maes      | Laptop     | Klaar          | 129.75    |
| 4  | Claes     | Printer    | Afgehaald      | 45.25     |
| 5  | Wouters   | Smartphone | Klaar          | 59.50     |
| 6  | Willems   | Desktop    | In behandeling | 35.50     |

---

### 2. De interface

Maak een map Repositories, met daarin een interface `IHerstellingRepository`, in de namespace `WebApi.Repositories`. Ze bevat precies deze methodes.

De CRUD-methodes:

| Methode | Parameters | Geeft terug |
|---------|------------|-------------|
| `CreateAsync` | `Herstelling herstelling` | `Task<Herstelling>` |
| `DeleteAsync` | `int id` | `Task` |
| `GetAllAsync` | geen | `Task<List<Herstelling>>` |
| `GetByIdAsync` | `int id` | `Task<Herstelling?>` |
| `UpdateAsync` | `int id, Herstelling herstelling` | `Task` |

De query-methodes:

| Methode | Parameters | Geeft terug |
|---------|------------|-------------|
| `GetByStatusAsync` | `string status` | `Task<List<Herstelling>>` |
| `GetSortedByKostprijsAsync` | `bool descending` | `Task<List<Herstelling>>` |
| `GetDuurderDanAsync` | `double bedrag` | `Task<List<Herstelling>>` |
| `GetToestellenAsync` | geen | `Task<List<string>>` |
| `GetOmzetAsync` | geen | `Task<double>` |
| `GetGemiddeldeKostprijsAsync` | geen | `Task<double>` |
| `GetGemiddeldeKostprijsByStatusAsync` | `string status` | `Task<double>` |
| `GetLaagsteKostprijsAsync` | geen | `Task<double>` |
| `GetHoogsteKostprijsAsync` | geen | `Task<double>` |
| `CountByStatusAsync` | `string status` | `Task<int>` |
| `HasAnyWithStatusAsync` | `string status` | `Task<bool>` |

> [!IMPORTANT]
> Neem de namen, de parameters en de types letterlijk over. Op het examen krijg je de interface op deze manier, en de testen roepen de methodes van je repository rechtstreeks aan.

---

### 3. De repository

Maak in de map Repositories een klasse `HerstellingRepository` die `IHerstellingRepository` implementeert.

De repository:

- krijgt je `HerstelplaatsContext` via zijn constructor, en bewaart die in een `readonly` veld;
- gebruikt overal de asynchrone methodes van Entity Framework Core;
- geeft elke asynchrone methode het achtervoegsel `Async`;
- doet niets bij `UpdateAsync` en `DeleteAsync` als de herstelling niet bestaat;
- negeert bij `CreateAsync` de Id uit de body, en berekent ze zelf zoals in 04_07: de hoogste Id plus één, of 1 in een lege tabel.

Wat elke methode teruggeeft:

| Methode | Wat ze teruggeeft |
|---------|-------------------|
| `GetAllAsync` | alle herstellingen, gesorteerd op `KlantNaam`, alfabetisch |
| `GetByIdAsync` | de herstelling met die Id, of `null` |
| `GetByStatusAsync` | de herstellingen met die status, gesorteerd op `Kostprijs`, oplopend |
| `GetSortedByKostprijsAsync` | alle herstellingen op `Kostprijs`: aflopend als `descending` `true` is, anders oplopend |
| `GetDuurderDanAsync` | de herstellingen met een `Kostprijs` die strikt hoger ligt dan het bedrag, op Id gesorteerd |
| `GetToestellenAsync` | elk toestel één keer, alfabetisch gesorteerd; gebruik `Select` en `Distinct` |
| `GetOmzetAsync` | de som van alle kostprijzen, met `SumAsync` |
| `GetGemiddeldeKostprijsAsync` | het gemiddelde van alle kostprijzen, met `AverageAsync` |
| `GetGemiddeldeKostprijsByStatusAsync` | het gemiddelde van de kostprijzen met die status, met `AverageAsync` |
| `GetLaagsteKostprijsAsync` | de laagste kostprijs, met `MinAsync` |
| `GetHoogsteKostprijsAsync` | de hoogste kostprijs, met `MaxAsync` |
| `CountByStatusAsync` | het aantal herstellingen met die status, met `CountAsync` |
| `HasAnyWithStatusAsync` | `true` als er minstens één herstelling met die status is, anders `false`, met `AnyAsync` |

Elke vergelijking op de status negeert hoofdletters, zoals in 04_07. Een lijst zonder resultaten is een lege lijst. Elke methode die een getal berekent, geeft `0` als er niets te berekenen valt.

Registreer je repository in Program.cs met `AddScoped`, zodat de controller een `HerstellingRepository` krijgt wanneer hij om een `IHerstellingRepository` vraagt.

> [!WARNING]
> Van `SumAsync`, `AverageAsync`, `MinAsync` en `MaxAsync` geeft er één bij een lege verzameling gewoon `0`. De andere drie doen dat niet. Probeer het met een status die niet bestaat, en lees wat er misloopt. Zorg dat elke methode in dat geval `0` teruggeeft.

> [!TIP]
> Let op de volgorde waarin je dubbels weghaalt en sorteert. Komen je toestellen niet alfabetisch terug, kijk dan in de SQL in je terminal of er nog een `ORDER BY` in staat, en waar.

---

### 4. De controller op de repository

Pas je `HerstellingController` aan: hij krijgt voortaan een `IHerstellingRepository` binnen in plaats van de context, en elk endpoint werkt asynchroon.

Alle endpoints uit 04_07 blijven bestaan, met dezelfde statuscodes en dezelfde logging. Twee antwoorden veranderen wel van volgorde, omdat de repository het zo vraagt:

- <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> herstellingen</code> is nu gesorteerd op klantnaam;
- <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> herstellingen/status/{status}</code> is nu gesorteerd op kostprijs.

De controller kijkt zelf of een herstelling bestaat voor hij ze bijwerkt of verwijdert. De repository doet in dat geval immers niets, en zegt dat ook niet.

> [!NOTE]
> Je `TechniekerController` mag rechtstreeks met de context blijven werken. Enkel de herstellingen krijgen een repository.

> [!WARNING]
> Krijg je bij een POST een <code style="color:#f03e3e;font-weight:600">500</code> met *No route matches the supplied values*, terwijl de herstelling wel in je database staat? Lees in je leerboek, hoofdstuk 38, wat ASP.NET Core met het achtervoegsel `Async` in de naam van een controlleractie doet.

---

### 5. Sorteren, filteren en projecteren

| Endpoint | Antwoord |
|----------|----------|
| <span style="background:#1c7ed6;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>GET</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">herstellingen/sorteren/kostprijs</code> | <code style="color:#37b24d;font-weight:600">200 OK</code> met alle herstellingen op kostprijs, oplopend |
| <span style="background:#1c7ed6;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>GET</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">herstellingen/sorteren/kostprijs?descending=true</code> | <code style="color:#37b24d;font-weight:600">200 OK</code> met alle herstellingen op kostprijs, aflopend |
| <span style="background:#1c7ed6;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>GET</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">herstellingen/duurder/<span style="color:#e64980"><b>{bedrag}</b></span></code> | <code style="color:#37b24d;font-weight:600">200 OK</code> met de herstellingen die meer kosten dan het bedrag |
| <span style="background:#1c7ed6;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>GET</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">herstellingen/toestellen</code> | <code style="color:#37b24d;font-weight:600">200 OK</code> met de lijst van alle toestellen |

`descending` is een parameter uit de querystring. Ontbreekt hij, of is hij `false`, dan sorteer je oplopend.

<code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> herstellingen/toestellen</code> geeft als resultaat:

```json
["Desktop", "Laptop", "Printer", "Smartphone"]
```

> [!TIP]
> Een routeparameter komt uit de route. `descending` staat achter het vraagteken. Zoek op met welk attribuut je ASP.NET Core zegt waar een parameter vandaan komt, en wat je parameter krijgt als de client hem weglaat.

---

### 6. De cijfers

Elk van deze endpoints geeft een klein JSON-object terug, met <code style="color:#37b24d;font-weight:600">200 OK</code>, ook als er niets gevonden wordt. Dan staat er `0` of `false` in.

| Endpoint | Antwoord |
|----------|----------|
| <span style="background:#1c7ed6;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>GET</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">herstellingen/omzet</code> | een object met een property `omzet`, een `double` |
| <span style="background:#1c7ed6;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>GET</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">herstellingen/gemiddelde</code> | een object met een property `gemiddelde`, een `double` |
| <span style="background:#1c7ed6;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>GET</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">herstellingen/gemiddelde/status/<span style="color:#e64980"><b>{status}</b></span></code> | een object met een property `gemiddelde`, een `double` |
| <span style="background:#1c7ed6;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>GET</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">herstellingen/grenzen</code> | een object met de properties `laagste` en `hoogste`, twee `double`'s |
| <span style="background:#1c7ed6;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>GET</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">herstellingen/tellen/status/<span style="color:#e64980"><b>{status}</b></span></code> | een object met een property `count`, een `int` |
| <span style="background:#1c7ed6;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>GET</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">herstellingen/exists/status/<span style="color:#e64980"><b>{status}</b></span></code> | een object met een property `exists`, een `bool` |

<code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> herstellingen/grenzen</code> geeft als resultaat:

```json
{
  "laagste": 35.5,
  "hoogste": 189.5
}
```

Elk nieuw endpoint schrijft bij elk verzoek een logbericht van niveau `Information`, zoals de endpoints uit 04_07.

> [!NOTE]
> Neem de namen van de properties letterlijk over. In de schoolopgaven zijn ze soms Engels, zoals `totalRevenue`, `count` en `exists`. De test zoekt de naam die in de opgave staat.

> [!NOTE]
> Rond hier niets af. Een gemiddelde geef je terug zoals `AverageAsync` het berekent. De testen bij de schoolopgaven controleren een gemiddelde tussen twee grenzen, zoals 28,45 en 28,46. Een gemiddelde dat je zelf afrondt, kan daar net buiten vallen.

---

### 7. Alles samen: van startgegevens tot lege tabel

<span style="background:#7048e8;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>SAMENGESTELDE OEFENING</b></span>

De repository, de controller en de nieuwe endpoints komen samen in één reeks. Ze begint bij de startgegevens, voegt toe, werkt bij, en eindigt met een lege tabel. Juist daar blijkt of elke methode haar `0` teruggeeft.

#### Testscenario

Begin met een database die exact de startgegevens bevat, en doorloop deze reeks. Bedragen vergelijk je als getal.

1. <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> herstellingen</code> geeft de herstellingen 4, 2, 3, 1, 6 en 5, in die volgorde.
2. <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> herstellingen/sorteren/kostprijs</code> geeft 6, 4, 5, 2, 3 en 1. Met `?descending=true` krijg je 1, 3, 2, 5, 4 en 6. Met `?descending=false` krijg je weer de eerste volgorde.
3. <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> herstellingen/status/KLAAR</code> geeft 5 en 3, in die volgorde.
4. <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> herstellingen/duurder/100</code> geeft 1 en 3. <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> herstellingen/duurder/189.5</code> geeft `[]`.
5. <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> herstellingen/omzet</code> geeft `{ "omzet": 534.5 }`.
6. <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> herstellingen/gemiddelde</code> geeft een gemiddelde dat begint met `89.083`. <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> herstellingen/gemiddelde/status/klaar</code> geeft `94.625`. <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> herstellingen/gemiddelde/status/geannuleerd</code> geeft `0`, en geen <code style="color:#f03e3e;font-weight:600">500</code>.
7. <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> herstellingen/tellen/status/in behandeling</code> geeft `{ "count": 2 }`. <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> herstellingen/exists/status/Afgehaald</code> geeft `{ "exists": true }`, en met de status `geannuleerd` `{ "exists": false }`.
8. Voeg deze herstelling toe:

   ```json
   { "id": 0, "klantNaam": "Hermans", "toestel": "Tablet", "probleem": "Scherm reageert niet", "status": "Ontvangen", "kostprijs": 64.5 }
   ```

   Je krijgt <code style="color:#37b24d;font-weight:600">201</code>, met Id 7, en een header `Location` die eindigt op `/herstellingen/7`. De omzet is nu `599`, het gemiddelde begint met `85.571`, en `Tablet` staat achteraan in de lijst van toestellen.
9. Werk herstelling 2 bij:

   ```json
   { "klantNaam": "Janssens", "toestel": "Desktop", "probleem": "Start niet op", "status": "Klaar", "kostprijs": 80.25 }
   ```

   Je krijgt <code style="color:#37b24d;font-weight:600">204</code>. <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> herstellingen/tellen/status/klaar</code> geeft `3`, en de omzet is `604.25`.
10. Werk herstelling 99 bij met de body uit stap 9, en verwijder herstelling 99. Je krijgt telkens <code style="color:#f08c00;font-weight:600">404</code>. In je terminal staan een `Warning` en een `Error`.
11. <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> herstellingen</code> geeft nu 4, 7, 2, 3, 1, 6 en 5.
12. Verwijder de herstellingen 1 tot en met 7, één voor één. Je krijgt telkens <code style="color:#37b24d;font-weight:600">204</code>.
13. Met een lege tabel geeft <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> herstellingen</code> `[]`, de omzet `0`, het gemiddelde `0`, de grenzen `0` en `0`, de toestellen `[]`, het aantal met status `klaar` `0`, en `exists` `false`. Geen enkel endpoint geeft een <code style="color:#f03e3e;font-weight:600">500</code>.
14. Voeg de herstelling uit stap 8 opnieuw toe. Ze krijgt Id 1.

> [!NOTE]
> Wil je de reeks herhalen, verwijder dan je database en werk ze opnieuw bij met je migratie. Je startgegevens komen dan terug. Verwijderen via de API haalt ze enkel uit je tabel, niet uit je migratie.
