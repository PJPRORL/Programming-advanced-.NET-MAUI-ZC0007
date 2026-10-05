# 08_03

<b>Hoofdstuk 08</b> &nbsp;·&nbsp; Include over een hele lijst &nbsp;·&nbsp; PC-onderdelen &nbsp;·&nbsp; niveau <span style="color:#f03e3e">●●●</span> &nbsp;·&nbsp; ±2,5 uur

## Leerdoel

Na deze oefening kan je met `Include` gerelateerde gegevens mee ophalen voor één object én voor een gefilterde lijst, telkens in één query.

Je leert die methodes in een specifieke repository zetten, ze via je Unit of Work bereiken, en een omweg uit een vorige oefening opruimen nu je API cycli kan afbreken.

Daarnaast merk je dat `Include` niet filtert: wat je mee ophaalt, is alles wat bij het object hoort, ook wat je API op andere plaatsen verbergt.

## 

## Opdracht

De boekhouding wil een bestelling kunnen bekijken zoals op papier: de referentie, de klant en alle lijnen eronder. En wie een klant aan de lijn heeft, wil meteen al zijn bestellingen zien, met hun lijnen.

Jouw taak is om endpoints te maken die een bestellijn, een bestelling of alle bestellingen van een klant teruggeven, samen met de gegevens die erbij horen, telkens met één query naar de database.

Via de API moet een medewerker, net als in 07_03, bestellijnen en bestellingen kunnen beheren, de fiche kunnen opvragen en een volledige bestelling kunnen plaatsen. Daarnaast moet hij:

1. één actieve bestellijn kunnen opvragen, samen met haar bestelling;
2. één bestelling kunnen opvragen, samen met al haar lijnen;
3. alle bestellingen van één klant kunnen opvragen, elk met al haar lijnen.

Implementeer onderstaande functionaliteiten.

### 1. Je vertrekpunt

Vertrek van je eigen oplossing van 07_03. Alle endpoints uit die oefening blijven werken, met dezelfde controles, dezelfde berichten en dezelfde JSON, op één uitzondering na: punt 5. Bij de andere endpoints blijven de navigation properties `null`.

Je modellen en je database veranderen in deze oefening niet. De startgegevens blijven die van 06_03.

---

### 2. Het ERD en de cycli

Maak in de map van je project een bestand `ERD.md`. Beschrijf daarin de relatie van je database met haar cardinaliteiten, in dezelfde vorm als het ERD in les 08: één regel per relatie, met een korte uitleg.

Stel daarna in Program.cs de JSON-serializer zo in dat hij circulaire referenties negeert, zoals les 08 dat doet.

> [!WARNING]
> Les 08 noemt dit zelf een tijdelijke oplossing: op het examen volstaat ze niet. In hoofdstuk 09 haal je deze instelling weer weg.

Het ophalen met `Include` gebeurt in specifieke repositories, niet in de generieke. Laat je Unit of Work telkens het specifieke type teruggeven.

---

### 3. Een bestellijn met haar bestelling

<span style="background:#1c7ed6;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>GET</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/bestellijn/<span style="color:#e64980"><b>{id}</b></span>/details</code>

- Er is geen actieve bestellijn met dat Id: <code style="color:#f08c00;font-weight:600">404 Not Found</code> met <code style="color:#845ef7">We vonden geen actieve bestellijn met Id <span style="color:#e64980"><b>{id}</b></span>.</code>
- Anders: <code style="color:#37b24d;font-weight:600">200 OK</code> met de lijn, en daarin haar bestelling.

<code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> api/bestellijn/2/details</code> geeft als resultaat:

```json
{
  "id": 2,
  "omschrijving": "DDR5 32 GB kit",
  "aantal": 2,
  "stukPrijs": 109.90,
  "isActief": true,
  "bestellingId": 1,
  "bestelling": {
    "id": 1,
    "referentie": "BB-2026-001",
    "klantnaam": "An Vermeulen",
    "bestellijnen": [null]
  }
}
```

> [!NOTE]
> Die `[null]` is geen fout. Entity Framework Core vult de lijst van de bestelling zelf aan met wat het in dit verzoek al opgehaald heeft. Hier is dat enkel de lijn, en die lijn is het begin van je JSON. `IgnoreCycles` schrijft ze daarom niet opnieuw uit, maar zet er `null`. In les 09 los je dat netter op.

---

### 4. Een bestelling met al haar lijnen

<span style="background:#1c7ed6;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>GET</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/bestelling/<span style="color:#e64980"><b>{id}</b></span>/details</code>

- De bestelling bestaat niet: <code style="color:#f08c00;font-weight:600">404 Not Found</code> met <code style="color:#845ef7">We vonden geen bestelling met Id <span style="color:#e64980"><b>{id}</b></span>.</code>
- Anders: <code style="color:#37b24d;font-weight:600">200 OK</code> met de bestelling, en daarin al haar lijnen: de actieve en de geschrapte. Een bestelling zonder lijnen heeft een lege lijst.

<code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> api/bestelling/2/details</code> geeft als resultaat:

```json
{
  "id": 2,
  "referentie": "BB-2026-002",
  "klantnaam": "Pieter De Wilde",
  "bestellijnen": [
    { "id": 5, "omschrijving": "Voeding 750 W", "aantal": 1, "stukPrijs": 119.00, "isActief": true, "bestellingId": 2, "bestelling": null },
    { "id": 6, "omschrijving": "Casefan 120 mm", "aantal": 3, "stukPrijs": 14.99, "isActief": true, "bestellingId": 2, "bestelling": null }
  ]
}
```

De volgorde van de lijnen binnen een bestelling ligt niet vast.

---

### 5. Een volledige bestelling, nu met haar lijnen in het antwoord

In 07_03 gaf <span style="background:#2f9e44;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>POST</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/bestelling/volledig</code> de nieuwe bestelling terug met `"bestellijnen": null`: met haar lijnen erbij liep je API vast op een *object cycle*. Nu je API cycli afbreekt, kan dat wel.

Het endpoint werkt zoals in 07_03, met dezelfde controles, dezelfde berichten en hetzelfde adres. Enkel de body van de <code style="color:#37b24d;font-weight:600">201</code> verandert: de nieuwe bestelling komt terug mét haar nieuwe lijnen, zoals <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> api/bestelling/{id}/details</code> ze zou tonen.

> [!TIP]
> Had je in 07_03 een omweg nodig om de cyclus te vermijden? Kijk of je die nu nog nodig hebt.

---

### 6. Alles samen: alle bestellingen van één klant

<span style="background:#7048e8;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>SAMENGESTELDE OEFENING</b></span>

<span style="background:#1c7ed6;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>GET</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/bestelling/klant/<span style="color:#e64980"><b>{klantnaam}</b></span></code>

- Er is geen enkele bestelling met die klantnaam: <code style="color:#f08c00;font-weight:600">404 Not Found</code> met <code style="color:#845ef7">We vonden geen bestellingen voor <span style="color:#e64980"><b>{klantnaam}</b></span>.</code>
- Anders: <code style="color:#37b24d;font-weight:600">200 OK</code> met alle bestellingen van die klant, op Id gesorteerd, elk met al haar lijnen, zoals in punt 4.

Een klantnaam komt overeen als hij gelijk is, hoofdletters tellen daarbij niet.

> [!IMPORTANT]
> Je API stuurt voor dit verzoek precies één query naar de database, net als bij punt 3 en 4. Kijk in je terminal: Entity Framework Core toont elke query die het uitvoert. Zie je er meer, lees dan nog eens wat les 07 over het N+1-probleem zegt.

#### Testscenario

Bouw je database opnieuw op, zodat ze exact de startgegevens bevat, en doorloop deze reeks:

1. <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> api/bestellijn/2/details</code> en <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> api/bestelling/2/details</code> geven de JSON uit punt 3 en 4.
2. <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> api/bestellijn/4/details</code> geeft <code style="color:#f08c00;font-weight:600">404</code> met <code style="color:#845ef7">We vonden geen actieve bestellijn met Id 4.</code> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> api/bestelling/1/details</code> geeft wél alle vier de lijnen van bestelling 1, ook lijn 4 met `"isActief": false`.
3. <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> api/bestelling/3/details</code> geeft `"bestellijnen": []`. <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> api/bestelling/99/details</code> geeft <code style="color:#f08c00;font-weight:600">404</code> met <code style="color:#845ef7">We vonden geen bestelling met Id 99.</code>
4. <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> api/bestelling/1</code> geeft nog altijd `"bestellijnen": null`, net als in 07_03.
5. Plaats de volledige bestelling uit punt 4 van 07_03. Je krijgt <code style="color:#37b24d;font-weight:600">201</code> met bestelling 4, en daarin twee lijnen, met de Id's 7 en 8. Beide lijnen zijn actief, hebben `bestellingId` `4` en `"bestelling": null`.
6. Plaats nog een volledige bestelling, voor een klant die al eens bestelde. Je krijgt <code style="color:#37b24d;font-weight:600">201</code> met bestelling 5, en daarin lijn 9.

   ```json
   {
     "referentie": "BB-2026-005",
     "klantnaam": "An Vermeulen",
     "bestellijnen": [ { "omschrijving": "Voeding 850 W", "aantal": 1, "stukPrijs": 139.00 } ]
   }
   ```

7. <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> api/bestelling/klant/An Vermeulen</code> geeft de bestellingen 1 en 5, in die volgorde: bestelling 1 met de lijnen 1, 2, 3 en 4, bestelling 5 met lijn 9. In je terminal staat bij dit verzoek één query.
8. <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> api/bestelling/klant/an vermeulen</code> geeft hetzelfde antwoord.
9. <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> api/bestelling/klant/Lotte Jacobs</code> geeft bestelling 3, met `"bestellijnen": []`.
10. <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> api/bestelling/klant/Jan Peeters</code> geeft <code style="color:#f08c00;font-weight:600">404</code> met <code style="color:#845ef7">We vonden geen bestellingen voor Jan Peeters.</code>
11. Open je `ERD.md`. Staat de relatie erin, met aan beide kanten de juiste cardinaliteit?
