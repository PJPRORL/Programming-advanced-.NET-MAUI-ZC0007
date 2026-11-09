# Conflictanalyse PC-onderdelen

Analyse ter voorbereiding van de datamodellen van Bouwcontrole. Bepaalt welke
eigenschappen per onderdeeltype in een conflictregel voorkomen — en dus welke in
het model horen.

> **Status: concept, te toetsen aan de incompatibiliteitenlijst van Alternate.**
> Dit is algemene bouwkennis, geen bedrijfskennis. Waar deze analyse en jullie
> eigen lijst verschillen, wint jullie lijst: die is gebaseerd op wat er werkelijk
> misgaat, deze op wat er in theorie kan misgaan.
>
> Opgesteld 19 september 2026.

---

## Hoe je dit gebruikt

1. Loop Deel 1 af en schrap wat bij jullie nooit voorkomt.
2. Vul aan met wat op jullie lijst staat en hier ontbreekt — dat is het
   waardevolste deel van de oefening.
3. Pas dan Deel 3 gebruiken als vertrekpunt voor je modellen.

Alles onder **Te verifiëren** is onzeker. Neem dat niet over zonder controle.

---

# Deel 1 — Conflictoverzicht

## Paarsgewijs

| Id | Onderdelen | Conflict | Wat er misgaat |
|---|---|---|---|
| `CPU-MB-SOCKET` | processor · moederbord | Socket komt niet overeen | De processor past fysiek niet in het bord |
| `CPU-MB-BIOS` | processor · moederbord | Generatie niet ondersteund | Juiste socket, maar het bord herkent deze processor niet zonder BIOS-update |
| `CPU-KOEL-MOUNT` | processor · koeler | Bevestiging past niet | De koeler heeft geen montageset voor deze socket |
| `CPU-KOEL-TDP` | processor · koeler | Koeler te licht | De koeler kan het warmtevermogen van de processor niet afvoeren |
| `RAM-MB-TYPE` | geheugen · moederbord | Verkeerd geheugentype | DDR4-module in een DDR5-bord of omgekeerd; past fysiek niet |
| `RAM-MB-SLOTS` | geheugen · moederbord | Te veel modules | Meer modules besteld dan het bord sloten heeft |
| `RAM-MB-MAX` | geheugen · moederbord | Boven de maximumcapaciteit | Totale capaciteit hoger dan het bord ondersteunt |
| `RAM-CPU-SNELHEID` | geheugen · processor | Snelheid niet officieel ondersteund | Draait alleen op volle snelheid met een profiel ingeschakeld |
| `RAM-MB-PROFIEL` | geheugen · moederbord | Profieltype niet ondersteund | Een EXPO-kit op een bord dat alleen XMP kent valt terug op de basissnelheid |
| `RAM-KOEL-HOOGTE` | geheugen · koeler | Module past niet onder de koeler | Hoge modules botsen tegen een brede luchtkoeler |
| `M2-MB-AANTAL` | SSD · moederbord | Te veel M.2-drives | Meer M.2-SSD's dan het bord sloten heeft |
| `M2-MB-LENGTE` | SSD · moederbord | Lengte past niet | Het slot ondersteunt de lengte van deze SSD niet |
| `M2-MB-INTERFACE` | SSD · moederbord | Interface komt niet overeen | Een SATA-M.2 in een slot dat alleen PCIe voert, of omgekeerd |
| `M2-MB-GENERATIE` | SSD · moederbord | Slot trager dan de SSD | De SSD werkt, maar haalt zijn snelheid niet in een slot van een oudere generatie |
| `SATA-MB-POORTEN` | SSD/HDD · moederbord | Te veel SATA-drives | Meer SATA-schijven dan het bord poorten heeft |
| `DRIVE-KST-PLAATSEN` | SSD/HDD · behuizing | Te weinig montageplaatsen | Meer schijven dan de behuizing kan dragen, per formaat gerekend |
| `GPU-KST-LENGTE` | videokaart · behuizing | Kaart te lang | De kaart past niet in de beschikbare lengte |
| `GPU-KST-DIKTE` | videokaart · behuizing | Kaart te dik | De kaart beslaat meer uitbreidingsposities dan de behuizing heeft |
| `GPU-VOE-CONNECTOR` | videokaart · voeding | Stroomaansluiting ontbreekt | De voeding heeft niet het aantal of type aansluitingen dat de kaart vraagt |
| `MB-KST-FORMFACTOR` | moederbord · behuizing | Formfactor past niet | Het bord past niet op de montageplaat |
| `MB-KST-BACKCONNECT` | moederbord · behuizing | Achterzijde-aansluitingen | Een bord met aansluitingen op de achterkant past alleen in een daarvoor gemaakte behuizing |
| `MB-KST-FRONTUSB` | moederbord · behuizing | Te weinig interne USB | De frontpoorten van de behuizing kunnen niet allemaal aangesloten worden |
| `MB-KST-USBC` | moederbord · behuizing | Geen interne USB-C | De behuizing heeft een USB-C-frontpoort, het bord heeft er geen aansluiting voor |
| `KOEL-KST-HOOGTE` | koeler · behuizing | Luchtkoeler te hoog | Het zijpaneel sluit niet |
| `KOEL-KST-RADIATOR` | koeler · behuizing | Radiator past niet | De behuizing heeft geen montageplaats voor deze radiatormaat |
| `FAN-KST-MAAT` | casefans · behuizing | Ventilatormaat past niet | De behuizing heeft geen plaats voor ventilatoren van deze afmeting |
| `FAN-KST-AANTAL` | casefans · behuizing | Te veel ventilatoren | Meer ventilatoren dan montageplaatsen |
| `FAN-MB-HEADERS` | casefans · moederbord | Te weinig aansluitingen | Meer ventilatoren dan het bord kan aansturen |
| `FAN-MB-RGB` | casefans · moederbord | Verlichting niet aanstuurbaar | Verkeerd type of te weinig verlichtingsaansluitingen |
| `FAN-MB-USB` | casefans · moederbord | Geen interne USB vrij | De meegeleverde controller vraagt een interne USB-aansluiting die het bord niet vrij heeft |
| `VOE-KST-FORMAAT` | voeding · behuizing | Voeding past niet | Verkeerde formfactor, of te lang voor het compartiment |
| `VOE-MB-EPS` | voeding · moederbord | CPU-stroomaansluiting ontbreekt | Het bord vraagt meer EPS-aansluitingen dan de voeding levert |
| `VOE-DRIVE-SATA` | voeding · SSD/HDD | Te weinig SATA-stroom | Meer schijven dan de voeding van stroom kan voorzien |

## Binnen hetzelfde type

| Id | Onderdeel | Conflict | Wat er misgaat |
|---|---|---|---|
| `RAM-RAM-MENGEN` | geheugen | Modules uit verschillende sets | Modules die niet samen getest zijn draaien vaak niet op de opgegeven snelheid |
| `RAM-RAM-KANALEN` | geheugen | Aantal past niet bij de kanalen | Een oneven aantal modules in een bord met twee kanalen werkt, maar niet optimaal |

## Eén onderdeel tegen het totaal

| Id | Onderdeel | Conflict | Wat er misgaat |
|---|---|---|---|
| `VOE-TOT-VERMOGEN` | voeding | Onvoldoende vermogen | Het opgetelde verbruik van de build ligt te dicht bij of boven wat de voeding levert |
| `CPU-TOT-BEELD` | processor | Geen beelduitgang | Een processor zonder geïntegreerde grafische kern, en geen videokaart in de order |
| `KST-TOT-KOELING` | behuizing | Te weinig luchtstroom | Warme onderdelen in een behuizing met weinig of geen ventilatoren |
| `KLEUR-TOT-MIX` | alle zichtbare onderdelen | Kleuren lopen door elkaar | De order bevat zichtbare onderdelen in meer dan één kleur |

## Drie onderdelen samen

| Id | Onderdelen | Conflict | Wat er misgaat |
|---|---|---|---|
| `TRI-RAD-GPU-KST` | koeler · videokaart · behuizing | Radiator verkort de kaartruimte | Een radiator vooraan neemt lengte weg die de videokaart nodig heeft |
| `TRI-RAD-FAN-KST` | koeler · casefans · behuizing | Dubbel bezette montageplaats | Radiator en ventilatoren zijn voor dezelfde plaats bedoeld |
| `TRI-RAM-KOEL-MB` | geheugen · koeler · moederbord | Slotpositie onder de koeler | Alleen het slot het dichtst bij de processor botst; met minder modules speelt het niet |
| `TRI-USB-FAN-KST` | casefans · behuizing · moederbord | Concurrentie om interne USB | De frontpoorten van de behuizing en de ventilatorcontroller vragen allebei een interne USB-aansluiting |

Alle tien onderdeeltypes uit de opdracht komen in minstens één regel voor.

**Nieuw sinds de eerste versie:** `RAM-MB-PROFIEL`, `M2-MB-GENERATIE`, `FAN-MB-USB`,
`KLEUR-TOT-MIX` en `TRI-USB-FAN-KST`. Samen 43 regels.

---

# Deel 2 — Conflictregels

Ernst: **hard** = de bouw kan niet · **waarschuwing** = de bouw kan, met gevolgen.

## Processor, moederbord en koeler

| Id | Eigenschappen nodig | Oordeel | Ernst | Eenheid / formaat |
|---|---|---|---|---|
| `CPU-MB-SOCKET` | socket van beide | Ongelijk → niet OK | hard | keuzelijst |
| `CPU-MB-BIOS` | ondersteunde processorlijst of generatie van het bord; generatie van de processor | Niet in de lijst → niet OK. Wel in de lijst maar alleen na update → opmerking; heeft het bord geen update-zonder-processor, dan niet OK | hard of waarschuwing | keuzelijst + ja/nee |
| `CPU-KOEL-MOUNT` | ondersteunde sockets van de koeler; socket van de processor | Socket niet in de lijst → niet OK | hard | reeks keuzewaarden |
| `CPU-KOEL-TDP` | warmtevermogen processor; koelvermogen koeler | Koeler lager → niet OK. Minder dan een marge erboven → opmerking | waarschuwing | W |

## Geheugen

| Id | Eigenschappen nodig | Oordeel | Ernst | Eenheid / formaat |
|---|---|---|---|---|
| `RAM-MB-TYPE` | geheugentype van beide | Ongelijk → niet OK | hard | keuzelijst |
| `RAM-MB-SLOTS` | aantal sloten op het bord; aantal modules in de order | Modules meer dan sloten → niet OK | hard | aantal |
| `RAM-MB-MAX` | maximumcapaciteit bord; capaciteit per module × aantal | Totaal hoger → niet OK | hard | GB |
| `RAM-CPU-SNELHEID` | snelheid module; officieel ondersteunde snelheid van de processor | Module hoger → opmerking, profiel nodig | waarschuwing | MT/s |
| `RAM-MB-PROFIEL` | profieltype van de kit; profieltypes die het bord ondersteunt | Niet ondersteund → opmerking, draait op basissnelheid | waarschuwing | keuzelijst (XMP/EXPO) |
| `RAM-KOEL-HOOGTE` | hoogte module; vrije hoogte boven de sloten bij de koeler | Module hoger → niet OK | hard | mm |
| `RAM-RAM-MENGEN` | set-aanduiding of artikelcode van elke module | Verschillende sets → opmerking | waarschuwing | tekst |
| `RAM-RAM-KANALEN` | aantal kanalen van het bord; aantal modules | Aantal niet deelbaar door de kanalen → opmerking | waarschuwing | aantal |

## Opslag

| Id | Eigenschappen nodig | Oordeel | Ernst | Eenheid / formaat |
|---|---|---|---|---|
| `M2-MB-AANTAL` | aantal M.2-sloten; aantal M.2-drives | Drives meer dan sloten → niet OK | hard | aantal |
| `M2-MB-LENGTE` | lengtes die elk slot toelaat; lengte van de SSD | Geen enkel vrij slot dat deze lengte toelaat → niet OK | hard | keuzelijst (2242/2260/2280/22110) |
| `M2-MB-INTERFACE` | interface per slot; interface van de SSD | Geen vrij slot met de juiste interface → niet OK | hard | keuzelijst (PCIe/SATA) |
| `M2-MB-GENERATIE` | PCIe-generatie per slot; PCIe-generatie van de SSD | SSD hoger dan het vrije slot → opmerking, werkt trager | waarschuwing | getal (generatie) |
| `SATA-MB-POORTEN` | aantal SATA-poorten; aantal SATA-drives | Drives meer dan poorten → niet OK | hard | aantal |
| `DRIVE-KST-PLAATSEN` | aantal plaatsen per formaat in de behuizing; formaat per drive | Meer drives van een formaat dan plaatsen → niet OK | hard | aantal per keuzewaarde |
| `VOE-DRIVE-SATA` | aantal SATA-stroomaansluitingen; aantal drives die er een nodig hebben | Meer drives dan aansluitingen → opmerking, splitter nodig | waarschuwing | aantal |

## Videokaart

| Id | Eigenschappen nodig | Oordeel | Ernst | Eenheid / formaat |
|---|---|---|---|---|
| `GPU-KST-LENGTE` | lengte kaart; maximale kaartlengte behuizing | Kaart langer → niet OK | hard | mm |
| `GPU-KST-DIKTE` | dikte kaart in posities; aantal uitbreidingsposities behuizing | Kaart beslaat er meer dan beschikbaar → niet OK | hard | aantal posities |
| `GPU-VOE-CONNECTOR` | type en aantal aansluitingen die de kaart vraagt; wat de voeding levert | Voeding levert minder of het verkeerde type → niet OK | hard | keuzelijst + aantal |
| `CPU-TOT-BEELD` | heeft de processor een geïntegreerde grafische kern; zit er een videokaart in de order | Geen van beide → niet OK | hard | ja/nee |

## Behuizing en moederbord

| Id | Eigenschappen nodig | Oordeel | Ernst | Eenheid / formaat |
|---|---|---|---|---|
| `MB-KST-FORMFACTOR` | formfactor bord; ondersteunde formfactors behuizing | Niet in de lijst → niet OK | hard | keuzelijst + reeks |
| `MB-KST-BACKCONNECT` | achterzijde-aansluitingen ja/nee bij beide | Verschillend → niet OK, in beide richtingen | hard | ja/nee |
| `MB-KST-FRONTUSB` | interne USB-aansluitingen op het bord per type; frontpoorten van de behuizing per type | Behuizing vraagt er meer dan het bord biedt → opmerking | waarschuwing | aantal per keuzewaarde |
| `MB-KST-USBC` | interne USB-C-aansluiting op het bord ja/nee; USB-C-frontpoort behuizing ja/nee | Behuizing wel, bord niet → opmerking | waarschuwing | ja/nee |

## Koeling

| Id | Eigenschappen nodig | Oordeel | Ernst | Eenheid / formaat |
|---|---|---|---|---|
| `KOEL-KST-HOOGTE` | hoogte luchtkoeler; maximale koelerhoogte behuizing | Koeler hoger → niet OK | hard | mm |
| `KOEL-KST-RADIATOR` | radiatormaat; radiatormaten die de behuizing ondersteunt | Maat niet ondersteund → niet OK | hard | keuzelijst + reeks |
| `FAN-KST-MAAT` | afmeting ventilator; afmetingen die de behuizing ondersteunt | Niet ondersteund → niet OK | hard | keuzelijst |
| `FAN-KST-AANTAL` | aantal montageplaatsen; aantal ventilatoren in de order | Meer ventilatoren dan plaatsen → niet OK | hard | aantal |
| `FAN-MB-HEADERS` | aantal ventilatoraansluitingen op het bord; aantal aan te sluiten ventilatoren | Meer ventilatoren dan aansluitingen → opmerking, hub nodig | waarschuwing | aantal |
| `FAN-MB-RGB` | type en aantal verlichtingsaansluitingen op het bord; verlichtingstype van de ventilatoren; controller meegeleverd ja/nee | Met controller → niet van toepassing. Zonder: type ontbreekt of te weinig aansluitingen → opmerking | waarschuwing | keuzelijst + aantal |
| `FAN-MB-USB` | vraagt de set een interne USB-aansluiting; interne USB-aansluitingen op het bord | Set vraagt er een en het bord heeft er geen vrij → opmerking | waarschuwing | ja/nee + aantal |
| `KST-TOT-KOELING` | aantal meegeleverde of bestelde ventilatoren; warmteafgifte van processor en videokaart | Geen of één ventilator bij hoge warmteafgifte → opmerking | waarschuwing | aantal + W |

## Voeding

| Id | Eigenschappen nodig | Oordeel | Ernst | Eenheid / formaat |
|---|---|---|---|---|
| `VOE-KST-FORMAAT` | formfactor voeding; ondersteunde formfactors behuizing; lengte voeding; maximale lengte behuizing | Formfactor niet ondersteund of voeding langer → niet OK | hard | keuzelijst + mm |
| `VOE-MB-EPS` | aantal en type CPU-aansluitingen die het bord vraagt; wat de voeding levert | Voeding levert minder → niet OK | hard | aantal per keuzewaarde |
| `VOE-TOT-VERMOGEN` | vermogen voeding; warmtevermogen processor; stroomverbruik videokaart; aantal overige onderdelen | Optelling boven het vermogen → niet OK. Binnen een marge eronder → opmerking | hard of waarschuwing | W |

## Kleur

| Id | Eigenschappen nodig | Oordeel | Ernst | Eenheid / formaat |
|---|---|---|---|---|
| `KLEUR-TOT-MIX` | kleurtype van elk zichtbaar onderdeel in de order | Meer dan één kleur onder de zichtbare onderdelen → opmerking | waarschuwing | keuzelijst |

## Drie onderdelen samen

| Id | Eigenschappen nodig | Oordeel | Ernst | Eenheid / formaat |
|---|---|---|---|---|
| `TRI-RAD-GPU-KST` | radiatordikte plus ventilatordikte; maximale kaartlengte mét radiator vooraan; lengte kaart | Kaart langer dan de gereduceerde ruimte → niet OK | hard | mm |
| `TRI-RAD-FAN-KST` | montageplaatsen behuizing; plaatsen die de radiator inneemt; aantal ventilatoren | Samen meer plaatsen nodig dan beschikbaar → opmerking | waarschuwing | aantal |
| `TRI-RAM-KOEL-MB` | hoogte modules; vrije hoogte bij de koeler; aantal modules; aantal sloten | Alleen conflict als het slot naast de processor ook bezet raakt → opmerking | waarschuwing | mm + aantal |
| `TRI-USB-FAN-KST` | interne USB-aansluitingen op het bord; USB-behoefte van de frontpoorten; USB-behoefte van de ventilatorcontroller | Samen meer gevraagd dan het bord biedt → opmerking | waarschuwing | aantal |

---

# Deel 3 — Attributenlijst per onderdeeltype

Per onderdeel de eigenschappen die in minstens één regel uit Deel 2 voorkomen.
Achter elke eigenschap staat het soort waarde en de regels die hem gebruiken.
Wat meerdere waarden heeft of niet van een productpagina komt, staat er apart bij.

---

## Moederbord

- **socket** — keuzelijst · `CPU-MB-SOCKET`, `CPU-KOEL-MOUNT`
- **formfactor** — ATX, mATX, ITX of E-ATX · `MB-KST-FORMFACTOR`
- **aansluitingen op de achterzijde** — ja/nee · `MB-KST-BACKCONNECT`
- **geheugentype** — DDR4 of DDR5 · `RAM-MB-TYPE`
- **aantal geheugensloten** — getal · `RAM-MB-SLOTS`, `TRI-RAM-KOEL-MB`
- **aantal geheugenkanalen** — getal · `RAM-RAM-KANALEN`
- **maximale geheugencapaciteit** — GB · `RAM-MB-MAX`
- **ondersteunde geheugenprofielen** — XMP, EXPO of beide · `RAM-MB-PROFIEL`
- **aantal SATA-poorten** — getal · `SATA-MB-POORTEN`
- **aantal ventilatoraansluitingen** — getal · `FAN-MB-HEADERS`
- **kleurtype** — zwart, wit of geen · `KLEUR-TOT-MIX`

De **M.2-sloten** zijn geen eigenschap maar een lijst: een bord heeft er meerdere
en ze verschillen onderling. Per slot heb je de toegelaten lengtes nodig
(2242 tot 22110, `M2-MB-LENGTE`), de interface (PCIe of SATA, `M2-MB-INTERFACE`)
en de PCIe-generatie (`M2-MB-GENERATIE`). Je eigen productpagina liet zien dat
slot 1 een andere generatie kan hebben dan slot 2 en 3 — dat verschil is precies
wat je hier vasthoudt.

Ook per type geteld, dus meerdere waarden: de **interne USB-aansluitingen**
(`MB-KST-FRONTUSB`, `FAN-MB-USB`, `TRI-USB-FAN-KST`), de
**verlichtingsaansluitingen** (`FAN-MB-RGB`) en de **CPU-stroomaansluitingen**
(`VOE-MB-EPS`). Daarnaast een losse ja/nee voor de **interne USB-C-aansluiting**
(`MB-KST-USBC`).

Twee dingen komen niet van de productpagina. De **ondersteunde
processorgeneraties** staan bij de fabrikant op een aparte pagina
(`CPU-MB-BIOS`), en of het bord **een BIOS-update zonder processor** aankan staat
er vaak helemaal niet (`CPU-MB-BIOS`).

---

## Processor

- **socket** — keuzelijst · `CPU-MB-SOCKET`, `CPU-KOEL-MOUNT`
- **generatie** — keuzelijst · `CPU-MB-BIOS`
- **warmtevermogen** — W · `CPU-KOEL-TDP`, `VOE-TOT-VERMOGEN`, `KST-TOT-KOELING`
- **officieel ondersteunde geheugensnelheid** — MT/s · `RAM-CPU-SNELHEID`
- **geïntegreerde grafische kern** — ja/nee · `CPU-TOT-BEELD`
- **kleurtype** — zwart, wit of geen · `KLEUR-TOT-MIX`

Een processor is in een gebouwd systeem nooit zichtbaar; zijn kleur zal in de
praktijk altijd "geen" zijn. De eigenschap staat er alleen omdat elk onderdeel
hem draagt.

---

## CPU-koeler

- **soort koeling** — lucht of AIO · bepaalt welke van de regels hieronder gelden
- **ondersteunde sockets** — meerdere keuzewaarden · `CPU-KOEL-MOUNT`
- **koelvermogen** — W · `CPU-KOEL-TDP`
- **kleurtype** — zwart, wit of geen · `KLEUR-TOT-MIX`

Alleen bij **luchtkoeling**: de **hoogte** in mm (`KOEL-KST-HOOGTE`) en de **vrije
hoogte boven de geheugensloten** (`RAM-KOEL-HOOGTE`, `TRI-RAM-KOEL-MB`).

Alleen bij **AIO**: de **radiatormaat** — 120 tot 420 (`KOEL-KST-RADIATOR`,
`TRI-RAD-FAN-KST`) — en de **radiatordikte** in mm (`TRI-RAD-GPU-KST`).

Het koelvermogen en de vrije hoogte bij de sloten moet je zelf opzoeken. Voor het
koelvermogen bestaat geen genormeerde meetmethode, dus reken op schattingen.

---

## Geheugen

- **geheugentype** — DDR4 of DDR5 · `RAM-MB-TYPE`
- **capaciteit per module** — GB · `RAM-MB-MAX`
- **aantal modules in de set** — getal · `RAM-MB-SLOTS`, `RAM-RAM-KANALEN`
- **snelheid** — MT/s · `RAM-CPU-SNELHEID`
- **profieltype** — XMP, EXPO, beide of geen · `RAM-MB-PROFIEL`
- **set-aanduiding** — tekst · `RAM-RAM-MENGEN`
- **hoogte** — mm · `RAM-KOEL-HOOGTE`, `TRI-RAM-KOEL-MB`
- **kleurtype** — zwart, wit of geen · `KLEUR-TOT-MIX`

De hoogte staat zelden op een productpagina, terwijl het conflict met brede
luchtkoelers er volledig van afhangt. Reken erop dat je die met de hand invult.

---

## Videokaart

- **lengte** — mm · `GPU-KST-LENGTE`, `TRI-RAD-GPU-KST`
- **dikte in posities** — getal · `GPU-KST-DIKTE`
- **stroomverbruik** — W · `VOE-TOT-VERMOGEN`, `KST-TOT-KOELING`
- **kleurtype** — zwart, wit of geen · `KLEUR-TOT-MIX`

De **stroomaansluitingen** worden per type geteld en zijn dus meerdere waarden:
hoeveel achtpins, hoeveel 12V-2x6 (`GPU-VOE-CONNECTOR`).

Eerder stond hier "warmteafgifte". Dat is voor een videokaart hetzelfde getal,
maar de productpagina noemt het stroomverbruik en die regels gaan over de voeding
— dus houden we die naam aan.

---

## SSD

- **aansluitvorm** — M.2 of SATA 2,5" · bepaalt welke regels gelden
- **kleurtype** — zwart, wit of geen · `KLEUR-TOT-MIX`

Alleen bij **M.2**: de **lengte** (2242 tot 22110, `M2-MB-LENGTE`), de
**interface** (PCIe of SATA, `M2-MB-INTERFACE`) en de **PCIe-generatie**
(`M2-MB-GENERATIE`). Samen met `M2-MB-AANTAL` bepalen die of er een geschikt slot
vrij is.

Alleen bij **SATA**: het **formaat** — 2,5" (`DRIVE-KST-PLAATSEN`). Verder gelden
`SATA-MB-POORTEN` en `VOE-DRIVE-SATA`, en daarvoor volstaat dat het om een
SATA-schijf gaat.

---

## HDD

- **formaat** — 3,5" of 2,5" · `DRIVE-KST-PLAATSEN`
- **kleurtype** — zwart, wit of geen · `KLEUR-TOT-MIX`

Verder deelt de HDD `SATA-MB-POORTEN` en `VOE-DRIVE-SATA` met de SATA-SSD. Net
als een processor zit een schijf meestal uit het zicht.

---

## PC-behuizing

- **geschikt voor achterzijde-aansluitingen** — ja/nee · `MB-KST-BACKCONNECT`
- **maximale koelerhoogte** — mm · `KOEL-KST-HOOGTE`
- **maximale kaartlengte** — mm · `GPU-KST-LENGTE`
- **aantal uitbreidingsposities** — getal · `GPU-KST-DIKTE`
- **aantal ventilatorplaatsen** — getal · `FAN-KST-AANTAL`, `TRI-RAD-FAN-KST`
- **aantal meegeleverde ventilatoren** — getal · `KST-TOT-KOELING`
- **USB-C-frontpoort** — ja/nee · `MB-KST-USBC`
- **maximale voedingslengte** — mm · `VOE-KST-FORMAAT`
- **kleurtype** — zwart of wit · `KLEUR-TOT-MIX`

Vier eigenschappen dragen meerdere waarden: de **ondersteunde formfactors**
(`MB-KST-FORMFACTOR`), de **ondersteunde radiatormaten** (`KOEL-KST-RADIATOR`,
`TRI-RAD-FAN-KST`), de **ondersteunde ventilatorafmetingen** (`FAN-KST-MAAT`) en
de **ondersteunde voedingsformfactors** (`VOE-KST-FORMAAT`). Daarnaast worden de
**montageplaatsen per drive-formaat** (`DRIVE-KST-PLAATSEN`) en de **frontpoorten
per type** (`MB-KST-FRONTUSB`, `TRI-USB-FAN-KST`) per soort geteld.

Twee getallen ontbreken bijna altijd op een productpagina: de **maximale
kaartlengte mét radiator vooraan** (`TRI-RAD-GPU-KST`) en de maximale
voedingslengte. Behuizingen vermelden alleen het gunstigste geval.

---

## Voeding

- **formfactor** — ATX, SFX of SFX-L · `VOE-KST-FORMAAT`
- **lengte** — mm · `VOE-KST-FORMAAT`
- **vermogen** — W · `VOE-TOT-VERMOGEN`
- **aantal SATA-stroomaansluitingen** — getal · `VOE-DRIVE-SATA`
- **kleurtype** — zwart, wit of geen · `KLEUR-TOT-MIX`

Per type geteld: de **CPU-stroomaansluitingen** (`VOE-MB-EPS`) en de
**kaartaansluitingen** (`GPU-VOE-CONNECTOR`). Bij die laatste gaat het om het
type én het aantal — een voeding met twee achtpins is iets anders dan een met één
12V-2x6.

De lengte wordt vaak niet vermeld, terwijl ze in kleine behuizingen beslissend is.

---

## Casefans

- **afmeting** — 120 of 140 mm · `FAN-KST-MAAT`
- **aantal in de set** — getal · `FAN-KST-AANTAL`, `FAN-MB-HEADERS`
- **verlichtingstype** — geen, RGB of ARGB · `FAN-MB-RGB`
- **controller meegeleverd** — ja/nee · `FAN-MB-HEADERS`, `FAN-MB-RGB`
- **interne USB nodig** — ja/nee · `FAN-MB-USB`, `TRI-USB-FAN-KST`
- **kleurtype** — zwart of wit · `KLEUR-TOT-MIX`

De twee nieuwe eigenschappen hangen samen. Een set mét controller sluit niet op
de bordaansluitingen aan maar op de hub, dus `FAN-MB-HEADERS` en `FAN-MB-RGB`
vervallen dan. Maar juist die controllers vragen vaak een interne USB-aansluiting
— dus wat je aan de ene kant wint, verlies je aan de andere.

# Mogelijk later nodig

Eigenschappen die in geen enkele regel hierboven voorkomen, maar waarvan
aannemelijk is dat er ooit een regel voor komt.

| Eigenschap | Op welk onderdeel | Waarom mogelijk later |
|---|---|---|
| gedeelde bandbreedte tussen M.2 en SATA | moederbord | Bij sommige borden schakelt een bezet M.2-slot SATA-poorten of een PCIe-slot uit. Reëel conflict, maar staat alleen in de handleiding |
| PCIe-generatie van het kaartslot | moederbord · videokaart | Voor M.2 is dit nu een regel (`M2-MB-GENERATIE`); voor de videokaart nog niet |
| breedte van de kaart | videokaart | Speelt bij smalle behuizingen; zelden opgegeven |
| positie van de M.2-sloten | moederbord | Een slot onder de videokaart is lastig bereikbaar. Moeilijk te modelleren |
| kwaliteit van de spanningsregeling | moederbord | Bepaalt of een zware processor zijn snelheid haalt. Nergens gespecificeerd |
| certificering van de voeding | voeding | Zegt iets over rendement, niets over of het past |
| ventilatorstatiek | casefans | Een ventilator op een radiator heeft andere eigenschappen nodig dan een vrije ventilator |

---

# Vaak ontbrekend of onbetrouwbaar

Hier gaat je scraper je in de steek laten.

| Eigenschap | Probleem |
|---|---|
| hoogte van geheugenmodules | Zelden opgegeven, terwijl het conflict met luchtkoelers er volledig van afhangt |
| vrije hoogte bij de koeler | Staat hooguit in de handleiding van de koeler |
| maximale kaartlengte met radiator vooraan | Bijna nooit gegeven; behuizingen vermelden alleen het gunstigste getal |
| lengte van de voeding | Vaak niet vermeld, terwijl het in kleine behuizingen beslissend is |
| koelvermogen van een koeler | Geen genormeerde meetmethode; fabrikanten geven optimistische cijfers of niets |
| geschikt voor achterzijde-aansluitingen | Recent verschijnsel, staat vaak alleen in de reclametekst |
| ondersteunde processorgeneraties | Meestal een aparte pagina bij de fabrikant, niet bij het product |
| USB-benaming | Hetzelfde poorttype heeft drie namen over de generaties heen. Zie de aantekening in je stappenplan |
| montageplaatsen per drive-formaat | Vaak samengevat als één getal terwijl het per formaat verschilt |
| kleurtype | Staat zelden als apart gegeven; meestal alleen af te leiden uit de productnaam of de foto |
| interne USB nodig voor ventilatoren | Staat in de handleiding of bij de controller, niet bij de ventilatoren zelf |

---

# Te verifiëren

Dit weet ik niet en moet uit jullie praktijk komen.

1. **Welke van deze conflicten staan werkelijk op jullie lijst?** Deze analyse is
   opgesteld vanuit algemene bouwkennis. De kans is groot dat jullie lijst regels
   bevat die hier ontbreken — onverwachte merkcombinaties, borden met een bekend
   probleem, koelers die volgens de specificatie passen maar in de praktijk niet.
   Dat zijn precies de regels die een tool als deze waardevol maken.
2. **Is een processor zonder beelduitgang bij jullie blokkerend?** Of wordt ervan
   uitgegaan dat de klant weet wat hij bestelt?
3. **Houden jullie bij welk BIOS er op een bord staat?** Zonder dat is
   `CPU-MB-BIOS` niet betrouwbaar te beoordelen.
4. **Tellen jullie geheugenkanalen mee?** Drie modules in een bord met twee
   kanalen werkt; of dat een melding waard is, is een keuze.
5. **Verkopen jullie ventilatoren met verlichting?** Zo ja, dan is het aantal
   verlichtingsaansluitingen op een bord wel degelijk een conflictregel — eerder
   in onze gesprekken heb ik gezegd dat dat waarschijnlijk niet het geval was.
6. **Hoe rekenen jullie het totale vermogen?** Processor plus videokaart plus een
   vaste marge is gebruikelijk, maar de marge is een bedrijfskeuze.
7. **Zit AIO-waterkoeling en luchtkoeling bij jullie in één productgroep?** Dat
   bepaalt of dit één onderdeeltype is met een kenmerk, of twee.
8. **Is `TRI-RAM-KOEL-MB` het modelleren waard?** Het hangt af van welk slot
   bezet raakt, en dat weet je pas bij het bouwen.
9. **Welke onderdelen tellen mee voor de kleurregel?** Zie de aantekening
   hieronder — zonder antwoord vuurt die regel op bijna elke order.
10. **Volstaan zwart en wit?** Grijs, zilver en doorzichtig bestaan ook. En een
    onderdeel met verlichting heeft strikt genomen geen vaste kleur.

---

## Aantekening bij de kleurregel

`KLEUR-TOT-MIX` is de enige regel in deze lijst die over uiterlijk gaat, en dat
maakt hem lastiger dan hij lijkt. Twee dingen moet je beslissen voor hij bruikbaar
is.

**Welke onderdelen doen mee?** Een processor, een M.2-SSD onder een koelplaatje en
een schijf in een kooi ziet niemand ooit. Laat je die meetellen, dan krijgt bijna
elke order een gele melding omdat er ergens een groen printplaatje in zit. De
eigenschap heeft dus een waarde nodig die "niet zichtbaar" betekent, en de regel
moet die overslaan.

Daarom staat er bij de meeste onderdelen hierboven **zwart, wit of geen** en bij
de behuizing en de ventilatoren alleen **zwart of wit**. Die twee zie je altijd.

**Wat doe je met de rest van het kleurenspectrum?** Zwart en wit dekken jullie
bouwstijl, maar de werkelijkheid kent grijs, zilver en hout-look. Een keuzelijst
die niet volledig is, dwingt je scraper tot gokken. Overweeg een waarde die
"anders" betekent en daar geen melding op te geven — beter geen melding dan een
verkeerde.
