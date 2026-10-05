# 09_04

<b>Hoofdstuk 09</b> &nbsp;·&nbsp; DTO's en Mapster &nbsp;·&nbsp; Dierenasiel &nbsp;·&nbsp; niveau <span style="color:#37b24d">●○○</span> &nbsp;·&nbsp; ±2 uur

## Leerdoel

Na deze oefening kan je je modellen afschermen van de buitenwereld met DTO's: aparte klassen voor wat je API toont en voor wat ze ontvangt.

Je leert Mapster gebruiken om modellen naar DTO's om te zetten en omgekeerd, met een `MapperProfile` voor wat niet vanzelf overeenkomt.

Daarnaast merk je dat `IgnoreCycles` overbodig wordt, en dat een client die meer meestuurt dan nodig, niets meer kan aanrichten.

## 

## Opdracht

De JSON van de API toont vandaag de binnenkant van de database: lege navigation properties en `[null]`-lijsten. En een POST neemt alles aan wat een model kent. De website wil een API die enkel toont wat ze nodig heeft, en enkel aanneemt wat ze mag sturen.

Jouw taak is om elk endpoint om te bouwen naar DTO's, met Mapster, en de details-endpoints uit 08_04 een eigen, platte vorm te geven.

Via de API moet de website, net als in 08_04, alle dieren en verblijven kunnen opvragen, filteren, zoeken en de fiche en de details tonen, verblijven kunnen toevoegen, bijwerken en verwijderen, en dieren kunnen verhuizen. Daarnaast moet ze:

1. bij elk verblijf in het overzicht meteen zien hoeveel dieren er wonen, en hoeveel plaatsen er nog vrij zijn.

Implementeer onderstaande functionaliteiten.

### 1. Je vertrekpunt

Vertrek van je eigen oplossing van 08_04. Alle endpoints blijven werken, met dezelfde routes, dezelfde controles en dezelfde berichten. Enkel hun JSON verandert, zoals hieronder beschreven.

Je modellen en je database veranderen in deze oefening niet. De startgegevens blijven die van 06_04.

Haal de instelling `IgnoreCycles` uit Program.cs weg, zoals les 09 vraagt.

---

### 2. Mapster en de structuur

Installeer Mapster in je project. Maak een map `DTOs`, met daarin een map per model, en een map `Configuration` met een klasse `MapperProfile`, zoals in les 09. Zorg dat Mapster je `MapperProfile` vindt bij het opstarten.

Of je DTO's klassen of records zijn, kies je zelf. Kies je voor records, denk dan na over hoe je de berekende velden invult: een record pas je na het aanmaken niet zomaar meer aan.

> [!IMPORTANT]
> Een DTO bevat nooit een model, en een model nooit een DTO. Een DTO mag wel een andere DTO bevatten, zolang die niet terugverwijst.

---

### 3. De read-DTO's

Elk endpoint dat vroeger een model teruggaf, geeft nu een read-DTO terug. Die heeft dezelfde properties als het model, **zonder de navigation properties**. De `DierFiche` was nooit een tabel in je database: ze is altijd al een antwoord van je API geweest. Verhuis ze daarom naar je map `DTOs`, en laat ze de read-DTO van het dier bevatten in plaats van het model.

<code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> api/dier/4</code> geeft nu als resultaat:

```json
{
  "id": 4,
  "naam": "Bella",
  "soort": "hond",
  "leeftijdInJaren": 1,
  "isGechipt": true,
  "verblijfId": 2
}
```

<code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> api/verblijf/2</code> geeft `{ "id": 2, "naam": "Hondenren", "capaciteit": 4 }`.

---

### 4. De write-DTO

<span style="background:#2f9e44;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>POST</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/verblijf</code> en <span style="background:#d9822b;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>PUT</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/verblijf/<span style="color:#e64980"><b>{id}</b></span></code> ontvangen voortaan een write-DTO met enkel `naam` en `capaciteit`. Een Id of een lijst dieren in de body negeert je API, gewoon omdat de DTO ze niet kent.

De controles en de berichten blijven dezelfde.

> [!WARNING]
> Les 09 vermeldt data annotations op write-DTO's. Gebruik ze hier niet voor je controles: ze geven een eigen <code style="color:#f08c00;font-weight:600">400</code>, met een ander bericht dan je API nu belooft.

---

### 5. Details, plat en zonder cycli

<span style="background:#1c7ed6;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>GET</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/dier/<span style="color:#e64980"><b>{id}</b></span>/details</code> geeft het dier met de naam en de capaciteit van zijn verblijf erbij, plat in één object:

```json
{
  "id": 4,
  "naam": "Bella",
  "soort": "hond",
  "leeftijdInJaren": 1,
  "isGechipt": true,
  "verblijfId": 2,
  "verblijfNaam": "Hondenren",
  "verblijfCapaciteit": 4
}
```

<span style="background:#1c7ed6;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>GET</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/verblijf/<span style="color:#e64980"><b>{id}</b></span>/details</code> geeft het verblijf met een korte vorm van zijn dieren, in `bewoners`:

```json
{
  "id": 2,
  "naam": "Hondenren",
  "capaciteit": 4,
  "aantalDieren": 2,
  "vrijePlaatsen": 2,
  "bewoners": [
    { "id": 3, "naam": "Rex", "soort": "hond", "leeftijdInJaren": 7 },
    { "id": 4, "naam": "Bella", "soort": "hond", "leeftijdInJaren": 1 }
  ]
}
```

- `aantalDieren` is het aantal dieren in het verblijf;
- `vrijePlaatsen` is de capaciteit min dat aantal, en nooit minder dan 0.

De volgorde van de dieren binnen een verblijf ligt niet vast. De berichten bij een <code style="color:#f08c00;font-weight:600">404</code> blijven die van 08_04.

> [!TIP]
> Les 09 raadt af om te rekenen in je `MapperProfile`. Beslis zelf waar `aantalDieren` en `vrijePlaatsen` berekend worden, en waarom daar.

---

### 6. Alles samen: het overzicht van alle verblijven

<span style="background:#7048e8;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>SAMENGESTELDE OEFENING</b></span>

<span style="background:#1c7ed6;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>GET</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/verblijf/details</code> geeft alle verblijven, op Id gesorteerd, elk in de vorm van punt 5. Je API stuurt daarvoor nog altijd één query naar de database.

Zo komen alle stukken samen: de `Include` uit 08_04, je read-DTO's, je `MapperProfile` en je berekening.

#### Testscenario

Bouw je database opnieuw op, zodat ze exact de startgegevens bevat, en doorloop deze reeks:

1. <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> api/dier/4</code>, <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> api/verblijf/2</code>, <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> api/dier/4/details</code> en <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> api/verblijf/2/details</code> geven de JSON uit punt 3 en 5. Nergens staat nog een veld `verblijf` of `dieren` met `null`.
2. <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> api/dier/fiche/5</code> geeft de fiche van `Flappie`, met `"verblijfId": 3` in het dier, en zonder `"verblijf"`.
3. <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> api/verblijf/details</code> geeft de verblijven 1, 2, 3, 4 en 5, in die volgorde. Verblijf 1 heeft `aantalDieren` `2` en `vrijePlaatsen` `4`, verblijf 2 `2` en `2`, verblijf 3 `1` en `4`, verblijf 4 `1` en `9`, en verblijf 5 `0` en `2`, met `"bewoners": []`. In je terminal staat bij dit verzoek één query.
4. Voeg dit verblijf toe. Je krijgt <code style="color:#37b24d;font-weight:600">201</code>, met `{ "id": 6, "naam": "Opvangkamer", "capaciteit": 3 }`. <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> api/verblijf/6/details</code> geeft `"aantalDieren": 0` en `"bewoners": []`.

   ```json
   {
     "id": 99,
     "naam": "Opvangkamer",
     "capaciteit": 3,
     "dieren": [ { "naam": "Pluis", "soort": "konijn", "leeftijdInJaren": 1 } ]
   }
   ```

5. <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> api/dier</code> geeft nog altijd zes dieren.
6. Werk verblijf 5 bij met de naam `Quarantaine` en een capaciteit van 1. Je krijgt <code style="color:#37b24d;font-weight:600">204</code>. Werk verblijf 1 bij met een lege naam, en verder geldige gegevens. Je krijgt <code style="color:#f08c00;font-weight:600">400</code> met <code style="color:#845ef7">Een verblijf moet een naam hebben.</code>
7. Verhuis dier 5 naar verblijf 5. Je krijgt <code style="color:#37b24d;font-weight:600">204</code>. <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> api/dier/5/details</code> geeft `"verblijfId": 5` en `"verblijfNaam": "Quarantaine"`. In het overzicht heeft verblijf 3 nu `aantalDieren` `0` en `vrijePlaatsen` `5`, en verblijf 5 `1` en `0`.
8. <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> api/dier/99/details</code> geeft <code style="color:#f08c00;font-weight:600">404</code> met <code style="color:#845ef7">We vonden geen dier met Id 99.</code>
