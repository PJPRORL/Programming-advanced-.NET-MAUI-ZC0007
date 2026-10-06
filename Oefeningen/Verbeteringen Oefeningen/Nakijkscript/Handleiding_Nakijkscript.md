# Handleiding nakijkscript

*Programming Advanced · oefeningen 01 tot 09 en TM_02 · versie 1 · oktober 2026*

> [!NOTE]
> Het nakijkscript neemt het manuele nakijken over. Het bouwt je project, start je API, stuurt de verzoeken uit de opgave en schrijft een verslag `Nakijk_<oefening>_V#.md` en `.docx`. Het verslag zegt **wat** er fout loopt en **waar je moet kijken**. Het geeft nooit verbeterde code.

---

## 1. Wat je nodig hebt

| Wat | Voor welke oefeningen | Hoe je het controleert |
|---|---|---|
| Windows PowerShell 5.1 (staat standaard op Windows) of PowerShell 7 | alle | `$PSVersionTable.PSVersion` |
| .NET SDK, dezelfde versie als je project | alle | `dotnet --version` |
| Docker Desktop, met je PostgreSQL-container gestart | 04 tot 09 | `docker ps` |
| `dotnet-ef` | 06 tot 09 (migraties) | `dotnet ef --version` |

`dotnet-ef` installeer je eenmalig met `dotnet tool install --global dotnet-ef`.

---

## 2. Installeren

1. Zet de map `Nakijkscript` op een vaste plaats, bijvoorbeeld `C:\School\PA\Nakijkscript`. Niet in de map van een oefening.
2. Windows blokkeert scripts die van internet komen. Open PowerShell in de map en deblokkeer ze eenmalig:

   ```powershell
   Get-ChildItem -Recurse | Unblock-File
   ```

3. Mag PowerShell geen scripts uitvoeren (`running scripts is disabled on this system`)? Kies één van beide:
   - eenmalig voor je eigen account: `Set-ExecutionPolicy -Scope CurrentUser RemoteSigned`
   - of telkens bij het starten: `powershell -ExecutionPolicy Bypass -File .\Nakijk.ps1 ...`

### Wat er in de map staat

| Onderdeel | Inhoud |
|---|---|
| `Nakijk.ps1` | het script dat je start |
| `Motor\` | de motor in C#. PowerShell compileert die bij de eerste start in een venster |
| `nakijkbestanden\` | per oefening: de verzoeken, de verwachte antwoorden en de punten die je zelf nakijkt |
| `valkuilen.json` | de bekende valkuilen, met de uitleg en richting voor het verslag |
| `formaat-nakijkbestand.md` | hoe een nakijkbestand opgebouwd is, voor nieuwe hoofdstukken |

> [!CAUTION]
> `valkuilen.json` en de nakijkbestanden verklappen waar een oefening je laat struikelen. Open ze niet voor je de oefening zelf gemaakt hebt: het script heeft ze nodig, jij niet.

---

## 3. Een oefening nakijken

Sluit eerst je API in Visual Studio of VS Code: het script start ze zelf. Daarna:

```powershell
cd C:\School\PA\Nakijkscript
.\Nakijk.ps1 -Oefening 02_05 -Project C:\School\PA\Oefening_02_05
```

`-Project` is de map met het `.csproj`-bestand. Sta je al in die map, dan mag `-Project` weg.

Het script toont onderweg wat het doet, en eindigt met de score en het pad van het verslag:

```text
Nakijken van oefening 02_05 in C:\School\PA\Oefening_02_05
  Bouwen met dotnet build ...
  Verzoeken uitvoeren ...
26 van de 29 stappen juist.
Verslag: C:\School\PA\Nakijken\Nakijk_02_05_V1.md
```

### Per soort oefening

| Oefeningen | Wat het script extra doet | Let op |
|---|---|---|
| 01, 02, 03_01, 03_02, 03_04, 03_05, TM_02 | herstart je API waar de opgave dat vraagt | niets |
| 03_03, 03_06 (opslag in een bestand) | wist het gegevensbestand uit de opgave voor elke reeks verzoeken | het bestand in je projectmap wordt gewist: bewaar zelf een kopie als je het wil houden |
| 04 en 05 (database met SQL-scripts) | voert je SQL-scripts uit in de database in Docker | zie hieronder voor de volgorde van de scripts. 04_03, 04_06, 05_03 en 05_06 wissen ook een gegevensbestand |
| 06 tot 09 (migraties) | verwijdert je database en bouwt ze opnieuw op met `dotnet ef database update` | **de database wordt gewist**. Gebruik voor het nakijken geen database met gegevens die je wil houden |

### SQL-scripts (hoofdstuk 04 en 05)

Zonder extra parameter voert het script alle `.sql`-bestanden uit je projectmap uit (behalve in `bin` en `obj`), in alfabetische volgorde. Het verslag noemt de volgorde. Moet het anders, geef ze dan zelf mee:

```powershell
.\Nakijk.ps1 -Oefening 05_03 -Project C:\School\PA\Oefening_05_03 -SqlScript .\tabellen.sql, .\startgegevens.sql
```

---

## 4. Alle parameters

| Parameter | Betekenis | Standaard |
|---|---|---|
| `-Oefening` | het nummer, bv. `01_03`, `06_01`, `TM_02_04` | verplicht |
| `-Project` | de map met het `.csproj`-bestand | de map waarin je staat |
| `-Uitvoer` | de map voor de verslagen | de map `Nakijken` naast je project |
| `-Poort` | de poort waarop het script je API start | 5199 |
| `-SqlScript` | één of meer SQL-scripts, in de volgorde waarin ze moeten lopen | alle `.sql`-bestanden, alfabetisch |
| `-DockerContainer` | de naam van je PostgreSQL-container | het script zoekt zelf een container met `postgres` of poort 5432 |
| `-BasisUrl` | het adres van een API die al draait, bv. `http://localhost:5123` | het script start je API zelf |
| `-ZonderDocx` | schrijf enkel het `.md`-verslag | ook `.docx` |

> [!WARNING]
> Met `-BasisUrl` bouwt en start het script niets. Het kan je API dan ook niet herstarten en de database niet opnieuw opbouwen: reeksen die dat vragen, lopen verder op de toestand die je API al had, en het verslag zegt welke. Gebruik het enkel als het gewone nakijken niet lukt.

---

## 5. Het verslag

Elke keer dat je het script start, komt er een nieuwe versie bij: `Nakijk_02_05_V1`, `_V2`, `_V3` … Een oude versie wordt nooit overschreven.

| Deel | Wat erin staat |
|---|---|
| **In één oogopslag** | de score, een tabel per punt van de opgave, en vanaf V2 wat opgelost is, wat nog openstaat en wat nieuw is |
| **A. Wat een verkeerd antwoord geeft** | per fout: waar, een tabel *Verzoek · Je API antwoordt nu · De opgave vraagt*, waarom, richting en tips |
| **B. Werkt, maar wijkt af** | wat de opgave of de les anders vraagt, ook als de voorbeelden slagen |
| **C. Opkuis, zonder invloed op het antwoord** | bv. een overbodige `$` of een lege regel onder een attribuut |
| **D. Wat beter is dan in V…** | enkel vanaf V2 |
| **E. Zelf nakijken** | per fout de verzoeken om na het oplossen zelf te controleren, de punten die het script niet kan nakijken, en wat het script niet kon uitvoeren |
| **Bijlage** | alle stappen met hun uitkomst |

Een verzoek dat mislukt omdat een eerder verzoek mislukte, staat bij die eerste fout, met de vermelding *gevolg*. Los dus altijd de bovenste fout eerst op.

> [!IMPORTANT]
> Naast de verslagen staat telkens een `Nakijk_<oefening>_V#.json`. Het script vergelijkt een nieuwe versie daarmee. Wis je die, dan valt de vergelijking met de vorige versie weg.

---

## 6. Wat het script niet kan

- Het controleert enkel wat de opgave vraagt en met voorbeelden vastlegt. Een fout in een verzoek dat de opgave niet toont, ziet het niet.
- Punten zonder vast antwoord (naamgeving, de opbouw van je code, een uitleg in woorden) staan in deel E onder *Zelf nakijken*.
- De code-analyse zoekt bekende patronen. Ze kan een valkuil missen die er anders uitziet, en het verslag zegt *Punt X: het antwoord wijkt af* als het geen bekende oorzaak vindt. De tabel toont dan wel wat er verschilt.

---

## 7. Problemen

| Melding | Wat je doet |
|---|---|
| `Er is geen nakijkbestand voor oefening …` | Kijk het nummer na: `01_03`, niet `1_3` of `01-03`. |
| `In … staat geen .csproj` | Geef met `-Project` de map waarin het `.csproj`-bestand staat. |
| *Je API start niet* in het verslag, met `address already in use` | Je API draait nog ergens, of een ander programma gebruikt poort 5199. Sluit het, of kies een andere poort met `-Poort`. |
| *De database was niet bereikbaar voor het script* | Start Docker Desktop en je container. Vindt het script de container niet, geef dan de naam mee met `-DockerContainer`. |
| `dotnet ef` niet gevonden | Installeer `dotnet-ef` (zie deel 1) en open een nieuw venster. |
| Je paste een bestand in `Motor\` of `valkuilen.json` aan, maar het script doet nog hetzelfde | PowerShell houdt de motor bij tot je het venster sluit. Open een nieuw PowerShell-venster. |

---

## 8. Nieuwe oefeningen en hoofdstukken

Een nieuwe oefening heeft één nieuw bestand nodig: `nakijkbestanden\Nakijk_<oefening>.json`. `formaat-nakijkbestand.md` beschrijft de opbouw. Een nieuwe valkuil komt in `valkuilen.json`, met de hoofdstukken waarin ze geldt (`vanaf`, `tot`). Het script zelf hoeft daarvoor niet te veranderen.
