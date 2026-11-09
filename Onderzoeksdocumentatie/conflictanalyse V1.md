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
| `RAM-KOEL-HOOGTE` | geheugen · koeler | Module past niet onder de koeler | Hoge modules botsen tegen een brede luchtkoeler |
| `M2-MB-AANTAL` | SSD · moederbord | Te veel M.2-drives | Meer M.2-SSD's dan het bord sloten heeft |
| `M2-MB-LENGTE` | SSD · moederbord | Lengte past niet | Het slot ondersteunt de lengte van deze SSD niet |
| `M2-MB-INTERFACE` | SSD · moederbord | Interface komt niet overeen | Een SATA-M.2 in een slot dat alleen PCIe voert, of omgekeerd |
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

## Drie onderdelen samen

| Id | Onderdelen | Conflict | Wat er misgaat |
|---|---|---|---|
| `TRI-RAD-GPU-KST` | koeler · videokaart · behuizing | Radiator verkort de kaartruimte | Een radiator vooraan neemt lengte weg die de videokaart nodig heeft |
| `TRI-RAD-FAN-KST` | koeler · casefans · behuizing | Dubbel bezette montageplaats | Radiator en ventilatoren zijn voor dezelfde plaats bedoeld |
| `TRI-RAM-KOEL-MB` | geheugen · koeler · moederbord | Slotpositie onder de koeler | Alleen het slot het dichtst bij de processor botst; met minder modules speelt het niet |

Alle tien onderdeeltypes uit de opdracht komen in minstens één regel voor.

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
| `RAM-KOEL-HOOGTE` | hoogte module; vrije hoogte boven de sloten bij de koeler | Module hoger → niet OK | hard | mm |
| `RAM-RAM-MENGEN` | set-aanduiding of artikelcode van elke module | Verschillende sets → opmerking | waarschuwing | tekst |
| `RAM-RAM-KANALEN` | aantal kanalen van het bord; aantal modules | Aantal niet deelbaar door de kanalen → opmerking | waarschuwing | aantal |

## Opslag

| Id | Eigenschappen nodig | Oordeel | Ernst | Eenheid / formaat |
|---|---|---|---|---|
| `M2-MB-AANTAL` | aantal M.2-sloten; aantal M.2-drives | Drives meer dan sloten → niet OK | hard | aantal |
| `M2-MB-LENGTE` | lengtes die elk slot toelaat; lengte van de SSD | Geen enkel vrij slot dat deze lengte toelaat → niet OK | hard | keuzelijst (2242/2260/2280/22110) |
| `M2-MB-INTERFACE` | interface per slot; interface van de SSD | Geen vrij slot met de juiste interface → niet OK | hard | keuzelijst (PCIe/SATA) |
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
| `FAN-MB-RGB` | type en aantal verlichtingsaansluitingen op het bord; verlichtingstype van de ventilatoren | Type ontbreekt of te weinig aansluitingen → opmerking | waarschuwing | keuzelijst + aantal |
| `KST-TOT-KOELING` | aantal meegeleverde of bestelde ventilatoren; warmteafgifte van processor en videokaart | Geen of één ventilator bij hoge warmteafgifte → opmerking | waarschuwing | aantal + W |

## Voeding

| Id | Eigenschappen nodig | Oordeel | Ernst | Eenheid / formaat |
|---|---|---|---|---|
| `VOE-KST-FORMAAT` | formfactor voeding; ondersteunde formfactors behuizing; lengte voeding; maximale lengte behuizing | Formfactor niet ondersteund of voeding langer → niet OK | hard | keuzelijst + mm |
| `VOE-MB-EPS` | aantal en type CPU-aansluitingen die het bord vraagt; wat de voeding levert | Voeding levert minder → niet OK | hard | aantal per keuzewaarde |
| `VOE-TOT-VERMOGEN` | vermogen voeding; warmteafgifte processor en videokaart; aantal overige onderdelen | Optelling boven het vermogen → niet OK. Binnen een marge eronder → opmerking | hard of waarschuwing | W |

## Drie onderdelen samen

| Id | Eigenschappen nodig | Oordeel | Ernst | Eenheid / formaat |
|---|---|---|---|---|
| `TRI-RAD-GPU-KST` | radiatordikte plus ventilatordikte; maximale kaartlengte mét radiator vooraan; lengte kaart | Kaart langer dan de gereduceerde ruimte → niet OK | hard | mm |
| `TRI-RAD-FAN-KST` | montageplaatsen behuizing; plaatsen die de radiator inneemt; aantal ventilatoren | Samen meer plaatsen nodig dan beschikbaar → opmerking | waarschuwing | aantal |
| `TRI-RAM-KOEL-MB` | hoogte modules; vrije hoogte bij de koeler; aantal modules; aantal sloten | Alleen conflict als het slot naast de processor ook bezet raakt → opmerking | waarschuwing | mm + aantal |

---

# Deel 3 — Attributenlijst per onderdeeltype

Kolom **meervoud**: heeft dit onderdeel er één van, of meerdere?
Kolom **bron**: `pagina` = normaal van een productpagina te halen ·
`handmatig` = moet ingevuld of nagekeken worden.

## Moederbord

| Eigenschap | Soort | Eenheid / waarden | Conflict-id's | Meervoud | Bron |
|---|---|---|---|---|---|
| socket | keuzelijst | vaste lijst | `CPU-MB-SOCKET` | één | pagina |
| ondersteunde processorgeneraties | reeks keuzewaarden | vaste lijst | `CPU-MB-BIOS` | meerdere | handmatig |
| BIOS-update zonder processor mogelijk | ja/nee | — | `CPU-MB-BIOS` | één | handmatig |
| geheugentype | keuzelijst | DDR4 / DDR5 | `RAM-MB-TYPE` | één | pagina |
| aantal geheugensloten | getal | aantal | `RAM-MB-SLOTS`, `TRI-RAM-KOEL-MB` | één | pagina |
| aantal geheugenkanalen | getal | aantal | `RAM-RAM-KANALEN` | één | pagina |
| maximale geheugencapaciteit | getal | GB | `RAM-MB-MAX` | één | pagina |
| M.2-slot | samengesteld | zie hieronder | `M2-MB-*` | **meerdere** | pagina |
| — lengtes die het slot toelaat | reeks keuzewaarden | 2242/2260/2280/22110 | `M2-MB-LENGTE` | meerdere per slot | pagina |
| — interface van het slot | keuzelijst | PCIe / SATA | `M2-MB-INTERFACE` | één per slot | pagina |
| aantal SATA-poorten | getal | aantal | `SATA-MB-POORTEN` | één | pagina |
| formfactor | keuzelijst | ATX / mATX / ITX / E-ATX | `MB-KST-FORMFACTOR` | één | pagina |
| aansluitingen op achterzijde | ja/nee | — | `MB-KST-BACKCONNECT` | één | handmatig |
| interne USB-aansluitingen per type | getal per keuzewaarde | aantal | `MB-KST-FRONTUSB` | meerdere | pagina |
| interne USB-C-aansluiting | ja/nee | — | `MB-KST-USBC` | één | pagina |
| aantal ventilatoraansluitingen | getal | aantal | `FAN-MB-HEADERS` | één | pagina |
| verlichtingsaansluitingen per type | getal per keuzewaarde | aantal | `FAN-MB-RGB` | meerdere | pagina |
| aantal CPU-stroomaansluitingen per type | getal per keuzewaarde | aantal | `VOE-MB-EPS` | meerdere | pagina |

## Processor

| Eigenschap | Soort | Eenheid / waarden | Conflict-id's | Meervoud | Bron |
|---|---|---|---|---|---|
| socket | keuzelijst | vaste lijst | `CPU-MB-SOCKET`, `CPU-KOEL-MOUNT` | één | pagina |
| generatie | keuzelijst | vaste lijst | `CPU-MB-BIOS` | één | pagina |
| warmtevermogen | getal | W | `CPU-KOEL-TDP`, `VOE-TOT-VERMOGEN`, `KST-TOT-KOELING` | één | pagina |
| officieel ondersteunde geheugensnelheid | getal | MT/s | `RAM-CPU-SNELHEID` | één | pagina |
| geïntegreerde grafische kern | ja/nee | — | `CPU-TOT-BEELD` | één | pagina |

## CPU-koeler

| Eigenschap | Soort | Eenheid / waarden | Conflict-id's | Meervoud | Bron |
|---|---|---|---|---|---|
| soort koeling | keuzelijst | lucht / AIO | bepaalt welke regels gelden: lucht → `KOEL-KST-HOOGTE`, `RAM-KOEL-HOOGTE`, `TRI-RAM-KOEL-MB` · AIO → `KOEL-KST-RADIATOR`, `TRI-RAD-GPU-KST`, `TRI-RAD-FAN-KST` | één | pagina |
| ondersteunde sockets | reeks keuzewaarden | vaste lijst | `CPU-KOEL-MOUNT` | meerdere | pagina |
| koelvermogen | getal | W | `CPU-KOEL-TDP` | één | handmatig |
| hoogte *(alleen lucht)* | getal | mm | `KOEL-KST-HOOGTE` | één | pagina |
| vrije hoogte boven de geheugensloten *(alleen lucht)* | getal | mm | `RAM-KOEL-HOOGTE`, `TRI-RAM-KOEL-MB` | één | handmatig |
| radiatormaat *(alleen AIO)* | keuzelijst | 120/240/280/360/420 | `KOEL-KST-RADIATOR` | één | pagina |
| radiatordikte *(alleen AIO)* | getal | mm | `TRI-RAD-GPU-KST` | één | pagina |

## Geheugen

| Eigenschap | Soort | Eenheid / waarden | Conflict-id's | Meervoud | Bron |
|---|---|---|---|---|---|
| geheugentype | keuzelijst | DDR4 / DDR5 | `RAM-MB-TYPE` | één | pagina |
| capaciteit per module | getal | GB | `RAM-MB-MAX` | één | pagina |
| aantal modules in de set | getal | aantal | `RAM-MB-SLOTS`, `RAM-RAM-KANALEN` | één | pagina |
| snelheid | getal | MT/s | `RAM-CPU-SNELHEID` | één | pagina |
| hoogte | getal | mm | `RAM-KOEL-HOOGTE`, `TRI-RAM-KOEL-MB` | één | handmatig |
| set-aanduiding | tekst | — | `RAM-RAM-MENGEN` | één | pagina |

## Videokaart

| Eigenschap | Soort | Eenheid / waarden | Conflict-id's | Meervoud | Bron |
|---|---|---|---|---|---|
| lengte | getal | mm | `GPU-KST-LENGTE`, `TRI-RAD-GPU-KST` | één | pagina |
| dikte in posities | getal | aantal | `GPU-KST-DIKTE` | één | pagina |
| stroomaansluitingen per type | getal per keuzewaarde | aantal | `GPU-VOE-CONNECTOR` | meerdere | pagina |
| warmteafgifte | getal | W | `VOE-TOT-VERMOGEN`, `KST-TOT-KOELING` | één | pagina |

## SSD

| Eigenschap | Soort | Eenheid / waarden | Conflict-id's | Meervoud | Bron |
|---|---|---|---|---|---|
| aansluitvorm | keuzelijst | M.2 / SATA 2,5" | bepaalt welke regels gelden: M.2 → `M2-MB-AANTAL`, `M2-MB-LENGTE`, `M2-MB-INTERFACE` · SATA → `SATA-MB-POORTEN`, `DRIVE-KST-PLAATSEN`, `VOE-DRIVE-SATA` | één | pagina |
| lengte *(alleen M.2)* | keuzelijst | 2242/2260/2280/22110 | `M2-MB-LENGTE` | één | pagina |
| interface *(alleen M.2)* | keuzelijst | PCIe / SATA | `M2-MB-INTERFACE` | één | pagina |
| formaat *(alleen SATA)* | keuzelijst | 2,5" | `DRIVE-KST-PLAATSEN` | één | pagina |

## HDD

| Eigenschap | Soort | Eenheid / waarden | Conflict-id's | Meervoud | Bron |
|---|---|---|---|---|---|
| formaat | keuzelijst | 3,5" / 2,5" | `DRIVE-KST-PLAATSEN` | één | pagina |

Verder deelt de HDD de regels `SATA-MB-POORTEN` en `VOE-DRIVE-SATA` met de SATA-SSD;
daarvoor is geen eigen eigenschap nodig, alleen dat het om een SATA-schijf gaat.

## PC-behuizing

| Eigenschap | Soort | Eenheid / waarden | Conflict-id's | Meervoud | Bron |
|---|---|---|---|---|---|
| ondersteunde formfactors | reeks keuzewaarden | ATX / mATX / ITX / E-ATX | `MB-KST-FORMFACTOR` | meerdere | pagina |
| geschikt voor achterzijde-aansluitingen | ja/nee | — | `MB-KST-BACKCONNECT` | één | handmatig |
| maximale koelerhoogte | getal | mm | `KOEL-KST-HOOGTE` | één | pagina |
| maximale kaartlengte | getal | mm | `GPU-KST-LENGTE` | één | pagina |
| maximale kaartlengte met radiator vooraan | getal | mm | `TRI-RAD-GPU-KST` | één | handmatig |
| aantal uitbreidingsposities | getal | aantal | `GPU-KST-DIKTE` | één | pagina |
| montageplaatsen per drive-formaat | getal per keuzewaarde | aantal | `DRIVE-KST-PLAATSEN` | meerdere | pagina |
| ondersteunde radiatormaten | reeks keuzewaarden | 120/240/280/360/420 | `KOEL-KST-RADIATOR`, `TRI-RAD-FAN-KST` | meerdere | pagina |
| ondersteunde ventilatorafmetingen | reeks keuzewaarden | 120/140 mm | `FAN-KST-MAAT` | meerdere | pagina |
| aantal ventilatorplaatsen | getal | aantal | `FAN-KST-AANTAL`, `TRI-RAD-FAN-KST` | één | pagina |
| aantal meegeleverde ventilatoren | getal | aantal | `KST-TOT-KOELING` | één | pagina |
| frontpoorten per type | getal per keuzewaarde | aantal | `MB-KST-FRONTUSB` | meerdere | pagina |
| USB-C-frontpoort | ja/nee | — | `MB-KST-USBC` | één | pagina |
| ondersteunde voedingsformfactors | reeks keuzewaarden | ATX / SFX / SFX-L | `VOE-KST-FORMAAT` | meerdere | pagina |
| maximale voedingslengte | getal | mm | `VOE-KST-FORMAAT` | één | handmatig |

## Voeding

| Eigenschap | Soort | Eenheid / waarden | Conflict-id's | Meervoud | Bron |
|---|---|---|---|---|---|
| formfactor | keuzelijst | ATX / SFX / SFX-L | `VOE-KST-FORMAAT` | één | pagina |
| lengte | getal | mm | `VOE-KST-FORMAAT` | één | handmatig |
| vermogen | getal | W | `VOE-TOT-VERMOGEN` | één | pagina |
| CPU-stroomaansluitingen per type | getal per keuzewaarde | aantal | `VOE-MB-EPS` | meerdere | pagina |
| kaartaansluitingen per type | getal per keuzewaarde | aantal | `GPU-VOE-CONNECTOR` | meerdere | pagina |
| aantal SATA-stroomaansluitingen | getal | aantal | `VOE-DRIVE-SATA` | één | pagina |

## Casefans

| Eigenschap | Soort | Eenheid / waarden | Conflict-id's | Meervoud | Bron |
|---|---|---|---|---|---|
| afmeting | keuzelijst | 120 / 140 mm | `FAN-KST-MAAT` | één | pagina |
| aantal in de set | getal | aantal | `FAN-KST-AANTAL`, `FAN-MB-HEADERS` | één | pagina |
| verlichtingstype | keuzelijst | geen / RGB / ARGB | `FAN-MB-RGB` | één | pagina |

---

# Mogelijk later nodig

Eigenschappen die in geen enkele regel hierboven voorkomen, maar waarvan
aannemelijk is dat er ooit een regel voor komt.

| Eigenschap | Op welk onderdeel | Waarom mogelijk later |
|---|---|---|
| gedeelde bandbreedte tussen M.2 en SATA | moederbord | Bij sommige borden schakelt een bezet M.2-slot SATA-poorten of een PCIe-slot uit. Reëel conflict, maar staat alleen in de handleiding |
| PCIe-generatie per slot | moederbord · SSD · videokaart | Werkt altijd, maar trager. Alleen een regel als jullie daarover willen waarschuwen |
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
