# 01_02

<b>Hoofdstuk 01</b> &nbsp;·&nbsp; Controller en routeparameters &nbsp;·&nbsp; PC-onderdelen &nbsp;·&nbsp; niveau <span style="color:#37b24d">●○○</span> &nbsp;·&nbsp; ±45 min

## Leerdoel

Na deze oefening kan je zelfstandig een API-controller opzetten en er meerdere GET-endpoints aan koppelen.

Je oefent opnieuw op routes en routeparameters, maar je gebruikt de waarde uit de URL nu ook om er iets mee te berekenen of te bewerken, in plaats van ze enkel door te geven.

Daarnaast merk je dat de naam van je C#-methode niets uitmaakt voor de gebruiker van je API. Enkel de route en het HTTP-verb bepalen wat er bereikbaar is.

## 

## Opdracht

De computerwinkel Bit & Byte wil aan de balie een kleine API waarmee medewerkers snel informatie over onderdelen kunnen opvragen.

Jouw taak is om een OnderdelenController te maken met verschillende GET-endpoints.

Via de API moeten medewerkers:

1. een algemeen openingsbericht kunnen opvragen;
2. uitleg over een soort onderdeel kunnen krijgen;
3. de voorraadstatus van een onderdeel kunnen opvragen;
4. een artikelcode kunnen laten nakijken.

Implementeer onderstaande functionaliteiten.

### 1. Openingsbericht

Voorzie een GET-endpoint op:

<span style="background:#1c7ed6;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>GET</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">/onderdelen</code>

Dit endpoint geeft volgende tekst terug:

<code style="color:#845ef7">Welkom bij de onderdelenbalie van Bit & Byte.</code>

---

### 2. Uitleg over een soort onderdeel

Voorzie een GET-endpoint op:

<span style="background:#1c7ed6;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>GET</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">/onderdelen/soort/<span style="color:#e64980"><b>{soort}</b></span></code>

De soort van het onderdeel wordt meegegeven via de URL.

De API moet drie soorten herkennen:

- moederbord
- processor
- videokaart

Geef voor iedere soort het bijbehorende bericht terug:

| Soort       | Bericht                                                     |
|-------------|-------------------------------------------------------------|
| moederbord  | <span style="color:#845ef7">Het moederbord verbindt alle onderdelen met elkaar.</span> |
| processor   | <span style="color:#845ef7">De processor voert alle berekeningen uit.</span> |
| videokaart  | <span style="color:#845ef7">De videokaart tekent het beeld voor je scherm.</span> |

Wordt een onbekende soort opgegeven, geef dan het bericht terug:

<code style="color:#845ef7">Sorry, dit soort onderdeel verkopen wij niet.</code>

---

### 3. Voorraadstatus

Voorzie een GET-endpoint op:

<span style="background:#1c7ed6;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>GET</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">/onderdelen/voorraad/<span style="color:#e64980"><b>{aantal}</b></span></code>

De routeparameter <code style="color:#e64980;font-weight:600">{aantal}</code> stelt het aantal stuks voor dat nog in het magazijn ligt.

Geef de volgende status:

- 0 stuks: <code style="color:#845ef7">Niet op voorraad, bestel bij de leverancier.</code>
- 1 tot en met 4 stuks: <code style="color:#845ef7">Beperkt op voorraad, hou dit in het oog.</code>
- Meer dan 4 stuks: <code style="color:#845ef7">Ruim op voorraad.</code>

Bijvoorbeeld:

<code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> /onderdelen/voorraad/0</code> geeft als resultaat: <code style="color:#845ef7">Niet op voorraad, bestel bij de leverancier.</code>
<code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> /onderdelen/voorraad/3</code> geeft als resultaat: <code style="color:#845ef7">Beperkt op voorraad, hou dit in het oog.</code>
<code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> /onderdelen/voorraad/12</code> geeft als resultaat: <code style="color:#845ef7">Ruim op voorraad.</code>

---

### 4. Artikelcode nakijken

Voorzie een GET-endpoint op:

<span style="background:#1c7ed6;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>GET</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">/onderdelen/code/<span style="color:#e64980"><b>{artikelcode}</b></span></code>

Het endpoint ontvangt een artikelcode via de route.

Geef een bericht terug dat de code in hoofdletters toont en vermeldt uit hoeveel tekens ze bestaat.

Bijvoorbeeld:

<code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> /onderdelen/code/mb-b650-01</code> geeft als resultaat: <code style="color:#845ef7">Artikelcode MB-B650-01 telt 10 tekens.</code>

> [!NOTE]
> De code moet afkomstig zijn uit de routeparameter. Je mag dus geen artikelcodes hardcoderen.

---

### 5. Alles samen: de onderdelenfiche

<span style="background:#7048e8;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>SAMENGESTELDE OEFENING</b></span>

De drie vorige endpoints staan los van elkaar. Je vraagt uitleg over een soort, maar de voorraad die je daarna opvraagt hoort bij niets, en de artikelcode al evenmin.

In dit laatste endpoint hangt alles aan één onderwerp: je geeft één soort onderdeel op, en je krijgt de uitleg, de voorraad én de artikelcode van dát onderdeel.

Voorzie een GET-endpoint op:

<span style="background:#1c7ed6;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>GET</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">/onderdelen/fiche/<span style="color:#e64980"><b>{soort}</b></span></code>

Je controller kent per soort de volgende gegevens:

| Soort       | Artikelcode  | Voorraad |
|-------------|--------------|----------|
| moederbord  | MB-B650-01   | 12       |
| processor   | CPU-7600X-01 | 3        |
| videokaart  | GPU-4070-01  | 0        |

Geef één antwoord terug dat de drie boodschappen na elkaar zet:

1. de uitleg over de soort, zoals in punt 2;
2. de voorraadstatus, zoals in punt 3, maar dan voor de voorraad uit de tabel hierboven;
3. de artikelcode in hoofdletters met het aantal tekens, zoals in punt 4, maar dan voor de code uit de tabel hierboven.

Bijvoorbeeld:

<code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> /onderdelen/fiche/processor</code> geeft als resultaat:

<code style="color:#845ef7">De processor voert alle berekeningen uit. Beperkt op voorraad, hou dit in het oog. Artikelcode CPU-7600X-01 telt 12 tekens.</code>

<code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> /onderdelen/fiche/videokaart</code> geeft als resultaat:

<code style="color:#845ef7">De videokaart tekent het beeld voor je scherm. Niet op voorraad, bestel bij de leverancier. Artikelcode GPU-4070-01 telt 11 tekens.</code>

Wordt een onbekende soort opgegeven, geef dan het bericht terug:

<code style="color:#845ef7">Sorry, dit soort onderdeel verkopen wij niet.</code>

> [!NOTE]
> De voorraad en de artikelcode staan niet meer in de URL. Ze komen uit je eigen code, op basis van de soort die de gebruiker opgeeft.

> [!TIP]
> Schrijf de drie boodschappen niet opnieuw uit per soort. De logica die je in punt 2, 3 en 4 al geschreven hebt, moet je hier kunnen hergebruiken. Merk je dat je aan het kopiëren bent, kijk dan eerst naar hoe je je code kan opsplitsen.
> 