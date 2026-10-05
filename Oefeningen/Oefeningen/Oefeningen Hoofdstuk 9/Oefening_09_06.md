# 09_06

<b>Hoofdstuk 09</b> &nbsp;·&nbsp; DTO's voor lezen, toevoegen en bijwerken &nbsp;·&nbsp; Concertzaal &nbsp;·&nbsp; niveau <span style="color:#f03e3e">●●●</span> &nbsp;·&nbsp; ±3 uur

## Leerdoel

Na deze oefening kan je per endpoint de juiste DTO kiezen: een read-DTO om te tonen, en aparte write-DTO's om toe te voegen en bij te werken, met telkens enkel wat de client mag invullen.

Je leert met Mapster lijsten over meerdere niveaus platslaan tot één leesbare lijst, zoals les 09 dat met `SelectMany` doet.

Daarnaast merk je dat een goede write-DTO de regels uit vorige oefeningen voor een deel vanzelf afdwingt: wat niet in de DTO staat, kan de client niet veranderen.

## 

## Opdracht

Wie het seizoen plant, wil per jaar één overzicht: hoeveel concerten, wat ze samen opbrachten, en alle tickets onder elkaar, met de titel van hun concert erbij. En de API mag niets meer aannemen wat een client niet hoort te sturen.

Jouw taak is om elk endpoint om te bouwen naar DTO's, met Mapster, de details-endpoints uit 08_06 een eigen, platte vorm te geven, en het jaaroverzicht te maken.

Via de API moet een medewerker, net als in 08_06, tickets en concerten kunnen beheren, tickets kunnen verhuizen en herstellen, groepen kunnen verkopen, de opbrengst kunnen opvragen en de details kunnen tonen. Daarnaast moet hij:

1. per jaar één overzicht kunnen opvragen, met alle tickets van alle concerten in één lijst.

Implementeer onderstaande functionaliteiten.

### 1. Je vertrekpunt

Vertrek van je eigen oplossing van 08_06. Alle endpoints blijven werken, met dezelfde routes, dezelfde controles en dezelfde berichten. Enkel hun JSON verandert, zoals hieronder beschreven.

Je modellen en je database veranderen in deze oefening niet. De startgegevens blijven die van 06_06.

Haal de instelling `IgnoreCycles` uit Program.cs weg, zoals les 09 vraagt.

---

### 2. Mapster en de structuur

Installeer Mapster in je project. Maak een map `DTOs`, met daarin een map per model, en een map `Configuration` met een klasse `MapperProfile`, zoals in les 09. Zorg dat Mapster je `MapperProfile` vindt bij het opstarten.

Of je DTO's klassen of records zijn, kies je zelf. Kies je voor records, denk dan na over hoe je de berekende velden invult: een record pas je na het aanmaken niet zomaar meer aan.

> [!IMPORTANT]
> Een DTO bevat nooit een model, en een model nooit een DTO. Een DTO mag wel een andere DTO bevatten, zolang die niet terugverwijst.

---

### 3. De read-DTO's

Elk endpoint dat vroeger een model teruggaf, geeft nu een read-DTO terug. Die heeft dezelfde properties als het model, **zonder de navigation properties**. Dat geldt ook voor de lijst die de groepsverkoop teruggeeft.

<code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> api/ticket/3</code> geeft nu als resultaat:

```json
{ "id": 3, "naam": "Lisa Maes", "rij": 3, "stoel": 12, "prijs": 35.00, "isGeannuleerd": false, "concertId": 1 }
```

---

### 4. De write-DTO's

De client stuurt voortaan enkel nog wat hij mag invullen:

| Verzoek | Velden in de body |
|---------|-------------------|
| <span style="background:#2f9e44;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>POST</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/ticket</code> | `naam`, `rij`, `stoel`, `prijs`, `concertId` |
| <span style="background:#d9822b;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>PUT</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/ticket/<span style="color:#e64980"><b>{id}</b></span></code> | `naam`, `rij`, `stoel`, `prijs` |
| <span style="background:#2f9e44;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>POST</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/concert</code> | `titel`, `datum` |
| <span style="background:#2f9e44;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>POST</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/ticket/groep</code> | een lijst tickets, elk met `naam`, `rij`, `stoel`, `prijs` en `concertId` |

Al de rest in de body negeert je API, gewoon omdat de DTO het niet kent. De controles en de berichten blijven dezelfde.

> [!WARNING]
> Les 09 vermeldt data annotations op write-DTO's. Gebruik ze hier niet voor je controles: ze geven een eigen <code style="color:#f08c00;font-weight:600">400</code>, met een ander bericht dan je API nu belooft.

> [!TIP]
> Een DTO om bij te werken bevat minder dan je model. Vergelijk na een <span style="background:#d9822b;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>PUT</b></span> elk veld met de toestand ervoor, ook de velden die niet in je DTO staan.

---

### 5. De details, plat

<span style="background:#1c7ed6;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>GET</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/ticket/<span style="color:#e64980"><b>{id}</b></span>/details</code> geeft het ticket met de titel en de datum van zijn concert erbij, plat in één object:

```json
{
  "id": 3,
  "naam": "Lisa Maes",
  "rij": 3,
  "stoel": 12,
  "prijs": 35.00,
  "isGeannuleerd": false,
  "concertId": 1,
  "titel": "Jazz in de Notenbalk",
  "datum": "14-11-2026"
}
```

<span style="background:#1c7ed6;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>GET</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/concert/<span style="color:#e64980"><b>{id}</b></span>/details</code> geeft het concert met een korte vorm van al zijn tickets, en zijn opbrengst:

```json
{
  "id": 2,
  "titel": "Strijkkwartet Aurora",
  "datum": "21-11-2026",
  "opbrengst": 50.00,
  "tickets": [
    { "id": 5, "naam": "Emma Claes", "rij": 7, "stoel": 2, "prijs": 25.00, "isGeannuleerd": false },
    { "id": 6, "naam": "Noah Willems", "rij": 7, "stoel": 3, "prijs": 25.00, "isGeannuleerd": false }
  ]
}
```

`opbrengst` betekent hetzelfde als in 06_06: de som van de prijzen van de geldige tickets van het concert.

De volgorde binnen een lijst met tickets ligt niet vast. De berichten bij een <code style="color:#f08c00;font-weight:600">404</code> blijven die van 08_06.

---

### 6. Alles samen: het overzicht van een jaar

<span style="background:#7048e8;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>SAMENGESTELDE OEFENING</b></span>

<span style="background:#1c7ed6;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>GET</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/concert/jaar/<span style="color:#e64980"><b>{jaar}</b></span></code> geeft voortaan één overzicht, in plaats van een lijst concerten:

```json
{
  "jaar": 2027,
  "aantalConcerten": 1,
  "opbrengst": 0,
  "tickets": []
}
```

- `jaar` is het jaar uit de route;
- `aantalConcerten` is het aantal concerten in dat jaar;
- `opbrengst` is de som over de geldige tickets van al die concerten;
- `tickets` bevat alle tickets van al die concerten, geldig en geannuleerd, in één lijst, elk met de titel van zijn concert erbij. Elk ticket in die lijst heeft de velden `titel`, `naam`, `rij`, `stoel`, `prijs` en `isGeannuleerd`. Hun volgorde ligt niet vast.

Het jaar wordt bepaald zoals in 08_06, en de <code style="color:#f08c00;font-weight:600">404</code> blijft dezelfde. Je API stuurt voor dit verzoek nog altijd één query naar de database.

> [!TIP]
> Les 09 slaat een klant met bestellingen met orderlijnen plat tot één lijst. Kijk hoe, en beslis dan of je dat in je `MapperProfile` doet of in je controller.

#### Testscenario

Bouw je database opnieuw op, zodat ze exact de startgegevens bevat, en doorloop deze reeks. Bedragen vergelijk je als getal: `0`, `0.0` en `0.00` zijn hetzelfde antwoord.

1. <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> api/ticket/3</code>, <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> api/ticket/3/details</code>, <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> api/concert/2/details</code> en <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> api/concert/jaar/2027</code> geven de JSON uit punt 3, 5 en 6. <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> api/concert/1</code> geeft `{ "id": 1, "titel": "Jazz in de Notenbalk", "datum": "14-11-2026" }`.
2. <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> api/concert/1/details</code> geeft `opbrengst` `125.00`, met alle vier de tickets, ook ticket 4 met `"isGeannuleerd": true`.
3. Verkoop dit ticket. Je krijgt <code style="color:#37b24d;font-weight:600">201</code>, met een ticket met Id 7 dat niet geannuleerd is en bij concert 2 hoort. <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> api/concert</code> geeft nog altijd de concerten 1, 2 en 3.

   ```json
   {
     "id": 99,
     "naam": "Jonas Wouters",
     "rij": 1,
     "stoel": 5,
     "prijs": 30.00,
     "isGeannuleerd": true,
     "concertId": 2,
     "concert": { "titel": "Vals concert", "datum": "01-01-2027" }
   }
   ```

4. Wijzig ticket 7 met dezelfde gegevens, maar op stoel 7, met `concertId` `1` en `isGeannuleerd` `true`. Je krijgt <code style="color:#37b24d;font-weight:600">204</code>. Ticket 7 zit op rij 1 stoel 7, is niet geannuleerd en hoort nog altijd bij concert 2.
5. <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> api/concert/2/details</code> geeft `opbrengst` `80.00`, met de tickets 5, 6 en 7.
6. Verkoop de groep uit 07_06. Je krijgt <code style="color:#37b24d;font-weight:600">200</code> met drie tickets, met de Id's 8, 9 en 10, zonder veld `concert`.
7. <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> api/concert/jaar/2026</code> geeft `aantalConcerten` `2`, `opbrengst` `235.00` en acht tickets: vijf met titel `Jazz in de Notenbalk`, waarvan één geannuleerd, en drie met titel `Strijkkwartet Aurora`. In je terminal staat bij dit verzoek één query.
8. <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> api/concert/jaar/2027</code> geeft `aantalConcerten` `1`, `opbrengst` `60.00`, en de tickets van `Jonas Wouters` en `Lies Martens`.
9. <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> api/concert/jaar/2028</code> geeft <code style="color:#f08c00;font-weight:600">404</code> met <code style="color:#845ef7">We vonden geen concerten in 2028.</code>
10. Wijzig ticket 3 met een lege naam, en verder geldige gegevens. Je krijgt <code style="color:#f08c00;font-weight:600">400</code> met <code style="color:#845ef7">Een ticket moet op naam staan.</code>
