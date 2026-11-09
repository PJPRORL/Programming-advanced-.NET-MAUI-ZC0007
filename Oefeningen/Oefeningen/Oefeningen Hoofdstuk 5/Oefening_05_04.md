# 05_04

<b>Hoofdstuk 05</b> &nbsp;·&nbsp; Een asynchrone repository met query's, in schoolstijl &nbsp;·&nbsp; Dierenasiel &nbsp;·&nbsp; niveau <span style="color:#37b24d">●○○</span> &nbsp;·&nbsp; ±2 uur

## Leerdoel

Na deze oefening kan je een asynchrone repository schrijven die een gegeven interface letterlijk volgt, en je controller er weer op laten steunen.

Je leert filteren, sorteren, tellen en rekenen met de asynchrone methodes van Entity Framework Core, een lijst zonder dubbels maken met `Select` en `Distinct`, en een parameter uit de querystring lezen.

Daarnaast merk je wat `AverageAsync`, `MinAsync` en `MaxAsync` doen als ze niets vinden.

## 

## Opdracht

In 04_04 stapte je dieren-API over naar de database, in de stijl van de schoolopgaven, met een controller die rechtstreeks met de context werkte. Knuffelhof wil die code achter een repository, en wil daarnaast een paar cijfers over de dieren op de website tonen.

Jouw taak is om een asynchrone repository te schrijven die een gegeven interface volgt, je controller erop te laten overstappen, en de nieuwe query's als endpoints beschikbaar te maken.

Via de API moet de website, net als in 04_04, de dieren opvragen, filteren, zoeken en de fiche tonen. Daarnaast moet ze:

1. de lijst van alle soorten kunnen opvragen;
2. de dieren kunnen sorteren op leeftijd, oplopend of aflopend;
3. per soort kunnen tellen en nagaan of ze voorkomt;
4. de gemiddelde leeftijd kunnen opvragen, in het hele asiel en per soort;
5. de leeftijd van het jongste en het oudste dier kunnen opvragen.

Implementeer onderstaande functionaliteiten.

### 1. Je vertrekpunt

Vertrek van je eigen oplossing van 04_04. Je database `AsielDb` bevat de zes dieren, met hun chip. Je model en je database veranderen in deze oefening niet.

Alle endpoints uit 04_04 moeten na deze oefening nog precies hetzelfde antwoorden.

---

### 2. De interface

Vervang je oude `IDierRepository` uit les 03 door deze interface, in de map Repositories:

| Methode | Parameters | Geeft terug |
|---------|------------|-------------|
| `GetAllAsync` | geen | `Task<List<Dier>>` |
| `GetByIdAsync` | `int id` | `Task<Dier?>` |
| `GetBySoortAsync` | `string soort` | `Task<List<Dier>>` |
| `GetJongAsync` | geen | `Task<List<Dier>>` |
| `ZoekAsync` | `string tekst` | `Task<List<Dier>>` |
| `GetSoortenAsync` | geen | `Task<List<string>>` |
| `GetSortedByLeeftijdAsync` | `bool descending` | `Task<List<Dier>>` |
| `CountBySoortAsync` | `string soort` | `Task<int>` |
| `HasAnyWithSoortAsync` | `string soort` | `Task<bool>` |
| `GetGemiddeldeLeeftijdAsync` | geen | `Task<double>` |
| `GetGemiddeldeLeeftijdBySoortAsync` | `string soort` | `Task<double>` |
| `GetJongsteLeeftijdAsync` | geen | `Task<int>` |
| `GetOudsteLeeftijdAsync` | geen | `Task<int>` |

> [!IMPORTANT]
> Neem de namen, de parameters en de types letterlijk over. Op het examen krijg je de interface zo, en de testen roepen de methodes van je repository rechtstreeks aan.

---

### 3. De repository

Maak in de map Repositories een klasse `DierRepository` die `IDierRepository` implementeert.

De repository:

- krijgt je `AsielContext` via zijn constructor, en bewaart die in een `readonly` veld;
- gebruikt overal de asynchrone methodes van Entity Framework Core;
- vergelijkt de soort en de zoektekst zonder op hoofdletters te letten.

| Methode | Wat ze teruggeeft |
|---------|-------------------|
| `GetAllAsync` | alle dieren, op Id gesorteerd |
| `GetByIdAsync` | het dier met die Id, of `null` |
| `GetBySoortAsync` | de dieren van die soort, op Id gesorteerd; een lege lijst als er geen zijn |
| `GetJongAsync` | de dieren jonger dan twee jaar, op Id gesorteerd |
| `ZoekAsync` | de dieren waarvan de naam de tekst bevat, op Id gesorteerd |
| `GetSoortenAsync` | elke soort één keer, alfabetisch gesorteerd; gebruik `Select` en `Distinct` |
| `GetSortedByLeeftijdAsync` | alle dieren op leeftijd: aflopend als `descending` `true` is, anders oplopend; bij een gelijke leeftijd het kleinste Id eerst |
| `CountBySoortAsync` | het aantal dieren van die soort, met `CountAsync` |
| `HasAnyWithSoortAsync` | `true` als er minstens één dier van die soort is, anders `false`, met `AnyAsync` |
| `GetGemiddeldeLeeftijdAsync` | de gemiddelde leeftijd van alle dieren, met `AverageAsync` |
| `GetGemiddeldeLeeftijdBySoortAsync` | de gemiddelde leeftijd van de dieren van die soort, met `AverageAsync` |
| `GetJongsteLeeftijdAsync` | de laagste leeftijd, met `MinAsync` |
| `GetOudsteLeeftijdAsync` | de hoogste leeftijd, met `MaxAsync` |

Elke methode die een getal berekent, geeft `0` als er niets te berekenen valt.

Registreer je repository in Program.cs met `AddScoped`.

> [!WARNING]
> `AverageAsync`, `MinAsync` en `MaxAsync` geven geen `0` als ze niets vinden. Probeer `GetGemiddeldeLeeftijdBySoortAsync` met een soort die niet bestaat, en lees wat er misloopt. Zorg dat je methode dan `0` teruggeeft.

> [!WARNING]
> Een database belooft geen volgorde bij twee dieren met dezelfde leeftijd. Zonder tweede sorteersleutel kan Tommie vandaag voor Bella staan, en morgen erna.

> [!TIP]
> Let op de volgorde waarin je dubbels weghaalt en sorteert. Komen je soorten niet alfabetisch terug, kijk dan in de SQL in je terminal of er nog een `ORDER BY` in staat, en waar.

---

### 4. De controller op de repository

Pas je `DierController` aan: hij krijgt een `IDierRepository` binnen in plaats van de context, naast zijn logger, en elk endpoint werkt asynchroon.

> [!IMPORTANT]
> Async geldt van de database tot in de controller. Gebruik nergens `.Result` of `.Wait()`, en nergens `async void`.

---

### 5. De nieuwe endpoints

| Endpoint | Antwoord |
|----------|----------|
| <span style="background:#1c7ed6;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>GET</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">dieren/soorten</code> | <code style="color:#37b24d;font-weight:600">200 OK</code> met de lijst van alle soorten |
| <span style="background:#1c7ed6;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>GET</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">dieren/sorteren/leeftijd?descending=true</code> | <code style="color:#37b24d;font-weight:600">200 OK</code> met alle dieren op leeftijd, aflopend |
| <span style="background:#1c7ed6;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>GET</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">dieren/tellen/soort/<span style="color:#e64980"><b>{soort}</b></span></code> | <code style="color:#37b24d;font-weight:600">200 OK</code> met een object met een property `count`, een `int` |
| <span style="background:#1c7ed6;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>GET</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">dieren/exists/soort/<span style="color:#e64980"><b>{soort}</b></span></code> | <code style="color:#37b24d;font-weight:600">200 OK</code> met een object met een property `exists`, een `bool` |
| <span style="background:#1c7ed6;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>GET</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">dieren/leeftijd/gemiddelde</code> | <code style="color:#37b24d;font-weight:600">200 OK</code> met een object met een property `gemiddelde`, een `double` |
| <span style="background:#1c7ed6;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>GET</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">dieren/leeftijd/gemiddelde/soort/<span style="color:#e64980"><b>{soort}</b></span></code> | <code style="color:#37b24d;font-weight:600">200 OK</code> met een object met een property `gemiddelde`, een `double` |
| <span style="background:#1c7ed6;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>GET</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">dieren/leeftijd/grenzen</code> | <code style="color:#37b24d;font-weight:600">200 OK</code> met een object met de properties `jongste` en `oudste`, twee `int`'s |

`descending` is een parameter uit de querystring. Ontbreekt hij, of is hij `false`, dan sorteer je oplopend. Elk endpoint antwoordt met <code style="color:#37b24d;font-weight:600">200 OK</code>, ook als het niets vindt: dan staat er `0` of `false` in het object. Log elk verzoek op niveau Information.

> [!TIP]
> Een routeparameter komt uit de route. `descending` staat achter het vraagteken. Zoek op met welk attribuut je ASP.NET Core zegt waar een parameter vandaan komt, en wat je parameter krijgt als de client hem weglaat.

> [!NOTE]
> Neem de namen van de properties letterlijk over. In de schoolopgaven zijn ze soms Engels, zoals `count` en `exists`. De test zoekt de naam die in de opgave staat.

---

### 6. Alles samen: dezelfde antwoorden, en de cijfers

<span style="background:#7048e8;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>SAMENGESTELDE OEFENING</b></span>

De vorige punten vervangen de binnenkant van je API en voegen zeven endpoints toe. Voor de website mag er aan de oude endpoints niets veranderen.

#### Testscenario

Doe de volgende verzoeken. Elk antwoord moet letter voor letter kloppen.

| Verzoek | Statuscode | Antwoord |
|---------|------------|----------|
| <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> dieren</code> | <code style="color:#37b24d;font-weight:600">200</code> | de zes dieren, op Id gesorteerd |
| <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> dieren/99</code> | <code style="color:#f08c00;font-weight:600">404</code> | geen bericht van jou |
| <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> dieren/soort/kat</code> | <code style="color:#37b24d;font-weight:600">200</code> | de dieren met Id 1 en 2 |
| <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> dieren/zoek/xyz</code> | <code style="color:#37b24d;font-weight:600">200</code> | `[]` |
| <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> dieren/soorten</code> | <code style="color:#37b24d;font-weight:600">200</code> | `["hond", "kat", "konijn", "parkiet"]` |
| <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> dieren/sorteren/leeftijd</code> | <code style="color:#37b24d;font-weight:600">200</code> | de dieren 2, 4, 5, 1, 6 en 3, in die volgorde |
| <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> dieren/sorteren/leeftijd?descending=true</code> | <code style="color:#37b24d;font-weight:600">200</code> | de dieren 3, 6, 1, 5, 2 en 4, in die volgorde |
| <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> dieren/tellen/soort/HOND</code> | <code style="color:#37b24d;font-weight:600">200</code> | `{ "count": 2 }` |
| <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> dieren/tellen/soort/cavia</code> | <code style="color:#37b24d;font-weight:600">200</code> | `{ "count": 0 }` |
| <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> dieren/exists/soort/konijn</code> | <code style="color:#37b24d;font-weight:600">200</code> | `{ "exists": true }` |
| <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> dieren/exists/soort/cavia</code> | <code style="color:#37b24d;font-weight:600">200</code> | `{ "exists": false }` |
| <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> dieren/leeftijd/gemiddelde</code> | <code style="color:#37b24d;font-weight:600">200</code> | `{ "gemiddelde": 3 }` |
| <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> dieren/leeftijd/gemiddelde/soort/hond</code> | <code style="color:#37b24d;font-weight:600">200</code> | `{ "gemiddelde": 4 }` |
| <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> dieren/leeftijd/gemiddelde/soort/cavia</code> | <code style="color:#37b24d;font-weight:600">200</code> | `{ "gemiddelde": 0 }`, en geen <code style="color:#f03e3e;font-weight:600">500</code> |
| <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> dieren/leeftijd/grenzen</code> | <code style="color:#37b24d;font-weight:600">200</code> | `{ "jongste": 1, "oudste": 7 }` |

<code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> dieren/fiche/2</code> geeft `isJong` `true`, `aantalVanDezelfdeSoort` `1` en `isOudsteVanDeSoort` `false`.

Controleer tot slot in het terminalvenster van je API:

- of je bij het tellen een `COUNT` in de SQL ziet, bij `exists` een `EXISTS`, en bij het gemiddelde een `AVG`, in plaats van een query die alle dieren ophaalt;
- of je logberichten er nog staan, met dezelfde niveaus als in 04_04.

> [!NOTE]
> Het gemiddelde van gehele getallen is een `double`. Een gemiddelde van 3 schrijft je API als `3`, niet als `3.0`: een `double` toont geen nullen achter de komma.
