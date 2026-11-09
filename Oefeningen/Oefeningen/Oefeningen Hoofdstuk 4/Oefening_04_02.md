# 04_02

<b>Hoofdstuk 04</b> &nbsp;·&nbsp; CRUD op de database, twee tabellen &nbsp;·&nbsp; PC-onderdelen &nbsp;·&nbsp; niveau <span style="color:#f08c00">●●○</span> &nbsp;·&nbsp; ±2 uur

## Leerdoel

Na deze oefening kan je met één DbContext twee tabellen beheren, en elke wijziging van je API echt in de database bewaren.

Je leert toevoegen, bijwerken en verwijderen via de DbContext, een tweede tabel via een tweede migratie toevoegen, en zorgen dat de teller van PostgreSQL klopt na startgegevens met een vaste Id.

Daarnaast zorg je dat de regels van je API en die van je database overeenkomen: wat de database zou weigeren, weigert je API al eerder, en netjes.

## 

## Opdracht

De ventilator-API van Bit & Byte uit 03_02 werkt met twee repositories in het geheugen: één voor de ventilatoren en één voor de historiek. Bij elke herstart is de historiek leeg en is elke nieuwe ventilator weg. De inkoopafdeling wil dat alles blijft.

Jouw taak is om een database op te zetten met een tabel voor de ventilatoren en een tabel voor de wijzigingen, en je controller zijn gegevens voortaan rechtstreeks in die database te laten lezen en bewaren.

Via de API moet de inkoopafdeling, net als in 03_02:

1. alle ventilatoren en één ventilator kunnen opvragen;
2. een ventilator kunnen toevoegen, bijwerken, verwijderen en vervangen;
3. de historiek kunnen nalezen;

en daarnaast:

4. erop kunnen rekenen dat dit alles een herstart overleeft.

Implementeer onderstaande functionaliteiten.

### 1. Je vertrekpunt

Vertrek van je eigen oplossing van 03_02. Na deze oefening moeten deze endpoints nog precies hetzelfde antwoorden:

| Verzoek | Antwoord |
|---------|----------|
| <span style="background:#1c7ed6;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>GET</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/ventilator</code> | <code style="color:#37b24d;font-weight:600">200 OK</code> met de volledige lijst, op Id gesorteerd |
| <span style="background:#1c7ed6;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>GET</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/ventilator/<span style="color:#e64980"><b>{id}</b></span></code> | <code style="color:#37b24d;font-weight:600">200 OK</code> met de ventilator, of <code style="color:#f08c00;font-weight:600">404 Not Found</code> met <code style="color:#845ef7">We vonden geen ventilator met Id <span style="color:#e64980"><b>{id}</b></span>.</code> |
| <span style="background:#2f9e44;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>POST</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/ventilator</code> | <code style="color:#37b24d;font-weight:600">201 Created</code> met de nieuwe ventilator en zijn adres, of <code style="color:#f08c00;font-weight:600">400 Bad Request</code> met de reden |
| <span style="background:#d9822b;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>PUT</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/ventilator/<span style="color:#e64980"><b>{id}</b></span></code> | <code style="color:#37b24d;font-weight:600">204 No Content</code>, <code style="color:#f08c00;font-weight:600">404 Not Found</code> met <code style="color:#845ef7">Kan geen ventilator bijwerken met Id <span style="color:#e64980"><b>{id}</b></span>, omdat deze niet bestaat.</code>, of <code style="color:#f08c00;font-weight:600">400 Bad Request</code> met de reden |
| <span style="background:#e03131;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>DELETE</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/ventilator/<span style="color:#e64980"><b>{id}</b></span></code> | <code style="color:#37b24d;font-weight:600">204 No Content</code>, of <code style="color:#f08c00;font-weight:600">404 Not Found</code> met <code style="color:#845ef7">Kan ventilator met Id <span style="color:#e64980"><b>{id}</b></span> niet verwijderen, omdat deze niet gevonden is.</code> |
| <span style="background:#d9822b;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>PUT</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/ventilator/vervang/<span style="color:#e64980"><b>{id}</b></span></code> | <code style="color:#37b24d;font-weight:600">201 Created</code> met de nieuwe ventilator en zijn adres, <code style="color:#f08c00;font-weight:600">404 Not Found</code> met <code style="color:#845ef7">Kan ventilator met Id <span style="color:#e64980"><b>{id}</b></span> niet vervangen, omdat deze niet gevonden is.</code>, of <code style="color:#f08c00;font-weight:600">400 Bad Request</code> met de reden |
| <span style="background:#1c7ed6;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>GET</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/ventilator/historiek</code> | <code style="color:#37b24d;font-weight:600">200 OK</code> met alle wijzigingen, de oudste eerst |

De redenen voor een <code style="color:#f08c00;font-weight:600">400</code> blijven die uit 03_02: <code style="color:#845ef7">Een ventilator moet een merk hebben.</code>, <code style="color:#845ef7">We verkopen enkel ventilatoren van 120 of 140 mm.</code> en <code style="color:#845ef7">De prijs moet groter zijn dan 0.</code> Ook de regels voor de historiek blijven dezelfde: na elke geslaagde actie een wijziging, een vervanging geeft `verwijderd` en daarna `toegevoegd`, en een actie die mislukt laat geen spoor na.

Vanaf nu liggen de modellen vast, met deze C#-types:

| Ventilator   | C#-type   |     | Wijziging    | C#-type  |
|--------------|-----------|-----|--------------|----------|
| Id           | `int`     |     | Id           | `int`    |
| Merk         | `string`  |     | Actie        | `string` |
| AfmetingInMm | `int`     |     | VentilatorId | `int`    |
| Verlichting  | `string`  |     |              |          |
| Prijs        | `double`  |     |              |          |

---

### 2. De database en de context

Gebruik je PostgreSQL-container uit les 04, of start er een. Deze oefening krijgt een eigen database met de naam `VentilatorDb`.

Installeer de packages uit les 04 en zet in `appsettings.json` een connectiestring met de naam `PostgresConnection`, die naar `VentilatorDb` wijst.

Maak een map Data, met daarin een klasse `VentilatorContext` die erft van `DbContext`, en registreer ze in Program.cs.

---

### 3. De tabel voor de ventilatoren

Voeg aan je context een `DbSet<Ventilator>` toe met de naam `Ventilatoren`, en leg met de Fluent API vast:

| Property     | Kolom in de database |
|--------------|----------------------|
| Id           | de sleutel, de database telt zelf op |
| Merk         | tekst van hoogstens 50 tekens, verplicht |
| AfmetingInMm | geheel getal, verplicht |
| Verlichting  | tekst van hoogstens 20 tekens, verplicht |
| Prijs        | kommagetal, verplicht |

De tabel heet `Ventilatoren`. Leg met `HasData` deze vier ventilatoren vast:

| Id | Merk      | AfmetingInMm | Verlichting | Prijs |
|----|-----------|--------------|-------------|-------|
| 1  | Noctua    | 120          | geen        | 29.90 |
| 2  | be quiet! | 140          | geen        | 24.90 |
| 3  | Corsair   | 120          | RGB         | 34.90 |
| 4  | Arctic    | 140          | ARGB        | 19.99 |

Maak een migratie met de naam `InitialCreate` en werk de database bij.

---

### 4. De tabel voor de wijzigingen

Voeg daarna een `DbSet<Wijziging>` toe met de naam `Wijzigingen`:

| Property     | Kolom in de database |
|--------------|----------------------|
| Id           | de sleutel, de database telt zelf op |
| Actie        | tekst van hoogstens 20 tekens, verplicht |
| VentilatorId | geheel getal, verplicht |

De tabel heet `Wijzigingen`, en begint leeg.

Maak een tweede migratie met de naam `WijzigingTabel` en werk de database opnieuw bij.

> [!NOTE]
> `VentilatorId` verwijst naar een ventilator, maar de database weet dat nog niet: er is geen relatie tussen de twee tabellen. Relaties komen in les 06. Een wijziging moet bovendien kunnen blijven bestaan voor een ventilator die al verwijderd is.

> [!TIP]
> Open de tweede migratie voor je de database bijwerkt. Ze mag enkel over de tabel `Wijzigingen` gaan. Zie je er ook iets van de ventilatoren in terug, dan is er tussen de twee migraties iets aan je model veranderd.

---

### 5. De controller op de database

Pas je `VentilatorController` aan: hij krijgt via zijn constructor voortaan je `VentilatorContext` binnen, naast zijn `ILogger<VentilatorController>`, in plaats van zijn twee repositories. Elk endpoint leest en bewaart rechtstreeks in de tabellen `Ventilatoren` en `Wijzigingen`. Haal de registraties van je repositories weg uit Program.cs.

De database bepaalt vanaf nu de Id van een nieuwe ventilator en van een nieuwe wijziging. Een Id die de client meestuurt, telt niet.

> [!WARNING]
> Een wijziging toevoegen of een ventilator aanpassen in de context schrijft nog niets naar de database. Geeft je API <code style="color:#37b24d;font-weight:600">201</code> of <code style="color:#37b24d;font-weight:600">204</code>, maar zie je na een herstart niets terug, kijk dan in les 04 welke methode alles wat de context genoteerd heeft echt bewaart.

> [!WARNING]
> Startgegevens met een vaste Id vertellen de teller van PostgreSQL niet welke nummers al gebruikt zijn. Je eerste POST geeft dan een <code style="color:#f03e3e;font-weight:600">500</code> over een dubbele sleutel. Lees in je leerboek, hoofdstuk 39 en 31, hoe je dat oplost. Na je migraties moet de eerste ventilator die je via de API toevoegt, Id 5 krijgen.

Log zoals in 03_02: elk verzoek op niveau Information, elke <code style="color:#f08c00;font-weight:600">400</code> en <code style="color:#f08c00;font-weight:600">404</code> op niveau Warning, met de reden in het bericht. Eén ding verandert, zoals in de schoolopgaven: een DELETE die niets vindt, logt op niveau `Error`.

---

### 6. Dezelfde regels

Je database weigert een merk van meer dan 50 tekens en een verlichting van meer dan 20 tekens. Doet je API dat niet zelf, dan krijgt de client een <code style="color:#f03e3e;font-weight:600">500</code> in plaats van een nette fout.

Voeg daarom aan de controles uit 03_02 deze twee toe, bij toevoegen, bijwerken en vervangen:

- het merk is langer dan 50 tekens: <code style="color:#f08c00;font-weight:600">400 Bad Request</code> met <code style="color:#845ef7">Het merk mag hoogstens 50 tekens lang zijn.</code>
- de verlichting is langer dan 20 tekens: <code style="color:#f08c00;font-weight:600">400 Bad Request</code> met <code style="color:#845ef7">De verlichting mag hoogstens 20 tekens lang zijn.</code>

Een lege verlichting blijft toegelaten, zoals in 03_02. Is een verzoek op meer dan één punt ongeldig, dan kies je zelf welk bericht je teruggeeft. Test ook een merk van precies 50 tekens: dat moet lukken.

> [!NOTE]
> Een `double` heeft in de database geen grens zoals een tekst die heeft. Voor de prijs hoef je hier dus niets extra te controleren.

---

### 7. Alles samen: niets verdwijnt nog

<span style="background:#7048e8;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>SAMENGESTELDE OEFENING</b></span>

De vorige punten verhuizen je ventilatoren én je historiek naar de database. In dit laatste punt toon je dat alles blijft: na een herstart vertellen je API en je database nog precies hetzelfde verhaal.

#### Testscenario

Begin met een database die exact de startgegevens bevat: verwijder ze, en werk ze opnieuw bij met je migraties. Doorloop dan deze reeks.

1. Vraag de historiek op. Ze is leeg: `[]`.
2. Voeg deze ventilator toe:

   ```json
   { "id": 0, "merk": "Arctic", "afmetingInMm": 120, "verlichting": "geen", "prijs": 14.9 }
   ```

   Je krijgt <code style="color:#37b24d;font-weight:600">201</code>, en de nieuwe ventilator heeft Id 5. De header `Location` eindigt op `/api/ventilator/5`; hoofdletters tellen daarbij niet.
3. Voeg dezelfde ventilator toe met een merk van 51 tekens. Je krijgt <code style="color:#f08c00;font-weight:600">400</code> met <code style="color:#845ef7">Het merk mag hoogstens 50 tekens lang zijn.</code>
4. Werk ventilator 1 bij met een verlichting van 21 tekens. Je krijgt <code style="color:#f08c00;font-weight:600">400</code> met <code style="color:#845ef7">De verlichting mag hoogstens 20 tekens lang zijn.</code> Ventilator 1 is niet veranderd.
5. Werk ventilator 5 bij met dezelfde gegevens als in stap 2, maar een prijs van 12.9. Je krijgt <code style="color:#37b24d;font-weight:600">204</code>.
6. Vervang ventilator 2 door deze ventilator:

   ```json
   { "merk": "be quiet!", "afmetingInMm": 140, "verlichting": "ARGB", "prijs": 26.9 }
   ```

   Je krijgt <code style="color:#37b24d;font-weight:600">201</code>, en de nieuwe ventilator heeft Id 6.
7. Verwijder ventilator 99. Je krijgt <code style="color:#f08c00;font-weight:600">404</code>, en in je terminal staat een `Error`.
8. Stop je API en start hem opnieuw.
9. <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> api/ventilator/historiek</code> geeft als resultaat:

   ```json
   [
     { "id": 1, "actie": "toegevoegd", "ventilatorId": 5 },
     { "id": 2, "actie": "bijgewerkt", "ventilatorId": 5 },
     { "id": 3, "actie": "verwijderd", "ventilatorId": 2 },
     { "id": 4, "actie": "toegevoegd", "ventilatorId": 6 }
   ]
   ```

10. <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> api/ventilator</code> geeft de ventilatoren met Id 1, 3, 4, 5 en 6, in die volgorde. Ventilator 5 kost `12.9`.
11. De tabellen `Ventilatoren` en `Wijzigingen` in je database bevatten precies hetzelfde als je API, en de geschiedenis van je migraties bevat `InitialCreate` en `WijzigingTabel`.

> [!WARNING]
> Staat in stap 9 een wijziging van een actie die mislukte, of ontbreekt er een? Kijk dan waar in je endpoint je de wijziging toevoegt en bewaart: pas na alle controles, en pas als de actie zelf gelukt is.
