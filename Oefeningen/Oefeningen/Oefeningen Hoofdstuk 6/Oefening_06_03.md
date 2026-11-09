# 06_03

<b>Hoofdstuk 06</b> &nbsp;·&nbsp; Bestellingen en hun lijnen &nbsp;·&nbsp; PC-onderdelen &nbsp;·&nbsp; niveau <span style="color:#f03e3e">●●●</span> &nbsp;·&nbsp; ±2,5 uur

## Leerdoel

Na deze oefening kan je een bestaand model onder een nieuw model brengen in een één-op-veel-relatie, en alles wat je al kon, ook per "één"-kant mogelijk maken.

Je leert `OnModelCreating` opsplitsen zodra het te lang wordt, startgegevens voor twee verbonden tabellen meegeven, en in een query filteren via een navigation property.

Daarnaast reken je per bestelling, en bewaak je dat een bestelling met lijnen niet verdwijnt.

## 

## Opdracht

De bestellijnen van Bit & Byte hingen tot nu toe los in de lucht: niemand wist bij welke bestelling een lijn hoorde. Vanaf nu hoort elke lijn bij precies één bestelling, en een bestelling heeft meerdere lijnen. De boekhouding wil de fiche voortaan per bestelling kunnen opvragen, een bestelling in één keer kunnen schrappen, en per klant kunnen tellen hoeveel lijnen er nog openstaan.

Jouw taak is om een model `Bestelling` toe te voegen, `Bestellijn` eraan te verbinden, en alles wat je over bestellijnen al kon, ook per bestelling mogelijk te maken.

Via de API moet een medewerker, net als in 05_03, de bestellijnen kunnen beheren, de fiche opvragen en de lijnen boven een bedrag opvragen. Daarnaast moet hij:

1. alle bestellingen en één bestelling kunnen opvragen;
2. een bestelling kunnen toevoegen, en een lege bestelling kunnen verwijderen;
3. de fiche van één bestelling kunnen opvragen;
4. alle actieve lijnen van één bestelling in één keer kunnen schrappen;
5. per klant kunnen tellen hoeveel actieve lijnen er zijn.

Implementeer onderstaande functionaliteiten.

### 1. Je vertrekpunt

Vertrek van je eigen oplossing van 05_03, met de databaserepository. Alle endpoints uit die oefening blijven werken, met dezelfde berichten.

> [!NOTE]
> Je bestandsrepository uit 05_03 kent geen bestellingen. Vanaf nu gebruik je enkel nog de databaserepository. De bestandsversie mag blijven staan, maar je registreert ze niet meer.

> [!NOTE]
> In deze oefening bouw je je database opnieuw op. Verwijder ze, maak de nieuwe migratie uit punt 3, en laat je database daarna volledig opbouwen door al je migraties.

---

### 2. De modellen

Maak in de map Models een klasse `Bestelling`:

| Property     | C#-type             | Kolom in de database |
|--------------|---------------------|----------------------|
| Id           | `int`               | de sleutel, de database telt zelf op |
| Referentie   | `string`            | tekst van hoogstens 20 tekens, verplicht |
| Klantnaam    | `string`            | tekst van hoogstens 100 tekens, verplicht |
| Bestellijnen | `List<Bestellijn>?` | navigation property, geen kolom |

Voeg aan `Bestellijn` toe:

| Property     | C#-type       | Kolom in de database |
|--------------|---------------|----------------------|
| BestellingId | `int`         | vreemde sleutel naar `Bestellingen`, verplicht |
| Bestelling   | `Bestelling?` | navigation property, geen kolom |

Leg de relatie vast met de Fluent API: een lijn hoort bij één bestelling, een bestelling heeft veel lijnen, de relatie is verplicht, en een bestelling met lijnen mag niet verwijderd kunnen worden, ook niet rechtstreeks in de database. De tabel heet `Bestellingen`.

> [!TIP]
> Les 06 verdeelt `OnModelCreating` in twee methodes: één die de tabellen opbouwt en één die de startgegevens toevoegt. Doe dat ook. Zo blijft je context leesbaar, nu er een tweede tabel bij komt.

---

### 3. De startgegevens

Leg met `HasData` deze bestellingen vast:

| Id | Referentie  | Klantnaam       |
|----|-------------|-----------------|
| 1  | BB-2026-001 | An Vermeulen    |
| 2  | BB-2026-002 | Pieter De Wilde |
| 3  | BB-2026-003 | Lotte Jacobs    |

Geef elke bestellijn in je startgegevens haar bestelling:

| Bestellijn | BestellingId |
|------------|--------------|
| 1, 2, 3, 4 | 1            |
| 5, 6       | 2            |

Bestelling 3 heeft nog geen lijnen. Maak een migratie met de naam `Bestellingen`, verwijder je database, en bouw ze opnieuw op met al je migraties. Zorg dat de teller van beide tabellen daarna klopt, zoals in 04_03.

---

### 4. Bestellingen

Maak een interface `IBestellingRepository` en een klasse `BestellingRepository`, asynchroon, met je context in een `readonly` veld. Registreer ze met `AddScoped`.

| Methode | Parameters | Geeft terug |
|---------|------------|-------------|
| `GetAllAsync` | geen | `Task<List<Bestelling>>` |
| `GetByIdAsync` | `int id` | `Task<Bestelling?>` |
| `CreateAsync` | `Bestelling bestelling` | `Task<Bestelling>` |
| `DeleteAsync` | `int id` | `Task` |
| `HeeftLijnenAsync` | `int bestellingId` | `Task<bool>` |
| `GetFicheAsync` | `int bestellingId` | `Task<BestelFiche>` |

`HeeftLijnenAsync` kijkt naar alle lijnen, ook de geschrapte. `GetFicheAsync` geeft dezelfde zes gegevens als de fiche uit 05_03, maar enkel over de lijnen van die bestelling. De database telt en telt op, en elke som rond je af zoals in 04_03.

Maak een `BestellingController` die luistert op <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/bestelling</code>:

| Verzoek | Antwoord |
|---------|----------|
| <span style="background:#1c7ed6;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>GET</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/bestelling</code> | <code style="color:#37b24d;font-weight:600">200 OK</code> met alle bestellingen, op Id gesorteerd |
| <span style="background:#1c7ed6;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>GET</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/bestelling/<span style="color:#e64980"><b>{id}</b></span></code> | <code style="color:#37b24d;font-weight:600">200 OK</code> met de bestelling, of <code style="color:#f08c00;font-weight:600">404 Not Found</code> met <code style="color:#845ef7">We vonden geen bestelling met Id <span style="color:#e64980"><b>{id}</b></span>.</code> |
| <span style="background:#2f9e44;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>POST</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/bestelling</code> | <code style="color:#37b24d;font-weight:600">201 Created</code> met de nieuwe bestelling en haar adres, of <code style="color:#f08c00;font-weight:600">400 Bad Request</code> met de reden |
| <span style="background:#e03131;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>DELETE</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/bestelling/<span style="color:#e64980"><b>{id}</b></span></code> | <code style="color:#37b24d;font-weight:600">204 No Content</code>, <code style="color:#f08c00;font-weight:600">404 Not Found</code> met hetzelfde bericht als hierboven, of <code style="color:#f08c00;font-weight:600">400 Bad Request</code> met <code style="color:#845ef7">Deze bestelling heeft nog bestellijnen.</code> |

De redenen voor een <code style="color:#f08c00;font-weight:600">400</code> bij het toevoegen:

- de referentie is leeg: <code style="color:#845ef7">Een bestelling moet een referentie hebben.</code>
- de klantnaam is leeg: <code style="color:#845ef7">Een bestelling moet een klantnaam hebben.</code>

Log zoals in 05_03, met `Error` bij een DELETE die niets vindt.

---

### 5. Bestellijnen bij een bestelling

Een nieuwe of bijgewerkte bestellijn moet bij een bestaande bestelling horen. Voeg aan de controles uit 05_03 deze toe, bij toevoegen en bijwerken:

- de bestelling bestaat niet: <code style="color:#f08c00;font-weight:600">400 Bad Request</code> met <code style="color:#845ef7">We vonden geen bestelling met Id <span style="color:#e64980"><b>{bestellingId}</b></span>.</code>

Breid `IBestellijnRepository` uit met:

| Methode | Parameters | Geeft terug |
|---------|------------|-------------|
| `SchrapAllesVanBestellingAsync` | `int bestellingId` | `Task` |
| `HasAnyActiefInBestellingAsync` | `int bestellingId` | `Task<bool>` |
| `CountActiefByKlantAsync` | `string klantnaam` | `Task<int>` |

`CountActiefByKlantAsync` telt de actieve lijnen van alle bestellingen van die klant, zonder op hoofdletters te letten, met `CountAsync`.

> [!TIP]
> De klantnaam staat niet in de tabel `Bestellijnen`, maar in `Bestellingen`. Je hoeft de bestellingen niet eerst op te halen: in een query mag je via de navigation property van een lijn naar de klantnaam van haar bestelling. Kijk in je terminal welke SQL Entity Framework Core daarvan maakt.

<code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> api/bestellijn/4</code> geeft <code style="color:#f08c00;font-weight:600">404 Not Found</code>, want lijn 4 is geschrapt. <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> api/bestellijn/1</code> geeft als resultaat:

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

De fiche, het schrappen van alle lijnen en het tellen werkten tot nu toe over de hele tabel. In dit laatste punt werken ze ook per bestelling en per klant, met de relatie, de startgegevens en de regels uit de vorige punten.

| Verzoek | Antwoord |
|---------|----------|
| <span style="background:#1c7ed6;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>GET</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/bestelling/<span style="color:#e64980"><b>{id}</b></span>/fiche</code> | <code style="color:#37b24d;font-weight:600">200 OK</code> met de `BestelFiche`, enkel over de lijnen van die bestelling, of <code style="color:#f08c00;font-weight:600">404 Not Found</code> |
| <span style="background:#e03131;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>DELETE</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/bestelling/<span style="color:#e64980"><b>{id}</b></span>/lijnen</code> | <code style="color:#37b24d;font-weight:600">200 OK</code> met de bijgewerkte fiche van die bestelling, <code style="color:#f08c00;font-weight:600">404 Not Found</code>, of <code style="color:#f08c00;font-weight:600">400 Bad Request</code> met <code style="color:#845ef7">Er zijn geen actieve bestellijnen om te schrappen.</code> |
| <span style="background:#1c7ed6;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>GET</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/bestellijn/klant/<span style="color:#e64980"><b>{klantnaam}</b></span>/tellen</code> | <code style="color:#37b24d;font-weight:600">200 OK</code> met een object met een property `count`, een `int` |

De <code style="color:#f08c00;font-weight:600">404</code> heeft telkens het bericht <code style="color:#845ef7">We vonden geen bestelling met Id <span style="color:#e64980"><b>{id}</b></span>.</code> Een klant zonder bestellingen geeft `{ "count": 0 }`, met <code style="color:#37b24d;font-weight:600">200 OK</code>.

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
3. <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> api/bestellijn/klant/an vermeulen/tellen</code> geeft `{ "count": 3 }`, en voor `Lotte Jacobs` `{ "count": 0 }`.
4. Voeg deze lijn toe:

   ```json
   { "omschrijving": "CPU-koeler", "aantal": 1, "stukPrijs": 109.95, "bestellingId": 3 }
   ```

   Je krijgt <code style="color:#37b24d;font-weight:600">201</code>, en de nieuwe lijn heeft Id 7, is actief, en hoort bij bestelling 3. Voor `LOTTE JACOBS` telt je API nu `{ "count": 1 }`.
5. Voeg dezelfde lijn toe met `bestellingId` 99. Je krijgt <code style="color:#f08c00;font-weight:600">400</code> met <code style="color:#845ef7">We vonden geen bestelling met Id 99.</code>
6. Verwijder bestelling 3. Je krijgt <code style="color:#f08c00;font-weight:600">400</code> met <code style="color:#845ef7">Deze bestelling heeft nog bestellijnen.</code>
7. Schrap alle lijnen van bestelling 2. Je krijgt <code style="color:#37b24d;font-weight:600">200</code>, met een fiche waarin `aantalActieveLijnen` `0` is, `aantalGeschrapteLijnen` `2`, `totaalActief` `0`, `totaalGeschrapt` `163.97`, `duursteActieveLijn` `null` en `isLeeg` `true`.
8. Doe het nog eens. Je krijgt <code style="color:#f08c00;font-weight:600">400</code> met <code style="color:#845ef7">Er zijn geen actieve bestellijnen om te schrappen.</code>
9. <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> api/bestellijn/fiche</code>, over alle lijnen samen, geeft `aantalActieveLijnen` `4`, `aantalGeschrapteLijnen` `3`, `totaalActief` `718.74`, `totaalGeschrapt` `253.92`, en lijn 1 als duurste.
10. Verwijder bestelling 2. Je krijgt <code style="color:#f08c00;font-weight:600">400</code>: haar lijnen zijn geschrapt, maar ze bestaan nog.
11. Voeg een bestelling toe met referentie `BB-2026-004` en klantnaam `Mehdi Aziz`. Je krijgt <code style="color:#37b24d;font-weight:600">201</code>, en ze heeft Id 4. Verwijder ze meteen weer. Je krijgt <code style="color:#37b24d;font-weight:600">204</code>. Verwijder ze nog eens: nu krijg je <code style="color:#f08c00;font-weight:600">404</code>, en in je terminal staat een `Error`.
