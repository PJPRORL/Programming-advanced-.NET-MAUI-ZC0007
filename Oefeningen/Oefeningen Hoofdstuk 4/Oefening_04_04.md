# 04_04

<b>Hoofdstuk 04</b> &nbsp;·&nbsp; Database en migraties &nbsp;·&nbsp; Dierenasiel &nbsp;·&nbsp; niveau <span style="color:#37b24d">●○○</span> &nbsp;·&nbsp; ±75 min

## Leerdoel

Na deze oefening kan je voor een bestaand model een tabel laten maken in een PostgreSQL-database, met Entity Framework Core en de code-first werkwijze.

Je leert een DbContext opstellen, met de Fluent API vastleggen hoe elke kolom eruitziet, en je model via een migratie naar de database brengen.

Daarnaast doorloop je de vaste cyclus van les 04 twee keer: je past je model aan, maakt een nieuwe migratie, kijkt ze na en werkt de database bij.

## 

## Opdracht

De dieren-API van Knuffelhof uit 03_04 werkt met een repository, maar de dieren leven nog altijd in het geheugen. Voor ze naar een echte database kunnen verhuizen, moet die database er eerst zijn, met een tabel die precies past bij het model.

Jouw taak is om naast je bestaande API een database op te zetten voor de dieren, en die via migraties op te bouwen.

In deze oefening:

1. zet je een PostgreSQL-database op in Docker;
2. koppel je je project aan die database;
3. leg je in een DbContext vast hoe de tabel eruitziet;
4. maak je de tabel via een migratie;
5. zet je de startgegevens in de tabel;
6. breid je het model uit en breng je die wijziging naar de database.

> [!NOTE]
> Je API gebruikt de database in deze les nog niet. Hij blijft werken met je `InMemoryDierRepository`. De overstap gebeurt in les 05, en dan moet alles klaarstaan.

Implementeer onderstaande functionaliteiten.

### 1. Je vertrekpunt

Vertrek van je eigen oplossing van 03_04. Alle endpoints uit die oefening blijven werken zoals ze nu werken.

Vanaf nu ligt het model vast, met deze C#-types:

| Property        | C#-type  |
|-----------------|----------|
| Id              | `int`    |
| Naam            | `string` |
| Soort           | `string` |
| LeeftijdInJaren | `int`    |
| Verblijfsnummer | `int`    |

---

### 2. De database in Docker

Start een PostgreSQL-database in Docker, zoals in les 04. Heb je de container uit de les nog, dan mag je die hergebruiken.

Elke oefening krijgt wel haar eigen database. Deze database heet `AsielDb`.

---

### 3. Je project koppelen

Installeer de packages uit les 04 en zet in `appsettings.json` een connectiestring met de naam `PostgresConnection`, die naar de database `AsielDb` wijst.

---

### 4. De DbContext

Maak een map Data, met daarin een klasse `AsielContext` die erft van `DbContext`. Ze bevat een `DbSet<Dier>` met de naam `Dieren`.

Leg in `OnModelCreating` met de Fluent API vast hoe de tabel eruitziet:

| Property        | Kolom in de database                     |
|-----------------|------------------------------------------|
| Id              | de sleutel, de database telt zelf op     |
| Naam            | tekst van hoogstens 50 tekens, verplicht |
| Soort           | tekst van hoogstens 30 tekens, verplicht |
| LeeftijdInJaren | geheel getal, verplicht                  |
| Verblijfsnummer | geheel getal, verplicht                  |

De tabel heet `Dieren`.

Registreer je `AsielContext` in Program.cs met de PostgreSQL-provider en de connectiestring uit punt 3.

---

### 5. De eerste migratie

Maak een migratie met de naam `DierTabel` en werk daarmee de database bij.

Controleer daarna in pgAdmin of in de PostgreSQL-extensie van VS Code:

- of de database `AsielDb` bestaat, met een tabel `Dieren`;
- of elke kolom het type en de lengte heeft uit de tabel van punt 4;
- of geen enkele kolom leeg mag blijven.

> [!TIP]
> Open de migratie voor je de database bijwerkt. Zie je daar de lengtes en de verplichte kolommen uit je Fluent API terug? Zo niet, dan staan ze ook niet in je database.

---

### 6. De startgegevens

Zet deze zes dieren in de tabel, met een SQL-script dat je zelf schrijft:

| Id | Naam    | Soort   | LeeftijdInJaren | Verblijfsnummer |
|----|---------|---------|-----------------|-----------------|
| 1  | Mimi    | kat     | 3               | 2               |
| 2  | Tommie  | kat     | 1               | 2               |
| 3  | Rex     | hond    | 7               | 5               |
| 4  | Bella   | hond    | 1               | 5               |
| 5  | Flappie | konijn  | 2               | 8               |
| 6  | Kiki    | parkiet | 4               | 9               |

Zet ook de startlijst van je `InMemoryDierRepository` gelijk aan deze tabel. In les 05 moet je API dezelfde gegevens teruggeven, of hij nu uit het geheugen leest of uit de database.

> [!WARNING]
> Krijg je een melding dat je tabel niet bestaat, terwijl je ze in pgAdmin ziet staan? Kijk dan naar de hoofdletters in je SQL. PostgreSQL gaat met namen anders om dan C#.

> [!WARNING]
> Zet je de Id's zelf in je INSERT, dan weet de teller van PostgreSQL daar niets van. Je merkt dat pas in les 05, bij de eerste POST. Denk dus nu al na over hoe je ervoor zorgt dat de Id's in je tabel overeenkomen met de tabel hierboven. Met SQL mag je daarvoor alles doen wat de gegevens raakt, ook de teller zelf bijstellen. Enkel de structuur van de tabel blijft voor migraties.

---

### 7. Alles samen: een kolom erbij

<span style="background:#7048e8;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>SAMENGESTELDE OEFENING</b></span>

De zes vorige punten brachten je model één keer naar de database. Maar een model blijft niet stilstaan. In dit laatste punt doorloop je de hele cyclus opnieuw, en moeten je model, je database, je startgegevens en je API daarna weer op één lijn liggen.

Knuffelhof wil bij elk dier tonen of het al een chip heeft.

Voeg aan het model een property `IsGechipt` toe, van het type `bool`. In de database wordt het een verplichte kolom met ja of nee.

1. Maak een migratie met de naam `ChipToegevoegd`. Open ze en zoek in `Up` de regel die de kolom toevoegt, en in `Down` de regel die ze weer weghaalt.
2. Werk de database bij.
3. Kijk welke waarde de bestaande dieren nu in `IsGechipt` hebben, en verklaar waar die waarde vandaan komt.
4. Vul `IsGechipt` in met een SQL-script:

| Id | IsGechipt |
|----|-----------|
| 1  | ja        |
| 2  | nee       |
| 3  | ja        |
| 4  | ja        |
| 5  | nee       |
| 6  | nee       |

5. Zet dezelfde waarden in de startlijst van je `InMemoryDierRepository`.

Bijvoorbeeld:

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

Controleer tot slot:

- of de geschiedenis van je migraties in de database nu precies twee migraties bevat: `DierTabel` en `ChipToegevoegd`;
- of elk dier in je database dezelfde gegevens heeft als in je API.

> [!IMPORTANT]
> Pas de database nooit met de hand aan. De kolom `IsGechipt` komt er via een migratie, niet via een ALTER TABLE in pgAdmin. Enkel de gegevens zelf zet je er met SQL in.
