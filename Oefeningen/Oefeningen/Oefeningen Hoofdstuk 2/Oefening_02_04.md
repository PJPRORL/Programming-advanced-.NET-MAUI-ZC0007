# 02_04

<b>Hoofdstuk 02</b> &nbsp;·&nbsp; Model, lijst en statuscodes &nbsp;·&nbsp; Dierenasiel &nbsp;·&nbsp; niveau <span style="color:#37b24d">●○○</span> &nbsp;·&nbsp; ±60 min

## Leerdoel

Na deze oefening kan je een Model opstellen dat een object uit de echte wereld beschrijft, en het vullen met een tijdelijke lijst in het geheugen.

Je leert die gegevens via GET-endpoints beschikbaar stellen en je antwoord verpakken in `ActionResult<T>`, zodat je naast de data ook een HTTP-statuscode kan meesturen.

Daarnaast leer je wanneer een leeg resultaat een fout is (404) en wanneer het gewoon een geldig antwoord is (200). Dat is een keuze die je per endpoint bewust maakt.

## 

## Opdracht

Dierenasiel Knuffelhof wil zijn bewoners via een API tonen, zodat de website en de balie dezelfde gegevens gebruiken.

Jouw taak is om een Model en een DierController te maken.

Via de API moet de website:

1. alle dieren kunnen opvragen;
2. één dier kunnen opvragen op basis van zijn Id;
3. alle dieren van één soort kunnen opvragen;
4. enkel de jonge dieren kunnen opvragen.

Implementeer onderstaande functionaliteiten.

### 1. Het model

Maak in de map Models een klasse `Dier` met volgende gegevens:

| Gegeven          | Soort waarde |
|------------------|--------------|
| Id               | geheel getal |
| Naam             | tekst        |
| Soort            | tekst        |
| LeeftijdInJaren  | geheel getal |
| Verblijfsnummer  | geheel getal |

---

### 2. De controller en de tijdelijke lijst

Maak een DierController die luistert op <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/dier</code>.

Voorzie in de controller een lijst met minstens zes dieren. Zorg dat er minstens drie soorten in voorkomen, en dat er minstens twee dieren jonger zijn dan twee jaar.

---

### 3. Alle dieren opvragen

Voorzie een GET-endpoint op:

<span style="background:#1c7ed6;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>GET</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/dier</code>

Dit endpoint geeft de volledige lijst terug met statuscode <code style="color:#37b24d;font-weight:600">200 OK</code>.

---

### 4. Eén dier opvragen

Voorzie een GET-endpoint op:

<span style="background:#1c7ed6;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>GET</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/dier/<span style="color:#e64980"><b>{id}</b></span></code>

Bestaat het dier, geef het dan terug met statuscode <code style="color:#37b24d;font-weight:600">200 OK</code>.

Bestaat het dier niet, geef dan statuscode <code style="color:#f08c00;font-weight:600">404 Not Found</code> met het bericht:

<code style="color:#845ef7">We vonden geen dier met Id <span style="color:#e64980"><b>{id}</b></span>.</code>

---

### 5. Dieren van één soort opvragen

Voorzie een GET-endpoint op:

<span style="background:#1c7ed6;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>GET</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/dier/soort/<span style="color:#e64980"><b>{soort}</b></span></code>

Dit endpoint geeft alle dieren van die soort terug met statuscode <code style="color:#37b24d;font-weight:600">200 OK</code>.

Zijn er geen dieren van die soort, geef dan statuscode <code style="color:#f08c00;font-weight:600">404 Not Found</code> met het bericht:

<code style="color:#845ef7">We hebben op dit moment geen <span style="color:#e64980"><b>{soort}</b></span> in het asiel.</code>

> [!WARNING]
> Let op: deze route en de route uit punt 4 lijken op elkaar. Zorg dat een verzoek naar <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/dier/soort/kat</code> niet per ongeluk bij het endpoint van punt 4 terechtkomt.

---

### 6. Enkel de jonge dieren

Voorzie een GET-endpoint op:

<span style="background:#1c7ed6;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>GET</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/dier/jong</code>

Dit endpoint geeft enkel de dieren terug die jonger zijn dan twee jaar.

Zijn er geen jonge dieren, geef dan een lege lijst terug met statuscode <code style="color:#37b24d;font-weight:600">200 OK</code>, en dus géén 404.

> [!NOTE]
> Een leeg resultaat is hier namelijk geen fout: de vraag was geldig en het antwoord is "geen enkel". Dat is een ander geval dan een dier of een soort die niet bestaat.

---

### 7. Alles samen: de dierfiche

<span style="background:#7048e8;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>SAMENGESTELDE OEFENING</b></span>

De vier vorige endpoints beantwoorden elk één vraag over het asiel. Een bezoeker die alles over één dier wil weten, moet ze nu alle vier oproepen en de antwoorden zelf bij elkaar leggen.

In dit laatste endpoint doe je dat werk voor hem: één dier, één antwoord, met alles erin.

Maak in de map Models een tweede klasse `DierFiche` met volgende gegevens:

| Gegeven                | Soort waarde       |
|------------------------|--------------------|
| Dier                   | het dier zelf      |
| IsJong                 | ja of nee          |
| AantalVanDezelfdeSoort | geheel getal       |
| IsOudsteVanDeSoort     | ja of nee          |

`IsJong` volgt dezelfde regel als punt 6: jonger dan twee jaar.

`AantalVanDezelfdeSoort` telt de andere dieren van dezelfde soort. Het dier zelf telt niet mee.

`IsOudsteVanDeSoort` zegt of geen enkel ander dier van die soort ouder is.

Voorzie een GET-endpoint op:

<span style="background:#1c7ed6;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>GET</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/dier/fiche/<span style="color:#e64980"><b>{id}</b></span></code>

Bestaat het dier, geef dan de fiche terug met statuscode <code style="color:#37b24d;font-weight:600">200 OK</code>.

Bestaat het dier niet, geef dan statuscode <code style="color:#f08c00;font-weight:600">404 Not Found</code> met het bericht:

<code style="color:#845ef7">We vonden geen dier met Id <span style="color:#e64980"><b>{id}</b></span>.</code>

Test je endpoint en controleer:

- of `AantalVanDezelfdeSoort` op 0 staat bij een dier dat als enige van zijn soort in het asiel zit;
- of `IsJong` op ja staat bij de dieren die je jonger dan twee jaar hebt gemaakt;
- of er bij twee dieren van dezelfde soort maar bij één van de twee `IsOudsteVanDeSoort` op ja staat.

> [!NOTE]
> Dit is de eerste keer dat je een klasse maakt die geen object uit de echte wereld beschrijft. Een dier bestaat, een dierfiche niet: die bestaat alleen omdat een client hem handig vindt. Onthoud dat verschil, het komt later in de cursus terug.
> 