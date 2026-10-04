# 02_05

<b>Hoofdstuk 02</b> &nbsp;·&nbsp; Volledige CRUD &nbsp;·&nbsp; Fietsverhuur &nbsp;·&nbsp; niveau <span style="color:#f08c00">●●○</span> &nbsp;·&nbsp; ±90 min

## Leerdoel

Na deze oefening kan je de volledige CRUD-cyclus op een Model uitvoeren via HTTP: ophalen, toevoegen, bijwerken en verwijderen.

Je leert data ontvangen in de body van een verzoek in plaats van in de URL, en je leert per actie de juiste statuscode terugsturen: <code style="color:#37b24d;font-weight:600">201 Created</code> bij een POST, <code style="color:#37b24d;font-weight:600">204 No Content</code> bij een PUT en een DELETE.

Daarnaast leer je foutieve invoer afwijzen met <code style="color:#f08c00;font-weight:600">400 Bad Request</code>, zodat er geen onmogelijke gegevens in je lijst terechtkomen.

## 

## Opdracht

Fietsverhuur Trapdoor beheert zijn vloot nu nog in een rekenblad. Ze willen dat vervangen door een API, zodat de balie en de website met dezelfde gegevens werken.

Jouw taak is om een Model en een FietsController te maken met een volledige CRUD.

Via de API moet de balie:

1. alle fietsen kunnen opvragen;
2. één fiets kunnen opvragen;
3. een nieuwe fiets kunnen toevoegen;
4. een bestaande fiets kunnen bijwerken;
5. een fiets uit de vloot kunnen halen.

Implementeer onderstaande functionaliteiten.

### 1. Het model

Maak in de map Models een klasse `Fiets` met volgende gegevens:

| Gegeven        | Soort waarde |
|----------------|--------------|
| Id             | geheel getal |
| Type           | tekst        |
| Framemaat      | geheel getal |
| PrijsPerDag    | kommagetal   |
| IsBeschikbaar  | ja of nee    |

---

### 2. De controller en de tijdelijke lijst

Maak een FietsController die luistert op <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/fiets</code>, met een lijst van minstens vier fietsen.

> [!WARNING]
> Zorg ervoor dat een fiets die je toevoegt, er bij een volgend verzoek nog steeds staat. Test dit: voeg er een toe via POST en vraag daarna de volledige lijst op. Staat je nieuwe fiets er niet meer bij, dan is er iets mis met de manier waarop je de lijst hebt gedeclareerd.

---

### 3. Alles ophalen en één ophalen

Voorzie een GET-endpoint op <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/fiets</code> dat de volledige lijst teruggeeft met <code style="color:#37b24d;font-weight:600">200 OK</code>.

Voorzie een GET-endpoint op <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/fiets/<span style="color:#e64980"><b>{id}</b></span></code> dat één fiets teruggeeft met <code style="color:#37b24d;font-weight:600">200 OK</code>, of <code style="color:#f08c00;font-weight:600">404 Not Found</code> met het bericht:

<code style="color:#845ef7">We vonden geen fiets met Id <span style="color:#e64980"><b>{id}</b></span>.</code>

---

### 4. Een fiets toevoegen

Voorzie een POST-endpoint op:

<span style="background:#2f9e44;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>POST</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/fiets</code>

De gegevens van de nieuwe fiets komen in de body van het verzoek. Het Id wordt door de API zelf bepaald. Stuurt de client toch een Id mee, dan wordt dat overschreven.

Slaagt de toevoeging, geef dan statuscode <code style="color:#37b24d;font-weight:600">201 Created</code> terug, samen met de nieuwe fiets én het adres waarop ze opgevraagd kan worden.

Weiger de toevoeging met statuscode <code style="color:#f08c00;font-weight:600">400 Bad Request</code> wanneer:

- het type leeg is: <code style="color:#845ef7">Een fiets moet een type hebben.</code>
- de framemaat kleiner is dan 44 of groter dan 62: <code style="color:#845ef7">De framemaat moet tussen 44 en 62 liggen.</code>
- de prijs per dag 0 of lager is: <code style="color:#845ef7">De prijs per dag moet groter zijn dan 0.</code>

---

### 5. Een fiets bijwerken

Voorzie een PUT-endpoint op:

<span style="background:#d9822b;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>PUT</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/fiets/<span style="color:#e64980"><b>{id}</b></span></code>

Het Id staat in de URL, de nieuwe gegevens staan in de body.

Bestaat de fiets niet, geef dan <code style="color:#f08c00;font-weight:600">404 Not Found</code> met het bericht:

<code style="color:#845ef7">Kan geen fiets bijwerken met Id <span style="color:#e64980"><b>{id}</b></span>, omdat deze niet bestaat.</code>

Dezelfde drie controles uit punt 4 gelden ook hier. Zijn de gegevens ongeldig, geef dan <code style="color:#f08c00;font-weight:600">400 Bad Request</code>.

Slaagt de wijziging, geef dan <code style="color:#37b24d;font-weight:600">204 No Content</code> terug.

---

### 6. Een fiets verwijderen

Voorzie een DELETE-endpoint op:

<span style="background:#e03131;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>DELETE</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/fiets/<span style="color:#e64980"><b>{id}</b></span></code>

Bestaat de fiets niet, geef dan <code style="color:#f08c00;font-weight:600">404 Not Found</code> met het bericht:

<code style="color:#845ef7">Kan fiets met Id <span style="color:#e64980"><b>{id}</b></span> niet verwijderen, omdat deze niet gevonden is.</code>

Slaagt de verwijdering, geef dan <code style="color:#37b24d;font-weight:600">204 No Content</code> terug.

---

### 7. Testen

Test elk endpoint via Scalar of Postman en noteer welke statuscode je terugkrijgt. Controleer in het bijzonder:

- of een POST met framemaat 70 effectief <code style="color:#f08c00;font-weight:600">400</code> geeft en niet in de lijst belandt;
- of een DELETE op een Id dat je net verwijderd hebt, de tweede keer <code style="color:#f08c00;font-weight:600">404</code> geeft.

---

### 8. Alles samen: een fiets vervangen

<span style="background:#7048e8;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>SAMENGESTELDE OEFENING</b></span>

Trapdoor vervangt geregeld een versleten fiets door een nieuwe van hetzelfde type. Vandaag zijn dat twee verzoeken, en tussen die twee in klopt de vloot even niet.

In dit laatste endpoint doe je het in één keer.

Voorzie een PUT-endpoint op:

<span style="background:#d9822b;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>PUT</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/fiets/vervang/<span style="color:#e64980"><b>{id}</b></span></code>

Het Id in de URL is de fiets die uit de vloot gaat. De gegevens van de nieuwe fiets staan in de body. Het Id van de nieuwe fiets bepaalt de API zelf.

Het endpoint moet:

- <code style="color:#f08c00;font-weight:600">404 Not Found</code> geven wanneer de te vervangen fiets niet bestaat, met het bericht <code style="color:#845ef7">Kan fiets met Id <span style="color:#e64980"><b>{id}</b></span> niet vervangen, omdat deze niet gevonden is.</code>
- <code style="color:#f08c00;font-weight:600">400 Bad Request</code> geven wanneer de nieuwe fiets niet door de drie controles uit punt 4 geraakt;
- bij succes de oude fiets verwijderen, de nieuwe toevoegen, en <code style="color:#37b24d;font-weight:600">201 Created</code> teruggeven met de nieuwe fiets en het adres waarop ze opgevraagd kan worden.

> [!IMPORTANT]
> Er is één regel die je nergens in dit bestand eerder bent tegengekomen: **mislukt de vervanging, dan mag er niets veranderd zijn.** Wie een ongeldige nieuwe fiets doorstuurt, mag zijn oude niet kwijt zijn.

> [!WARNING]
> Dat maakt de volgorde van je stappen belangrijk. Verwijder je eerst en controleer je daarna, dan is de oude fiets al weg op het moment dat je <code style="color:#f08c00;font-weight:600">400</code> teruggeeft.

Test dit expliciet:

1. Vraag de volledige lijst op en onthoud welke fietsen erin staan.
2. Stuur een vervanging voor een bestaande fiets, met framemaat 70 in de body.
3. Controleer dat je <code style="color:#f08c00;font-weight:600">400</code> krijgt.
4. Vraag de volledige lijst opnieuw op. De oude fiets moet er nog steeds in staan.
