# 05_03

<b>Hoofdstuk 05</b> &nbsp;·&nbsp; Bestand of database, asynchroon &nbsp;·&nbsp; PC-onderdelen &nbsp;·&nbsp; niveau <span style="color:#f03e3e">●●●</span> &nbsp;·&nbsp; ±2,5 uur

## Leerdoel

Na deze oefening kan je één asynchroon contract laten volgen door twee repositories die op een totaal andere manier werken: één met een bestand en één met een database.

Je leert de database laten optellen, tellen en zoeken met `SumAsync`, `CountAsync`, `AnyAsync` en `AverageAsync`, en een filter met een drempel naar SQL laten vertalen.

Daarnaast bewijs je dat twee repositories tot op de cent hetzelfde antwoorden, ook als er niets meer te berekenen valt.

## 

## Opdracht

In 04_03 werkte je bestellijnen-API rechtstreeks met de database, en bleef je bestandsrepository ongebruikt staan. De boekhouding wil de databasecode achter een repository, maar wil de bestandsversie houden als reserve, voor als de database ooit niet bereikbaar is. Dan moet ze wel precies hetzelfde antwoorden.

Jouw taak is om één asynchroon contract op te stellen, een repository te schrijven die met de database werkt, je bestandsrepository mee asynchroon te maken, en te bewijzen dat beide hetzelfde antwoorden.

Via de API moet een medewerker, net als in 04_03:

1. de actieve en alle bestellijnen kunnen opvragen, en één bestellijn;
2. een bestellijn kunnen toevoegen, bijwerken, schrappen en herstellen;
3. de totale waarde en de bestelfiche kunnen opvragen;
4. de hele bestelling in één keer kunnen schrappen;

en daarnaast:

5. de actieve bestellijnen boven een bepaald bedrag kunnen opvragen;
6. de gemiddelde waarde van een actieve lijn kunnen opvragen.

Implementeer onderstaande functionaliteiten.

### 1. Je vertrekpunt

Vertrek van je eigen oplossing van 04_03. Je model en je database veranderen in deze oefening niet. Alle tien endpoints uit 04_03 moeten nog precies hetzelfde antwoorden.

---

### 2. Het contract

Stel in de map Repositories een asynchroon contract `IBestellijnRepository` op, met precies deze methodes:

| Methode | Parameters | Geeft terug |
|---------|------------|-------------|
| `GetActieveAsync` | geen | `Task<List<Bestellijn>>` |
| `GetAllesAsync` | geen | `Task<List<Bestellijn>>` |
| `GetByIdAsync` | `int id` | `Task<Bestellijn?>` |
| `CreateAsync` | `Bestellijn bestellijn` | `Task<Bestellijn>` |
| `UpdateAsync` | `int id, Bestellijn bestellijn` | `Task` |
| `SchrapAsync` | `int id` | `Task` |
| `HerstelAsync` | `int id` | `Task` |
| `SchrapAllesAsync` | geen | `Task` |
| `GetTotaalActiefAsync` | geen | `Task<double>` |
| `GetFicheAsync` | geen | `Task<BestelFiche>` |
| `GetBovenAsync` | `double bedrag` | `Task<List<Bestellijn>>` |
| `GetGemiddeldeLijnwaardeAsync` | geen | `Task<double>` |
| `HasAnyActiefAsync` | geen | `Task<bool>` |

Wat elke methode doet:

- `GetByIdAsync` geeft de lijn met die Id, ook als ze geschrapt is, of `null`. Of een geschrapte lijn een <code style="color:#f08c00;font-weight:600">404</code> geeft, beslist de controller.
- `UpdateAsync`, `SchrapAsync` en `HerstelAsync` doen niets als de lijn niet bestaat.
- De waarde van een lijn is haar aantal maal haar stukprijs. `GetTotaalActiefAsync` telt de waarde van de actieve lijnen op, `GetFicheAsync` geeft de fiche zoals in 04_03.
- `GetBovenAsync` geeft de actieve lijnen met een waarde die strikt hoger ligt dan het bedrag, op Id gesorteerd.
- `GetGemiddeldeLijnwaardeAsync` geeft het gemiddelde van de waarden van de actieve lijnen, of `0` als er geen actieve lijnen zijn.
- `HasAnyActiefAsync` geeft `true` als er minstens één actieve lijn is.

> [!IMPORTANT]
> Neem de namen, de parameters en de types letterlijk over. Pas je bestaande repositories en je controller daaraan aan.

> [!NOTE]
> Je `InMemoryBestellijnRepository` volgt dit contract niet meer. Zoals in de les mag hij weg.

---

### 3. De bestandsrepository asynchroon

Maak je `BestandBestellijnRepository` uit 03_03 asynchroon, zodat hij het nieuwe contract volgt. Hij leest en schrijft `bestellijnen.json` voortaan met de asynchrone methodes van .NET, en begint, als het bestand nog niet bestaat, met dezelfde zes lijnen als je startgegevens in de database.

> [!NOTE]
> Les 05 toont de asynchrone methodes om een bestand te lezen en te schrijven niet. Ze hebben dezelfde naam als de synchrone, met `Async` erachter. Zoek ze op in de documentatie van .NET.

---

### 4. De databaserepository

Maak een klasse `BestellijnRepository` die het contract volgt. Ze krijgt je `BestellijnContext` via haar constructor, bewaart die in een `readonly` veld, en gebruikt overal de asynchrone methodes van Entity Framework Core.

> [!IMPORTANT]
> Laat de database het rekenwerk doen. Het totaal en het gemiddelde berekent ze met `SumAsync` en `AverageAsync`, de tellingen in de fiche met `CountAsync`, en `HasAnyActiefAsync` met `AnyAsync`. Ook het filter van `GetBovenAsync` moet in de SQL staan, niet in je API.

> [!WARNING]
> `AverageAsync` geeft geen `0` als er niets te berekenen valt. Probeer het nadat je alle lijnen geschrapt hebt, en lees wat er misloopt. `SumAsync` heeft dat probleem niet.

> [!WARNING]
> Een som van `double`'s kan een staartje ver achter de komma krijgen, zoals in 04_03. Rond elke som van bedragen af op twee cijfers na de komma, in je beide repositories. Een gemiddelde rond je niet af.

Registreer met `AddScoped` welke repository je controller krijgt. Begin met de databaserepository.

---

### 5. De nieuwe endpoints

| Endpoint | Antwoord |
|----------|----------|
| <span style="background:#1c7ed6;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>GET</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/bestellijn/boven/<span style="color:#e64980"><b>{bedrag}</b></span></code> | <code style="color:#37b24d;font-weight:600">200 OK</code> met de actieve lijnen met een waarde boven dat bedrag, op Id gesorteerd |
| <span style="background:#1c7ed6;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>GET</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/bestellijn/gemiddelde</code> | <code style="color:#37b24d;font-weight:600">200 OK</code> met een object met een property `gemiddelde`, een `double` |

Vindt het eerste endpoint niets, dan geeft het `[]`. Zijn er geen actieve lijnen, dan geeft het tweede `{ "gemiddelde": 0 }`. Log elk verzoek op niveau Information.

> [!TIP]
> `{bedrag}` is een kommagetal. Probeer <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> api/bestellijn/boven/219.8</code>: komt lijn 2 erin, of net niet? Kijk naar het woord "strikt" in punt 2.

---

### 6. Alles samen: twee repositories, één antwoord

<span style="background:#7048e8;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>SAMENGESTELDE OEFENING</b></span>

Je hebt nu twee repositories die hetzelfde asynchrone contract volgen, maar elk op een totaal andere manier werken. In dit laatste punt doorloop je dezelfde reeks met allebei. Elk antwoord moet bij de twee hetzelfde zijn, tot op de Id's en tot op de cent.

#### Testscenario

Begin met de databaserepository en een database die exact de startgegevens bevat. Bedragen vergelijk je als getal: `0`, `0.0` en `0.00` zijn hetzelfde antwoord.

1. <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> api/bestellijn/fiche</code> geeft de fiche uit 04_03: 5 actieve en 1 geschrapte lijn, `totaalActief` `772.76`, `totaalGeschrapt` `89.95`, en lijn 1 als duurste.
2. Voeg een lijn toe met omschrijving `CPU-koeler`, aantal 1 en stukprijs 109.95. Je krijgt <code style="color:#37b24d;font-weight:600">201</code>, en de nieuwe lijn heeft Id 7 en is actief.
3. Schrap lijn 3. Je krijgt <code style="color:#37b24d;font-weight:600">204</code>.
4. <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> api/bestellijn/fiche</code> geeft als resultaat:

   ```json
   {
     "aantalActieveLijnen": 5,
     "aantalGeschrapteLijnen": 2,
     "totaalActief": 743.71,
     "totaalGeschrapt": 228.95,
     "duursteActieveLijn": {
       "id": 1,
       "omschrijving": "Moederbord ASUS B650",
       "aantal": 1,
       "stukPrijs": 249.99,
       "isActief": true
     },
     "isLeeg": false
   }
   ```

5. <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> api/bestellijn/boven/150</code> geeft de lijnen 1 en 2, in die volgorde. <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> api/bestellijn/gemiddelde</code> geeft een `gemiddelde` dat begint met `148.74`.
6. Stop je API en start hem opnieuw. De fiche is exact dezelfde als in stap 4.
7. Herstel lijn 3. Je krijgt <code style="color:#37b24d;font-weight:600">204</code>. Schrap daarna de hele bestelling in één keer. Je krijgt <code style="color:#37b24d;font-weight:600">200</code>, met een fiche waarin `aantalActieveLijnen` `0` is, `aantalGeschrapteLijnen` `7`, `totaalActief` `0`, `totaalGeschrapt` `972.66`, `duursteActieveLijn` `null` en `isLeeg` `true`.
8. <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> api/bestellijn/gemiddelde</code> geeft `{ "gemiddelde": 0 }`, en <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> api/bestellijn/boven/0</code> geeft `[]`. Geen enkel endpoint geeft een <code style="color:#f03e3e;font-weight:600">500</code>.
9. Schrap de hele bestelling nog eens. Je krijgt <code style="color:#f08c00;font-weight:600">400</code> met <code style="color:#845ef7">Er zijn geen actieve bestellijnen om te schrappen.</code>

Wissel daarna in Program.cs naar de bestandsrepository, verwijder `bestellijnen.json`, start je API opnieuw en doorloop dezelfde negen stappen. Elk antwoord moet precies hetzelfde zijn als bij de database, ook de Id 7 in stap 2.

> [!WARNING]
> Verschilt een totaal tussen je twee repositories op de laatste cijfers? Dan rondt één van de twee niet af, of rondt ze af op een andere plaats. Kijk waar elke repository het totaal berekent.

> [!NOTE]
> Wil je de reeks met de database herhalen, verwijder dan je database en werk ze opnieuw bij met je migratie. Zorg dat de teller daarna nog klopt, zoals in 04_03.
