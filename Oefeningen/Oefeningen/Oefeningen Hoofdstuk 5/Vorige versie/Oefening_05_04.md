# 05_04

<b>Hoofdstuk 05</b> &nbsp;·&nbsp; Een repository op de database, asynchroon &nbsp;·&nbsp; Dierenasiel &nbsp;·&nbsp; niveau <span style="color:#37b24d">●○○</span> &nbsp;·&nbsp; ±75 min

## Leerdoel

Na deze oefening kan je een repository schrijven die via Entity Framework Core met een echte database praat, en je API daarop laten overstappen met één regel in Program.cs.

Je leert de hele keten asynchroon maken: je interface, je repository en je controller.

Daarnaast merk je dat een database geen volgorde belooft, tenzij je erom vraagt, en dat je dezelfde LINQ die je op een lijst gebruikte, nu op een tabel loslaat.

## 

## Opdracht

In 04_04 zette je naast je dieren-API een database op met precies dezelfde gegevens als je in-memory repository. Nu neemt de database het over.

Jouw taak is om een repository te schrijven die met de database werkt, de hele keten asynchroon te maken, en je API te laten overstappen zonder dat de website iets merkt.

Via de API moet de website, net als in 04_04:

1. alle dieren kunnen opvragen;
2. één dier kunnen opvragen op basis van zijn Id;
3. alle dieren van één soort kunnen opvragen;
4. enkel de jonge dieren kunnen opvragen;
5. dieren kunnen zoeken op een stukje van hun naam;
6. de dierfiche kunnen opvragen;

en daarnaast:

7. de lijst van alle soorten kunnen opvragen.

Implementeer onderstaande functionaliteiten.

### 1. Je vertrekpunt

Vertrek van je eigen oplossing van 04_04. Je database `AsielDb` bevat de zes dieren uit 04_04, met hun chip.

| Id | Naam    | Soort   | LeeftijdInJaren | Verblijfsnummer | IsGechipt |
|----|---------|---------|-----------------|-----------------|-----------|
| 1  | Mimi    | kat     | 3               | 2               | ja        |
| 2  | Tommie  | kat     | 1               | 2               | nee       |
| 3  | Rex     | hond    | 7               | 5               | ja        |
| 4  | Bella   | hond    | 1               | 5               | ja        |
| 5  | Flappie | konijn  | 2               | 8               | nee       |
| 6  | Kiki    | parkiet | 4               | 9               | nee       |

Alle endpoints uit 04_04 moeten na deze oefening nog precies hetzelfde antwoorden: dezelfde routes, dezelfde statuscodes, dezelfde berichten en dezelfde JSON.

---

### 2. Het contract asynchroon

Pas `IDierRepository` aan, zodat elke methode een `Task` teruggeeft. Geef elke methode het achtervoegsel `Async`, zoals de les het als conventie vraagt.

> [!NOTE]
> Je `InMemoryDierRepository` volgt dit contract niet meer. Zoals in de les mag hij weg.

---

### 3. De repository op de database

Maak in de map Repositories een klasse `DierRepository` die `IDierRepository` implementeert.

De repository krijgt je `AsielContext` binnen via zijn constructor, en haalt al zijn gegevens uit de tabel `Dieren`. Gebruik overal de asynchrone methodes van Entity Framework Core.

> [!WARNING]
> Een database belooft geen volgorde, tenzij je erom vraagt. Vandaag komen je dieren misschien netjes op Id terug, morgen na een wijziging niet meer. Elke lijst die je API teruggeeft, is gesorteerd op Id, zoals ze dat in je in-memory versie altijd was, behalve waar deze opgave een andere volgorde vraagt.

> [!WARNING]
> Niet elke C#-methode kan Entity Framework Core naar SQL vertalen. Krijg je een <code style="color:#f03e3e;font-weight:600">500</code> met een melding dat een expressie *could not be translated*, kijk dan welke methode je in je vergelijking gebruikt, en zoek een vorm die de database begrijpt.

---

### 4. De controller asynchroon

Pas de DierController aan, zodat elk endpoint asynchroon werkt en op de repository wacht.

> [!IMPORTANT]
> Async geldt van de database tot in de controller. Gebruik nergens `.Result` of `.Wait()` om een asynchrone methode toch synchroon te maken, en nergens `async void`.

---

### 5. Wisselen naar de database

Laat ASP.NET Core voortaan je `DierRepository` aanleveren wanneer de controller om een `IDierRepository` vraagt. Dat is één regel in Program.cs.

Herstart je API en doe alle verzoeken uit 04_04 opnieuw. Ze moeten dezelfde antwoorden geven als vroeger.

---

### 6. De lijst van alle soorten

Voorzie een GET-endpoint op:

<span style="background:#1c7ed6;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>GET</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/dier/soorten</code>

Dit endpoint geeft alle soorten uit de tabel terug, elke soort één keer, alfabetisch gesorteerd, met statuscode <code style="color:#37b24d;font-weight:600">200 OK</code>.

<code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> api/dier/soorten</code> geeft als resultaat:

```json
["hond", "kat", "konijn", "parkiet"]
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
| <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> api/dier</code> | <code style="color:#37b24d;font-weight:600">200</code> | de zes dieren uit de tabel van punt 1, op Id gesorteerd |
| <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> api/dier/99</code> | <code style="color:#f08c00;font-weight:600">404</code> | <code style="color:#845ef7">We vonden geen dier met Id 99.</code> |
| <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> api/dier/soort/hond</code> | <code style="color:#37b24d;font-weight:600">200</code> | de dieren met Id 3 en 4 |
| <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> api/dier/soort/cavia</code> | <code style="color:#f08c00;font-weight:600">404</code> | <code style="color:#845ef7">We hebben op dit moment geen cavia in het asiel.</code> |
| <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> api/dier/jong</code> | <code style="color:#37b24d;font-weight:600">200</code> | de dieren met Id 2 en 4 |
| <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> api/dier/zoek/MI</code> | <code style="color:#37b24d;font-weight:600">200</code> | de dieren met Id 1 en 2 |
| <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> api/dier/zoek/xyz</code> | <code style="color:#f08c00;font-weight:600">404</code> | <code style="color:#845ef7">We vonden geen dier waarvan de naam xyz bevat.</code> |

<code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> api/dier/4</code> geeft als resultaat:

```json
{
  "id": 4,
  "naam": "Bella",
  "soort": "hond",
  "leeftijdInJaren": 1,
  "verblijfsnummer": 5,
  "isGechipt": true
}
```

<code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> api/dier/fiche/3</code> geeft als resultaat:

```json
{
  "dier": {
    "id": 3,
    "naam": "Rex",
    "soort": "hond",
    "leeftijdInJaren": 7,
    "verblijfsnummer": 5,
    "isGechipt": true
  },
  "isJong": false,
  "aantalVanDezelfdeSoort": 1,
  "isOudsteVanDeSoort": true
}
```

En voor de fiche van dier 2: `isJong` is `true`, `aantalVanDezelfdeSoort` is `1` en `isOudsteVanDeSoort` is `false`.

Controleer tot slot in het terminalvenster van je API:

- of je logberichten er nog staan, met hetzelfde niveau als in 03_04;
- of je bij elk verzoek de SQL ziet die Entity Framework Core naar de database stuurt. Kijk bij het zoeken op naam of het vergelijken zonder hoofdletters in die SQL staat.

> [!NOTE]
> Vergelijk het met les 05: de controller veranderde enkel omdat het contract asynchroon werd. Dat hij nu met een database praat in plaats van met een lijst, weet hij niet.
