# 07_04

<b>Hoofdstuk 07</b> &nbsp;·&nbsp; Eén generieke repository, één Unit of Work &nbsp;·&nbsp; Dierenasiel &nbsp;·&nbsp; niveau <span style="color:#37b24d">●○○</span> &nbsp;·&nbsp; ±90 min

## Leerdoel

Na deze oefening kan je de standaardbewerkingen op een tabel één keer schrijven, in een generieke repository, en ze voor elk model opnieuw gebruiken.

Je leert een specifieke repository laten overerven van de generieke, en al je repositories achter één Unit of Work zetten, met één plaats waar je wijzigingen bewaart.

Daarnaast merk je dat bijwerken via de generieke repository anders werkt dan je gewoon bent: Entity Framework Core volgt de objecten die je ophaalt, en dat heeft gevolgen.

## 

## Opdracht

De API van Knuffelhof werkt, maar de code herhaalt zich: elke repository haalt op, voegt toe en verwijdert op bijna dezelfde manier. En elke controller vraagt zijn eigen repositories aan.

Jouw taak is om je repositories om te bouwen naar een generieke repository met specifieke repositories erbovenop, ze achter een Unit of Work te zetten, en twee nieuwe endpoints toe te voegen die iets bijwerken.

Via de API moet de website, net als in 06_04, alle dieren en verblijven kunnen opvragen, filteren, zoeken en de fiche tonen, en verblijven kunnen toevoegen en verwijderen. Daarnaast moet ze:

1. de gegevens van een verblijf kunnen bijwerken;
2. een dier naar een ander verblijf kunnen verhuizen, zolang daar plaats is.

Implementeer onderstaande functionaliteiten.

### 1. Je vertrekpunt

Vertrek van je eigen oplossing van 06_04. Alle endpoints uit die oefening blijven werken, met dezelfde controles, dezelfde berichten en dezelfde JSON.

Je modellen en je database veranderen in deze oefening niet. De startgegevens blijven die van 06_04.

---

### 2. De generieke repository

Maak in de map Repositories een generieke interface `IGenericRepository<TEntity>` en een klasse `GenericRepository<TEntity>` die ze implementeert, met de vijf bewerkingen uit les 07: alles ophalen, één ophalen op Id, toevoegen, bijwerken en verwijderen.

> [!IMPORTANT]
> Toevoegen, bijwerken en verwijderen bewaren niets. Ze vertellen Entity Framework Core enkel wat er moet gebeuren. Na deze oefening roept geen enkele repository nog zelf `SaveChangesAsync` aan.

---

### 3. De specifieke repositories

Een vraag die de generieke repository niet kan beantwoorden, zoals alle dieren van één soort, hoort in een specifieke repository. Die overerft van de generieke, en voegt enkel toe wat ontbreekt.

Bouw `IDierRepository` en `DierRepository` zo om. Beslis zelf of `Verblijf` een specifieke repository nodig heeft, of dat de generieke volstaat.

> [!TIP]
> Overloop elk endpoint en vraag je af welke repository het antwoord kan geven. Een model dat enkel de vijf standaardbewerkingen nodig heeft, krijgt in les 07 geen eigen repository.

> [!WARNING]
> Lijsten blijven op Id gesorteerd. Een database belooft geen volgorde, tenzij je erom vraagt. Kijk na waar je API die volgorde nog vraagt. Of dat in een specifieke repository gebeurt of in je controller, kies je zelf.

> [!WARNING]
> De waarschuwing over een *object cycle* uit 06_04 blijft gelden. De generieke repository haalt altijd een volledig object op, ook als je enkel wil weten óf het bestaat.

---

### 4. De Unit of Work

Maak een interface `IUnitOfWork` en een klasse `UnitOfWork`, zoals in les 07: een property per repository die je API gebruikt, en één methode `SaveChangesAsync`.

- Program.cs registreert enkel nog je `AsielContext` en je Unit of Work.
- Elke controller krijgt de Unit of Work binnen, en verder enkel zijn logger.

Heeft een repository specifieke methodes, laat de Unit of Work dan het specifieke type teruggeven. Anders kan je controller die methodes niet bereiken.

#### Controle

Maak een migratie met de naam `Controle`, zonder iets aan je modellen te veranderen. Ze moet leeg zijn: `Up` en `Down` doen niets. Verwijder ze daarna weer.

Doorloop daarna het testscenario van 06_04 opnieuw. Elk antwoord moet hetzelfde zijn.

---

### 5. Een verblijf bijwerken

<span style="background:#d9822b;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>PUT</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/verblijf/<span style="color:#e64980"><b>{id}</b></span></code>

Het Id staat in de URL, de nieuwe gegevens staan in de body. Stuurt de client ook een Id in de body, dan telt enkel dat uit de URL.

- Het verblijf bestaat niet: <code style="color:#f08c00;font-weight:600">404 Not Found</code> met <code style="color:#845ef7">Kan geen verblijf bijwerken met Id <span style="color:#e64980"><b>{id}</b></span>, omdat het niet bestaat.</code>
- De naam is leeg: <code style="color:#f08c00;font-weight:600">400 Bad Request</code> met <code style="color:#845ef7">Een verblijf moet een naam hebben.</code>
- Anders: werk het verblijf bij en geef <code style="color:#37b24d;font-weight:600">204 No Content</code> terug.

Een geweigerde wijziging verandert niets aan het verblijf.

> [!WARNING]
> Krijg je een <code style="color:#f03e3e;font-weight:600">500</code>, met in je terminal een melding dat er al een object met hetzelfde Id gevolgd wordt (*already being tracked*)? De laptopcontroller uit les 07 loopt hier zelf op vast: neem hem niet zomaar over. Tel hoeveel objecten met hetzelfde Id je API aan Entity Framework Core geeft.

---

### 6. Alles samen: een dier verhuizen

<span style="background:#7048e8;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>SAMENGESTELDE OEFENING</b></span>

Dieren verhuizen geregeld: naar quarantaine, of naar een rustiger verblijf. Maar in een verblijf kunnen niet meer dieren wonen dan zijn capaciteit toelaat. De generieke repository, de specifieke repositories en de Unit of Work komen samen in één endpoint.

<span style="background:#d9822b;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>PUT</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/dier/<span style="color:#e64980"><b>{id}</b></span>/verblijf/<span style="color:#e64980"><b>{verblijfId}</b></span></code>

Controleer in deze volgorde:

1. het dier bestaat niet: <code style="color:#f08c00;font-weight:600">404 Not Found</code> met <code style="color:#845ef7">We vonden geen dier met Id <span style="color:#e64980"><b>{id}</b></span>.</code>
2. het verblijf bestaat niet: <code style="color:#f08c00;font-weight:600">400 Bad Request</code> met <code style="color:#845ef7">We vonden geen verblijf met Id <span style="color:#e64980"><b>{verblijfId}</b></span>.</code>
3. het dier woont al in dat verblijf: <code style="color:#f08c00;font-weight:600">400 Bad Request</code> met <code style="color:#845ef7">Dit dier woont al in dit verblijf.</code>
4. er wonen al minstens evenveel dieren in dat verblijf als zijn capaciteit toelaat: <code style="color:#f08c00;font-weight:600">400 Bad Request</code> met <code style="color:#845ef7">Dit verblijf is vol.</code>

Anders woont het dier voortaan in dat verblijf, en geef je <code style="color:#37b24d;font-weight:600">204 No Content</code> terug. Verder verandert er niets aan het dier.

#### Testscenario

Bouw je database opnieuw op, zodat ze exact de startgegevens bevat, en doorloop deze reeks:

1. Werk verblijf 2 bij. Je krijgt <code style="color:#37b24d;font-weight:600">204</code>. <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> api/verblijf/2</code> toont de nieuwe naam.

   ```json
   { "naam": "Hondenweide", "capaciteit": 4 }
   ```

2. <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> api/verblijf</code> geeft de verblijven 1, 2, 3, 4 en 5, in die volgorde.
3. Werk verblijf 99 bij. Je krijgt <code style="color:#f08c00;font-weight:600">404</code> met <code style="color:#845ef7">Kan geen verblijf bijwerken met Id 99, omdat het niet bestaat.</code>
4. Werk verblijf 1 bij met een lege naam. Je krijgt <code style="color:#f08c00;font-weight:600">400</code> met <code style="color:#845ef7">Een verblijf moet een naam hebben.</code> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> api/verblijf/1</code> toont nog altijd `Kattenkamer`.
5. Werk verblijf 5 bij met de naam `Quarantaine` en een capaciteit van 1. Verhuis daarna dier 5 naar verblijf 5. Je krijgt telkens <code style="color:#37b24d;font-weight:600">204</code>. <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> api/verblijf/5/dieren</code> geeft enkel dier 5, en <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> api/verblijf/3/dieren</code> geeft `[]`.
6. Verhuis dier 6 naar verblijf 5. Je krijgt <code style="color:#f08c00;font-weight:600">400</code> met <code style="color:#845ef7">Dit verblijf is vol.</code>
7. Doe stap 5 nog eens. Je krijgt <code style="color:#f08c00;font-weight:600">400</code> met <code style="color:#845ef7">Dit dier woont al in dit verblijf.</code> Die controle komt vóór de capaciteit.
8. Verhuis dier 5 naar verblijf 99. Je krijgt <code style="color:#f08c00;font-weight:600">400</code> met <code style="color:#845ef7">We vonden geen verblijf met Id 99.</code> Verhuis dier 99 naar verblijf 1. Je krijgt <code style="color:#f08c00;font-weight:600">404</code> met <code style="color:#845ef7">We vonden geen dier met Id 99.</code>
9. <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> api/dier/5</code> geeft als resultaat:

   ```json
   {
     "id": 5,
     "naam": "Flappie",
     "soort": "konijn",
     "leeftijdInJaren": 2,
     "isGechipt": false,
     "verblijfId": 5,
     "verblijf": null
   }
   ```

10. Verwijder verblijf 3. Je krijgt <code style="color:#37b24d;font-weight:600">204</code>: het is nu leeg.
11. Voeg een verblijf toe met de naam `Opvangkamer` en een capaciteit van 3. Je krijgt <code style="color:#37b24d;font-weight:600">201</code>, en het nieuwe verblijf heeft Id 6.
