# 04_03

<b>Hoofdstuk 04</b> &nbsp;·&nbsp; Migraties terugdraaien &nbsp;·&nbsp; PC-onderdelen &nbsp;·&nbsp; niveau <span style="color:#f03e3e">●●●</span> &nbsp;·&nbsp; ±2 uur

## Leerdoel

Na deze oefening kan je een database opbouwen via migraties, en een migratie die al op de database toegepast is weer volledig ongedaan maken.

Je leert wat er in een migratie staat, wat `Up` en `Down` doen, en waarom je een migratie niet zomaar mag weggooien zodra de database ze kent.

Daarnaast zet je dezelfde startgegevens op drie plaatsen tegelijk: in je database, in je lijst in het geheugen en in je bestand. En je zorgt dat je API een tekst weigert die te lang is voor de database.

## 

## Opdracht

De bestellijnen-API van Bit & Byte uit 03_03 kan al wisselen tussen twee repositories: één in het geheugen en één in een JSON-bestand. De database moet de derde worden. Ondertussen twijfelt de boekhouding nog over een nieuw veld voor kortingen.

Jouw taak is om naast je bestaande API een database op te zetten voor de bestellijnen, en een migratie die achteraf een vergissing blijkt, netjes terug te draaien.

In deze oefening:

1. zet je een database op en koppel je je project eraan;
2. maak je de tabel voor de bestellijnen via een migratie;
3. zet je de startgegevens in de database én in je twee repositories;
4. laat je je API weigeren wat de database zou weigeren;
5. voeg je een kolom toe en draai je die volledig terug.

> [!NOTE]
> Je API gebruikt de database in deze les nog niet. Hij blijft werken met de repository die je in Program.cs gekozen hebt. Een derde repository, voor de database, schrijf je in les 05.

Implementeer onderstaande functionaliteiten.

### 1. Je vertrekpunt

Vertrek van je eigen oplossing van 03_03. Alle endpoints uit die oefening blijven werken zoals ze nu werken, met beide repositories.

Vanaf nu ligt het model vast, met deze C#-types:

| Property     | C#-type   |
|--------------|-----------|
| Id           | `int`     |
| Omschrijving | `string`  |
| Aantal       | `int`     |
| StukPrijs    | `decimal` |
| IsActief     | `bool`    |

> [!TIP]
> Was een bedrag bij jou een `double`, maak er dan nu een `decimal` van. Een `double` kan een bedrag als 0,10 niet exact bewaren, een `decimal` wel. Voor geld kies je daarom altijd `decimal`.

---

### 2. De database en de tabel

Gebruik je PostgreSQL-container uit les 04, of start er een. Deze oefening krijgt een eigen database met de naam `BestellijnDb`.

Installeer de packages uit les 04, zet in `appsettings.json` een connectiestring met de naam `PostgresConnection` die naar `BestellijnDb` wijst, en maak in de map Data een klasse `BestellijnContext`. Registreer ze in Program.cs.

Voeg een `DbSet<Bestellijn>` toe met de naam `Bestellijnen`, en leg met de Fluent API vast:

| Property     | Kolom in de database                                    |
|--------------|---------------------------------------------------------|
| Id           | de sleutel, de database telt zelf op                    |
| Omschrijving | tekst van hoogstens 200 tekens, verplicht               |
| Aantal       | geheel getal, verplicht                                 |
| StukPrijs    | getal met 10 cijfers, waarvan 2 na de komma, verplicht  |
| IsActief     | ja of nee, verplicht                                    |

De tabel heet `Bestellijnen`.

Maak een migratie met de naam `BestellijnTabel` en werk de database bij. Controleer in pgAdmin of in VS Code dat elke kolom het juiste type heeft.

---

### 3. De startgegevens

Zet deze zes bestellijnen in de tabel, met een SQL-script dat je zelf schrijft:

| Id | Omschrijving         | Aantal | StukPrijs | IsActief |
|----|----------------------|--------|-----------|----------|
| 1  | Moederbord ASUS B650 | 1      | 249.99    | ja       |
| 2  | DDR5 32 GB kit       | 2      | 109.90    | ja       |
| 3  | NVMe SSD 2 TB        | 1      | 139.00    | ja       |
| 4  | Behuizing midi tower | 1      | 89.95     | nee      |
| 5  | Voeding 750 W        | 1      | 119.00    | ja       |
| 6  | Casefan 120 mm       | 3      | 14.99     | ja       |

Zet de startlijst van je `InMemoryBestellijnRepository` en van je `BestandBestellijnRepository` gelijk aan deze tabel. Verwijder `bestellijnen.json`, zodat je bestandsrepository opnieuw begint met de nieuwe startlijst.

> [!TIP]
> Schrijf de bedragen in je startlijst met twee cijfers na de komma, zoals in de tabel. Een `decimal` onthoudt hoeveel cijfers na de komma je schreef, en dat zie je terug in de JSON: `139m` en `139.00m` zijn even groot, maar je API toont ze anders. De database geeft ze altijd met twee cijfers terug.

> [!WARNING]
> Zet je de Id's zelf in je INSERT, dan weet de teller van PostgreSQL daar niets van. Je merkt dat pas in les 05, bij de eerste POST. Denk dus nu al na over hoe je ervoor zorgt dat de Id's in je tabel overeenkomen met de tabel hierboven. Met SQL mag je daarvoor alles doen wat de gegevens raakt, ook de teller zelf bijstellen. Enkel de structuur van de tabel blijft voor migraties.

<code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> api/bestellijn/fiche</code> geeft nu als resultaat, met welke repository ook:

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

---

### 4. Dezelfde regels

Je database weigert een omschrijving van meer dan 200 tekens. Voeg daarom aan de controles uit 03_03 deze toe, bij toevoegen en bijwerken:

- de omschrijving is langer dan 200 tekens: <code style="color:#f08c00;font-weight:600">400 Bad Request</code> met <code style="color:#845ef7">De omschrijving mag hoogstens 200 tekens lang zijn.</code>

Test het met een omschrijving van precies 200 tekens, die je wel moet kunnen toevoegen, en een van 201 tekens, telkens met verder geldige gegevens. Is een verzoek op meer dan één punt ongeldig, dan kies je zelf welk bericht je teruggeeft.

---

### 5. Alles samen: een migratie terugdraaien

<span style="background:#7048e8;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>SAMENGESTELDE OEFENING</b></span>

Tot nu toe ging een migratie altijd één kant op: van je model naar de database. In dit laatste punt maak je een migratie, voer je ze uit, en draai je ze daarna volledig terug. Je database, je startgegevens en je API moeten dat ongeschonden overleven.

#### De korting erbij

De boekhouding wil per bestellijn een korting kunnen bewaren.

1. Voeg aan het model een property `Korting` toe, van het type `decimal`. In de database wordt het een verplicht getal met 5 cijfers, waarvan 2 na de komma.
2. Maak een migratie met de naam `KortingToegevoegd`. Open ze en zoek in `Up` en in `Down` wat er met de kolom gebeurt.
3. Werk de database bij. Controleer dat de kolom bestaat, welke waarde de zes bestaande lijnen erin gekregen hebben, en dat ze er nog allemaal staan.

#### De korting weer weg

Een dag later beslist de boekhouding anders: een korting hoort bij een hele bestelling, niet bij één lijn. Het veld moet weg, alsof het er nooit geweest is.

4. Breng de database terug naar de toestand van na `BestellijnTabel`.
5. Verwijder de migratie `KortingToegevoegd` uit je project.
6. Haal de property `Korting` weer uit je model.

> [!NOTE]
> Les 04 zegt dat je een migratie die de database al uitvoerde, niet zomaar mag verwijderen: je moet de database eerst terugrollen naar een eerdere versie. Hoe dat moet, toont de les niet. Zoek het op in de documentatie van Entity Framework Core.

> [!IMPORTANT]
> Pas de database niet met de hand aan, en verwijder het migratiebestand niet zelf uit je map Migrations. Houd de volgorde van stap 4, 5 en 6 aan. Op deze drie plaatsen mag geen spoor van de korting overblijven: in de database, in je map Migrations en in je model.

#### Testscenario

Herstart je API voor je begint. Werk je met de bestandsrepository, verwijder dan eerst `bestellijnen.json`, zodat de lijnen uit je test van punt 4 weg zijn.

Controleer na stap 6:

1. De geschiedenis van je migraties in de database bevat enkel nog `BestellijnTabel`.
2. De tabel `Bestellijnen` heeft geen kolom `Korting` meer, en bevat nog altijd dezelfde zes bestellijnen als in punt 3.
3. In je map Migrations staat geen bestand meer met `Korting` in de naam, en ook in het bestand met de momentopname van je model komt `Korting` niet meer voor.
4. Je project compileert, en de fiche geeft nog altijd hetzelfde antwoord als in punt 3.
5. Maak een nieuwe migratie zonder iets aan je model te veranderen. Ze is leeg: `Up` en `Down` doen niets. Verwijder ze daarna weer.

> [!WARNING]
> Is de migratie uit stap 5 van het testscenario niet leeg, dan komen je model en de momentopname in je map Migrations niet overeen. Kijk dan na of je in stap 4 tot en met 6 een van de drie plaatsen vergeten bent.
