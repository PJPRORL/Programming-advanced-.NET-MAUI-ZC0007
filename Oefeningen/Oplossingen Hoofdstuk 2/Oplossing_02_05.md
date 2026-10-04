# Oplossing 02_05

<span style="background:#c2255c;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>OPLOSSING</b></span> &nbsp;·&nbsp; <b>Hoofdstuk 02</b> &nbsp;·&nbsp; Volledige CRUD &nbsp;·&nbsp; Fietsverhuur &nbsp;·&nbsp; bij `Oefening_02_05.md`

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

- `Fiets` in Models: `Id` en `Framemaat` als `int`, `Type` als `string`, `PrijsPerDag` als `decimal` of `double`, `IsBeschikbaar` als `bool`.

<details>
<summary><span style="color:#845ef7"><b>Toon de code</b></span></summary>

```csharp
namespace Trapdoor.Models;

public class Fiets
{
    public int Id { get; set; }
    public string Type { get; set; } = string.Empty;
    public int Framemaat { get; set; }
    public decimal PrijsPerDag { get; set; }
    public bool IsBeschikbaar { get; set; }
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

- `/api/fiets`; na een POST staat de nieuwe fiets er bij de volgende GET nog bij.

**Aanpak.** De lijst is `private static`. Bij elk verzoek maakt ASP.NET Core een nieuw controllerobject; enkel een `static` lijst hoort bij de klasse zelf en blijft dus bestaan tussen twee verzoeken (leerboek, hoofdstuk 24).

<details>
<summary><span style="color:#845ef7"><b>Toon de code</b></span></summary>

```csharp
using Trapdoor.Models;
using Microsoft.AspNetCore.Mvc;

namespace Trapdoor.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FietsController : ControllerBase
{
    private static List<Fiets> fietsen = new List<Fiets>
    {
        new Fiets { Id = 1, Type = "stadsfiets", Framemaat = 53, PrijsPerDag = 12.00m, IsBeschikbaar = true },
        new Fiets { Id = 2, Type = "e-bike", Framemaat = 56, PrijsPerDag = 29.50m, IsBeschikbaar = true },
        new Fiets { Id = 3, Type = "racefiets", Framemaat = 58, PrijsPerDag = 22.00m, IsBeschikbaar = false },
        new Fiets { Id = 4, Type = "mountainbike", Framemaat = 48, PrijsPerDag = 18.00m, IsBeschikbaar = true }
    };

    // ... hier komen de endpoints uit de punten hieronder
}
```

</details>

> [!WARNING]
> **Veelgemaakte fouten**
>
> - De lijst zonder `static`: de nieuwe fiets is bij het volgende verzoek weg.

---

## 3. Alles ophalen en één ophalen

**Nakijkpunten**

| Verzoek | Status | Antwoord |
|---|---|---|
| <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> /api/fiets</code> | <code style="color:#37b24d;font-weight:600">200 OK</code> | de volledige lijst |
| <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> /api/fiets/999</code> | <code style="color:#f08c00;font-weight:600">404 Not Found</code> | <code style="color:#845ef7">We vonden geen fiets met Id 999.</code> |

<details>
<summary><span style="color:#845ef7"><b>Toon de code</b></span></summary>

```csharp
[HttpGet]
public ActionResult<List<Fiets>> GetAlle()
{
    return Ok(fietsen);
}

[HttpGet("{id}")]
public ActionResult<Fiets> GetById(int id)
{
    Fiets? fiets = fietsen.FirstOrDefault(f => f.Id == id);

    if (fiets == null)
    {
        return NotFound($"We vonden geen fiets met Id {id}.");
    }

    return Ok(fiets);
}
```

</details>

---

## 4. Een fiets toevoegen

**Nakijkpunten**

| Verzoek | Status | Antwoord |
|---|---|---|
| <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>POST</b> /api/fiets</code> | <code style="color:#37b24d;font-weight:600">201 Created</code> | de nieuwe fiets, met een Id van de API en het adres in de header *Location* |
| <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>POST</b> /api/fiets</code> | <code style="color:#f08c00;font-weight:600">400 Bad Request</code> | <code style="color:#845ef7">Een fiets moet een type hebben.</code> |
| <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>POST</b> /api/fiets</code> | <code style="color:#f08c00;font-weight:600">400 Bad Request</code> | <code style="color:#845ef7">De framemaat moet tussen 44 en 62 liggen.</code> (bij 43 of 70) |
| <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>POST</b> /api/fiets</code> | <code style="color:#37b24d;font-weight:600">201 Created</code> | bij framemaat 44 of 62: de grenzen zelf zijn toegelaten |
| <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>POST</b> /api/fiets</code> | <code style="color:#f08c00;font-weight:600">400 Bad Request</code> | <code style="color:#845ef7">De prijs per dag moet groter zijn dan 0.</code> |

**Aanpak.** Eén `Controleer` voor POST, PUT en de vervanging. De framemaat is fout als ze
kleiner is dan 44 **of** groter dan 62. Pas als alles klopt, krijgt de fiets een Id.

<details>
<summary><span style="color:#845ef7"><b>Toon de code</b></span></summary>

```csharp
[HttpPost]
public ActionResult<Fiets> VoegToe(Fiets nieuw)
{
    string? fout = Controleer(nieuw);
    if (fout != null)
    {
        return BadRequest(fout);
    }

    nieuw.Id = VolgendId();
    fietsen.Add(nieuw);

    return CreatedAtAction(nameof(GetById), new { id = nieuw.Id }, nieuw);
}

// Geeft de foutboodschap terug, of null als alles in orde is.
private string? Controleer(Fiets fiets)
{
    if (string.IsNullOrWhiteSpace(fiets.Type))
    {
        return "Een fiets moet een type hebben.";
    }

    if (fiets.Framemaat < 44 || fiets.Framemaat > 62)
    {
        return "De framemaat moet tussen 44 en 62 liggen.";
    }

    if (fiets.PrijsPerDag <= 0)
    {
        return "De prijs per dag moet groter zijn dan 0.";
    }

    return null;
}

private int VolgendId()
{
    if (fietsen.Count == 0)
    {
        return 1;
    }

    return fietsen.Max(f => f.Id) + 1;
}
```

</details>

> [!WARNING]
> **Veelgemaakte fouten**
>
> - `fiets.Framemaat <= 44 || fiets.Framemaat >= 62`: dan zijn 44 en 62 zelf verboden,
>   terwijl *tussen 44 en 62* ze toelaat.
> - `&&` in plaats van `||`. Een maat kan niet tegelijk kleiner dan 44 en groter dan
>   62 zijn, dus dan geraakt alles erdoor.

> [!TIP]
> **Zo kan het beter**
>
> - `VolgendId` is de manier van de cursus, de hoogste Id plus 1, met één verbetering.
>   `Max` op een lege lijst crasht. Verwijder je alle items en voeg je er daarna een
>   toe, dan gaf de code uit les 02 een 500. Hier wordt het gewoon Id 1.

> [!NOTE]
> Dankzij `[ApiController]` krijg je soms een 400 die niet van jou komt. Stuur je een
> tekst helemaal niet mee of als `null`, dan weigert ASP.NET Core het verzoek zelf, met
> een eigen foutmelding, nog voor je methode draait. Je eigen bericht zie je bij een
> lege tekst `""`. Met `= string.Empty` in je model krijg je het ook als de tekst
> ontbreekt.

---

## 5. Een fiets bijwerken

**Nakijkpunten**

| Verzoek | Status | Antwoord |
|---|---|---|
| <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>PUT</b> /api/fiets/1</code> | <code style="color:#37b24d;font-weight:600">204 No Content</code> | lege body |
| <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>PUT</b> /api/fiets/999</code> | <code style="color:#f08c00;font-weight:600">404 Not Found</code> | <code style="color:#845ef7">Kan geen fiets bijwerken met Id 999, omdat deze niet bestaat.</code> |
| <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>PUT</b> /api/fiets/1</code> | <code style="color:#f08c00;font-weight:600">400 Bad Request</code> | een van de drie berichten uit punt 4 |

**Aanpak.** Zoeken, controleren, en elke eigenschap behalve het Id overnemen, ook `IsBeschikbaar`.

<details>
<summary><span style="color:#845ef7"><b>Toon de code</b></span></summary>

```csharp
[HttpPut("{id}")]
public ActionResult Wijzig(int id, Fiets gewijzigd)
{
    Fiets? bestaand = fietsen.FirstOrDefault(f => f.Id == id);

    if (bestaand == null)
    {
        return NotFound($"Kan geen fiets bijwerken met Id {id}, omdat deze niet bestaat.");
    }

    string? fout = Controleer(gewijzigd);
    if (fout != null)
    {
        return BadRequest(fout);
    }

    bestaand.Type = gewijzigd.Type;
    bestaand.Framemaat = gewijzigd.Framemaat;
    bestaand.PrijsPerDag = gewijzigd.PrijsPerDag;
    bestaand.IsBeschikbaar = gewijzigd.IsBeschikbaar;

    return NoContent();
}
```

</details>

> [!WARNING]
> **Veelgemaakte fouten**
>
> - `bestaand = gewijzigd;` in plaats van de eigenschappen over te nemen: de fiets in
>   de lijst verandert dan niet (leerboek, hoofdstuk 16).

---

## 6. Een fiets verwijderen

**Nakijkpunten**

| Verzoek | Status | Antwoord |
|---|---|---|
| <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>DELETE</b> /api/fiets/3</code> | <code style="color:#37b24d;font-weight:600">204 No Content</code> | lege body |
| <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>DELETE</b> /api/fiets/3</code> | <code style="color:#f08c00;font-weight:600">404 Not Found</code> | <code style="color:#845ef7">Kan fiets met Id 3 niet verwijderen, omdat deze niet gevonden is.</code> |

<details>
<summary><span style="color:#845ef7"><b>Toon de code</b></span></summary>

```csharp
[HttpDelete("{id}")]
public ActionResult Verwijder(int id)
{
    Fiets? fiets = fietsen.FirstOrDefault(f => f.Id == id);

    if (fiets == null)
    {
        return NotFound($"Kan fiets met Id {id} niet verwijderen, omdat deze niet gevonden is.");
    }

    fietsen.Remove(fiets);

    return NoContent();
}
```

</details>

---

## 7. Testen

**Nakijkpunten**

| Verzoek | Status | Antwoord |
|---|---|---|
| <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>POST</b> /api/fiets</code> | <code style="color:#f08c00;font-weight:600">400 Bad Request</code> | met `"framemaat": 70`; een GET daarna toont ze niet |
| <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>DELETE</b> /api/fiets/3</code> | <code style="color:#f08c00;font-weight:600">404 Not Found</code> | de tweede keer op hetzelfde Id |

**Aanpak.** Geen nieuwe code. Haal je deze twee, dan werken je controles en je verwijdering.

---

## 8. Alles samen: een fiets vervangen

**Nakijkpunten**

| Verzoek | Status | Antwoord |
|---|---|---|
| <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>PUT</b> /api/fiets/vervang/999</code> | <code style="color:#f08c00;font-weight:600">404 Not Found</code> | <code style="color:#845ef7">Kan fiets met Id 999 niet vervangen, omdat deze niet gevonden is.</code> |
| <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>PUT</b> /api/fiets/vervang/2</code> | <code style="color:#f08c00;font-weight:600">400 Bad Request</code> | met framemaat 70; fiets 2 staat er daarna nog |
| <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>PUT</b> /api/fiets/vervang/2</code> | <code style="color:#37b24d;font-weight:600">201 Created</code> | de nieuwe fiets met een nieuw Id; fiets 2 geeft daarna 404 |

**Aanpak.** Eerst alles controleren zonder iets te veranderen, dan pas de lijst aanpassen. Het
nieuwe Id wordt gekozen voor de oude fiets eruit gaat, zodat een Id nooit twee keer
uitgedeeld wordt.

<details>
<summary><span style="color:#845ef7"><b>Toon de code</b></span></summary>

```csharp
[HttpPut("vervang/{id}")]
public ActionResult<Fiets> Vervang(int id, Fiets nieuw)
{
    // 1. Eerst alles controleren, nog niets veranderen.
    Fiets? oud = fietsen.FirstOrDefault(f => f.Id == id);

    if (oud == null)
    {
        return NotFound($"Kan fiets met Id {id} niet vervangen, omdat deze niet gevonden is.");
    }

    string? fout = Controleer(nieuw);
    if (fout != null)
    {
        return BadRequest(fout);
    }

    // 2. Pas als alles in orde is, de lijst aanpassen.
    nieuw.Id = VolgendId();
    fietsen.Remove(oud);
    fietsen.Add(nieuw);

    return CreatedAtAction(nameof(GetById), new { id = nieuw.Id }, nieuw);
}
```

</details>

> [!WARNING]
> **Veelgemaakte fouten**
>
> - Eerst verwijderen, dan controleren: bij een 400 is de oude fiets dan al weg.

---

## Alles samen: de volledige bestanden

Hetzelfde als hierboven, maar in één stuk. Handig om naast je eigen bestanden te leggen.

<details>
<summary><span style="color:#845ef7"><b>Toon Fiets.cs</b></span></summary>

```csharp
namespace Trapdoor.Models;

public class Fiets
{
    public int Id { get; set; }
    public string Type { get; set; } = string.Empty;
    public int Framemaat { get; set; }
    public decimal PrijsPerDag { get; set; }
    public bool IsBeschikbaar { get; set; }
}
```

</details>

<details>
<summary><span style="color:#845ef7"><b>Toon FietsController.cs</b></span></summary>

```csharp
using Trapdoor.Models;
using Microsoft.AspNetCore.Mvc;

namespace Trapdoor.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FietsController : ControllerBase
{
    private static List<Fiets> fietsen = new List<Fiets>
    {
        new Fiets { Id = 1, Type = "stadsfiets", Framemaat = 53, PrijsPerDag = 12.00m, IsBeschikbaar = true },
        new Fiets { Id = 2, Type = "e-bike", Framemaat = 56, PrijsPerDag = 29.50m, IsBeschikbaar = true },
        new Fiets { Id = 3, Type = "racefiets", Framemaat = 58, PrijsPerDag = 22.00m, IsBeschikbaar = false },
        new Fiets { Id = 4, Type = "mountainbike", Framemaat = 48, PrijsPerDag = 18.00m, IsBeschikbaar = true }
    };
    [HttpGet]
    public ActionResult<List<Fiets>> GetAlle()
    {
        return Ok(fietsen);
    }

    [HttpGet("{id}")]
    public ActionResult<Fiets> GetById(int id)
    {
        Fiets? fiets = fietsen.FirstOrDefault(f => f.Id == id);

        if (fiets == null)
        {
            return NotFound($"We vonden geen fiets met Id {id}.");
        }

        return Ok(fiets);
    }

    [HttpPost]
    public ActionResult<Fiets> VoegToe(Fiets nieuw)
    {
        string? fout = Controleer(nieuw);
        if (fout != null)
        {
            return BadRequest(fout);
        }

        nieuw.Id = VolgendId();
        fietsen.Add(nieuw);

        return CreatedAtAction(nameof(GetById), new { id = nieuw.Id }, nieuw);
    }

    // Geeft de foutboodschap terug, of null als alles in orde is.
    private string? Controleer(Fiets fiets)
    {
        if (string.IsNullOrWhiteSpace(fiets.Type))
        {
            return "Een fiets moet een type hebben.";
        }

        if (fiets.Framemaat < 44 || fiets.Framemaat > 62)
        {
            return "De framemaat moet tussen 44 en 62 liggen.";
        }

        if (fiets.PrijsPerDag <= 0)
        {
            return "De prijs per dag moet groter zijn dan 0.";
        }

        return null;
    }

    private int VolgendId()
    {
        if (fietsen.Count == 0)
        {
            return 1;
        }

        return fietsen.Max(f => f.Id) + 1;
    }

    [HttpPut("{id}")]
    public ActionResult Wijzig(int id, Fiets gewijzigd)
    {
        Fiets? bestaand = fietsen.FirstOrDefault(f => f.Id == id);

        if (bestaand == null)
        {
            return NotFound($"Kan geen fiets bijwerken met Id {id}, omdat deze niet bestaat.");
        }

        string? fout = Controleer(gewijzigd);
        if (fout != null)
        {
            return BadRequest(fout);
        }

        bestaand.Type = gewijzigd.Type;
        bestaand.Framemaat = gewijzigd.Framemaat;
        bestaand.PrijsPerDag = gewijzigd.PrijsPerDag;
        bestaand.IsBeschikbaar = gewijzigd.IsBeschikbaar;

        return NoContent();
    }

    [HttpDelete("{id}")]
    public ActionResult Verwijder(int id)
    {
        Fiets? fiets = fietsen.FirstOrDefault(f => f.Id == id);

        if (fiets == null)
        {
            return NotFound($"Kan fiets met Id {id} niet verwijderen, omdat deze niet gevonden is.");
        }

        fietsen.Remove(fiets);

        return NoContent();
    }

    [HttpPut("vervang/{id}")]
    public ActionResult<Fiets> Vervang(int id, Fiets nieuw)
    {
        // 1. Eerst alles controleren, nog niets veranderen.
        Fiets? oud = fietsen.FirstOrDefault(f => f.Id == id);

        if (oud == null)
        {
            return NotFound($"Kan fiets met Id {id} niet vervangen, omdat deze niet gevonden is.");
        }

        string? fout = Controleer(nieuw);
        if (fout != null)
        {
            return BadRequest(fout);
        }

        // 2. Pas als alles in orde is, de lijst aanpassen.
        nieuw.Id = VolgendId();
        fietsen.Remove(oud);
        fietsen.Add(nieuw);

        return CreatedAtAction(nameof(GetById), new { id = nieuw.Id }, nieuw);
    }
}
```

</details>
