# 01_04

<b>Hoofdstuk 01</b> &nbsp;·&nbsp; Eigen route en geneste beslissingen &nbsp;·&nbsp; PC-onderdelen &nbsp;·&nbsp; niveau <span style="color:#f03e3e">●●●</span> &nbsp;·&nbsp; ±75 min

## Leerdoel

Na deze oefening kan je zelf bepalen op welke route een controller luistert, los van de naam die de klasse draagt.

Je leert dat `[controller]` in de route een plaatshouder is die je mag vervangen door een eigen naam, en dat de gebruiker van je API de klassenaam nooit te zien krijgt.

Daarnaast oefen je op geneste beslissingen: endpoints waar het antwoord van twee routeparameters samen afhangt, en waar de volgorde van je controles bepaalt of je het juiste antwoord geeft.

## 

## Opdracht

Bit & Byte wil klanten aan de ingang een adviespaneel geven. Marketing heeft beslist dat alle adressen met `bouwadvies` in de URL moeten werken, terwijl de ontwikkelaars de klasse liever `AdviesController` noemen.

Jouw taak is om een AdviesController te maken die luistert op de route <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/bouwadvies</code>.

Via de API moeten klanten:

1. een overzicht van het adviespaneel kunnen opvragen;
2. advies kunnen krijgen op basis van hun budget;
3. advies kunnen krijgen op basis van hun doel én hun budget;
4. advies kunnen krijgen over de koeling van hun processor.

Implementeer onderstaande functionaliteiten.

### 1. Overzicht

Voorzie een GET-endpoint op:

<span style="background:#1c7ed6;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>GET</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/bouwadvies</code>

Dit endpoint geeft volgende tekst terug:

<code style="color:#845ef7">Adviespaneel Bit & Byte. Vraag advies op over je budget, je doel of je koeling.</code>

> [!IMPORTANT]
> De klasse heet AdviesController, maar de route moet <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/bouwadvies</code> zijn. Een gebruiker die naar <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/advies</code> surft, mag niets terugkrijgen.

---

### 2. Advies op basis van budget

Voorzie een GET-endpoint op:

<span style="background:#1c7ed6;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>GET</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/bouwadvies/budget/<span style="color:#e64980"><b>{bedrag}</b></span></code>

De routeparameter <code style="color:#e64980;font-weight:600">{bedrag}</code> is het budget van de klant in euro.

Geef het volgende advies:

- Minder dan 600 euro: <code style="color:#845ef7">Met dit budget kies je best een tweedehandstoestel.</code>
- 600 tot en met 1199 euro: <code style="color:#845ef7">Met dit budget bouw je een degelijke kantoor-pc.</code>
- 1200 tot en met 2499 euro: <code style="color:#845ef7">Met dit budget bouw je een volwaardige gaming-pc.</code>
- 2500 euro of meer: <code style="color:#845ef7">Met dit budget kan je vrijwel alles bouwen.</code>

---

### 3. Advies op basis van doel en budget

Voorzie een GET-endpoint op:

<span style="background:#1c7ed6;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>GET</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/bouwadvies/doel/<span style="color:#e64980"><b>{doel}</b></span>/<span style="color:#e64980"><b>{bedrag}</b></span></code>

De API moet drie doelen herkennen, elk met een eigen minimumbudget:

| Doel      | Minimumbudget |
|-----------|---------------|
| kantoor   | 600           |
| gaming    | 1200          |
| montage   | 1500          |

Geef het volgende advies:

- Het doel is onbekend: <code style="color:#845ef7">We kennen dit doel niet.</code>
- Het bedrag ligt onder het minimum: <code style="color:#845ef7">Voor een <span style="color:#e64980"><b>{doel}</b></span>-pc reken je op minstens <span style="color:#e64980"><b>{minimum}</b></span> euro. Je komt <span style="color:#e64980"><b>{tekort}</b></span> euro te kort.</code>
- Het bedrag is gelijk aan of hoger dan het minimum: <code style="color:#845ef7">Een <span style="color:#e64980"><b>{doel}</b></span>-pc is haalbaar binnen dit budget.</code>

Het tekort moet je zelf berekenen.

Bijvoorbeeld:

<code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> api/bouwadvies/doel/gaming/900</code> geeft als resultaat: <code style="color:#845ef7">Voor een gaming-pc reken je op minstens 1200 euro. Je komt 300 euro te kort.</code>
<code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> api/bouwadvies/doel/kantoor/900</code> geeft als resultaat: <code style="color:#845ef7">Een kantoor-pc is haalbaar binnen dit budget.</code>
<code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> api/bouwadvies/doel/server/900</code> geeft als resultaat: <code style="color:#845ef7">We kennen dit doel niet.</code>

---

### 4. Advies over de koeling

Voorzie een GET-endpoint op:

<span style="background:#1c7ed6;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>GET</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/bouwadvies/koeling/<span style="color:#e64980"><b>{tdp}</b></span>/<span style="color:#e64980"><b>{koeling}</b></span></code>

De routeparameter <code style="color:#e64980;font-weight:600">{tdp}</code> is het warmtevermogen van de processor in watt. De routeparameter <code style="color:#e64980;font-weight:600">{koeling}</code> is `lucht` of `water`.

Geef het volgende advies:

Bij `lucht`:

- 95 watt of minder: <code style="color:#845ef7">Een gewone luchtkoeler volstaat.</code>
- 96 tot en met 150 watt: <code style="color:#845ef7">Kies een zware luchtkoeler.</code>
- Meer dan 150 watt: <code style="color:#845ef7">Luchtkoeling volstaat niet, ga voor waterkoeling.</code>

Bij `water`:

- 250 watt of minder: <code style="color:#845ef7">Een waterkoeling van 240 mm volstaat.</code>
- Meer dan 250 watt: <code style="color:#845ef7">Kies een radiator van 360 mm of groter.</code>

Bij een andere waarde dan `lucht` of `water`: <code style="color:#845ef7">We kennen dit soort koeling niet.</code>

> [!WARNING]
> Let goed op de volgorde waarin je je controles schrijft. Kijk je eerst naar het getal en pas daarna naar het soort koeling, dan geef je voor een onbekend soort koeling toch een antwoord.

---

### 5. Alles samen: het volledige advies

<span style="background:#7048e8;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>SAMENGESTELDE OEFENING</b></span>

De drie vorige endpoints geven elk een stukje advies over een pc die verder niets met de andere twee te maken heeft. In dit laatste endpoint gaat alles over dezelfde pc.

Voorzie een GET-endpoint op:

<span style="background:#1c7ed6;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>GET</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/bouwadvies/volledig/<span style="color:#e64980"><b>{doel}</b></span>/<span style="color:#e64980"><b>{bedrag}</b></span>/<span style="color:#e64980"><b>{tdp}</b></span>/<span style="color:#e64980"><b>{koeling}</b></span></code>

Je controller kent per doel niet alleen een minimumbudget, maar ook welke koeling erbij hoort:

| Doel      | Minimumbudget | Aangeraden koeling |
|-----------|---------------|--------------------|
| kantoor   | 600           | lucht              |
| gaming    | 1200          | lucht              |
| montage   | 1500          | water              |

Geef één antwoord terug dat de volgende boodschappen na elkaar zet:

1. het budgetadvies, zoals in punt 2;
2. het advies op basis van doel en budget, zoals in punt 3;
3. het koeladvies, zoals in punt 4;
4. en, enkel wanneer de gekozen koeling niet die van de tabel is: <code style="color:#845ef7">Voor een <span style="color:#e64980"><b>{doel}</b></span>-pc raden we <span style="color:#e64980"><b>{aangeradenKoeling}</b></span> aan.</code>

Bijvoorbeeld:

<code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> api/bouwadvies/volledig/gaming/1500/140/lucht</code> geeft als resultaat:

<code style="color:#845ef7">Met dit budget bouw je een volwaardige gaming-pc. Een gaming-pc is haalbaar binnen dit budget. Kies een zware luchtkoeler.</code>

<code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> api/bouwadvies/volledig/montage/1000/200/lucht</code> geeft als resultaat:

<code style="color:#845ef7">Met dit budget bouw je een degelijke kantoor-pc. Voor een montage-pc reken je op minstens 1500 euro. Je komt 500 euro te kort. Luchtkoeling volstaat niet, ga voor waterkoeling. Voor een montage-pc raden we water aan.</code>

Wordt een onbekend doel opgegeven, geef dan enkel het bericht terug:

<code style="color:#845ef7">We kennen dit doel niet.</code>

Wordt een onbekende koeling opgegeven, dan blijven de eerste twee boodschappen staan en vervang je de derde door: <code style="color:#845ef7">We kennen dit soort koeling niet.</code> De vierde boodschap valt dan weg.

> [!NOTE]
> Merk op dat de eerste twee boodschappen elkaar mogen tegenspreken. Bij het tweede voorbeeld zegt punt 2 dat je een kantoor-pc kan bouwen, terwijl punt 3 zegt dat een montage-pc te duur is. Dat is geen fout: punt 2 kijkt enkel naar het bedrag en weet niets van het doel. Twee regels die naar hetzelfde getal kijken maar een andere vraag beantwoorden, geven nu eenmaal een ander antwoord.
> 