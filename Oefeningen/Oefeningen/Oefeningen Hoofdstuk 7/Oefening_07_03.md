# 07_03

<b>Hoofdstuk 07</b> &nbsp;·&nbsp; Alles of niets, met één Unit of Work &nbsp;·&nbsp; PC-onderdelen &nbsp;·&nbsp; niveau <span style="color:#f03e3e">●●●</span> &nbsp;·&nbsp; ±2,5 uur

## Leerdoel

Na deze oefening kan je je repositories ombouwen naar een generieke repository met specifieke repositories erbovenop, achter één Unit of Work, zonder dat een client iets van die ombouw merkt.

Je leert een bestelling en al haar lijnen in één verzoek ontvangen, alles controleren voor je iets bewaart, en dan alles in één keer bewaren.

Daarnaast merk je dat de generieke repository niet weet welke regels er voor jouw modellen gelden: welke lijnen actief zijn, en welke gegevens een client niet mag veranderen.

## 

## Opdracht

Een klant die aan de balie bestelt, krijgt vandaag eerst een lege bestelling, en daarna komen de lijnen er één voor één bij. Loopt er onderweg iets mis, dan blijft er een halve bestelling achter. De boekhouding wil voortaan een bestelling met al haar lijnen in één keer ontvangen: alles klopt, of er komt niets bij.

Jouw taak is om je repositories om te bouwen naar een generieke repository met specifieke repositories erbovenop, ze achter een Unit of Work te zetten, en een endpoint te maken dat een volledige bestelling in één keer plaatst.

Via de API moet een medewerker, net als in 06_03, bestellijnen en bestellingen kunnen beheren, de fiche opvragen, per bestelling en over alles samen, en lijnen schrappen. Daarnaast moet hij:

1. een bestelling met al haar lijnen in één verzoek kunnen plaatsen.

Implementeer onderstaande functionaliteiten.

### 1. Je vertrekpunt

Vertrek van je eigen oplossing van 06_03. Alle endpoints uit die oefening blijven werken, met dezelfde controles, dezelfde berichten en dezelfde JSON.

Je modellen en je database veranderen in deze oefening niet. De startgegevens blijven die van 06_03.

---

### 2. De generieke repository en de Unit of Work

Maak in de map Repositories een generieke interface `IGenericRepository<TEntity>` en een klasse `GenericRepository<TEntity>` die ze implementeert, met de vijf bewerkingen uit les 07: alles ophalen, één ophalen op Id, toevoegen, bijwerken en verwijderen.

Beslis per model of de generieke repository volstaat, of dat het een specifieke repository nodig heeft die overerft van de generieke. Maak daarna een interface `IUnitOfWork` en een klasse `UnitOfWork`, zoals in les 07.

- Geen enkele repository roept nog zelf `SaveChangesAsync` aan.
- Program.cs registreert enkel nog je `BestellijnContext` en je Unit of Work.
- Elke controller krijgt de Unit of Work binnen, en verder enkel zijn logger.

> [!WARNING]
> De generieke repository kent je regels niet. Ze geeft geschrapte lijnen even graag terug als actieve, en ze belooft geen volgorde. Kijk voor elk endpoint na of het nog altijd enkel teruggeeft wat het in 06_03 teruggaf, in dezelfde volgorde.

> [!WARNING]
> De waarschuwing over een *object cycle* uit 06_03 blijft gelden. De generieke repository haalt altijd een volledig object op, ook als je enkel wil weten óf het bestaat.

---

### 3. Bijwerken met de generieke repository

<span style="background:#d9822b;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>PUT</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/bestellijn/<span style="color:#e64980"><b>{id}</b></span></code> werkt zoals vroeger, met dezelfde controles en dezelfde berichten. Het Id staat in de URL. Stuurt de client ook een Id in de body, dan telt enkel dat uit de URL. Daarnaast gelden twee regels, wat de client ook meestuurt:

- bijwerken verandert nooit of een lijn actief is;
- bijwerken verandert nooit bij welke bestelling een lijn hoort.

> [!WARNING]
> Krijg je een <code style="color:#f03e3e;font-weight:600">500</code>, met in je terminal een melding dat er al een object met hetzelfde Id gevolgd wordt (*already being tracked*), of een melding over een vreemde sleutel? Kijk dan welke objecten je API aan Entity Framework Core geeft, en welke van hun gegevens het als gewijzigd beschouwt.

---

### 4. Alles samen: een bestelling in één keer plaatsen

<span style="background:#7048e8;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>SAMENGESTELDE OEFENING</b></span>

<span style="background:#2f9e44;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>POST</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/bestelling/volledig</code>

De client stuurt een bestelling, met haar lijnen in `bestellijnen`. Bij dit ene endpoint stuurt de client een navigation property dus wél mee.

```json
{
  "referentie": "BB-2026-004",
  "klantnaam": "Mehdi Aziz",
  "bestellijnen": [
    { "omschrijving": "Processor Ryzen 7 7800X3D", "aantal": 1, "stukPrijs": 389.00 },
    { "omschrijving": "Koelpasta", "aantal": 2, "stukPrijs": 7.99 }
  ]
}
```

Controleer in deze volgorde, en geef de eerste fout terug die je tegenkomt:

1. de bestelling zelf, met de controles uit 06_03;
2. er zijn geen lijnen: <code style="color:#f08c00;font-weight:600">400 Bad Request</code> met <code style="color:#845ef7">Een bestelling moet minstens één bestellijn hebben.</code>
3. de lijnen, één voor één, in de volgorde van het verzoek, met de controles op de lijn zelf uit 06_03. De controle of de bestelling bestaat, valt hier weg: de lijnen horen bij de nieuwe bestelling.

Faalt één controle, dan geef je <code style="color:#f08c00;font-weight:600">400 Bad Request</code> terug, en komt er niets in de database: geen bestelling en geen enkele lijn.

Slaagt alles, dan bewaar je de bestelling en al haar lijnen met één `SaveChangesAsync`. Elke nieuwe lijn is actief en hoort bij de nieuwe bestelling, wat de client ook meestuurt. Je geeft <code style="color:#37b24d;font-weight:600">201 Created</code> terug, met het adres van de nieuwe bestelling, en de bestelling zoals <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> api/bestelling/{id}</code> ze ook zou tonen:

```json
{
  "id": 4,
  "referentie": "BB-2026-004",
  "klantnaam": "Mehdi Aziz",
  "bestellijnen": null
}
```

> [!TIP]
> De controles op de lijn zelf bestaan al, voor <span style="background:#2f9e44;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>POST</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/bestellijn</code>. Gebruik ze opnieuw, in plaats van ze te kopiëren.

> [!WARNING]
> Krijg je een <code style="color:#f03e3e;font-weight:600">500</code> over een *object cycle*, terwijl de bestelling wel bewaard is? Dan kent de nieuwe bestelling haar lijnen, en kennen die lijnen haar weer. Denk na over welk object je in je antwoord terugstuurt.

#### Testscenario

Bouw je database opnieuw op, zodat ze exact de startgegevens bevat, en doorloop deze reeks. Bedragen vergelijk je als getal: `0`, `0.0` en `0.00` zijn hetzelfde antwoord.

1. Werk lijn 2 bij. Je krijgt <code style="color:#37b24d;font-weight:600">204</code>. <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> api/bestellijn/2</code> toont aantal 3 en een stukprijs van 104.90, is actief en hoort bij bestelling 1.

   ```json
   { "omschrijving": "DDR5 32 GB kit", "aantal": 3, "stukPrijs": 104.90 }
   ```

2. Werk lijn 5 bij met dezelfde gegevens als in de startgegevens, maar met `bestellingId` `3` en `isActief` `false`. Je krijgt <code style="color:#37b24d;font-weight:600">204</code>. Lijn 5 is nog altijd actief en hoort nog altijd bij bestelling 2.
3. <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> api/bestellijn</code> geeft de lijnen 1, 2, 3, 5 en 6, in die volgorde. <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> api/bestellijn/4</code> geeft nog altijd <code style="color:#f08c00;font-weight:600">404</code> met <code style="color:#845ef7">We vonden geen actieve bestellijn met Id 4.</code>
4. <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> api/bestelling/1/fiche</code> geeft `totaalActief` `703.69` en `totaalGeschrapt` `89.95`.
5. Plaats de bestelling uit punt 4, maar met een stukprijs van `0` voor de koelpasta. Je krijgt <code style="color:#f08c00;font-weight:600">400</code> met <code style="color:#845ef7">De stukprijs moet groter zijn dan 0.</code> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> api/bestelling</code> geeft nog altijd de bestellingen 1, 2 en 3, en <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> api/bestellijn/alles</code> nog altijd zes lijnen.
6. Plaats dezelfde bestelling met een lege klantnaam en zonder lijnen, met `"bestellijnen": []`. Je krijgt <code style="color:#f08c00;font-weight:600">400</code> met <code style="color:#845ef7">Een bestelling moet een klantnaam hebben.</code> Zet de klantnaam terug. Nu krijg je <code style="color:#f08c00;font-weight:600">400</code> met <code style="color:#845ef7">Een bestelling moet minstens één bestellijn hebben.</code>
7. Plaats de bestelling uit punt 4, met `"isActief": false` bij de koelpasta. Je krijgt <code style="color:#37b24d;font-weight:600">201</code>, met als body de bestelling met Id 4, zoals in punt 4. De header `Location` eindigt op `/api/bestelling/4`; hoofdletters tellen daarbij niet.
8. <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> api/bestellijn/alles</code> geeft acht lijnen. De twee nieuwe lijnen hebben de Id's 7 en 8, zijn allebei actief en horen bij bestelling 4.
9. <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> api/bestelling/4/fiche</code> geeft `aantalActieveLijnen` `2`, `aantalGeschrapteLijnen` `0`, `totaalActief` `404.98`, `totaalGeschrapt` `0`, de lijn met de processor als duurste, en `isLeeg` `false`.
10. <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> api/bestellijn/fiche</code>, over alle lijnen samen, geeft `aantalActieveLijnen` `7`, `aantalGeschrapteLijnen` `1` en `totaalActief` `1272.64`.
11. Verwijder bestelling 4. Je krijgt <code style="color:#f08c00;font-weight:600">400</code> met <code style="color:#845ef7">Deze bestelling heeft nog bestellijnen.</code>
