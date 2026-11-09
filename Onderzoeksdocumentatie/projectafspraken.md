# Projectafspraken — Bouwcontrole

Dit document legt vast hoe wij samenwerken aan Bouwcontrole. Het staat boven alle
andere documenten in dit project. Als een ander document hiermee in strijd is,
wint dit document en wordt het andere document aangepast.

Vastgelegd op 11 september 2026, op basis van de afspraken zoals Jeroen ze heeft
geformuleerd.

---

## 1. Doel en aanpak

Bouwcontrole wordt gebouwd met de leerstof van drie vakken:

- Programming Advanced
- Database
- .NET Development

Het project wordt opgebouwd **volgens de lessen, in dezelfde volgorde als de
leerstof**. Niet vooruitlopen, niet overslaan. Wat in les 7 staat, komt pas aan
bod nadat les 1 tot en met 6 in het project zijn toegepast.

---

## 2. Volgorde van de vakken

1. **Programming Advanced** — het project start met alles wat hier wordt
   aangeleerd. Dit legt de grondlaag.
2. **Database** — wordt gebruikt voor het opslaan van de gegevens.
3. **.NET Development** — het project bouwt hier later op verder, volgens de
   principes van dat vak.

---

## 3. Aanlevering van de cursus

Jeroen levert alle nodige cursuspagina's aan als PDF, voor zowel Programming
Advanced als .NET Development.

**Claude bekijkt al dat materiaal volledig voordat het stappenplan wordt
opgesteld of aangevuld.**

Eerst komt de documentatie van Programming Advanced, omdat dat vak als eerste
aan bod komt en de grondlaag legt. Daarna volgen de PDF's van .NET Development.

### Status van de aanlevering — afgewerkt op 11 september 2026

**Programming Advanced** — les 01 Introductie, 02 HTTP Verbs, 03 Repositories,
04 Entity Framework Core, 05 Asynchroon programmeren, 06 Relaties,
07 Generics & Unit of Work, 08 Include statements, 09 DTO's: aangeleverd en
volledig gelezen. Les 08 en 09 kwamen elk in twee varianten die inhoudelijk
identiek bleken.

Nog niet aangeleverd: **les 10 Unit testing**.

**.NET Development** — les 01 Gegevens ophalen uit een database, 02 Gegevens
toevoegen wijzigen en verwijderen, 03 Relaties tussen entiteiten,
04 Generics & UnitOfWork, 05 Data Transfer Objects, 06 Identity Security deel 1,
07 Identity Security deel 2, 08 Unit Testing, 09 MVC deel 1, 10 MVC deel 2,
11 MVC deel 3: aangeleverd en volledig gelezen. Compleet.

**Database** — geen aparte leerstof aangeleverd; wat bekend is komt uit de
cursusrepo.

---

## 4. Wat het stappenplan moet bevatten

Een duidelijke **uitleg** en een duidelijke **weergave** van de opbouw en de
structuur van het project.

Claude mag per vak kiezen tussen twee werkwijzen:

- **Optie A — verwijzen naar de cursus.** Claude wijst de relevante cursusdelen
  aan. Jeroen leest en leert die eerst, en gaat daarna verder met het project.
- **Optie B — de cursus verwerken in het stappenplan.** Claude verwerkt alles wat
  Jeroen moet weten rechtstreeks in het stappenplan, zodat hij van daaruit kan
  werken.

### Gemaakte keuze — Optie A voor alle drie de vakken

| Vak | Keuze | Reden |
|---|---|---|
| Programming Advanced | **Optie A** | De leerlijn is strak opgebouwd en volledig uitgeschreven, met werkende voorbeelden. Die overschrijven in het stappenplan zou de cursus dupliceren én zou neerkomen op het voorzeggen van uitwerkingen, wat punt 5 uitsluit. |
| .NET Development | **Optie A** | Zelfde reden. Het materiaal is compleet en sequentieel; herkauwen voegt niets toe en neemt je het denkwerk uit handen. |
| Database | **Optie A** | Het vak bestaat uit SQL-oefeningen op een gegeven schema. Het stuurt de architectuur van het project niet; het project past de kennis toe, niet omgekeerd. |

### Wat het stappenplan dan wél doet

Optie A betekent niet "hier is een lijstje lesnummers, zoek het uit". Het
stappenplan draagt drie dingen die in geen van beide cursussen staan:

1. **Navigatie.** Per fase: welke les je eerst leest, en wat je daarna op
   Bouwcontrole toepast.
2. **Arbitrage.** De twee vakken behandelen grotendeels dezelfde onderwerpen
   maar spreken elkaar tegen op bijna elke technische keuze (zie hieronder).
   Het stappenplan zegt per onderwerp welk vak voorgaat en waarom.
3. **Het domein.** Alles wat eigen is aan Bouwcontrole — de analyse van de
   onderdelen, de overerving tussen onderdeeltypes, de conflictregels, de
   scanflow, het ERD. Geen van beide cursussen raakt hieraan.

---

## 5. Harde regels (niet onderhandelbaar)

Letterlijk zoals afgesproken:

> - Geen code schrijven. Niet voor Programming Advanced, niet voor Database,
>   niet voor .NET Development.
> - Geen expliciete voorbeelden die het antwoord voorzeggen. Uitwerkingen die
>   mij het werk uit handen nemen, zijn uitgesloten.
> - Geen andere cruciale hulpmiddelen die het denkwerk overnemen.
> - Het volledige project moet door mij gemaakt worden.
> - Hulp mag uitsluitend in de vorm van tips en richting geven — je wijst mij
>   de weg, meer niet.

### Hoe Claude dit toepast

Wel:

- vertellen **wat** een fase moet opleveren en **waarom**
- verwijzen naar de les waar het staat
- benoemen welke valkuil er in die fase zit, zonder de oplossing te geven
- een fout in je eigen code aanwijzen en uitleggen waarom het fout is
- een vraag terugstellen als dat je verder helpt
- structuur en volgorde bewaken

Niet:

- code schrijven, ook geen fragment, ook niet "ter illustratie"
- method signatures, klassendefinities of veldenlijsten opsommen die jij nog
  moet bedenken
- een uitgewerkt voorbeeld geven dat je één op één kan overnemen
- testgevallen opsommen die jij zelf moet afleiden
- vooruitlopen op leerstof die nog niet aan bod is gekomen

### Beslist: met terugwerkende kracht

De bestaande documenten waren geschreven vóór deze regels en bevatten zaken die
er niet meer in horen. Jeroen heeft beslist dat de regels ook met terugwerkende
kracht gelden.

- `docs/ontwerp-bouwcontrole.md` — uitgekuist op 11 september 2026. Klassen,
  signatures, veldenlijsten, DTO-vormen en de lijst testgevallen zijn eruit. Wat
  overblijft is: welke keuzes gemaakt zijn, waarom, en welke vragen nog open
  staan.
- `docs/stappenplan.md` — wordt volledig herschreven, voor de nieuwe ondergrond
  én de nieuwe volgorde. Daarmee is het meteen uitgekuist.

---

## 6. Waar de twee vakken elkaar tegenspreken

Beide cursussen behandelen EF Core, relaties, repositories, generics,
Unit of Work en DTO's — maar met andere keuzes. Dit moet één keer beslist
worden, anders staat het project halverwege op twee sporen.

| Onderwerp | Programming Advanced | .NET Development |
|---|---|---|
| Framework | .NET 10 | .NET 8 |
| Test- en documentatie-UI | Scalar ("Swagger is met pensioen") | Swagger / Swashbuckle, met JWT-securitydefinitie |
| Database | PostgreSQL in Docker + pgAdmin | SQL Server, in Docker of als LocalDB |
| Mapping-library | Mapster | AutoMapper |
| Circulaire referenties | opgelost met DTO's; de IgnoreCycles-tijdelijke oplossing wordt expliciet weer verwijderd | JsonIgnore en IgnoreCycles, naast DTO's |
| Validatie | DataAnnotations op de Write-DTO's | eerst op het model, vanaf les 05 op de DTO's |
| Extra data-toegang | Dapper als begrip, EF Core in de praktijk | ruwe SQL naast LINQ, met waarschuwing voor SQL-injectie |

### Beslist: de gekozen ondergrond

Jeroen heeft de keuze aan Claude gelaten, met één gegeven: .NET 8 bestaat niet
meer actief, het wordt sowieso .NET 10. Dat maakt het framework vast en dwingt
een aantal andere keuzes mee.

| Onderdeel | Keuze | Waarom |
|---|---|---|
| Framework | **.NET 10** | Gegeven. .NET 8 is voorbij. |
| API-UI | **Scalar** | .NET 10 genereert zelf OpenAPI; Swashbuckle wordt door Programming Advanced expliciet afgeschreven. Scalar heeft een eigen veld om een JWT-token te plakken, dus de Identity-lessen blijven bruikbaar. |
| Database | **PostgreSQL in Docker** | Beide vakken werken met Docker; ze verschillen enkel in engine. Programming Advanced gaat voor, en de provider is één package plus één regel in Program.cs — Entity Framework zelf verandert niet. LocalDB blijft de uitwijkweg als Docker tegenwerkt. |
| Repositories en Unit of Work | **de variant van Programming Advanced** | Volledig asynchroon, met één centrale SaveChangesAsync op de Unit of Work. De variant van .NET Development slaat synchroon op, wat botst met "async all the way". |
| Mapping | **Mapster** | Wat Programming Advanced onderwijst. Bovendien: MIT-licentie, ondersteunt .NET 10, geen licentiesleutel nodig. AutoMapper v15 is sinds 2025 commercieel — gratis onder vijf miljoen dollar omzet, maar mét verplichte registratie, en Alternate zit boven die grens. |
| Validatie | **DataAnnotations op de Write-DTO's** | Beide vakken komen hier samen. |
| Services-laag | **toevoegen** | Uit .NET Development les 08. Programming Advanced spreekt dit niet tegen. |
| Tests | **NUnit met Moq** | Uit .NET Development les 08. |
| Beveiliging | **Identity met JWT en rollen** | Uit .NET Development les 06 en 07. |
| Front-end | **MVC met Razor en ViewModels** | Keuze van Jeroen. Uit .NET Development les 09 tot 11. |

### Twee wrijvingspunten die hieruit volgen

1. **Mapping in unit tests.** .NET Development les 08 legt uit hoe je een
   AutoMapper handmatig opbouwt in een test, omdat je hem niet kunt mocken. Met
   Mapster werkt dat anders. In de praktijk valt dit grotendeels weg: mapping
   hoort aan de rand van de applicatie, niet in de Service die je test.
2. **De JWT-opzet van les 06 bevat .NET 8-workarounds.** Er staan daar
   instellingen in die bewust een oudere, tragere token-afhandeling aanzetten om
   een bug uit .NET 8 te omzeilen. Op .NET 10 compileert dat nog, maar het is
   niet wat je wilt. Dit wordt opgepakt in de fase waar Identity aan bod komt.

### Wat het ene vak heeft en het andere niet

Uit **Programming Advanced** alleen: Scalar, Docker en PostgreSQL, Dapper als
concept, en de expliciete soft-delete-norm.

Uit **.NET Development** alleen, en dus echte aanvulling:

- **Identity, JWT en rollen** (les 06–07) — registratie, aanmelden, rechten
- **Unit testing met NUnit, TDD en mocking met Moq** (les 08)
- **De Services-laag.** Les 08 stelt dat controllers en repositories in de
  praktijk niet unit-testbaar zijn, en dat businesslogica daarom in een Service
  hoort. Voor Bouwcontrole is dat de belangrijkste zin uit beide cursussen: de
  conflict-engine is businesslogica, en dus een Service.
- **MVC met Razor en ViewModels** (les 09–11) — een webfront-end als alternatief
  of aanvulling op de MAUI-app

---

## 7. Vervolg

Zodra dit allemaal correct is omschreven en alle regels vaststaan, gaan we
verder met het bespreken van de structuur en de inhoud van het project zelf.
