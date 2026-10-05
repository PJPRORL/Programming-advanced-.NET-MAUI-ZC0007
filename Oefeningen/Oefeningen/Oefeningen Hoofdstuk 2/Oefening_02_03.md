# 02_03

<b>Hoofdstuk 02</b> &nbsp;·&nbsp; Soft delete en berekeningen &nbsp;·&nbsp; PC-onderdelen &nbsp;·&nbsp; niveau <span style="color:#f03e3e">●●●</span> &nbsp;·&nbsp; ±2 uur

## Leerdoel

Na deze oefening kan je een API bouwen waarin verwijderen niet betekent dat gegevens verdwijnen.

Je leert een soft delete toepassen: het item krijgt een label dat zegt dat het niet meer actief is, en je API filtert het weg zodat de client het niet meer ziet. De gegevens zelf blijven bewaard.

Daarnaast leer je dat dezelfde lijst verschillende antwoorden kan geven, afhankelijk van het endpoint, en dat je statuscodes moet kiezen op basis van wat de client mag weten.

## 

## Opdracht

Bit & Byte stelt bestellingen samen uit losse lijnen. Een medewerker moet een lijn kunnen schrappen, maar de boekhouding wil nadien nog kunnen zien wat er ooit op de bestelling stond.

Jouw taak is om een Model en een BestellijnController te maken.

Via de API moet een medewerker:

1. de actieve bestellijnen kunnen opvragen;
2. alle bestellijnen kunnen opvragen, ook de geschrapte;
3. één bestellijn kunnen opvragen;
4. een bestellijn kunnen toevoegen, bijwerken en schrappen;
5. een geschrapte bestellijn kunnen herstellen;
6. de totale waarde van de bestelling kunnen opvragen.

Implementeer onderstaande functionaliteiten.

### 1. Het model

Maak in de map Models een klasse `Bestellijn` met volgende gegevens:

| Gegeven      | Soort waarde |
|--------------|--------------|
| Id           | geheel getal |
| Omschrijving | tekst        |
| Aantal       | geheel getal |
| StukPrijs    | kommagetal   |
| IsActief     | ja of nee    |

---

### 2. De controller en de tijdelijke lijst

Maak een BestellijnController die luistert op <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/bestellijn</code>, met een lijst van minstens vijf bestellijnen. Zorg dat er minstens één lijn bij zit die al geschrapt is.

---

### 3. De actieve bestellijnen

Voorzie een GET-endpoint op:

<span style="background:#1c7ed6;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>GET</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/bestellijn</code>

Dit endpoint geeft enkel de actieve bestellijnen terug, met statuscode <code style="color:#37b24d;font-weight:600">200 OK</code>. Geschrapte lijnen mag de client hier niet zien.

---

### 4. Alle bestellijnen

Voorzie een GET-endpoint op:

<span style="background:#1c7ed6;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>GET</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/bestellijn/alles</code>

Dit endpoint geeft alle bestellijnen terug, geschrapte inbegrepen, met statuscode <code style="color:#37b24d;font-weight:600">200 OK</code>. Dit is het endpoint voor de boekhouding.

---

### 5. Eén bestellijn

Voorzie een GET-endpoint op:

<span style="background:#1c7ed6;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>GET</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/bestellijn/<span style="color:#e64980"><b>{id}</b></span></code>

Bestaat de lijn en is ze actief, geef ze dan terug met <code style="color:#37b24d;font-weight:600">200 OK</code>.

Bestaat de lijn niet, of is ze geschrapt, geef dan <code style="color:#f08c00;font-weight:600">404 Not Found</code> met het bericht:

<code style="color:#845ef7">We vonden geen actieve bestellijn met Id <span style="color:#e64980"><b>{id}</b></span>.</code>

> [!NOTE]
> Een geschrapte lijn bestaat nog in je lijst, maar voor deze client bestaat ze niet. Dat is precies wat een soft delete betekent.

---

### 6. Een bestellijn toevoegen

Voorzie een POST-endpoint op:

<span style="background:#2f9e44;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>POST</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/bestellijn</code>

Het Id bepaalt de API zelf. Stuurt de client toch een Id mee, dan wordt dat overschreven. Een nieuwe lijn is altijd actief, ook als de client iets anders meestuurt.

Slaagt de toevoeging, geef dan <code style="color:#37b24d;font-weight:600">201 Created</code> terug, samen met de nieuwe lijn en het adres waarop ze opgevraagd kan worden.

Weiger de toevoeging met <code style="color:#f08c00;font-weight:600">400 Bad Request</code> wanneer:

- de omschrijving leeg is: <code style="color:#845ef7">Een bestellijn moet een omschrijving hebben.</code>
- het aantal kleiner is dan 1: <code style="color:#845ef7">Het aantal moet minstens 1 zijn.</code>
- de stukprijs 0 of lager is: <code style="color:#845ef7">De stukprijs moet groter zijn dan 0.</code>

---

### 7. Een bestellijn bijwerken

Voorzie een PUT-endpoint op:

<span style="background:#d9822b;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>PUT</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/bestellijn/<span style="color:#e64980"><b>{id}</b></span></code>

Werkt enkel op actieve lijnen. Bestaat de lijn niet of is ze geschrapt, geef dan <code style="color:#f08c00;font-weight:600">404 Not Found</code>.

Dezelfde drie controles uit punt 6 gelden ook hier.

Slaagt de wijziging, geef dan <code style="color:#37b24d;font-weight:600">204 No Content</code> terug.

---

### 8. Een bestellijn schrappen

Voorzie een DELETE-endpoint op:

<span style="background:#e03131;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>DELETE</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/bestellijn/<span style="color:#e64980"><b>{id}</b></span></code>

Dit endpoint verwijdert de lijn niet uit je lijst. Het zet ze enkel op non-actief.

Slaagt het schrappen, geef dan <code style="color:#37b24d;font-weight:600">204 No Content</code> terug.

Bestaat de lijn niet, of is ze al geschrapt, geef dan <code style="color:#f08c00;font-weight:600">404 Not Found</code> met het bericht:

<code style="color:#845ef7">Kan bestellijn met Id <span style="color:#e64980"><b>{id}</b></span> niet schrappen, omdat deze niet gevonden is.</code>

> [!TIP]
> Roep dit endpoint twee keer na elkaar op met hetzelfde Id. De eerste keer krijg je <code style="color:#37b24d;font-weight:600">204</code>, de tweede keer <code style="color:#f08c00;font-weight:600">404</code>.

---

### 9. Een bestellijn herstellen

Voorzie een PUT-endpoint op:

<span style="background:#d9822b;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>PUT</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/bestellijn/herstel/<span style="color:#e64980"><b>{id}</b></span></code>

Dit endpoint zet een geschrapte lijn terug op actief.

- De lijn bestaat niet: <code style="color:#f08c00;font-weight:600">404 Not Found</code>
- De lijn is al actief: <code style="color:#f08c00;font-weight:600">400 Bad Request</code> met het bericht <code style="color:#845ef7">Deze bestellijn is niet geschrapt.</code>
- Het herstel is gelukt: <code style="color:#37b24d;font-weight:600">204 No Content</code>

---

### 10. De totale waarde

Voorzie een GET-endpoint op:

<span style="background:#1c7ed6;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>GET</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/bestellijn/totaal</code>

Dit endpoint geeft de totale waarde van de bestelling terug met <code style="color:#37b24d;font-weight:600">200 OK</code>. Per actieve lijn vermenigvuldig je het aantal met de stukprijs, en die bedragen tel je op.

Geschrapte lijnen tellen niet mee.

Schrap een lijn via punt 8 en vraag daarna dit endpoint opnieuw op. Het totaal moet gedaald zijn.

---

### 11. Alles samen: de bestelfiche en de bestelling schrappen

<span style="background:#7048e8;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>SAMENGESTELDE OEFENING</b></span>

De tien vorige punten bekijken de bestelling telkens door één venster: de actieve lijnen, of alle lijnen, of het totaal. Wie wil weten hoe de bestelling ervoor staat, moet drie endpoints oproepen en de antwoorden zelf bij elkaar leggen.

In dit laatste punt maak je één venster waar alles in past, en één actie die de hele bestelling in één keer schrapt.

#### De fiche

Maak in de map Models een tweede klasse `BestelFiche` met volgende gegevens:

| Gegeven               | Soort waarde         |
|-----------------------|----------------------|
| AantalActieveLijnen   | geheel getal         |
| AantalGeschrapteLijnen| geheel getal         |
| TotaalActief          | kommagetal           |
| TotaalGeschrapt       | kommagetal           |
| DuursteActieveLijn    | een bestellijn       |
| IsLeeg                | ja of nee            |

`TotaalActief` is de berekening uit punt 10. `TotaalGeschrapt` is diezelfde berekening, maar dan over de geschrapte lijnen.

`DuursteActieveLijn` is de actieve lijn met de hoogste waarde, dus aantal maal stukprijs. Zijn er geen actieve lijnen, dan blijft dit leeg.

`IsLeeg` staat op ja zodra er geen enkele actieve lijn meer is.

Voorzie een GET-endpoint op:

<span style="background:#1c7ed6;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>GET</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/bestellijn/fiche</code>

Dit endpoint geeft de fiche terug met statuscode <code style="color:#37b24d;font-weight:600">200 OK</code>.

#### De hele bestelling schrappen

Voorzie een DELETE-endpoint op:

<span style="background:#e03131;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>DELETE</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/bestellijn</code>

Let op het ontbrekende Id: dit endpoint werkt op de hele bestelling.

Het zet alle actieve lijnen op non-actief. Net als in punt 8 verdwijnt er niets uit je lijst.

- Waren er actieve lijnen, geef dan <code style="color:#37b24d;font-weight:600">200 OK</code> terug met de bijgewerkte fiche. Hier gebruik je dus geen <code style="color:#37b24d;font-weight:600">204</code>, want er is wél iets te tonen.
- Waren er geen actieve lijnen meer, geef dan <code style="color:#f08c00;font-weight:600">400 Bad Request</code> met het bericht <code style="color:#845ef7">Er zijn geen actieve bestellijnen om te schrappen.</code>

#### Testscenario

Doorloop deze reeks en controleer na elke stap de fiche:

1. Vraag de fiche op. Noteer `AantalActieveLijnen` en `TotaalActief`.
2. Voeg een lijn toe via punt 6. Het aantal actieve lijnen stijgt met één, het totaal stijgt met aantal maal stukprijs.
3. Schrap die lijn via punt 8. Het aantal actieve lijnen daalt weer, `AantalGeschrapteLijnen` stijgt, en `TotaalGeschrapt` stijgt met hetzelfde bedrag als waarmee `TotaalActief` daalde.
4. Herstel die lijn via punt 9. Alles staat weer zoals na stap 2.
5. Schrap de hele bestelling via het nieuwe DELETE-endpoint. `IsLeeg` staat op ja, `TotaalActief` op 0, en `DuursteActieveLijn` is leeg.
6. Roep het DELETE-endpoint nog eens op. Je krijgt <code style="color:#f08c00;font-weight:600">400</code>.

> [!WARNING]
> Verspringt er in stap 3 iets dat niet klopt, dan zit de fout niet in je fiche maar in je soft delete. De fiche is namelijk niets meer dan een spiegel van je lijst.
> 