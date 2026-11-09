# 08_06

<b>Hoofdstuk 08</b> &nbsp;·&nbsp; Include over een hele lijst &nbsp;·&nbsp; Concertzaal &nbsp;·&nbsp; niveau <span style="color:#f03e3e">●●●</span> &nbsp;·&nbsp; ±2,5 uur

## Leerdoel

Na deze oefening kan je met `Include` gerelateerde gegevens mee ophalen voor één object én voor een gefilterde lijst, telkens in één query.

Je leert die methodes in een specifieke repository zetten, ze via je Unit of Work bereiken, en de JSON lezen die `IgnoreCycles` maakt van een hele lijst.

Daarnaast merk je dat `Include` niet filtert: wat je mee ophaalt, is alles wat bij het object hoort, ook wat je API op andere plaatsen verbergt.

## 

## Opdracht

De Notenbalk wil een concert kunnen bekijken zoals op het zaalplan: de titel, de datum en alle tickets die ervoor verkocht zijn. En wie het seizoen plant, wil alle concerten van één jaar zien, met hun tickets.

Jouw taak is om endpoints te maken die een ticket, een concert of alle concerten van een jaar teruggeven, samen met de gegevens die erbij horen, telkens met één query naar de database.

Via de API moet een medewerker, net als in 07_06, tickets en concerten kunnen beheren, tickets kunnen verhuizen en herstellen, groepen kunnen verkopen en de opbrengst kunnen opvragen. Daarnaast moet hij:

1. één geldig ticket kunnen opvragen, samen met zijn concert;
2. één concert kunnen opvragen, samen met al zijn tickets;
3. alle concerten van één jaar kunnen opvragen, elk met al zijn tickets.

Implementeer onderstaande functionaliteiten.

### 1. Je vertrekpunt

Vertrek van je eigen oplossing van 07_06. Alle endpoints uit die oefening blijven werken, met dezelfde controles, dezelfde berichten en dezelfde JSON. Daar blijven de navigation properties `null`: enkel de nieuwe endpoints vullen ze.

Je modellen en je database veranderen in deze oefening niet. De startgegevens blijven die van 06_06.

---

### 2. Het ERD en de cycli

Maak in de map van je project een bestand `ERD.md`. Beschrijf daarin de relatie van je database met haar cardinaliteiten, in dezelfde vorm als het ERD in les 08: één regel per relatie, met een korte uitleg.

Stel daarna in Program.cs de JSON-serializer zo in dat hij circulaire referenties negeert, zoals les 08 dat doet.

> [!WARNING]
> Les 08 noemt dit zelf een tijdelijke oplossing: op het examen volstaat ze niet. In hoofdstuk 09 haal je deze instelling weer weg.

Het ophalen met `Include` gebeurt in specifieke repositories, niet in de generieke. Laat je Unit of Work telkens het specifieke type teruggeven.

---

### 3. Een ticket met zijn concert

<span style="background:#1c7ed6;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>GET</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">tickets/<span style="color:#e64980"><b>{id}</b></span>/details</code>

- Er is geen geldig ticket met dat Id: <code style="color:#f08c00;font-weight:600">404 Not Found</code> zonder body.
- Anders: <code style="color:#37b24d;font-weight:600">200 OK</code> met het ticket, en daarin zijn concert.

<code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> tickets/3/details</code> geeft als resultaat:

```json
{
  "id": 3,
  "naam": "Lisa Maes",
  "rij": 3,
  "stoel": 12,
  "prijs": 35,
  "isGeannuleerd": false,
  "concertId": 1,
  "concert": {
    "id": 1,
    "titel": "Jazz in de Notenbalk",
    "datum": "2026-11-14T20:00:00Z",
    "tickets": [null]
  }
}
```

> [!NOTE]
> Die `[null]` is geen fout. Entity Framework Core vult de lijst van het concert zelf aan met wat het in dit verzoek al opgehaald heeft. Hier is dat enkel het ticket, en dat ticket is het begin van je JSON. `IgnoreCycles` schrijft het daarom niet opnieuw uit, maar zet er `null`. In les 09 los je dat netter op.

---

### 4. Een concert met al zijn tickets

<span style="background:#1c7ed6;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>GET</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">concerten/<span style="color:#e64980"><b>{id}</b></span>/details</code>

- Het concert bestaat niet: <code style="color:#f08c00;font-weight:600">404 Not Found</code> zonder body.
- Anders: <code style="color:#37b24d;font-weight:600">200 OK</code> met het concert, en daarin al zijn tickets: de geldige en de geannuleerde. Een concert zonder tickets heeft een lege lijst.

<code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> concerten/2/details</code> geeft als resultaat:

```json
{
  "id": 2,
  "titel": "Strijkkwartet Aurora",
  "datum": "2026-11-21T20:00:00Z",
  "tickets": [
    { "id": 5, "naam": "Emma Claes", "rij": 7, "stoel": 2, "prijs": 25, "isGeannuleerd": false, "concertId": 2, "concert": null },
    { "id": 6, "naam": "Noah Willems", "rij": 7, "stoel": 3, "prijs": 25, "isGeannuleerd": false, "concertId": 2, "concert": null }
  ]
}
```

De volgorde van de tickets binnen een concert ligt niet vast.

---

### 5. De groepsverkoop verandert niet

De groepsverkoop uit 07_06 blijft precies hetzelfde antwoorden, ook nu je API cycli afbreekt: elk nieuw ticket komt terug met `"concert": null`.

> [!TIP]
> Met `IgnoreCycles` krijg je geen <code style="color:#f03e3e;font-weight:600">500</code> meer als je API te veel ophaalt. Krijgt een nieuw ticket in je antwoord plots een concert mee, dan haalt je API ergens meer op dan het nodig heeft.

---

### 6. Alles samen: alle concerten van één jaar

<span style="background:#7048e8;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>SAMENGESTELDE OEFENING</b></span>

<span style="background:#1c7ed6;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>GET</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">concerten/jaar/<span style="color:#e64980"><b>{jaar}</b></span></code>

- Er is geen enkel concert in dat jaar: <code style="color:#37b24d;font-weight:600">200 OK</code> met een lege lijst `[]`, zoals de schoolopgaven het vragen.
- Anders: <code style="color:#37b24d;font-weight:600">200 OK</code> met alle concerten van dat jaar, op Id gesorteerd, elk met al zijn tickets, zoals in punt 4.

`{jaar}` is een geheel getal. Het jaar haal je uit de datum van het concert. Je API laat de database filteren: hij haalt enkel de concerten van dat jaar op.

> [!IMPORTANT]
> Je API stuurt voor dit verzoek precies één query naar de database, net als bij punt 3 en 4. Kijk in je terminal: Entity Framework Core toont elke query die het uitvoert. Zie je er meer, lees dan nog eens wat les 07 over het N+1-probleem zegt.

#### Testscenario

Bouw je database opnieuw op, zodat ze exact de startgegevens bevat, en doorloop deze reeks:

1. <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> tickets/3/details</code> en <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> concerten/2/details</code> geven de JSON uit punt 3 en 4.
2. <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> tickets/4/details</code> geeft <code style="color:#f08c00;font-weight:600">404</code>. <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> concerten/1/details</code> geeft wél alle vier de tickets van concert 1, ook ticket 4 met `"isGeannuleerd": true`.
3. <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> concerten/3/details</code> geeft `"tickets": []`. <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> concerten/99/details</code> geeft <code style="color:#f08c00;font-weight:600">404</code>.
4. <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> concerten/1</code> geeft nog altijd `"tickets": null`, net als in 07_06.
5. Verkoop de groep uit 07_06. Je krijgt <code style="color:#37b24d;font-weight:600">200</code> met drie tickets, met de Id's 7, 8 en 9, elk met `"concert": null`, net als in 07_06.
6. <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> concerten/3/details</code> geeft de tickets van `Jonas Wouters` en `Lies Martens`. <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> concerten/1/details</code> geeft vijf tickets: de tickets 1 tot en met 4, en dat van `Ward Peeters`.
7. <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> concerten/jaar/2026</code> geeft de concerten 1 en 2, in die volgorde, met hun tickets zoals in stap 6 en punt 4. In je terminal staat bij dit verzoek één query.
8. <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> concerten/jaar/2027</code> geeft concert 3, met de tickets van `Jonas Wouters` en `Lies Martens`.
9. <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> concerten/jaar/2028</code> geeft `[]`, met <code style="color:#37b24d;font-weight:600">200</code>.
10. Open je `ERD.md`. Staat de relatie erin, met aan beide kanten de juiste cardinaliteit?
