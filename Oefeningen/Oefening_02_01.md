# 02_01

<b>Hoofdstuk 02</b> &nbsp;·&nbsp; Model, lijst en statuscodes &nbsp;·&nbsp; PC-onderdelen &nbsp;·&nbsp; niveau <span style="color:#37b24d">●○○</span> &nbsp;·&nbsp; ±60 min

## Leerdoel

Na deze oefening kan je een Model opstellen dat een object uit de echte wereld beschrijft, en het vullen met een tijdelijke lijst in het geheugen.

Je leert die gegevens via GET-endpoints beschikbaar stellen en je antwoord verpakken in `ActionResult<T>`, zodat je naast de data ook een HTTP-statuscode kan meesturen.

Daarnaast leer je wanneer een leeg resultaat een fout is (404) en wanneer het gewoon een geldig antwoord is (200). Dat is een keuze die je per endpoint bewust maakt.

## 

## Opdracht

Bit & Byte wil de voorraad van de winkel via een API ontsluiten, zodat de website later dezelfde gegevens kan tonen als de balie.

Jouw taak is om een Model en een ArtikelController te maken.

Via de API moet de website:

1. de volledige voorraad kunnen opvragen;
2. één artikel kunnen opvragen op basis van zijn Id;
3. alle artikelen van één soort kunnen opvragen;
4. enkel de artikelen kunnen opvragen die nog op voorraad liggen.

Implementeer onderstaande functionaliteiten.

### 1. Het model

Maak in de map Models een klasse `Artikel` met volgende gegevens:

| Gegeven          | Soort waarde |
|------------------|--------------|
| Id               | geheel getal |
| Naam             | tekst        |
| Merk             | tekst        |
| Soort            | tekst        |
| Prijs            | kommagetal   |
| AantalOpVoorraad | geheel getal |

---

### 2. De controller en de tijdelijke voorraad

Maak een ArtikelController die luistert op <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/artikel</code>.

Voorzie in de controller een lijst met minstens zes artikelen. Zorg dat er minstens twee soorten in voorkomen, en dat minstens één artikel een voorraad van 0 heeft.

---

### 3. Volledige voorraad opvragen

Voorzie een GET-endpoint op:

<span style="background:#1c7ed6;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>GET</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/artikel</code>

Dit endpoint geeft de volledige lijst terug met statuscode <code style="color:#37b24d;font-weight:600">200 OK</code>.

---

### 4. Eén artikel opvragen

Voorzie een GET-endpoint op:

<span style="background:#1c7ed6;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>GET</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/artikel/<span style="color:#e64980"><b>{id}</b></span></code>

Bestaat het artikel, geef het dan terug met statuscode <code style="color:#37b24d;font-weight:600">200 OK</code>.

Bestaat het artikel niet, geef dan statuscode <code style="color:#f08c00;font-weight:600">404 Not Found</code> met het bericht:

<code style="color:#845ef7">We vonden geen artikel met Id <span style="color:#e64980"><b>{id}</b></span>.</code>

---

### 5. Artikelen van één soort opvragen

Voorzie een GET-endpoint op:

<span style="background:#1c7ed6;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>GET</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/artikel/soort/<span style="color:#e64980"><b>{soort}</b></span></code>

Dit endpoint geeft alle artikelen van die soort terug met statuscode <code style="color:#37b24d;font-weight:600">200 OK</code>.

Zijn er geen artikelen van die soort, geef dan statuscode <code style="color:#f08c00;font-weight:600">404 Not Found</code> met het bericht:

<code style="color:#845ef7">We vonden geen artikelen van de soort <span style="color:#e64980"><b>{soort}</b></span>.</code>

> [!WARNING]
> Let op: deze route en de route uit punt 4 lijken op elkaar. Zorg dat een verzoek naar <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/artikel/soort/processor</code> niet per ongeluk bij het endpoint van punt 4 terechtkomt.

---

### 6. Enkel wat op voorraad is

Voorzie een GET-endpoint op:

<span style="background:#1c7ed6;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>GET</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/artikel/voorradig</code>

Dit endpoint geeft enkel de artikelen terug waarvan de voorraad groter is dan 0.

Is er niets op voorraad, geef dan een lege lijst terug met statuscode <code style="color:#37b24d;font-weight:600">200 OK</code>, en dus géén 404.

> [!NOTE]
> Een lege voorraad is namelijk geen fout: de vraag was geldig en het antwoord is "niets". Dat is een ander geval dan een artikel of een soort die niet bestaat.

---

### 7. Alles samen: de artikelfiche

<span style="background:#7048e8;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>SAMENGESTELDE OEFENING</b></span>

De vier vorige endpoints beantwoorden elk één vraag over de voorraad. Een medewerker die alles over één artikel wil weten, moet ze nu alle vier oproepen en de antwoorden zelf bij elkaar leggen.

In dit laatste endpoint doe je dat werk voor hem: één artikel, één antwoord, met alles erin.

Maak in de map Models een tweede klasse `ArtikelFiche` met volgende gegevens:

| Gegeven                | Soort waarde              |
|------------------------|---------------------------|
| Artikel                | het artikel zelf          |
| IsVoorradig            | ja of nee                 |
| AantalVanDezelfdeSoort | geheel getal              |
| IsGoedkoopsteVanDeSoort| ja of nee                 |

`AantalVanDezelfdeSoort` telt de andere artikelen van dezelfde soort. Het artikel zelf telt niet mee.

`IsGoedkoopsteVanDeSoort` zegt of geen enkel ander artikel van die soort goedkoper is.

Voorzie een GET-endpoint op:

<span style="background:#1c7ed6;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>GET</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/artikel/fiche/<span style="color:#e64980"><b>{id}</b></span></code>

Bestaat het artikel, geef dan de fiche terug met statuscode <code style="color:#37b24d;font-weight:600">200 OK</code>.

Bestaat het artikel niet, geef dan statuscode <code style="color:#f08c00;font-weight:600">404 Not Found</code> met het bericht:

<code style="color:#845ef7">We vonden geen artikel met Id <span style="color:#e64980"><b>{id}</b></span>.</code>

Test je endpoint en controleer:

- of `AantalVanDezelfdeSoort` op 0 staat bij een artikel dat als enige van zijn soort in de lijst zit;
- of `IsVoorradig` op nee staat bij het artikel waarvan je de voorraad op 0 hebt gezet;
- of `IsGoedkoopsteVanDeSoort` bij twee artikelen van dezelfde soort maar bij één van de twee op ja staat.

> [!NOTE]
> Dit is de eerste keer dat je een klasse maakt die geen object uit de echte wereld beschrijft. Een artikel bestaat, een artikelfiche niet: die bestaat alleen omdat een client hem handig vindt. Onthoud dat verschil, het komt later in de cursus terug.
> 