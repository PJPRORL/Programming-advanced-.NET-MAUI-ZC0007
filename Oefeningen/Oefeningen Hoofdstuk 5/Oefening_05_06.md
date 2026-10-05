# 05_06

<b>Hoofdstuk 05</b> &nbsp;·&nbsp; Bestand of database, asynchroon &nbsp;·&nbsp; Concertzaal &nbsp;·&nbsp; niveau <span style="color:#f03e3e">●●●</span> &nbsp;·&nbsp; ±2 uur

## Leerdoel

Na deze oefening kan je één asynchroon contract laten volgen door twee repositories die op een totaal andere manier werken: één met een bestand en één met een database.

Je leert dat async niet enkel bij databases hoort: ook een bestand lezen of schrijven kan asynchroon, en ook daar mag je je threads niet laten wachten.

Daarnaast laat je de database het zware werk doen: filteren en sorteren gebeurt in SQL, niet in het geheugen van je API.

## 

## Opdracht

De ticket-API van De Notenbalk uit 04_06 kan wisselen tussen een repository in het geheugen en een repository in een JSON-bestand, en heeft ernaast een database klaarstaan. Nu krijgt die database haar eigen repository, de derde. De versie in het geheugen verdwijnt, de bestandsversie blijft als reserve.

Jouw taak is om het contract asynchroon te maken, een repository te schrijven die met de database werkt, je bestandsrepository mee asynchroon te maken, en te bewijzen dat beide precies hetzelfde antwoorden.

Via de API moet een medewerker, net als in 04_06:

1. de geldige en alle tickets kunnen opvragen, en één ticket;
2. een ticket kunnen verkopen, wijzigen, annuleren en herstellen;
3. de opbrengst van de avond kunnen opvragen;
4. een ticket kunnen verhuizen naar een andere plaats;

en daarnaast:

5. de geldige tickets van één rij kunnen opvragen.

Implementeer onderstaande functionaliteiten.

### 1. Je vertrekpunt

Vertrek van je eigen oplossing van 04_06. Je database `ConcertzaalDb` bevat de zes tickets uit 04_06, en heeft geen kolom `Categorie` meer.

| Id | Naam             | Rij | Stoel | Prijs | IsGeannuleerd |
|----|------------------|-----|-------|-------|---------------|
| 1  | Sofie Peeters    | 1   | 5     | 45.00 | nee           |
| 2  | Tom Janssens     | 1   | 6     | 45.00 | nee           |
| 3  | Lisa Maes        | 3   | 12    | 35.00 | nee           |
| 4  | Karim El Idrissi | 3   | 13    | 35.00 | ja            |
| 5  | Emma Claes       | 7   | 2     | 25.00 | nee           |
| 6  | Noah Willems     | 7   | 3     | 25.00 | nee           |

Alle endpoints uit 04_06 moeten na deze oefening nog precies hetzelfde antwoorden, met dezelfde controles en dezelfde berichten.

> [!TIP]
> Je zal in deze oefening vaak terug willen naar de startgegevens. Maak de tabel dan leeg, zet haar teller terug, en voer het SQL-script uit 04_06 opnieuw uit. De structuur van de database pas je nooit met de hand aan, enkel de gegevens.

---

### 2. Het contract asynchroon

Pas `ITicketRepository` aan, zodat elke methode een `Task` teruggeeft en het achtervoegsel `Async` draagt.

Je `InMemoryTicketRepository` mag weg, zoals in de les.

---

### 3. De bestandsrepository asynchroon

Pas je `BestandTicketRepository` aan het nieuwe contract aan. Hij leest en schrijft `tickets.json` voortaan asynchroon.

Een constructor kan niet wachten. Lees het bestand dus niet meer in je constructor, maar telkens wanneer een methode de gegevens nodig heeft. Bestaat het bestand dan nog niet, dan geldt nog altijd de startlijst.

> [!NOTE]
> Bestanden lezen en schrijven kost net als een database tijd waarin je thread niets anders doet. Daarom bestaan er ook voor bestanden asynchrone methodes. Zoek ze op: hun namen volgen dezelfde afspraak als die van Entity Framework Core.

> [!WARNING]
> Een methode die met `async` gemarkeerd is maar nergens wacht, compileert met een waarschuwing en werkt verder gewoon synchroon. Lees de waarschuwingen in je build: elke `async`-methode in je project hoort minstens één keer te wachten.

---

### 4. De databaserepository

Maak een klasse `TicketRepository` die `ITicketRepository` implementeert. Ze krijgt je `ConcertzaalContext` binnen via haar constructor en werkt met de tabel `Tickets`, met de asynchrone methodes van Entity Framework Core.

Pas de TicketController aan, zodat elk endpoint asynchroon werkt, en laat ASP.NET Core voortaan je `TicketRepository` aanleveren.

> [!IMPORTANT]
> Async geldt van de database en het bestand tot in de controller. Gebruik nergens `.Result` of `.Wait()`, en nergens `async void`.

> [!NOTE]
> Lijsten blijven op Id gesorteerd, met welke repository ook, behalve waar deze opgave een andere volgorde vraagt. Een database belooft geen volgorde, tenzij je erom vraagt.

---

### 5. Eén rij

Voorzie een GET-endpoint op:

<span style="background:#1c7ed6;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>GET</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/ticket/rij/<span style="color:#e64980"><b>{rij}</b></span></code>

Dit endpoint geeft de geldige tickets van die rij terug, gesorteerd op stoel. Het antwoord is altijd <code style="color:#37b24d;font-weight:600">200 OK</code>, ook als de lijst leeg is.

| Verzoek | Antwoord |
|---------|----------|
| <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> api/ticket/rij/1</code> | de tickets met Id 1 en 2, in die volgorde |
| <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> api/ticket/rij/3</code> | enkel het ticket met Id 3; ticket 4 is geannuleerd |
| <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> api/ticket/rij/9</code> | `[]` |

> [!TIP]
> In je databaserepository laat je het filteren en het sorteren door de database doen. Kijk in het terminalvenster van je API naar de SQL bij dit verzoek: staan beide voorwaarden en het sorteren erin?

---

### 6. Alles samen: twee repositories, één antwoord

<span style="background:#7048e8;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>SAMENGESTELDE OEFENING</b></span>

Je hebt nu twee repositories die hetzelfde asynchrone contract volgen, maar elk op een totaal andere manier werken. In dit laatste punt doorloop je dezelfde reeks met allebei. Elk antwoord moet bij de twee hetzelfde zijn, tot op de Id's.

Begin met de databaserepository en een database die exact de startgegevens bevat. Bedragen vergelijk je als getal: `0`, `0.0` en `0.00` zijn hetzelfde antwoord.

1. <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> api/ticket/opbrengst</code> geeft `175.00`.
2. Verkoop dit ticket. Je krijgt <code style="color:#37b24d;font-weight:600">201</code>, en het nieuwe ticket heeft Id 7 en is niet geannuleerd.

   ```json
   { "naam": "Jonas Wouters", "rij": 3, "stoel": 13, "prijs": 35.00 }
   ```

3. Annuleer ticket 5. Je krijgt <code style="color:#37b24d;font-weight:600">204</code>.
4. De opbrengst is `185.00`, en <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> api/ticket/rij/3</code> geeft de tickets met Id 3 en 7, in die volgorde.
5. Stop je API en start hem opnieuw. De opbrengst is nog altijd `185.00`.
6. Herstel ticket 4. Je krijgt <code style="color:#f08c00;font-weight:600">400</code> met <code style="color:#845ef7">Rij 3 stoel 13 is intussen aan iemand anders verkocht.</code>
7. Herstel ticket 5. Je krijgt <code style="color:#37b24d;font-weight:600">204</code>, en de opbrengst is `210.00`.
8. Verhuis ticket 7 naar rij 7, stoel 3. Je krijgt <code style="color:#f08c00;font-weight:600">400</code> met <code style="color:#845ef7">Rij 7 stoel 3 is al verkocht.</code>
9. Verhuis ticket 7 naar rij 9, stoel 1. Je krijgt <code style="color:#37b24d;font-weight:600">204</code>. De opbrengst is nog altijd `210.00`, en rij 3 bevat enkel nog ticket 3.

Wissel daarna in Program.cs naar je `BestandTicketRepository`, verwijder `tickets.json`, start je API, en doorloop stap 1 tot en met 9 opnieuw. Elk antwoord moet hetzelfde zijn.

> [!WARNING]
> Krijgt het nieuwe ticket in stap 2 bij een van de twee een ander Id dan 7, dan klopt de teller van een van de twee niet. Bij de database is dat de teller van PostgreSQL; bij het bestand bepaal jij het volgende Id.

> [!NOTE]
> Merk op dat de controller in deze hele reeks niet één keer veranderde, ook niet de plaatscontrole. Twee repositories die niets van elkaar weten, een bestand en een database, geven dezelfde antwoorden omdat ze hetzelfde contract volgen.
