# 05_03

<b>Hoofdstuk 05</b> &nbsp;·&nbsp; Bestand of database, asynchroon &nbsp;·&nbsp; PC-onderdelen &nbsp;·&nbsp; niveau <span style="color:#f03e3e">●●●</span> &nbsp;·&nbsp; ±2 uur

## Leerdoel

Na deze oefening kan je één asynchroon contract laten volgen door twee repositories die op een totaal andere manier werken: één met een bestand en één met een database.

Je leert dat async niet enkel bij databases hoort: ook een bestand lezen of schrijven kan asynchroon, en ook daar mag je je threads niet laten wachten.

Daarnaast laat je de database het zware werk doen: filteren en sorteren gebeurt in SQL, niet in het geheugen van je API.

## 

## Opdracht

De bestellijnen-API van Bit & Byte uit 04_03 kan wisselen tussen een repository in het geheugen en een repository in een JSON-bestand, en heeft ernaast een database klaarstaan. Nu krijgt die database haar eigen repository, de derde. De versie in het geheugen verdwijnt, de bestandsversie blijft als reserve.

Jouw taak is om het contract asynchroon te maken, een repository te schrijven die met de database werkt, je bestandsrepository mee asynchroon te maken, en te bewijzen dat beide precies hetzelfde antwoorden.

Via de API moet een medewerker, net als in 04_03:

1. alle actieve en alle bestellijnen kunnen opvragen, en één bestellijn;
2. een bestellijn kunnen toevoegen, bijwerken, schrappen en herstellen;
3. de totale waarde en de bestelfiche kunnen opvragen;
4. de hele bestelling in één keer kunnen schrappen;

en daarnaast:

5. de actieve bestellijnen boven een bepaald bedrag kunnen opvragen.

Implementeer onderstaande functionaliteiten.

### 1. Je vertrekpunt

Vertrek van je eigen oplossing van 04_03. Je database `BestellijnDb` bevat de zes bestellijnen uit 04_03, en heeft geen kolom `Korting` meer.

| Id | Omschrijving         | Aantal | StukPrijs | IsActief |
|----|----------------------|--------|-----------|----------|
| 1  | Moederbord ASUS B650 | 1      | 249.99    | ja       |
| 2  | DDR5 32 GB kit       | 2      | 109.90    | ja       |
| 3  | NVMe SSD 2 TB        | 1      | 139.00    | ja       |
| 4  | Behuizing midi tower | 1      | 89.95     | nee      |
| 5  | Voeding 750 W        | 1      | 119.00    | ja       |
| 6  | Casefan 120 mm       | 3      | 14.99     | ja       |

Alle endpoints uit 04_03 moeten na deze oefening nog precies hetzelfde antwoorden, met dezelfde controles en dezelfde berichten.

> [!TIP]
> Je zal in deze oefening vaak terug willen naar de startgegevens. Maak de tabel dan leeg, zet haar teller terug, en voer het SQL-script uit 04_03 opnieuw uit. De structuur van de database pas je nooit met de hand aan, enkel de gegevens.

---

### 2. Het contract asynchroon

Pas `IBestellijnRepository` aan, zodat elke methode een `Task` teruggeeft en het achtervoegsel `Async` draagt.

Je `InMemoryBestellijnRepository` mag weg, zoals in de les.

---

### 3. De bestandsrepository asynchroon

Pas je `BestandBestellijnRepository` aan het nieuwe contract aan. Hij leest en schrijft `bestellijnen.json` voortaan asynchroon.

Een constructor kan niet wachten. Lees het bestand dus niet meer in je constructor, maar telkens wanneer een methode de gegevens nodig heeft. Bestaat het bestand dan nog niet, dan geldt nog altijd de startlijst.

> [!NOTE]
> Bestanden lezen en schrijven kost net als een database tijd waarin je thread niets anders doet. Daarom bestaan er ook voor bestanden asynchrone methodes. Zoek ze op: hun namen volgen dezelfde afspraak als die van Entity Framework Core.

> [!WARNING]
> Een methode die met `async` gemarkeerd is maar nergens wacht, compileert met een waarschuwing en werkt verder gewoon synchroon. Lees de waarschuwingen in je build: elke `async`-methode in je project hoort minstens één keer te wachten.

---

### 4. De databaserepository

Maak een klasse `BestellijnRepository` die `IBestellijnRepository` implementeert. Ze krijgt je `BestellijnContext` binnen via haar constructor en werkt met de tabel `Bestellijnen`, met de asynchrone methodes van Entity Framework Core.

Pas de BestellijnController aan, zodat elk endpoint asynchroon werkt, en laat ASP.NET Core voortaan je `BestellijnRepository` aanleveren.

> [!IMPORTANT]
> Async geldt van de database en het bestand tot in de controller. Gebruik nergens `.Result` of `.Wait()`, en nergens `async void`.

> [!NOTE]
> Lijsten blijven op Id gesorteerd, met welke repository ook, behalve waar deze opgave een andere volgorde vraagt. Een database belooft geen volgorde, tenzij je erom vraagt.

---

### 5. Boven een bedrag

Voorzie een GET-endpoint op:

<span style="background:#1c7ed6;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>GET</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/bestellijn/boven/<span style="color:#e64980"><b>{bedrag}</b></span></code>

Dit endpoint geeft de actieve bestellijnen terug waarvan de waarde, het aantal maal de stukprijs, groter is dan <code style="color:#e64980;font-weight:600">{bedrag}</code>. De duurste lijn staat eerst. Het antwoord is altijd <code style="color:#37b24d;font-weight:600">200 OK</code>, ook als de lijst leeg is.

| Verzoek | Antwoord |
|---------|----------|
| <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> api/bestellijn/boven/150</code> | de bestellijnen met Id 1 en 2, in die volgorde |
| <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> api/bestellijn/boven/1000</code> | `[]` |

> [!TIP]
> In je databaserepository laat je het filteren en het sorteren door de database doen. Kijk in het terminalvenster van je API naar de SQL bij dit verzoek: staan de vermenigvuldiging, de vergelijking en het sorteren erin?

---

### 6. Alles samen: twee repositories, één antwoord

<span style="background:#7048e8;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>SAMENGESTELDE OEFENING</b></span>

Je hebt nu twee repositories die hetzelfde asynchrone contract volgen, maar elk op een totaal andere manier werken. In dit laatste punt doorloop je dezelfde reeks met allebei. Elk antwoord moet bij de twee hetzelfde zijn, tot op de Id's.

Begin met de databaserepository en een database die exact de startgegevens bevat. Bedragen vergelijk je als getal: `0`, `0.0` en `0.00` zijn hetzelfde antwoord.

1. <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> api/bestellijn/fiche</code> geeft de fiche uit 04_03: 5 actieve en 1 geschrapte lijn, `totaalActief` `772.76`, `totaalGeschrapt` `89.95`, en lijn 1 als duurste.
2. Voeg deze lijn toe. Je krijgt <code style="color:#37b24d;font-weight:600">201</code>, en de nieuwe lijn heeft Id 7 en is actief.

   ```json
   { "omschrijving": "Koeler Noctua NH-D15", "aantal": 1, "stukPrijs": 109.95 }
   ```

3. Schrap lijn 3. Je krijgt <code style="color:#37b24d;font-weight:600">204</code>.
4. <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> api/bestellijn/fiche</code> geeft als resultaat:

   ```json
   {
     "aantalActieveLijnen": 5,
     "aantalGeschrapteLijnen": 2,
     "totaalActief": 743.71,
     "totaalGeschrapt": 228.95,
     "duursteActieveLijn": {
       "id": 1,
       "omschrijving": "Moederbord ASUS B650",
       "aantal": 1,
       "stukPrijs": 249.99,
       "isActief": true
     },
     "isLeeg": false
   }
   ```

5. <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> api/bestellijn/boven/150</code> geeft de lijnen 1 en 2, in die volgorde.
6. Stop je API en start hem opnieuw. De fiche is exact dezelfde als in stap 4.
7. Herstel lijn 3. Je krijgt <code style="color:#37b24d;font-weight:600">204</code>. Schrap daarna de hele bestelling in één keer. Je krijgt <code style="color:#37b24d;font-weight:600">200</code>, met een fiche waarin `aantalActieveLijnen` `0` is, `aantalGeschrapteLijnen` `7`, `totaalActief` `0`, `totaalGeschrapt` `972.66`, `duursteActieveLijn` `null` en `isLeeg` `true`.
8. Schrap de hele bestelling nog eens. Je krijgt <code style="color:#f08c00;font-weight:600">400</code> met <code style="color:#845ef7">Er zijn geen actieve bestellijnen om te schrappen.</code>

Wissel daarna in Program.cs naar je `BestandBestellijnRepository`, verwijder `bestellijnen.json`, start je API, en doorloop stap 1 tot en met 8 opnieuw. Elk antwoord moet hetzelfde zijn.

> [!WARNING]
> Krijgt de nieuwe lijn in stap 2 bij een van de twee een ander Id dan 7, dan klopt de teller van een van de twee niet. Bij de database is dat de teller van PostgreSQL; bij het bestand bepaal jij het volgende Id.

> [!NOTE]
> Merk op dat de controller in deze hele reeks niet één keer veranderde. Twee repositories die niets van elkaar weten, een bestand en een database, geven dezelfde antwoorden omdat ze hetzelfde contract volgen.
