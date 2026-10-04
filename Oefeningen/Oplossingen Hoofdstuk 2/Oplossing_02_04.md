# Oplossing 02_04

<span style="background:#c2255c;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>OPLOSSING</b></span> &nbsp;·&nbsp; <b>Hoofdstuk 02</b> &nbsp;·&nbsp; Model, lijst en statuscodes &nbsp;·&nbsp; Dierenasiel &nbsp;·&nbsp; bij `Oefening_02_04.md`

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

- `Dier` in Models: `Id`, `LeeftijdInJaren` en `Verblijfsnummer` als `int`, `Naam` en `Soort` als `string`.

<details>
<summary><span style="color:#845ef7"><b>Toon de code</b></span></summary>

```csharp
namespace Knuffelhof.Models;

public class Dier
{
    public int Id { get; set; }
    public string Naam { get; set; } = string.Empty;
    public string Soort { get; set; } = string.Empty;
    public int LeeftijdInJaren { get; set; }
    public int Verblijfsnummer { get; set; }
}
```

</details>

> [!TIP]
> **Zo kan het beter**
>
> - `= string.Empty` geeft elke tekst een beginwaarde. Zonder die regel waarschuwt
>   Visual Studio dat de eigenschap `null` kan zijn.

---

## 2. De controller en de tijdelijke lijst

**Nakijkpunten**

- `/api/dier`, een `static` lijst met minstens zes dieren, drie soorten en twee dieren jonger dan twee jaar.

**Aanpak.** De lijst is `private static`. Bij elk verzoek maakt ASP.NET Core een nieuw controllerobject; enkel een `static` lijst hoort bij de klasse zelf en blijft dus bestaan tussen twee verzoeken (leerboek, hoofdstuk 24).

<details>
<summary><span style="color:#845ef7"><b>Toon de code</b></span></summary>

```csharp
using Knuffelhof.Models;
using Microsoft.AspNetCore.Mvc;

namespace Knuffelhof.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DierController : ControllerBase
{
    private static List<Dier> dieren = new List<Dier>
    {
        new Dier { Id = 1, Naam = "Bo", Soort = "hond", LeeftijdInJaren = 6, Verblijfsnummer = 12 },
        new Dier { Id = 2, Naam = "Luna", Soort = "hond", LeeftijdInJaren = 1, Verblijfsnummer = 14 },
        new Dier { Id = 3, Naam = "Minou", Soort = "kat", LeeftijdInJaren = 9, Verblijfsnummer = 3 },
        new Dier { Id = 4, Naam = "Pluis", Soort = "kat", LeeftijdInJaren = 0, Verblijfsnummer = 4 },
        new Dier { Id = 5, Naam = "Snuf", Soort = "konijn", LeeftijdInJaren = 3, Verblijfsnummer = 21 },
        new Dier { Id = 6, Naam = "Kiwi", Soort = "parkiet", LeeftijdInJaren = 2, Verblijfsnummer = 30 }
    };

    // ... hier komen de endpoints uit de punten hieronder
}
```

</details>

---

## 3. Alle dieren opvragen

**Nakijkpunten**

| Verzoek | Status | Antwoord |
|---|---|---|
| <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> /api/dier</code> | <code style="color:#37b24d;font-weight:600">200 OK</code> | de volledige lijst |

<details>
<summary><span style="color:#845ef7"><b>Toon de code</b></span></summary>

```csharp
[HttpGet]
public ActionResult<List<Dier>> GetAlle()
{
    return Ok(dieren);
}
```

</details>

---

## 4. Eén dier opvragen

**Nakijkpunten**

| Verzoek | Status | Antwoord |
|---|---|---|
| <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> /api/dier/3</code> | <code style="color:#37b24d;font-weight:600">200 OK</code> | dier 3 |
| <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> /api/dier/999</code> | <code style="color:#f08c00;font-weight:600">404 Not Found</code> | <code style="color:#845ef7">We vonden geen dier met Id 999.</code> |

**Aanpak.** `FirstOrDefault`, een controle op `null`, en `NotFound` of `Ok`.

<details>
<summary><span style="color:#845ef7"><b>Toon de code</b></span></summary>

```csharp
[HttpGet("{id}")]
public ActionResult<Dier> GetById(int id)
{
    Dier? dier = dieren.FirstOrDefault(d => d.Id == id);

    if (dier == null)
    {
        return NotFound($"We vonden geen dier met Id {id}.");
    }

    return Ok(dier);
}
```

</details>

> [!WARNING]
> **Veelgemaakte fouten**
>
> - `First` in plaats van `FirstOrDefault`: een onbestaand Id geeft dan een 500.

---

## 5. Dieren van één soort opvragen

**Nakijkpunten**

| Verzoek | Status | Antwoord |
|---|---|---|
| <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> /api/dier/soort/kat</code> | <code style="color:#37b24d;font-weight:600">200 OK</code> | enkel de katten |
| <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> /api/dier/soort/cavia</code> | <code style="color:#f08c00;font-weight:600">404 Not Found</code> | <code style="color:#845ef7">We hebben op dit moment geen cavia in het asiel.</code> |

**Aanpak.** `Where` op de soort, `ToList`, en een 404 als de lijst leeg is. `OrdinalIgnoreCase` laat ook `Kat` werken.

<details>
<summary><span style="color:#845ef7"><b>Toon de code</b></span></summary>

```csharp
[HttpGet("soort/{soort}")]
public ActionResult<List<Dier>> GetPerSoort(string soort)
{
    List<Dier> resultaat = dieren
        .Where(d => d.Soort.Equals(soort, StringComparison.OrdinalIgnoreCase))
        .ToList();

    if (resultaat.Count == 0)
    {
        return NotFound($"We hebben op dit moment geen {soort} in het asiel.");
    }

    return Ok(resultaat);
}
```

</details>

> [!NOTE]
> De routes lijken op elkaar, maar botsen niet. `soort/{soort}` heeft twee stukken, `{id}` maar één, dus een verzoek past nooit bij allebei. En bij `jong` tegenover `{id}` kiest ASP.NET Core altijd de route met het vaste woord. Een echte botsing krijg je pas met `[HttpGet("{soort}")]` zonder het woord `soort` ervoor: dan passen twee methodes op hetzelfde adres, en dat geeft een 500 (leerboek, hoofdstuk 25).

---

## 6. Enkel de jonge dieren

**Nakijkpunten**

| Verzoek | Status | Antwoord |
|---|---|---|
| <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> /api/dier/jong</code> | <code style="color:#37b24d;font-weight:600">200 OK</code> | enkel dieren jonger dan twee jaar |
| <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> /api/dier/jong</code> | <code style="color:#37b24d;font-weight:600">200 OK</code> | een lege lijst `[]`, als er geen jonge dieren zijn |

**Aanpak.** De regel *jonger dan twee jaar* staat in één methode, `IsJong`. De fiche in punt 7
gebruikt dezelfde regel, en zo kunnen de twee nooit uit elkaar lopen.

<details>
<summary><span style="color:#845ef7"><b>Toon de code</b></span></summary>

```csharp
[HttpGet("jong")]
public ActionResult<List<Dier>> GetJong()
{
    return Ok(dieren.Where(d => IsJong(d)).ToList());
}

private bool IsJong(Dier dier)
{
    return dier.LeeftijdInJaren < 2;
}
```

</details>

> [!WARNING]
> **Veelgemaakte fouten**
>
> - `<= 2` in plaats van `< 2`. Dan is een dier van precies twee jaar ook jong.

---

## 7. Alles samen: de dierfiche

**Nakijkpunten**

| Verzoek | Status | Antwoord |
|---|---|---|
| <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> /api/dier/fiche/5</code> | <code style="color:#37b24d;font-weight:600">200 OK</code> | bij een dier dat alleen is van zijn soort staat `aantalVanDezelfdeSoort` op 0 |
| <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> /api/dier/fiche/999</code> | <code style="color:#f08c00;font-weight:600">404 Not Found</code> | <code style="color:#845ef7">We vonden geen dier met Id 999.</code> |

- Van twee dieren van dezelfde soort, met een verschillende leeftijd, heeft er maar één `isOudsteVanDeSoort` op `true`.

**Aanpak.** Eerst het dier, met de 404 van punt 4. Dan één lijst met de **andere** dieren van
dezelfde soort, en daaruit het aantal, en of er eentje ouder is.

<details>
<summary><span style="color:#845ef7"><b>Toon de code</b></span></summary>

```csharp
namespace Knuffelhof.Models;

public class DierFiche
{
    public Dier Dier { get; set; } = new Dier();
    public bool IsJong { get; set; }
    public int AantalVanDezelfdeSoort { get; set; }
    public bool IsOudsteVanDeSoort { get; set; }
}

[HttpGet("fiche/{id}")]
public ActionResult<DierFiche> GetFiche(int id)
{
    Dier? dier = dieren.FirstOrDefault(d => d.Id == id);

    if (dier == null)
    {
        return NotFound($"We vonden geen dier met Id {id}.");
    }

    List<Dier> andereVanDezelfdeSoort = dieren
        .Where(d => d.Soort == dier.Soort && d.Id != dier.Id)
        .ToList();

    DierFiche fiche = new DierFiche
    {
        Dier = dier,
        IsJong = IsJong(dier),
        AantalVanDezelfdeSoort = andereVanDezelfdeSoort.Count,
        IsOudsteVanDeSoort = !andereVanDezelfdeSoort.Any(d => d.LeeftijdInJaren > dier.LeeftijdInJaren)
    };

    return Ok(fiche);
}
```

</details>

> [!WARNING]
> **Veelgemaakte fouten**
>
> - Het dier zelf meetellen bij `AantalVanDezelfdeSoort`.
> - `>=` voor *ouder*. Dan is een dier nooit de oudste als een ander even oud is.

---

## Alles samen: de volledige bestanden

Hetzelfde als hierboven, maar in één stuk. Handig om naast je eigen bestanden te leggen.

<details>
<summary><span style="color:#845ef7"><b>Toon Dier.cs</b></span></summary>

```csharp
namespace Knuffelhof.Models;

public class Dier
{
    public int Id { get; set; }
    public string Naam { get; set; } = string.Empty;
    public string Soort { get; set; } = string.Empty;
    public int LeeftijdInJaren { get; set; }
    public int Verblijfsnummer { get; set; }
}
```

</details>

<details>
<summary><span style="color:#845ef7"><b>Toon DierFiche.cs</b></span></summary>

```csharp
namespace Knuffelhof.Models;

public class DierFiche
{
    public Dier Dier { get; set; } = new Dier();
    public bool IsJong { get; set; }
    public int AantalVanDezelfdeSoort { get; set; }
    public bool IsOudsteVanDeSoort { get; set; }
}
```

</details>

<details>
<summary><span style="color:#845ef7"><b>Toon DierController.cs</b></span></summary>

```csharp
using Knuffelhof.Models;
using Microsoft.AspNetCore.Mvc;

namespace Knuffelhof.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DierController : ControllerBase
{
    private static List<Dier> dieren = new List<Dier>
    {
        new Dier { Id = 1, Naam = "Bo", Soort = "hond", LeeftijdInJaren = 6, Verblijfsnummer = 12 },
        new Dier { Id = 2, Naam = "Luna", Soort = "hond", LeeftijdInJaren = 1, Verblijfsnummer = 14 },
        new Dier { Id = 3, Naam = "Minou", Soort = "kat", LeeftijdInJaren = 9, Verblijfsnummer = 3 },
        new Dier { Id = 4, Naam = "Pluis", Soort = "kat", LeeftijdInJaren = 0, Verblijfsnummer = 4 },
        new Dier { Id = 5, Naam = "Snuf", Soort = "konijn", LeeftijdInJaren = 3, Verblijfsnummer = 21 },
        new Dier { Id = 6, Naam = "Kiwi", Soort = "parkiet", LeeftijdInJaren = 2, Verblijfsnummer = 30 }
    };
    [HttpGet]
    public ActionResult<List<Dier>> GetAlle()
    {
        return Ok(dieren);
    }

    [HttpGet("{id}")]
    public ActionResult<Dier> GetById(int id)
    {
        Dier? dier = dieren.FirstOrDefault(d => d.Id == id);

        if (dier == null)
        {
            return NotFound($"We vonden geen dier met Id {id}.");
        }

        return Ok(dier);
    }

    [HttpGet("soort/{soort}")]
    public ActionResult<List<Dier>> GetPerSoort(string soort)
    {
        List<Dier> resultaat = dieren
            .Where(d => d.Soort.Equals(soort, StringComparison.OrdinalIgnoreCase))
            .ToList();

        if (resultaat.Count == 0)
        {
            return NotFound($"We hebben op dit moment geen {soort} in het asiel.");
        }

        return Ok(resultaat);
    }

    [HttpGet("jong")]
    public ActionResult<List<Dier>> GetJong()
    {
        return Ok(dieren.Where(d => IsJong(d)).ToList());
    }

    private bool IsJong(Dier dier)
    {
        return dier.LeeftijdInJaren < 2;
    }

    [HttpGet("fiche/{id}")]
    public ActionResult<DierFiche> GetFiche(int id)
    {
        Dier? dier = dieren.FirstOrDefault(d => d.Id == id);

        if (dier == null)
        {
            return NotFound($"We vonden geen dier met Id {id}.");
        }

        List<Dier> andereVanDezelfdeSoort = dieren
            .Where(d => d.Soort == dier.Soort && d.Id != dier.Id)
            .ToList();

        DierFiche fiche = new DierFiche
        {
            Dier = dier,
            IsJong = IsJong(dier),
            AantalVanDezelfdeSoort = andereVanDezelfdeSoort.Count,
            IsOudsteVanDeSoort = !andereVanDezelfdeSoort.Any(d => d.LeeftijdInJaren > dier.LeeftijdInJaren)
        };

        return Ok(fiche);
    }
}
```

</details>
