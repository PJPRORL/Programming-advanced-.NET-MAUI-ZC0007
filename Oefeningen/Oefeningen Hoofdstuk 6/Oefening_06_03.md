# 06_03

<b>Hoofdstuk 06</b> &nbsp;·&nbsp; Bestellingen en hun lijnen &nbsp;·&nbsp; PC-onderdelen &nbsp;·&nbsp; niveau <span style="color:#f03e3e">●●●</span> &nbsp;·&nbsp; ±2,5 uur

## Leerdoel

Na deze oefening kan je een bestaand model onderbrengen onder een nieuw model, via een één-op-veel-relatie, en alle bestaande regels laten meegroeien.

Je leert je startgegevens voor twee verbonden tabellen via de Fluent API vastleggen, in een eigen methode, zoals les 06 dat doet.

Daarnaast merk je dat dezelfde berekening nu op twee niveaus moet werken: over alle lijnen samen, en over de lijnen van één bestelling.

## 

## Opdracht

De bestellijnen van Bit & Byte hingen tot nu toe los in de lucht: niemand wist bij welke bestelling een lijn hoorde. Vanaf nu hoort elke lijn bij precies één bestelling, en een bestelling heeft meerdere lijnen. De boekhouding wil de fiche voortaan per bestelling kunnen opvragen, en een bestelling in één keer kunnen schrappen.

Jouw taak is om een model `Bestelling` toe te voegen, `Bestellijn` eraan te verbinden, en alles wat je over bestellijnen al kon, ook per bestelling mogelijk te maken.

Via de API moet een medewerker, net als in 05_03, de bestellijnen kunnen beheren, de fiche opvragen en de lijnen boven een bedrag opvragen. Daarnaast moet hij:

1. alle bestellingen en één bestelling kunnen opvragen;
2. een bestelling kunnen toevoegen, en een lege bestelling kunnen verwijderen;
3. de fiche van één bestelling kunnen opvragen;
4. alle actieve lijnen van één bestelling in één keer kunnen schrappen.

Implementeer onderstaande functionaliteiten.

### 1. Je vertrekpunt

Vertrek van je eigen oplossing van 05_03. Alle endpoints uit die oefening blijven werken, met dezelfde controles en dezelfde berichten. Punt 5 voegt er een regel aan toe.

Je `BestandBestellijnRepository` heeft zijn werk gedaan: een JSON-bestand kent geen relaties. Haal hem weg, samen met `bestellijnen.json`.

> [!NOTE]
> In deze oefening bouw je je database opnieuw op. Verwijder ze, en laat ze daarna volledig opbouwen door je migraties. Vanaf nu komen alle startgegevens via de Fluent API, zoals in les 06.

---

### 2. De modellen

Maak in de map Models een klasse `Bestelling`:

| Property     | C#-type            | Kolom in de database                      |
|--------------|--------------------|-------------------------------------------|
| Id           | `int`              | de sleutel, de database telt zelf op      |
| Referentie   | `string`           | tekst van hoogstens 20 tekens, verplicht  |
| Klantnaam    | `string`           | tekst van hoogstens 100 tekens, verplicht |
| Bestellijnen | `List<Bestellijn>?` | navigation property, geen kolom           |

Voeg aan `Bestellijn` toe:

| Property    | C#-type       | Kolom in de database                          |
|-------------|---------------|-----------------------------------------------|
| BestellingId | `int`        | vreemde sleutel naar `Bestellingen`, verplicht |
| Bestelling  | `Bestelling?` | navigation property, geen kolom               |

De tabel heet `Bestellingen`. Voeg ze toe aan je `BestellijnContext`, en leg de relatie vast met de Fluent API.

Een bestelling waarnaar nog een bestellijn verwijst, mag niet verwijderd kunnen worden. Ook niet rechtstreeks in de database, en ook niet als al haar lijnen geschrapt zijn: een geschrapte lijn bestaat nog.

> [!NOTE]
> Let op het vraagteken bij de navigation properties: de client stuurt ze nooit mee. Scalar vult ze in zijn voorbeeld wel in. Haal ze daar weg voor je een verzoek verstuurt.

> [!NOTE]
> De maximale lengtes leg je in deze oefening enkel in je database vast. Een controle in je API vraagt ze niet: de testen sturen enkel teksten die passen.

---

### 3. De startgegevens

> [!IMPORTANT]
> Verdeel `OnModelCreating` in twee methodes, zoals les 06 dat doet: één die de tabellen en relaties opbouwt, en één die de startgegevens toevoegt.

Deze bestellingen:

| Id | Referentie  | Klantnaam       |
|----|-------------|-----------------|
| 1  | BB-2026-001 | An Vermeulen    |
| 2  | BB-2026-002 | Pieter De Wilde |
| 3  | BB-2026-003 | Lotte Jacobs    |

De zes bestellijnen uit 05_03 komen er ook in, met deze bestelling:

| Bestellijn | BestellingId |
|------------|--------------|
| 1, 2, 3, 4 | 1            |
| 5, 6       | 2            |

Bestelling 3 heeft nog geen lijnen.

Maak een migratie met de naam `Bestellingen` en bouw daarmee je database opnieuw op.

> [!WARNING]
> Startgegevens met een vaste Id vertellen de teller van PostgreSQL niet welke Id's al gebruikt zijn. Na het opbouwen van je database moet de eerste bestelling die je via de API toevoegt, Id 4 krijgen, en de eerste bestellijn Id 7. Je leerboek waarschuwt hiervoor bij de startgegevens. Kies een oplossing waarbij je startgegevens precies de Id's uit de tabel houden.

---

### 4. Bestellingen

Maak de repository en de controller die je nodig hebt, asynchroon zoals in les 05.

| Verzoek | Antwoord |
|---------|----------|
| <span style="background:#1c7ed6;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>GET</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/bestelling</code> | <code style="color:#37b24d;font-weight:600">200 OK</code> met alle bestellingen, op Id gesorteerd |
| <span style="background:#1c7ed6;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>GET</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/bestelling/<span style="color:#e64980"><b>{id}</b></span></code> | <code style="color:#37b24d;font-weight:600">200 OK</code> met de bestelling, of <code style="color:#f08c00;font-weight:600">404 Not Found</code> met <code style="color:#845ef7">We vonden geen bestelling met Id <span style="color:#e64980"><b>{id}</b></span>.</code> |
| <span style="background:#2f9e44;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>POST</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/bestelling</code> | <code style="color:#37b24d;font-weight:600">201 Created</code> met de nieuwe bestelling en haar adres, of <code style="color:#f08c00;font-weight:600">400 Bad Request</code> met de reden |
| <span style="background:#e03131;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>DELETE</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/bestelling/<span style="color:#e64980"><b>{id}</b></span></code> | <code style="color:#37b24d;font-weight:600">204 No Content</code>, <code style="color:#f08c00;font-weight:600">404 Not Found</code> met hetzelfde bericht als hierboven, of <code style="color:#f08c00;font-weight:600">400 Bad Request</code> met <code style="color:#845ef7">Deze bestelling heeft nog bestellijnen.</code> |

De redenen voor een <code style="color:#f08c00;font-weight:600">400</code> bij het toevoegen:

- de referentie is leeg: <code style="color:#845ef7">Een bestelling moet een referentie hebben.</code>
- de klantnaam is leeg: <code style="color:#845ef7">Een bestelling moet een klantnaam hebben.</code>

> [!WARNING]
> Krijg je een <code style="color:#f03e3e;font-weight:600">500</code> met een melding over een *object cycle*? Dan kennen twee objecten elkaar over en weer, zonder einde. Les 08 legt uit waarom. Voor nu: haal een object niet op als je enkel wil weten óf het bestaat.

---

### 5. Bestellijnen bij een bestelling

Een nieuwe bestellijn komt voortaan altijd bij een bestelling. De client stuurt daarom ook een `BestellingId` mee.

Na de controles uit 05_03 komt er één bij:

- de bestelling bestaat niet: <code style="color:#f08c00;font-weight:600">400 Bad Request</code> met <code style="color:#845ef7">We vonden geen bestelling met Id <span style="color:#e64980"><b>{bestellingId}</b></span>.</code>

Bijwerken verandert nooit bij welke bestelling een lijn hoort, wat de client ook meestuurt.

> [!TIP]
> De waarschuwing over een *object cycle* uit punt 4 geldt ook hier, en ook bij de fiche in punt 6. Ook een toevoeging kan die opleveren, als je API daarvoor andere objecten heeft opgehaald.

<code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> api/bestellijn/4</code> geeft nu <code style="color:#f08c00;font-weight:600">404 Not Found</code>, want lijn 4 is geschrapt. <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> api/bestellijn/1</code> geeft als resultaat:

```json
{
  "id": 1,
  "omschrijving": "Moederbord ASUS B650",
  "aantal": 1,
  "stukPrijs": 249.99,
  "isActief": true,
  "bestellingId": 1,
  "bestelling": null
}
```

---

### 6. Alles samen: de fiche per bestelling

<span style="background:#7048e8;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>SAMENGESTELDE OEFENING</b></span>

De fiche en het schrappen van alle lijnen werkten tot nu toe over de hele tabel. In dit laatste punt werken ze ook per bestelling, met de relatie, de startgegevens en de regels uit de vorige punten.

| Verzoek | Antwoord |
|---------|----------|
| <span style="background:#1c7ed6;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>GET</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/bestelling/<span style="color:#e64980"><b>{id}</b></span>/fiche</code> | <code style="color:#37b24d;font-weight:600">200 OK</code> met de `BestelFiche`, enkel over de lijnen van die bestelling, of <code style="color:#f08c00;font-weight:600">404 Not Found</code> |
| <span style="background:#e03131;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>DELETE</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/bestelling/<span style="color:#e64980"><b>{id}</b></span>/lijnen</code> | <code style="color:#37b24d;font-weight:600">200 OK</code> met de bijgewerkte fiche van die bestelling, <code style="color:#f08c00;font-weight:600">404 Not Found</code>, of <code style="color:#f08c00;font-weight:600">400 Bad Request</code> met <code style="color:#845ef7">Er zijn geen actieve bestellijnen om te schrappen.</code> |

De <code style="color:#f08c00;font-weight:600">404</code> heeft telkens het bericht <code style="color:#845ef7">We vonden geen bestelling met Id <span style="color:#e64980"><b>{id}</b></span>.</code> De fiche heeft dezelfde zes gegevens met dezelfde betekenis als in 02_03.

#### Testscenario

Bouw je database opnieuw op, zodat ze exact de startgegevens bevat, en doorloop deze reeks. Bedragen vergelijk je als getal: `0`, `0.0` en `0.00` zijn hetzelfde antwoord.

1. <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> api/bestelling/1/fiche</code> geeft als resultaat:

   ```json
   {
     "aantalActieveLijnen": 3,
     "aantalGeschrapteLijnen": 1,
     "totaalActief": 608.79,
     "totaalGeschrapt": 89.95,
     "duursteActieveLijn": {
       "id": 1,
       "omschrijving": "Moederbord ASUS B650",
       "aantal": 1,
       "stukPrijs": 249.99,
       "isActief": true,
       "bestellingId": 1,
       "bestelling": null
     },
     "isLeeg": false
   }
   ```

2. <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> api/bestelling/3/fiche</code> geeft een fiche met overal `0`, `duursteActieveLijn` `null` en `isLeeg` `true`.
3. Voeg deze lijn toe. Je krijgt <code style="color:#37b24d;font-weight:600">201</code>, en de nieuwe lijn heeft Id 7, is actief, en hoort bij bestelling 3.

   ```json
   { "omschrijving": "Koeler Noctua NH-D15", "aantal": 1, "stukPrijs": 109.95, "bestellingId": 3 }
   ```

4. Voeg dezelfde lijn toe met `bestellingId` 99. Je krijgt <code style="color:#f08c00;font-weight:600">400</code> met <code style="color:#845ef7">We vonden geen bestelling met Id 99.</code>
5. Verwijder bestelling 3. Je krijgt <code style="color:#f08c00;font-weight:600">400</code> met <code style="color:#845ef7">Deze bestelling heeft nog bestellijnen.</code>
6. Schrap alle lijnen van bestelling 2. Je krijgt <code style="color:#37b24d;font-weight:600">200</code>, met een fiche waarin `aantalActieveLijnen` `0` is, `aantalGeschrapteLijnen` `2`, `totaalActief` `0`, `totaalGeschrapt` `163.97`, `duursteActieveLijn` `null` en `isLeeg` `true`.
7. Doe het nog eens. Je krijgt <code style="color:#f08c00;font-weight:600">400</code> met <code style="color:#845ef7">Er zijn geen actieve bestellijnen om te schrappen.</code>
8. <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> api/bestellijn/fiche</code>, over alle lijnen samen, geeft `aantalActieveLijnen` `4`, `aantalGeschrapteLijnen` `3`, `totaalActief` `718.74`, `totaalGeschrapt` `253.92`, en lijn 1 als duurste.
9. Verwijder bestelling 2. Je krijgt <code style="color:#f08c00;font-weight:600">400</code>: haar lijnen zijn geschrapt, maar ze bestaan nog.
10. Voeg een bestelling toe met referentie `BB-2026-004` en klantnaam `Mehdi Aziz`. Je krijgt <code style="color:#37b24d;font-weight:600">201</code>, en ze heeft Id 4. Verwijder ze meteen weer. Je krijgt <code style="color:#37b24d;font-weight:600">204</code>.
