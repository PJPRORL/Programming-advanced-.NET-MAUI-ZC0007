# 05_05

<b>Hoofdstuk 05</b> &nbsp;·&nbsp; CRUD op de database, asynchroon &nbsp;·&nbsp; Fietsverhuur &nbsp;·&nbsp; niveau <span style="color:#f08c00">●●○</span> &nbsp;·&nbsp; ±90 min

## Leerdoel

Na deze oefening kan je toevoegen, wijzigen en verwijderen via een repository die met de database praat, en weet je wanneer die wijzigingen echt in de database terechtkomen.

Je leert twee repositories op dezelfde DbContext laten werken, en de hele keten asynchroon maken, van de database tot in de controller.

Daarnaast kan je botsen op twee fouten die je pas nu tegenkomt: een teller die niet weet welke Id's al bestaan, en een antwoord dat mislukt nadat de gegevens al bewaard zijn.

## 

## Opdracht

In 04_05 zette je naast je fietsen-API een database op met twee tabellen, en zorgde je dat je API en je database hetzelfde verhaal vertelden. Nu neemt de database het over, voor de fietsen én voor de verhuringen.

Jouw taak is om twee repositories te schrijven die met de database werken, de hele keten asynchroon te maken, en je API te laten overstappen zonder dat de balie iets merkt, behalve dat er niets meer verdwijnt bij een herstart.

Via de API moet de balie, net als in 04_05:

1. alle fietsen en één fiets kunnen opvragen;
2. een fiets kunnen toevoegen, bijwerken, verwijderen en vervangen;
3. een fiets kunnen verhuren en terugbrengen;
4. de verhuringen kunnen nalezen;

en daarnaast:

5. erop kunnen rekenen dat dit alles een herstart overleeft.

Implementeer onderstaande functionaliteiten.

### 1. Je vertrekpunt

Vertrek van je eigen oplossing van 04_05. Je database `FietsverhuurDb` bevat de vier fietsen uit 04_05, en de tabel `Verhuringen` is leeg.

| Id | Type         | Framemaat | PrijsPerDag | IsBeschikbaar |
|----|--------------|-----------|-------------|---------------|
| 1  | stadsfiets   | 54        | 12.50       | ja            |
| 2  | mountainbike | 48        | 18.00       | ja            |
| 3  | e-bike       | 56        | 25.00       | nee           |
| 4  | bakfiets     | 52        | 30.00       | ja            |

Alle endpoints uit 04_05 moeten na deze oefening nog precies hetzelfde antwoorden, met dezelfde controles en dezelfde berichten.

> [!TIP]
> Je zal in deze oefening vaak terug willen naar de startgegevens. Maak de twee tabellen dan leeg, zet hun tellers terug, en voer het SQL-script uit 04_05 opnieuw uit. Wat je in les 04 leerde over de structuur van de database, blijft gelden: die pas je nooit met de hand aan, enkel de gegevens.

---

### 2. De contracten asynchroon

Pas `IFietsRepository` en `IVerhuringRepository` aan, zodat elke methode een `Task` teruggeeft en het achtervoegsel `Async` draagt.

Je twee in-memory repositories volgen deze contracten niet meer. Zoals in de les mogen ze weg.

---

### 3. Twee repositories op één context

Maak een klasse `FietsRepository` die `IFietsRepository` implementeert, en een klasse `VerhuringRepository` die `IVerhuringRepository` implementeert.

Beide krijgen je `FietsverhuurContext` binnen via hun constructor. De ene werkt met de tabel `Fietsen`, de andere met `Verhuringen`. Gebruik overal de asynchrone methodes van Entity Framework Core.

> [!NOTE]
> Lijsten blijven gesorteerd zoals vroeger: de fietsen op Id, de verhuringen van oud naar nieuw. Een database belooft geen volgorde, tenzij je erom vraagt.

---

### 4. De controller asynchroon en de overstap

Pas de FietsController aan, zodat elk endpoint asynchroon werkt en op zijn repositories wacht. Laat ASP.NET Core daarna je twee nieuwe repositories aanleveren.

> [!WARNING]
> Geeft je API bij het opstarten een fout over een *scoped service* die gebruikt wordt vanuit een *singleton*? Kijk dan met welke levensduur je je repositories registreert. Een DbContext leeft één verzoek lang, en wat hem gebruikt, mag niet langer leven.

> [!IMPORTANT]
> Async geldt van de database tot in de controller. Gebruik nergens `.Result` of `.Wait()`, en nergens `async void`.

---

### 5. Wijzigingen die blijven

Voeg een fiets toe, werk een andere bij, verhuur een derde en verwijder een vierde die beschikbaar is. Herstart je API en vraag de lijst opnieuw op: alles moet er nog zijn zoals je het achterliet. Kijk ook in je database.

> [!WARNING]
> Is een wijziging na de herstart toch weg, terwijl je API <code style="color:#37b24d;font-weight:600">201</code> of <code style="color:#37b24d;font-weight:600">204</code> gaf? Dan heeft Entity Framework Core ze wel bijgehouden, maar nooit naar de database gestuurd. Lees nog eens wanneer dat precies gebeurt.

> [!WARNING]
> Geeft je allereerste POST een <code style="color:#f03e3e;font-weight:600">500</code> met een melding over een dubbele sleutel? Dan zie je het gevolg van de waarschuwing uit 04_05, punt 5. Los het op in de gegevens, niet in je code.

> [!WARNING]
> Geeft een POST een <code style="color:#f03e3e;font-weight:600">500</code>, terwijl de nieuwe fiets wél in je database staat? Lees dan de foutmelding in het terminalvenster van je API. Ze gaat niet over de database, maar over het adres dat je API probeert te maken voor het antwoord.

---

### 6. Alles samen: verhuringen die blijven

<span style="background:#7048e8;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>SAMENGESTELDE OEFENING</b></span>

In 03_05 verdwenen de verhuringen bij elke herstart, en wist niemand nog welke fiets er buiten stond. Nu staan beide in de database. In dit laatste punt bewijs je dat de fietsen en de verhuringen na een reeks acties en een herstart hetzelfde verhaal vertellen.

Begin met een database die exact de startgegevens bevat, met een lege lijst verhuringen, en doorloop deze reeks:

1. Vraag de verhuringen op. De lijst is leeg: `[]`.
2. Voeg deze fiets toe:

   ```json
   {
     "id": 2,
     "type": "racefiets",
     "framemaat": 58,
     "prijsPerDag": 22.50,
     "isBeschikbaar": false
   }
   ```

   Je krijgt <code style="color:#37b24d;font-weight:600">201</code>, met als antwoord:

   ```json
   {
     "id": 5,
     "type": "racefiets",
     "framemaat": 58,
     "prijsPerDag": 22.50,
     "isBeschikbaar": true
   }
   ```

   Het meegestuurde Id werd dus overschreven, en de fiets is beschikbaar. De header `Location` eindigt op `/api/fiets/5`; hoofdletters tellen daarbij niet.
3. Verhuur fiets 5. Je krijgt <code style="color:#37b24d;font-weight:600">204</code>.
4. Verhuur fiets 1. Je krijgt <code style="color:#37b24d;font-weight:600">204</code>.
5. Breng fiets 5 terug. Je krijgt <code style="color:#37b24d;font-weight:600">204</code>.
6. Verwijder fiets 1. Je krijgt <code style="color:#f08c00;font-weight:600">400</code> met <code style="color:#845ef7">Een verhuurde fiets kan je niet uit de vloot halen.</code>
7. Verhuur fiets 3. Je krijgt <code style="color:#f08c00;font-weight:600">400</code> met <code style="color:#845ef7">Deze fiets is al verhuurd.</code>
8. Stop je API en start hem opnieuw.
9. <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> api/fiets/verhuringen</code> geeft als resultaat:

   ```json
   [
     { "id": 1, "actie": "verhuurd", "fietsId": 5 },
     { "id": 2, "actie": "verhuurd", "fietsId": 1 },
     { "id": 3, "actie": "teruggebracht", "fietsId": 5 }
   ]
   ```

10. Vraag fiets 1 en fiets 5 op. Fiets 1 is niet beschikbaar, fiets 5 wel.
11. De tabellen `Fietsen` en `Verhuringen` in je database bevatten precies hetzelfde als je API.

> [!NOTE]
> Bij een verhuring gebeuren er twee dingen: de fiets verandert, en er komt een verhuring bij. Vraag je af hoeveel keer je API daarvoor iets naar de database stuurt, en wat er zou gebeuren als het halverwege misloopt. Les 07 komt daarop terug.
