# Oplossing 02_02

<span style="background:#c2255c;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>OPLOSSING</b></span> &nbsp;·&nbsp; <b>Hoofdstuk 02</b> &nbsp;·&nbsp; Volledige CRUD &nbsp;·&nbsp; PC-onderdelen &nbsp;·&nbsp; bij `Oefening_02_02.md`

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

- `Ventilator` in Models: `Id` en `AfmetingInMm` als `int`, `Merk` en `Verlichting` als `string`, `Prijs` als `decimal` of `double`.

<details>
<summary><span style="color:#845ef7"><b>Toon de code</b></span></summary>

```csharp
namespace BitEnByte.Models;

public class Ventilator
{
    public int Id { get; set; }
    public string Merk { get; set; } = string.Empty;
    public int AfmetingInMm { get; set; }
    public string Verlichting { get; set; } = string.Empty;
    public decimal Prijs { get; set; }
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

- `[Route("api/[controller]")]`, dus `/api/ventilator`.
- Na een POST staat de nieuwe ventilator er bij de volgende GET nog bij.

**Aanpak.** De lijst is `private static`. Bij elk verzoek maakt ASP.NET Core een nieuw controllerobject; enkel een `static` lijst hoort bij de klasse zelf en blijft dus bestaan tussen twee verzoeken (leerboek, hoofdstuk 24).

<details>
<summary><span style="color:#845ef7"><b>Toon de code</b></span></summary>

```csharp
using BitEnByte.Models;
using Microsoft.AspNetCore.Mvc;

namespace BitEnByte.Controllers;

[ApiController]
[Route("api/[controller]")]
public class VentilatorController : ControllerBase
{
    private static List<Ventilator> ventilatoren = new List<Ventilator>
    {
        new Ventilator { Id = 1, Merk = "Noctua", AfmetingInMm = 120, Verlichting = "geen", Prijs = 29.90m },
        new Ventilator { Id = 2, Merk = "be quiet!", AfmetingInMm = 140, Verlichting = "geen", Prijs = 24.90m },
        new Ventilator { Id = 3, Merk = "Corsair", AfmetingInMm = 120, Verlichting = "RGB", Prijs = 34.90m },
        new Ventilator { Id = 4, Merk = "Arctic", AfmetingInMm = 140, Verlichting = "ARGB", Prijs = 14.90m }
    };

    // ... hier komen de endpoints uit de punten hieronder
}
```

</details>

> [!WARNING]
> **Veelgemaakte fouten**
>
> - De lijst zonder `static`. Elke POST lijkt te lukken, maar bij het volgende verzoek
>   is de nieuwe ventilator weg. Dat is de waarschuwing uit de opgave.

---

## 3. Alles ophalen en één ophalen

**Nakijkpunten**

| Verzoek | Status | Antwoord |
|---|---|---|
| <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> /api/ventilator</code> | <code style="color:#37b24d;font-weight:600">200 OK</code> | de volledige lijst |
| <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> /api/ventilator/2</code> | <code style="color:#37b24d;font-weight:600">200 OK</code> | ventilator 2 |
| <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> /api/ventilator/999</code> | <code style="color:#f08c00;font-weight:600">404 Not Found</code> | <code style="color:#845ef7">We vonden geen ventilator met Id 999.</code> |

**Aanpak.** Zoals in les 02: `Ok` met de lijst, en `FirstOrDefault` met een controle op `null` voor één ventilator.

<details>
<summary><span style="color:#845ef7"><b>Toon de code</b></span></summary>

```csharp
[HttpGet]
public ActionResult<List<Ventilator>> GetAlle()
{
    return Ok(ventilatoren);
}

[HttpGet("{id}")]
public ActionResult<Ventilator> GetById(int id)
{
    Ventilator? ventilator = ventilatoren.FirstOrDefault(v => v.Id == id);

    if (ventilator == null)
    {
        return NotFound($"We vonden geen ventilator met Id {id}.");
    }

    return Ok(ventilator);
}
```

</details>

---

## 4. Een ventilator toevoegen

**Nakijkpunten**

| Verzoek | Status | Antwoord |
|---|---|---|
| <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>POST</b> /api/ventilator</code> | <code style="color:#37b24d;font-weight:600">201 Created</code> | de nieuwe ventilator, met een Id van de API, en het adres in de header *Location* |
| <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>POST</b> /api/ventilator</code> | <code style="color:#37b24d;font-weight:600">201 Created</code> | met `"id": 77` in de body: het Id wordt toch door de API gekozen |
| <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>POST</b> /api/ventilator</code> | <code style="color:#f08c00;font-weight:600">400 Bad Request</code> | <code style="color:#845ef7">Een ventilator moet een merk hebben.</code> |
| <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>POST</b> /api/ventilator</code> | <code style="color:#f08c00;font-weight:600">400 Bad Request</code> | <code style="color:#845ef7">We verkopen enkel ventilatoren van 120 of 140 mm.</code> |
| <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>POST</b> /api/ventilator</code> | <code style="color:#f08c00;font-weight:600">400 Bad Request</code> | <code style="color:#845ef7">De prijs moet groter zijn dan 0.</code> |

**Aanpak.** De drie controles staan in één private methode, `Controleer`, die het
foutbericht teruggeeft, of `null` als alles klopt (leerboek, hoofdstuk 17). POST, PUT
en de vervanging in punt 8 roepen ze alle drie op. Pas als er geen fout is, krijgt de
ventilator een Id en gaat hij in de lijst. `CreatedAtAction` geeft 201, met de
ventilator in de body en het adres van `GetById` in de header.

<details>
<summary><span style="color:#845ef7"><b>Toon de code</b></span></summary>

```csharp
[HttpPost]
public ActionResult<Ventilator> VoegToe(Ventilator nieuw)
{
    string? fout = Controleer(nieuw);
    if (fout != null)
    {
        return BadRequest(fout);
    }

    nieuw.Id = VolgendId();
    ventilatoren.Add(nieuw);

    return CreatedAtAction(nameof(GetById), new { id = nieuw.Id }, nieuw);
}

// Geeft de foutboodschap terug, of null als alles in orde is.
private string? Controleer(Ventilator ventilator)
{
    if (string.IsNullOrWhiteSpace(ventilator.Merk))
    {
        return "Een ventilator moet een merk hebben.";
    }

    if (ventilator.AfmetingInMm != 120 && ventilator.AfmetingInMm != 140)
    {
        return "We verkopen enkel ventilatoren van 120 of 140 mm.";
    }

    if (ventilator.Prijs <= 0)
    {
        return "De prijs moet groter zijn dan 0.";
    }

    return null;
}

private int VolgendId()
{
    if (ventilatoren.Count == 0)
    {
        return 1;
    }

    return ventilatoren.Max(v => v.Id) + 1;
}
```

</details>

> [!WARNING]
> **Veelgemaakte fouten**
>
> - Eerst toevoegen en dan controleren. Dan staat de ongeldige ventilator al in de
>   lijst als je de 400 teruggeeft.
> - `ventilator.Merk == ""` als enige controle. Een merk van enkel spaties komt er dan
>   door; `string.IsNullOrWhiteSpace` vangt beide.

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

## 5. Een ventilator bijwerken

**Nakijkpunten**

| Verzoek | Status | Antwoord |
|---|---|---|
| <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>PUT</b> /api/ventilator/1</code> | <code style="color:#37b24d;font-weight:600">204 No Content</code> | lege body; een GET toont daarna de nieuwe gegevens |
| <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>PUT</b> /api/ventilator/999</code> | <code style="color:#f08c00;font-weight:600">404 Not Found</code> | <code style="color:#845ef7">Kan geen ventilator bijwerken met Id 999, omdat deze niet bestaat.</code> |
| <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>PUT</b> /api/ventilator/1</code> | <code style="color:#f08c00;font-weight:600">400 Bad Request</code> | een van de drie berichten uit punt 4 |

**Aanpak.** Eerst zoeken, dan controleren, dan elke eigenschap overnemen. Het Id neem je
niet over: dat staat in de URL. Het returntype is gewoon `ActionResult`, zoals in les
02, omdat er geen gegevens terugkomen.

<details>
<summary><span style="color:#845ef7"><b>Toon de code</b></span></summary>

```csharp
[HttpPut("{id}")]
public ActionResult Wijzig(int id, Ventilator gewijzigd)
{
    Ventilator? bestaand = ventilatoren.FirstOrDefault(v => v.Id == id);

    if (bestaand == null)
    {
        return NotFound($"Kan geen ventilator bijwerken met Id {id}, omdat deze niet bestaat.");
    }

    string? fout = Controleer(gewijzigd);
    if (fout != null)
    {
        return BadRequest(fout);
    }

    bestaand.Merk = gewijzigd.Merk;
    bestaand.AfmetingInMm = gewijzigd.AfmetingInMm;
    bestaand.Verlichting = gewijzigd.Verlichting;
    bestaand.Prijs = gewijzigd.Prijs;

    return NoContent();
}
```

</details>

> [!WARNING]
> **Veelgemaakte fouten**
>
> - Het gevonden object vervangen in plaats van de eigenschappen over te nemen,
>   bijvoorbeeld met `bestaand = gewijzigd;`. Dan verandert enkel je lokale variabele,
>   niet de ventilator in de lijst (leerboek, hoofdstuk 16).

---

## 6. Een ventilator verwijderen

**Nakijkpunten**

| Verzoek | Status | Antwoord |
|---|---|---|
| <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>DELETE</b> /api/ventilator/3</code> | <code style="color:#37b24d;font-weight:600">204 No Content</code> | lege body |
| <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>DELETE</b> /api/ventilator/3</code> | <code style="color:#f08c00;font-weight:600">404 Not Found</code> | <code style="color:#845ef7">Kan ventilator met Id 3 niet verwijderen, omdat deze niet gevonden is.</code> |

**Aanpak.** Zoeken, op `null` een 404, anders `Remove` met het gevonden object en een 204.

<details>
<summary><span style="color:#845ef7"><b>Toon de code</b></span></summary>

```csharp
[HttpDelete("{id}")]
public ActionResult Verwijder(int id)
{
    Ventilator? ventilator = ventilatoren.FirstOrDefault(v => v.Id == id);

    if (ventilator == null)
    {
        return NotFound($"Kan ventilator met Id {id} niet verwijderen, omdat deze niet gevonden is.");
    }

    ventilatoren.Remove(ventilator);

    return NoContent();
}
```

</details>

---

## 7. Testen

**Nakijkpunten**

| Verzoek | Status | Antwoord |
|---|---|---|
| <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>POST</b> /api/ventilator</code> | <code style="color:#f08c00;font-weight:600">400 Bad Request</code> | met `"afmetingInMm": 92`; een GET daarna toont hem niet |
| <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>DELETE</b> /api/ventilator/3</code> | <code style="color:#f08c00;font-weight:600">404 Not Found</code> | de tweede keer op hetzelfde Id |

**Aanpak.** Hier hoort geen nieuwe code bij. Haal je deze twee, dan werken je controles en je verwijdering zoals het hoort.

---

## 8. Alles samen: een ventilator vervangen

**Nakijkpunten**

| Verzoek | Status | Antwoord |
|---|---|---|
| <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>PUT</b> /api/ventilator/vervang/999</code> | <code style="color:#f08c00;font-weight:600">404 Not Found</code> | <code style="color:#845ef7">Kan ventilator met Id 999 niet vervangen, omdat deze niet gevonden is.</code> |
| <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>PUT</b> /api/ventilator/vervang/1</code> | <code style="color:#f08c00;font-weight:600">400 Bad Request</code> | met 92 mm in de body; een GET op ventilator 1 werkt daarna nog |
| <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>PUT</b> /api/ventilator/vervang/1</code> | <code style="color:#37b24d;font-weight:600">201 Created</code> | de nieuwe ventilator met een nieuw Id; ventilator 1 geeft daarna 404 |

**Aanpak.** Twee blokken. Eerst alles controleren zonder iets te veranderen: bestaat de oude,
klopt de nieuwe. Pas daarna de lijst aanpassen. Het nieuwe Id wordt gekozen
**voor** de oude eruit gaat. Was de oude ventilator de laatste in de lijst, dan zou
`VolgendId` na het verwijderen zijn Id opnieuw uitdelen, en een client die het oude
adres nog kent, krijgt dan een andere ventilator te zien.

<details>
<summary><span style="color:#845ef7"><b>Toon de code</b></span></summary>

```csharp
[HttpPut("vervang/{id}")]
public ActionResult<Ventilator> Vervang(int id, Ventilator nieuw)
{
    // 1. Eerst alles controleren, nog niets veranderen.
    Ventilator? oud = ventilatoren.FirstOrDefault(v => v.Id == id);

    if (oud == null)
    {
        return NotFound($"Kan ventilator met Id {id} niet vervangen, omdat deze niet gevonden is.");
    }

    string? fout = Controleer(nieuw);
    if (fout != null)
    {
        return BadRequest(fout);
    }

    // 2. Pas als alles in orde is, de lijst aanpassen.
    nieuw.Id = VolgendId();
    ventilatoren.Remove(oud);
    ventilatoren.Add(nieuw);

    return CreatedAtAction(nameof(GetById), new { id = nieuw.Id }, nieuw);
}
```

</details>

> [!WARNING]
> **Veelgemaakte fouten**
>
> - Eerst verwijderen, dan controleren. De oude ventilator is weg op het moment dat je
>   de 400 teruggeeft: de valkuil uit de opgave.
> - De bestaande `Wijzig` uit punt 5 hergebruiken. Die houdt het oude Id, terwijl de
>   opgave een nieuw Id vraagt.

---

## Alles samen: de volledige bestanden

Hetzelfde als hierboven, maar in één stuk. Handig om naast je eigen bestanden te leggen.

<details>
<summary><span style="color:#845ef7"><b>Toon Ventilator.cs</b></span></summary>

```csharp
namespace BitEnByte.Models;

public class Ventilator
{
    public int Id { get; set; }
    public string Merk { get; set; } = string.Empty;
    public int AfmetingInMm { get; set; }
    public string Verlichting { get; set; } = string.Empty;
    public decimal Prijs { get; set; }
}
```

</details>

<details>
<summary><span style="color:#845ef7"><b>Toon VentilatorController.cs</b></span></summary>

```csharp
using BitEnByte.Models;
using Microsoft.AspNetCore.Mvc;

namespace BitEnByte.Controllers;

[ApiController]
[Route("api/[controller]")]
public class VentilatorController : ControllerBase
{
    private static List<Ventilator> ventilatoren = new List<Ventilator>
    {
        new Ventilator { Id = 1, Merk = "Noctua", AfmetingInMm = 120, Verlichting = "geen", Prijs = 29.90m },
        new Ventilator { Id = 2, Merk = "be quiet!", AfmetingInMm = 140, Verlichting = "geen", Prijs = 24.90m },
        new Ventilator { Id = 3, Merk = "Corsair", AfmetingInMm = 120, Verlichting = "RGB", Prijs = 34.90m },
        new Ventilator { Id = 4, Merk = "Arctic", AfmetingInMm = 140, Verlichting = "ARGB", Prijs = 14.90m }
    };
    [HttpGet]
    public ActionResult<List<Ventilator>> GetAlle()
    {
        return Ok(ventilatoren);
    }

    [HttpGet("{id}")]
    public ActionResult<Ventilator> GetById(int id)
    {
        Ventilator? ventilator = ventilatoren.FirstOrDefault(v => v.Id == id);

        if (ventilator == null)
        {
            return NotFound($"We vonden geen ventilator met Id {id}.");
        }

        return Ok(ventilator);
    }

    [HttpPost]
    public ActionResult<Ventilator> VoegToe(Ventilator nieuw)
    {
        string? fout = Controleer(nieuw);
        if (fout != null)
        {
            return BadRequest(fout);
        }

        nieuw.Id = VolgendId();
        ventilatoren.Add(nieuw);

        return CreatedAtAction(nameof(GetById), new { id = nieuw.Id }, nieuw);
    }

    // Geeft de foutboodschap terug, of null als alles in orde is.
    private string? Controleer(Ventilator ventilator)
    {
        if (string.IsNullOrWhiteSpace(ventilator.Merk))
        {
            return "Een ventilator moet een merk hebben.";
        }

        if (ventilator.AfmetingInMm != 120 && ventilator.AfmetingInMm != 140)
        {
            return "We verkopen enkel ventilatoren van 120 of 140 mm.";
        }

        if (ventilator.Prijs <= 0)
        {
            return "De prijs moet groter zijn dan 0.";
        }

        return null;
    }

    private int VolgendId()
    {
        if (ventilatoren.Count == 0)
        {
            return 1;
        }

        return ventilatoren.Max(v => v.Id) + 1;
    }

    [HttpPut("{id}")]
    public ActionResult Wijzig(int id, Ventilator gewijzigd)
    {
        Ventilator? bestaand = ventilatoren.FirstOrDefault(v => v.Id == id);

        if (bestaand == null)
        {
            return NotFound($"Kan geen ventilator bijwerken met Id {id}, omdat deze niet bestaat.");
        }

        string? fout = Controleer(gewijzigd);
        if (fout != null)
        {
            return BadRequest(fout);
        }

        bestaand.Merk = gewijzigd.Merk;
        bestaand.AfmetingInMm = gewijzigd.AfmetingInMm;
        bestaand.Verlichting = gewijzigd.Verlichting;
        bestaand.Prijs = gewijzigd.Prijs;

        return NoContent();
    }

    [HttpDelete("{id}")]
    public ActionResult Verwijder(int id)
    {
        Ventilator? ventilator = ventilatoren.FirstOrDefault(v => v.Id == id);

        if (ventilator == null)
        {
            return NotFound($"Kan ventilator met Id {id} niet verwijderen, omdat deze niet gevonden is.");
        }

        ventilatoren.Remove(ventilator);

        return NoContent();
    }

    [HttpPut("vervang/{id}")]
    public ActionResult<Ventilator> Vervang(int id, Ventilator nieuw)
    {
        // 1. Eerst alles controleren, nog niets veranderen.
        Ventilator? oud = ventilatoren.FirstOrDefault(v => v.Id == id);

        if (oud == null)
        {
            return NotFound($"Kan ventilator met Id {id} niet vervangen, omdat deze niet gevonden is.");
        }

        string? fout = Controleer(nieuw);
        if (fout != null)
        {
            return BadRequest(fout);
        }

        // 2. Pas als alles in orde is, de lijst aanpassen.
        nieuw.Id = VolgendId();
        ventilatoren.Remove(oud);
        ventilatoren.Add(nieuw);

        return CreatedAtAction(nameof(GetById), new { id = nieuw.Id }, nieuw);
    }
}
```

</details>
