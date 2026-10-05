# Oplossing 02_01

<span style="background:#c2255c;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>OPLOSSING</b></span> &nbsp;·&nbsp; <b>Hoofdstuk 02</b> &nbsp;·&nbsp; Model, lijst en statuscodes &nbsp;·&nbsp; PC-onderdelen &nbsp;·&nbsp; bij `Oefening_02_01.md`

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

- `Artikel` staat in de map Models, met zes eigenschappen: `Id` en `AantalOpVoorraad`
  als `int`, `Naam`, `Merk` en `Soort` als `string`, `Prijs` als `decimal` of `double`.
- Elke eigenschap heeft `{ get; set; }`.

<details>
<summary><span style="color:#845ef7"><b>Toon de code</b></span></summary>

```csharp
namespace BitEnByte.Models;

public class Artikel
{
    public int Id { get; set; }
    public string Naam { get; set; } = string.Empty;
    public string Merk { get; set; } = string.Empty;
    public string Soort { get; set; } = string.Empty;
    public decimal Prijs { get; set; }
    public int AantalOpVoorraad { get; set; }
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

## 2. De controller en de tijdelijke voorraad

**Nakijkpunten**

- `ArtikelController` met `[Route("api/[controller]")]`: alle adressen beginnen met
  `/api/artikel`.
- Een lijst met minstens zes artikelen, minstens twee soorten, en minstens één
  artikel met voorraad 0.

**Aanpak.** De lijst is `private static`. Bij elk verzoek maakt ASP.NET Core een nieuw controllerobject; enkel een `static` lijst hoort bij de klasse zelf en blijft dus bestaan tussen twee verzoeken (leerboek, hoofdstuk 24). In deze oefening doe je enkel GET-verzoeken, dus zonder `static` zou alles nog werken. Vanaf 02_02 niet meer.

<details>
<summary><span style="color:#845ef7"><b>Toon de code</b></span></summary>

```csharp
using BitEnByte.Models;
using Microsoft.AspNetCore.Mvc;

namespace BitEnByte.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ArtikelController : ControllerBase
{
    private static List<Artikel> artikelen = new List<Artikel>
    {
        new Artikel { Id = 1, Naam = "B650 Tomahawk", Merk = "MSI", Soort = "moederbord", Prijs = 219.00m, AantalOpVoorraad = 12 },
        new Artikel { Id = 2, Naam = "Ryzen 5 7600X", Merk = "AMD", Soort = "processor", Prijs = 199.00m, AantalOpVoorraad = 3 },
        new Artikel { Id = 3, Naam = "Ryzen 7 7800X3D", Merk = "AMD", Soort = "processor", Prijs = 389.00m, AantalOpVoorraad = 5 },
        new Artikel { Id = 4, Naam = "GeForce RTX 4070", Merk = "ASUS", Soort = "videokaart", Prijs = 589.00m, AantalOpVoorraad = 0 },
        new Artikel { Id = 5, Naam = "Radeon RX 7800 XT", Merk = "Sapphire", Soort = "videokaart", Prijs = 499.00m, AantalOpVoorraad = 2 },
        new Artikel { Id = 6, Naam = "NH-D15", Merk = "Noctua", Soort = "koeler", Prijs = 109.00m, AantalOpVoorraad = 7 }
    };

    // ... hier komen de endpoints uit de punten hieronder
}
```

</details>

---

## 3. Volledige voorraad opvragen

**Nakijkpunten**

| Verzoek | Status | Antwoord |
|---|---|---|
| <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> /api/artikel</code> | <code style="color:#37b24d;font-weight:600">200 OK</code> | de volledige lijst, in JSON |

**Aanpak.** `ActionResult<List<Artikel>>` zegt wat er terugkomt, en laat je toch een
statuscode kiezen. `Ok(artikelen)` is 200 met de lijst als body. ASP.NET Core zet de
namen in de JSON om naar kleine beginletters: `aantalOpVoorraad`.

<details>
<summary><span style="color:#845ef7"><b>Toon de code</b></span></summary>

```csharp
[HttpGet]
public ActionResult<List<Artikel>> GetAlle()
{
    return Ok(artikelen);
}
```

</details>

---

## 4. Eén artikel opvragen

**Nakijkpunten**

| Verzoek | Status | Antwoord |
|---|---|---|
| <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> /api/artikel/2</code> | <code style="color:#37b24d;font-weight:600">200 OK</code> | het artikel met Id 2 |
| <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> /api/artikel/999</code> | <code style="color:#f08c00;font-weight:600">404 Not Found</code> | <code style="color:#845ef7">We vonden geen artikel met Id 999.</code> |

**Aanpak.** `FirstOrDefault` geeft het eerste artikel met dat Id, of `null` als er geen is.
Op `null` antwoord je met `NotFound` en het bericht, anders met `Ok`. Het `?` achter
`Artikel` zegt dat de variabele `null` mag zijn (leerboek, hoofdstuk 17).

<details>
<summary><span style="color:#845ef7"><b>Toon de code</b></span></summary>

```csharp
[HttpGet("{id}")]
public ActionResult<Artikel> GetById(int id)
{
    Artikel? artikel = artikelen.FirstOrDefault(a => a.Id == id);

    if (artikel == null)
    {
        return NotFound($"We vonden geen artikel met Id {id}.");
    }

    return Ok(artikel);
}
```

</details>

> [!WARNING]
> **Veelgemaakte fouten**
>
> - `First` in plaats van `FirstOrDefault`. Bij een Id dat niet bestaat crasht
>   `First`, en de client krijgt een 500 in plaats van een 404 (leerboek, hoofdstuk
>   26).

---

## 5. Artikelen van één soort opvragen

**Nakijkpunten**

| Verzoek | Status | Antwoord |
|---|---|---|
| <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> /api/artikel/soort/processor</code> | <code style="color:#37b24d;font-weight:600">200 OK</code> | enkel de processors |
| <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> /api/artikel/soort/voeding</code> | <code style="color:#f08c00;font-weight:600">404 Not Found</code> | <code style="color:#845ef7">We vonden geen artikelen van de soort voeding.</code> |

**Aanpak.** `Where` houdt de artikelen van die soort over, `ToList` maakt er een lijst van
(leerboek, hoofdstuk 11). Is die lijst leeg, dan bestaat de soort niet in je
voorraad, en dat is hier een 404. `Equals` met `OrdinalIgnoreCase` laat ook
`Processor` werken; de opgave vraagt dat niet, maar het kost niets.

<details>
<summary><span style="color:#845ef7"><b>Toon de code</b></span></summary>

```csharp
[HttpGet("soort/{soort}")]
public ActionResult<List<Artikel>> GetPerSoort(string soort)
{
    List<Artikel> resultaat = artikelen
        .Where(a => a.Soort.Equals(soort, StringComparison.OrdinalIgnoreCase))
        .ToList();

    if (resultaat.Count == 0)
    {
        return NotFound($"We vonden geen artikelen van de soort {soort}.");
    }

    return Ok(resultaat);
}
```

</details>

> [!NOTE]
> De routes lijken op elkaar, maar botsen niet. `soort/{soort}` heeft twee stukken, `{id}` maar één, dus een verzoek past nooit bij allebei. En bij `voorradig` tegenover `{id}` kiest ASP.NET Core altijd de route met het vaste woord. Een echte botsing krijg je pas met `[HttpGet("{soort}")]` zonder het woord `soort` ervoor: dan passen twee methodes op hetzelfde adres, en dat geeft een 500 (leerboek, hoofdstuk 25).

---

## 6. Enkel wat op voorraad is

**Nakijkpunten**

| Verzoek | Status | Antwoord |
|---|---|---|
| <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> /api/artikel/voorradig</code> | <code style="color:#37b24d;font-weight:600">200 OK</code> | enkel artikelen met voorraad groter dan 0 |
| <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> /api/artikel/voorradig</code> | <code style="color:#37b24d;font-weight:600">200 OK</code> | een lege lijst `[]`, als niets op voorraad is |

**Aanpak.** Hetzelfde patroon als punt 5, maar zonder 404. Een lege lijst is hier een geldig
antwoord: de vraag klopte, het antwoord is *niets*.

<details>
<summary><span style="color:#845ef7"><b>Toon de code</b></span></summary>

```csharp
[HttpGet("voorradig")]
public ActionResult<List<Artikel>> GetVoorradig()
{
    List<Artikel> voorradig = artikelen
        .Where(a => a.AantalOpVoorraad > 0)
        .ToList();

    return Ok(voorradig);
}
```

</details>

> [!WARNING]
> **Veelgemaakte fouten**
>
> - Ook hier een 404 geven bij een lege lijst. De opgave zegt uitdrukkelijk dat het
>   een 200 is.
> - Testen zonder een artikel met voorraad 0. Dan zie je niet of je filter werkt.

---

## 7. Alles samen: de artikelfiche

**Nakijkpunten**

| Verzoek | Status | Antwoord |
|---|---|---|
| <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> /api/artikel/fiche/1</code> | <code style="color:#37b24d;font-weight:600">200 OK</code> | een fiche; bij het enige artikel van zijn soort staat `aantalVanDezelfdeSoort` op 0 |
| <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> /api/artikel/fiche/4</code> | <code style="color:#37b24d;font-weight:600">200 OK</code> | bij het artikel zonder voorraad staat `isVoorradig` op `false` |
| <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> /api/artikel/fiche/999</code> | <code style="color:#f08c00;font-weight:600">404 Not Found</code> | <code style="color:#845ef7">We vonden geen artikel met Id 999.</code> |

- Van twee artikelen van dezelfde soort, met een verschillende prijs, heeft er maar één `isGoedkoopsteVanDeSoort` op `true`.

**Aanpak.** `ArtikelFiche` is een gewone klasse in de map Models, met het artikel zelf als
eerste eigenschap. Het endpoint zoekt eerst het artikel, met dezelfde 404 als punt 4.
Daarna maak je één lijst met de **andere** artikelen van dezelfde soort, en uit die
ene lijst haal je alles: hoeveel het er zijn, en of er eentje goedkoper is.

<details>
<summary><span style="color:#845ef7"><b>Toon de code</b></span></summary>

```csharp
namespace BitEnByte.Models;

public class ArtikelFiche
{
    public Artikel Artikel { get; set; } = new Artikel();
    public bool IsVoorradig { get; set; }
    public int AantalVanDezelfdeSoort { get; set; }
    public bool IsGoedkoopsteVanDeSoort { get; set; }
}

[HttpGet("fiche/{id}")]
public ActionResult<ArtikelFiche> GetFiche(int id)
{
    Artikel? artikel = artikelen.FirstOrDefault(a => a.Id == id);

    if (artikel == null)
    {
        return NotFound($"We vonden geen artikel met Id {id}.");
    }

    List<Artikel> andereVanDezelfdeSoort = artikelen
        .Where(a => a.Soort == artikel.Soort && a.Id != artikel.Id)
        .ToList();

    ArtikelFiche fiche = new ArtikelFiche
    {
        Artikel = artikel,
        IsVoorradig = artikel.AantalOpVoorraad > 0,
        AantalVanDezelfdeSoort = andereVanDezelfdeSoort.Count,
        IsGoedkoopsteVanDeSoort = !andereVanDezelfdeSoort.Any(a => a.Prijs < artikel.Prijs)
    };

    return Ok(fiche);
}
```

</details>

> [!WARNING]
> **Veelgemaakte fouten**
>
> - Het artikel zelf meetellen. Dan staat `aantalVanDezelfdeSoort` op 1 bij een
>   artikel dat alleen is.
> - `a.Prijs <= artikel.Prijs` voor *goedkoper*. Dan is een artikel nooit de
>   goedkoopste als een ander exact even duur is.

> [!TIP]
> **Zo kan het beter**
>
> - Zijn twee artikelen even goedkoop, dan staan ze hier allebei op `true`. Dat volgt
>   uit de definitie in de opgave: geen enkel ander artikel is goedkoper.
> - Zo'n klasse die enkel bestaat voor de client, komt later terug als DTO (leerboek,
>   hoofdstuk 33).

---

## Alles samen: de volledige bestanden

Hetzelfde als hierboven, maar in één stuk. Handig om naast je eigen bestanden te leggen.

<details>
<summary><span style="color:#845ef7"><b>Toon Artikel.cs</b></span></summary>

```csharp
namespace BitEnByte.Models;

public class Artikel
{
    public int Id { get; set; }
    public string Naam { get; set; } = string.Empty;
    public string Merk { get; set; } = string.Empty;
    public string Soort { get; set; } = string.Empty;
    public decimal Prijs { get; set; }
    public int AantalOpVoorraad { get; set; }
}
```

</details>

<details>
<summary><span style="color:#845ef7"><b>Toon ArtikelFiche.cs</b></span></summary>

```csharp
namespace BitEnByte.Models;

public class ArtikelFiche
{
    public Artikel Artikel { get; set; } = new Artikel();
    public bool IsVoorradig { get; set; }
    public int AantalVanDezelfdeSoort { get; set; }
    public bool IsGoedkoopsteVanDeSoort { get; set; }
}
```

</details>

<details>
<summary><span style="color:#845ef7"><b>Toon ArtikelController.cs</b></span></summary>

```csharp
using BitEnByte.Models;
using Microsoft.AspNetCore.Mvc;

namespace BitEnByte.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ArtikelController : ControllerBase
{
    private static List<Artikel> artikelen = new List<Artikel>
    {
        new Artikel { Id = 1, Naam = "B650 Tomahawk", Merk = "MSI", Soort = "moederbord", Prijs = 219.00m, AantalOpVoorraad = 12 },
        new Artikel { Id = 2, Naam = "Ryzen 5 7600X", Merk = "AMD", Soort = "processor", Prijs = 199.00m, AantalOpVoorraad = 3 },
        new Artikel { Id = 3, Naam = "Ryzen 7 7800X3D", Merk = "AMD", Soort = "processor", Prijs = 389.00m, AantalOpVoorraad = 5 },
        new Artikel { Id = 4, Naam = "GeForce RTX 4070", Merk = "ASUS", Soort = "videokaart", Prijs = 589.00m, AantalOpVoorraad = 0 },
        new Artikel { Id = 5, Naam = "Radeon RX 7800 XT", Merk = "Sapphire", Soort = "videokaart", Prijs = 499.00m, AantalOpVoorraad = 2 },
        new Artikel { Id = 6, Naam = "NH-D15", Merk = "Noctua", Soort = "koeler", Prijs = 109.00m, AantalOpVoorraad = 7 }
    };
    [HttpGet]
    public ActionResult<List<Artikel>> GetAlle()
    {
        return Ok(artikelen);
    }

    [HttpGet("{id}")]
    public ActionResult<Artikel> GetById(int id)
    {
        Artikel? artikel = artikelen.FirstOrDefault(a => a.Id == id);

        if (artikel == null)
        {
            return NotFound($"We vonden geen artikel met Id {id}.");
        }

        return Ok(artikel);
    }

    [HttpGet("soort/{soort}")]
    public ActionResult<List<Artikel>> GetPerSoort(string soort)
    {
        List<Artikel> resultaat = artikelen
            .Where(a => a.Soort.Equals(soort, StringComparison.OrdinalIgnoreCase))
            .ToList();

        if (resultaat.Count == 0)
        {
            return NotFound($"We vonden geen artikelen van de soort {soort}.");
        }

        return Ok(resultaat);
    }

    [HttpGet("voorradig")]
    public ActionResult<List<Artikel>> GetVoorradig()
    {
        List<Artikel> voorradig = artikelen
            .Where(a => a.AantalOpVoorraad > 0)
            .ToList();

        return Ok(voorradig);
    }

    [HttpGet("fiche/{id}")]
    public ActionResult<ArtikelFiche> GetFiche(int id)
    {
        Artikel? artikel = artikelen.FirstOrDefault(a => a.Id == id);

        if (artikel == null)
        {
            return NotFound($"We vonden geen artikel met Id {id}.");
        }

        List<Artikel> andereVanDezelfdeSoort = artikelen
            .Where(a => a.Soort == artikel.Soort && a.Id != artikel.Id)
            .ToList();

        ArtikelFiche fiche = new ArtikelFiche
        {
            Artikel = artikel,
            IsVoorradig = artikel.AantalOpVoorraad > 0,
            AantalVanDezelfdeSoort = andereVanDezelfdeSoort.Count,
            IsGoedkoopsteVanDeSoort = !andereVanDezelfdeSoort.Any(a => a.Prijs < artikel.Prijs)
        };

        return Ok(fiche);
    }
}
```

</details>
