# 04_05

<b>Hoofdstuk 04</b> &nbsp;·&nbsp; Twee tabellen, één context &nbsp;·&nbsp; Fietsverhuur &nbsp;·&nbsp; niveau <span style="color:#f08c00">●●○</span> &nbsp;·&nbsp; ±90 min

## Leerdoel

Na deze oefening kan je meer dan één model naar dezelfde database brengen, via één DbContext en meerdere migraties.

Je leert dat de regels in je database en de regels in je API moeten overeenkomen: een tekst die te lang is voor de database, moet je API al eerder netjes weigeren.

Daarnaast zorg je ervoor dat je API en je database hetzelfde verhaal vertellen, zodat de overstap in les 05 niets verandert voor wie je API gebruikt.

## 

## Opdracht

De fietsen-API van Trapdoor uit 03_05 werkt met twee repositories in het geheugen: één voor de fietsen en één voor de verhuringen. Beide moeten later naar een database verhuizen.

Jouw taak is om naast je bestaande API een database op te zetten met een tabel voor de fietsen en een tabel voor de verhuringen, en je API zo aan te passen dat hij dezelfde regels volgt als die database.

In deze oefening:

1. zet je een database op en koppel je je project eraan;
2. leg je de tabel voor de fietsen vast en maak je ze via een migratie;
3. doe je hetzelfde voor de verhuringen, in een tweede migratie;
4. zet je de startgegevens in de database én in je API;
5. laat je je API weigeren wat de database zou weigeren.

> [!NOTE]
> Je API gebruikt de database in deze les nog niet. Hij blijft werken met je twee in-memory repositories. De overstap gebeurt in les 05.

Implementeer onderstaande functionaliteiten.

### 1. Je vertrekpunt

Vertrek van je eigen oplossing van 03_05. Alle endpoints uit die oefening blijven werken zoals ze nu werken, met de regels uit punt 6 van 03_05.

Vanaf nu liggen de modellen vast, met deze C#-types:

| Fiets         | C#-type   |     | Verhuring | C#-type  |
|---------------|-----------|-----|-----------|----------|
| Id            | `int`     |     | Id        | `int`    |
| Type          | `string`  |     | Actie     | `string` |
| Framemaat     | `int`     |     | FietsId   | `int`    |
| PrijsPerDag   | `decimal` |     |           |          |
| IsBeschikbaar | `bool`    |     |           |          |

> [!TIP]
> Was een bedrag bij jou een `double`, maak er dan nu een `decimal` van. Een `double` kan een bedrag als 0,10 niet exact bewaren, een `decimal` wel. Voor geld kies je daarom altijd `decimal`.

---

### 2. De database en de koppeling

Gebruik je PostgreSQL-container uit les 04, of start er een. Deze oefening krijgt een eigen database met de naam `FietsverhuurDb`.

Installeer de packages uit les 04 en zet in `appsettings.json` een connectiestring met de naam `PostgresConnection`, die naar `FietsverhuurDb` wijst.

Maak een map Data, met daarin een klasse `FietsverhuurContext` die erft van `DbContext`, en registreer ze in Program.cs.

---

### 3. De tabel voor de fietsen

Voeg aan je context een `DbSet<Fiets>` toe met de naam `Fietsen`, en leg met de Fluent API vast:

| Property      | Kolom in de database                                   |
|---------------|--------------------------------------------------------|
| Id            | de sleutel, de database telt zelf op                   |
| Type          | tekst van hoogstens 30 tekens, verplicht               |
| Framemaat     | geheel getal, verplicht                                |
| PrijsPerDag   | getal met 6 cijfers, waarvan 2 na de komma, verplicht  |
| IsBeschikbaar | ja of nee, verplicht                                   |

De tabel heet `Fietsen`.

Maak een migratie met de naam `FietsTabel` en werk de database bij.

---

### 4. De tabel voor de verhuringen

Voeg daarna een `DbSet<Verhuring>` toe met de naam `Verhuringen`:

| Property | Kolom in de database                     |
|----------|------------------------------------------|
| Id       | de sleutel, de database telt zelf op     |
| Actie    | tekst van hoogstens 20 tekens, verplicht |
| FietsId  | geheel getal, verplicht                  |

De tabel heet `Verhuringen`.

Maak een tweede migratie met de naam `VerhuringTabel` en werk de database opnieuw bij.

> [!NOTE]
> `FietsId` verwijst naar een fiets, maar de database weet dat nog niet: er is geen relatie tussen de twee tabellen. Relaties komen in les 06. Een verhuring moet bovendien kunnen blijven bestaan voor een fiets die al uit de vloot is.

> [!TIP]
> Open de tweede migratie voor je de database bijwerkt. Ze mag enkel over de tabel `Verhuringen` gaan. Zie je er ook iets van de fietsen in terug, dan is er tussen de twee migraties iets aan je model veranderd.

---

### 5. De startgegevens

Zet deze vier fietsen in de tabel `Fietsen`, met een SQL-script dat je zelf schrijft:

| Id | Type         | Framemaat | PrijsPerDag | IsBeschikbaar |
|----|--------------|-----------|-------------|---------------|
| 1  | stadsfiets   | 54        | 12.50       | ja            |
| 2  | mountainbike | 48        | 18.00       | ja            |
| 3  | e-bike       | 56        | 25.00       | nee           |
| 4  | bakfiets     | 52        | 30.00       | ja            |

De tabel `Verhuringen` blijft leeg.

Zet ook de startlijst van je `InMemoryFietsRepository` gelijk aan deze tabel. Je `InMemoryVerhuringRepository` begint leeg, zoals in 03_05.

> [!TIP]
> Schrijf de bedragen in je startlijst met twee cijfers na de komma, zoals in de tabel. Een `decimal` onthoudt hoeveel cijfers na de komma je schreef, en dat zie je terug in de JSON: `25m` en `25.00m` zijn even groot, maar je API toont ze anders. De database geeft ze altijd met twee cijfers terug.

> [!NOTE]
> De e-bike is niet beschikbaar, en geldt dus als verhuurd, zoals 03_05 het afsprak. Ze heeft geen verhuring in de lijst: ze was al weg voor je API bestond.

> [!WARNING]
> Zet je de Id's zelf in je INSERT, dan weet de teller van PostgreSQL daar niets van. Je merkt dat pas in les 05, bij de eerste POST. Denk dus nu al na over hoe je ervoor zorgt dat de Id's in je tabel overeenkomen met de tabel hierboven. Met SQL mag je daarvoor alles doen wat de gegevens raakt, ook de teller zelf bijstellen. Enkel de structuur van de tabel blijft voor migraties.

---

### 6. Alles samen: twee bronnen, één verhaal

<span style="background:#7048e8;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>SAMENGESTELDE OEFENING</b></span>

Je hebt nu twee plaatsen waar fietsen bestaan: het geheugen van je API en je database. In les 05 vervang je de ene door de andere. Dat lukt enkel als ze vandaag al exact hetzelfde vertellen en dezelfde regels volgen.

#### Dezelfde gegevens

Herstart je API en vergelijk het antwoord op <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> api/fiets</code> met de inhoud van de tabel `Fietsen`. Elk veld van elke fiets moet gelijk zijn.

<code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> api/fiets/3</code> geeft als resultaat:

```json
{
  "id": 3,
  "type": "e-bike",
  "framemaat": 56,
  "prijsPerDag": 25.00,
  "isBeschikbaar": false
}
```

#### Dezelfde regels

Je database weigert een type van meer dan 30 tekens. Doet je API dat niet, dan krijgt de client in les 05 een <code style="color:#f03e3e;font-weight:600">500</code> in plaats van een nette fout.

Voeg daarom aan de controles uit 03_05 deze toe, bij toevoegen, bijwerken en vervangen:

- het type is langer dan 30 tekens: <code style="color:#f08c00;font-weight:600">400 Bad Request</code> met <code style="color:#845ef7">Het type mag hoogstens 30 tekens lang zijn.</code>

Is een verzoek op meer dan één punt ongeldig, dan kies je zelf welk bericht je teruggeeft.

> [!NOTE]
> Ook een bedrag heeft in de database een grens: hoeveel cijfers het mag tellen. Met de prijs van een fiets kom je daar nooit in de buurt, dus die laat je hier rusten.

> [!IMPORTANT]
> Een geweigerde actie verandert niets aan de fiets en laat geen spoor na in de verhuringen, net als in 03_05.

#### Testscenario

Herstart je API, zodat je met de startgegevens en een lege lijst verhuringen begint. Alle fietsen die je in deze reeks doorstuurt, zijn op elk ander punt geldig.

1. Vraag de verhuringen op. De lijst is leeg.
2. Voeg een fiets toe met een type van precies 30 tekens. Je krijgt <code style="color:#37b24d;font-weight:600">201</code>, en de nieuwe fiets is beschikbaar.
3. Voeg een fiets toe met een type van 31 tekens. Je krijgt <code style="color:#f08c00;font-weight:600">400</code> met <code style="color:#845ef7">Het type mag hoogstens 30 tekens lang zijn.</code>
4. Werk fiets 1 bij met een type van 31 tekens. Je krijgt <code style="color:#f08c00;font-weight:600">400</code> met hetzelfde bericht. Vraag fiets 1 op: ze is niet veranderd.
5. Verhuur fiets 1. Je krijgt <code style="color:#37b24d;font-weight:600">204</code>. De verhuringen bevatten één regel: `verhuurd`.
6. Verhuur fiets 3. Je krijgt <code style="color:#f08c00;font-weight:600">400</code>, want de e-bike is al verhuurd.
7. Controleer in je database dat de geschiedenis van je migraties precies twee migraties bevat, `FietsTabel` en `VerhuringTabel`, en dat beide tabellen bestaan met de kolommen uit punt 3 en 4.

> [!NOTE]
> De fiets uit stap 2 en de verhuring uit stap 5 staan enkel in het geheugen van je API, niet in je database. Dat is normaal in deze les. Herstart je API, dan vertellen ze weer hetzelfde verhaal.
