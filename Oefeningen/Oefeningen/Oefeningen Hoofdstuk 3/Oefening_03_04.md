# 03_04

<b>Hoofdstuk 03</b> &nbsp;·&nbsp; Repository en dependency injection &nbsp;·&nbsp; Dierenasiel &nbsp;·&nbsp; niveau <span style="color:#37b24d">●○○</span> &nbsp;·&nbsp; ±60 min

## Leerdoel

Na deze oefening kan je de data-logica uit een controller halen en onderbrengen in een repository, achter een interface.

Je leert de controller zijn repository laten vragen via de constructor in plaats van hem zelf aan te maken, en je legt in Program.cs vast welke klasse hij dan krijgt.

Daarnaast oefen je op refactoren: je herschrijft de binnenkant van je API grondig, maar voor wie je API gebruikt, verandert er niets.

## 

## Opdracht

De dieren-API van Knuffelhof uit 02_04 werkt, maar de DierController doet alles zelf: hij houdt de lijst bij, doorzoekt ze en beslist welk antwoord de website krijgt. Het asiel wil de lijst later in een database bewaren, en dan niet de hele controller opnieuw moeten schrijven.

Jouw taak is om de DierController te refactoren naar een repository, en er één nieuw endpoint aan toe te voegen.

Via de API moet de website, net als in 02_04:

1. alle dieren kunnen opvragen;
2. één dier kunnen opvragen op basis van zijn Id;
3. alle dieren van één soort kunnen opvragen;
4. enkel de jonge dieren kunnen opvragen;
5. de dierfiche kunnen opvragen;

en daarnaast:

6. dieren kunnen zoeken op een stukje van hun naam.

Implementeer onderstaande functionaliteiten.

### 1. Je vertrekpunt

Vertrek van je eigen oplossing van 02_04. Na deze oefening moeten de volgende endpoints nog precies hetzelfde antwoorden als vandaag:

| Verzoek | Antwoord |
|---------|----------|
| <span style="background:#1c7ed6;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>GET</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/dier</code> | <code style="color:#37b24d;font-weight:600">200 OK</code> met de volledige lijst |
| <span style="background:#1c7ed6;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>GET</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/dier/<span style="color:#e64980"><b>{id}</b></span></code> | <code style="color:#37b24d;font-weight:600">200 OK</code> met het dier, of <code style="color:#f08c00;font-weight:600">404 Not Found</code> met <code style="color:#845ef7">We vonden geen dier met Id <span style="color:#e64980"><b>{id}</b></span>.</code> |
| <span style="background:#1c7ed6;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>GET</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/dier/soort/<span style="color:#e64980"><b>{soort}</b></span></code> | <code style="color:#37b24d;font-weight:600">200 OK</code> met de dieren van die soort, of <code style="color:#f08c00;font-weight:600">404 Not Found</code> met <code style="color:#845ef7">We hebben op dit moment geen <span style="color:#e64980"><b>{soort}</b></span> in het asiel.</code> |
| <span style="background:#1c7ed6;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>GET</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/dier/jong</code> | <code style="color:#37b24d;font-weight:600">200 OK</code> met de dieren jonger dan twee jaar, ook als dat een lege lijst is |
| <span style="background:#1c7ed6;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>GET</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/dier/fiche/<span style="color:#e64980"><b>{id}</b></span></code> | <code style="color:#37b24d;font-weight:600">200 OK</code> met de `DierFiche`, of <code style="color:#f08c00;font-weight:600">404 Not Found</code> met <code style="color:#845ef7">We vonden geen dier met Id <span style="color:#e64980"><b>{id}</b></span>.</code> |

> [!TIP]
> Doe al deze verzoeken één keer vóór je begint, en noteer wat je terugkrijgt. Na je refactoring doe je exact dezelfde verzoeken opnieuw. Een refactoring is pas geslaagd als je geen enkel verschil ziet.

---

### 2. Het contract

Maak in je project een map Repositories, met daarin een interface `IDierRepository`.

De interface beschrijft wat je repository moet kunnen:

- alle dieren teruggeven;
- één dier teruggeven op basis van zijn Id, of niets als het niet bestaat;
- alle dieren van één soort teruggeven;
- alle jonge dieren teruggeven;
- alle dieren teruggeven waarvan de naam een bepaald stukje tekst bevat.

> [!NOTE]
> Een interface zegt enkel wát er kan, nooit hóé. Je schrijft er dus enkel de koppen van methodes in: naam, parameters en returntype.

---

### 3. De repository

Maak in dezelfde map een klasse `InMemoryDierRepository` die `IDierRepository` implementeert.

Verhuis je lijst met dieren uit de controller naar deze klasse. Al het zoekwerk in die lijst gebeurt vanaf nu hier.

> [!IMPORTANT]
> Na deze stap staat er in je DierController geen enkele lijst met dieren meer. Alles wat de controller over dieren wil weten, vraagt hij aan de repository.

---

### 4. De controller via dependency injection

Pas de DierController aan, zodat hij een `IDierRepository` binnenkrijgt via zijn constructor en die bewaart in een privaat veld.

Leg in Program.cs vast welke klasse ASP.NET Core moet aanleveren wanneer een controller om een `IDierRepository` vraagt.

> [!WARNING]
> De controller maakt zijn repository niet zelf aan. Kom je in je controller nog ergens `new` tegen voor een repository, dan heb je de stap uit les 03 overgeslagen waarin de verantwoordelijkheid om de repository te maken buiten de klasse wordt gelegd.

---

### 5. Logging

Laat de controller ook een `ILogger<DierController>` binnenkrijgen via zijn constructor.

Log in elk endpoint minstens één regel:

- elk binnenkomend verzoek op niveau Information;
- elke keer dat je een <code style="color:#f08c00;font-weight:600">404</code> teruggeeft, op niveau Warning.

Kijk in het terminalvenster van je API of de berichten verschijnen, met het juiste niveau ervoor.

> [!TIP]
> Een logger hoef je niet zelf te registreren in Program.cs. Die levert ASP.NET Core uit zichzelf.

---

### 6. Zoeken op naam

Voorzie een GET-endpoint op:

<span style="background:#1c7ed6;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>GET</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/dier/zoek/<span style="color:#e64980"><b>{tekst}</b></span></code>

Dit endpoint geeft alle dieren terug waarvan de naam het stukje <code style="color:#e64980;font-weight:600">{tekst}</code> bevat, met statuscode <code style="color:#37b24d;font-weight:600">200 OK</code>. Het stukje mag overal in de naam staan, en hoofdletters spelen geen rol: `mi`, `MI` en `Mi` vinden allemaal een dier dat Mimi heet, en ook een dier dat Tommie heet.

Vindt je API geen enkel dier, geef dan statuscode <code style="color:#f08c00;font-weight:600">404 Not Found</code> met het bericht:

<code style="color:#845ef7">We vonden geen dier waarvan de naam <span style="color:#e64980"><b>{tekst}</b></span> bevat.</code>

> [!NOTE]
> Het zoeken zelf gebeurt in de repository. De controller beslist enkel welke statuscode bij het resultaat hoort.

Test je endpoint en controleer:

- of een stukje uit het midden van een naam dat dier vindt;
- of hetzelfde stukje in kleine letters en in hoofdletters twee keer dezelfde dieren geeft;
- of een stukje dat in geen enkele naam voorkomt, <code style="color:#f08c00;font-weight:600">404</code> geeft en een Warning in je log zet.

---

### 7. Alles samen: de dierfiche via de repository

<span style="background:#7048e8;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>SAMENGESTELDE OEFENING</b></span>

In 02_04 stelde je controller de dierfiche samen door zelf in zijn lijst te zoeken: naar het dier, naar de andere dieren van dezelfde soort, naar het oudste. Die lijst heeft hij nu niet meer.

In dit laatste punt laat je de fiche opnieuw werken, met alles wat je in deze oefening hebt opgebouwd: de repository, de dependency injection en de logging.

<span style="background:#1c7ed6;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>GET</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/dier/fiche/<span style="color:#e64980"><b>{id}</b></span></code>

De fiche bevat dezelfde vier gegevens als in 02_04, met dezelfde betekenis:

| Gegeven                | Betekenis                                              |
|------------------------|--------------------------------------------------------|
| Dier                   | het dier zelf                                          |
| IsJong                 | ja als het dier jonger is dan twee jaar                |
| AantalVanDezelfdeSoort | het aantal andere dieren van dezelfde soort            |
| IsOudsteVanDeSoort     | ja als geen enkel ander dier van die soort ouder is    |

Bestaat het dier niet, geef dan statuscode <code style="color:#f08c00;font-weight:600">404 Not Found</code> met het bericht <code style="color:#845ef7">We vonden geen dier met Id <span style="color:#e64980"><b>{id}</b></span>.</code> en log een Warning.

> [!NOTE]
> Wie stelt de fiche samen: de repository of de controller? Lees nog eens hoe les 03 de taak van elk beschrijft, en beslis dan of je interface er iets bij nodig heeft, en zo ja wat.

Test je endpoint en controleer:

- of de fiches van drie dieren dezelfde gegevens tonen als in 02_04;
- of `AantalVanDezelfdeSoort` op 0 staat bij een dier dat als enige van zijn soort in het asiel zit;
- of bij twee dieren van dezelfde soort met een verschillende leeftijd maar bij één van de twee `IsOudsteVanDeSoort` op ja staat.

> [!NOTE]
> Kijk wat er níet veranderd is: de routes, de statuscodes en de JSON. Enkel de binnenkant van je API is anders. Dat is precies wat refactoren betekent.
