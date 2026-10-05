# 04_06

<b>Hoofdstuk 04</b> &nbsp;·&nbsp; Migraties terugdraaien &nbsp;·&nbsp; Concertzaal &nbsp;·&nbsp; niveau <span style="color:#f03e3e">●●●</span> &nbsp;·&nbsp; ±2 uur

## Leerdoel

Na deze oefening kan je een database opbouwen via migraties, en een migratie die al op de database toegepast is weer volledig ongedaan maken.

Je leert wat er in een migratie staat, wat `Up` en `Down` doen, en waarom je een migratie niet zomaar mag weggooien zodra de database ze kent.

Daarnaast zet je dezelfde startgegevens op drie plaatsen tegelijk: in je database, in je lijst in het geheugen en in je bestand. En je zorgt dat je API een tekst weigert die te lang is voor de database.

## 

## Opdracht

De ticket-API van De Notenbalk uit 03_06 kan al wisselen tussen twee repositories: één in het geheugen en één in een JSON-bestand. De database moet de derde worden. Ondertussen wil de zaal nog iets uitproberen met prijscategorieën.

Jouw taak is om naast je bestaande API een database op te zetten voor de tickets, en een migratie die achteraf een vergissing blijkt, netjes terug te draaien.

In deze oefening:

1. zet je een database op en koppel je je project eraan;
2. maak je de tabel voor de tickets via een migratie;
3. zet je de startgegevens in de database én in je twee repositories;
4. laat je je API weigeren wat de database zou weigeren;
5. voeg je een kolom toe en draai je die volledig terug.

> [!NOTE]
> Je API gebruikt de database in deze les nog niet. Hij blijft werken met de repository die je in Program.cs gekozen hebt. Een derde repository, voor de database, schrijf je in les 05.

Implementeer onderstaande functionaliteiten.

### 1. Je vertrekpunt

Vertrek van je eigen oplossing van 03_06. Alle endpoints uit die oefening blijven werken zoals ze nu werken, met beide repositories.

Vanaf nu ligt het model vast, met deze C#-types:

| Property      | C#-type   |
|---------------|-----------|
| Id            | `int`     |
| Naam          | `string`  |
| Rij           | `int`     |
| Stoel         | `int`     |
| Prijs         | `decimal` |
| IsGeannuleerd | `bool`    |

> [!TIP]
> Was een bedrag bij jou een `double`, maak er dan nu een `decimal` van. Een `double` kan een bedrag als 0,10 niet exact bewaren, een `decimal` wel. Voor geld kies je daarom altijd `decimal`.

---

### 2. De database en de tabel

Gebruik je PostgreSQL-container uit les 04, of start er een. Deze oefening krijgt een eigen database met de naam `ConcertzaalDb`.

Installeer de packages uit les 04, zet in `appsettings.json` een connectiestring met de naam `PostgresConnection` die naar `ConcertzaalDb` wijst, en maak in de map Data een klasse `ConcertzaalContext`. Registreer ze in Program.cs.

Voeg een `DbSet<Ticket>` toe met de naam `Tickets`, en leg met de Fluent API vast:

| Property      | Kolom in de database                                   |
|---------------|--------------------------------------------------------|
| Id            | de sleutel, de database telt zelf op                   |
| Naam          | tekst van hoogstens 100 tekens, verplicht              |
| Rij           | geheel getal, verplicht                                |
| Stoel         | geheel getal, verplicht                                |
| Prijs         | getal met 6 cijfers, waarvan 2 na de komma, verplicht  |
| IsGeannuleerd | ja of nee, verplicht                                   |

De tabel heet `Tickets`.

Maak een migratie met de naam `TicketTabel` en werk de database bij. Controleer in pgAdmin of in VS Code dat elke kolom het juiste type heeft.

---

### 3. De startgegevens

Zet deze zes tickets in de tabel, met een SQL-script dat je zelf schrijft:

| Id | Naam             | Rij | Stoel | Prijs | IsGeannuleerd |
|----|------------------|-----|-------|-------|---------------|
| 1  | Sofie Peeters    | 1   | 5     | 45.00 | nee           |
| 2  | Tom Janssens     | 1   | 6     | 45.00 | nee           |
| 3  | Lisa Maes        | 3   | 12    | 35.00 | nee           |
| 4  | Karim El Idrissi | 3   | 13    | 35.00 | ja            |
| 5  | Emma Claes       | 7   | 2     | 25.00 | nee           |
| 6  | Noah Willems     | 7   | 3     | 25.00 | nee           |

Zet de startlijst van je `InMemoryTicketRepository` en van je `BestandTicketRepository` gelijk aan deze tabel. Verwijder `tickets.json`, zodat je bestandsrepository opnieuw begint met de nieuwe startlijst.

> [!TIP]
> Schrijf de bedragen in je startlijst met twee cijfers na de komma, zoals in de tabel. Een `decimal` onthoudt hoeveel cijfers na de komma je schreef, en dat zie je terug in de JSON: `35m` en `35.00m` zijn even groot, maar je API toont ze anders. De database geeft ze altijd met twee cijfers terug.

> [!WARNING]
> Zet je de Id's zelf in je INSERT, dan weet de teller van PostgreSQL daar niets van. Je merkt dat pas in les 05, bij de eerste POST. Denk dus nu al na over hoe je ervoor zorgt dat de Id's in je tabel overeenkomen met de tabel hierboven. Met SQL mag je daarvoor alles doen wat de gegevens raakt, ook de teller zelf bijstellen. Enkel de structuur van de tabel blijft voor migraties.

Met welke repository ook, je API antwoordt nu zo:

<code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> api/ticket/opbrengst</code> geeft als resultaat: `175.00`

<code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> api/ticket/3</code> geeft als resultaat:

```json
{
  "id": 3,
  "naam": "Lisa Maes",
  "rij": 3,
  "stoel": 12,
  "prijs": 35.00,
  "isGeannuleerd": false
}
```

<code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> api/ticket/4</code> geeft <code style="color:#f08c00;font-weight:600">404 Not Found</code> met <code style="color:#845ef7">We vonden geen geldig ticket met Id 4.</code>

---

### 4. Dezelfde regels

Je database weigert een naam van meer dan 100 tekens. Voeg daarom aan de controles uit 03_06 deze toe, bij verkopen en wijzigen:

- de naam is langer dan 100 tekens: <code style="color:#f08c00;font-weight:600">400 Bad Request</code> met <code style="color:#845ef7">De naam mag hoogstens 100 tekens lang zijn.</code>

Test het met een naam van precies 100 tekens, die je wel moet kunnen verkopen, en een van 101 tekens, telkens op een vrije plaats en met verder geldige gegevens. Is een verzoek op meer dan één punt ongeldig, dan kies je zelf welk bericht je teruggeeft.

---

### 5. Alles samen: een migratie terugdraaien

<span style="background:#7048e8;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>SAMENGESTELDE OEFENING</b></span>

Tot nu toe ging een migratie altijd één kant op: van je model naar de database. In dit laatste punt maak je een migratie, voer je ze uit, en draai je ze daarna volledig terug. Je database, je startgegevens en je API moeten dat ongeschonden overleven.

#### De categorie erbij

De zaal wil per ticket een prijscategorie bewaren, zoals `parterre` of `balkon`.

1. Voeg aan het model een property `Categorie` toe, van het type `string`. In de database wordt het een verplichte tekst van hoogstens 20 tekens.
2. Maak een migratie met de naam `CategorieToegevoegd`. Open ze en zoek in `Up` en in `Down` wat er met de kolom gebeurt.
3. Werk de database bij. Controleer dat de kolom bestaat, welke waarde de zes bestaande tickets erin gekregen hebben, en dat ze er nog allemaal staan.

#### De categorie weer weg

Een week later beslist de zaal anders: de categorie volgt uit de rij, en hoeft dus niet apart bewaard te worden. Het veld moet weg, alsof het er nooit geweest is.

4. Breng de database terug naar de toestand van na `TicketTabel`.
5. Verwijder de migratie `CategorieToegevoegd` uit je project.
6. Haal de property `Categorie` weer uit je model.

> [!NOTE]
> Les 04 zegt dat je een migratie die de database al uitvoerde, niet zomaar mag verwijderen: je moet de database eerst terugrollen naar een eerdere versie. Hoe dat moet, toont de les niet. Zoek het op in de documentatie van Entity Framework Core.

> [!IMPORTANT]
> Pas de database niet met de hand aan, en verwijder het migratiebestand niet zelf uit je map Migrations. Houd de volgorde van stap 4, 5 en 6 aan. Op deze drie plaatsen mag geen spoor van de categorie overblijven: in de database, in je map Migrations en in je model.

#### Testscenario

Herstart je API voor je begint. Werk je met de bestandsrepository, verwijder dan eerst `tickets.json`, zodat de tickets uit je test van punt 4 weg zijn.

Controleer na stap 6:

1. De geschiedenis van je migraties in de database bevat enkel nog `TicketTabel`.
2. De tabel `Tickets` heeft geen kolom `Categorie` meer, en bevat nog altijd dezelfde zes tickets als in punt 3.
3. In je map Migrations staat geen bestand meer met `Categorie` in de naam, en ook in het bestand met de momentopname van je model komt `Categorie` niet meer voor.
4. Je project compileert, en de opbrengst is nog altijd `175.00`.
5. Maak een nieuwe migratie zonder iets aan je model te veranderen. Ze is leeg: `Up` en `Down` doen niets. Verwijder ze daarna weer.

> [!WARNING]
> Is de migratie uit stap 5 van het testscenario niet leeg, dan komen je model en de momentopname in je map Migrations niet overeen. Kijk dan na of je in stap 4 tot en met 6 een van de drie plaatsen vergeten bent.
