# 06_06

<b>Hoofdstuk 06</b> &nbsp;·&nbsp; Concerten en hun tickets, in schoolstijl &nbsp;·&nbsp; Concertzaal &nbsp;·&nbsp; niveau <span style="color:#f03e3e">●●●</span> &nbsp;·&nbsp; ±2,5 uur

## Leerdoel

Na deze oefening kan je een bestaand model onder een nieuw model brengen in een één-op-veel-relatie, en alles wat je al kon, ook per "één"-kant mogelijk maken.

Je leert een datum als `DateTime` in je model en je startgegevens opnemen, de "veel"-kant van een relatie opvragen en optellen per "één"-kant, en een bestaande regel uitbreiden zodat ze per concert geldt.

Daarnaast bewaak je dat een concert met tickets niet verdwijnt, en dat een ticket enkel bij een bestaand concert kan horen.

## 

## Opdracht

De Notenbalk verkocht tot nu toe tickets voor één avond. Vanaf nu speelt de zaal meerdere concerten, en hoort elk ticket bij precies één concert. Een stoel die op het ene concert bezet is, kan op een ander concert gewoon vrij zijn. De datum van een concert wordt een echte datum, geen tekst meer.

Jouw taak is om een model `Concert` toe te voegen, `Ticket` eraan te verbinden, en de plaatscontrole en de opbrengst per concert te laten werken, in de stijl van de schoolopgaven.

Via de API moet een medewerker, net als in 05_06, tickets kunnen verkopen, wijzigen, annuleren, herstellen en verhuizen, en de cijfers zien. Daarnaast moet hij:

1. alle concerten en één concert kunnen opvragen;
2. een concert kunnen toevoegen, en een concert zonder tickets kunnen verwijderen;
3. de tickets en de opbrengst van één concert kunnen opvragen;
4. erop kunnen rekenen dat de plaatscontrole per concert werkt.

Implementeer onderstaande functionaliteiten.

### 1. Je vertrekpunt

Vertrek van je eigen oplossing van 05_06, met de databaserepository. Alle endpoints uit die oefening blijven werken.

> [!NOTE]
> Je bestandsrepository uit 05_06 kent geen concerten. Vanaf nu gebruik je enkel nog de databaserepository. De bestandsversie mag blijven staan, maar je registreert ze niet meer.

> [!NOTE]
> In deze oefening bouw je je database opnieuw op. Verwijder ze, maak de nieuwe migratie uit punt 3, en laat je database daarna volledig opbouwen door al je migraties.

---

### 2. De modellen

Maak in de map Models een klasse `Concert`:

| Property | C#-type         | Kolom in de database |
|----------|-----------------|----------------------|
| Id       | `int`           | de sleutel |
| Titel    | `string`        | tekst van hoogstens 100 tekens, verplicht |
| Datum    | `DateTime`      | datum en uur, verplicht |
| Tickets  | `List<Ticket>?` | navigation property, geen kolom |

Voeg aan `Ticket` toe:

| Property  | C#-type    | Kolom in de database |
|-----------|------------|----------------------|
| ConcertId | `int`      | vreemde sleutel naar `Concerten`, verplicht |
| Concert   | `Concert?` | navigation property, geen kolom |

Leg de relatie vast met de Fluent API, met `HasOne`, `WithMany`, `HasForeignKey`, `OnDelete` en `IsRequired`: een ticket hoort bij één concert, een concert heeft veel tickets, de relatie is verplicht, en een concert met tickets mag niet verwijderd kunnen worden, ook niet rechtstreeks in de database. De tabel heet `Concerten`.

> [!NOTE]
> In de schoolopgaven van hoofdstuk 4 is een datum soms een `string` in de vorm `dd-MM-yyyy`, in die van hoofdstuk 6 een `DateTime`. Volg altijd het type uit de opgave.

---

### 3. De startgegevens

Leg met `HasData` deze concerten vast:

| Id | Titel                | Datum            |
|----|----------------------|------------------|
| 1  | Jazz in de Notenbalk | 2026-11-14 20:00 |
| 2  | Strijkkwartet Aurora | 2026-11-21 20:00 |
| 3  | Nieuwjaarsconcert    | 2027-01-02 15:00 |

Alle uren zijn in UTC. Geef elk ticket in je startgegevens zijn concert:

| Ticket     | ConcertId |
|------------|-----------|
| 1, 2, 3, 4 | 1         |
| 5, 6       | 2         |

Concert 3 heeft nog geen tickets. Maak een migratie met de naam `Concerten`, verwijder je database, en bouw ze opnieuw op met al je migraties.

> [!WARNING]
> Een datum in je startgegevens kan PostgreSQL weigeren, met een melding over *Kind=Unspecified* en *only UTC is supported*. Lees in je leerboek, hoofdstuk 39, bij "Let op bij de lescode", wat je dan meegeeft. Hoofdstuk 3 legt uit waarom.

> [!TIP]
> Les 06 verdeelt `OnModelCreating` in twee methodes: één die de tabellen opbouwt en één die de startgegevens toevoegt. Doe dat ook.

---

### 4. Concerten

Maak een interface `IConcertRepository` en een klasse `ConcertRepository`, asynchroon, met je context in een `readonly` veld. Registreer ze met `AddScoped`.

| Methode | Parameters | Geeft terug |
|---------|------------|-------------|
| `GetAllAsync` | geen | `Task<List<Concert>>` |
| `GetByIdAsync` | `int id` | `Task<Concert?>` |
| `CreateAsync` | `Concert concert` | `Task<Concert>` |
| `DeleteAsync` | `int id` | `Task` |
| `HeeftTicketsAsync` | `int concertId` | `Task<bool>` |
| `GetTicketsAsync` | `int concertId` | `Task<List<Ticket>>` |
| `GetOpbrengstAsync` | `int concertId` | `Task<double>` |

- `GetAllAsync` en `GetTicketsAsync` zijn op Id gesorteerd. `GetTicketsAsync` geeft alle tickets van het concert, ook de geannuleerde, of een lege lijst.
- `CreateAsync` berekent de Id zelf: de hoogste Id plus één, of 1 in een lege tabel.
- `HeeftTicketsAsync` kijkt naar alle tickets, ook de geannuleerde.
- `GetOpbrengstAsync` telt de prijzen van de geldige tickets van het concert op, met `SumAsync`, of geeft `0`.

Maak een `ConcertController` die luistert op <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">concerten</code>:

| Endpoint | Antwoord |
|----------|----------|
| <span style="background:#1c7ed6;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>GET</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">concerten</code> | <code style="color:#37b24d;font-weight:600">200 OK</code> met alle concerten, op Id gesorteerd |
| <span style="background:#1c7ed6;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>GET</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">concerten/<span style="color:#e64980"><b>{id}</b></span></code> | <code style="color:#37b24d;font-weight:600">200 OK</code> met het concert, of <code style="color:#f08c00;font-weight:600">404 Not Found</code> zonder body |
| <span style="background:#2f9e44;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>POST</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">concerten</code> | <code style="color:#37b24d;font-weight:600">201 Created</code> met het nieuwe concert en zijn adres, of <code style="color:#f08c00;font-weight:600">400 Bad Request</code> met de reden |
| <span style="background:#e03131;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>DELETE</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">concerten/<span style="color:#e64980"><b>{id}</b></span></code> | <code style="color:#37b24d;font-weight:600">204 No Content</code>, <code style="color:#f08c00;font-weight:600">404 Not Found</code> zonder body, of <code style="color:#f08c00;font-weight:600">400 Bad Request</code> met <code style="color:#845ef7">Dit concert heeft nog tickets.</code> |
| <span style="background:#1c7ed6;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>GET</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">concerten/<span style="color:#e64980"><b>{id}</b></span>/tickets</code> | <code style="color:#37b24d;font-weight:600">200 OK</code> met de tickets van dat concert, op Id gesorteerd |
| <span style="background:#1c7ed6;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>GET</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">concerten/<span style="color:#e64980"><b>{id}</b></span>/opbrengst</code> | <code style="color:#37b24d;font-weight:600">200 OK</code> met een object met een property `opbrengst`, een `double` |

Een concert zonder tickets, of een concert dat niet bestaat, geeft bij de laatste twee endpoints `[]` en `{ "opbrengst": 0 }`, telkens met <code style="color:#37b24d;font-weight:600">200 OK</code>, zoals de schoolopgaven het vragen.

De redenen voor een <code style="color:#f08c00;font-weight:600">400</code> bij het toevoegen:

- de titel is leeg: <code style="color:#845ef7">Een concert moet een titel hebben.</code>
- er is geen datum meegestuurd: <code style="color:#845ef7">Een concert moet een datum hebben.</code>

Log elk verzoek op niveau `Information`, elke <code style="color:#f08c00;font-weight:600">400</code> en <code style="color:#f08c00;font-weight:600">404</code> op niveau `Warning`, en een DELETE die niets vindt op niveau `Error`.

> [!TIP]
> Stuurt de client geen datum mee, dan krijgt `Datum` de standaardwaarde van een `DateTime`. Zoek op welke waarde dat is, en controleer daarop.

<code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> concerten/1</code> geeft als resultaat:

```json
{
  "id": 1,
  "titel": "Jazz in de Notenbalk",
  "datum": "2026-11-14T20:00:00Z",
  "tickets": null
}
```

---

### 5. Tickets bij een concert

Een nieuw ticket moet bij een bestaand concert horen. Voeg aan de controles uit 05_06 deze toe, bij verkopen:

- het concert bestaat niet: <code style="color:#f08c00;font-weight:600">400 Bad Request</code> met <code style="color:#845ef7">We vonden geen concert met Id <span style="color:#e64980"><b>{concertId}</b></span>.</code>

Wijzigen verandert nooit bij welk concert een ticket hoort, wat de client ook meestuurt.

De plaatscontrole geldt voortaan per concert: een plaats is enkel bezet als er op hetzelfde concert een geldig ticket op zit. Pas daarvoor in `ITicketRepository` de methode `IsPlaatsBezetAsync` aan, zodat ze ook een `int concertId` krijgt, als eerste parameter.

<code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> tickets/3</code> geeft nu als resultaat:

```json
{
  "id": 3,
  "naam": "Lisa Maes",
  "rij": 3,
  "stoel": 12,
  "prijs": 35,
  "isGeannuleerd": false,
  "concertId": 1,
  "concert": null
}
```

> [!WARNING]
> Krijg je een <code style="color:#f03e3e;font-weight:600">500</code> met een melding over een *object cycle*? Dan kent een ticket zijn concert, en kent dat concert zijn tickets weer, zonder einde. Kijk welke objecten je in één verzoek ophaalt, en of je ze allemaal nodig hebt.

---

### 6. Alles samen: een plaats per concert

<span style="background:#7048e8;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>SAMENGESTELDE OEFENING</b></span>

De relatie, de startgegevens, de plaatscontrole en de opbrengst komen samen. Elke regel uit 05_06 blijft gelden, maar nu per concert.

<span style="background:#1c7ed6;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>GET</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">tickets/rij/<span style="color:#e64980"><b>{rij}</b></span></code> en <span style="background:#1c7ed6;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>GET</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">tickets/opbrengst</code> blijven over alle concerten samen werken. Zitten er in een rij twee geldige tickets op dezelfde stoel, van verschillende concerten, dan komt het kleinste Id eerst.

#### Testscenario

Bouw je database opnieuw op, zodat ze exact de startgegevens bevat, en doorloop deze reeks. Bedragen vergelijk je als getal.

1. De opbrengst van concert 1 is `125`, die van concert 2 `50`, die van concert 3 `0`. <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> tickets/opbrengst</code> geeft `{ "opbrengst": 175 }`.
2. Verkoop dit ticket:

   ```json
   { "naam": "Jonas Wouters", "rij": 1, "stoel": 5, "prijs": 30, "concertId": 2 }
   ```

   Je krijgt <code style="color:#37b24d;font-weight:600">201</code>, en het nieuwe ticket heeft Id 7. Rij 1 stoel 5 is bezet op concert 1, maar was vrij op concert 2.
3. Verkoop hetzelfde ticket aan `Lies Martens`. Je krijgt <code style="color:#f08c00;font-weight:600">400</code> met <code style="color:#845ef7">Rij 1 stoel 5 is al verkocht.</code>
4. Verkoop een geldig ticket voor concert 99. Je krijgt <code style="color:#f08c00;font-weight:600">400</code> met <code style="color:#845ef7">We vonden geen concert met Id 99.</code>
5. Verhuis ticket 5 naar rij 3, stoel 12. Je krijgt <code style="color:#37b24d;font-weight:600">204</code>: die plaats is enkel op concert 1 bezet.
6. Herstel ticket 4. Je krijgt <code style="color:#37b24d;font-weight:600">204</code>. De opbrengst van concert 1 is nu `160`.
7. De opbrengst van concert 2 is `80`, en die over alles samen `240`. <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> tickets/rij/3</code> geeft de tickets 3, 5 en 4, in die volgorde.
8. Verwijder concert 2. Je krijgt <code style="color:#f08c00;font-weight:600">400</code> met <code style="color:#845ef7">Dit concert heeft nog tickets.</code> Verwijder concert 3. Je krijgt <code style="color:#37b24d;font-weight:600">204</code>.
9. Voeg dit concert toe:

   ```json
   { "titel": "Lentefeest", "datum": "2027-03-21T20:00:00Z" }
   ```

   Je krijgt <code style="color:#37b24d;font-weight:600">201</code>, en het heeft Id 3: de hoogste Id is 2.
10. Voeg een concert toe met enkel een titel. Je krijgt <code style="color:#f08c00;font-weight:600">400</code> met <code style="color:#845ef7">Een concert moet een datum hebben.</code>
11. Annuleer de tickets 5, 6 en 7. Je krijgt telkens <code style="color:#37b24d;font-weight:600">204</code>. Verwijder concert 2. Je krijgt <code style="color:#f08c00;font-weight:600">400</code>: zijn tickets zijn geannuleerd, maar ze bestaan nog.
12. <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> concerten/2/tickets</code> geeft de tickets 5, 6 en 7, en <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> concerten/2/opbrengst</code> geeft `{ "opbrengst": 0 }`. <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> concerten/99/tickets</code> geeft `[]`.
13. Verwijder concert 99. Je krijgt <code style="color:#f08c00;font-weight:600">404</code>, en in je terminal staat een `Error`.
