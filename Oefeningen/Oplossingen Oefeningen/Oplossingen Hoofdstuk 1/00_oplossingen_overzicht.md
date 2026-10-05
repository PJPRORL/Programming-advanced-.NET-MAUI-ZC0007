# Oplossingen — hoofdstuk 01 en 02

<b>Programming Advanced</b> &nbsp;·&nbsp; twaalf oplossingen &nbsp;·&nbsp; bij de map *Oefeningen PA 01-02*

Per oefening één bestand, met dezelfde nummering als de opgave. Elk punt van een
oefening heeft hier een eigen stuk, in deze volgorde:

| Onderdeel | Wat je eraan hebt | Zichtbaar |
|---|---|---|
| **Nakijkpunten** | wat je API moet antwoorden: verzoek, statuscode en tekst | meteen |
| **Aanpak** | welke bouwstenen, in welke volgorde, en waarom | meteen |
| **Toon de code** | een uitgewerkte oplossing van dat ene punt | dichtgeklapt |
| <span style="color:#f08c00"><b>Veelgemaakte fouten</b></span> | waar de meeste oplossingen stuklopen | meteen |
| <span style="color:#37b24d"><b>Zo kan het beter</b></span> | wat juist is, maar netter of veiliger kan | meteen |

Onderaan elk bestand staan de volledige bestanden in één stuk, ook dichtgeklapt.

---

## Zo kijk je na

1. Werk een punt eerst zelf af, tot het werkt of tot je echt vastzit.
2. Doe de verzoeken uit de nakijkpunten in Scalar of Postman. Klopt de statuscode en
   klopt de tekst letter voor letter, dan is je punt **juist**, ook als je code er
   anders uitziet. Er bestaan meerdere juiste oplossingen.
3. Lees de veelgemaakte fouten. Herken je er een in je eigen code, dan weet je waar je
   **fout** zit, ook als de nakijkpunten toevallig nog kloppen.
4. Open pas dan de code, en lees *Zo kan het beter*. Daar zie je wat **beter** kan.

> [!IMPORTANT]
> Vergelijk niet regel per regel. Vraag je bij elk verschil af: doet mijn code
> hetzelfde, en zo ja, welke van de twee lees je over een maand nog het vlotst?

---

## De oplossingen

### Hoofdstuk 01 — controller, routes, routeparameters

| Oplossing | Domein | Waar het om draait |
|---|---|---|
| [01_02](Oplossing_01_02.md) | PC-onderdelen | een hulpmethode per bericht, zodat de fiche niets opnieuw typt |
| [01_03](Oplossing_01_03.md) | PC-onderdelen | drie oordelen die elkaar niet tegenhouden |
| [01_04](Oplossing_01_04.md) | PC-onderdelen | een eigen route, en eerst het soort controleren, dan het getal |
| [01_05](Oplossing_01_05.md) | Bibliotheek | knippen met `Substring`, en hergebruik in de lenersfiche |
| [01_06](Oplossing_01_06.md) | Zwembad | delen met rest, en elk tarief maar één keer in je code |
| [01_07](Oplossing_01_07.md) | Festival | de volgorde van controles, en rekenen met `decimal` |

### Hoofdstuk 02 — model, in-memory lijst, CRUD, statuscodes

| Oplossing | Domein | Waar het om draait |
|---|---|---|
| [02_01](Oplossing_02_01.md) | PC-onderdelen | 404 tegenover een lege lijst met 200, en een fiche |
| [02_02](Oplossing_02_02.md) | PC-onderdelen | één controlemethode voor POST, PUT en vervangen |
| [02_03](Oplossing_02_03.md) | PC-onderdelen | soft delete, totalen en één methode die de fiche bouwt |
| [02_04](Oplossing_02_04.md) | Dierenasiel | zoals 02_01, met de regel *jong* op één plaats |
| [02_05](Oplossing_02_05.md) | Fietsverhuur | zoals 02_02, met een grens *tussen* twee getallen |
| [02_06](Oplossing_02_06.md) | Concertzaal | een plaatscontrole die je eigen plaats overslaat |

---

## Wat in elke oplossing terugkomt

- **Eén plaats per regel.** Elk bericht en elke controle staat in één private methode.
  Het endpoint roept ze op, de samengestelde oefening ook. Verandert een regel, dan
  pas je één plaats aan.
- **Eerst controleren, dan veranderen.** Een endpoint dat iets aanpast, kijkt eerst
  alles na en past pas daarna iets aan. Zo blijft er bij een fout nooit iets half
  veranderd achter.
- **De volgorde van de opgave.** Staan er meerdere controles, dan staan ze in de
  volgorde waarin de opgave ze noemt. Bij twee fouten tegelijk krijgt de client zo de
  eerste uit die lijst.
- **Enkel leerstof van les 01 en 02**, plus wat in je leerboek staat. Waar een latere
  les iets beters biedt, staat dat er in één zin bij.

> [!NOTE]
> De code is gecompileerd en getest met een nagebootste basis van ASP.NET Core: elk
> voorbeeld uit de opgaven, de grenzen van elke regel en de testscenario's uit 02_02,
> 02_03 en 02_06, samen 261 verzoeken. Alle voorbeelden uit de opgaven geven de
> statuscode en de tekst die gevraagd is. Twee extra verzoeken tonen bewust een zwakke
> plek die de opgave niet regelt, een te kort lidkaartnummer in 01_05 en nul banen in
> 01_06. Die staan bij *Zo kan het beter*.
