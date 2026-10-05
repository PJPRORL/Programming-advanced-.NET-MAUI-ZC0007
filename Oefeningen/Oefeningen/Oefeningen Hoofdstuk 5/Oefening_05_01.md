# 05_01

<b>Hoofdstuk 05</b> &nbsp;·&nbsp; Een repository op de database, asynchroon &nbsp;·&nbsp; PC-onderdelen &nbsp;·&nbsp; niveau <span style="color:#37b24d">●○○</span> &nbsp;·&nbsp; ±75 min

## Leerdoel

Na deze oefening kan je een repository schrijven die via Entity Framework Core met een echte database praat, en je API daarop laten overstappen met één regel in Program.cs.

Je leert de hele keten asynchroon maken: je interface, je repository en je controller.

Daarnaast merk je dat een database geen volgorde belooft, tenzij je erom vraagt, en dat je dezelfde LINQ die je op een lijst gebruikte, nu op een tabel loslaat.

## 

## Opdracht

In 04_01 zette je naast je artikel-API een database op met precies dezelfde gegevens als je in-memory repository. Nu neemt de database het over.

Jouw taak is om een repository te schrijven die met de database werkt, de hele keten asynchroon te maken, en je API te laten overstappen zonder dat de website iets merkt.

Via de API moet de website, net als in 04_01:

1. de volledige voorraad kunnen opvragen;
2. één artikel kunnen opvragen op basis van zijn Id;
3. alle artikelen van één soort kunnen opvragen;
4. enkel de artikelen kunnen opvragen die nog op voorraad liggen;
5. alle artikelen van één merk kunnen opvragen;
6. de artikelfiche kunnen opvragen;

en daarnaast:

7. de lijst van alle merken kunnen opvragen.

Implementeer onderstaande functionaliteiten.

### 1. Je vertrekpunt

Vertrek van je eigen oplossing van 04_01. Je database `ArtikelDb` bevat de zes artikelen uit 04_01, met hun garantie.

| Id | Naam                | Merk | Soort      | Prijs  | AantalOpVoorraad | Garantiejaren |
|----|---------------------|------|------------|--------|------------------|---------------|
| 1  | ROG Strix B650-A    | Asus | moederbord | 249.99 | 12               | 3             |
| 2  | MAG B650 Tomahawk   | MSI  | moederbord | 219.00 | 5                | 2             |
| 3  | Ryzen 5 7600X       | AMD  | processor  | 229.50 | 3                | 3             |
| 4  | Ryzen 7 7800X3D     | AMD  | processor  | 389.00 | 0                | 3             |
| 5  | TUF Gaming RTX 4070 | Asus | videokaart | 649.00 | 2                | 3             |
| 6  | Ventus 2X RTX 4060  | MSI  | videokaart | 299.99 | 0                | 2             |

Alle endpoints uit 04_01 moeten na deze oefening nog precies hetzelfde antwoorden: dezelfde routes, dezelfde statuscodes, dezelfde berichten en dezelfde JSON.

---

### 2. Het contract asynchroon

Pas `IArtikelRepository` aan, zodat elke methode een `Task` teruggeeft. Geef elke methode het achtervoegsel `Async`, zoals de les het als conventie vraagt.

> [!NOTE]
> Je `InMemoryArtikelRepository` volgt dit contract niet meer. Zoals in de les mag hij weg.

---

### 3. De repository op de database

Maak in de map Repositories een klasse `ArtikelRepository` die `IArtikelRepository` implementeert.

De repository krijgt je `ArtikelContext` binnen via zijn constructor, en haalt al zijn gegevens uit de tabel `Artikelen`. Gebruik overal de asynchrone methodes van Entity Framework Core.

> [!WARNING]
> Een database belooft geen volgorde, tenzij je erom vraagt. Vandaag komen je artikelen misschien netjes op Id terug, morgen na een wijziging niet meer. Elke lijst die je API teruggeeft, is gesorteerd op Id, zoals ze dat in je in-memory versie altijd was, behalve waar deze opgave een andere volgorde vraagt.

> [!WARNING]
> Niet elke C#-methode kan Entity Framework Core naar SQL vertalen. Krijg je een <code style="color:#f03e3e;font-weight:600">500</code> met een melding dat een expressie *could not be translated*, kijk dan welke methode je in je vergelijking gebruikt, en zoek een vorm die de database begrijpt.

---

### 4. De controller asynchroon

Pas de ArtikelController aan, zodat elk endpoint asynchroon werkt en op de repository wacht.

> [!IMPORTANT]
> Async geldt van de database tot in de controller. Gebruik nergens `.Result` of `.Wait()` om een asynchrone methode toch synchroon te maken, en nergens `async void`.

---

### 5. Wisselen naar de database

Laat ASP.NET Core voortaan je `ArtikelRepository` aanleveren wanneer de controller om een `IArtikelRepository` vraagt. Dat is één regel in Program.cs.

Herstart je API en doe alle verzoeken uit 04_01 opnieuw. Ze moeten dezelfde antwoorden geven als vroeger.

---

### 6. De lijst van alle merken

Voorzie een GET-endpoint op:

<span style="background:#1c7ed6;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>GET</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/artikel/merken</code>

Dit endpoint geeft alle merken uit de tabel terug, elk merk één keer, alfabetisch gesorteerd, met statuscode <code style="color:#37b24d;font-weight:600">200 OK</code>.

<code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> api/artikel/merken</code> geeft als resultaat:

```json
["AMD", "Asus", "MSI"]
```

> [!TIP]
> Kijk naar de LINQ-methodes in les 05, ook naar de voorbeelden in de tabel. Laat al het werk door de database doen, niet door je API.

---

### 7. Alles samen: dezelfde antwoorden, nu uit de database

<span style="background:#7048e8;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>SAMENGESTELDE OEFENING</b></span>

De zes vorige punten vervangen de hele binnenkant van je API: een andere repository, een ander contract, een asynchrone controller. Voor de website mag daarvan niets te merken zijn.

Doe de volgende verzoeken. Elk antwoord moet letter voor letter kloppen.

| Verzoek | Statuscode | Antwoord |
|---------|------------|----------|
| <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> api/artikel</code> | <code style="color:#37b24d;font-weight:600">200</code> | de zes artikelen uit de tabel van punt 1, op Id gesorteerd |
| <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> api/artikel/99</code> | <code style="color:#f08c00;font-weight:600">404</code> | <code style="color:#845ef7">We vonden geen artikel met Id 99.</code> |
| <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> api/artikel/soort/processor</code> | <code style="color:#37b24d;font-weight:600">200</code> | de artikelen met Id 3 en 4 |
| <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> api/artikel/soort/koeler</code> | <code style="color:#f08c00;font-weight:600">404</code> | <code style="color:#845ef7">We vonden geen artikelen van de soort koeler.</code> |
| <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> api/artikel/voorradig</code> | <code style="color:#37b24d;font-weight:600">200</code> | de artikelen met Id 1, 2, 3 en 5 |
| <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> api/artikel/merk/asus</code> | <code style="color:#37b24d;font-weight:600">200</code> | de artikelen met Id 1 en 5 |
| <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> api/artikel/merk/corsair</code> | <code style="color:#f08c00;font-weight:600">404</code> | <code style="color:#845ef7">We vonden geen artikelen van het merk corsair.</code> |

<code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> api/artikel/4</code> geeft als resultaat:

```json
{
  "id": 4,
  "naam": "Ryzen 7 7800X3D",
  "merk": "AMD",
  "soort": "processor",
  "prijs": 389.00,
  "aantalOpVoorraad": 0,
  "garantiejaren": 3
}
```

<code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> api/artikel/fiche/6</code> geeft als resultaat:

```json
{
  "artikel": {
    "id": 6,
    "naam": "Ventus 2X RTX 4060",
    "merk": "MSI",
    "soort": "videokaart",
    "prijs": 299.99,
    "aantalOpVoorraad": 0,
    "garantiejaren": 2
  },
  "isVoorradig": false,
  "aantalVanDezelfdeSoort": 1,
  "isGoedkoopsteVanDeSoort": true
}
```

En voor de fiche van artikel 1: `isVoorradig` is `true`, `aantalVanDezelfdeSoort` is `1` en `isGoedkoopsteVanDeSoort` is `false`.

Controleer tot slot in het terminalvenster van je API:

- of je logberichten er nog staan, met hetzelfde niveau als in 03_01;
- of je bij elk verzoek de SQL ziet die Entity Framework Core naar de database stuurt. Kijk bij het verzoek naar de merken of het sorteren in die SQL staat.

> [!NOTE]
> Vergelijk het met les 05: de controller veranderde enkel omdat het contract asynchroon werd. Dat hij nu met een database praat in plaats van met een lijst, weet hij niet.
