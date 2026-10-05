# 02_06

<b>Hoofdstuk 02</b> &nbsp;·&nbsp; Soft delete en plaatscontrole &nbsp;·&nbsp; Concertzaal &nbsp;·&nbsp; niveau <span style="color:#f03e3e">●●●</span> &nbsp;·&nbsp; ±2 uur

## Leerdoel

Na deze oefening kan je een API bouwen waarin annuleren niet betekent dat gegevens verdwijnen.

Je leert een soft delete toepassen: het item krijgt een label dat zegt dat het geannuleerd is, en je API filtert het weg zodat de client het niet meer ziet. De gegevens zelf blijven bewaard.

Daarnaast leer je controles schrijven die niet alleen naar het binnenkomende object kijken, maar ook naar wat er al in je lijst staat. Twee bezoekers op dezelfde stoel is iets wat je API zelf moet tegenhouden.

## 

## Opdracht

Concertzaal De Notenbalk verkoopt genummerde plaatsen. Een medewerker moet een ticket kunnen annuleren, maar de boekhouding wil nadien nog kunnen zien wat er verkocht is geweest.

Jouw taak is om een Model en een TicketController te maken.

Via de API moet een medewerker:

1. de geldige tickets kunnen opvragen;
2. alle tickets kunnen opvragen, ook de geannuleerde;
3. één ticket kunnen opvragen;
4. een ticket kunnen verkopen, wijzigen en annuleren;
5. een geannuleerd ticket kunnen herstellen;
6. de opbrengst van de avond kunnen opvragen.

Implementeer onderstaande functionaliteiten.

### 1. Het model

Maak in de map Models een klasse `Ticket` met volgende gegevens:

| Gegeven        | Soort waarde |
|----------------|--------------|
| Id             | geheel getal |
| Naam           | tekst        |
| Rij            | geheel getal |
| Stoel          | geheel getal |
| Prijs          | kommagetal   |
| IsGeannuleerd  | ja of nee    |

---

### 2. De controller en de tijdelijke lijst

Maak een TicketController die luistert op <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/ticket</code>, met een lijst van minstens vijf tickets. Zorg dat er minstens één geannuleerd ticket bij zit.

---

### 3. De geldige tickets

Voorzie een GET-endpoint op:

<span style="background:#1c7ed6;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>GET</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/ticket</code>

Dit endpoint geeft enkel de niet-geannuleerde tickets terug, met statuscode <code style="color:#37b24d;font-weight:600">200 OK</code>.

---

### 4. Alle tickets

Voorzie een GET-endpoint op:

<span style="background:#1c7ed6;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>GET</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/ticket/alles</code>

Dit endpoint geeft alle tickets terug, geannuleerde inbegrepen, met statuscode <code style="color:#37b24d;font-weight:600">200 OK</code>. Dit is het endpoint voor de boekhouding.

---

### 5. Eén ticket

Voorzie een GET-endpoint op:

<span style="background:#1c7ed6;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>GET</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/ticket/<span style="color:#e64980"><b>{id}</b></span></code>

Bestaat het ticket en is het niet geannuleerd, geef het dan terug met <code style="color:#37b24d;font-weight:600">200 OK</code>.

Bestaat het ticket niet, of is het geannuleerd, geef dan <code style="color:#f08c00;font-weight:600">404 Not Found</code> met het bericht:

<code style="color:#845ef7">We vonden geen geldig ticket met Id <span style="color:#e64980"><b>{id}</b></span>.</code>

> [!NOTE]
> Een geannuleerd ticket bestaat nog in je lijst, maar voor deze client bestaat het niet. Dat is precies wat een soft delete betekent.

---

### 6. Een ticket verkopen

Voorzie een POST-endpoint op:

<span style="background:#2f9e44;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>POST</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/ticket</code>

Het Id bepaalt de API zelf. Stuurt de client toch een Id mee, dan wordt dat overschreven. Een nieuw ticket is nooit geannuleerd, ook als de client iets anders meestuurt.

Slaagt de verkoop, geef dan <code style="color:#37b24d;font-weight:600">201 Created</code> terug, samen met het nieuwe ticket en het adres waarop het opgevraagd kan worden.

Weiger de verkoop met <code style="color:#f08c00;font-weight:600">400 Bad Request</code> wanneer:

- de naam leeg is: <code style="color:#845ef7">Een ticket moet op naam staan.</code>
- de rij kleiner is dan 1: <code style="color:#845ef7">De rij moet minstens 1 zijn.</code>
- de stoel kleiner is dan 1: <code style="color:#845ef7">De stoel moet minstens 1 zijn.</code>
- de prijs 0 of lager is: <code style="color:#845ef7">De prijs moet groter zijn dan 0.</code>
- die rij en stoel al bezet zijn door een niet-geannuleerd ticket: <code style="color:#845ef7">Rij <span style="color:#e64980"><b>{rij}</b></span> stoel <span style="color:#e64980"><b>{stoel}</b></span> is al verkocht.</code>

> [!TIP]
> Die laatste controle kan je niet doen door enkel naar het binnenkomende ticket te kijken. Je moet je bestaande lijst doorzoeken.

---

### 7. Een ticket wijzigen

Voorzie een PUT-endpoint op:

<span style="background:#d9822b;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>PUT</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/ticket/<span style="color:#e64980"><b>{id}</b></span></code>

Werkt enkel op niet-geannuleerde tickets. Bestaat het ticket niet of is het geannuleerd, geef dan <code style="color:#f08c00;font-weight:600">404 Not Found</code>.

Dezelfde controles uit punt 6 gelden ook hier, met één verschil: een ticket mag zijn eigen plaats houden. Verhuis je ticket 3 van rij 2 stoel 5 naar rij 2 stoel 5, dan is dat geen dubbele verkoop.

Slaagt de wijziging, geef dan <code style="color:#37b24d;font-weight:600">204 No Content</code> terug.

---

### 8. Een ticket annuleren

Voorzie een DELETE-endpoint op:

<span style="background:#e03131;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>DELETE</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/ticket/<span style="color:#e64980"><b>{id}</b></span></code>

Dit endpoint verwijdert het ticket niet uit je lijst. Het zet het enkel op geannuleerd, waardoor de plaats weer vrijkomt.

Slaagt de annulering, geef dan <code style="color:#37b24d;font-weight:600">204 No Content</code> terug.

Bestaat het ticket niet, of is het al geannuleerd, geef dan <code style="color:#f08c00;font-weight:600">404 Not Found</code> met het bericht:

<code style="color:#845ef7">Kan ticket met Id <span style="color:#e64980"><b>{id}</b></span> niet annuleren, omdat het niet gevonden is.</code>

> [!TIP]
> Roep dit endpoint twee keer na elkaar op met hetzelfde Id. De eerste keer krijg je <code style="color:#37b24d;font-weight:600">204</code>, de tweede keer <code style="color:#f08c00;font-weight:600">404</code>.

---

### 9. Een ticket herstellen

Voorzie een PUT-endpoint op:

<span style="background:#d9822b;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>PUT</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/ticket/herstel/<span style="color:#e64980"><b>{id}</b></span></code>

Dit endpoint maakt een geannuleerd ticket weer geldig.

- Het ticket bestaat niet: <code style="color:#f08c00;font-weight:600">404 Not Found</code>
- Het ticket is niet geannuleerd: <code style="color:#f08c00;font-weight:600">400 Bad Request</code> met het bericht <code style="color:#845ef7">Dit ticket is niet geannuleerd.</code>
- De plaats is intussen aan iemand anders verkocht: <code style="color:#f08c00;font-weight:600">400 Bad Request</code> met het bericht <code style="color:#845ef7">Rij <span style="color:#e64980"><b>{rij}</b></span> stoel <span style="color:#e64980"><b>{stoel}</b></span> is intussen aan iemand anders verkocht.</code>
- Het herstel is gelukt: <code style="color:#37b24d;font-weight:600">204 No Content</code>

Test dit scenario volledig: annuleer een ticket, verkoop diezelfde plaats aan iemand anders, en probeer daarna het eerste ticket te herstellen.

---

### 10. De opbrengst

Voorzie een GET-endpoint op:

<span style="background:#1c7ed6;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>GET</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/ticket/opbrengst</code>

Dit endpoint geeft de totale opbrengst van de avond terug met <code style="color:#37b24d;font-weight:600">200 OK</code>: de prijzen van alle niet-geannuleerde tickets opgeteld.

Annuleer een ticket via punt 8 en vraag daarna dit endpoint opnieuw op. De opbrengst moet gedaald zijn.

---

### 11. Alles samen: een ticket verhuizen

<span style="background:#7048e8;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>SAMENGESTELDE OEFENING</b></span>

De tien vorige punten behandelen elk één stukje van de zaal: een plaats verkopen, een plaats vrijgeven, nakijken of een plaats al bezet is, de opbrengst tellen. Een medewerker die iemand naar een andere stoel wil zetten, moet vandaag annuleren en opnieuw verkopen, en tussen die twee handelingen in klopt de zaal even niet.

In dit laatste punt doe je het in één keer, en alle regels uit dit bestand komen er samen.

Voorzie een PUT-endpoint op:

<span style="background:#d9822b;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>PUT</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/ticket/verhuis/<span style="color:#e64980"><b>{id}</b></span>/<span style="color:#e64980"><b>{rij}</b></span>/<span style="color:#e64980"><b>{stoel}</b></span></code>

Het endpoint verplaatst een bestaand ticket naar een andere plaats. De oude plaats komt daarbij vrij.

Het endpoint moet:

- <code style="color:#f08c00;font-weight:600">404 Not Found</code> geven wanneer het ticket niet bestaat of geannuleerd is, met het bericht <code style="color:#845ef7">We vonden geen geldig ticket met Id <span style="color:#e64980"><b>{id}</b></span>.</code>
- <code style="color:#f08c00;font-weight:600">400 Bad Request</code> geven wanneer de rij of de stoel kleiner is dan 1, met de berichten uit punt 6;
- <code style="color:#f08c00;font-weight:600">400 Bad Request</code> geven wanneer de bezoeker al op die plaats zit, met het bericht <code style="color:#845ef7">Dit ticket zit al op rij <span style="color:#e64980"><b>{rij}</b></span> stoel <span style="color:#e64980"><b>{stoel}</b></span>.</code>
- <code style="color:#f08c00;font-weight:600">400 Bad Request</code> geven wanneer die plaats al bezet is door een ánder niet-geannuleerd ticket, met het bericht <code style="color:#845ef7">Rij <span style="color:#e64980"><b>{rij}</b></span> stoel <span style="color:#e64980"><b>{stoel}</b></span> is al verkocht.</code>
- bij succes de rij en de stoel aanpassen en <code style="color:#37b24d;font-weight:600">204 No Content</code> teruggeven.

> [!WARNING]
> Let op het verschil tussen de derde en de vierde regel. Beide gaan over een bezette plaats, maar in het ene geval is het je eigen plaats en in het andere die van iemand anders. Die twee gevallen scheiden is precies waar je in punt 7 al op gebotst bent.

De prijs verandert niet. Een verhuizing mag de opbrengst dus niet beïnvloeden.

#### Testscenario

Doorloop deze reeks en controleer na elke stap wat er gebeurt:

1. Vraag de opbrengst op via punt 10 en noteer ze.
2. Verhuis een ticket naar een vrije plaats. Je krijgt <code style="color:#37b24d;font-weight:600">204</code>. Vraag de opbrengst opnieuw op: ze is niet veranderd.
3. Verhuis datzelfde ticket naar precies dezelfde plaats. Je krijgt <code style="color:#f08c00;font-weight:600">400</code>.
4. Verhuis het naar de plaats van een ander geldig ticket. Je krijgt <code style="color:#f08c00;font-weight:600">400</code>.
5. Annuleer dat andere ticket via punt 8. Verhuis nu opnieuw naar die plaats. Nu krijg je <code style="color:#37b24d;font-weight:600">204</code>, want de plaats is vrijgekomen.
6. Probeer het geannuleerde ticket te herstellen via punt 9. Je krijgt <code style="color:#f08c00;font-weight:600">400</code>, want die plaats is intussen ingenomen.
7. Vraag de opbrengst een laatste keer op. Ze is gedaald met de prijs van het geannuleerde ticket, en met niets meer.

> [!WARNING]
> Loopt stap 5 mis, dan geeft je annulering de plaats niet vrij. Loopt stap 6 mis, dan kijkt je herstel niet naar wat er intussen gebeurd is. Beide fouten zijn onzichtbaar zolang je de endpoints apart test. Dat is de reden waarom deze oefening bestaat.
> 