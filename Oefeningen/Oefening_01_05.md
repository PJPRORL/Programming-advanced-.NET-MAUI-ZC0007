# 01_05

<b>Hoofdstuk 01</b> &nbsp;·&nbsp; Controller en routeparameters &nbsp;·&nbsp; Bibliotheek &nbsp;·&nbsp; niveau <span style="color:#37b24d">●○○</span> &nbsp;·&nbsp; ±45 min

## Leerdoel

Na deze oefening kan je een API-controller maken met meerdere GET-endpoints en er routeparameters aan koppelen.

Je oefent op het omzetten van een waarde uit de URL naar een antwoord: soms via een vaste lijst van mogelijkheden, soms via een reeks getalgrenzen, soms door de tekst zelf te bewerken.

Daarnaast leer je dat een route uit meerdere vaste delen kan bestaan voor de parameter komt, zoals <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">/bibliotheek/afdeling/<span style="color:#e64980"><b>{afdeling}</b></span></code>.

## 

## Opdracht

De stadsbibliotheek De Leeszaal wil bezoekers via een kleine API wegwijs maken.

Jouw taak is om een BibliotheekController te maken met verschillende GET-endpoints.

Via de API moeten bezoekers:

1. een algemeen welkomstbericht kunnen opvragen;
2. informatie over een afdeling kunnen opvragen;
3. weten wat een te late teruggave kost;
4. de controlecode van hun lidkaart kunnen nakijken.

Implementeer onderstaande functionaliteiten.

### 1. Welkomstbericht

Voorzie een GET-endpoint op:

<span style="background:#1c7ed6;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>GET</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">/bibliotheek</code>

Dit endpoint geeft volgende tekst terug:

<code style="color:#845ef7">Welkom in De Leeszaal. Wij zijn open van dinsdag tot zaterdag.</code>

---

### 2. Informatie over een afdeling

Voorzie een GET-endpoint op:

<span style="background:#1c7ed6;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>GET</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">/bibliotheek/afdeling/<span style="color:#e64980"><b>{afdeling}</b></span></code>

De API moet drie afdelingen herkennen:

- jeugd
- strips
- studie

Geef voor iedere afdeling het bijbehorende bericht terug:

| Afdeling | Bericht                                                          |
|----------|------------------------------------------------------------------|
| jeugd    | <span style="color:#845ef7">De jeugdafdeling vind je op het gelijkvloers, achteraan links.</span> |
| strips   | <span style="color:#845ef7">De stripafdeling vind je op de eerste verdieping.</span> |
| studie   | <span style="color:#845ef7">De studiezaal vind je op de tweede verdieping en is stiltezone.</span> |

Wordt een onbekende afdeling opgegeven, geef dan het bericht terug:

<code style="color:#845ef7">Sorry, deze afdeling bestaat niet in onze bibliotheek.</code>

---

### 3. Boete bij te late teruggave

Voorzie een GET-endpoint op:

<span style="background:#1c7ed6;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>GET</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">/bibliotheek/boete/<span style="color:#e64980"><b>{dagen}</b></span></code>

De routeparameter <code style="color:#e64980;font-weight:600">{dagen}</code> stelt het aantal dagen voor dat een boek te laat is.

Geef het volgende bericht:

- 0 dagen: <code style="color:#845ef7">Je bent op tijd, er is geen boete.</code>
- 1 tot en met 7 dagen: <code style="color:#845ef7">Je betaalt een boete van 1 euro.</code>
- Meer dan 7 dagen: <code style="color:#845ef7">Je betaalt een boete van 5 euro en je lidkaart wordt geblokkeerd.</code>

Bijvoorbeeld:

<code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> /bibliotheek/boete/0</code> geeft als resultaat: <code style="color:#845ef7">Je bent op tijd, er is geen boete.</code>
<code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> /bibliotheek/boete/4</code> geeft als resultaat: <code style="color:#845ef7">Je betaalt een boete van 1 euro.</code>
<code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> /bibliotheek/boete/20</code> geeft als resultaat: <code style="color:#845ef7">Je betaalt een boete van 5 euro en je lidkaart wordt geblokkeerd.</code>

---

### 4. Controlecode van een lidkaart

Voorzie een GET-endpoint op:

<span style="background:#1c7ed6;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>GET</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">/bibliotheek/lidkaart/<span style="color:#e64980"><b>{nummer}</b></span></code>

Het endpoint ontvangt een lidkaartnummer via de route. De laatste vier tekens van dat nummer vormen de controlecode.

Geef een bericht terug met het volledige nummer en de controlecode.

Bijvoorbeeld:

<code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> /bibliotheek/lidkaart/LZ-2026-4471</code> geeft als resultaat: <code style="color:#845ef7">Lidkaart LZ-2026-4471 heeft controlecode 4471.</code>

> [!NOTE]
> Het nummer moet afkomstig zijn uit de routeparameter. Je mag dus geen nummers hardcoderen.

---

### 5. Alles samen: de lenersfiche

<span style="background:#7048e8;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>SAMENGESTELDE OEFENING</b></span>

De drie vorige endpoints staan los van elkaar. Je vraagt een afdeling op, daarna een boete die bij niemand hoort, daarna een lidkaartnummer dat verder nergens mee te maken heeft.

In dit laatste endpoint hangt alles aan één lener: je geeft één lidkaartnummer op, en je krijgt de controlecode, de afdeling waar zijn boek ligt én de boete die hij moet betalen.

Voorzie een GET-endpoint op:

<span style="background:#1c7ed6;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>GET</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">/bibliotheek/lener/<span style="color:#e64980"><b>{nummer}</b></span></code>

Je controller kent de volgende leners:

| Lidkaartnummer | Afdeling | Dagen te laat |
|----------------|----------|---------------|
| LZ-2026-4471   | jeugd    | 0             |
| LZ-2026-8820   | strips   | 4             |
| LZ-2025-1093   | studie   | 19            |

Geef één antwoord terug dat de drie boodschappen na elkaar zet:

1. het nummer met zijn controlecode, zoals in punt 4;
2. de informatie over de afdeling, zoals in punt 2;
3. de boete, zoals in punt 3.

Bijvoorbeeld:

<code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> /bibliotheek/lener/LZ-2026-8820</code> geeft als resultaat:

<code style="color:#845ef7">Lidkaart LZ-2026-8820 heeft controlecode 8820. De stripafdeling vind je op de eerste verdieping. Je betaalt een boete van 1 euro.</code>

<code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> /bibliotheek/lener/LZ-2026-4471</code> geeft als resultaat:

<code style="color:#845ef7">Lidkaart LZ-2026-4471 heeft controlecode 4471. De jeugdafdeling vind je op het gelijkvloers, achteraan links. Je bent op tijd, er is geen boete.</code>

Wordt een onbekend lidkaartnummer opgegeven, geef dan het bericht terug:

<code style="color:#845ef7">We kennen dit lidkaartnummer niet.</code>

> [!NOTE]
> De afdeling en het aantal dagen staan niet meer in de URL. Ze komen uit je eigen code, op basis van het nummer dat de gebruiker opgeeft.

> [!TIP]
> Schrijf de drie boodschappen niet opnieuw uit per lener. De logica uit punt 2, 3 en 4 moet je hier kunnen hergebruiken.
> 