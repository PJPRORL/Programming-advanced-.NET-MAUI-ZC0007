# 01_06

<b>Hoofdstuk 01</b> &nbsp;·&nbsp; Twee routeparameters in één route &nbsp;·&nbsp; Zwembad &nbsp;·&nbsp; niveau <span style="color:#f08c00">●●○</span> &nbsp;·&nbsp; ±60 min

## Leerdoel

Na deze oefening kan je een route opbouwen die twee routeparameters bevat, en de twee waarden samen gebruiken om tot één antwoord te komen.

Je leert dat een tekstparameter en een getalparameter in dezelfde route kunnen voorkomen, en dat je daar een geneste beslissing op kan bouwen.

Daarnaast oefen je op rekenen met routeparameters: delen, een rest overhouden, en dat resultaat in je antwoord verwerken.

## 

## Opdracht

Zwembad De Waterval wil een API waarmee bezoekers hun bezoek kunnen voorbereiden.

Jouw taak is om een ZwembadController te maken met verschillende GET-endpoints.

Via de API moeten bezoekers:

1. de openingsuren kunnen opvragen;
2. hun tarief kunnen berekenen;
3. weten hoe warm het water is;
4. weten hoe druk het in de banen wordt.

Implementeer onderstaande functionaliteiten.

### 1. Openingsuren

Voorzie een GET-endpoint op:

<span style="background:#1c7ed6;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>GET</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">/zwembad</code>

Dit endpoint geeft volgende tekst terug:

<code style="color:#845ef7">Zwembad De Waterval is elke dag open van 7 tot 21 uur.</code>

---

### 2. Tarief berekenen

Voorzie een GET-endpoint op:

<span style="background:#1c7ed6;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>GET</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">/zwembad/tarief/<span style="color:#e64980"><b>{leeftijd}</b></span>/<span style="color:#e64980"><b>{dag}</b></span></code>

De routeparameter <code style="color:#e64980;font-weight:600">{dag}</code> is `week` of `weekend`.

Geef het volgende tarief:

Bij `week`:

- Jonger dan 3 jaar: `Gratis.`
- 3 tot en met 17 jaar: <code style="color:#845ef7">Je betaalt 2 euro.</code>
- 18 tot en met 64 jaar: <code style="color:#845ef7">Je betaalt 4 euro.</code>
- 65 jaar of ouder: <code style="color:#845ef7">Je betaalt 3 euro.</code>

Bij `weekend` gelden dezelfde leeftijdsgroepen, maar ligt elk tarief 1 euro hoger. Wie gratis binnen mag, blijft gratis binnen.

Bij een andere waarde dan `week` of `weekend`: <code style="color:#845ef7">We kennen deze dag niet.</code>

Bijvoorbeeld:

<code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> /zwembad/tarief/2/weekend</code> geeft als resultaat: `Gratis.`
<code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> /zwembad/tarief/30/week</code> geeft als resultaat: <code style="color:#845ef7">Je betaalt 4 euro.</code>
<code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> /zwembad/tarief/30/weekend</code> geeft als resultaat: <code style="color:#845ef7">Je betaalt 5 euro.</code>

---

### 3. Watertemperatuur

Voorzie een GET-endpoint op:

<span style="background:#1c7ed6;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>GET</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">/zwembad/temperatuur/<span style="color:#e64980"><b>{graden}</b></span></code>

Geef het volgende bericht:

- Minder dan 24 graden: <code style="color:#845ef7">Het water is fris, neem je tijd om in te stappen.</code>
- 24 tot en met 28 graden: <code style="color:#845ef7">Het water heeft een aangename temperatuur.</code>
- Meer dan 28 graden: <code style="color:#845ef7">Het water is warm, ideaal voor de kleinsten.</code>

---

### 4. Drukte in de banen

Voorzie een GET-endpoint op:

<span style="background:#1c7ed6;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>GET</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">/zwembad/banen/<span style="color:#e64980"><b>{aantalBanen}</b></span>/<span style="color:#e64980"><b>{aantalZwemmers}</b></span></code>

Geef het volgende bericht:

- Er zijn evenveel of minder zwemmers dan banen: <code style="color:#845ef7">Iedereen krijgt een eigen baan.</code>
- Er zijn meer zwemmers dan banen, en de verdeling gaat gelijk op: <code style="color:#845ef7">Er zwemmen <span style="color:#e64980"><b>{aantal}</b></span> zwemmers per baan.</code>
- Er zijn meer zwemmers dan banen, en er blijft een rest over: <code style="color:#845ef7">Er zwemmen <span style="color:#e64980"><b>{aantal}</b></span> zwemmers per baan, <span style="color:#e64980"><b>{rest}</b></span> banen krijgen er één extra.</code>

Zowel <code style="color:#e64980;font-weight:600">{aantal}</code> als <code style="color:#e64980;font-weight:600">{rest}</code> moet je zelf berekenen. Ze staan niet in de URL.

Bijvoorbeeld:

<code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> /zwembad/banen/4/3</code> geeft als resultaat: <code style="color:#845ef7">Iedereen krijgt een eigen baan.</code>
<code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> /zwembad/banen/4/8</code> geeft als resultaat: <code style="color:#845ef7">Er zwemmen 2 zwemmers per baan.</code>
<code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> /zwembad/banen/4/10</code> geeft als resultaat: <code style="color:#845ef7">Er zwemmen 2 zwemmers per baan, 2 banen krijgen er één extra.</code>

---

### 5. Alles samen: het bezoekrapport

<span style="background:#7048e8;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>SAMENGESTELDE OEFENING</b></span>

De drie vorige endpoints gaan elk over iets anders. In dit laatste endpoint gaan ze alle drie over hetzelfde bezoek.

Voorzie een GET-endpoint op:

<span style="background:#1c7ed6;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>GET</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">/zwembad/bezoek/<span style="color:#e64980"><b>{leeftijd}</b></span>/<span style="color:#e64980"><b>{dag}</b></span>/<span style="color:#e64980"><b>{graden}</b></span>/<span style="color:#e64980"><b>{aantalZwemmers}</b></span></code>

Het aantal banen staat niet in de URL. De Waterval heeft er zes, en dat weet je controller.

Geef één antwoord terug dat de drie boodschappen na elkaar zet:

1. het tarief, zoals in punt 2;
2. de watertemperatuur, zoals in punt 3;
3. de drukte in de banen, zoals in punt 4, gerekend met zes banen.

Bijvoorbeeld:

<code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> /zwembad/bezoek/30/weekend/26/14</code> geeft als resultaat:

<code style="color:#845ef7">Je betaalt 5 euro. Het water heeft een aangename temperatuur. Er zwemmen 2 zwemmers per baan, 2 banen krijgen er één extra.</code>

<code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> /zwembad/bezoek/2/week/22/4</code> geeft als resultaat:

<code style="color:#845ef7">Gratis. Het water is fris, neem je tijd om in te stappen. Iedereen krijgt een eigen baan.</code>

Wordt een andere dag dan `week` of `weekend` opgegeven, geef dan enkel het bericht terug:

<code style="color:#845ef7">We kennen deze dag niet.</code>

> [!NOTE]
> Het aantal banen zit nu vast in je controller in plaats van in de URL. Dat lijkt een detail, maar het is een eerste stap richting het volgende hoofdstuk: gegevens die bij het zwembad horen, horen bij het zwembad en niet bij het verzoek van de bezoeker.
> 