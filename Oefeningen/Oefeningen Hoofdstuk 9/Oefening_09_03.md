# 09_03

<b>Hoofdstuk 09</b> &nbsp;·&nbsp; DTO's voor lezen, toevoegen en bijwerken &nbsp;·&nbsp; PC-onderdelen &nbsp;·&nbsp; niveau <span style="color:#f03e3e">●●●</span> &nbsp;·&nbsp; ±3 uur

## Leerdoel

Na deze oefening kan je per endpoint de juiste DTO kiezen: een read-DTO om te tonen, en aparte write-DTO's om toe te voegen en bij te werken, met telkens enkel wat de client mag invullen.

Je leert met Mapster lijsten over meerdere niveaus platslaan tot één leesbare lijst, zoals les 09 dat met `SelectMany` doet.

Daarnaast merk je dat een goede write-DTO de regels uit vorige oefeningen voor een deel vanzelf afdwingt: wat niet in de DTO staat, kan de client niet veranderen.

## 

## Opdracht

De boekhouding wil per klant één overzicht: hoeveel bestellingen, wat de actieve lijnen samen waard zijn, en alle lijnen onder elkaar, met de referentie van hun bestelling erbij. En de API mag niets meer aannemen wat een client niet hoort te sturen.

Jouw taak is om elk endpoint om te bouwen naar DTO's, met Mapster, de details-endpoints uit 08_03 een eigen, platte vorm te geven, en het klantenoverzicht te maken.

Via de API moet een medewerker, net als in 08_03, bestellijnen en bestellingen kunnen beheren, de fiche kunnen opvragen, een volledige bestelling kunnen plaatsen en de details kunnen tonen. Daarnaast moet hij:

1. per klant één overzicht kunnen opvragen, met alle lijnen van al zijn bestellingen in één lijst.

Implementeer onderstaande functionaliteiten.

### 1. Je vertrekpunt

Vertrek van je eigen oplossing van 08_03. Alle endpoints blijven werken, met dezelfde routes, dezelfde controles en dezelfde berichten. Enkel hun JSON verandert, zoals hieronder beschreven.

Je modellen en je database veranderen in deze oefening niet. De startgegevens blijven die van 06_03.

Haal de instelling `IgnoreCycles` uit Program.cs weg, zoals les 09 vraagt.

---

### 2. Mapster en de structuur

Installeer Mapster in je project. Maak een map `DTOs`, met daarin een map per model, en een map `Configuration` met een klasse `MapperProfile`, zoals in les 09. Zorg dat Mapster je `MapperProfile` vindt bij het opstarten.

Of je DTO's klassen of records zijn, kies je zelf. Kies je voor records, denk dan na over hoe je de berekende velden invult: een record pas je na het aanmaken niet zomaar meer aan.

> [!IMPORTANT]
> Een DTO bevat nooit een model, en een model nooit een DTO. Een DTO mag wel een andere DTO bevatten, zolang die niet terugverwijst.

---

### 3. De read-DTO's

Elk endpoint dat vroeger een model teruggaf, geeft nu een read-DTO terug. Die heeft dezelfde properties als het model, **zonder de navigation properties**. De `BestelFiche` was nooit een tabel in je database: ze is altijd al een antwoord van je API geweest. Verhuis ze daarom naar je map `DTOs`. Haar `duursteActieveLijn` is voortaan de read-DTO van een bestellijn.

<code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> api/bestellijn/2</code> geeft nu als resultaat:

```json
{ "id": 2, "omschrijving": "DDR5 32 GB kit", "aantal": 2, "stukPrijs": 109.90, "isActief": true, "bestellingId": 1 }
```

---

### 4. De write-DTO's

De client stuurt voortaan enkel nog wat hij mag invullen:

| Verzoek | Velden in de body |
|---------|-------------------|
| <span style="background:#2f9e44;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>POST</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/bestellijn</code> | `omschrijving`, `aantal`, `stukPrijs`, `bestellingId` |
| <span style="background:#d9822b;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>PUT</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/bestellijn/<span style="color:#e64980"><b>{id}</b></span></code> | `omschrijving`, `aantal`, `stukPrijs` |
| <span style="background:#2f9e44;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>POST</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/bestelling</code> | `referentie`, `klantnaam` |
| <span style="background:#2f9e44;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>POST</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/bestelling/volledig</code> | `referentie`, `klantnaam`, en `bestellijnen`, elk met `omschrijving`, `aantal` en `stukPrijs` |

Al de rest in de body negeert je API, gewoon omdat de DTO het niet kent. De controles en de berichten blijven dezelfde.

> [!WARNING]
> Les 09 vermeldt data annotations op write-DTO's. Gebruik ze hier niet voor je controles: ze geven een eigen <code style="color:#f08c00;font-weight:600">400</code>, met een ander bericht dan je API nu belooft.

> [!TIP]
> Een DTO om bij te werken bevat minder dan je model. Vergelijk na een <span style="background:#d9822b;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>PUT</b></span> elk veld met de toestand ervoor, ook de velden die niet in je DTO staan.

---

### 5. De details, plat

<span style="background:#1c7ed6;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>GET</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/bestellijn/<span style="color:#e64980"><b>{id}</b></span>/details</code> geeft de lijn met de referentie en de klantnaam van haar bestelling erbij, plat in één object:

```json
{
  "id": 2,
  "omschrijving": "DDR5 32 GB kit",
  "aantal": 2,
  "stukPrijs": 109.90,
  "isActief": true,
  "bestellingId": 1,
  "referentie": "BB-2026-001",
  "klantnaam": "An Vermeulen"
}
```

<span style="background:#1c7ed6;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>GET</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/bestelling/<span style="color:#e64980"><b>{id}</b></span>/details</code> geeft de bestelling met een korte vorm van al haar lijnen, en het totaal van haar actieve lijnen:

```json
{
  "id": 2,
  "referentie": "BB-2026-002",
  "klantnaam": "Pieter De Wilde",
  "totaalActief": 163.97,
  "lijnen": [
    { "id": 5, "omschrijving": "Voeding 750 W", "aantal": 1, "stukPrijs": 119.00, "isActief": true },
    { "id": 6, "omschrijving": "Casefan 120 mm", "aantal": 3, "stukPrijs": 14.99, "isActief": true }
  ]
}
```

`totaalActief` betekent hetzelfde als in de fiche: de som van aantal maal stukprijs, over de actieve lijnen.

<span style="background:#2f9e44;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>POST</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/bestelling/volledig</code> geeft bij een <code style="color:#37b24d;font-weight:600">201</code> de nieuwe bestelling terug in deze vorm.

De volgorde binnen een lijst met lijnen ligt niet vast. De berichten bij een <code style="color:#f08c00;font-weight:600">404</code> blijven die van 08_03.

---

### 6. Alles samen: het overzicht van een klant

<span style="background:#7048e8;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>SAMENGESTELDE OEFENING</b></span>

<span style="background:#1c7ed6;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>GET</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/bestelling/klant/<span style="color:#e64980"><b>{klantnaam}</b></span></code> geeft voortaan één overzicht, in plaats van een lijst bestellingen:

```json
{
  "klantnaam": "Pieter De Wilde",
  "aantalBestellingen": 1,
  "totaalActief": 163.97,
  "lijnen": [
    { "referentie": "BB-2026-002", "omschrijving": "Voeding 750 W", "aantal": 1, "stukPrijs": 119.00, "isActief": true },
    { "referentie": "BB-2026-002", "omschrijving": "Casefan 120 mm", "aantal": 3, "stukPrijs": 14.99, "isActief": true }
  ]
}
```

- `klantnaam` staat zoals in de database, bij de bestelling van die klant met het kleinste Id;
- `aantalBestellingen` is het aantal bestellingen van die klant;
- `totaalActief` is de som over de actieve lijnen van al die bestellingen;
- `lijnen` bevat alle lijnen van al die bestellingen, actief en geschrapt, in één lijst, elk met de referentie van haar bestelling. Hun volgorde ligt niet vast.

Een klantnaam komt overeen zoals in 08_03, en de <code style="color:#f08c00;font-weight:600">404</code> blijft dezelfde. Je API stuurt voor dit verzoek nog altijd één query naar de database.

> [!TIP]
> Les 09 slaat een klant met bestellingen met orderlijnen plat tot één lijst. Kijk hoe, en beslis dan of je dat in je `MapperProfile` doet of in je controller.

#### Testscenario

Bouw je database opnieuw op, zodat ze exact de startgegevens bevat, en doorloop deze reeks. Bedragen vergelijk je als getal: `0`, `0.0` en `0.00` zijn hetzelfde antwoord.

1. <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> api/bestellijn/2</code>, <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> api/bestellijn/2/details</code>, <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> api/bestelling/2/details</code> en <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> api/bestelling/klant/Pieter De Wilde</code> geven de JSON uit punt 3, 5 en 6. <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> api/bestelling/1</code> geeft `{ "id": 1, "referentie": "BB-2026-001", "klantnaam": "An Vermeulen" }`.
2. <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> api/bestellijn/fiche</code> geeft `totaalActief` `772.76`, en lijn 1 als duurste, zonder veld `bestelling`.
3. Voeg deze lijn toe. Je krijgt <code style="color:#37b24d;font-weight:600">201</code>, met een lijn met Id 7 die actief is en bij bestelling 3 hoort. <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> api/bestelling</code> geeft nog altijd de bestellingen 1, 2 en 3.

   ```json
   {
     "id": 99,
     "omschrijving": "Koeler Noctua NH-D15",
     "aantal": 1,
     "stukPrijs": 109.95,
     "isActief": false,
     "bestellingId": 3,
     "bestelling": { "referentie": "BB-2026-999", "klantnaam": "Niemand" }
   }
   ```

4. Werk lijn 7 bij met dezelfde gegevens, maar met aantal 2, `bestellingId` `1` en `isActief` `false`. Je krijgt <code style="color:#37b24d;font-weight:600">204</code>. Lijn 7 heeft aantal 2, is actief en hoort nog altijd bij bestelling 3.
5. <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> api/bestelling/3/details</code> geeft `totaalActief` `219.90`, met enkel lijn 7.
6. Plaats de volledige bestelling uit punt 4 van 07_03. Je krijgt <code style="color:#37b24d;font-weight:600">201</code> met bestelling 4 in de vorm van punt 5: `totaalActief` `404.98`, en twee actieve lijnen met de Id's 8 en 9.
7. Plaats de volledige bestelling uit stap 6 van 08_03. Je krijgt <code style="color:#37b24d;font-weight:600">201</code> met bestelling 5, en daarin lijn 10.
8. <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> api/bestelling/klant/an vermeulen</code> geeft `"klantnaam": "An Vermeulen"`, `aantalBestellingen` `2`, `totaalActief` `747.79` en vijf lijnen: vier met referentie `BB-2026-001`, waarvan één geschrapt, en één met `BB-2026-005`. In je terminal staat bij dit verzoek één query.
9. <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> api/bestelling/klant/Jan Peeters</code> geeft <code style="color:#f08c00;font-weight:600">404</code> met <code style="color:#845ef7">We vonden geen bestellingen voor Jan Peeters.</code>
10. Plaats een volledige bestelling, met verder geldige gegevens, maar met een lijn met stukprijs `0`. Je krijgt <code style="color:#f08c00;font-weight:600">400</code> met <code style="color:#845ef7">De stukprijs moet groter zijn dan 0.</code> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> api/bestelling</code> geeft nog altijd vijf bestellingen.
