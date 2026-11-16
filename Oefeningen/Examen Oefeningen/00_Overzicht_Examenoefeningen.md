# Overzicht examenoefeningen

**PROGRAMMING ADVANCED · ALLES IN ÉÉN KEER · HOOFDSTUK 01 TOT 06 · V3**

## Waarvoor ze dienen

In de lesoefeningen bouwde je telkens verder op het vorige hoofdstuk. Op het examen krijg je het einddoel in één keer, en begin je van nul. Deze drie oefeningen trainen dat: een nieuw project, één keer door de zes fases, elk bestand meteen in zijn eindvorm.

De fases zelf staan in `Examen_Blauwdruk_PA_Alles_In_Een_Keer_V2.docx`. Leg die ernaast.

## De drie oefeningen

| Oefening | Project | Modellen | Hulp | Tijd |
|---|---|---|---|---|
| 01 · Muziekschool | `ExamenMuziekschool` | Leerling, Lesgever, Instrument, en de tussentabel LesgeverInstrument | volledige checklist per fase | ± 90 min |
| 02 · Kookatelier | `ExamenKookatelier` | Deelnemer, Chef, Workshop, en de tussentabel ChefWorkshop; met een bedrag | één regel per fase | 75 tot 90 min |
| 03 · HardwareBeurs | `ExamenHardwareBeurs` | Bezoeker, Spreker, Sessie, en de tussentabel SprekerSessie | geen: examensimulatie op tijd | 75 min |

Elke oefening heeft dezelfde vorm als je tussentijds examen: een één-op-veel-relatie, een veel-op-veel-relatie via een tussentabel, drie controllers, zoeken op naam, en verwijderen zonder corrupte data.

Elke opgave eindigt met een eindcontrole in Scalar. Daarmee kijk je jezelf na. Daarna volgen de *Verwachte uitkomsten*: hoe elke fase en elke stap van de eindcontrole eruitziet als ze lukt, met de Scalar-schermen, de JSON en de statuscodes.

## Voor je begint

- **Eén container tegelijk.** Elke oefening heeft haar eigen database op poort 5432. Stop de container van de vorige oefening voor je de nieuwe start.
- **De handleiding.** Op het examen kreeg je `Installatie Docker.docx`, met de connectiestring, het begin van je context en de regels voor `Program.cs`. Gebruik die van je tussentijds examen, en pas de namen aan. Scalar staat er niet in.
- **Begin telkens van nul.** Kopieer geen project van een vorige oefening. Het examen begint ook leeg.
- **Was je al aan oefening 01 begonnen?** Het nieuwe `docker-compose.yml` heeft een andere gebruiker en een ander wachtwoord dan het oude. Begin opnieuw met het nieuwe bestand: dat is meteen een goede herhaling van fase 1.

## Nieuw in V3

- **Verwachte uitkomsten** achteraan in elke opgave: de uitvoer van Docker en van `Update-Database`, de tabellen in pgAdmin, Scalar leeg en met alle routes, en per stap van de eindcontrole het verzoek, de statuscode en het antwoord. De Scalar-schermen zijn nagebouwd. Je poort en de volgorde van je routes kunnen anders zijn.
- **De afbeeldingen** staan per oefening in de map `Afbeeldingen`.

## Wat er veranderd is tegenover V1

- **Toevoegen en `Add`.** V1 vroeg overal `AddAsync`. Dat is niet nodig: `Add` blijft, enkel `SaveChanges` wordt `SaveChangesAsync`, zoals in les 05.
- **De Id bij toevoegen.** Nieuw in fase 4 en in elke eindcontrole. Zonder botst je eerste POST met de startgegevens.
- **Oefening 02 is het kookatelier.** De sportclub staat uitgewerkt in hoofdstuk 43 van je leerboek.
- **De drie `docker-compose.yml`** hebben nu de vorm van het examen, met pgAdmin erbij.
- **Fase 6, indienen,** is erbij gekomen, met de regel: dien altijd iets in dat bouwt.
