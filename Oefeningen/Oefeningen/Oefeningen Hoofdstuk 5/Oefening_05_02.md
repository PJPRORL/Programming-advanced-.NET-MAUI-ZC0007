# 05_02

<b>Hoofdstuk 05</b> &nbsp;·&nbsp; CRUD op de database, asynchroon &nbsp;·&nbsp; PC-onderdelen &nbsp;·&nbsp; niveau <span style="color:#f08c00">●●○</span> &nbsp;·&nbsp; ±90 min

## Leerdoel

Na deze oefening kan je toevoegen, wijzigen en verwijderen via een repository die met de database praat, en weet je wanneer die wijzigingen echt in de database terechtkomen.

Je leert twee repositories op dezelfde DbContext laten werken, en de hele keten asynchroon maken, van de database tot in de controller.

Daarnaast kan je botsen op twee fouten die je pas nu tegenkomt: een teller die niet weet welke Id's al bestaan, en een antwoord dat mislukt nadat de gegevens al bewaard zijn.

## 

## Opdracht

In 04_02 zette je naast je ventilator-API een database op met twee tabellen, en zorgde je dat je API en je database hetzelfde verhaal vertelden. Nu neemt de database het over, voor de ventilatoren én voor de historiek.

Jouw taak is om twee repositories te schrijven die met de database werken, de hele keten asynchroon te maken, en je API te laten overstappen zonder dat de client iets merkt, behalve dat er niets meer verdwijnt bij een herstart.

Via de API moet de inkoopafdeling, net als in 04_02:

1. alle ventilatoren kunnen opvragen;
2. één ventilator kunnen opvragen;
3. een nieuwe ventilator kunnen toevoegen;
4. een bestaande ventilator kunnen bijwerken;
5. een ventilator kunnen verwijderen;
6. een ventilator kunnen vervangen;
7. de historiek kunnen nalezen;

en daarnaast:

8. erop kunnen rekenen dat dit alles een herstart overleeft.

Implementeer onderstaande functionaliteiten.

### 1. Je vertrekpunt

Vertrek van je eigen oplossing van 04_02. Je database `VentilatorDb` bevat de vier ventilatoren uit 04_02, en de tabel `Wijzigingen` is leeg.

| Id | Merk      | AfmetingInMm | Verlichting | Prijs |
|----|-----------|--------------|-------------|-------|
| 1  | Noctua    | 120          | geen        | 29.90 |
| 2  | be quiet! | 140          | geen        | 24.90 |
| 3  | Corsair   | 120          | RGB         | 34.90 |
| 4  | Arctic    | 140          | ARGB        | 19.99 |

Alle endpoints uit 04_02 moeten na deze oefening nog precies hetzelfde antwoorden, met dezelfde controles en dezelfde berichten.

> [!TIP]
> Je zal in deze oefening vaak terug willen naar de startgegevens. Maak de twee tabellen dan leeg, zet hun tellers terug, en voer het SQL-script uit 04_02 opnieuw uit. Wat je in les 04 leerde over de structuur van de database, blijft gelden: die pas je nooit met de hand aan, enkel de gegevens.

---

### 2. De contracten asynchroon

Pas `IVentilatorRepository` en `IWijzigingRepository` aan, zodat elke methode een `Task` teruggeeft en het achtervoegsel `Async` draagt.

Je twee in-memory repositories volgen deze contracten niet meer. Zoals in de les mogen ze weg.

---

### 3. Twee repositories op één context

Maak een klasse `VentilatorRepository` die `IVentilatorRepository` implementeert, en een klasse `WijzigingRepository` die `IWijzigingRepository` implementeert.

Beide krijgen je `VentilatorContext` binnen via hun constructor. De ene werkt met de tabel `Ventilatoren`, de andere met `Wijzigingen`. Gebruik overal de asynchrone methodes van Entity Framework Core.

> [!NOTE]
> Lijsten blijven gesorteerd zoals vroeger: de ventilatoren op Id, de historiek van oud naar nieuw. Een database belooft geen volgorde, tenzij je erom vraagt.

---

### 4. De controller asynchroon en de overstap

Pas de VentilatorController aan, zodat elk endpoint asynchroon werkt en op zijn repositories wacht. Laat ASP.NET Core daarna je twee nieuwe repositories aanleveren.

> [!WARNING]
> Geeft je API bij het opstarten een fout over een *scoped service* die gebruikt wordt vanuit een *singleton*? Kijk dan met welke levensduur je je repositories registreert. Een DbContext leeft één verzoek lang, en wat hem gebruikt, mag niet langer leven.

> [!IMPORTANT]
> Async geldt van de database tot in de controller. Gebruik nergens `.Result` of `.Wait()`, en nergens `async void`.

---

### 5. Wijzigingen die blijven

Voeg een ventilator toe, werk een andere bij en verwijder een derde. Herstart je API en vraag de lijst opnieuw op: alle drie de wijzigingen moeten er nog zijn. Kijk ook in je database.

> [!WARNING]
> Is een wijziging na de herstart toch weg, terwijl je API <code style="color:#37b24d;font-weight:600">201</code> of <code style="color:#37b24d;font-weight:600">204</code> gaf? Dan heeft Entity Framework Core ze wel bijgehouden, maar nooit naar de database gestuurd. Lees nog eens wanneer dat precies gebeurt.

> [!WARNING]
> Geeft je allereerste POST een <code style="color:#f03e3e;font-weight:600">500</code> met een melding over een dubbele sleutel? Dan zie je het gevolg van de waarschuwing uit 04_02, punt 5. Los het op in de gegevens, niet in je code.

> [!WARNING]
> Geeft een POST een <code style="color:#f03e3e;font-weight:600">500</code>, terwijl de nieuwe ventilator wél in je database staat? Lees dan de foutmelding in het terminalvenster van je API. Ze gaat niet over de database, maar over het adres dat je API probeert te maken voor het antwoord.

---

### 6. Alles samen: een historiek die blijft

<span style="background:#7048e8;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>SAMENGESTELDE OEFENING</b></span>

In 03_02 verdween de historiek bij elke herstart. Nu staat ze in de database, samen met de ventilatoren. In dit laatste punt bewijs je dat beide tabellen na een reeks acties en een herstart hetzelfde verhaal vertellen.

Begin met een database die exact de startgegevens bevat, met een lege historiek, en doorloop deze reeks:

1. Vraag de historiek op. Ze is leeg: `[]`.
2. Voeg deze ventilator toe:

   ```json
   {
     "id": 2,
     "merk": "Fractal",
     "afmetingInMm": 140,
     "verlichting": "geen",
     "prijs": 21.90
   }
   ```

   Je krijgt <code style="color:#37b24d;font-weight:600">201</code>, met als antwoord:

   ```json
   {
     "id": 5,
     "merk": "Fractal",
     "afmetingInMm": 140,
     "verlichting": "geen",
     "prijs": 21.90
   }
   ```

   Het meegestuurde Id werd dus overschreven. De header `Location` eindigt op `/api/ventilator/5`; hoofdletters tellen daarbij niet.
3. Werk ventilator 5 bij met dezelfde gegevens, maar een prijs van `19.90`. Je krijgt <code style="color:#37b24d;font-weight:600">204</code>.
4. Vervang ventilator 2 door deze ventilator. Je krijgt <code style="color:#37b24d;font-weight:600">201</code>, en de nieuwe ventilator krijgt Id 6.

   ```json
   { "merk": "Noctua", "afmetingInMm": 140, "verlichting": "geen", "prijs": 32.50 }
   ```

5. Verwijder ventilator 99. Je krijgt <code style="color:#f08c00;font-weight:600">404</code>.
6. Voeg een ventilator van 92 mm toe, met verder geldige gegevens. Je krijgt <code style="color:#f08c00;font-weight:600">400</code>.
7. Stop je API en start hem opnieuw.
8. <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> api/ventilator/historiek</code> geeft als resultaat:

   ```json
   [
     { "id": 1, "actie": "toegevoegd", "ventilatorId": 5 },
     { "id": 2, "actie": "bijgewerkt", "ventilatorId": 5 },
     { "id": 3, "actie": "verwijderd", "ventilatorId": 2 },
     { "id": 4, "actie": "toegevoegd", "ventilatorId": 6 }
   ]
   ```

9. <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> api/ventilator</code> geeft de ventilatoren met Id 1, 3, 4, 5 en 6, in die volgorde. Ventilator 5 kost `19.90`.
10. De tabellen `Ventilatoren` en `Wijzigingen` in je database bevatten precies hetzelfde als je API.

> [!NOTE]
> Bij een vervanging gebeuren er vier dingen: een ventilator verdwijnt, een andere komt erbij, en de historiek krijgt twee regels. Vraag je af hoeveel keer je API daarvoor iets naar de database stuurt, en wat er zou gebeuren als het halverwege misloopt. Les 07 komt daarop terug.
