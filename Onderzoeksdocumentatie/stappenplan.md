# Bouwcontrole — stappenplan

Zestien fases, van een solution die niet laadt tot een werkend scherm op de
werkvloer — met halverwege een demo die je kan tonen.

> **Herschreven 12 september 2026** voor de gekozen ondergrond en de volgorde van
> de leerstof. Dit document volgt `docs/projectafspraken.md`: het wijst je de weg,
> het loopt hem niet voor je. Er staat geen code in, geen signatures, geen
> veldenlijsten en geen testgevallen. Dat is jouw werk.
>
> Per fase staat er welke les je **eerst** leest. Lees die ook echt eerst — de
> fase is geschreven in de veronderstelling dat je hem kent.

---

## Hoe dit plan in elkaar zit

Het project wordt opgebouwd volgens de lessen, in dezelfde volgorde als de
leerstof. Dat betekent dat je onderweg je eigen code herschrijft. Dat is geen
verspilling: elke les vervangt iets voorlopigs door iets beters, en je begrijpt
pas waaróm een repository nuttig is als je eerst zonder hebt gewerkt.

```
DEEL 1 — Programming Advanced legt de grondlaag én levert een werkende demo
┌────┬────┬────┬────┬────┬────┬────┬────┬────┬────┬────┬────┐
│ 0  │ 1  │ 2  │ 3  │ 4  │ 5  │ 6  │ 7  │ 8  │ 9  │ 10 │ 11 │
└────┴────┴────┴────┴────┴────┴────┴────┴────┴────┴────┴────┘
  op  draait analyse crud  repo   db  async model  uow  joins  dto  ENGINE
                ▲                                                      │
        jouw werk, geen les                                       ◄ demo hier
                                                                       ▼
DEEL 2 — .NET Development verbetert wat er staat   ┌────┬────┬────┬────┐
                                                   │ 12 │ 13 │ 14 │ 15 │
                                                   └────┴────┴────┴────┘
                                                  tests identity scherm rechten
```

**Het doel van deel 1 is een werkende demo.** Aan het einde van fase 11 kan je een
bouworder insturen en de gevonden conflicten terugkrijgen. Dat werkt echt, en het
is te tonen aan wie de mockup moet goedkeuren. Deel 2 verbetert dan een applicatie
die al draait, in plaats van hem alsnog af te maken.

Drie fases verdienen nu al aandacht. **Fase 2** staat in geen enkele cursus: het
is de analyse van je eigen incompatibiliteitenlijst, en zonder die analyse weet je
niet welke velden je modellen nodig hebben. **Fase 11** is het hart van het
project en meteen je demo. **Fase 12** zet er de tests omheen — en zal daar
waarschijnlijk fouten uit halen. Dat is geen falen, dat is waarvoor die fase
bestaat.

---

## De vorm waar je naartoe werkt

Beide cursussen bouwen alles in één webproject met mappen erin, plus een apart
testproject. Niet in losse projecten per laag.

```
Alternate.Bouwcontrole.sln
├── Alternate.Bouwcontrole            net10.0   het webproject
│   ├── Controllers/                            api- en mvc-controllers
│   ├── Models/                                 het domein
│   ├── DTOs/                                   per entiteit een submap
│   ├── ViewModels/                             voor de MVC-schermen
│   ├── Data/                                   de databasecontext
│   ├── Repositories/                           interfaces én implementaties
│   ├── Services/                               de conflict-engine woont hier
│   ├── Configuration/                          de mapping-configuratie
│   ├── Views/                                  razor
│   ├── Migrations/                             door EF gegenereerd
│   └── docker-compose.yml
└── Alternate.Bouwcontrole.Tests      net10.0   nunit, test op Services
```

Die mappen ontstaan niet in één keer. Ze komen fase per fase erbij, op het moment
dat de les ze introduceert. Probeer ze niet vooruit aan te maken.

```
   verzoek
      │
      ▼
 ┌─────────────┐   http en statuscodes, meer niet
 │ Controller  │
 └──────┬──────┘
        ▼
 ┌─────────────┐   businesslogica — hier zit de conflict-engine
 │  Service    │   en dit is wat je unit-test
 └──────┬──────┘
        ▼
 ┌─────────────┐   ophalen en wegschrijven, geen beslissingen
 │ Repository  │
 └──────┬──────┘
        ▼
 ┌─────────────┐   één transactie per verzoek
 │ Unit of Work│
 └──────┬──────┘
        ▼
    EF Core  ──►  PostgreSQL in Docker
```

---

# DEEL 1 — Programming Advanced

---

## Fase 0 — Opruimen

**Lees eerst:** niets.

**Waarom nu:** je solution laadt op dit moment niet. Een solutionbestand is een
tekstbestand met een lijstje van welke projecten erbij horen en waar ze staan; in
`Alternate.Bouwcontrole.slnx` wijzen beide paden naar mappen die niet bestaan, en
de projectreferentie in het app-project heeft dezelfde fout. Je gaat dat niet
repareren — de vorm van de solution verandert toch, omdat de front-end MVC wordt
en beide bestaande projecten verdwijnen.

**Wat je doet:**

- De solution opnieuw opzetten in de vorm hierboven: één webproject, één
  testproject, met één referentie van Tests naar het webproject. Dat gaat sneller
  dan de bestaande paden repareren. Het testproject blijft leeg tot fase 12 — je
  zet hem nu alleen klaar zodat de vorm van je solution meteen goed staat.
- Het MAUI-project verwijderen. Dat vak staat niet meer in je opleiding en de
  front-end wordt MVC.
- Ook het Core-project verdwijnt. De scheiding tussen de lagen zit vanaf nu in de
  mappen van het webproject, niet in aparte projecten — dat is hoe beide cursussen
  het doen. Er staat niets in Core dat je moet overzetten: die bestanden zijn leeg.
- De lege placeholders weggooien. Er staat nu een abstracte klasse zonder inhoud,
  een subklasse die niet overerft van die abstracte klasse, en een bestand dat
  `IMoederbordRepository` heet maar als klasse gedeclareerd staat in plaats van als
  interface.
- Het ERD-bestand leegmaken. Vijf vormen die allemaal `Table` heten met rijen
  `Row 1` tot `Row 3` zeggen niets; je tekent dit opnieuw in fase 2.

**Wat je niet doet:** nog geen modellen schrijven. Je weet nog niet welke velden
ze nodig hebben.

**Klaar wanneer:** de solution opent zonder fouten en er staat geen enkel bestand
meer in dat niets doet.

**Valkuil:** de oude bestanden half laten staan. Maak je een nieuwe solution maar
blijft het oude `.slnx` in de map liggen, dan open je vroeg of laat het verkeerde
bestand — via je lijst met recente projecten bijvoorbeeld — en vraag je je af
waarom er niets werkt. Gooi het oude solutionbestand en de oude projectmappen weg,
en controleer daarna in git dat die verwijderingen ook echt opgenomen zijn.

---

## Fase 1 — Het project draait

**Lees eerst:** Programming Advanced les 01.

**Waarom nu:** je wilt zo snel mogelijk iets dat opstart en antwoordt. Alles wat
daarna komt bouwt daarop; als dit niet werkt, weet je bij elke volgende fout niet
of het aan de fout of aan de opzet ligt.

**Wat je doet:** het webproject op .NET 10 met controllers aanmaken, Scalar
toevoegen en registreren, het weer-voorbeeld dat .NET erbij geeft weggooien, en
één eigen controller maken die iets triviaals teruggeeft. Een begroeting volstaat.

**Wat je niet doet:** geen database, geen modellen, geen repository. Alleen:
draait het, en zie ik mijn endpoint in Scalar.

**Klaar wanneer:** je surft naar de Scalar-pagina, klikt je eigen endpoint aan en
krijgt antwoord.

**Valkuil:** oudere handleidingen op het internet laten je Swashbuckle
installeren. Doe dat niet. .NET 10 genereert OpenAPI zelf en je cursus schrijft
Swagger expliciet af.

---

## Fase 2 — Analyse: van je lijst naar eigenschappen

**Lees eerst:** niets. Dit staat in geen enkele cursus.

**Waarom nu:** dit is de fase die bepaalt of het project werkt. Iedereen wil
meteen modellen schrijven. Maar een model schrijven zonder te weten welke
eigenschappen je conflictregels nodig hebben, betekent dat je in fase 7 opnieuw
begint.

**Wat je doet:** neem je incompatibiliteitenlijst erbij en werk hem regel voor
regel af. Per regel: van welke eigenschap van welk onderdeeltype hangt deze af?
Verzamel per onderdeeltype de eigenschappen die eruit komen. Dat, en niet meer,
wordt straks je model.

Leg daarnaast vast welke onderdeeltypes er überhaupt bestaan in een bouworder, en
welke eigenschappen ze mét elkaar delen. Dat laatste bepaalt in fase 7 hoe je
overerving naar tabellen vertaalt.

Teken hier je ERD opnieuw, nu met echte namen.

**Wat je niet doet:** eigenschappen opnemen "omdat ze op de productpagina staan".
Een eigenschap verdient alleen een kolom als een conflictregel er ooit van kan
afhangen. Een kolom later toevoegen is één migratie; een model opschonen dat
volgestouwd is, is dat niet.

**Klaar wanneer:** je hebt per onderdeeltype een lijst eigenschappen, en bij elke
eigenschap kan je de conflictregel aanwijzen die hem nodig heeft.

**Valkuil:** je incompatibiliteitenlijst bevat waarschijnlijk regels die over
concrete artikelcodes gaan ("deze kast en die koeler gaan niet samen") én regels
die over eigenschappen gaan ("een koeler hoger dan de kast toelaat"). Behandel ze
niet als twee soorten systemen. Zie fase 7.

---

## Fase 3 — Modellen en CRUD op een lijst in het geheugen

**Lees eerst:** Programming Advanced les 02.

**Waarom nu:** je leert hier de HTTP-kant los van de database-kant. Dat scheelt je
straks veel zoekwerk, want als er iets misgaat weet je meteen aan welke kant.

**Wat je doet:** één onderdeeltype als model, met de eigenschappen uit fase 2. Een
controller met de volledige CRUD-cyclus erop, werkend op een lijst in het
geheugen. Let op de statuscodes: de les geeft je een tabel van welke code bij
welke actie hoort.

**Wat je niet doet:** nog geen database, nog geen repository, nog niet alle
onderdeeltypes. Eén type volstaat om het patroon te leren.

**Klaar wanneer:** je kan via Scalar een onderdeel aanmaken, opvragen, wijzigen en
verwijderen, en je krijgt telkens de juiste statuscode terug.

**Valkuil:** de les bespreekt soft delete tegenover hard delete. Beslis nu al wat
Bouwcontrole doet met een onderdeel dat uit de handel gaat — want er hangen straks
conflictregels aan die verwijzen naar artikelcodes.

---

## Fase 4 — Interface, repository, Dependency Injection en logging

**Lees eerst:** Programming Advanced les 03.

**Waarom nu:** je controller doet op dit moment te veel. Deze fase haalt de
datalogica eruit, en dat is precies wat het straks mogelijk maakt om die lijst in
het geheugen te vervangen door een echte database zonder je controller aan te
raken.

**Wat je doet:** een interface die het contract vastlegt, een implementatie die
nog steeds op de lijst in het geheugen werkt, registratie in `Program.cs`, en de
controller die de interface via zijn constructor opvraagt in plaats van zelf een
repository aan te maken. Daarna logging toevoegen.

**Wat je niet doet:** nog steeds geen database. Het punt van deze fase is dat de
controller niet weet wat erachter zit.

**Klaar wanneer:** je applicatie werkt precies zoals in fase 3, maar de controller
bevat geen enkele regel die over data gaat.

**Valkuil:** de verleiding om in de constructor van je controller alsnog zelf een
repository aan te maken. Als het woord `new` in je controller staat, heb je de
oefening gemist.

---

## Fase 5 — Docker, PostgreSQL, DbContext en de eerste migratie

**Lees eerst:** Programming Advanced les 04.

**Waarom nu:** vanaf hier blijft je data bestaan na een herstart. Dat is ook het
moment waarop fouten duurder worden, dus lees deze les grondig.

**Wat je doet:** Docker Desktop installeren, het compose-bestand maken dat
PostgreSQL en pgAdmin opstart, de vier Entity Framework-packages installeren, de
connectiestring in `appsettings.json` zetten, de databasecontext aanmaken met je
model erin, hem registreren, en de eerste migratie aanmaken en uitvoeren.

**Wat je niet doet:** nog niet je repository omzetten. Dat is fase 6. Hier gaat
het er alleen om dat de database bestaat en dat je hem kan bekijken.

**Klaar wanneer:** je ziet je tabel staan in pgAdmin of via de PostgreSQL-extensie
in je editor.

**Valkuil:** twee dingen. Docker moet draaien vóór je de database bijwerkt,
anders krijg je een foutmelding die niets met je code te maken heeft. En: pas je
database nooit rechtstreeks aan. Altijd via een migratie, anders loopt het bij de
volgende migratie vast op een manier die lastig terug te draaien is.

> Werkt Docker niet mee, dan geeft de les je een alternatieve connectiestring voor
> een lokale database. De cursus zegt zelf dat het type database er op het examen
> niet toe doet, alleen of je code klopt. Verlies hier geen dagen aan.

---

## Fase 6 — De echte repository, en dan alles async

**Lees eerst:** Programming Advanced les 05.

**Waarom nu:** hier plukken je de vruchten van fase 4. Je schrijft een tweede
implementatie van dezelfde interface, ditmaal tegen de databasecontext, en je
wisselt om met één regel.

**Wat je doet:** de nieuwe repository schrijven, de registratie omzetten, testen
dat alles nog werkt, en dán pas de hele keten asynchroon maken — van repository
via interface tot controller.

**Wat je niet doet:** de omzetting naar async niet tegelijk met de omzetting naar
de database doen. Doe ze na elkaar, dan weet je bij een fout welke stap hem
veroorzaakte.

**Klaar wanneer:** je applicatie werkt als voorheen, je data blijft bestaan na een
herstart, en er staat nergens nog een synchrone databaseoproep.

**Valkuil:** de cursus noemt het "async all the way". Breek die keten niet
halverwege door een asynchrone aanroep geforceerd synchroon te maken — dat heft
niet alleen het voordeel op, het kan je applicatie laten vastlopen.

---

## Fase 7 — De hele hiërarchie, de conflictregels en seeding

**Lees eerst:** Programming Advanced les 06.

**Waarom nu:** dit is de grootste fase van deel 1. Je gaat van één onderdeeltype
naar het volledige domein, inclusief de conflictregels zelf.

**Wat je doet, in deze volgorde:**

1. De overige onderdeeltypes als modellen, op basis van fase 2.
2. De overerving naar tabellen vertalen. **Let op: geen van beide cursussen
   behandelt dit.** Zie `docs/ontwerp-bouwcontrole.md` sectie 5 voor de afweging
   die je moet maken; de beslissing is aan jou.
3. De conflictregels modelleren. Denk eraan dat een conflict een *verzameling*
   voorwaarden is, niet één regel — dat is wat het straks mogelijk maakt om naast
   artikelcodes ook eigenschappen als voorwaarde te gebruiken.
4. De relaties expliciet vastleggen met de Fluent API, inclusief wat er moet
   gebeuren bij verwijderen.
5. Seeding, zodat je niet met een lege database werkt.
6. Migratie aanmaken en uitvoeren.

**Wat je niet doet:** nog geen conflict-engine schrijven. Je legt hier alleen vast
wat een conflict *is*, niet hoe je hem opspoort.

**Klaar wanneer:** je database bevat je onderdelen én je conflictregels, en je kan
een handvol regels uit je incompatibiliteitenlijst er als data in terugvinden.

> Denk bij het seeden al aan je demo in fase 11. Twee regels volstaan om te
> bewijzen dat het werkt; ze overtuigen niemand op je werk. Zet er genoeg in dat
> een echte bouworder er iets zinnigs uit haalt.

**Valkuil:** de standaardinstelling bij het verwijderen van een record is in veel
gevallen "verwijder alles wat eraan hangt". De les besteedt daar niet voor niets
een waarschuwing aan. Denk na over wat er met je conflictregels gebeurt als een
onderdeel verdwijnt.

---

## Fase 8 — GenericRepository en Unit of Work

**Lees eerst:** Programming Advanced les 07.

**Waarom nu:** je hebt inmiddels een handvol modellen en je merkt dat elke
repository hetzelfde doet. Deze fase haalt die herhaling eruit.

**Wat je doet:** een generieke repository die de standaardbewerkingen één keer
afhandelt, specifieke repositories die daarvan overerven waar je iets bijzonders
nodig hebt, en een Unit of Work die ze allemaal beheert en één keer centraal
opslaat. Daarna: je controllers krijgen de Unit of Work in plaats van losse
repositories.

**Wat je niet doet:** niet elke repository specifiek maken. Alleen waar een model
werkelijk iets nodig heeft dat de generieke variant niet biedt.

**Klaar wanneer:** je controllers vragen nog maar één ding op in hun constructor,
en er staat nergens meer dubbele opzoekcode.

**Valkuil:** de les zet bewust het opslaan uit de repositories en verplaatst het
naar de Unit of Work. Dat voelt onaf. Het is het niet: dat is precies wat ervoor
zorgt dat een bewerking over meerdere tabellen in één keer slaagt of in één keer
faalt.

---

## Fase 9 — Gerelateerde data ophalen

**Lees eerst:** Programming Advanced les 08.

**Waarom nu:** een conflict met zijn voorwaarden ophalen betekent data uit
meerdere tabellen. Als je dat naïef doet, vuurt je applicatie tientallen losse
vragen af op de database voor één controle.

**Wat je doet:** de repositories die gerelateerde data nodig hebben uitbreiden
zodat ze alles in één keer ophalen. Dit hoort per model, niet in de generieke
repository — die weet niets van relaties.

**Wat je niet doet:** de tijdelijke oplossing voor eindeloze lussen die de les je
aanreikt, blijft tijdelijk. In fase 10 haal je hem weer weg.

**Klaar wanneer:** je kan een conflict met al zijn voorwaarden opvragen zonder dat
er een reeks losse vragen naar de database gaat.

**Valkuil:** deze les toont ook Dapper. Je hebt dat niet nodig — de cursus zegt
zelf dat je het concept moet kunnen uitleggen, niet toepassen. Laat je niet
verleiden om halverwege van data-toegang te wisselen.

---

## Fase 10 — DTO's en Mapster

**Lees eerst:** Programming Advanced les 09.

**Waarom nu:** je stuurt op dit moment je interne modellen de deur uit. Dat geeft
eindeloze lussen, het lekt velden die niemand buiten je applicatie hoeft te zien,
en het betekent dat elke wijziging aan je model je clients breekt.

**Wat je doet:** aparte objecten voor wat je terugstuurt en voor wat je ontvangt.
De validatieregels verhuizen van je modellen naar de objecten die binnenkomen.
Daarna Mapster installeren en de omzetting configureren, zodat je controllers kort
blijven.

**Wat je niet doet:** geen modellen in je DTO's. Een DTO mag naar een andere DTO
verwijzen, maar niet naar een model — anders heb je het probleem dat je aan het
oplossen was gewoon verplaatst.

**Klaar wanneer:** elk endpoint geeft een DTO terug, ontvangt een DTO, en de
tijdelijke oplossing uit fase 9 is uit `Program.cs` verdwenen.

**Valkuil:** de cursus onderwijst Mapster, maar veel voorbeelden op het internet
gebruiken AutoMapper. Dat is een andere library met een andere syntax — en sinds
2025 ook een commerciële licentie. Meng ze niet.

> Houd je eigenschapsnamen tussen model en DTO gelijk waar dat kan. Alles wat
> gelijk heet, hoef je niet te configureren.

---

## Fase 11 — De conflict-engine, en je demo

**Lees eerst:** niets nieuws. Je hebt alles wat je nodig hebt.

**Waarom nu:** dit is het doel van deel 1. Alles eronder staat — modellen,
database, conflictregels, repositories, DTO's. Wat ontbreekt is het stuk dat de
eigenlijke vraag beantwoordt: gaan deze onderdelen samen?

**Wat je doet:**

1. De engine als een gewone klasse, geregistreerd zoals je in les 03 geleerd hebt.
   Hij krijgt onderdelen en conflictregels binnen en geeft een resultaat terug.
   Geen database erin, geen HTTP.
2. Een endpoint dat een bouworder aanneemt en dat resultaat teruggeeft. Dit is het
   endpoint zonder databasetabel erachter — denk na over welk verb erbij past.
3. Genoeg seed-data zodat een echte bouworder iets zinnigs oplevert.

**Twee dingen om over na te denken vóór je begint:**

Het resultaat moet meer dragen dan "dit conflict vuurde". De mockup kleurt de
regel van een ónderdeel. Vraag jezelf af wat de engine moet teruggeven opdat het
scherm straks weet wélke regels het moet kleuren.

Als een conflict twee voorwaarden heeft en er is maar één onderdeel dat aan
allebei voldoet — vuurt het dan? Beantwoord dat vóór je de controle schrijft. De
voor de hand liggende manier om "zijn alle voorwaarden vervuld?" op te schrijven
geeft hier het verkeerde antwoord.

**Wat je niet doet:** de engine niet in een controller schrijven, en niet in een
repository. En laat hem niet zelf de database aanspreken, hoe verleidelijk ook —
zodra hij dat doet, kan je hem in fase 12 niet meer testen zonder database.

**Klaar wanneer:** je opent Scalar, stuurt een bouworder met een reeks
artikelcodes in, en krijgt de juiste conflicten terug. Dit is wat je toont.

**De prijs van deze keuze.** Je hebt hier nog geen testframework. Je controleert
met de hand, via Scalar, tegen een handvol gevallen uit je lijst waarvan je het
antwoord al kent. Dat is genoeg om te tonen, niet genoeg om zeker te zijn. Fase 12
haalt daar waarschijnlijk fouten uit. Reken daarop en schrik er niet van.

**Valkuil:** een demo die alleen werkt met de artikelcodes die jij toevallig hebt
uitgeprobeerd. Vraag iemand anders een bouworder te kiezen voor je hem toont.

---

# DEEL 2 — .NET Development

---

## Fase 12 — Tests rond de engine

**Lees eerst:** .NET Development les 08.

**Waarom nu:** je engine werkt, maar je weet alleen dat hij werkt voor de gevallen
die je zelf geprobeerd hebt. Deze fase maakt daar zekerheid van. De les legt ook
uit waarom juist dit stuk testbaar is: controllers zijn het slecht door hun
returntypes, repositories test je niet omdat data verandert. Services blijven over.

**Wat je doet:** het testproject dat sinds fase 0 leeg stond inrichten — NUnit en
Moq erbij — en tests schrijven rond de engine zoals hij er nu staat.

De les beschrijft test-first: eerst de test, dan de code. Dat past hier niet meer,
want je engine bestaat al. Pas het toe op alles wat je ná deze fase nog bouwt.

De testgevallen leid je zelf af uit je incompatibiliteitenlijst. Denk aan wat er
moet gebeuren bij een lege order, bij een regel die vuurt, bij een regel die net
níét vuurt, bij een regel die uitgeschakeld is, en bij een onderdeel dat in
meerdere conflicten zit.

**Wat je niet doet:** geen tests schrijven die je engine bevestigen in wat hij nu
toevallig doet. Je test wat er hóórt te gebeuren, en dat leid je af uit je lijst —
niet uit je eigen code.

**Klaar wanneer:** je tests draaien groen, en er zit minstens één geval bij dat je
in fase 11 niet met de hand geprobeerd had.

**Valkuil:** vind je een fout — en de kans is groot — pas dan niet meteen je
engine aan. Schrijf eerst de test die faalt, en herstel daarna pas. Anders weet je
niet of je het werkelijk opgelost hebt.

> Heb je veel mocks nodig om je engine te kunnen testen, dan is dat een teken dat
> hij te veel weet. Dat is bruikbare informatie over je ontwerp, geen tegenslag.

---

## Fase 13 — Identity, gebruikers en rollen

**Lees eerst:** .NET Development les 06, daarna les 07.

**Waarom nu:** je conflictbeheer mag niet door iedereen te wijzigen zijn. Dit is
ook de fase waar je moet beslissen wie op de werkvloer wat mag — een van de open
vragen uit het ontwerpdocument.

**Wat je doet:** de Identity-packages toevoegen, je databasecontext laten overerven
van de Identity-variant, een eigen gebruikersmodel met de extra velden die je
nodig hebt, registratie in `Program.cs`, en de migratie. Daarna rollen, en je
endpoints afschermen.

**Wat je niet doet:** geen wachtwoorden hard in je code zetten die daar blijven
staan. De les maakt via seeding een beheerder aan en waarschuwt er zelf voor: die
moet bij een eerste aanmelding zijn wachtwoord wijzigen.

**Klaar wanneer:** je kan aanmelden, je krijgt een token, en een endpoint dat je
afgeschermd hebt weigert je zonder dat token.

**Valkuil:** de JWT-opzet in les 06 bevat instellingen die er specifiek in staan om
een fout in .NET 8 te omzeilen. Ze zetten bewust een oudere, tragere
token-afhandeling aan. Op .NET 10 heb je dat niet nodig. Neem die regels niet
blind over — vraag het na bij je docent als je twijfelt.

> Scalar heeft een eigen veld om je token te plakken. De les beschrijft dat voor
> Swagger; het idee is hetzelfde, de knop staat alleen elders.

---

## Fase 14 — Het scherm

**Lees eerst:** .NET Development les 09, daarna les 10.

**Waarom nu:** alles wat eronder zit werkt en is getest. Nu pas bouw je het scherm
uit de mockup.

**Wat je doet:** de MVC-onderdelen toevoegen aan hetzelfde project — les 09 zet
twee manieren naast elkaar en Bouwcontrole neemt de tweede, waarbij front-end en
back-end samen in één applicatie zitten. Daarna: ViewModels voor wat het scherm
nodig heeft, en de views zelf.

Werk toe naar het ene scherm uit de mockup: een scanzone, een kolom met gescande
onderdelen, en een paneel met gevonden conflicten.

**Wat je niet doet:** de kleuren groen, oranje en rood zijn een beslissing van het
scherm, niet van je API. Je backend geeft een ernst terug; de view maakt daar een
kleur van. Zet je kleur in je DTO, dan kan een tweede client er nooit iets anders
van maken.

**Klaar wanneer:** je scant een reeks codes, en het scherm toont per onderdeel de
juiste kleur en onderaan de gevonden conflicten.

**Valkuil:** ViewModels zijn niet hetzelfde als DTO's, ook al lijken ze erop. Ze
horen in een eigen map en dienen één scherm. Probeer je DTO's niet te hergebruiken
als ViewModel.

---

## Fase 15 — Keuzelijsten, aanmeldschermen en rechten in de UI

**Lees eerst:** .NET Development les 11.

**Waarom nu:** de afwerking. Het conflictbeheer heeft keuzelijsten nodig, en de
beveiliging uit fase 13 moet ook zichtbaar zijn in het scherm.

**Wat je doet:** keuzelijsten in je formulieren, de aanmeld- en
gebruikersbeheerschermen, en de instelling die bepaalt waar iemand belandt als hij
naar een pagina gaat waar hij geen rechten op heeft.

**Wat je niet doet:** niet vertrouwen op het verbergen van knoppen als beveiliging.
Een knop die je niet ziet, is nog steeds een adres dat iemand kan intypen.

**Klaar wanneer:** iemand zonder beheerrechten kan controleren maar geen
conflictregels wijzigen, en ziet die knoppen ook niet staan.

**Valkuil:** rolnamen zijn hoofdlettergevoelig en moeten exact overeenkomen met wat
in de database staat. Een typfout hier geeft je geen foutmelding — alleen een
gebruiker die onterecht geen toegang krijgt.

---

## Wat hierna komt

Dit plan brengt je tot een werkende applicatie met de volledige leerstof van beide
vakken erin. Wat daarbuiten valt en pas daarna aan de beurt is:

- De onderdelenlijst vullen met de scraper — zie `docs/scrapy-handleiding.md`.
- Een uitwijk voor een bouwplek zonder netwerk.
- De vijf open vragen uit `docs/ontwerp-bouwcontrole.md`, waarvan er twee fase 2
  blokkeren.

---

## De regels waaronder dit plan geschreven is

Uit `docs/projectafspraken.md`:

> Geen code schrijven. Geen expliciete voorbeelden die het antwoord voorzeggen.
> Geen andere cruciale hulpmiddelen die het denkwerk overnemen. Het volledige
> project moet door mij gemaakt worden. Hulp mag uitsluitend in de vorm van tips
> en richting geven.

Loop je vast in een fase, vraag het dan. Je krijgt een aanwijzing, een vraag terug,
of de plek in de les waar het staat — geen uitwerking.
