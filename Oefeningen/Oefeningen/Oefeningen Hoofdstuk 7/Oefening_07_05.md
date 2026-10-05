# 07_05

<b>Hoofdstuk 07</b> &nbsp;·&nbsp; Vier repositories, één Unit of Work &nbsp;·&nbsp; Fietsverhuur &nbsp;·&nbsp; niveau <span style="color:#f08c00">●●○</span> &nbsp;·&nbsp; ±2 uur

## Leerdoel

Na deze oefening kan je vier modellen bedienen met één generieke repository en enkel de specifieke repositories die je echt nodig hebt, allemaal achter één Unit of Work.

Je leert wijzigingen aan meerdere tabellen samen naar de database te sturen, met één `SaveChangesAsync`, zodat ze samen slagen of samen mislukken.

Daarnaast merk je dat de generieke repository de regels van je fietsen niet kent.

## 

## Opdracht

Vier modellen, vier repositories, en een controller die er drie tegelijk nodig heeft: de API van Trapdoor groeit. Tegelijk past een verhuring vandaag twee tabellen aan, de fietsen en de verhuringen, in twee aparte stappen. Valt de tweede weg, dan staat er een fiets op verhuurd zonder dat iemand weet waarom.

Jouw taak is om je repositories om te bouwen naar een generieke repository met specifieke repositories erbovenop, ze achter een Unit of Work te zetten, en een accessoire met al zijn koppelingen in één keer te laten verdwijnen.

Via de API moet de balie, net als in 06_05, fietsen kunnen beheren, vervangen, verhuren en terugbrengen, accessoires koppelen en de verhuringen nalezen. Daarnaast moet ze:

1. één koppeling kunnen weghalen;
2. een accessoire uit het assortiment kunnen halen, samen met al zijn koppelingen.

Implementeer onderstaande functionaliteiten.

### 1. Je vertrekpunt

Vertrek van je eigen oplossing van 06_05. Alle endpoints uit die oefening blijven werken, met dezelfde controles, dezelfde berichten en dezelfde JSON. Ook de verhuringen blijven precies zo werken.

Je modellen en je database veranderen in deze oefening niet. De startgegevens blijven die van 06_05.

---

### 2. De generieke repository

Maak in de map Repositories een generieke interface `IGenericRepository<TEntity>` en een klasse `GenericRepository<TEntity>` die ze implementeert, met de vijf bewerkingen uit les 07: alles ophalen, één ophalen op Id, toevoegen, bijwerken en verwijderen.

> [!IMPORTANT]
> Toevoegen, bijwerken en verwijderen bewaren niets. Ze vertellen Entity Framework Core enkel wat er moet gebeuren. Na deze oefening roept geen enkele repository nog zelf `SaveChangesAsync` aan.

---

### 3. De specifieke repositories en de Unit of Work

Je API werkt met vier modellen: `Fiets`, `Verhuring`, `Accessoire` en `FietsAccessoire`. Beslis per model of de generieke repository volstaat, of dat het een specifieke repository nodig heeft die overerft van de generieke.

Maak daarna een interface `IUnitOfWork` en een klasse `UnitOfWork`, zoals in les 07, met een property per repository en één methode `SaveChangesAsync`.

- Program.cs registreert enkel nog je `FietsverhuurContext` en je Unit of Work.
- Elke controller krijgt de Unit of Work binnen, en verder enkel zijn logger.

> [!WARNING]
> Lijsten blijven gesorteerd zoals vroeger: fietsen, accessoires en koppelingen op Id, de verhuringen van oud naar nieuw. Een database belooft geen volgorde, tenzij je erom vraagt. Kijk na waar je API die volgorde nog vraagt. Of dat in een specifieke repository gebeurt of in je controller, kies je zelf.

> [!WARNING]
> De waarschuwing over een *object cycle* uit 06_05 blijft gelden. De generieke repository haalt altijd een volledig object op, ook als je enkel wil weten óf het bestaat.

---

### 4. Eén keer bewaren

Verhuren en terugbrengen passen elk twee tabellen aan: de fietsen en de verhuringen. Die twee wijzigingen gaan voortaan samen naar de database, met één `SaveChangesAsync`.

Bij het bijwerken van een fiets staat het Id in de URL. Stuurt de client ook een Id in de body, dan telt enkel dat uit de URL. En de regel uit 03_05 blijft: bijwerken verandert nooit of een fiets beschikbaar is, wat de client ook meestuurt.

> [!WARNING]
> Krijg je bij het bijwerken van een fiets een <code style="color:#f03e3e;font-weight:600">500</code>, met in je terminal een melding dat er al een object met hetzelfde Id gevolgd wordt (*already being tracked*)? De laptopcontroller uit les 07 loopt hier zelf op vast: neem hem niet zomaar over. Tel hoeveel objecten met hetzelfde Id je API aan Entity Framework Core geeft. Is die fout weg, kijk dan na of een fiets na het bijwerken nog altijd even beschikbaar is als ervoor.

---

### 5. Een koppeling weghalen

<span style="background:#e03131;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>DELETE</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/fietsaccessoire/<span style="color:#e64980"><b>{id}</b></span></code>

- De koppeling bestaat niet: <code style="color:#f08c00;font-weight:600">404 Not Found</code> met <code style="color:#845ef7">We vonden geen koppeling met Id <span style="color:#e64980"><b>{id}</b></span>.</code>
- Anders: verwijder ze en geef <code style="color:#37b24d;font-weight:600">204 No Content</code> terug.

De fiets en het accessoire blijven bestaan, en de verhuringen veranderen niet.

---

### 6. Alles samen: een accessoire uit het assortiment halen

<span style="background:#7048e8;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>SAMENGESTELDE OEFENING</b></span>

Haalt Trapdoor een accessoire uit het assortiment, dan verdwijnen ook alle koppelingen van dat accessoire. Dat zijn twee soorten wijzigingen in twee tabellen, en ze horen bij elkaar: of alles verdwijnt, of niets. Eén ding kan niet: een accessoire weghalen dat nu met een fiets op pad is.

<span style="background:#e03131;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>DELETE</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/accessoire/<span style="color:#e64980"><b>{id}</b></span></code>

Controleer in deze volgorde:

1. het accessoire bestaat niet: <code style="color:#f08c00;font-weight:600">404 Not Found</code> met <code style="color:#845ef7">We vonden geen accessoire met Id <span style="color:#e64980"><b>{id}</b></span>.</code>
2. het accessoire hoort bij een fiets die verhuurd is: <code style="color:#f08c00;font-weight:600">400 Bad Request</code> met <code style="color:#845ef7">Dit accessoire hoort bij een verhuurde fiets.</code>

Anders verwijder je het accessoire en al zijn koppelingen, met één `SaveChangesAsync`, en geef je <code style="color:#37b24d;font-weight:600">204 No Content</code> terug. De fietsen blijven bestaan, en de verhuringen veranderen niet.

> [!IMPORTANT]
> De regel uit 06_05 blijft: een accessoire waarnaar nog een koppeling verwijst, mag de database niet laten verwijderen. Pas je relatie dus niet aan. Je API ruimt zelf op.

> [!WARNING]
> Krijg je een <code style="color:#f03e3e;font-weight:600">500</code> over een relatie die verbroken wordt (*severed*), of over een vreemde sleutel? Dan verwijst er bij het bewaren nog een koppeling naar het accessoire.

#### Testscenario

Bouw je database opnieuw op, zodat ze exact de startgegevens bevat, en doorloop deze reeks:

1. Werk fiets 2 bij. Je krijgt <code style="color:#37b24d;font-weight:600">204</code>. <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> api/fiets/2</code> toont framemaat 50 en een prijs per dag van 19.00, en de fiets is nog altijd beschikbaar.

   ```json
   { "type": "mountainbike", "framemaat": 50, "prijsPerDag": 19.00 }
   ```

2. Werk fiets 3 bij met type `e-bike`, framemaat 56, een prijs per dag van 27.00 en `isBeschikbaar` `true`. Je krijgt <code style="color:#37b24d;font-weight:600">204</code>. De fiets is nog altijd niet beschikbaar.
3. <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> api/fiets</code> geeft de fietsen 1, 2, 3 en 4, in die volgorde. Werk fiets 99 bij. Je krijgt <code style="color:#f08c00;font-weight:600">404</code> met <code style="color:#845ef7">Kan geen fiets bijwerken met Id 99, omdat deze niet bestaat.</code>
4. Haal koppeling 6 weg. Je krijgt <code style="color:#37b24d;font-weight:600">204</code>. Doe het nog eens: nu krijg je <code style="color:#f08c00;font-weight:600">404</code> met <code style="color:#845ef7">We vonden geen koppeling met Id 6.</code>
5. Koppel accessoire 2 aan fiets 3, met aantal 1. Je krijgt <code style="color:#37b24d;font-weight:600">201</code>, en de nieuwe koppeling heeft Id 7.
6. Haal accessoire 2 uit het assortiment. Je krijgt <code style="color:#f08c00;font-weight:600">400</code> met <code style="color:#845ef7">Dit accessoire hoort bij een verhuurde fiets.</code> Koppeling 4 en 7 bestaan nog.
7. Breng fiets 3 terug. Je krijgt <code style="color:#37b24d;font-weight:600">204</code>. De verhuringen bevatten één regel: `teruggebracht`.
8. Haal accessoire 2 uit het assortiment. Je krijgt <code style="color:#37b24d;font-weight:600">204</code>. <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> api/fietsaccessoire/4</code> en <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> api/fietsaccessoire/7</code> geven <code style="color:#f08c00;font-weight:600">404</code>, <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> api/accessoire</code> geeft de accessoires 1, 3 en 4.
9. <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> api/fiets/2/accessoires</code> geeft enkel koppeling 3. De verhuringen bevatten nog altijd één regel.
10. Probeer in pgAdmin of in VS Code accessoire 1 rechtstreeks uit de tabel te verwijderen. De database weigert, met een melding over de vreemde sleutel.
11. Haal accessoire 99 uit het assortiment. Je krijgt <code style="color:#f08c00;font-weight:600">404</code> met <code style="color:#845ef7">We vonden geen accessoire met Id 99.</code>
