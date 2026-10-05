# 09_01

<b>Hoofdstuk 09</b> &nbsp;·&nbsp; DTO's en Mapster &nbsp;·&nbsp; PC-onderdelen &nbsp;·&nbsp; niveau <span style="color:#37b24d">●○○</span> &nbsp;·&nbsp; ±2 uur

## Leerdoel

Na deze oefening kan je je modellen afschermen van de buitenwereld met DTO's: aparte klassen voor wat je API toont en voor wat ze ontvangt.

Je leert Mapster gebruiken om modellen naar DTO's om te zetten en omgekeerd, met een `MapperProfile` voor wat niet vanzelf overeenkomt.

Daarnaast merk je dat `IgnoreCycles` overbodig wordt, en dat een client die meer meestuurt dan nodig, niets meer kan aanrichten.

## 

## Opdracht

De JSON van de API toont vandaag de binnenkant van de database: lege navigation properties en `[null]`-lijsten. En een POST neemt alles aan wat een model kent. De website wil een API die enkel toont wat ze nodig heeft, en enkel aanneemt wat ze mag sturen.

Jouw taak is om elk endpoint om te bouwen naar DTO's, met Mapster, en de details-endpoints uit 08_01 een eigen, platte vorm te geven.

Via de API moet de website, net als in 08_01, alle artikelen en leveranciers kunnen opvragen, filteren en de fiche en de details tonen, leveranciers kunnen toevoegen, bijwerken en verwijderen, en artikelen bij een andere leverancier kunnen onderbrengen. Daarnaast moet ze:

1. bij elke leverancier in het overzicht meteen zien hoeveel artikelen hij levert, en hoeveel stuks daarvan op voorraad liggen.

Implementeer onderstaande functionaliteiten.

### 1. Je vertrekpunt

Vertrek van je eigen oplossing van 08_01. Alle endpoints blijven werken, met dezelfde routes, dezelfde controles en dezelfde berichten. Enkel hun JSON verandert, zoals hieronder beschreven.

Je modellen en je database veranderen in deze oefening niet. De startgegevens blijven die van 06_01.

Haal de instelling `IgnoreCycles` uit Program.cs weg, zoals les 09 vraagt.

---

### 2. Mapster en de structuur

Installeer Mapster in je project. Maak een map `DTOs`, met daarin een map per model, en een map `Configuration` met een klasse `MapperProfile`, zoals in les 09. Zorg dat Mapster je `MapperProfile` vindt bij het opstarten.

Of je DTO's klassen of records zijn, kies je zelf. Kies je voor records, denk dan na over hoe je de berekende velden invult: een record pas je na het aanmaken niet zomaar meer aan.

> [!IMPORTANT]
> Een DTO bevat nooit een model, en een model nooit een DTO. Een DTO mag wel een andere DTO bevatten, zolang die niet terugverwijst.

---

### 3. De read-DTO's

Elk endpoint dat vroeger een model teruggaf, geeft nu een read-DTO terug. Die heeft dezelfde properties als het model, **zonder de navigation properties**. De `ArtikelFiche` was nooit een tabel in je database: ze is altijd al een antwoord van je API geweest. Verhuis ze daarom naar je map `DTOs`, en laat ze de read-DTO van het artikel bevatten in plaats van het model.

<code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> api/artikel/4</code> geeft nu als resultaat:

```json
{
  "id": 4,
  "naam": "Ryzen 7 7800X3D",
  "merk": "AMD",
  "soort": "processor",
  "prijs": 389.00,
  "aantalOpVoorraad": 0,
  "garantiejaren": 3,
  "leverancierId": 3
}
```

<code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> api/leverancier/3</code> geeft `{ "id": 3, "naam": "ChipCentraal", "land": "Duitsland" }`.

---

### 4. De write-DTO

<span style="background:#2f9e44;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>POST</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/leverancier</code> en <span style="background:#d9822b;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>PUT</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/leverancier/<span style="color:#e64980"><b>{id}</b></span></code> ontvangen voortaan een write-DTO met enkel `naam` en `land`. Een Id of een lijst artikelen in de body negeert je API, gewoon omdat de DTO ze niet kent.

De controles en de berichten blijven dezelfde.

> [!WARNING]
> Les 09 vermeldt data annotations op write-DTO's. Gebruik ze hier niet voor je controles: ze geven een eigen <code style="color:#f08c00;font-weight:600">400</code>, met een ander bericht dan je API nu belooft.

---

### 5. Details, plat en zonder cycli

<span style="background:#1c7ed6;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>GET</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/artikel/<span style="color:#e64980"><b>{id}</b></span>/details</code> geeft het artikel met de naam en het land van zijn leverancier erbij, plat in één object:

```json
{
  "id": 4,
  "naam": "Ryzen 7 7800X3D",
  "merk": "AMD",
  "soort": "processor",
  "prijs": 389.00,
  "aantalOpVoorraad": 0,
  "garantiejaren": 3,
  "leverancierId": 3,
  "leverancierNaam": "ChipCentraal",
  "leverancierLand": "Duitsland"
}
```

<span style="background:#1c7ed6;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>GET</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/leverancier/<span style="color:#e64980"><b>{id}</b></span>/details</code> geeft de leverancier met een korte vorm van zijn artikelen, in `assortiment`:

```json
{
  "id": 3,
  "naam": "ChipCentraal",
  "land": "Duitsland",
  "aantalArtikelen": 2,
  "totaleVoorraad": 3,
  "assortiment": [
    { "id": 3, "naam": "Ryzen 5 7600X", "soort": "processor", "prijs": 229.50 },
    { "id": 4, "naam": "Ryzen 7 7800X3D", "soort": "processor", "prijs": 389.00 }
  ]
}
```

- `aantalArtikelen` is het aantal artikelen van de leverancier;
- `totaleVoorraad` is de som van hun `aantalOpVoorraad`.

De volgorde van de artikelen binnen een leverancier ligt niet vast. De berichten bij een <code style="color:#f08c00;font-weight:600">404</code> blijven die van 08_01.

> [!TIP]
> Les 09 raadt af om te rekenen in je `MapperProfile`. Beslis zelf waar `aantalArtikelen` en `totaleVoorraad` berekend worden, en waarom daar.

---

### 6. Alles samen: het overzicht van alle leveranciers

<span style="background:#7048e8;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>SAMENGESTELDE OEFENING</b></span>

<span style="background:#1c7ed6;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>GET</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/leverancier/details</code> geeft alle leveranciers, op Id gesorteerd, elk in de vorm van punt 5. Je API stuurt daarvoor nog altijd één query naar de database.

Zo komen alle stukken samen: de `Include` uit 08_01, je read-DTO's, je `MapperProfile` en je berekening.

#### Testscenario

Bouw je database opnieuw op, zodat ze exact de startgegevens bevat, en doorloop deze reeks:

1. <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> api/artikel/4</code>, <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> api/leverancier/3</code>, <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> api/artikel/4/details</code> en <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> api/leverancier/3/details</code> geven de JSON uit punt 3 en 5. Nergens staat nog een veld `leverancier` of `artikelen` met `null`.
2. <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> api/artikel/fiche/6</code> geeft dezelfde fiche als in 05_01, met `"leverancierId": 2` in het artikel, en zonder `"leverancier"`.
3. <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> api/leverancier/details</code> geeft de leveranciers 1, 2, 3 en 4, in die volgorde. Leverancier 1 heeft `aantalArtikelen` `2` en `totaleVoorraad` `14`, leverancier 2 `2` en `5`, leverancier 3 `2` en `3`, en leverancier 4 `0` en `0`, met `"assortiment": []`. In je terminal staat bij dit verzoek één query.
4. Voeg deze leverancier toe. Je krijgt <code style="color:#37b24d;font-weight:600">201</code>, met `{ "id": 5, "naam": "Kabelkoning", "land": "België" }`. <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> api/leverancier/5/details</code> geeft `"aantalArtikelen": 0` en `"assortiment": []`.

   ```json
   {
     "id": 99,
     "naam": "Kabelkoning",
     "land": "België",
     "artikelen": [ { "naam": "HDMI-kabel", "merk": "Kabelkoning", "soort": "kabel", "prijs": 9.99 } ]
   }
   ```

5. <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> api/artikel</code> geeft nog altijd zes artikelen.
6. Werk leverancier 2 bij met de naam `Componentenhuis NL` en als land `Nederland`. Je krijgt <code style="color:#37b24d;font-weight:600">204</code>. Werk leverancier 1 bij met een lege naam, en verder geldige gegevens. Je krijgt <code style="color:#f08c00;font-weight:600">400</code> met <code style="color:#845ef7">Een leverancier moet een naam hebben.</code>
7. Breng artikel 1 onder bij leverancier 4. Je krijgt <code style="color:#37b24d;font-weight:600">204</code>. <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> api/artikel/1/details</code> geeft `"leverancierId": 4` en `"leverancierNaam": "Koelwerk"`. In het overzicht heeft leverancier 1 nu `aantalArtikelen` `1` en `totaleVoorraad` `2`, en leverancier 4 `1` en `12`.
8. <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> api/artikel/99/details</code> geeft <code style="color:#f08c00;font-weight:600">404</code> met <code style="color:#845ef7">We vonden geen artikel met Id 99.</code>
