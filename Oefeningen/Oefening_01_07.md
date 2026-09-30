# 01_07

<b>Hoofdstuk 01</b> &nbsp;·&nbsp; Eigen route en geneste beslissingen &nbsp;·&nbsp; Festival &nbsp;·&nbsp; niveau <span style="color:#f03e3e">●●●</span> &nbsp;·&nbsp; ±75 min

## Leerdoel

Na deze oefening kan je zelf bepalen op welke route een controller luistert, los van de naam van de klasse.

Je leert dat `[controller]` in de route een plaatshouder is die je mag vervangen, en dat de gebruiker van je API nooit te zien krijgt hoe je klassen en methodes heten.

Daarnaast oefen je op geneste beslissingen waarbij de volgorde van je controles bepaalt of je antwoord klopt. Een regel die je te vroeg controleert, houdt de rest tegen.

## 

## Opdracht

Festival Klankdal wil bezoekers een infopaneel geven. De organisatie eist dat alle adressen met `festival` in de URL werken, terwijl de ontwikkelaars de klasse liever `PodiumController` noemen.

Jouw taak is om een PodiumController te maken die luistert op de route <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/festival</code>.

Via de API moeten bezoekers:

1. een overzicht van het festival kunnen opvragen;
2. weten wie er op een podium speelt;
3. de prijs van hun ticket kennen;
4. weten wat ze moeten meenemen.

Implementeer onderstaande functionaliteiten.

### 1. Overzicht

Voorzie een GET-endpoint op:

<span style="background:#1c7ed6;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>GET</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/festival</code>

Dit endpoint geeft volgende tekst terug:

<code style="color:#845ef7">Welkom op Klankdal. Drie podia, twee dagen, één weide.</code>

> [!IMPORTANT]
> De klasse heet PodiumController, maar de route moet <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/festival</code> zijn. Een bezoeker die naar <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/podium</code> surft, mag niets terugkrijgen.

---

### 2. Wie speelt er op een podium

Voorzie een GET-endpoint op:

<span style="background:#1c7ed6;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>GET</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/festival/podium/<span style="color:#e64980"><b>{podium}</b></span>/<span style="color:#e64980"><b>{uur}</b></span></code>

De routeparameter <code style="color:#e64980;font-weight:600">{uur}</code> is een getal van 0 tot en met 23.

De API moet drie podia herkennen:

Bij `hoofdpodium`:

- Voor 18 uur: <code style="color:#845ef7">Op het hoofdpodium spelen nu de opwarmers.</code>
- 18 tot en met 21 uur: <code style="color:#845ef7">Op het hoofdpodium speelt nu de hoofdact.</code>
- Na 21 uur: <code style="color:#845ef7">Het hoofdpodium is gesloten.</code>

Bij `tent`:

- Voor 22 uur: <code style="color:#845ef7">In de tent draaien de dj's van de dag.</code>
- Vanaf 22 uur: <code style="color:#845ef7">In de tent begint nu de nachtset.</code>

Bij `strand`:

- Op elk uur: <code style="color:#845ef7">Op het strandpodium speelt akoestische muziek.</code>

Bij een onbekend podium: <code style="color:#845ef7">Dit podium staat niet op het terrein.</code>

---

### 3. Prijs van een ticket

Voorzie een GET-endpoint op:

<span style="background:#1c7ed6;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>GET</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/festival/ticket/<span style="color:#e64980"><b>{type}</b></span>/<span style="color:#e64980"><b>{leeftijd}</b></span></code>

De API moet drie tickettypes herkennen, elk met een eigen basisprijs:

| Type  | Basisprijs |
|-------|------------|
| dag   | 65         |
| combi | 120        |
| vip   | 210        |

Bereken de prijs als volgt:

- Jonger dan 12 jaar: gratis, welk van de drie types ook gekozen wordt. Geef terug: <code style="color:#845ef7">Kinderen jonger dan 12 komen gratis binnen.</code>
- 12 tot en met 17 jaar: de helft van de basisprijs. Geef terug: <code style="color:#845ef7">Een <span style="color:#e64980"><b>{type}</b></span>ticket kost jou <span style="color:#e64980"><b>{prijs}</b></span> euro.</code>
- 18 jaar of ouder: de volle basisprijs. Geef terug: <code style="color:#845ef7">Een <span style="color:#e64980"><b>{type}</b></span>ticket kost jou <span style="color:#e64980"><b>{prijs}</b></span> euro.</code>

Bij een onbekend type: <code style="color:#845ef7">Dit tickettype bestaat niet.</code>

> [!WARNING]
> Let op de volgorde. Een kind van 8 jaar dat een onbekend tickettype opgeeft, moet <code style="color:#845ef7">Dit tickettype bestaat niet.</code> terugkrijgen en niet de gratis boodschap.

Bijvoorbeeld:

<code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> api/festival/ticket/combi/8</code> geeft als resultaat: <code style="color:#845ef7">Kinderen jonger dan 12 komen gratis binnen.</code>
<code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> api/festival/ticket/combi/15</code> geeft als resultaat: <code style="color:#845ef7">Een combiticket kost jou 60 euro.</code>
<code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> api/festival/ticket/vip/30</code> geeft als resultaat: <code style="color:#845ef7">Een vipticket kost jou 210 euro.</code>

---

### 4. Wat moet je meenemen

Voorzie een GET-endpoint op:

<span style="background:#1c7ed6;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>GET</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/festival/weer/<span style="color:#e64980"><b>{graden}</b></span>/<span style="color:#e64980"><b>{regenkans}</b></span></code>

De routeparameter <code style="color:#e64980;font-weight:600">{regenkans}</code> is een percentage van 0 tot en met 100.

Geef het volgende advies, in deze volgorde van belangrijkheid:

1. Is de regenkans groter dan 70: <code style="color:#845ef7">Neem een regenjas mee.</code>
2. Is het anders warmer dan 28 graden: <code style="color:#845ef7">Neem water en zonnecrème mee.</code>
3. Is het anders kouder dan 12 graden: <code style="color:#845ef7">Trek een warme trui aan.</code>
4. In alle andere gevallen: <code style="color:#845ef7">Ideaal festivalweer.</code>

Bijvoorbeeld:

<code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> api/festival/weer/31/85</code> geeft als resultaat: <code style="color:#845ef7">Neem een regenjas mee.</code>
<code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> api/festival/weer/31/20</code> geeft als resultaat: <code style="color:#845ef7">Neem water en zonnecrème mee.</code>
<code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> api/festival/weer/9/20</code> geeft als resultaat: <code style="color:#845ef7">Trek een warme trui aan.</code>
<code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> api/festival/weer/20/20</code> geeft als resultaat: <code style="color:#845ef7">Ideaal festivalweer.</code>

---

### 5. Alles samen: het bezoekersplan

<span style="background:#7048e8;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>SAMENGESTELDE OEFENING</b></span>

De drie vorige endpoints staan los van elkaar: je vraagt een ticketprijs op, daarna wie er ergens speelt, daarna wat het weer doet op een moment dat met dat podium niets te maken heeft.

In dit laatste endpoint hangt alles aan één bezoeker op één moment.

Voorzie een GET-endpoint op:

<span style="background:#1c7ed6;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>GET</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/festival/plan/<span style="color:#e64980"><b>{type}</b></span>/<span style="color:#e64980"><b>{leeftijd}</b></span>/<span style="color:#e64980"><b>{podium}</b></span>/<span style="color:#e64980"><b>{uur}</b></span></code>

De weersvoorspelling staat niet meer in de URL. Je controller kent ze per moment van de dag:

| Moment              | Graden | Regenkans |
|---------------------|--------|-----------|
| voor 12 uur         | 14     | 20        |
| 12 tot en met 17 uur| 29     | 10        |
| 18 tot en met 21 uur| 22     | 80        |
| vanaf 22 uur        | 11     | 30        |

Geef één antwoord terug dat de drie boodschappen na elkaar zet:

1. de ticketprijs, zoals in punt 3;
2. wie er op dat podium op dat uur speelt, zoals in punt 2;
3. wat de bezoeker moet meenemen, zoals in punt 4, gerekend met het weer uit de tabel hierboven.

Bijvoorbeeld:

<code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> api/festival/plan/combi/15/hoofdpodium/19</code> geeft als resultaat:

<code style="color:#845ef7">Een combiticket kost jou 60 euro. Op het hoofdpodium speelt nu de hoofdact. Neem een regenjas mee.</code>

<code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> api/festival/plan/dag/30/strand/14</code> geeft als resultaat:

<code style="color:#845ef7">Een dagticket kost jou 65 euro. Op het strandpodium speelt akoestische muziek. Neem water en zonnecrème mee.</code>

Wordt een onbekend tickettype opgegeven, geef dan enkel het bericht terug:

<code style="color:#845ef7">Dit tickettype bestaat niet.</code>

Wordt een onbekend podium opgegeven, dan blijven de eerste en de derde boodschap staan en vervang je de tweede door: <code style="color:#845ef7">Dit podium staat niet op het terrein.</code>

> [!NOTE]
> Het uur doet nu twee dingen tegelijk: het bepaalt wie er speelt én welk weer er staat. Eén routeparameter die twee verschillende beslissingen voedt, is precies wat het samenbrengen van deze oefening betekent.
> 