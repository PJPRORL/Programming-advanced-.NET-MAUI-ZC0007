# 03_01

<b>Hoofdstuk 03</b> &nbsp;·&nbsp; Repository en dependency injection &nbsp;·&nbsp; PC-onderdelen &nbsp;·&nbsp; niveau <span style="color:#37b24d">●○○</span> &nbsp;·&nbsp; ±60 min

## Leerdoel

Na deze oefening kan je de data-logica uit een controller halen en onderbrengen in een repository, achter een interface.

Je leert de controller zijn repository laten vragen via de constructor in plaats van hem zelf aan te maken, en je legt in Program.cs vast welke klasse hij dan krijgt.

Daarnaast oefen je op refactoren: je herschrijft de binnenkant van je API grondig, maar voor wie je API gebruikt, verandert er niets.

## 

## Opdracht

De artikel-API van Bit & Byte uit 02_01 werkt, maar de ArtikelController doet alles zelf: hij houdt de lijst bij, doorzoekt ze en beslist welk antwoord de client krijgt. Later moet die lijst plaatsmaken voor een echte database, en de ontwikkelaars willen dan niet de hele controller opnieuw schrijven.

Jouw taak is om de ArtikelController te refactoren naar een repository, en er één nieuw endpoint aan toe te voegen.

Via de API moet de website, net als in 02_01:

1. de volledige voorraad kunnen opvragen;
2. één artikel kunnen opvragen op basis van zijn Id;
3. alle artikelen van één soort kunnen opvragen;
4. enkel de artikelen kunnen opvragen die nog op voorraad liggen;
5. de artikelfiche kunnen opvragen;

en daarnaast:

6. alle artikelen van één merk kunnen opvragen.

Implementeer onderstaande functionaliteiten.

### 1. Je vertrekpunt

Vertrek van je eigen oplossing van 02_01. Na deze oefening moeten de volgende endpoints nog precies hetzelfde antwoorden als vandaag:

| Verzoek | Antwoord |
|---------|----------|
| <span style="background:#1c7ed6;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>GET</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/artikel</code> | <code style="color:#37b24d;font-weight:600">200 OK</code> met de volledige lijst |
| <span style="background:#1c7ed6;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>GET</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/artikel/<span style="color:#e64980"><b>{id}</b></span></code> | <code style="color:#37b24d;font-weight:600">200 OK</code> met het artikel, of <code style="color:#f08c00;font-weight:600">404 Not Found</code> met <code style="color:#845ef7">We vonden geen artikel met Id <span style="color:#e64980"><b>{id}</b></span>.</code> |
| <span style="background:#1c7ed6;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>GET</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/artikel/soort/<span style="color:#e64980"><b>{soort}</b></span></code> | <code style="color:#37b24d;font-weight:600">200 OK</code> met de artikelen van die soort, of <code style="color:#f08c00;font-weight:600">404 Not Found</code> met <code style="color:#845ef7">We vonden geen artikelen van de soort <span style="color:#e64980"><b>{soort}</b></span>.</code> |
| <span style="background:#1c7ed6;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>GET</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/artikel/voorradig</code> | <code style="color:#37b24d;font-weight:600">200 OK</code> met de artikelen die op voorraad liggen, ook als dat een lege lijst is |
| <span style="background:#1c7ed6;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>GET</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/artikel/fiche/<span style="color:#e64980"><b>{id}</b></span></code> | <code style="color:#37b24d;font-weight:600">200 OK</code> met de `ArtikelFiche`, of <code style="color:#f08c00;font-weight:600">404 Not Found</code> met <code style="color:#845ef7">We vonden geen artikel met Id <span style="color:#e64980"><b>{id}</b></span>.</code> |

> [!TIP]
> Doe al deze verzoeken één keer vóór je begint, en noteer wat je terugkrijgt. Na je refactoring doe je exact dezelfde verzoeken opnieuw. Een refactoring is pas geslaagd als je geen enkel verschil ziet.

---

### 2. Het contract

Maak in je project een map Repositories, met daarin een interface `IArtikelRepository`.

De interface beschrijft wat je repository moet kunnen:

- alle artikelen teruggeven;
- één artikel teruggeven op basis van zijn Id, of niets als het niet bestaat;
- alle artikelen van één soort teruggeven;
- alle artikelen teruggeven die op voorraad liggen;
- alle artikelen van één merk teruggeven.

> [!NOTE]
> Een interface zegt enkel wát er kan, nooit hóé. Je schrijft er dus enkel de koppen van methodes in: naam, parameters en returntype.

---

### 3. De repository

Maak in dezelfde map een klasse `InMemoryArtikelRepository` die `IArtikelRepository` implementeert.

Verhuis je lijst met artikelen uit de controller naar deze klasse. Al het zoekwerk in die lijst gebeurt vanaf nu hier.

> [!IMPORTANT]
> Na deze stap staat er in je ArtikelController geen enkele lijst met artikelen meer. Alles wat de controller over artikelen wil weten, vraagt hij aan de repository.

---

### 4. De controller via dependency injection

Pas de ArtikelController aan, zodat hij een `IArtikelRepository` binnenkrijgt via zijn constructor en die bewaart in een privaat veld.

Leg in Program.cs vast welke klasse ASP.NET Core moet aanleveren wanneer een controller om een `IArtikelRepository` vraagt.

> [!WARNING]
> De controller maakt zijn repository niet zelf aan. Kom je in je controller nog ergens `new` tegen voor een repository, dan heb je de stap uit les 03 overgeslagen waarin de verantwoordelijkheid om de repository te maken buiten de klasse wordt gelegd.

---

### 5. Logging

Laat de controller ook een `ILogger<ArtikelController>` binnenkrijgen via zijn constructor.

Log in elk endpoint minstens één regel:

- elk binnenkomend verzoek op niveau Information;
- elke keer dat je een <code style="color:#f08c00;font-weight:600">404</code> teruggeeft, op niveau Warning.

Kijk in het terminalvenster van je API of de berichten verschijnen, met het juiste niveau ervoor.

> [!TIP]
> Een logger hoef je niet zelf te registreren in Program.cs. Die levert ASP.NET Core uit zichzelf.

---

### 6. Artikelen van één merk

Voorzie een GET-endpoint op:

<span style="background:#1c7ed6;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>GET</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/artikel/merk/<span style="color:#e64980"><b>{merk}</b></span></code>

Dit endpoint geeft alle artikelen van dat merk terug met statuscode <code style="color:#37b24d;font-weight:600">200 OK</code>.

Hoofdletters spelen geen rol: `asus`, `ASUS` en `Asus` moeten hetzelfde resultaat geven.

Zijn er geen artikelen van dat merk, geef dan statuscode <code style="color:#f08c00;font-weight:600">404 Not Found</code> met het bericht:

<code style="color:#845ef7">We vonden geen artikelen van het merk <span style="color:#e64980"><b>{merk}</b></span>.</code>

> [!NOTE]
> Het filteren zelf gebeurt in de repository. De controller beslist enkel welke statuscode bij het resultaat hoort.

Test je endpoint en controleer:

- of een merk uit je lijst, één keer in kleine letters en één keer in hoofdletters, twee keer dezelfde artikelen geeft;
- of een merk dat niet in je lijst staat, <code style="color:#f08c00;font-weight:600">404</code> geeft en een Warning in je log zet.

---

### 7. Alles samen: de artikelfiche via de repository

<span style="background:#7048e8;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>SAMENGESTELDE OEFENING</b></span>

In 02_01 stelde je controller de artikelfiche samen door zelf in zijn lijst te zoeken: naar het artikel, naar de andere artikelen van dezelfde soort, naar de goedkoopste. Die lijst heeft hij nu niet meer.

In dit laatste punt laat je de fiche opnieuw werken, met alles wat je in deze oefening hebt opgebouwd: de repository, de dependency injection en de logging.

<span style="background:#1c7ed6;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>GET</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/artikel/fiche/<span style="color:#e64980"><b>{id}</b></span></code>

De fiche bevat dezelfde vier gegevens als in 02_01, met dezelfde betekenis:

| Gegeven                 | Betekenis                                                  |
|-------------------------|------------------------------------------------------------|
| Artikel                 | het artikel zelf                                           |
| IsVoorradig             | ja als de voorraad groter is dan 0                         |
| AantalVanDezelfdeSoort  | het aantal andere artikelen van dezelfde soort             |
| IsGoedkoopsteVanDeSoort | ja als geen enkel ander artikel van die soort goedkoper is |

Bestaat het artikel niet, geef dan statuscode <code style="color:#f08c00;font-weight:600">404 Not Found</code> met het bericht <code style="color:#845ef7">We vonden geen artikel met Id <span style="color:#e64980"><b>{id}</b></span>.</code> en log een Warning.

> [!NOTE]
> Wie stelt de fiche samen: de repository of de controller? Lees nog eens hoe les 03 de taak van elk beschrijft, en beslis dan of je interface er iets bij nodig heeft, en zo ja wat.

Test je endpoint en controleer:

- of de fiches van drie artikelen dezelfde gegevens tonen als in 02_01;
- of `AantalVanDezelfdeSoort` op 0 staat bij een artikel dat als enige van zijn soort in de lijst zit;
- of bij twee artikelen van dezelfde soort met een verschillende prijs maar bij één van de twee `IsGoedkoopsteVanDeSoort` op ja staat.

> [!NOTE]
> Kijk wat er níet veranderd is: de routes, de statuscodes en de JSON. Enkel de binnenkant van je API is anders. Dat is precies wat refactoren betekent.
