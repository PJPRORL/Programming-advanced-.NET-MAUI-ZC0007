# 04_05

<b>Hoofdstuk 04</b> &nbsp;·&nbsp; CRUD op de database, twee tabellen, in schoolstijl &nbsp;·&nbsp; Fietsverhuur &nbsp;·&nbsp; niveau <span style="color:#f08c00">●●○</span> &nbsp;·&nbsp; ±2 uur

## Leerdoel

Na deze oefening kan je met één DbContext twee tabellen beheren, en elke wijziging van je API echt in de database bewaren.

Je leert toevoegen, bijwerken en verwijderen via de DbContext, een tweede tabel via een tweede migratie toevoegen, en de Id van een nieuw item zelf berekenen, zoals de schoolopgaven het vragen.

Daarnaast zorg je dat de regels van je API en die van je database overeenkomen, en leg je in elk endpoint een logbericht met een vast niveau.

## 

## Opdracht

De fietsen-API van Trapdoor uit 03_05 werkt met twee repositories in het geheugen: één voor de fietsen en één voor de verhuringen. Bij elke herstart zijn de verhuringen weg. De balie wil dat alles blijft, en de nieuwe kassasoftware verwacht de routes en de antwoorden in de stijl van de schoolopgaven, net als de website van Knuffelhof in 04_04.

Jouw taak is om een database op te zetten met een tabel voor de fietsen en een tabel voor de verhuringen, je controller rechtstreeks met die database te laten werken, en je routes en antwoorden om te zetten naar de nieuwe stijl.

Via de API moet de balie, net als in 03_05:

1. alle fietsen en één fiets kunnen opvragen;
2. een fiets kunnen toevoegen, bijwerken, verwijderen en vervangen;
3. een fiets kunnen verhuren en terugbrengen;
4. de verhuringen kunnen nalezen;

en daarnaast:

5. erop kunnen rekenen dat dit alles een herstart overleeft.

Implementeer onderstaande functionaliteiten.

### 1. Je vertrekpunt

Vertrek van je eigen oplossing van 03_05. De regels uit die oefening blijven gelden: de drie controles bij toevoegen, bijwerken en vervangen, de regels voor verhuren en terugbrengen, en de regel dat een verhuurde fiets niet uit de vloot kan.

Vanaf nu liggen de modellen vast, met deze C#-types:

| Fiets         | C#-type   |     | Verhuring | C#-type  |
|---------------|-----------|-----|-----------|----------|
| Id            | `int`     |     | Id        | `int`    |
| Type          | `string`  |     | Actie     | `string` |
| Framemaat     | `int`     |     | FietsId   | `int`    |
| PrijsPerDag   | `double`  |     |           |          |
| IsBeschikbaar | `bool`    |     |           |          |

> [!IMPORTANT]
> Net als in 04_04 volgt de reeks _04 tot en met _06 vanaf nu de stijl van de schoolopgaven: routes zonder `api/`, in het meervoud, en een <code style="color:#f08c00;font-weight:600">404</code> zonder body. Een <code style="color:#f08c00;font-weight:600">400</code> houdt wel zijn reden: die regels zijn van de fietsverhuur zelf, en de schoolopgaven zeggen er niets over.

---

### 2. De database en de context

Gebruik je PostgreSQL-container uit les 04, of start er een. Deze oefening krijgt een eigen database met de naam `FietsverhuurDb`.

Installeer de packages uit les 04 en zet in `appsettings.json` een connectiestring met de naam `PostgresConnection`, die naar `FietsverhuurDb` wijst.

Maak een map Data, met daarin een klasse `FietsverhuurContext` die erft van `DbContext`, en registreer ze in Program.cs.

---

### 3. De tabel voor de fietsen

Voeg aan je context een `DbSet<Fiets>` toe met de naam `Fietsen`, en leg met de Fluent API vast:

| Property      | Kolom in de database |
|---------------|----------------------|
| Id            | de sleutel |
| Type          | tekst van hoogstens 30 tekens, verplicht |
| Framemaat     | geheel getal, verplicht |
| PrijsPerDag   | kommagetal, verplicht |
| IsBeschikbaar | ja of nee, verplicht |

De tabel heet `Fietsen`. Leg met `HasData` deze vier fietsen vast:

| Id | Type         | Framemaat | PrijsPerDag | IsBeschikbaar |
|----|--------------|-----------|-------------|---------------|
| 1  | stadsfiets   | 54        | 12.50       | ja            |
| 2  | mountainbike | 48        | 18.00       | ja            |
| 3  | e-bike       | 56        | 25.00       | nee           |
| 4  | bakfiets     | 52        | 30.00       | ja            |

Fiets 3 is dus verhuurd. Maak een migratie met de naam `InitialCreate` en werk de database bij.

---

### 4. De tabel voor de verhuringen

Voeg daarna een `DbSet<Verhuring>` toe met de naam `Verhuringen`:

| Property | Kolom in de database |
|----------|----------------------|
| Id       | de sleutel |
| Actie    | tekst van hoogstens 20 tekens, verplicht |
| FietsId  | geheel getal, verplicht |

De tabel heet `Verhuringen`, en begint leeg. Maak een tweede migratie met de naam `VerhuringTabel` en werk de database opnieuw bij.

> [!NOTE]
> `FietsId` verwijst naar een fiets, maar de database weet dat nog niet: relaties komen in les 06.

> [!TIP]
> Open de tweede migratie voor je de database bijwerkt. Ze mag enkel over de tabel `Verhuringen` gaan.

---

### 5. De controller op de database, in de nieuwe stijl

Pas je `FietsController` aan: hij krijgt via zijn constructor voortaan je `FietsverhuurContext` binnen, naast zijn `ILogger<FietsController>`, in plaats van zijn twee repositories. Haal de registraties van je repositories weg uit Program.cs. Elk endpoint leest en bewaart rechtstreeks in de tabellen `Fietsen` en `Verhuringen`.

| Endpoint | Antwoord |
|----------|----------|
| <span style="background:#1c7ed6;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>GET</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">fietsen</code> | <code style="color:#37b24d;font-weight:600">200 OK</code> met alle fietsen, op Id gesorteerd |
| <span style="background:#1c7ed6;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>GET</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">fietsen/<span style="color:#e64980"><b>{id}</b></span></code> | <code style="color:#37b24d;font-weight:600">200 OK</code> met de fiets, of <code style="color:#f08c00;font-weight:600">404 Not Found</code> zonder body |
| <span style="background:#2f9e44;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>POST</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">fietsen</code> | <code style="color:#37b24d;font-weight:600">201 Created</code> met de nieuwe fiets en haar adres, of <code style="color:#f08c00;font-weight:600">400 Bad Request</code> met de reden |
| <span style="background:#d9822b;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>PUT</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">fietsen/<span style="color:#e64980"><b>{id}</b></span></code> | <code style="color:#37b24d;font-weight:600">204 No Content</code>, <code style="color:#f08c00;font-weight:600">404 Not Found</code> zonder body, of <code style="color:#f08c00;font-weight:600">400 Bad Request</code> met de reden |
| <span style="background:#e03131;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>DELETE</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">fietsen/<span style="color:#e64980"><b>{id}</b></span></code> | <code style="color:#37b24d;font-weight:600">204 No Content</code>, <code style="color:#f08c00;font-weight:600">404 Not Found</code> zonder body, of <code style="color:#f08c00;font-weight:600">400 Bad Request</code> met de reden |
| <span style="background:#d9822b;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>PUT</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">fietsen/vervang/<span style="color:#e64980"><b>{id}</b></span></code> | <code style="color:#37b24d;font-weight:600">201 Created</code> met de nieuwe fiets en haar adres, <code style="color:#f08c00;font-weight:600">404 Not Found</code> zonder body, of <code style="color:#f08c00;font-weight:600">400 Bad Request</code> met de reden |
| <span style="background:#d9822b;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>PUT</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">fietsen/verhuur/<span style="color:#e64980"><b>{id}</b></span></code> | <code style="color:#37b24d;font-weight:600">204 No Content</code>, <code style="color:#f08c00;font-weight:600">404 Not Found</code> zonder body, of <code style="color:#f08c00;font-weight:600">400 Bad Request</code> met <code style="color:#845ef7">Deze fiets is al verhuurd.</code> |
| <span style="background:#d9822b;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>PUT</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">fietsen/terug/<span style="color:#e64980"><b>{id}</b></span></code> | <code style="color:#37b24d;font-weight:600">204 No Content</code>, <code style="color:#f08c00;font-weight:600">404 Not Found</code> zonder body, of <code style="color:#f08c00;font-weight:600">400 Bad Request</code> met <code style="color:#845ef7">Deze fiets is niet verhuurd.</code> |
| <span style="background:#1c7ed6;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>GET</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">fietsen/verhuringen</code> | <code style="color:#37b24d;font-weight:600">200 OK</code> met alle verhuringen, de oudste eerst |

Bij een toevoeging berekent je API zelf de Id: de hoogste Id in de tabel, plus één. Dat geldt voor een nieuwe fiets en voor een nieuwe verhuring. Een Id die de client meestuurt, telt niet.

> [!IMPORTANT]
> Ook in een lege tabel moet een toevoeging lukken: je tabel `Verhuringen` begint leeg, en de eerste verhuring krijgt Id 1. Vraag je af wat de database antwoordt als je de hoogste Id vraagt van een tabel zonder rijen, en laat je API daar niet op vastlopen.

> [!NOTE]
> Omdat je API de Id zelf berekent, speelt de teller van PostgreSQL hier geen rol, ook niet na je startgegevens met `HasData`. In de reeks _01 tot en met _03 kiest de database de Id. Beide komen voor in de schoolopgaven: volg wat de opgave vraagt.

> [!WARNING]
> Iets toevoegen of aanpassen in de context schrijft nog niets naar de database. Geeft je API <code style="color:#37b24d;font-weight:600">201</code> of <code style="color:#37b24d;font-weight:600">204</code>, maar zie je na een herstart niets terug, kijk dan in les 04 welke methode alles wat de context genoteerd heeft echt bewaart.

Log in elk endpoint, zoals de schoolopgaven het vragen:

- elk verzoek op niveau `Information`;
- elke <code style="color:#f08c00;font-weight:600">404</code> en elke <code style="color:#f08c00;font-weight:600">400</code> op niveau `Warning`, bij een <code style="color:#f08c00;font-weight:600">400</code> met de reden;
- een DELETE op een fiets die niet bestaat op niveau `Error`.

---

### 6. Dezelfde regels

Je database weigert een type van meer dan 30 tekens. Voeg daarom aan de controles uit 03_05 deze toe, bij toevoegen, bijwerken en vervangen:

- het type is langer dan 30 tekens: <code style="color:#f08c00;font-weight:600">400 Bad Request</code> met <code style="color:#845ef7">Het type mag hoogstens 30 tekens lang zijn.</code>

Is een verzoek op meer dan één punt ongeldig, dan kies je zelf welk bericht je teruggeeft. Test ook een type van precies 30 tekens: dat moet lukken.

---

### 7. Alles samen: niets verdwijnt nog

<span style="background:#7048e8;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>SAMENGESTELDE OEFENING</b></span>

De vorige punten verhuizen je fietsen én je verhuringen naar de database, in een nieuwe stijl. In dit laatste punt toon je dat alles blijft: na een herstart vertellen je API en je database nog precies hetzelfde verhaal.

#### Testscenario

Begin met een database die exact de startgegevens bevat: verwijder ze, en werk ze opnieuw bij met je migraties. Doorloop dan deze reeks.

1. <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> fietsen/verhuringen</code> geeft `[]`.
2. Voeg deze fiets toe:

   ```json
   { "id": 50, "type": "racefiets", "framemaat": 58, "prijsPerDag": 22.5, "isBeschikbaar": false }
   ```

   Je krijgt <code style="color:#37b24d;font-weight:600">201</code>, en de nieuwe fiets heeft Id 5 en is beschikbaar. De header `Location` eindigt op `/fietsen/5`; hoofdletters tellen daarbij niet.
3. Voeg dezelfde fiets toe met een type van 31 tekens. Je krijgt <code style="color:#f08c00;font-weight:600">400</code> met <code style="color:#845ef7">Het type mag hoogstens 30 tekens lang zijn.</code>
4. Verhuur fiets 5, verhuur fiets 1, en breng fiets 5 terug. Je krijgt telkens <code style="color:#37b24d;font-weight:600">204</code>.
5. Verwijder fiets 1. Je krijgt <code style="color:#f08c00;font-weight:600">400</code> met <code style="color:#845ef7">Een verhuurde fiets kan je niet uit de vloot halen.</code>
6. Verhuur fiets 3. Je krijgt <code style="color:#f08c00;font-weight:600">400</code> met <code style="color:#845ef7">Deze fiets is al verhuurd.</code>
7. Verwijder fiets 99. Je krijgt <code style="color:#f08c00;font-weight:600">404</code>, en in je terminal staat een `Error`.
8. Stop je API en start hem opnieuw.
9. <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> fietsen/verhuringen</code> geeft als resultaat:

   ```json
   [
     { "id": 1, "actie": "verhuurd", "fietsId": 5 },
     { "id": 2, "actie": "verhuurd", "fietsId": 1 },
     { "id": 3, "actie": "teruggebracht", "fietsId": 5 }
   ]
   ```

10. <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> fietsen</code> geeft de fietsen 1 tot en met 5. Fiets 1 is niet beschikbaar, fiets 5 wel.
11. <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> api/fiets</code> geeft <code style="color:#f08c00;font-weight:600">404</code>: de oude route bestaat niet meer.
12. De tabellen `Fietsen` en `Verhuringen` in je database bevatten precies hetzelfde als je API, en de geschiedenis van je migraties bevat `InitialCreate` en `VerhuringTabel`.

> [!WARNING]
> Staat er in stap 9 een verhuring van een actie die mislukte? Kijk dan waar in je endpoint je de verhuring toevoegt en bewaart: pas na alle controles.
