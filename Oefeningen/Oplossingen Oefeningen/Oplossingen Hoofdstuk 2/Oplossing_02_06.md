# Oplossing 02_06

<span style="background:#c2255c;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>OPLOSSING</b></span> &nbsp;·&nbsp; <b>Hoofdstuk 02</b> &nbsp;·&nbsp; Soft delete en plaatscontrole &nbsp;·&nbsp; Concertzaal &nbsp;·&nbsp; bij `Oefening_02_06.md`

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

- `Ticket` in Models: `Id`, `Rij` en `Stoel` als `int`, `Naam` als `string`, `Prijs` als `decimal`, `IsGeannuleerd` als `bool`.

<details>
<summary><span style="color:#845ef7"><b>Toon de code</b></span></summary>

```csharp
namespace DeNotenbalk.Models;

public class Ticket
{
    public int Id { get; set; }
    public string Naam { get; set; } = string.Empty;
    public int Rij { get; set; }
    public int Stoel { get; set; }
    public decimal Prijs { get; set; }
    public bool IsGeannuleerd { get; set; }
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

- `/api/ticket`, een `static` lijst met minstens vijf tickets, waarvan minstens één geannuleerd.

**Aanpak.** De lijst is `private static`. Bij elk verzoek maakt ASP.NET Core een nieuw controllerobject; enkel een `static` lijst hoort bij de klasse zelf en blijft dus bestaan tussen twee verzoeken (leerboek, hoofdstuk 24).

<details>
<summary><span style="color:#845ef7"><b>Toon de code</b></span></summary>

```csharp
using DeNotenbalk.Models;
using Microsoft.AspNetCore.Mvc;

namespace DeNotenbalk.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TicketController : ControllerBase
{
    private static List<Ticket> tickets = new List<Ticket>
    {
        new Ticket { Id = 1, Naam = "Anna Peeters", Rij = 1, Stoel = 4, Prijs = 45.00m, IsGeannuleerd = false },
        new Ticket { Id = 2, Naam = "Bram Claes", Rij = 1, Stoel = 5, Prijs = 45.00m, IsGeannuleerd = false },
        new Ticket { Id = 3, Naam = "Chloé Maes", Rij = 2, Stoel = 5, Prijs = 38.00m, IsGeannuleerd = false },
        new Ticket { Id = 4, Naam = "Daan Wouters", Rij = 3, Stoel = 1, Prijs = 32.00m, IsGeannuleerd = true },
        new Ticket { Id = 5, Naam = "Eva Jacobs", Rij = 4, Stoel = 8, Prijs = 32.00m, IsGeannuleerd = false }
    };

    // ... hier komen de endpoints uit de punten hieronder
}
```

</details>

---

## 3. De geldige tickets

**Nakijkpunten**

| Verzoek | Status | Antwoord |
|---|---|---|
| <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> /api/ticket</code> | <code style="color:#37b24d;font-weight:600">200 OK</code> | enkel de tickets met `isGeannuleerd: false` |

<details>
<summary><span style="color:#845ef7"><b>Toon de code</b></span></summary>

```csharp
[HttpGet]
public ActionResult<List<Ticket>> GetGeldig()
{
    return Ok(tickets.Where(t => !t.IsGeannuleerd).ToList());
}
```

</details>

---

## 4. Alle tickets

**Nakijkpunten**

| Verzoek | Status | Antwoord |
|---|---|---|
| <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> /api/ticket/alles</code> | <code style="color:#37b24d;font-weight:600">200 OK</code> | alle tickets, ook de geannuleerde |

<details>
<summary><span style="color:#845ef7"><b>Toon de code</b></span></summary>

```csharp
[HttpGet("alles")]
public ActionResult<List<Ticket>> GetAlles()
{
    return Ok(tickets);
}
```

</details>

> [!NOTE]
> `alles` en `opbrengst` passen ook op de vorm van `{id}`, maar ASP.NET Core
> kiest altijd de route met het vaste woord. Ze botsen dus niet.

---

## 5. Eén ticket

**Nakijkpunten**

| Verzoek | Status | Antwoord |
|---|---|---|
| <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> /api/ticket/1</code> | <code style="color:#37b24d;font-weight:600">200 OK</code> | ticket 1, als het geldig is |
| <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> /api/ticket/4</code> | <code style="color:#f08c00;font-weight:600">404 Not Found</code> | <code style="color:#845ef7">We vonden geen geldig ticket met Id 4.</code> (geannuleerd) |

**Aanpak.** `ZoekGeldig` zoekt op Id én op `IsGeannuleerd`. PUT en DELETE gebruiken het ook.

<details>
<summary><span style="color:#845ef7"><b>Toon de code</b></span></summary>

```csharp
[HttpGet("{id}")]
public ActionResult<Ticket> GetById(int id)
{
    Ticket? ticket = ZoekGeldig(id);

    if (ticket == null)
    {
        return NotFound($"We vonden geen geldig ticket met Id {id}.");
    }

    return Ok(ticket);
}

private Ticket? ZoekGeldig(int id)
{
    return tickets.FirstOrDefault(t => t.Id == id && !t.IsGeannuleerd);
}
```

</details>

---

## 6. Een ticket verkopen

**Nakijkpunten**

| Verzoek | Status | Antwoord |
|---|---|---|
| <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>POST</b> /api/ticket</code> | <code style="color:#37b24d;font-weight:600">201 Created</code> | het nieuwe ticket, altijd met `isGeannuleerd: false` |
| <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>POST</b> /api/ticket</code> | <code style="color:#f08c00;font-weight:600">400 Bad Request</code> | <code style="color:#845ef7">Een ticket moet op naam staan.</code> |
| <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>POST</b> /api/ticket</code> | <code style="color:#f08c00;font-weight:600">400 Bad Request</code> | <code style="color:#845ef7">De rij moet minstens 1 zijn.</code> |
| <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>POST</b> /api/ticket</code> | <code style="color:#f08c00;font-weight:600">400 Bad Request</code> | <code style="color:#845ef7">De stoel moet minstens 1 zijn.</code> |
| <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>POST</b> /api/ticket</code> | <code style="color:#f08c00;font-weight:600">400 Bad Request</code> | <code style="color:#845ef7">De prijs moet groter zijn dan 0.</code> |
| <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>POST</b> /api/ticket</code> | <code style="color:#f08c00;font-weight:600">400 Bad Request</code> | <code style="color:#845ef7">Rij 1 stoel 4 is al verkocht.</code> (plaats van een geldig ticket) |
| <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>POST</b> /api/ticket</code> | <code style="color:#37b24d;font-weight:600">201 Created</code> | op de plaats van een **geannuleerd** ticket: die plaats is vrij |

**Aanpak.** Drie hulpmethodes, omdat PUT, herstel en verhuis dezelfde stukken nodig hebben.
`ControleerPlaats` kijkt enkel naar rij en stoel. `IsBezet` doorzoekt de lijst naar een
**geldig** ticket op die plaats, maar slaat het ticket met `eigenId` over. Bij een
nieuw ticket is `eigenId` 0, en dat Id heeft geen enkel bestaand ticket.
`Controleer` zet ze in de volgorde van de opgave.

<details>
<summary><span style="color:#845ef7"><b>Toon de code</b></span></summary>

```csharp
[HttpPost]
public ActionResult<Ticket> Verkoop(Ticket nieuw)
{
    string? fout = Controleer(nieuw, 0);
    if (fout != null)
    {
        return BadRequest(fout);
    }

    nieuw.Id = VolgendId();
    nieuw.IsGeannuleerd = false;
    tickets.Add(nieuw);

    return CreatedAtAction(nameof(GetById), new { id = nieuw.Id }, nieuw);
}

// eigenId: het ticket dat zijn eigen plaats mag houden. Bij een nieuw ticket is dat 0:
// geen enkel bestaand ticket heeft Id 0.
private string? Controleer(Ticket ticket, int eigenId)
{
    if (string.IsNullOrWhiteSpace(ticket.Naam))
    {
        return "Een ticket moet op naam staan.";
    }

    string? plaatsfout = ControleerPlaats(ticket.Rij, ticket.Stoel);
    if (plaatsfout != null)
    {
        return plaatsfout;
    }

    if (ticket.Prijs <= 0)
    {
        return "De prijs moet groter zijn dan 0.";
    }

    if (IsBezet(ticket.Rij, ticket.Stoel, eigenId))
    {
        return $"Rij {ticket.Rij} stoel {ticket.Stoel} is al verkocht.";
    }

    return null;
}

private string? ControleerPlaats(int rij, int stoel)
{
    if (rij < 1)
    {
        return "De rij moet minstens 1 zijn.";
    }

    if (stoel < 1)
    {
        return "De stoel moet minstens 1 zijn.";
    }

    return null;
}

// Zit er op deze plaats een geldig ticket, ander dan het ticket met eigenId?
private bool IsBezet(int rij, int stoel, int eigenId)
{
    return tickets.Any(t => !t.IsGeannuleerd
                         && t.Rij == rij
                         && t.Stoel == stoel
                         && t.Id != eigenId);
}

private int VolgendId()
{
    if (tickets.Count == 0)
    {
        return 1;
    }

    return tickets.Max(t => t.Id) + 1;
}
```

</details>

> [!WARNING]
> **Veelgemaakte fouten**
>
> - In `IsBezet` niet naar `IsGeannuleerd` kijken. Dan blijft een geannuleerde plaats
>   voor altijd bezet.
> - De plaatscontrole vergeten bij een gewone PUT. Dan kan je met punt 7 twee tickets
>   op dezelfde stoel zetten.

> [!TIP]
> **Zo kan het beter**
>
> - `VolgendId` is de manier van de cursus, de hoogste Id plus 1, met één verbetering.
>   `Max` op een lege lijst crasht. Verwijder je alle items en voeg je er daarna een
>   toe, dan gaf de code uit les 02 een 500. Hier wordt het gewoon Id 1.

---

## 7. Een ticket wijzigen

**Nakijkpunten**

| Verzoek | Status | Antwoord |
|---|---|---|
| <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>PUT</b> /api/ticket/3</code> | <code style="color:#37b24d;font-weight:600">204 No Content</code> | ticket 3 houdt zijn eigen plaats, met een andere naam |
| <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>PUT</b> /api/ticket/3</code> | <code style="color:#f08c00;font-weight:600">400 Bad Request</code> | <code style="color:#845ef7">Rij 1 stoel 5 is al verkocht.</code> (plaats van een ander ticket) |
| <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>PUT</b> /api/ticket/4</code> | <code style="color:#f08c00;font-weight:600">404 Not Found</code> | een geannuleerd ticket kan je niet wijzigen |

**Aanpak.** Dezelfde `Controleer`, maar nu met het Id van het ticket zelf als `eigenId`. Zo
telt je eigen plaats niet als bezet. De opgave noemt geen bericht voor de 404; hier is
dat van punt 5 hergebruikt.

<details>
<summary><span style="color:#845ef7"><b>Toon de code</b></span></summary>

```csharp
[HttpPut("{id}")]
public ActionResult Wijzig(int id, Ticket gewijzigd)
{
    Ticket? bestaand = ZoekGeldig(id);

    if (bestaand == null)
    {
        return NotFound($"We vonden geen geldig ticket met Id {id}.");
    }

    string? fout = Controleer(gewijzigd, id);
    if (fout != null)
    {
        return BadRequest(fout);
    }

    bestaand.Naam = gewijzigd.Naam;
    bestaand.Rij = gewijzigd.Rij;
    bestaand.Stoel = gewijzigd.Stoel;
    bestaand.Prijs = gewijzigd.Prijs;

    return NoContent();
}
```

</details>

> [!WARNING]
> **Veelgemaakte fouten**
>
> - `Controleer(gewijzigd, 0)`. Dan vindt `IsBezet` je eigen ticket, en mag niemand
>   een ticket wijzigen zonder te verhuizen.

---

## 8. Een ticket annuleren

**Nakijkpunten**

| Verzoek | Status | Antwoord |
|---|---|---|
| <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>DELETE</b> /api/ticket/5</code> | <code style="color:#37b24d;font-weight:600">204 No Content</code> | lege body; de plaats is vrij |
| <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>DELETE</b> /api/ticket/5</code> | <code style="color:#f08c00;font-weight:600">404 Not Found</code> | <code style="color:#845ef7">Kan ticket met Id 5 niet annuleren, omdat het niet gevonden is.</code> |

<details>
<summary><span style="color:#845ef7"><b>Toon de code</b></span></summary>

```csharp
[HttpDelete("{id}")]
public ActionResult Annuleer(int id)
{
    Ticket? ticket = ZoekGeldig(id);

    if (ticket == null)
    {
        return NotFound($"Kan ticket met Id {id} niet annuleren, omdat het niet gevonden is.");
    }

    ticket.IsGeannuleerd = true;

    return NoContent();
}
```

</details>

---

## 9. Een ticket herstellen

**Nakijkpunten**

| Verzoek | Status | Antwoord |
|---|---|---|
| <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>PUT</b> /api/ticket/herstel/5</code> | <code style="color:#37b24d;font-weight:600">204 No Content</code> | het ticket is weer geldig |
| <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>PUT</b> /api/ticket/herstel/3</code> | <code style="color:#f08c00;font-weight:600">400 Bad Request</code> | <code style="color:#845ef7">Dit ticket is niet geannuleerd.</code> |
| <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>PUT</b> /api/ticket/herstel/4</code> | <code style="color:#f08c00;font-weight:600">400 Bad Request</code> | <code style="color:#845ef7">Rij 3 stoel 1 is intussen aan iemand anders verkocht.</code> (als die plaats opnieuw verkocht is) |
| <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>PUT</b> /api/ticket/herstel/999</code> | <code style="color:#f08c00;font-weight:600">404 Not Found</code> | het ticket bestaat niet |

**Aanpak.** Zoek gewoon op Id, want je zoekt net een geannuleerd ticket. Voor je het herstelt,
kijk je met `IsBezet` of iemand anders intussen op die plaats zit.

<details>
<summary><span style="color:#845ef7"><b>Toon de code</b></span></summary>

```csharp
[HttpPut("herstel/{id}")]
public ActionResult Herstel(int id)
{
    Ticket? ticket = tickets.FirstOrDefault(t => t.Id == id);

    if (ticket == null)
    {
        return NotFound($"We vonden geen ticket met Id {id}.");
    }

    if (!ticket.IsGeannuleerd)
    {
        return BadRequest("Dit ticket is niet geannuleerd.");
    }

    if (IsBezet(ticket.Rij, ticket.Stoel, ticket.Id))
    {
        return BadRequest($"Rij {ticket.Rij} stoel {ticket.Stoel} is intussen aan iemand anders verkocht.");
    }

    ticket.IsGeannuleerd = false;

    return NoContent();
}
```

</details>

> [!WARNING]
> **Veelgemaakte fouten**
>
> - Herstellen zonder plaatscontrole. Dan zitten er twee geldige tickets op dezelfde
>   stoel: de fout die het testscenario in punt 11 bovenhaalt.

---

## 10. De opbrengst

**Nakijkpunten**

| Verzoek | Status | Antwoord |
|---|---|---|
| <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> /api/ticket/opbrengst</code> | <code style="color:#37b24d;font-weight:600">200 OK</code> | de som van de prijzen van de geldige tickets |

- Na een annulering is de opbrengst gedaald met exact de prijs van dat ticket.

<details>
<summary><span style="color:#845ef7"><b>Toon de code</b></span></summary>

```csharp
[HttpGet("opbrengst")]
public ActionResult<decimal> GetOpbrengst()
{
    return Ok(tickets.Where(t => !t.IsGeannuleerd).Sum(t => t.Prijs));
}
```

</details>

---

## 11. Alles samen: een ticket verhuizen

**Nakijkpunten**

| Verzoek | Status | Antwoord |
|---|---|---|
| <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>PUT</b> /api/ticket/verhuis/1/6/6</code> | <code style="color:#37b24d;font-weight:600">204 No Content</code> | naar een vrije plaats; de opbrengst blijft gelijk |
| <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>PUT</b> /api/ticket/verhuis/1/6/6</code> | <code style="color:#f08c00;font-weight:600">400 Bad Request</code> | <code style="color:#845ef7">Dit ticket zit al op rij 6 stoel 6.</code> |
| <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>PUT</b> /api/ticket/verhuis/1/1/5</code> | <code style="color:#f08c00;font-weight:600">400 Bad Request</code> | <code style="color:#845ef7">Rij 1 stoel 5 is al verkocht.</code> |
| <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>PUT</b> /api/ticket/verhuis/1/0/3</code> | <code style="color:#f08c00;font-weight:600">400 Bad Request</code> | <code style="color:#845ef7">De rij moet minstens 1 zijn.</code> |
| <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>PUT</b> /api/ticket/verhuis/999/1/1</code> | <code style="color:#f08c00;font-weight:600">404 Not Found</code> | <code style="color:#845ef7">We vonden geen geldig ticket met Id 999.</code> |

- Het testscenario uit de opgave: na stap 5 lukt het verhuizen, in stap 6 weigert het herstel, en in stap 7 is de opbrengst enkel gedaald met de prijs van het geannuleerde ticket.

**Aanpak.** De volgorde van de opgave, met de hulpmethodes van punt 6: eerst het ticket, dan
de plaats zelf, dan de twee bezette gevallen. De derde controle vergelijkt met het
ticket zelf: zit het er al? De vierde zoekt met `IsBezet` naar een **ander** geldig
ticket op die plaats, en krijgt daarom het Id van het ticket mee. Daarna pas je enkel
rij en stoel aan: de prijs blijft, en dus ook de opbrengst.

<details>
<summary><span style="color:#845ef7"><b>Toon de code</b></span></summary>

```csharp
[HttpPut("verhuis/{id}/{rij}/{stoel}")]
public ActionResult Verhuis(int id, int rij, int stoel)
{
    Ticket? ticket = ZoekGeldig(id);

    if (ticket == null)
    {
        return NotFound($"We vonden geen geldig ticket met Id {id}.");
    }

    string? plaatsfout = ControleerPlaats(rij, stoel);
    if (plaatsfout != null)
    {
        return BadRequest(plaatsfout);
    }

    if (ticket.Rij == rij && ticket.Stoel == stoel)
    {
        return BadRequest($"Dit ticket zit al op rij {rij} stoel {stoel}.");
    }

    if (IsBezet(rij, stoel, ticket.Id))
    {
        return BadRequest($"Rij {rij} stoel {stoel} is al verkocht.");
    }

    ticket.Rij = rij;
    ticket.Stoel = stoel;

    return NoContent();
}
```

</details>

> [!WARNING]
> **Veelgemaakte fouten**
>
> - De derde en de vierde regel samenvoegen. Dan krijgt wie al op die plaats zit, het
>   bericht *al verkocht* in plaats van *zit al op*.
> - Annuleren en opnieuw verkopen om te verhuizen. Dan krijgt het ticket een nieuw Id,
>   en tussen de twee stappen in klopt de zaal even niet.

---

## Alles samen: de volledige bestanden

Hetzelfde als hierboven, maar in één stuk. Handig om naast je eigen bestanden te leggen.

<details>
<summary><span style="color:#845ef7"><b>Toon Ticket.cs</b></span></summary>

```csharp
namespace DeNotenbalk.Models;

public class Ticket
{
    public int Id { get; set; }
    public string Naam { get; set; } = string.Empty;
    public int Rij { get; set; }
    public int Stoel { get; set; }
    public decimal Prijs { get; set; }
    public bool IsGeannuleerd { get; set; }
}
```

</details>

<details>
<summary><span style="color:#845ef7"><b>Toon TicketController.cs</b></span></summary>

```csharp
using DeNotenbalk.Models;
using Microsoft.AspNetCore.Mvc;

namespace DeNotenbalk.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TicketController : ControllerBase
{
    private static List<Ticket> tickets = new List<Ticket>
    {
        new Ticket { Id = 1, Naam = "Anna Peeters", Rij = 1, Stoel = 4, Prijs = 45.00m, IsGeannuleerd = false },
        new Ticket { Id = 2, Naam = "Bram Claes", Rij = 1, Stoel = 5, Prijs = 45.00m, IsGeannuleerd = false },
        new Ticket { Id = 3, Naam = "Chloé Maes", Rij = 2, Stoel = 5, Prijs = 38.00m, IsGeannuleerd = false },
        new Ticket { Id = 4, Naam = "Daan Wouters", Rij = 3, Stoel = 1, Prijs = 32.00m, IsGeannuleerd = true },
        new Ticket { Id = 5, Naam = "Eva Jacobs", Rij = 4, Stoel = 8, Prijs = 32.00m, IsGeannuleerd = false }
    };
    [HttpGet]
    public ActionResult<List<Ticket>> GetGeldig()
    {
        return Ok(tickets.Where(t => !t.IsGeannuleerd).ToList());
    }

    [HttpGet("alles")]
    public ActionResult<List<Ticket>> GetAlles()
    {
        return Ok(tickets);
    }

    [HttpGet("{id}")]
    public ActionResult<Ticket> GetById(int id)
    {
        Ticket? ticket = ZoekGeldig(id);

        if (ticket == null)
        {
            return NotFound($"We vonden geen geldig ticket met Id {id}.");
        }

        return Ok(ticket);
    }

    private Ticket? ZoekGeldig(int id)
    {
        return tickets.FirstOrDefault(t => t.Id == id && !t.IsGeannuleerd);
    }

    [HttpPost]
    public ActionResult<Ticket> Verkoop(Ticket nieuw)
    {
        string? fout = Controleer(nieuw, 0);
        if (fout != null)
        {
            return BadRequest(fout);
        }

        nieuw.Id = VolgendId();
        nieuw.IsGeannuleerd = false;
        tickets.Add(nieuw);

        return CreatedAtAction(nameof(GetById), new { id = nieuw.Id }, nieuw);
    }

    // eigenId: het ticket dat zijn eigen plaats mag houden. Bij een nieuw ticket is dat 0:
    // geen enkel bestaand ticket heeft Id 0.
    private string? Controleer(Ticket ticket, int eigenId)
    {
        if (string.IsNullOrWhiteSpace(ticket.Naam))
        {
            return "Een ticket moet op naam staan.";
        }

        string? plaatsfout = ControleerPlaats(ticket.Rij, ticket.Stoel);
        if (plaatsfout != null)
        {
            return plaatsfout;
        }

        if (ticket.Prijs <= 0)
        {
            return "De prijs moet groter zijn dan 0.";
        }

        if (IsBezet(ticket.Rij, ticket.Stoel, eigenId))
        {
            return $"Rij {ticket.Rij} stoel {ticket.Stoel} is al verkocht.";
        }

        return null;
    }

    private string? ControleerPlaats(int rij, int stoel)
    {
        if (rij < 1)
        {
            return "De rij moet minstens 1 zijn.";
        }

        if (stoel < 1)
        {
            return "De stoel moet minstens 1 zijn.";
        }

        return null;
    }

    // Zit er op deze plaats een geldig ticket, ander dan het ticket met eigenId?
    private bool IsBezet(int rij, int stoel, int eigenId)
    {
        return tickets.Any(t => !t.IsGeannuleerd
                             && t.Rij == rij
                             && t.Stoel == stoel
                             && t.Id != eigenId);
    }

    private int VolgendId()
    {
        if (tickets.Count == 0)
        {
            return 1;
        }

        return tickets.Max(t => t.Id) + 1;
    }

    [HttpPut("{id}")]
    public ActionResult Wijzig(int id, Ticket gewijzigd)
    {
        Ticket? bestaand = ZoekGeldig(id);

        if (bestaand == null)
        {
            return NotFound($"We vonden geen geldig ticket met Id {id}.");
        }

        string? fout = Controleer(gewijzigd, id);
        if (fout != null)
        {
            return BadRequest(fout);
        }

        bestaand.Naam = gewijzigd.Naam;
        bestaand.Rij = gewijzigd.Rij;
        bestaand.Stoel = gewijzigd.Stoel;
        bestaand.Prijs = gewijzigd.Prijs;

        return NoContent();
    }

    [HttpDelete("{id}")]
    public ActionResult Annuleer(int id)
    {
        Ticket? ticket = ZoekGeldig(id);

        if (ticket == null)
        {
            return NotFound($"Kan ticket met Id {id} niet annuleren, omdat het niet gevonden is.");
        }

        ticket.IsGeannuleerd = true;

        return NoContent();
    }

    [HttpPut("herstel/{id}")]
    public ActionResult Herstel(int id)
    {
        Ticket? ticket = tickets.FirstOrDefault(t => t.Id == id);

        if (ticket == null)
        {
            return NotFound($"We vonden geen ticket met Id {id}.");
        }

        if (!ticket.IsGeannuleerd)
        {
            return BadRequest("Dit ticket is niet geannuleerd.");
        }

        if (IsBezet(ticket.Rij, ticket.Stoel, ticket.Id))
        {
            return BadRequest($"Rij {ticket.Rij} stoel {ticket.Stoel} is intussen aan iemand anders verkocht.");
        }

        ticket.IsGeannuleerd = false;

        return NoContent();
    }

    [HttpGet("opbrengst")]
    public ActionResult<decimal> GetOpbrengst()
    {
        return Ok(tickets.Where(t => !t.IsGeannuleerd).Sum(t => t.Prijs));
    }

    [HttpPut("verhuis/{id}/{rij}/{stoel}")]
    public ActionResult Verhuis(int id, int rij, int stoel)
    {
        Ticket? ticket = ZoekGeldig(id);

        if (ticket == null)
        {
            return NotFound($"We vonden geen geldig ticket met Id {id}.");
        }

        string? plaatsfout = ControleerPlaats(rij, stoel);
        if (plaatsfout != null)
        {
            return BadRequest(plaatsfout);
        }

        if (ticket.Rij == rij && ticket.Stoel == stoel)
        {
            return BadRequest($"Dit ticket zit al op rij {rij} stoel {stoel}.");
        }

        if (IsBezet(rij, stoel, ticket.Id))
        {
            return BadRequest($"Rij {rij} stoel {stoel} is al verkocht.");
        }

        ticket.Rij = rij;
        ticket.Stoel = stoel;

        return NoContent();
    }
}
```

</details>
