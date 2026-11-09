# 04_03

<b>Hoofdstuk 04</b> &nbsp;·&nbsp; Een API op de database, migraties terugdraaien &nbsp;·&nbsp; PC-onderdelen &nbsp;·&nbsp; niveau <span style="color:#f03e3e">●●●</span> &nbsp;·&nbsp; ±2,5 uur

## Leerdoel

Na deze oefening kan je een API met tien endpoints rechtstreeks op een database laten werken, en een migratie die al op de database toegepast is weer volledig ongedaan maken.

Je leert wat er in een migratie staat, wat `Up` en `Down` doen, en waarom je een migratie niet zomaar mag weggooien zodra de database ze kent.

Daarnaast laat je de database het tel- en rekenwerk doen voor de fiche, en zorg je dat je API een tekst weigert die te lang is voor de database.

## 

## Opdracht

De bestellijnen-API van Bit & Byte uit 03_03 kan wisselen tussen een repository in het geheugen en een repository in een JSON-bestand. De boekhouding wil nu een echte database. Ondertussen twijfelt ze nog over een nieuw veld voor kortingen.

Jouw taak is om een database op te zetten voor de bestellijnen, je controller rechtstreeks met die database te laten werken, en een migratie die achteraf een vergissing blijkt, netjes terug te draaien.

Via de API moet een medewerker, net als in 03_03:

1. de actieve en alle bestellijnen kunnen opvragen, en één bestellijn;
2. een bestellijn kunnen toevoegen, bijwerken, schrappen en herstellen;
3. de totale waarde en de bestelfiche kunnen opvragen;
4. de hele bestelling in één keer kunnen schrappen.

Implementeer onderstaande functionaliteiten.

### 1. Je vertrekpunt

Vertrek van je eigen oplossing van 03_03. Na deze oefening moeten deze endpoints nog precies hetzelfde antwoorden:

| Verzoek | Antwoord |
|---------|----------|
| <span style="background:#1c7ed6;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>GET</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/bestellijn</code> | <code style="color:#37b24d;font-weight:600">200 OK</code> met de actieve bestellijnen, op Id gesorteerd |
| <span style="background:#1c7ed6;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>GET</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/bestellijn/alles</code> | <code style="color:#37b24d;font-weight:600">200 OK</code> met alle bestellijnen, geschrapte inbegrepen, op Id gesorteerd |
| <span style="background:#1c7ed6;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>GET</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/bestellijn/<span style="color:#e64980"><b>{id}</b></span></code> | <code style="color:#37b24d;font-weight:600">200 OK</code> met de lijn, of <code style="color:#f08c00;font-weight:600">404 Not Found</code> met <code style="color:#845ef7">We vonden geen actieve bestellijn met Id <span style="color:#e64980"><b>{id}</b></span>.</code> |
| <span style="background:#2f9e44;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>POST</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/bestellijn</code> | <code style="color:#37b24d;font-weight:600">201 Created</code> met de nieuwe lijn en haar adres, of <code style="color:#f08c00;font-weight:600">400 Bad Request</code> met de reden |
| <span style="background:#d9822b;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>PUT</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/bestellijn/<span style="color:#e64980"><b>{id}</b></span></code> | <code style="color:#37b24d;font-weight:600">204 No Content</code>, <code style="color:#f08c00;font-weight:600">404 Not Found</code>, of <code style="color:#f08c00;font-weight:600">400 Bad Request</code> met de reden |
| <span style="background:#e03131;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>DELETE</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/bestellijn/<span style="color:#e64980"><b>{id}</b></span></code> | <code style="color:#37b24d;font-weight:600">204 No Content</code>, of <code style="color:#f08c00;font-weight:600">404 Not Found</code> met <code style="color:#845ef7">Kan bestellijn met Id <span style="color:#e64980"><b>{id}</b></span> niet schrappen, omdat deze niet gevonden is.</code> |
| <span style="background:#d9822b;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>PUT</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/bestellijn/herstel/<span style="color:#e64980"><b>{id}</b></span></code> | <code style="color:#37b24d;font-weight:600">204 No Content</code>, <code style="color:#f08c00;font-weight:600">404 Not Found</code>, of <code style="color:#f08c00;font-weight:600">400 Bad Request</code> met <code style="color:#845ef7">Deze bestellijn is niet geschrapt.</code> |
| <span style="background:#1c7ed6;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>GET</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/bestellijn/totaal</code> | <code style="color:#37b24d;font-weight:600">200 OK</code> met de totale waarde van de actieve lijnen |
| <span style="background:#1c7ed6;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>GET</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/bestellijn/fiche</code> | <code style="color:#37b24d;font-weight:600">200 OK</code> met de `BestelFiche` |
| <span style="background:#e03131;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>DELETE</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/bestellijn</code> | <code style="color:#37b24d;font-weight:600">200 OK</code> met de bijgewerkte fiche, of <code style="color:#f08c00;font-weight:600">400 Bad Request</code> met <code style="color:#845ef7">Er zijn geen actieve bestellijnen om te schrappen.</code> |

De redenen voor een <code style="color:#f08c00;font-weight:600">400</code> blijven die uit 03_03: <code style="color:#845ef7">Een bestellijn moet een omschrijving hebben.</code>, <code style="color:#845ef7">Het aantal moet minstens 1 zijn.</code> en <code style="color:#845ef7">De stukprijs moet groter zijn dan 0.</code> Schrappen blijft schrappen: een geschrapte lijn verdwijnt niet, ze wordt enkel niet meer actief.

Vanaf nu ligt het model vast, met deze C#-types:

| Property     | C#-type  |
|--------------|----------|
| Id           | `int`    |
| Omschrijving | `string` |
| Aantal       | `int`    |
| StukPrijs    | `double` |
| IsActief     | `bool`   |

---

### 2. De database en de tabel

Gebruik je PostgreSQL-container uit les 04, of start er een. Deze oefening krijgt een eigen database met de naam `BestellijnDb`.

Installeer de packages uit les 04, zet in `appsettings.json` een connectiestring met de naam `PostgresConnection` die naar `BestellijnDb` wijst, en maak in de map Data een klasse `BestellijnContext`. Registreer ze in Program.cs.

Voeg een `DbSet<Bestellijn>` toe met de naam `Bestellijnen`, en leg met de Fluent API vast:

| Property     | Kolom in de database |
|--------------|----------------------|
| Id           | de sleutel, de database telt zelf op |
| Omschrijving | tekst van hoogstens 200 tekens, verplicht |
| Aantal       | geheel getal, verplicht |
| StukPrijs    | kommagetal, verplicht |
| IsActief     | ja of nee, verplicht |

De tabel heet `Bestellijnen`. Leg met `HasData` deze zes bestellijnen vast:

| Id | Omschrijving         | Aantal | StukPrijs | IsActief |
|----|----------------------|--------|-----------|----------|
| 1  | Moederbord ASUS B650 | 1      | 249.99    | ja       |
| 2  | DDR5 32 GB kit       | 2      | 109.90    | ja       |
| 3  | NVMe SSD 2 TB        | 1      | 139.00    | ja       |
| 4  | Behuizing midi tower | 1      | 89.95     | nee      |
| 5  | Voeding 750 W        | 1      | 119.00    | ja       |
| 6  | Casefan 120 mm       | 3      | 14.99     | ja       |

Maak een migratie met de naam `InitialCreate` en werk de database bij. Controleer in pgAdmin of in VS Code dat elke kolom het juiste type heeft, en dat de zes lijnen erin staan.

> [!TIP]
> `HasData` legt je startgegevens vast in je migraties. Dat heeft een gevolg voor je bestandsrepository: `bestellijnen.json` begint niet meer vanzelf met dezelfde lijnen. Je controller gebruikt die repository in deze oefening niet meer; in 05_03 komt ze terug.

---

### 3. De controller op de database

Pas je `BestellijnController` aan: hij krijgt via zijn constructor voortaan je `BestellijnContext` binnen, naast zijn `ILogger<BestellijnController>`, in plaats van een repository. Haal de registratie van je repository weg uit Program.cs. Je klassen voor het geheugen en voor het bestand mogen blijven staan.

Alle tien endpoints werken voortaan rechtstreeks op de tabel `Bestellijnen`, en bewaren elke wijziging echt.

> [!WARNING]
> Startgegevens met een vaste Id vertellen de teller van PostgreSQL niet welke nummers al gebruikt zijn. Je eerste POST geeft dan een <code style="color:#f03e3e;font-weight:600">500</code> over een dubbele sleutel. Lees in je leerboek, hoofdstuk 39 en 31, hoe je dat oplost. Na je migratie moet de eerste lijn die je via de API toevoegt, Id 7 krijgen.

> [!TIP]
> Voor het totaal en de fiche hoef je niet alle lijnen naar je API te halen. Laat de database tellen, optellen en de duurste lijn zoeken. Kijk in je terminal naar de SQL: je ziet dan wat er echt naar de database gaat.

> [!WARNING]
> Een `double` bewaart een bedrag als 109,90 niet helemaal exact. Tel je zulke bedragen op, dan kan er een staartje ver achter de komma ontstaan: `0.1 + 0.2` geeft in C# `0.30000000000000004`. Een test die een bedrag met twee cijfers na de komma verwacht, keurt zo'n staartje af. Rond daarom elke som van bedragen af op twee cijfers na de komma, net voor je API ze teruggeeft. Welke methode van .NET dat doet, zoek je zelf op.

Log zoals in 03_03: elk verzoek op niveau Information, elke <code style="color:#f08c00;font-weight:600">400</code> en <code style="color:#f08c00;font-weight:600">404</code> op niveau Warning. Eén ding verandert, zoals in de schoolopgaven: een DELETE op een lijn die niet bestaat, logt op niveau `Error`.

<code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> api/bestellijn/fiche</code> geeft nu als resultaat:

```json
{
  "aantalActieveLijnen": 5,
  "aantalGeschrapteLijnen": 1,
  "totaalActief": 772.76,
  "totaalGeschrapt": 89.95,
  "duursteActieveLijn": {
    "id": 1,
    "omschrijving": "Moederbord ASUS B650",
    "aantal": 1,
    "stukPrijs": 249.99,
    "isActief": true
  },
  "isLeeg": false
}
```

Controleer daarna:

1. Voeg een lijn toe met omschrijving `Koelpasta`, aantal 1 en stukprijs 7.5. Je krijgt <code style="color:#37b24d;font-weight:600">201</code>, en de lijn heeft Id 7.
2. De fiche telt nu 6 actieve lijnen, met een `totaalActief` van `780.26`.
3. Schrap lijn 7. Je krijgt <code style="color:#37b24d;font-weight:600">204</code>. <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> api/bestellijn/7</code> geeft <code style="color:#f08c00;font-weight:600">404</code> met <code style="color:#845ef7">We vonden geen actieve bestellijn met Id 7.</code>, maar <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> api/bestellijn/alles</code> toont ze nog, met `"isActief": false`.
4. Schrap lijn 99. Je krijgt <code style="color:#f08c00;font-weight:600">404</code>, en in je terminal staat een `Error`.
5. Stop je API en start hem opnieuw. De fiche geeft `totaalActief` `772.76` en `totaalGeschrapt` `97.45`.

---

### 4. Dezelfde regels

Je database weigert een omschrijving van meer dan 200 tekens. Voeg daarom aan de controles uit 03_03 deze toe, bij toevoegen en bijwerken:

- de omschrijving is langer dan 200 tekens: <code style="color:#f08c00;font-weight:600">400 Bad Request</code> met <code style="color:#845ef7">De omschrijving mag hoogstens 200 tekens lang zijn.</code>

Test het met een omschrijving van precies 200 tekens, die je wel moet kunnen toevoegen, en een van 201 tekens, telkens met verder geldige gegevens. Schrap de lijn van 200 tekens daarna weer. Is een verzoek op meer dan één punt ongeldig, dan kies je zelf welk bericht je teruggeeft.

---

### 5. Alles samen: een migratie terugdraaien

<span style="background:#7048e8;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>SAMENGESTELDE OEFENING</b></span>

Tot nu toe ging een migratie altijd één kant op: van je model naar de database. In dit laatste punt maak je een migratie, voer je ze uit, en draai je ze daarna volledig terug. Je database, je gegevens en je API moeten dat ongeschonden overleven.

#### De korting erbij

De boekhouding wil per bestellijn een korting kunnen bewaren.

1. Voeg aan het model een property `Korting` toe, van het type `double`. In de database wordt het een verplicht kommagetal.
2. Maak een migratie met de naam `KortingToegevoegd`. Open ze en zoek in `Up` en in `Down` wat er met de kolom gebeurt.
3. Werk de database bij. Controleer dat de kolom bestaat, welke waarde de bestaande lijnen erin gekregen hebben, en dat ze er nog allemaal staan.

#### De korting weer weg

Een dag later beslist de boekhouding anders: een korting hoort bij een hele bestelling, niet bij één lijn. Het veld moet weg, alsof het er nooit geweest is.

4. Breng de database terug naar de toestand van na `InitialCreate`.
5. Verwijder de migratie `KortingToegevoegd` uit je project.
6. Haal de property `Korting` weer uit je model.

> [!NOTE]
> Les 04 zegt dat je een migratie die de database al uitvoerde, niet zomaar mag verwijderen: je moet de database eerst terugrollen naar een eerdere versie. Hoe dat moet, toont de les niet. Zoek het op in de documentatie van Entity Framework Core.

> [!IMPORTANT]
> Pas de database niet met de hand aan, en verwijder het migratiebestand niet zelf uit je map Migrations. Houd de volgorde van stap 4, 5 en 6 aan. Op deze drie plaatsen mag geen spoor van de korting overblijven: in de database, in je map Migrations en in je model.

#### Testscenario

Noteer voor je aan stap 1 begint wat <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> api/bestellijn/alles</code> en <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> api/bestellijn/fiche</code> teruggeven. Controleer na stap 6:

1. De geschiedenis van je migraties in de database bevat enkel nog `InitialCreate`.
2. De tabel `Bestellijnen` heeft geen kolom `Korting` meer, en bevat nog altijd dezelfde lijnen als voor stap 1, ook lijn 7 uit punt 3.
3. In je map Migrations staat geen bestand meer met `Korting` in de naam, en ook in het bestand met de momentopname van je model komt `Korting` niet meer voor.
4. Je project compileert, en de fiche en de lijst met alle lijnen geven precies hetzelfde antwoord als voor stap 1.
5. Maak een nieuwe migratie zonder iets aan je model te veranderen. Ze is leeg: `Up` en `Down` doen niets. Verwijder ze daarna weer.

> [!WARNING]
> Is de migratie uit stap 5 van het testscenario niet leeg, dan komen je model en de momentopname in je map Migrations niet overeen. Kijk dan na of je in stap 4 tot en met 6 een van de drie plaatsen vergeten bent.
