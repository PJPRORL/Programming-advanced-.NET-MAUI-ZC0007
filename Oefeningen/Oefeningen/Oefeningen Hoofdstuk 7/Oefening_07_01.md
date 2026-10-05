# 07_01

<b>Hoofdstuk 07</b> &nbsp;·&nbsp; Eén generieke repository, één Unit of Work &nbsp;·&nbsp; PC-onderdelen &nbsp;·&nbsp; niveau <span style="color:#37b24d">●○○</span> &nbsp;·&nbsp; ±90 min

## Leerdoel

Na deze oefening kan je de standaardbewerkingen op een tabel één keer schrijven, in een generieke repository, en ze voor elk model opnieuw gebruiken.

Je leert een specifieke repository laten overerven van de generieke, en al je repositories achter één Unit of Work zetten, met één plaats waar je wijzigingen bewaart.

Daarnaast merk je dat bijwerken via de generieke repository anders werkt dan je gewoon bent: Entity Framework Core volgt de objecten die je ophaalt, en dat heeft gevolgen.

## 

## Opdracht

De API van Bit & Byte werkt, maar de code herhaalt zich: elke repository haalt op, voegt toe en verwijdert op bijna dezelfde manier. En elke controller vraagt zijn eigen repositories aan.

Jouw taak is om je repositories om te bouwen naar een generieke repository met specifieke repositories erbovenop, ze achter een Unit of Work te zetten, en twee nieuwe endpoints toe te voegen die iets bijwerken.

Via de API moet de website, net als in 06_01, alle artikelen en leveranciers kunnen opvragen, filteren en de fiche tonen, en leveranciers kunnen toevoegen en verwijderen. Daarnaast moet ze:

1. de gegevens van een leverancier kunnen bijwerken;
2. een artikel bij een andere leverancier kunnen onderbrengen.

Implementeer onderstaande functionaliteiten.

### 1. Je vertrekpunt

Vertrek van je eigen oplossing van 06_01. Alle endpoints uit die oefening blijven werken, met dezelfde controles, dezelfde berichten en dezelfde JSON.

Je modellen en je database veranderen in deze oefening niet. De startgegevens blijven die van 06_01.

---

### 2. De generieke repository

Maak in de map Repositories een generieke interface `IGenericRepository<TEntity>` en een klasse `GenericRepository<TEntity>` die ze implementeert, met de vijf bewerkingen uit les 07: alles ophalen, één ophalen op Id, toevoegen, bijwerken en verwijderen.

> [!IMPORTANT]
> Toevoegen, bijwerken en verwijderen bewaren niets. Ze vertellen Entity Framework Core enkel wat er moet gebeuren. Na deze oefening roept geen enkele repository nog zelf `SaveChangesAsync` aan.

---

### 3. De specifieke repositories

Een vraag die de generieke repository niet kan beantwoorden, zoals alle artikelen van één soort, hoort in een specifieke repository. Die overerft van de generieke, en voegt enkel toe wat ontbreekt.

Bouw `IArtikelRepository` en `ArtikelRepository` zo om. Beslis zelf of `Leverancier` een specifieke repository nodig heeft, of dat de generieke volstaat.

> [!TIP]
> Overloop elk endpoint en vraag je af welke repository het antwoord kan geven. Een model dat enkel de vijf standaardbewerkingen nodig heeft, krijgt in les 07 geen eigen repository.

> [!WARNING]
> Lijsten blijven op Id gesorteerd. Een database belooft geen volgorde, tenzij je erom vraagt. Kijk na waar je API die volgorde nog vraagt. Of dat in een specifieke repository gebeurt of in je controller, kies je zelf.

> [!WARNING]
> De waarschuwing over een *object cycle* uit 06_01 blijft gelden. De generieke repository haalt altijd een volledig object op, ook als je enkel wil weten óf het bestaat.

---

### 4. De Unit of Work

Maak een interface `IUnitOfWork` en een klasse `UnitOfWork`, zoals in les 07: een property per repository die je API gebruikt, en één methode `SaveChangesAsync`.

- Program.cs registreert enkel nog je `ArtikelContext` en je Unit of Work.
- Elke controller krijgt de Unit of Work binnen, en verder enkel zijn logger.

Heeft een repository specifieke methodes, laat de Unit of Work dan het specifieke type teruggeven. Anders kan je controller die methodes niet bereiken.

#### Controle

Maak een migratie met de naam `Controle`, zonder iets aan je modellen te veranderen. Ze moet leeg zijn: `Up` en `Down` doen niets. Verwijder ze daarna weer.

Doorloop daarna het testscenario van 06_01 opnieuw. Elk antwoord moet hetzelfde zijn.

---

### 5. Een leverancier bijwerken

<span style="background:#d9822b;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>PUT</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/leverancier/<span style="color:#e64980"><b>{id}</b></span></code>

Het Id staat in de URL, de nieuwe gegevens staan in de body. Stuurt de client ook een Id in de body, dan telt enkel dat uit de URL.

- De leverancier bestaat niet: <code style="color:#f08c00;font-weight:600">404 Not Found</code> met <code style="color:#845ef7">Kan geen leverancier bijwerken met Id <span style="color:#e64980"><b>{id}</b></span>, omdat deze niet bestaat.</code>
- De naam is leeg: <code style="color:#f08c00;font-weight:600">400 Bad Request</code> met <code style="color:#845ef7">Een leverancier moet een naam hebben.</code>
- Anders: werk de leverancier bij en geef <code style="color:#37b24d;font-weight:600">204 No Content</code> terug.

Een geweigerde wijziging verandert niets aan de leverancier.

> [!WARNING]
> Krijg je een <code style="color:#f03e3e;font-weight:600">500</code>, met in je terminal een melding dat er al een object met hetzelfde Id gevolgd wordt (*already being tracked*)? De laptopcontroller uit les 07 loopt hier zelf op vast: neem hem niet zomaar over. Tel hoeveel objecten met hetzelfde Id je API aan Entity Framework Core geeft.

---

### 6. Alles samen: een artikel naar een andere leverancier

<span style="background:#7048e8;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>SAMENGESTELDE OEFENING</b></span>

Soms neemt een andere leverancier een artikel over. De generieke repository, de specifieke repositories en de Unit of Work komen samen in één endpoint.

<span style="background:#d9822b;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>PUT</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/artikel/<span style="color:#e64980"><b>{id}</b></span>/leverancier/<span style="color:#e64980"><b>{leverancierId}</b></span></code>

Controleer in deze volgorde:

1. het artikel bestaat niet: <code style="color:#f08c00;font-weight:600">404 Not Found</code> met <code style="color:#845ef7">We vonden geen artikel met Id <span style="color:#e64980"><b>{id}</b></span>.</code>
2. de leverancier bestaat niet: <code style="color:#f08c00;font-weight:600">400 Bad Request</code> met <code style="color:#845ef7">We vonden geen leverancier met Id <span style="color:#e64980"><b>{leverancierId}</b></span>.</code>
3. het artikel komt al van die leverancier: <code style="color:#f08c00;font-weight:600">400 Bad Request</code> met <code style="color:#845ef7">Dit artikel komt al van deze leverancier.</code>

Anders hoort het artikel voortaan bij die leverancier, en geef je <code style="color:#37b24d;font-weight:600">204 No Content</code> terug. Verder verandert er niets aan het artikel.

#### Testscenario

Bouw je database opnieuw op, zodat ze exact de startgegevens bevat, en doorloop deze reeks:

1. Werk leverancier 2 bij. Je krijgt <code style="color:#37b24d;font-weight:600">204</code>. <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> api/leverancier/2</code> toont de nieuwe naam.

   ```json
   { "naam": "Componentenhuis NL", "land": "Nederland" }
   ```

2. <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> api/leverancier</code> geeft de leveranciers 1, 2, 3 en 4, in die volgorde.
3. Werk leverancier 99 bij. Je krijgt <code style="color:#f08c00;font-weight:600">404</code> met <code style="color:#845ef7">Kan geen leverancier bijwerken met Id 99, omdat deze niet bestaat.</code>
4. Werk leverancier 1 bij met een lege naam. Je krijgt <code style="color:#f08c00;font-weight:600">400</code> met <code style="color:#845ef7">Een leverancier moet een naam hebben.</code> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> api/leverancier/1</code> toont nog altijd `TechDistri`.
5. Breng artikel 4 onder bij leverancier 4. Je krijgt <code style="color:#37b24d;font-weight:600">204</code>. <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> api/leverancier/4/artikelen</code> geeft enkel artikel 4, en <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> api/leverancier/3/artikelen</code> enkel artikel 3.
6. <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> api/artikel/4</code> geeft dezelfde JSON als in 06_01, maar met `leverancierId` `4`.
7. Doe stap 5 nog eens. Je krijgt <code style="color:#f08c00;font-weight:600">400</code> met <code style="color:#845ef7">Dit artikel komt al van deze leverancier.</code>
8. Breng artikel 4 onder bij leverancier 99. Je krijgt <code style="color:#f08c00;font-weight:600">400</code> met <code style="color:#845ef7">We vonden geen leverancier met Id 99.</code> Breng artikel 99 onder bij leverancier 1. Je krijgt <code style="color:#f08c00;font-weight:600">404</code> met <code style="color:#845ef7">We vonden geen artikel met Id 99.</code>
9. Verwijder leverancier 4. Je krijgt <code style="color:#f08c00;font-weight:600">400</code> met <code style="color:#845ef7">Deze leverancier levert nog artikelen.</code>
10. Breng artikel 4 terug onder bij leverancier 3, en verwijder leverancier 4. Je krijgt telkens <code style="color:#37b24d;font-weight:600">204</code>.
11. Voeg een leverancier toe met de naam `Kabelkoning` en als land `België`. Je krijgt <code style="color:#37b24d;font-weight:600">201</code>, en de nieuwe leverancier heeft Id 5.
