# Oplossing 02_03

<span style="background:#c2255c;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>OPLOSSING</b></span> &nbsp;·&nbsp; <b>Hoofdstuk 02</b> &nbsp;·&nbsp; Soft delete en berekeningen &nbsp;·&nbsp; PC-onderdelen &nbsp;·&nbsp; bij `Oefening_02_03.md`

> [!IMPORTANT]
> Eerst zelf. Kijk per punt eerst naar de **nakijkpunten**. Haalt jouw API ze
> allemaal, dan is je punt juist, ook als je code er anders uitziet. Open de code
> pas daarna, om te vergelijken.

> [!NOTE]
> De Ids, soorten en plaatsen in de nakijkpunten komen uit de lijst van deze
> oplossing. Jouw lijst ziet er anders uit: vervang ze door een Id of een waarde uit je
> eigen lijst die in hetzelfde geval zit.

---

## 1. Het model

**Nakijkpunten**

- `Bestellijn` in Models: `Id` en `Aantal` als `int`, `Omschrijving` als `string`, `StukPrijs` als `decimal`, `IsActief` als `bool`.

<details>
<summary><span style="color:#845ef7"><b>Toon de code</b></span></summary>

```csharp
namespace BitEnByte.Models;

public class Bestellijn
{
    public int Id { get; set; }
    public string Omschrijving { get; set; } = string.Empty;
    public int Aantal { get; set; }
    public decimal StukPrijs { get; set; }
    public bool IsActief { get; set; }
}
```

</details>

> [!TIP]
> **Zo kan het beter**
>
> - `= string.Empty` geeft elke tekst een beginwaarde. Zonder die regel waarschuwt
>   Visual Studio dat de eigenschap `null` kan zijn. Voor een prijs is `decimal` de
>   beste keuze (leerboek, hoofdstuk 2). De cursus gebruikt in les 02 `double`, en dat
>   telt ook als juist.

---

## 2. De controller en de tijdelijke lijst

**Nakijkpunten**

- `/api/bestellijn`, een `static` lijst met minstens vijf lijnen, waarvan minstens één met `IsActief = false`.

**Aanpak.** De lijst is `private static`. Bij elk verzoek maakt ASP.NET Core een nieuw controllerobject; enkel een `static` lijst hoort bij de klasse zelf en blijft dus bestaan tussen twee verzoeken (leerboek, hoofdstuk 24).

<details>
<summary><span style="color:#845ef7"><b>Toon de code</b></span></summary>

```csharp
using BitEnByte.Models;
using Microsoft.AspNetCore.Mvc;

namespace BitEnByte.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BestellijnController : ControllerBase
{
    private static List<Bestellijn> lijnen = new List<Bestellijn>
    {
        new Bestellijn { Id = 1, Omschrijving = "Ryzen 5 7600X", Aantal = 1, StukPrijs = 199.00m, IsActief = true },
        new Bestellijn { Id = 2, Omschrijving = "DDR5 16 GB", Aantal = 2, StukPrijs = 54.90m, IsActief = true },
        new Bestellijn { Id = 3, Omschrijving = "B650 Tomahawk", Aantal = 1, StukPrijs = 219.00m, IsActief = true },
        new Bestellijn { Id = 4, Omschrijving = "Casefan 120 mm", Aantal = 3, StukPrijs = 14.90m, IsActief = true },
        new Bestellijn { Id = 5, Omschrijving = "Thermische pasta", Aantal = 1, StukPrijs = 8.50m, IsActief = false }
    };

    // ... hier komen de endpoints uit de punten hieronder
}
```

</details>

---

## 3. De actieve bestellijnen

**Nakijkpunten**

| Verzoek | Status | Antwoord |
|---|---|---|
| <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> /api/bestellijn</code> | <code style="color:#37b24d;font-weight:600">200 OK</code> | enkel de lijnen met `isActief: true` |

**Aanpak.** `Where` op `IsActief`, en de uitkomst als lijst teruggeven.

<details>
<summary><span style="color:#845ef7"><b>Toon de code</b></span></summary>

```csharp
[HttpGet]
public ActionResult<List<Bestellijn>> GetActief()
{
    return Ok(lijnen.Where(l => l.IsActief).ToList());
}
```

</details>

---

## 4. Alle bestellijnen

**Nakijkpunten**

| Verzoek | Status | Antwoord |
|---|---|---|
| <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> /api/bestellijn/alles</code> | <code style="color:#37b24d;font-weight:600">200 OK</code> | alle lijnen, ook de geschrapte |

**Aanpak.** De hele lijst, zonder filter.

<details>
<summary><span style="color:#845ef7"><b>Toon de code</b></span></summary>

```csharp
[HttpGet("alles")]
public ActionResult<List<Bestellijn>> GetAlles()
{
    return Ok(lijnen);
}
```

</details>

> [!NOTE]
> `alles`, `totaal` en `fiche` passen ook op de vorm van `{id}`. Toch kiest
> ASP.NET Core altijd de route met het vaste woord. Daarom botsen ze niet.

---

## 5. Eén bestellijn

**Nakijkpunten**

| Verzoek | Status | Antwoord |
|---|---|---|
| <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> /api/bestellijn/2</code> | <code style="color:#37b24d;font-weight:600">200 OK</code> | lijn 2, als ze actief is |
| <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> /api/bestellijn/5</code> | <code style="color:#f08c00;font-weight:600">404 Not Found</code> | <code style="color:#845ef7">We vonden geen actieve bestellijn met Id 5.</code> (geschrapt) |
| <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> /api/bestellijn/999</code> | <code style="color:#f08c00;font-weight:600">404 Not Found</code> | <code style="color:#845ef7">We vonden geen actieve bestellijn met Id 999.</code> |

**Aanpak.** `ZoekActief` zoekt op Id **en** op `IsActief` tegelijk. Een geschrapte lijn vindt
het dus niet, en dat is precies wat een soft delete betekent. PUT en DELETE gebruiken
dezelfde hulpmethode.

<details>
<summary><span style="color:#845ef7"><b>Toon de code</b></span></summary>

```csharp
[HttpGet("{id}")]
public ActionResult<Bestellijn> GetById(int id)
{
    Bestellijn? lijn = ZoekActief(id);

    if (lijn == null)
    {
        return NotFound($"We vonden geen actieve bestellijn met Id {id}.");
    }

    return Ok(lijn);
}

private Bestellijn? ZoekActief(int id)
{
    return lijnen.FirstOrDefault(l => l.Id == id && l.IsActief);
}
```

</details>

> [!WARNING]
> **Veelgemaakte fouten**
>
> - Eerst op Id zoeken en daarna niet meer naar `IsActief` kijken. Dan toont je API
>   een geschrapte lijn alsof er niets gebeurd is.

---

## 6. Een bestellijn toevoegen

**Nakijkpunten**

| Verzoek | Status | Antwoord |
|---|---|---|
| <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>POST</b> /api/bestellijn</code> | <code style="color:#37b24d;font-weight:600">201 Created</code> | de nieuwe lijn, altijd met `isActief: true`, ook als de body `false` meestuurt |
| <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>POST</b> /api/bestellijn</code> | <code style="color:#f08c00;font-weight:600">400 Bad Request</code> | <code style="color:#845ef7">Een bestellijn moet een omschrijving hebben.</code> |
| <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>POST</b> /api/bestellijn</code> | <code style="color:#f08c00;font-weight:600">400 Bad Request</code> | <code style="color:#845ef7">Het aantal moet minstens 1 zijn.</code> |
| <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>POST</b> /api/bestellijn</code> | <code style="color:#f08c00;font-weight:600">400 Bad Request</code> | <code style="color:#845ef7">De stukprijs moet groter zijn dan 0.</code> |

**Aanpak.** Zoals in 02_02: één `Controleer` voor POST en PUT, daarna het Id en `IsActief = true` zelf invullen, dan toevoegen.

<details>
<summary><span style="color:#845ef7"><b>Toon de code</b></span></summary>

```csharp
[HttpPost]
public ActionResult<Bestellijn> VoegToe(Bestellijn nieuw)
{
    string? fout = Controleer(nieuw);
    if (fout != null)
    {
        return BadRequest(fout);
    }

    nieuw.Id = VolgendId();
    nieuw.IsActief = true;
    lijnen.Add(nieuw);

    return CreatedAtAction(nameof(GetById), new { id = nieuw.Id }, nieuw);
}

private string? Controleer(Bestellijn lijn)
{
    if (string.IsNullOrWhiteSpace(lijn.Omschrijving))
    {
        return "Een bestellijn moet een omschrijving hebben.";
    }

    if (lijn.Aantal < 1)
    {
        return "Het aantal moet minstens 1 zijn.";
    }

    if (lijn.StukPrijs <= 0)
    {
        return "De stukprijs moet groter zijn dan 0.";
    }

    return null;
}

private int VolgendId()
{
    if (lijnen.Count == 0)
    {
        return 1;
    }

    return lijnen.Max(l => l.Id) + 1;
}
```

</details>

> [!TIP]
> **Zo kan het beter**
>
> - `VolgendId` is de manier van de cursus, de hoogste Id plus 1, met één verbetering.
>   `Max` op een lege lijst crasht. Verwijder je alle items en voeg je er daarna een
>   toe, dan gaf de code uit les 02 een 500. Hier wordt het gewoon Id 1.

---

## 7. Een bestellijn bijwerken

**Nakijkpunten**

| Verzoek | Status | Antwoord |
|---|---|---|
| <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>PUT</b> /api/bestellijn/2</code> | <code style="color:#37b24d;font-weight:600">204 No Content</code> | lege body |
| <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>PUT</b> /api/bestellijn/5</code> | <code style="color:#f08c00;font-weight:600">404 Not Found</code> | een geschrapte lijn kan je niet bijwerken |
| <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>PUT</b> /api/bestellijn/2</code> | <code style="color:#f08c00;font-weight:600">400 Bad Request</code> | een van de drie berichten uit punt 6 |

**Aanpak.** Enkel de drie gewone gegevens overnemen. `IsActief` laat je met rust: schrappen
en herstellen hebben elk hun eigen endpoint. De opgave noemt geen bericht voor de 404;
hier is het bericht van punt 5 hergebruikt.

<details>
<summary><span style="color:#845ef7"><b>Toon de code</b></span></summary>

```csharp
[HttpPut("{id}")]
public ActionResult Wijzig(int id, Bestellijn gewijzigd)
{
    Bestellijn? bestaand = ZoekActief(id);

    if (bestaand == null)
    {
        return NotFound($"We vonden geen actieve bestellijn met Id {id}.");
    }

    string? fout = Controleer(gewijzigd);
    if (fout != null)
    {
        return BadRequest(fout);
    }

    bestaand.Omschrijving = gewijzigd.Omschrijving;
    bestaand.Aantal = gewijzigd.Aantal;
    bestaand.StukPrijs = gewijzigd.StukPrijs;
    // IsActief bewust niet overnemen: schrappen en herstellen hebben hun eigen endpoint.

    return NoContent();
}
```

</details>

> [!WARNING]
> **Veelgemaakte fouten**
>
> - Ook `IsActief` overnemen. Dan kan een client met een PUT een lijn schrappen of
>   herstellen, buiten punt 8 en 9 om.

---

## 8. Een bestellijn schrappen

**Nakijkpunten**

| Verzoek | Status | Antwoord |
|---|---|---|
| <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>DELETE</b> /api/bestellijn/2</code> | <code style="color:#37b24d;font-weight:600">204 No Content</code> | lege body; lijn 2 staat nog in `/alles`, met `isActief: false` |
| <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>DELETE</b> /api/bestellijn/2</code> | <code style="color:#f08c00;font-weight:600">404 Not Found</code> | <code style="color:#845ef7">Kan bestellijn met Id 2 niet schrappen, omdat deze niet gevonden is.</code> |

**Aanpak.** Geen `Remove`: enkel `IsActief` op `false`. De lijn blijft in de lijst, voor de boekhouding.

<details>
<summary><span style="color:#845ef7"><b>Toon de code</b></span></summary>

```csharp
[HttpDelete("{id}")]
public ActionResult Schrap(int id)
{
    Bestellijn? lijn = ZoekActief(id);

    if (lijn == null)
    {
        return NotFound($"Kan bestellijn met Id {id} niet schrappen, omdat deze niet gevonden is.");
    }

    lijn.IsActief = false;

    return NoContent();
}
```

</details>

> [!WARNING]
> **Veelgemaakte fouten**
>
> - `Remove` gebruiken. Dan is het een gewone verwijdering, en kan punt 9 niets meer
>   herstellen.

---

## 9. Een bestellijn herstellen

**Nakijkpunten**

| Verzoek | Status | Antwoord |
|---|---|---|
| <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>PUT</b> /api/bestellijn/herstel/2</code> | <code style="color:#37b24d;font-weight:600">204 No Content</code> | lege body; lijn 2 is weer actief |
| <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>PUT</b> /api/bestellijn/herstel/2</code> | <code style="color:#f08c00;font-weight:600">400 Bad Request</code> | <code style="color:#845ef7">Deze bestellijn is niet geschrapt.</code> |
| <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>PUT</b> /api/bestellijn/herstel/999</code> | <code style="color:#f08c00;font-weight:600">404 Not Found</code> | de lijn bestaat niet |

**Aanpak.** Hier zoek je bewust **niet** met `ZoekActief`, want je zoekt net een geschrapte
lijn. Drie gevallen, in volgorde: bestaat niet, is niet geschrapt, herstellen.

<details>
<summary><span style="color:#845ef7"><b>Toon de code</b></span></summary>

```csharp
[HttpPut("herstel/{id}")]
public ActionResult Herstel(int id)
{
    Bestellijn? lijn = lijnen.FirstOrDefault(l => l.Id == id);

    if (lijn == null)
    {
        return NotFound($"We vonden geen bestellijn met Id {id}.");
    }

    if (lijn.IsActief)
    {
        return BadRequest("Deze bestellijn is niet geschrapt.");
    }

    lijn.IsActief = true;

    return NoContent();
}
```

</details>

---

## 10. De totale waarde

**Nakijkpunten**

| Verzoek | Status | Antwoord |
|---|---|---|
| <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> /api/bestellijn/totaal</code> | <code style="color:#37b24d;font-weight:600">200 OK</code> | één getal: de som van aantal maal stukprijs van de actieve lijnen |

- Na het schrappen van een lijn is het totaal gedaald met exact de waarde van die lijn.

**Aanpak.** `Waarde` rekent één lijn uit, `Totaal` telt een selectie op met `Sum` (leerboek,
hoofdstuk 11). Ze krijgen elk een eigen methode, omdat de fiche in punt 11 ze opnieuw
nodig heeft: één keer voor de actieve lijnen en één keer voor de geschrapte.

<details>
<summary><span style="color:#845ef7"><b>Toon de code</b></span></summary>

```csharp
[HttpGet("totaal")]
public ActionResult<decimal> GetTotaal()
{
    return Ok(Totaal(lijnen.Where(l => l.IsActief).ToList()));
}

private decimal Waarde(Bestellijn lijn)
{
    return lijn.Aantal * lijn.StukPrijs;
}

private decimal Totaal(List<Bestellijn> selectie)
{
    return selectie.Sum(l => Waarde(l));
}
```

</details>

> [!WARNING]
> **Veelgemaakte fouten**
>
> - `Sum(l => l.StukPrijs)`: dan vergeet je het aantal.
> - De geschrapte lijnen meetellen, door op de hele lijst te tellen in plaats van op
>   de actieve.

---

## 11. Alles samen: de bestelfiche en de bestelling schrappen

**Nakijkpunten**

| Verzoek | Status | Antwoord |
|---|---|---|
| <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> /api/bestellijn/fiche</code> | <code style="color:#37b24d;font-weight:600">200 OK</code> | de fiche, met de zes gegevens |
| <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>DELETE</b> /api/bestellijn</code> | <code style="color:#37b24d;font-weight:600">200 OK</code> | de bijgewerkte fiche: `isLeeg: true`, `totaalActief: 0`, `duursteActieveLijn: null` |
| <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>DELETE</b> /api/bestellijn</code> | <code style="color:#f08c00;font-weight:600">400 Bad Request</code> | <code style="color:#845ef7">Er zijn geen actieve bestellijnen om te schrappen.</code> |

- Het testscenario uit de opgave loopt van begin tot einde zonder een getal dat verspringt.

**Aanpak.** `MaakFiche` bouwt de fiche, en zowel de GET als de nieuwe DELETE roepen ze op. Ze
maakt twee lijsten, actief en geschrapt, en haalt alles uit die twee:
aantallen, totalen met `Totaal` uit punt 10, en de duurste lijn met `OrderByDescending`
en `FirstOrDefault`. Is er geen actieve lijn, dan geeft `FirstOrDefault` `null`, en
daarom staat er een `?` achter het type in `BestelFiche`. De DELETE zonder Id heeft
gewoon `[HttpDelete]` zonder tekst.

<details>
<summary><span style="color:#845ef7"><b>Toon de code</b></span></summary>

```csharp
namespace BitEnByte.Models;

public class BestelFiche
{
    public int AantalActieveLijnen { get; set; }
    public int AantalGeschrapteLijnen { get; set; }
    public decimal TotaalActief { get; set; }
    public decimal TotaalGeschrapt { get; set; }
    public Bestellijn? DuursteActieveLijn { get; set; }
    public bool IsLeeg { get; set; }
}

[HttpGet("fiche")]
public ActionResult<BestelFiche> GetFiche()
{
    return Ok(MaakFiche());
}

[HttpDelete]
public ActionResult<BestelFiche> SchrapAlles()
{
    List<Bestellijn> actief = lijnen.Where(l => l.IsActief).ToList();

    if (actief.Count == 0)
    {
        return BadRequest("Er zijn geen actieve bestellijnen om te schrappen.");
    }

    foreach (Bestellijn lijn in actief)
    {
        lijn.IsActief = false;
    }

    return Ok(MaakFiche());
}

private BestelFiche MaakFiche()
{
    List<Bestellijn> actief = lijnen.Where(l => l.IsActief).ToList();
    List<Bestellijn> geschrapt = lijnen.Where(l => !l.IsActief).ToList();

    return new BestelFiche
    {
        AantalActieveLijnen = actief.Count,
        AantalGeschrapteLijnen = geschrapt.Count,
        TotaalActief = Totaal(actief),
        TotaalGeschrapt = Totaal(geschrapt),
        DuursteActieveLijn = actief.OrderByDescending(l => Waarde(l)).FirstOrDefault(),
        IsLeeg = actief.Count == 0
    };
}
```

</details>

> [!WARNING]
> **Veelgemaakte fouten**
>
> - De fiche twee keer uitschrijven, één keer in de GET en één keer in de DELETE.
> - `NoContent()` na het schrappen van alles. De opgave vraagt een 200 met de fiche,
>   omdat er wel iets te tonen is.

> [!TIP]
> **Zo kan het beter**
>
> - `MaxBy(l => Waarde(l))` (leerboek, hoofdstuk 11) is een kortere vorm voor de
>   duurste lijn. Op een lege lijst geeft het voor een klasse ook `null`.

---

## Alles samen: de volledige bestanden

Hetzelfde als hierboven, maar in één stuk. Handig om naast je eigen bestanden te leggen.

<details>
<summary><span style="color:#845ef7"><b>Toon BestelFiche.cs</b></span></summary>

```csharp
namespace BitEnByte.Models;

public class BestelFiche
{
    public int AantalActieveLijnen { get; set; }
    public int AantalGeschrapteLijnen { get; set; }
    public decimal TotaalActief { get; set; }
    public decimal TotaalGeschrapt { get; set; }
    public Bestellijn? DuursteActieveLijn { get; set; }
    public bool IsLeeg { get; set; }
}
```

</details>

<details>
<summary><span style="color:#845ef7"><b>Toon Bestellijn.cs</b></span></summary>

```csharp
namespace BitEnByte.Models;

public class Bestellijn
{
    public int Id { get; set; }
    public string Omschrijving { get; set; } = string.Empty;
    public int Aantal { get; set; }
    public decimal StukPrijs { get; set; }
    public bool IsActief { get; set; }
}
```

</details>

<details>
<summary><span style="color:#845ef7"><b>Toon BestellijnController.cs</b></span></summary>

```csharp
using BitEnByte.Models;
using Microsoft.AspNetCore.Mvc;

namespace BitEnByte.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BestellijnController : ControllerBase
{
    private static List<Bestellijn> lijnen = new List<Bestellijn>
    {
        new Bestellijn { Id = 1, Omschrijving = "Ryzen 5 7600X", Aantal = 1, StukPrijs = 199.00m, IsActief = true },
        new Bestellijn { Id = 2, Omschrijving = "DDR5 16 GB", Aantal = 2, StukPrijs = 54.90m, IsActief = true },
        new Bestellijn { Id = 3, Omschrijving = "B650 Tomahawk", Aantal = 1, StukPrijs = 219.00m, IsActief = true },
        new Bestellijn { Id = 4, Omschrijving = "Casefan 120 mm", Aantal = 3, StukPrijs = 14.90m, IsActief = true },
        new Bestellijn { Id = 5, Omschrijving = "Thermische pasta", Aantal = 1, StukPrijs = 8.50m, IsActief = false }
    };
    [HttpGet]
    public ActionResult<List<Bestellijn>> GetActief()
    {
        return Ok(lijnen.Where(l => l.IsActief).ToList());
    }

    [HttpGet("alles")]
    public ActionResult<List<Bestellijn>> GetAlles()
    {
        return Ok(lijnen);
    }

    [HttpGet("{id}")]
    public ActionResult<Bestellijn> GetById(int id)
    {
        Bestellijn? lijn = ZoekActief(id);

        if (lijn == null)
        {
            return NotFound($"We vonden geen actieve bestellijn met Id {id}.");
        }

        return Ok(lijn);
    }

    private Bestellijn? ZoekActief(int id)
    {
        return lijnen.FirstOrDefault(l => l.Id == id && l.IsActief);
    }

    [HttpPost]
    public ActionResult<Bestellijn> VoegToe(Bestellijn nieuw)
    {
        string? fout = Controleer(nieuw);
        if (fout != null)
        {
            return BadRequest(fout);
        }

        nieuw.Id = VolgendId();
        nieuw.IsActief = true;
        lijnen.Add(nieuw);

        return CreatedAtAction(nameof(GetById), new { id = nieuw.Id }, nieuw);
    }

    private string? Controleer(Bestellijn lijn)
    {
        if (string.IsNullOrWhiteSpace(lijn.Omschrijving))
        {
            return "Een bestellijn moet een omschrijving hebben.";
        }

        if (lijn.Aantal < 1)
        {
            return "Het aantal moet minstens 1 zijn.";
        }

        if (lijn.StukPrijs <= 0)
        {
            return "De stukprijs moet groter zijn dan 0.";
        }

        return null;
    }

    private int VolgendId()
    {
        if (lijnen.Count == 0)
        {
            return 1;
        }

        return lijnen.Max(l => l.Id) + 1;
    }

    [HttpPut("{id}")]
    public ActionResult Wijzig(int id, Bestellijn gewijzigd)
    {
        Bestellijn? bestaand = ZoekActief(id);

        if (bestaand == null)
        {
            return NotFound($"We vonden geen actieve bestellijn met Id {id}.");
        }

        string? fout = Controleer(gewijzigd);
        if (fout != null)
        {
            return BadRequest(fout);
        }

        bestaand.Omschrijving = gewijzigd.Omschrijving;
        bestaand.Aantal = gewijzigd.Aantal;
        bestaand.StukPrijs = gewijzigd.StukPrijs;
        // IsActief bewust niet overnemen: schrappen en herstellen hebben hun eigen endpoint.

        return NoContent();
    }

    [HttpDelete("{id}")]
    public ActionResult Schrap(int id)
    {
        Bestellijn? lijn = ZoekActief(id);

        if (lijn == null)
        {
            return NotFound($"Kan bestellijn met Id {id} niet schrappen, omdat deze niet gevonden is.");
        }

        lijn.IsActief = false;

        return NoContent();
    }

    [HttpPut("herstel/{id}")]
    public ActionResult Herstel(int id)
    {
        Bestellijn? lijn = lijnen.FirstOrDefault(l => l.Id == id);

        if (lijn == null)
        {
            return NotFound($"We vonden geen bestellijn met Id {id}.");
        }

        if (lijn.IsActief)
        {
            return BadRequest("Deze bestellijn is niet geschrapt.");
        }

        lijn.IsActief = true;

        return NoContent();
    }

    [HttpGet("totaal")]
    public ActionResult<decimal> GetTotaal()
    {
        return Ok(Totaal(lijnen.Where(l => l.IsActief).ToList()));
    }

    private decimal Waarde(Bestellijn lijn)
    {
        return lijn.Aantal * lijn.StukPrijs;
    }

    private decimal Totaal(List<Bestellijn> selectie)
    {
        return selectie.Sum(l => Waarde(l));
    }

    [HttpGet("fiche")]
    public ActionResult<BestelFiche> GetFiche()
    {
        return Ok(MaakFiche());
    }

    [HttpDelete]
    public ActionResult<BestelFiche> SchrapAlles()
    {
        List<Bestellijn> actief = lijnen.Where(l => l.IsActief).ToList();

        if (actief.Count == 0)
        {
            return BadRequest("Er zijn geen actieve bestellijnen om te schrappen.");
        }

        foreach (Bestellijn lijn in actief)
        {
            lijn.IsActief = false;
        }

        return Ok(MaakFiche());
    }

    private BestelFiche MaakFiche()
    {
        List<Bestellijn> actief = lijnen.Where(l => l.IsActief).ToList();
        List<Bestellijn> geschrapt = lijnen.Where(l => !l.IsActief).ToList();

        return new BestelFiche
        {
            AantalActieveLijnen = actief.Count,
            AantalGeschrapteLijnen = geschrapt.Count,
            TotaalActief = Totaal(actief),
            TotaalGeschrapt = Totaal(geschrapt),
            DuursteActieveLijn = actief.OrderByDescending(l => Waarde(l)).FirstOrDefault(),
            IsLeeg = actief.Count == 0
        };
    }
}
```

</details>
