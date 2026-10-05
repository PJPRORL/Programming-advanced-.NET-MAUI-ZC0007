# 07_06

<b>Hoofdstuk 07</b> &nbsp;·&nbsp; Alles of niets, met één Unit of Work &nbsp;·&nbsp; Concertzaal &nbsp;·&nbsp; niveau <span style="color:#f03e3e">●●●</span> &nbsp;·&nbsp; ±2,5 uur

## Leerdoel

Na deze oefening kan je je repositories ombouwen naar een generieke repository met specifieke repositories erbovenop, achter één Unit of Work, zonder dat een client iets van die ombouw merkt.

Je leert een groep tickets in één verzoek ontvangen, alles controleren voor je iets bewaart, en dan alles in één keer bewaren.

Daarnaast merk je dat de generieke repository niet weet welke regels er voor jouw modellen gelden: welke tickets geldig zijn, en welke gegevens een client niet mag veranderen.

## 

## Opdracht

Een school belt voor twintig plaatsen naast elkaar. Vandaag verkoopt de medewerker die tickets één voor één. Is er onderweg een plaats al weg, dan is de groep gesplitst: een deel heeft een ticket, de rest niet. De Notenbalk wil voortaan een groep in één keer verkopen: alle plaatsen zijn vrij, of er wordt niets verkocht.

Jouw taak is om je repositories om te bouwen naar een generieke repository met specifieke repositories erbovenop, ze achter een Unit of Work te zetten, en een endpoint te maken dat een groep tickets in één keer verkoopt.

Via de API moet een medewerker, net als in 06_06, tickets en concerten kunnen beheren, tickets verhuizen en herstellen, en de opbrengst opvragen, per concert en over alles samen. Daarnaast moet hij:

1. een groep tickets in één verzoek kunnen verkopen.

Implementeer onderstaande functionaliteiten.

### 1. Je vertrekpunt

Vertrek van je eigen oplossing van 06_06. Alle endpoints uit die oefening blijven werken, met dezelfde controles, dezelfde berichten en dezelfde JSON.

Je modellen en je database veranderen in deze oefening niet. De startgegevens blijven die van 06_06.

---

### 2. De generieke repository en de Unit of Work

Maak in de map Repositories een generieke interface `IGenericRepository<TEntity>` en een klasse `GenericRepository<TEntity>` die ze implementeert, met de vijf bewerkingen uit les 07: alles ophalen, één ophalen op Id, toevoegen, bijwerken en verwijderen.

Beslis per model of de generieke repository volstaat, of dat het een specifieke repository nodig heeft die overerft van de generieke. Maak daarna een interface `IUnitOfWork` en een klasse `UnitOfWork`, zoals in les 07.

- Geen enkele repository roept nog zelf `SaveChangesAsync` aan.
- Program.cs registreert enkel nog je `ConcertzaalContext` en je Unit of Work.
- Elke controller krijgt de Unit of Work binnen, en verder enkel zijn logger.

> [!WARNING]
> De generieke repository kent je regels niet. Ze geeft geannuleerde tickets even graag terug als geldige, en ze belooft geen volgorde. Kijk voor elk endpoint na of het nog altijd enkel teruggeeft wat het in 06_06 teruggaf, in dezelfde volgorde.

> [!WARNING]
> De waarschuwing over een *object cycle* uit 06_06 blijft gelden. De generieke repository haalt altijd een volledig object op, ook als je enkel wil weten óf het bestaat.

---

### 3. Wijzigen met de generieke repository

<span style="background:#d9822b;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>PUT</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/ticket/<span style="color:#e64980"><b>{id}</b></span></code> werkt zoals vroeger, met dezelfde controles en dezelfde berichten. Het Id staat in de URL. Stuurt de client ook een Id in de body, dan telt enkel dat uit de URL. Daarnaast gelden twee regels, wat de client ook meestuurt:

- wijzigen verandert nooit of een ticket geannuleerd is;
- wijzigen verandert nooit bij welk concert een ticket hoort.

> [!WARNING]
> Krijg je een <code style="color:#f03e3e;font-weight:600">500</code>, met in je terminal een melding dat er al een object met hetzelfde Id gevolgd wordt (*already being tracked*), of een melding over een vreemde sleutel? Kijk dan welke objecten je API aan Entity Framework Core geeft, en welke van hun gegevens het als gewijzigd beschouwt.

---

### 4. Alles samen: een groep in één keer verkopen

<span style="background:#7048e8;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>SAMENGESTELDE OEFENING</b></span>

<span style="background:#2f9e44;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>POST</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/ticket/groep</code>

De client stuurt een lijst van tickets, elk met zijn eigen concert.

```json
[
  { "naam": "Jonas Wouters", "rij": 1, "stoel": 5, "prijs": 30.00, "concertId": 3 },
  { "naam": "Lies Martens", "rij": 1, "stoel": 6, "prijs": 30.00, "concertId": 3 },
  { "naam": "Ward Peeters", "rij": 3, "stoel": 13, "prijs": 30.00, "concertId": 1 }
]
```

Controleer in deze volgorde, en geef de eerste fout terug die je tegenkomt:

1. de lijst is leeg: <code style="color:#f08c00;font-weight:600">400 Bad Request</code> met <code style="color:#845ef7">Een groep moet minstens één ticket bevatten.</code>
2. de tickets, één voor één, in de volgorde van het verzoek, met de controles uit 06_06 voor een nieuw ticket;
3. twee tickets uit de groep op dezelfde plaats van hetzelfde concert: <code style="color:#f08c00;font-weight:600">400 Bad Request</code> met <code style="color:#845ef7">Rij <span style="color:#e64980"><b>{rij}</b></span> stoel <span style="color:#e64980"><b>{stoel}</b></span> komt twee keer voor in deze groep.</code> Komen er meerdere plaatsen dubbel voor, dan noem je de eerste plaats die een tweede keer voorkomt, in de volgorde van het verzoek.

Faalt één controle, dan geef je <code style="color:#f08c00;font-weight:600">400 Bad Request</code> terug, en wordt er niets verkocht: geen enkel ticket uit de groep komt in de database.

Slaagt alles, dan bewaar je alle tickets met één `SaveChangesAsync`. Geen enkel nieuw ticket is geannuleerd, wat de client ook meestuurt. Je geeft <code style="color:#37b24d;font-weight:600">200 OK</code> terug, met de nieuwe tickets, op Id gesorteerd, elk zoals <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> api/ticket/{id}</code> het zou tonen. Een groep heeft geen eigen adres, en daarom geen <code style="color:#37b24d;font-weight:600">201</code>.

> [!TIP]
> De controles op één ticket bestaan al, voor <span style="background:#2f9e44;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>POST</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/ticket</code>. Gebruik ze opnieuw, in plaats van ze te kopiëren.

> [!WARNING]
> Krijg je een <code style="color:#f03e3e;font-weight:600">500</code> over een *object cycle*, terwijl de tickets wel bewaard zijn? Dan kent een nieuw ticket zijn concert, en kent dat concert zijn tickets weer. Denk na over hoe je API controleert of een concert bestaat.

#### Testscenario

Bouw je database opnieuw op, zodat ze exact de startgegevens bevat, en doorloop deze reeks. Bedragen vergelijk je als getal: `0`, `0.0` en `0.00` zijn hetzelfde antwoord.

1. Wijzig ticket 3. Je krijgt <code style="color:#37b24d;font-weight:600">204</code>. <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> api/ticket/3</code> toont de nieuwe naam, is niet geannuleerd en hoort bij concert 1.

   ```json
   { "naam": "Lisa Maes-Peeters", "rij": 3, "stoel": 12, "prijs": 35.00 }
   ```

2. Wijzig ticket 5 met dezelfde gegevens als in de startgegevens, maar met `concertId` `3` en `isGeannuleerd` `true`. Je krijgt <code style="color:#37b24d;font-weight:600">204</code>. Ticket 5 hoort nog altijd bij concert 2 en is nog altijd geldig.
3. <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> api/ticket</code> geeft de tickets 1, 2, 3, 5 en 6, in die volgorde. <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> api/ticket/4</code> geeft nog altijd <code style="color:#f08c00;font-weight:600">404</code> met <code style="color:#845ef7">We vonden geen geldig ticket met Id 4.</code>
4. Verkoop de groep hierboven, maar met `Lies Martens` op rij 1 stoel 5. Je krijgt <code style="color:#f08c00;font-weight:600">400</code> met <code style="color:#845ef7">Rij 1 stoel 5 komt twee keer voor in deze groep.</code> De opbrengst van concert 3 is nog altijd `0`.
5. Verkoop de groep hierboven, maar met `concertId` `1` voor `Lies Martens`. Je krijgt <code style="color:#f08c00;font-weight:600">400</code> met <code style="color:#845ef7">Rij 1 stoel 6 is al verkocht.</code> Ook `Jonas Wouters` is niet verkocht: de opbrengst van concert 3 is nog altijd `0`, en <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> api/ticket/alles</code> geeft nog altijd zes tickets.
6. Verkoop een lege groep, `[]`. Je krijgt <code style="color:#f08c00;font-weight:600">400</code> met <code style="color:#845ef7">Een groep moet minstens één ticket bevatten.</code> Verkoop de groep hierboven met `concertId` `99` voor `Ward Peeters`. Je krijgt <code style="color:#f08c00;font-weight:600">400</code> met <code style="color:#845ef7">We vonden geen concert met Id 99.</code>
7. Verkoop de groep hierboven, met `"isGeannuleerd": true` bij `Ward Peeters`. Je krijgt <code style="color:#37b24d;font-weight:600">200</code> met drie tickets, met de Id's 7, 8 en 9, en geen van de drie is geannuleerd. Ward zit op de plaats van ticket 4: dat is geannuleerd, en telt dus niet.
8. De opbrengst van concert 3 is `60.00`, die van concert 1 `155.00`, en die over alles samen `265.00`.
9. Herstel ticket 4. Je krijgt <code style="color:#f08c00;font-weight:600">400</code> met <code style="color:#845ef7">Rij 3 stoel 13 is intussen aan iemand anders verkocht.</code>
