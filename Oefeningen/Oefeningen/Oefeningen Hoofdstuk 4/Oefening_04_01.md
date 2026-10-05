# 04_01

<b>Hoofdstuk 04</b> &nbsp;·&nbsp; Database en migraties &nbsp;·&nbsp; PC-onderdelen &nbsp;·&nbsp; niveau <span style="color:#37b24d">●○○</span> &nbsp;·&nbsp; ±75 min

## Leerdoel

Na deze oefening kan je voor een bestaand model een tabel laten maken in een PostgreSQL-database, met Entity Framework Core en de code-first werkwijze.

Je leert een DbContext opstellen, met de Fluent API vastleggen hoe elke kolom eruitziet, en je model via een migratie naar de database brengen.

Daarnaast doorloop je de vaste cyclus van les 04 twee keer: je past je model aan, maakt een nieuwe migratie, kijkt ze na en werkt de database bij.

## 

## Opdracht

De artikel-API van Bit & Byte uit 03_01 werkt met een repository, maar de artikelen leven nog altijd in het geheugen. Voor ze naar een echte database kunnen verhuizen, moet die database er eerst zijn, met een tabel die precies past bij het model.

Jouw taak is om naast je bestaande API een database op te zetten voor de artikelen, en die via migraties op te bouwen.

In deze oefening:

1. zet je een PostgreSQL-database op in Docker;
2. koppel je je project aan die database;
3. leg je in een DbContext vast hoe de tabel eruitziet;
4. maak je de tabel via een migratie;
5. zet je de startgegevens in de tabel;
6. breid je het model uit en breng je die wijziging naar de database.

> [!NOTE]
> Je API gebruikt de database in deze les nog niet. Hij blijft werken met je `InMemoryArtikelRepository`. De overstap gebeurt in les 05, en dan moet alles klaarstaan.

Implementeer onderstaande functionaliteiten.

### 1. Je vertrekpunt

Vertrek van je eigen oplossing van 03_01. Alle endpoints uit die oefening blijven werken zoals ze nu werken.

Vanaf nu ligt het model vast, met deze C#-types:

| Property         | C#-type   |
|------------------|-----------|
| Id               | `int`     |
| Naam             | `string`  |
| Merk             | `string`  |
| Soort            | `string`  |
| Prijs            | `decimal` |
| AantalOpVoorraad | `int`     |

> [!TIP]
> Was `Prijs` bij jou een `double`, maak er dan nu een `decimal` van. Een `double` kan een bedrag als 0,10 niet exact bewaren, een `decimal` wel. Voor geld kies je daarom altijd `decimal`.

---

### 2. De database in Docker

Start een PostgreSQL-database in Docker, zoals in les 04. Heb je de container uit de les nog, dan mag je die hergebruiken.

Elke oefening krijgt wel haar eigen database. Deze database heet `ArtikelDb`.

---

### 3. Je project koppelen

Installeer de packages uit les 04 en zet in `appsettings.json` een connectiestring met de naam `PostgresConnection`, die naar de database `ArtikelDb` wijst.

---

### 4. De DbContext

Maak een map Data, met daarin een klasse `ArtikelContext` die erft van `DbContext`. Ze bevat een `DbSet<Artikel>` met de naam `Artikelen`.

Leg in `OnModelCreating` met de Fluent API vast hoe de tabel eruitziet:

| Property         | Kolom in de database                    |
|------------------|-----------------------------------------|
| Id               | de sleutel, de database telt zelf op    |
| Naam             | tekst van hoogstens 100 tekens, verplicht |
| Merk             | tekst van hoogstens 50 tekens, verplicht  |
| Soort            | tekst van hoogstens 30 tekens, verplicht  |
| Prijs            | getal met 10 cijfers, waarvan 2 na de komma, verplicht |
| AantalOpVoorraad | geheel getal, verplicht                 |

De tabel heet `Artikelen`.

Registreer je `ArtikelContext` in Program.cs met de PostgreSQL-provider en de connectiestring uit punt 3.

---

### 5. De eerste migratie

Maak een migratie met de naam `ArtikelTabel` en werk daarmee de database bij.

Controleer daarna in pgAdmin of in de PostgreSQL-extensie van VS Code:

- of de database `ArtikelDb` bestaat, met een tabel `Artikelen`;
- of elke kolom het type en de lengte heeft uit de tabel van punt 4;
- of geen enkele kolom leeg mag blijven.

> [!TIP]
> Open de migratie voor je de database bijwerkt. Zie je daar de lengtes en de verplichte kolommen uit je Fluent API terug? Zo niet, dan staan ze ook niet in je database.

---

### 6. De startgegevens

Zet deze zes artikelen in de tabel, met een SQL-script dat je zelf schrijft:

| Id | Naam                | Merk | Soort      | Prijs  | AantalOpVoorraad |
|----|---------------------|------|------------|--------|------------------|
| 1  | ROG Strix B650-A    | Asus | moederbord | 249.99 | 12               |
| 2  | MAG B650 Tomahawk   | MSI  | moederbord | 219.00 | 5                |
| 3  | Ryzen 5 7600X       | AMD  | processor  | 229.50 | 3                |
| 4  | Ryzen 7 7800X3D     | AMD  | processor  | 389.00 | 0                |
| 5  | TUF Gaming RTX 4070 | Asus | videokaart | 649.00 | 2                |
| 6  | Ventus 2X RTX 4060  | MSI  | videokaart | 299.99 | 0                |

Zet ook de startlijst van je `InMemoryArtikelRepository` gelijk aan deze tabel. In les 05 moet je API dezelfde gegevens teruggeven, of hij nu uit het geheugen leest of uit de database.

> [!TIP]
> Schrijf de bedragen in je startlijst met twee cijfers na de komma, zoals in de tabel. Een `decimal` onthoudt hoeveel cijfers na de komma je schreef, en dat zie je terug in de JSON: `389m` en `389.00m` zijn even groot, maar je API toont ze anders. De database geeft ze altijd met twee cijfers terug.

> [!WARNING]
> Krijg je een melding dat je tabel niet bestaat, terwijl je ze in pgAdmin ziet staan? Kijk dan naar de hoofdletters in je SQL. PostgreSQL gaat met namen anders om dan C#.

> [!WARNING]
> Zet je de Id's zelf in je INSERT, dan weet de teller van PostgreSQL daar niets van. Je merkt dat pas in les 05, bij de eerste POST. Denk dus nu al na over hoe je ervoor zorgt dat de Id's in je tabel overeenkomen met de tabel hierboven. Met SQL mag je daarvoor alles doen wat de gegevens raakt, ook de teller zelf bijstellen. Enkel de structuur van de tabel blijft voor migraties.

---

### 7. Alles samen: een kolom erbij

<span style="background:#7048e8;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>SAMENGESTELDE OEFENING</b></span>

De zes vorige punten brachten je model één keer naar de database. Maar een model blijft niet stilstaan. In dit laatste punt doorloop je de hele cyclus opnieuw, en moeten je model, je database, je startgegevens en je API daarna weer op één lijn liggen.

Bit & Byte wil bij elk artikel tonen hoeveel jaar garantie de klant krijgt.

Voeg aan het model een property `Garantiejaren` toe, van het type `int`. In de database wordt het een verplicht geheel getal.

1. Maak een migratie met de naam `GarantieToegevoegd`. Open ze en zoek in `Up` de regel die de kolom toevoegt, en in `Down` de regel die ze weer weghaalt.
2. Werk de database bij.
3. Kijk welke waarde de bestaande artikelen nu in `Garantiejaren` hebben, en verklaar waar die waarde vandaan komt.
4. Vul de garantie in met een SQL-script:

| Id | Garantiejaren |
|----|---------------|
| 1  | 3             |
| 2  | 2             |
| 3  | 3             |
| 4  | 3             |
| 5  | 3             |
| 6  | 2             |

5. Zet dezelfde garantie in de startlijst van je `InMemoryArtikelRepository`.

Bijvoorbeeld:

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

Controleer tot slot:

- of de geschiedenis van je migraties in de database nu precies twee migraties bevat: `ArtikelTabel` en `GarantieToegevoegd`;
- of elk artikel in je database dezelfde gegevens heeft als in je API.

> [!IMPORTANT]
> Pas de database nooit met de hand aan. De kolom `Garantiejaren` komt er via een migratie, niet via een ALTER TABLE in pgAdmin. Enkel de gegevens zelf zet je er met SQL in.
