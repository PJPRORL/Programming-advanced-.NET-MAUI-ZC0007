# 07_02

<b>Hoofdstuk 07</b> &nbsp;·&nbsp; Vier repositories, één Unit of Work &nbsp;·&nbsp; PC-onderdelen &nbsp;·&nbsp; niveau <span style="color:#f08c00">●●○</span> &nbsp;·&nbsp; ±2 uur

## Leerdoel

Na deze oefening kan je vier modellen bedienen met één generieke repository en enkel de specifieke repositories die je echt nodig hebt, allemaal achter één Unit of Work.

Je leert wijzigingen aan meerdere tabellen samen naar de database te sturen, met één `SaveChangesAsync`, zodat ze samen slagen of samen mislukken.

Daarnaast merk je waar dat niet lukt: wat je pas weet nadat de database iets bewaard heeft, kan je niet in hetzelfde moment al gebruiken, als er geen relatie is die het voor je invult.

## 

## Opdracht

Vier modellen, vier repositories, en een controller die er drie tegelijk nodig heeft: de API van Bit & Byte groeit. Tegelijk past het bijwerken van een ventilator vandaag twee tabellen aan, de ventilatoren en de historiek, in twee aparte stappen. Valt de tweede weg, dan klopt de historiek niet meer.

Jouw taak is om je repositories om te bouwen naar een generieke repository met specifieke repositories erbovenop, ze achter een Unit of Work te zetten, en een behuizing met al haar compatibiliteiten in één keer te laten verdwijnen.

Via de API moet de inkoopafdeling, net als in 06_02, ventilatoren kunnen beheren, vervangen en koppelen, en de historiek nalezen. Daarnaast moet ze:

1. één compatibiliteit kunnen weghalen;
2. een behuizing uit het gamma kunnen halen, samen met al haar compatibiliteiten.

Implementeer onderstaande functionaliteiten.

### 1. Je vertrekpunt

Vertrek van je eigen oplossing van 06_02. Alle endpoints uit die oefening blijven werken, met dezelfde controles, dezelfde berichten en dezelfde JSON. Ook de historiek blijft precies zo werken.

Je modellen en je database veranderen in deze oefening niet. De startgegevens blijven die van 06_02.

---

### 2. De generieke repository

Maak in de map Repositories een generieke interface `IGenericRepository<TEntity>` en een klasse `GenericRepository<TEntity>` die ze implementeert, met de vijf bewerkingen uit les 07: alles ophalen, één ophalen op Id, toevoegen, bijwerken en verwijderen.

> [!IMPORTANT]
> Toevoegen, bijwerken en verwijderen bewaren niets. Ze vertellen Entity Framework Core enkel wat er moet gebeuren. Na deze oefening roept geen enkele repository nog zelf `SaveChangesAsync` aan.

---

### 3. De specifieke repositories en de Unit of Work

Je API werkt met vier modellen: `Ventilator`, `Wijziging`, `Behuizing` en `Compatibiliteit`. Beslis per model of de generieke repository volstaat, of dat het een specifieke repository nodig heeft die overerft van de generieke.

Maak daarna een interface `IUnitOfWork` en een klasse `UnitOfWork`, zoals in les 07, met een property per repository en één methode `SaveChangesAsync`.

- Program.cs registreert enkel nog je `VentilatorContext` en je Unit of Work.
- Elke controller krijgt de Unit of Work binnen, en verder enkel zijn logger.

> [!WARNING]
> Lijsten blijven gesorteerd zoals vroeger: ventilatoren, behuizingen en compatibiliteiten op Id, de historiek van oud naar nieuw. Een database belooft geen volgorde, tenzij je erom vraagt. Kijk na waar je API die volgorde nog vraagt. Of dat in een specifieke repository gebeurt of in je controller, kies je zelf.

> [!WARNING]
> De waarschuwing over een *object cycle* uit 06_02 blijft gelden. De generieke repository haalt altijd een volledig object op, ook als je enkel wil weten óf het bestaat.

---

### 4. Eén keer bewaren

Bijwerken en verwijderen passen elk twee tabellen aan: de ventilatoren en de historiek. Die twee wijzigingen gaan voortaan samen naar de database, met één `SaveChangesAsync`.

Lukt dat ook bij toevoegen en vervangen? Kijk welk Id de historiek daar nodig heeft, en wanneer de database dat Id geeft. Lukt het niet in één keer, dan mag je API daar twee keer bewaren.

Bij het bijwerken van een ventilator staat het Id in de URL. Stuurt de client ook een Id in de body, dan telt enkel dat uit de URL.

> [!WARNING]
> Krijg je bij het bijwerken van een ventilator een <code style="color:#f03e3e;font-weight:600">500</code>, met in je terminal een melding dat er al een object met hetzelfde Id gevolgd wordt (*already being tracked*)? De laptopcontroller uit les 07 loopt hier zelf op vast: neem hem niet zomaar over. Tel hoeveel objecten met hetzelfde Id je API aan Entity Framework Core geeft.

---

### 5. Een compatibiliteit weghalen

<span style="background:#e03131;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>DELETE</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/compatibiliteit/<span style="color:#e64980"><b>{id}</b></span></code>

- De compatibiliteit bestaat niet: <code style="color:#f08c00;font-weight:600">404 Not Found</code> met <code style="color:#845ef7">We vonden geen compatibiliteit met Id <span style="color:#e64980"><b>{id}</b></span>.</code>
- Anders: verwijder ze en geef <code style="color:#37b24d;font-weight:600">204 No Content</code> terug.

De ventilator en de behuizing blijven bestaan, en de historiek verandert niet.

---

### 6. Alles samen: een behuizing uit het gamma halen

<span style="background:#7048e8;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>SAMENGESTELDE OEFENING</b></span>

Haalt Bit & Byte een behuizing uit het gamma, dan verdwijnen ook alle compatibiliteiten van die behuizing. Dat zijn twee soorten wijzigingen in twee tabellen, en ze horen bij elkaar: of alles verdwijnt, of niets.

<span style="background:#e03131;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>DELETE</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/behuizing/<span style="color:#e64980"><b>{id}</b></span></code>

- De behuizing bestaat niet: <code style="color:#f08c00;font-weight:600">404 Not Found</code> met <code style="color:#845ef7">We vonden geen behuizing met Id <span style="color:#e64980"><b>{id}</b></span>.</code>
- Anders: verwijder de behuizing en al haar compatibiliteiten, met één `SaveChangesAsync`, en geef <code style="color:#37b24d;font-weight:600">204 No Content</code> terug.

De ventilatoren blijven bestaan, ook een ventilator die daarna in geen enkele behuizing meer past. De historiek verandert niet: ze gaat enkel over ventilatoren.

> [!IMPORTANT]
> De regel uit 06_02 blijft: een behuizing waarnaar nog een compatibiliteit verwijst, mag de database niet laten verwijderen. Pas je relatie dus niet aan. Je API ruimt zelf op.

> [!WARNING]
> Krijg je een <code style="color:#f03e3e;font-weight:600">500</code> over een relatie die verbroken wordt (*severed*), of over een vreemde sleutel? Dan verwijst er bij het bewaren nog een compatibiliteit naar de behuizing.

#### Testscenario

Bouw je database opnieuw op, zodat ze exact de startgegevens bevat, en doorloop deze reeks:

1. Werk ventilator 2 bij. Je krijgt <code style="color:#37b24d;font-weight:600">204</code>. <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> api/ventilator/2</code> toont de nieuwe gegevens. De historiek bevat één regel: `bijgewerkt`, met ventilatorId 2.

   ```json
   { "merk": "be quiet!", "afmetingInMm": 140, "verlichting": "ARGB", "prijs": 26.90 }
   ```

2. <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> api/ventilator</code> geeft de ventilatoren 1, 2, 3 en 4, in die volgorde.
3. Werk ventilator 99 bij. Je krijgt <code style="color:#f08c00;font-weight:600">404</code> met <code style="color:#845ef7">Kan geen ventilator bijwerken met Id 99, omdat deze niet bestaat.</code> Werk ventilator 1 bij met een afmeting van 92 mm. Je krijgt <code style="color:#f08c00;font-weight:600">400</code> met <code style="color:#845ef7">We verkopen enkel ventilatoren van 120 of 140 mm.</code> De historiek bevat nog altijd één regel.
4. Haal compatibiliteit 6 weg. Je krijgt <code style="color:#37b24d;font-weight:600">204</code>. Doe het nog eens: nu krijg je <code style="color:#f08c00;font-weight:600">404</code> met <code style="color:#845ef7">We vonden geen compatibiliteit met Id 6.</code>
5. Verwijder ventilator 4. Je krijgt <code style="color:#37b24d;font-weight:600">204</code>: hij past nergens meer in. De historiek bevat twee regels, de tweede `verwijderd` met ventilatorId 4.
6. Haal behuizing 1 uit het gamma. Je krijgt <code style="color:#37b24d;font-weight:600">204</code>. <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> api/compatibiliteit/1</code> en <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> api/compatibiliteit/2</code> geven <code style="color:#f08c00;font-weight:600">404</code>, <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> api/behuizing</code> geeft enkel de behuizingen 2 en 3.
7. <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> api/ventilator/1</code> en <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> api/ventilator/2</code> bestaan nog. De historiek bevat nog altijd twee regels.
8. Verwijder ventilator 2. Je krijgt <code style="color:#f08c00;font-weight:600">400</code> met <code style="color:#845ef7">Deze ventilator past nog in een behuizing.</code> Compatibiliteit 5 verwijst er nog naar.
9. Probeer in pgAdmin of in VS Code behuizing 2 rechtstreeks uit de tabel te verwijderen. De database weigert, met een melding over de vreemde sleutel.
10. Haal behuizing 99 uit het gamma. Je krijgt <code style="color:#f08c00;font-weight:600">404</code> met <code style="color:#845ef7">We vonden geen behuizing met Id 99.</code>
