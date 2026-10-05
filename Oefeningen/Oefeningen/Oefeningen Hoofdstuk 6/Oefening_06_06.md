# 06_06

<b>Hoofdstuk 06</b> &nbsp;·&nbsp; Concerten en hun tickets &nbsp;·&nbsp; Concertzaal &nbsp;·&nbsp; niveau <span style="color:#f03e3e">●●●</span> &nbsp;·&nbsp; ±2,5 uur

## Leerdoel

Na deze oefening kan je een bestaand model onderbrengen onder een nieuw model, via een één-op-veel-relatie, en alle bestaande regels laten meegroeien.

Je leert je startgegevens voor twee verbonden tabellen via de Fluent API vastleggen, in een eigen methode, zoals les 06 dat doet.

Daarnaast merk je dat een controle die naar de bestaande gegevens kijkt, zoals een bezette plaats, nu binnen de juiste groep moet kijken: dezelfde stoel kan op een ander concert gewoon vrij zijn.

## 

## Opdracht

De Notenbalk verkocht tot nu toe tickets voor één avond. Maar de zaal speelt meerdere concerten, en dezelfde stoel kan voor elk concert opnieuw verkocht worden. Vanaf nu hoort elk ticket bij precies één concert, en een concert heeft meerdere tickets.

Jouw taak is om een model `Concert` toe te voegen, `Ticket` eraan te verbinden, en de plaatscontrole en de opbrengst per concert te laten werken.

Via de API moet een medewerker, net als in 05_06, tickets kunnen beheren, verhuizen en de rij opvragen. Daarnaast moet hij:

1. alle concerten en één concert kunnen opvragen;
2. een concert kunnen toevoegen, en een concert zonder tickets kunnen verwijderen;
3. de opbrengst van één concert kunnen opvragen;
4. erop kunnen rekenen dat dezelfde stoel op twee verschillende concerten aan twee mensen verkocht kan worden.

Implementeer onderstaande functionaliteiten.

### 1. Je vertrekpunt

Vertrek van je eigen oplossing van 05_06. Alle endpoints uit die oefening blijven werken, met dezelfde controles en dezelfde berichten. Punt 5 en 6 passen er een paar regels aan.

Je `BestandTicketRepository` heeft zijn werk gedaan: een JSON-bestand kent geen relaties. Haal hem weg, samen met `tickets.json`.

> [!NOTE]
> In deze oefening bouw je je database opnieuw op. Verwijder ze, en laat ze daarna volledig opbouwen door je migraties. Vanaf nu komen alle startgegevens via de Fluent API, zoals in les 06.

---

### 2. De modellen

Maak in de map Models een klasse `Concert`:

| Property | C#-type        | Kolom in de database                      |
|----------|----------------|-------------------------------------------|
| Id       | `int`          | de sleutel, de database telt zelf op      |
| Titel    | `string`       | tekst van hoogstens 100 tekens, verplicht |
| Datum    | `string`       | tekst van hoogstens 10 tekens, in de vorm `dd-MM-yyyy`, verplicht |
| Tickets  | `List<Ticket>?` | navigation property, geen kolom           |

Voeg aan `Ticket` toe:

| Property  | C#-type    | Kolom in de database                       |
|-----------|------------|--------------------------------------------|
| ConcertId | `int`      | vreemde sleutel naar `Concerten`, verplicht |
| Concert   | `Concert?` | navigation property, geen kolom            |

De tabel heet `Concerten`. Voeg ze toe aan je `ConcertzaalContext`, en leg de relatie vast met de Fluent API.

Een concert waarnaar nog een ticket verwijst, mag niet verwijderd kunnen worden. Ook niet rechtstreeks in de database, en ook niet als al zijn tickets geannuleerd zijn: een geannuleerd ticket bestaat nog.

> [!NOTE]
> Let op het vraagteken bij de navigation properties: de client stuurt ze nooit mee. Scalar vult ze in zijn voorbeeld wel in. Haal ze daar weg voor je een verzoek verstuurt.

> [!NOTE]
> De maximale lengtes leg je in deze oefening enkel in je database vast, en de vorm van de datum controleer je niet. De testen sturen enkel teksten die passen.

---

### 3. De startgegevens

> [!IMPORTANT]
> Verdeel `OnModelCreating` in twee methodes, zoals les 06 dat doet: één die de tabellen en relaties opbouwt, en één die de startgegevens toevoegt.

Deze concerten:

| Id | Titel                | Datum      |
|----|----------------------|------------|
| 1  | Jazz in de Notenbalk | 14-11-2026 |
| 2  | Strijkkwartet Aurora | 21-11-2026 |
| 3  | Nieuwjaarsconcert    | 02-01-2027 |

De zes tickets uit 05_06 komen er ook in, met dit concert:

| Ticket     | ConcertId |
|------------|-----------|
| 1, 2, 3, 4 | 1         |
| 5, 6       | 2         |

Concert 3 heeft nog geen tickets.

Maak een migratie met de naam `Concerten` en bouw daarmee je database opnieuw op.

> [!WARNING]
> Startgegevens met een vaste Id vertellen de teller van PostgreSQL niet welke Id's al gebruikt zijn. Na het opbouwen van je database moet het eerste concert dat je via de API toevoegt, Id 4 krijgen, en het eerste ticket Id 7. Je leerboek waarschuwt hiervoor bij de startgegevens. Kies een oplossing waarbij je startgegevens precies de Id's uit de tabel houden.

---

### 4. Concerten

Maak de repository en de controller die je nodig hebt, asynchroon zoals in les 05.

| Verzoek | Antwoord |
|---------|----------|
| <span style="background:#1c7ed6;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>GET</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/concert</code> | <code style="color:#37b24d;font-weight:600">200 OK</code> met alle concerten, op Id gesorteerd |
| <span style="background:#1c7ed6;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>GET</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/concert/<span style="color:#e64980"><b>{id}</b></span></code> | <code style="color:#37b24d;font-weight:600">200 OK</code> met het concert, of <code style="color:#f08c00;font-weight:600">404 Not Found</code> met <code style="color:#845ef7">We vonden geen concert met Id <span style="color:#e64980"><b>{id}</b></span>.</code> |
| <span style="background:#2f9e44;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>POST</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/concert</code> | <code style="color:#37b24d;font-weight:600">201 Created</code> met het nieuwe concert en zijn adres, of <code style="color:#f08c00;font-weight:600">400 Bad Request</code> met de reden |
| <span style="background:#e03131;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>DELETE</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/concert/<span style="color:#e64980"><b>{id}</b></span></code> | <code style="color:#37b24d;font-weight:600">204 No Content</code>, <code style="color:#f08c00;font-weight:600">404 Not Found</code> met hetzelfde bericht als hierboven, of <code style="color:#f08c00;font-weight:600">400 Bad Request</code> met <code style="color:#845ef7">Dit concert heeft nog tickets.</code> |
| <span style="background:#1c7ed6;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>GET</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/concert/<span style="color:#e64980"><b>{id}</b></span>/opbrengst</code> | <code style="color:#37b24d;font-weight:600">200 OK</code> met de som van de prijzen van de geldige tickets van dat concert, of <code style="color:#f08c00;font-weight:600">404 Not Found</code> met hetzelfde bericht als hierboven |

De redenen voor een <code style="color:#f08c00;font-weight:600">400</code> bij het toevoegen:

- de titel is leeg: <code style="color:#845ef7">Een concert moet een titel hebben.</code>
- de datum is leeg: <code style="color:#845ef7">Een concert moet een datum hebben.</code>

> [!WARNING]
> Krijg je een <code style="color:#f03e3e;font-weight:600">500</code> met een melding over een *object cycle*? Dan kennen twee objecten elkaar over en weer, zonder einde. Les 08 legt uit waarom. Voor nu: haal een object niet op als je enkel wil weten óf het bestaat.

---

### 5. Tickets bij een concert

Een nieuw ticket hoort voortaan altijd bij een concert. De client stuurt daarom ook een `ConcertId` mee. Na de controles op het ticket zelf komt er één bij:

- het concert bestaat niet: <code style="color:#f08c00;font-weight:600">400 Bad Request</code> met <code style="color:#845ef7">We vonden geen concert met Id <span style="color:#e64980"><b>{concertId}</b></span>.</code>

Wijzigen verandert nooit bij welk concert een ticket hoort, wat de client ook meestuurt.

> [!TIP]
> De waarschuwing over een *object cycle* uit punt 4 geldt ook hier. Ook een toevoeging kan die opleveren, als je API daarvoor andere objecten heeft opgehaald.

<code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> api/ticket/3</code> geeft nu als resultaat:

```json
{
  "id": 3,
  "naam": "Lisa Maes",
  "rij": 3,
  "stoel": 12,
  "prijs": 35.00,
  "isGeannuleerd": false,
  "concertId": 1,
  "concert": null
}
```

---

### 6. Alles samen: een plaats per concert

<span style="background:#7048e8;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>SAMENGESTELDE OEFENING</b></span>

Elke regel over plaatsen keek tot nu toe naar de hele zaal: is deze stoel nog vrij? Vanaf nu is die vraag zinloos zonder te zeggen voor welk concert. In dit laatste punt pas je alle plaatscontroles aan, bij verkopen, wijzigen, herstellen en verhuizen.

> [!IMPORTANT]
> Een plaats is bezet als een geldig ticket **van hetzelfde concert** erop zit. Een ticket van een ander concert telt niet mee. De berichten blijven dezelfde.

<span style="background:#1c7ed6;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>GET</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/ticket/rij/<span style="color:#e64980"><b>{rij}</b></span></code> en <span style="background:#1c7ed6;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>GET</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/ticket/opbrengst</code> blijven over alle concerten samen werken. Zitten er in een rij twee geldige tickets op dezelfde stoel, van verschillende concerten, dan komt het kleinste Id eerst.

#### Testscenario

Bouw je database opnieuw op, zodat ze exact de startgegevens bevat, en doorloop deze reeks. Bedragen vergelijk je als getal: `0`, `0.0` en `0.00` zijn hetzelfde antwoord.

1. De opbrengst van concert 1 is `125.00`, die van concert 2 `50.00`, die van concert 3 `0`. De opbrengst over alles samen is `175.00`.
2. Verkoop dit ticket. Je krijgt <code style="color:#37b24d;font-weight:600">201</code>, en het nieuwe ticket heeft Id 7. Rij 1 stoel 5 is bezet op concert 1, maar vrij op concert 2.

   ```json
   { "naam": "Jonas Wouters", "rij": 1, "stoel": 5, "prijs": 30.00, "concertId": 2 }
   ```

3. Verkoop hetzelfde ticket aan `Lies Martens`. Je krijgt <code style="color:#f08c00;font-weight:600">400</code> met <code style="color:#845ef7">Rij 1 stoel 5 is al verkocht.</code>
4. Verkoop een geldig ticket voor concert 99. Je krijgt <code style="color:#f08c00;font-weight:600">400</code> met <code style="color:#845ef7">We vonden geen concert met Id 99.</code>
5. Verhuis ticket 5 naar rij 3, stoel 12. Je krijgt <code style="color:#37b24d;font-weight:600">204</code>: die plaats is enkel op concert 1 bezet.
6. Herstel ticket 4. Je krijgt <code style="color:#37b24d;font-weight:600">204</code>. De opbrengst van concert 1 is nu `160.00`.
7. De opbrengst van concert 2 is `80.00`, en die over alles samen `240.00`.
8. Verwijder concert 2. Je krijgt <code style="color:#f08c00;font-weight:600">400</code> met <code style="color:#845ef7">Dit concert heeft nog tickets.</code> Verwijder concert 3. Je krijgt <code style="color:#37b24d;font-weight:600">204</code>.
9. Voeg een concert toe met de titel `Lentefeest` en als datum `21-03-2027`. Je krijgt <code style="color:#37b24d;font-weight:600">201</code>, en het heeft Id 4.
10. Annuleer de tickets 5, 6 en 7. Je krijgt telkens <code style="color:#37b24d;font-weight:600">204</code>. Verwijder concert 2. Je krijgt <code style="color:#f08c00;font-weight:600">400</code>: zijn tickets zijn geannuleerd, maar ze bestaan nog.
