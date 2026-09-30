# 01_03

<b>Hoofdstuk 01</b> &nbsp;·&nbsp; Twee routeparameters in één route &nbsp;·&nbsp; PC-onderdelen &nbsp;·&nbsp; niveau <span style="color:#f08c00">●●○</span> &nbsp;·&nbsp; ±60 min

## Leerdoel

Na deze oefening kan je een route opbouwen die meer dan één routeparameter bevat.

Je leert twee waarden uit dezelfde URL opvangen, ze met elkaar vergelijken en er berekeningen mee uitvoeren voor je een antwoord terugstuurt.

Daarnaast oefen je op het verschil tussen een vaste tekst in je route en een variabel stuk: <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">/compatibiliteit/socket/...</code> is vast, <code style="color:#e64980;font-weight:600">{socketProcessor}</code> niet.

## 

## Opdracht

Bit & Byte wil een tweede API die medewerkers helpt bij het nakijken of twee onderdelen bij elkaar passen.

Jouw taak is om een CompatibiliteitController te maken met verschillende GET-endpoints.

Via de API moeten medewerkers:

1. een algemeen bericht kunnen opvragen;
2. twee sockets met elkaar kunnen laten vergelijken;
3. kunnen nakijken of een voeding zwaar genoeg is;
4. kunnen nakijken hoeveel geheugensloten er vrij blijven.

Implementeer onderstaande functionaliteiten.

### 1. Algemeen bericht

Voorzie een GET-endpoint op:

<span style="background:#1c7ed6;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>GET</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">/compatibiliteit</code>

Dit endpoint geeft volgende tekst terug:

<code style="color:#845ef7">Compatibiliteitsbalie Bit & Byte. Geef twee onderdelen op om ze te vergelijken.</code>

---

### 2. Twee sockets vergelijken

Voorzie een GET-endpoint op:

<span style="background:#1c7ed6;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>GET</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">/compatibiliteit/socket/<span style="color:#e64980"><b>{socketProcessor}</b></span>/<span style="color:#e64980"><b>{socketMoederbord}</b></span></code>

Beide sockets worden via de URL meegegeven.

Komen ze overeen, geef dan terug:

<code style="color:#845ef7">De processor past op dit moederbord.</code>

Komen ze niet overeen, geef dan terug:

<code style="color:#845ef7">De processor past niet: <span style="color:#e64980"><b>{socketProcessor}</b></span> tegenover <span style="color:#e64980"><b>{socketMoederbord}</b></span>.</code>

Het verschil tussen hoofdletters en kleine letters mag geen rol spelen. `am5` en `AM5` moeten als dezelfde socket beschouwd worden.

Bijvoorbeeld:

<code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> /compatibiliteit/socket/AM5/am5</code> geeft als resultaat: <code style="color:#845ef7">De processor past op dit moederbord.</code>
<code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> /compatibiliteit/socket/AM5/LGA1700</code> geeft als resultaat: <code style="color:#845ef7">De processor past niet: AM5 tegenover LGA1700.</code>

---

### 3. Voeding nakijken

Voorzie een GET-endpoint op:

<span style="background:#1c7ed6;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>GET</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">/compatibiliteit/voeding/<span style="color:#e64980"><b>{wattage}</b></span>/<span style="color:#e64980"><b>{verbruik}</b></span></code>

De routeparameter <code style="color:#e64980;font-weight:600">{wattage}</code> is het vermogen van de voeding, <code style="color:#e64980;font-weight:600">{verbruik}</code> is het opgetelde verbruik van de volledige build. Beide in watt.

We rekenen met een marge van 100 watt.

Geef het volgende advies:

- Het wattage ligt lager dan het verbruik: <code style="color:#845ef7">De voeding is te zwak voor deze build.</code>
- Het wattage ligt minder dan 100 watt boven het verbruik: <code style="color:#845ef7">De voeding volstaat, maar de marge is krap.</code>
- Het wattage ligt 100 watt of meer boven het verbruik: <code style="color:#845ef7">De voeding is ruim voldoende.</code>

Bijvoorbeeld:

<code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> /compatibiliteit/voeding/550/620</code> geeft als resultaat: <code style="color:#845ef7">De voeding is te zwak voor deze build.</code>
<code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> /compatibiliteit/voeding/650/620</code> geeft als resultaat: <code style="color:#845ef7">De voeding volstaat, maar de marge is krap.</code>
<code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> /compatibiliteit/voeding/850/620</code> geeft als resultaat: <code style="color:#845ef7">De voeding is ruim voldoende.</code>

---

### 4. Vrije geheugensloten

Voorzie een GET-endpoint op:

<span style="background:#1c7ed6;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>GET</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">/compatibiliteit/sloten/<span style="color:#e64980"><b>{aantalSloten}</b></span>/<span style="color:#e64980"><b>{aantalModules}</b></span></code>

Geef het volgende terug:

- Er zijn meer modules dan sloten: <code style="color:#845ef7">Er passen maar <span style="color:#e64980"><b>{aantalSloten}</b></span> modules in dit moederbord.</code>
- Er zijn evenveel modules als sloten: <code style="color:#845ef7">Alle sloten worden gebruikt.</code>
- Er zijn minder modules dan sloten: <code style="color:#845ef7">Er blijven nog <span style="color:#e64980"><b>{aantal}</b></span> sloten vrij.</code>

De waarde <code style="color:#e64980;font-weight:600">{aantal}</code> in het laatste bericht moet je zelf berekenen. Ze staat niet in de URL.

Bijvoorbeeld:

<code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> /compatibiliteit/sloten/2/4</code> geeft als resultaat: <code style="color:#845ef7">Er passen maar 2 modules in dit moederbord.</code>
<code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> /compatibiliteit/sloten/4/4</code> geeft als resultaat: <code style="color:#845ef7">Alle sloten worden gebruikt.</code>
<code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> /compatibiliteit/sloten/4/2</code> geeft als resultaat: <code style="color:#845ef7">Er blijven nog 2 sloten vrij.</code>

---

### 5. Alles samen: het bouwrapport

<span style="background:#7048e8;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>SAMENGESTELDE OEFENING</b></span>

De drie vorige endpoints controleren elk iets anders, maar telkens op losse getallen die nergens bij horen. In dit laatste endpoint horen ze bij één build.

Voorzie een GET-endpoint op:

<span style="background:#1c7ed6;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>GET</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">/compatibiliteit/build/<span style="color:#e64980"><b>{build}</b></span>/<span style="color:#e64980"><b>{socketProcessor}</b></span>/<span style="color:#e64980"><b>{wattage}</b></span>/<span style="color:#e64980"><b>{aantalModules}</b></span></code>

Je controller kent drie standaardbuilds:

| Build    | Socket moederbord | Geheugensloten | Verbruik |
|----------|-------------------|----------------|----------|
| kantoor  | AM5               | 2              | 180      |
| gaming   | AM5               | 4              | 520      |
| montage  | LGA1700           | 4              | 610      |

De klant kiest een build en geeft daarbij op welke processor hij wil, welke voeding hij wil en hoeveel geheugenmodules hij wil. De socket van het moederbord, het aantal geheugensloten en het verbruik van de build komen uit de tabel, niet uit de URL.

Geef één antwoord terug dat de drie oordelen na elkaar zet:

1. de socketvergelijking, zoals in punt 2;
2. het voedingsadvies, zoals in punt 3;
3. het aantal vrije geheugensloten, zoals in punt 4.

Bijvoorbeeld:

<code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> /compatibiliteit/build/gaming/AM5/600/4</code> geeft als resultaat:

<code style="color:#845ef7">De processor past op dit moederbord. De voeding volstaat, maar de marge is krap. Alle sloten worden gebruikt.</code>

<code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> /compatibiliteit/build/kantoor/LGA1700/450/4</code> geeft als resultaat:

<code style="color:#845ef7">De processor past niet: LGA1700 tegenover AM5. De voeding is ruim voldoende. Er passen maar 2 modules in dit moederbord.</code>

Wordt een onbekende build opgegeven, geef dan enkel het bericht terug:

<code style="color:#845ef7">We kennen deze build niet.</code>

> [!IMPORTANT]
> Let op: de drie oordelen mogen elkaar niet beïnvloeden. Ook wanneer de processor niet past, moet je nog steeds iets over de voeding en de sloten zeggen. Een klant wil weten wat er allemaal mis is, niet enkel wat er als eerste misloopt.
> 