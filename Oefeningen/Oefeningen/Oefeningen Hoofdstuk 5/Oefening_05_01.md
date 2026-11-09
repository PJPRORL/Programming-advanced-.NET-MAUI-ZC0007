# 05_01

<b>Hoofdstuk 05</b> &nbsp;·&nbsp; Een asynchrone repository op de database &nbsp;·&nbsp; PC-onderdelen &nbsp;·&nbsp; niveau <span style="color:#37b24d">●○○</span> &nbsp;·&nbsp; ±2 uur

## Leerdoel

Na deze oefening kan je een repository schrijven die via Entity Framework Core met de database praat, de hele keten asynchroon maken, en je controller er weer op laten steunen.

Je leert een interface letterlijk volgen zoals de schoolopgaven ze geven, met het achtervoegsel `Async`, en de asynchrone methodes van Entity Framework Core gebruiken waarvoor ze bedoeld zijn.

Daarnaast lees je een parameter uit de querystring, en geef je een telling en een ja-of-nee terug als klein JSON-object.

## 

## Opdracht

In 04_01 stapte je artikel-API over naar de database, met een controller die rechtstreeks met de DbContext werkte. Dat werkt, maar nu zit alle databasecode in je controller. Bit & Byte wil die code weer achter een interface, zoals in les 03, en wil daarnaast een paar extra vragen kunnen stellen over het assortiment.

Jouw taak is om een asynchrone repository te schrijven die met de database werkt, je controller erop te laten overstappen zonder dat de website iets merkt, en vier nieuwe endpoints toe te voegen.

Via de API moet de website, net als in 04_01, de voorraad opvragen, filteren en de fiche tonen. Daarnaast moet ze:

1. de lijst van alle merken kunnen opvragen;
2. de artikelen kunnen sorteren op prijs, oplopend of aflopend;
3. kunnen tellen hoeveel artikelen er van één soort zijn;
4. kunnen nagaan of een merk in het assortiment zit.

Implementeer onderstaande functionaliteiten.

### 1. Je vertrekpunt

Vertrek van je eigen oplossing van 04_01. Je database `ArtikelDb` bevat de zes artikelen met hun garantie. Je model en je database veranderen in deze oefening niet.

Alle endpoints uit 04_01 moeten na deze oefening nog precies hetzelfde antwoorden: dezelfde routes, dezelfde statuscodes, dezelfde berichten en dezelfde JSON.

---

### 2. Het contract

Vervang je oude `IArtikelRepository` uit les 03 door een asynchrone versie. Ze bevat precies deze methodes:

| Methode | Parameters | Geeft terug |
|---------|------------|-------------|
| `GetAllAsync` | geen | `Task<List<Artikel>>` |
| `GetByIdAsync` | `int id` | `Task<Artikel?>` |
| `GetBySoortAsync` | `string soort` | `Task<List<Artikel>>` |
| `GetVoorradigAsync` | geen | `Task<List<Artikel>>` |
| `GetByMerkAsync` | `string merk` | `Task<List<Artikel>>` |
| `GetMerkenAsync` | geen | `Task<List<string>>` |
| `GetSortedByPrijsAsync` | `bool descending` | `Task<List<Artikel>>` |
| `CountBySoortAsync` | `string soort` | `Task<int>` |
| `HasAnyVanMerkAsync` | `string merk` | `Task<bool>` |

> [!IMPORTANT]
> Neem de namen, de parameters en de types letterlijk over. Op het examen krijg je de interface zo, en de testen roepen je methodes rechtstreeks aan.

> [!NOTE]
> Je `InMemoryArtikelRepository` volgt dit contract niet meer. Zoals in de les mag hij weg.

---

### 3. De repository op de database

Maak in de map Repositories een klasse `ArtikelRepository` die `IArtikelRepository` implementeert.

De repository krijgt je `ArtikelContext` via zijn constructor, en bewaart die in een `readonly` veld. Hij gebruikt overal de asynchrone methodes van Entity Framework Core.

Wat elke methode teruggeeft:

| Methode | Wat ze teruggeeft |
|---------|-------------------|
| `GetAllAsync` | alle artikelen, op Id gesorteerd |
| `GetByIdAsync` | het artikel met die Id, of `null` |
| `GetBySoortAsync` | de artikelen van die soort, zonder op hoofdletters te letten, op Id gesorteerd; een lege lijst als er geen zijn |
| `GetVoorradigAsync` | de artikelen met minstens één stuk op voorraad, op Id gesorteerd |
| `GetByMerkAsync` | de artikelen van dat merk, zonder op hoofdletters te letten, op Id gesorteerd |
| `GetMerkenAsync` | elk merk één keer, alfabetisch gesorteerd; gebruik `Select` en `Distinct` |
| `GetSortedByPrijsAsync` | alle artikelen op prijs: aflopend als `descending` `true` is, anders oplopend |
| `CountBySoortAsync` | het aantal artikelen van die soort, met `CountAsync`, zonder op hoofdletters te letten |
| `HasAnyVanMerkAsync` | `true` als er minstens één artikel van dat merk is, anders `false`, met `AnyAsync` |

Registreer je repository in Program.cs met `AddScoped`, zodat je controller een `ArtikelRepository` krijgt wanneer hij om een `IArtikelRepository` vraagt.

> [!TIP]
> Let op de volgorde waarin je dubbels weghaalt en sorteert. Komen je merken niet alfabetisch terug, kijk dan in de SQL in je terminal of er nog een `ORDER BY` in staat, en waar.

---

### 4. De controller op de repository

Pas je `ArtikelController` aan: hij krijgt via zijn constructor weer een `IArtikelRepository` binnen in plaats van de context, en elk endpoint werkt asynchroon en wacht op de repository.

Beslissingen blijven in de controller: welke statuscode, welk bericht, en hoe de fiche eruitziet. De repository haalt enkel gegevens op.

> [!IMPORTANT]
> Async geldt van de database tot in de controller. Gebruik nergens `.Result` of `.Wait()` om een asynchrone methode toch synchroon te maken, en nergens `async void`.

---

### 5. De nieuwe endpoints

| Endpoint | Antwoord |
|----------|----------|
| <span style="background:#1c7ed6;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>GET</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/artikel/merken</code> | <code style="color:#37b24d;font-weight:600">200 OK</code> met de lijst van alle merken |
| <span style="background:#1c7ed6;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>GET</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/artikel/sorteren/prijs</code> | <code style="color:#37b24d;font-weight:600">200 OK</code> met alle artikelen op prijs, oplopend |
| <span style="background:#1c7ed6;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>GET</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/artikel/sorteren/prijs?descending=true</code> | <code style="color:#37b24d;font-weight:600">200 OK</code> met alle artikelen op prijs, aflopend |
| <span style="background:#1c7ed6;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>GET</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/artikel/tellen/soort/<span style="color:#e64980"><b>{soort}</b></span></code> | <code style="color:#37b24d;font-weight:600">200 OK</code> met een object met een property `count`, een `int` |
| <span style="background:#1c7ed6;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>GET</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/artikel/exists/merk/<span style="color:#e64980"><b>{merk}</b></span></code> | <code style="color:#37b24d;font-weight:600">200 OK</code> met een object met een property `exists`, een `bool` |

`descending` is een parameter uit de querystring. Ontbreekt hij, of is hij `false`, dan sorteer je oplopend. Een soort zonder artikelen geeft `{ "count": 0 }`, en een onbekend merk `{ "exists": false }`, allebei met <code style="color:#37b24d;font-weight:600">200 OK</code>.

> [!TIP]
> Een routeparameter komt uit de route. `descending` staat achter het vraagteken. Zoek op met welk attribuut je ASP.NET Core zegt waar een parameter vandaan komt, en wat je parameter krijgt als de client hem weglaat.

> [!NOTE]
> Een telling of een ja-of-nee als klein object teruggeven, zoals `{ "count": 2 }`, is de vorm die de schoolopgaven gebruiken. Neem de naam van de property altijd letterlijk over uit de opgave.

Log elk nieuw verzoek op niveau Information, zoals de andere endpoints.

---

### 6. Alles samen: dezelfde antwoorden, en meer

<span style="background:#7048e8;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>SAMENGESTELDE OEFENING</b></span>

De vorige punten vervangen de hele binnenkant van je API: een repository, een asynchroon contract, een asynchrone controller. Voor de website mag daarvan niets te merken zijn, behalve dat er vier endpoints bij zijn.

#### Testscenario

Doe de volgende verzoeken. Elk antwoord moet letter voor letter kloppen.

| Verzoek | Statuscode | Antwoord |
|---------|------------|----------|
| <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> api/artikel</code> | <code style="color:#37b24d;font-weight:600">200</code> | de zes artikelen, op Id gesorteerd |
| <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> api/artikel/99</code> | <code style="color:#f08c00;font-weight:600">404</code> | <code style="color:#845ef7">We vonden geen artikel met Id 99.</code> |
| <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> api/artikel/soort/processor</code> | <code style="color:#37b24d;font-weight:600">200</code> | de artikelen met Id 3 en 4 |
| <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> api/artikel/soort/koeler</code> | <code style="color:#f08c00;font-weight:600">404</code> | <code style="color:#845ef7">We vonden geen artikelen van de soort koeler.</code> |
| <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> api/artikel/voorradig</code> | <code style="color:#37b24d;font-weight:600">200</code> | de artikelen met Id 1, 2, 3 en 5 |
| <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> api/artikel/merk/corsair</code> | <code style="color:#f08c00;font-weight:600">404</code> | <code style="color:#845ef7">We vonden geen artikelen van het merk corsair.</code> |
| <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> api/artikel/merken</code> | <code style="color:#37b24d;font-weight:600">200</code> | `["AMD", "Asus", "MSI"]` |
| <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> api/artikel/sorteren/prijs</code> | <code style="color:#37b24d;font-weight:600">200</code> | de artikelen 2, 3, 1, 6, 4 en 5, in die volgorde |
| <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> api/artikel/sorteren/prijs?descending=true</code> | <code style="color:#37b24d;font-weight:600">200</code> | de artikelen 5, 4, 6, 1, 3 en 2, in die volgorde |
| <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> api/artikel/sorteren/prijs?descending=false</code> | <code style="color:#37b24d;font-weight:600">200</code> | dezelfde volgorde als zonder parameter |
| <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> api/artikel/tellen/soort/VIDEOKAART</code> | <code style="color:#37b24d;font-weight:600">200</code> | `{ "count": 2 }` |
| <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> api/artikel/tellen/soort/koeler</code> | <code style="color:#37b24d;font-weight:600">200</code> | `{ "count": 0 }` |
| <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> api/artikel/exists/merk/msi</code> | <code style="color:#37b24d;font-weight:600">200</code> | `{ "exists": true }` |
| <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> api/artikel/exists/merk/corsair</code> | <code style="color:#37b24d;font-weight:600">200</code> | `{ "exists": false }` |

<code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> api/artikel/fiche/1</code> geeft `isVoorradig` `true`, `aantalVanDezelfdeSoort` `1` en `isGoedkoopsteVanDeSoort` `false`, met artikel 1 zoals in 04_01.

Controleer tot slot in het terminalvenster van je API:

- of je logberichten er nog staan, met dezelfde niveaus als in 04_01;
- of je bij de merken één query ziet met `DISTINCT` en `ORDER BY`, en bij het tellen een `COUNT` in de SQL, in plaats van een query die alle artikelen ophaalt.

> [!NOTE]
> Vergelijk het met 04_01: de controller veranderde omdat hij nu een asynchrone repository krijgt. Dat de gegevens uit een database komen, weet hij niet meer.
