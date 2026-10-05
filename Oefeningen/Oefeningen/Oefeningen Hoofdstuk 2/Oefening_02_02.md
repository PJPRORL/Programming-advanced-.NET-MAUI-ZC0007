# 02_02

<b>Hoofdstuk 02</b> &nbsp;·&nbsp; Volledige CRUD &nbsp;·&nbsp; PC-onderdelen &nbsp;·&nbsp; niveau <span style="color:#f08c00">●●○</span> &nbsp;·&nbsp; ±90 min

## Leerdoel

Na deze oefening kan je de volledige CRUD-cyclus op een Model uitvoeren via HTTP: ophalen, toevoegen, bijwerken en verwijderen.

Je leert data ontvangen in de body van een verzoek in plaats van in de URL, en je leert per actie de juiste statuscode terugsturen: <code style="color:#37b24d;font-weight:600">201 Created</code> bij een POST, <code style="color:#37b24d;font-weight:600">204 No Content</code> bij een PUT en een DELETE.

Daarnaast leer je foutieve invoer afwijzen met <code style="color:#f08c00;font-weight:600">400 Bad Request</code>, zodat je API niet zomaar onmogelijke gegevens in zijn lijst laat binnensluipen.

## 

## Opdracht

Bit & Byte breidt het assortiment uit met ventilatoren. De inkoopafdeling wil ze via de API kunnen beheren zonder tussenkomst van een ontwikkelaar.

Jouw taak is om een Model en een VentilatorController te maken met een volledige CRUD.

Via de API moet de inkoopafdeling:

1. alle ventilatoren kunnen opvragen;
2. één ventilator kunnen opvragen;
3. een nieuwe ventilator kunnen toevoegen;
4. een bestaande ventilator kunnen bijwerken;
5. een ventilator kunnen verwijderen.

Implementeer onderstaande functionaliteiten.

### 1. Het model

Maak in de map Models een klasse `Ventilator` met volgende gegevens:

| Gegeven      | Soort waarde |
|--------------|--------------|
| Id           | geheel getal |
| Merk         | tekst        |
| AfmetingInMm | geheel getal |
| Verlichting  | tekst        |
| Prijs        | kommagetal   |

---

### 2. De controller en de tijdelijke lijst

Maak een VentilatorController die luistert op <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/ventilator</code>, met een lijst van minstens vier ventilatoren.

> [!WARNING]
> Zorg ervoor dat een ventilator die je toevoegt, er bij een volgend verzoek nog steeds staat. Test dit: voeg er een toe via POST en vraag daarna de volledige lijst op. Staat je nieuwe ventilator er niet meer bij, dan is er iets mis met de manier waarop je de lijst hebt gedeclareerd.

---

### 3. Alles ophalen en één ophalen

Voorzie een GET-endpoint op <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/ventilator</code> dat de volledige lijst teruggeeft met <code style="color:#37b24d;font-weight:600">200 OK</code>.

Voorzie een GET-endpoint op <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/ventilator/<span style="color:#e64980"><b>{id}</b></span></code> dat één ventilator teruggeeft met <code style="color:#37b24d;font-weight:600">200 OK</code>, of <code style="color:#f08c00;font-weight:600">404 Not Found</code> met het bericht:

<code style="color:#845ef7">We vonden geen ventilator met Id <span style="color:#e64980"><b>{id}</b></span>.</code>

---

### 4. Een ventilator toevoegen

Voorzie een POST-endpoint op:

<span style="background:#2f9e44;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>POST</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/ventilator</code>

De gegevens van de nieuwe ventilator komen in de body van het verzoek. Het Id wordt door de API zelf bepaald. Stuurt de client toch een Id mee, dan wordt dat overschreven.

Slaagt de toevoeging, geef dan statuscode <code style="color:#37b24d;font-weight:600">201 Created</code> terug, samen met de nieuwe ventilator én het adres waarop hij opgevraagd kan worden.

Weiger de toevoeging met statuscode <code style="color:#f08c00;font-weight:600">400 Bad Request</code> wanneer:

- het merk leeg is: <code style="color:#845ef7">Een ventilator moet een merk hebben.</code>
- de afmeting niet 120 of 140 is: <code style="color:#845ef7">We verkopen enkel ventilatoren van 120 of 140 mm.</code>
- de prijs 0 of lager is: <code style="color:#845ef7">De prijs moet groter zijn dan 0.</code>

---

### 5. Een ventilator bijwerken

Voorzie een PUT-endpoint op:

<span style="background:#d9822b;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>PUT</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/ventilator/<span style="color:#e64980"><b>{id}</b></span></code>

Het Id staat in de URL, de nieuwe gegevens staan in de body.

Bestaat de ventilator niet, geef dan <code style="color:#f08c00;font-weight:600">404 Not Found</code> met het bericht:

<code style="color:#845ef7">Kan geen ventilator bijwerken met Id <span style="color:#e64980"><b>{id}</b></span>, omdat deze niet bestaat.</code>

Dezelfde drie controles uit punt 4 gelden ook hier. Zijn de gegevens ongeldig, geef dan <code style="color:#f08c00;font-weight:600">400 Bad Request</code>.

Slaagt de wijziging, geef dan <code style="color:#37b24d;font-weight:600">204 No Content</code> terug.

---

### 6. Een ventilator verwijderen

Voorzie een DELETE-endpoint op:

<span style="background:#e03131;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>DELETE</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/ventilator/<span style="color:#e64980"><b>{id}</b></span></code>

Bestaat de ventilator niet, geef dan <code style="color:#f08c00;font-weight:600">404 Not Found</code> met het bericht:

<code style="color:#845ef7">Kan ventilator met Id <span style="color:#e64980"><b>{id}</b></span> niet verwijderen, omdat deze niet gevonden is.</code>

Slaagt de verwijdering, geef dan <code style="color:#37b24d;font-weight:600">204 No Content</code> terug.

---

### 7. Testen

Test elk endpoint via Scalar of Postman en noteer welke statuscode je terugkrijgt. Controleer in het bijzonder:

- of een POST met een afmeting van 92 mm effectief <code style="color:#f08c00;font-weight:600">400</code> geeft en niet in de lijst belandt;
- of een DELETE op een Id dat je net verwijderd hebt, de tweede keer <code style="color:#f08c00;font-weight:600">404</code> geeft.

---

### 8. Alles samen: een ventilator vervangen

<span style="background:#7048e8;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>SAMENGESTELDE OEFENING</b></span>

De inkoopafdeling doet vaak hetzelfde: een model uit het assortiment halen en er meteen een nieuw model voor in de plaats zetten. Vandaag zijn dat twee verzoeken, en tussen die twee in staat het assortiment even verkeerd.

In dit laatste endpoint doe je het in één keer.

Voorzie een PUT-endpoint op:

<span style="background:#d9822b;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>PUT</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/ventilator/vervang/<span style="color:#e64980"><b>{id}</b></span></code>

Het Id in de URL is de ventilator die uit het assortiment gaat. De gegevens van de nieuwe ventilator staan in de body. Het Id van de nieuwe ventilator bepaalt de API zelf.

Het endpoint moet:

- <code style="color:#f08c00;font-weight:600">404 Not Found</code> geven wanneer de te vervangen ventilator niet bestaat, met het bericht <code style="color:#845ef7">Kan ventilator met Id <span style="color:#e64980"><b>{id}</b></span> niet vervangen, omdat deze niet gevonden is.</code>
- <code style="color:#f08c00;font-weight:600">400 Bad Request</code> geven wanneer de nieuwe ventilator niet door de drie controles uit punt 4 geraakt;
- bij succes de oude ventilator verwijderen, de nieuwe toevoegen, en <code style="color:#37b24d;font-weight:600">201 Created</code> teruggeven met de nieuwe ventilator en het adres waarop hij opgevraagd kan worden.

> [!IMPORTANT]
> Er is één regel die je nergens in dit bestand eerder bent tegengekomen: **mislukt de vervanging, dan mag er niets veranderd zijn.** Een klant die een ongeldige nieuwe ventilator doorstuurt, mag zijn oude niet kwijt zijn.

> [!WARNING]
> Dat maakt de volgorde van je stappen belangrijk. Verwijder je eerst en controleer je daarna, dan is de oude ventilator al weg op het moment dat je <code style="color:#f08c00;font-weight:600">400</code> teruggeeft.

Test dit expliciet:

1. Vraag de volledige lijst op en onthoud welke ventilatoren erin staan.
2. Stuur een vervanging voor een bestaande ventilator, met een afmeting van 92 mm in de body.
3. Controleer dat je <code style="color:#f08c00;font-weight:600">400</code> krijgt.
4. Vraag de volledige lijst opnieuw op. De oude ventilator moet er nog steeds in staan.
