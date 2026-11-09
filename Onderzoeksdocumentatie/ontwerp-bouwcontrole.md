# Bouwcontrole — ontwerpkeuzes en hun onderbouwing

Naslag bij de repo `PJPRORL/Alternate.Bouwconflicten`.

> **Herzien 11 september 2026.** Dit document is uitgekuist volgens de
> projectafspraken: het legt vast *welke keuzes gemaakt zijn en waarom*, en
> *welke vragen nog open staan*. Het bevat geen klassen, geen signatures, geen
> veldenlijsten en geen testgevallen meer — die zijn jouw werk.
>
> Zie `docs/projectafspraken.md` voor de regels die hier boven staan.

---

## 1. Wat Bouwcontrole is

Onderdelen van een pc-bouworder worden gescand op artikelcode. Het systeem toont
per onderdeel of er bekende bouwconflicten zijn, op basis van de
incompatibiliteitenlijst van Alternate. Groen is in orde, oranje heeft
opmerkingen, rood is niet in orde.

De mockup is één scherm: een scanzone, een kolom met gescande onderdelen, en
onderaan een paneel met gevonden conflicten.

---

## 2. De gekozen ondergrond

Het project wordt gebouwd met de leerstof van Programming Advanced en
.NET Development. Die twee vakken spreken elkaar op bijna elk technisch punt
tegen. De gekozen lijn:

| Onderdeel | Keuze | Uit welk vak |
|---|---|---|
| Framework | .NET 10 | Programming Advanced |
| API-documentatie en testen | Scalar | Programming Advanced |
| Database | PostgreSQL in Docker | Programming Advanced |
| Data-toegang | EF Core, code-first, Fluent API | beide |
| Repositories en Unit of Work | de asynchrone variant | Programming Advanced |
| Businesslogica | in een Services-laag | .NET Development |
| DTO's en validatie | DataAnnotations op de Write-DTO's | beide |
| Mapping | Mapster | Programming Advanced |
| Tests | NUnit met Moq | .NET Development |
| Beveiliging | Identity met JWT en rollen | .NET Development |
| Front-end | MVC met Razor en ViewModels | .NET Development |

De onderbouwing per keuze staat in `docs/projectafspraken.md`.

---

## 3. Waar wat draait

```
   browser op de werkvloer                 één machine (voorlopig localhost)
┌──────────────────────────┐            ┌────────────────────────────────────┐
│  MVC-webapplicatie       │            │  ASP.NET Core                      │
│   · scanzone             │            │   · controllers                    │
│   · onderdelenlijst      │  ────────► │   · services  ← de conflictlogica  │
│   · conflictenpaneel     │  ◄──────── │   · repositories + unit of work    │
│   · Razor + ViewModels   │            │   · EF Core                        │
└──────────────────────────┘            └─────────────────┬──────────────────┘
                                                          │
                                              ┌───────────▼───────────┐
                                              │  PostgreSQL in Docker │
                                              └───────────────────────┘
```

De conflictlogica draait **aan de serverkant**. Dat is wat "één gedeelde
conflictenlijst" betekent: één plek waar de regels staan, zodat elke bouwplek
hetzelfde antwoord krijgt.

> **Gevolg om nu al te kennen:** een bouwplek zonder netwerk kan niets
> controleren. Voor een werkplaatstool is dat een echt risico. Bouw daar nu niets
> voor, maar weet dat het ooit terugkomt.

---

## 4. De front-end: MVC in plaats van MAUI

Oorspronkelijk was dit een .NET MAUI-project, omdat er een losse toepassing op de
werkvloer moest draaien. Dat vak staat niet meer in de opleiding.

.NET Development les 09 tot 11 bouwen een MVC-webapplicatie met Razor, ViewModels
en Identity-schermen. Dat is precies de vorm die de mockup vraagt, en het scheelt
een installatie per werkplek: een browser volstaat.

Les 09 zet twee manieren naast elkaar. Bouwcontrole neemt de tweede: **front-end
en back-end in één applicatie**, waarbij de logica van de API-controller in de
MVC-controller terechtkomt. Dat is wat de cursus uitwerkt, en het vermijdt dat je
twee applicaties tegelijk moet draaien en beveiligen.

---

## 5. Het datamodel

### Eén hiërarchie, geen losse modellen per onderdeeltype

Er is één basistype `Onderdeel` met de onderdeeltypes eronder. De reden is
praktisch: bij een gescande code weet je vooraf niet welk type het is. Met losse
modellen per type zou je elke repository moeten afgaan met de vraag "is deze code
van jou?".

### Welke eigenschappen een kolom verdienen

Niet alles wat op een productpagina staat. De maatstaf is: **een eigenschap
verdient een kolom alleen als een conflictregel er ooit van kan afhangen.** Anders
bouw je een productcatalogus, en dat is een veel grotere klus dan wat je nodig
hebt.

De werkvolgorde per onderdeeltype:

1. Neem je incompatibiliteitenlijst erbij.
2. Vraag je per item af van welke eigenschap het afhangt.
3. Die eigenschappen worden velden. De rest niet.

Een kolom later toevoegen is één migratie. Een model opschonen dat volgestouwd is
met velden waar niets naar kijkt, is dat niet.

### Overerving in de database — een open beslissing

**Geen van beide cursussen behandelt dit.** Je moet het dus zelf uitzoeken, en het
is de enige plek in het project waar je buiten de leerstof treedt.

EF Core kan een overervingshiërarchie op meerdere manieren naar tabellen vertalen.
De twee die voor jou in aanmerking komen, verschillen in:

- hoeveel tabellen er ontstaan
- of subtype-kolommen leeg blijven staan bij andere subtypes
- hoe genormaliseerd het resultaat is
- of er joins nodig zijn bij het opvragen

Wat er **niet** van afhangt: het opzoeken van een gescande code. EF geeft je in
beide gevallen het juiste subtype terug.

Vraag om te beantwoorden voor je kiest: hoeveel eigenschappen verschillen er
werkelijk per onderdeeltype, en hoeveel overlappen er? Als het antwoord "weinig
verschil" is, valt de keuze anders uit dan bij "elk type heeft zijn eigen wereld".

Vroeger stond hier een advies. Dat was gebaseerd op de veronderstelling dat je vak
Database je datamodel zou beoordelen. Dat blijkt niet zo: dat vak bestaat uit
SQL-oefeningen op een gegeven schema. De keuze is dus vrij, en aan jou.

### Conflicten

Een conflict is **een verzameling voorwaarden**, geen enkele regel. Dat is de
belangrijkste ontwerpkeuze in het hele project, en de reden is uitbreidbaarheid:

- Nu vul je alleen voorwaarden op artikelcode in, rechtstreeks uit je
  incompatibiliteitenlijst.
- Later wil je voorwaarden op eigenschappen ("een koeler hoger dan de kast
  toelaat"). Met deze opzet komen die erbij zonder dat de engine herschreven moet
  worden.

Een conflict draagt ook een **ernst**. De ernst bepaalt de kleur op het scherm.
Zit een onderdeel in meerdere conflicten, dan is de vraag welke ernst wint — denk
na over wat de gebruiker op de werkvloer moet zien.

---

## 6. De conflict-engine hoort in een Service

.NET Development les 08 stelt dat controllers en repositories in de praktijk niet
unit-testbaar zijn, en dat businesslogica daarom in een Service hoort. Voor
Bouwcontrole is dat richtinggevend.

De conflict-engine is businesslogica in zuivere vorm: onderdelen en conflictregels
in, een resultaat uit. Geen database, geen HTTP, geen async. Daarom is het
precies het stuk dat je **wél** volledig kunt testen — en het is meteen het stuk
waar de fouten in gaan zitten.

### Twee dingen om over na te denken voor je begint

**Het resultaat moet meer dragen dan "dit conflict vuurde".** De mockup kleurt de
regel van een ónderdeel. Vraag jezelf af wat de engine moet teruggeven opdat het
scherm weet wélke regels het moet kleuren.

**Meerdere voorwaarden, één onderdeel.** Als een conflict twee voorwaarden heeft
en er is maar één onderdeel dat aan allebei voldoet — vuurt het conflict dan? Denk
hierover na vóór je de controle schrijft, want de voor de hand liggende manier om
"zijn alle voorwaarden vervuld?" op te schrijven geeft hier het verkeerde
antwoord. Dit is het soort vraag waar je eerst een test voor schrijft en daarna
pas de methode, zoals les 08 het beschrijft.

---

## 7. De API en het scherm

De API geeft **ernst** terug, geen kleur. Groen, oranje en rood zijn een
beslissing van het scherm. Zet je kleur in je DTO, dan zit je UI in je API.

De vragen die je endpoints moeten beantwoorden:

- een gescande code opzoeken en het onderdeel teruggeven
- zoeken wanneer de code niets oplevert
- een volledige bouworder aanbieden en de controle terugkrijgen
- de conflictenlijst lezen
- de conflictenlijst beheren

Die laatste levert je meteen het conflictbeheer dat in de mockup ontbrak: zonder
beheer moet elke regelwijziging door een programmeur.

Het interessantste om te ontwerpen is de controle zelf. Dat is het enige endpoint
dat géén databasetabel achter zich heeft — je stuurt iets op, je krijgt een
antwoord, er wordt niets bewaard. Denk na over welk HTTP-verb daarbij hoort.

**De Scalar-pagina is je eerste scherm.** Je kan de hele controle testen voor er
één regel Razor geschreven is. Gebruik dat.

---

## 8. Data ophalen

De onderdelenlijst wordt gevuld met de Scrapy-scraper. Zie
`docs/scrapy-handleiding.md` voor de opzet en wat het de site kost.

De scraper vult de onderdelen. De **conflictregels** komen uit je eigen
incompatibiliteitenlijst en worden beheerd in de applicatie zelf — die zijn niet
te scrapen.

---

## 9. Open vragen

Dit zijn de vragen die het ontwerp nog blokkeren. Ze staan hier omdat het
antwoord uit jouw werksituatie moet komen, niet uit een cursus.

1. **Wat is "Kraken" precies, en welke code leest de scanner?** De mockup toont
   artikelcode én Kraken-Id naast elkaar. Als de scanner de ene leest en je lijst
   op de andere staat, heb je een vertaalstap nodig.
2. **Hoe komt een bouworder binnen?** Handmatig scannen per stuk, of komt er een
   bestand of nummer uit een bestaand systeem? Dit bepaalt of je een import nodig
   hebt.
3. **Wordt Bouwcontrole de bron van de conflictenlijst?** Of blijft de lijst
   elders bestaan en is dit een kopie? Twee bronnen die uit elkaar lopen is het
   klassieke einde van dit soort tools.
4. **Is rood werkelijk blokkerend?** Mag iemand doorgaan met een rode melding, of
   moet het systeem dat tegenhouden? Dit is een organisatievraag, geen technische.
5. **Wie mag wat?** Met Identity erbij: mag iedereen op de werkvloer conflicten
   beheren, of alleen lezen en controleren? Dit bepaalt je rollen.
