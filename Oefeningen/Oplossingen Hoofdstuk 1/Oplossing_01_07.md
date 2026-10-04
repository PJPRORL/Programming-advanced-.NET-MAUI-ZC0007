# Oplossing 01_07

<span style="background:#c2255c;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>OPLOSSING</b></span> &nbsp;·&nbsp; <b>Hoofdstuk 01</b> &nbsp;·&nbsp; Eigen route en geneste beslissingen &nbsp;·&nbsp; Festival &nbsp;·&nbsp; bij `Oefening_01_07.md`

> [!IMPORTANT]
> Eerst zelf. Kijk per punt eerst naar de **nakijkpunten**. Haalt jouw API ze
> allemaal, dan is je punt juist, ook als je code er anders uitziet. Open de code
> pas daarna, om te vergelijken.

---

## De controller zelf

**Nakijkpunten**

| Verzoek | Status | Antwoord |
|---|---|---|
| <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> /api/podium</code> | <code style="color:#f08c00;font-weight:600">404 Not Found</code> | geen endpoint |

- `PodiumController` met `[Route("api/festival")]`. `/api/podium` bestaat niet.

<details>
<summary><span style="color:#845ef7"><b>Toon de code</b></span></summary>

```csharp
using Microsoft.AspNetCore.Mvc;

namespace Klankdal.Controllers;

[ApiController]
[Route("api/festival")]
public class PodiumController : ControllerBase
{

    // ... hier komen de endpoints uit de punten hieronder
}
```

</details>

---

## 1. Overzicht

**Nakijkpunten**

| Verzoek | Status | Antwoord |
|---|---|---|
| <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> /api/festival</code> | <code style="color:#37b24d;font-weight:600">200 OK</code> | <code style="color:#845ef7">Welkom op Klankdal. Drie podia, twee dagen, één weide.</code> |

<details>
<summary><span style="color:#845ef7"><b>Toon de code</b></span></summary>

```csharp
[HttpGet]
public string GetOverzicht()
{
    return "Welkom op Klankdal. Drie podia, twee dagen, één weide.";
}
```

</details>

---

## 2. Wie speelt er op een podium

**Nakijkpunten**

| Verzoek | Status | Antwoord |
|---|---|---|
| <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> /api/festival/podium/hoofdpodium/17</code> | <code style="color:#37b24d;font-weight:600">200 OK</code> | <code style="color:#845ef7">Op het hoofdpodium spelen nu de opwarmers.</code> |
| <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> /api/festival/podium/hoofdpodium/18</code> | <code style="color:#37b24d;font-weight:600">200 OK</code> | <code style="color:#845ef7">Op het hoofdpodium speelt nu de hoofdact.</code> |
| <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> /api/festival/podium/hoofdpodium/21</code> | <code style="color:#37b24d;font-weight:600">200 OK</code> | <code style="color:#845ef7">Op het hoofdpodium speelt nu de hoofdact.</code> |
| <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> /api/festival/podium/hoofdpodium/22</code> | <code style="color:#37b24d;font-weight:600">200 OK</code> | <code style="color:#845ef7">Het hoofdpodium is gesloten.</code> |
| <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> /api/festival/podium/tent/21</code> | <code style="color:#37b24d;font-weight:600">200 OK</code> | <code style="color:#845ef7">In de tent draaien de dj's van de dag.</code> |
| <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> /api/festival/podium/tent/22</code> | <code style="color:#37b24d;font-weight:600">200 OK</code> | <code style="color:#845ef7">In de tent begint nu de nachtset.</code> |
| <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> /api/festival/podium/strand/3</code> | <code style="color:#37b24d;font-weight:600">200 OK</code> | <code style="color:#845ef7">Op het strandpodium speelt akoestische muziek.</code> |
| <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> /api/festival/podium/kelder/20</code> | <code style="color:#37b24d;font-weight:600">200 OK</code> | <code style="color:#845ef7">Dit podium staat niet op het terrein.</code> |

**Aanpak.** Buiten het podium, binnen het uur. Bij het strand speelt het uur geen rol, en
een onbekend podium krijgt altijd hetzelfde antwoord. Het apostrof in *dj's* is een
gewoon teken in een C#-tekst.

<details>
<summary><span style="color:#845ef7"><b>Toon de code</b></span></summary>

```csharp
[HttpGet("podium/{podium}/{uur}")]
public string GetPodium(string podium, int uur)
{
    return PodiumberichtVoor(podium, uur);
}

private string PodiumberichtVoor(string podium, int uur)
{
    if (podium == "hoofdpodium")
    {
        if (uur < 18)
        {
            return "Op het hoofdpodium spelen nu de opwarmers.";
        }
        else if (uur <= 21)
        {
            return "Op het hoofdpodium speelt nu de hoofdact.";
        }
        else
        {
            return "Het hoofdpodium is gesloten.";
        }
    }
    else if (podium == "tent")
    {
        if (uur < 22)
        {
            return "In de tent draaien de dj's van de dag.";
        }
        else
        {
            return "In de tent begint nu de nachtset.";
        }
    }
    else if (podium == "strand")
    {
        return "Op het strandpodium speelt akoestische muziek.";
    }
    else
    {
        return "Dit podium staat niet op het terrein.";
    }
}
```

</details>

> [!WARNING]
> **Veelgemaakte fouten**
>
> - `uur < 21` voor de hoofdact. De opgave zegt *18 tot en met 21*, dus om 21 uur
>   speelt de hoofdact nog.

---

## 3. Prijs van een ticket

**Nakijkpunten**

| Verzoek | Status | Antwoord |
|---|---|---|
| <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> /api/festival/ticket/combi/8</code> | <code style="color:#37b24d;font-weight:600">200 OK</code> | <code style="color:#845ef7">Kinderen jonger dan 12 komen gratis binnen.</code> |
| <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> /api/festival/ticket/combi/15</code> | <code style="color:#37b24d;font-weight:600">200 OK</code> | <code style="color:#845ef7">Een combiticket kost jou 60 euro.</code> |
| <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> /api/festival/ticket/vip/30</code> | <code style="color:#37b24d;font-weight:600">200 OK</code> | <code style="color:#845ef7">Een vipticket kost jou 210 euro.</code> |
| <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> /api/festival/ticket/weekend/8</code> | <code style="color:#37b24d;font-weight:600">200 OK</code> | <code style="color:#845ef7">Dit tickettype bestaat niet.</code> |
| <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> /api/festival/ticket/dag/12</code> | <code style="color:#37b24d;font-weight:600">200 OK</code> | <code style="color:#845ef7">Een dagticket kost jou 32.5 euro.</code> |
| <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> /api/festival/ticket/dag/11</code> | <code style="color:#37b24d;font-weight:600">200 OK</code> | <code style="color:#845ef7">Kinderen jonger dan 12 komen gratis binnen.</code> |
| <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> /api/festival/ticket/vip/17</code> | <code style="color:#37b24d;font-weight:600">200 OK</code> | <code style="color:#845ef7">Een vipticket kost jou 105 euro.</code> |
| <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> /api/festival/ticket/vip/18</code> | <code style="color:#37b24d;font-weight:600">200 OK</code> | <code style="color:#845ef7">Een vipticket kost jou 210 euro.</code> |

**Aanpak.** Eerst het type: een onbekend type stopt meteen, ook voor een kind. Daarna de
leeftijd. De basisprijs is een `decimal`, omdat de helft van 65 euro 32,50 euro is.

<details>
<summary><span style="color:#845ef7"><b>Toon de code</b></span></summary>

```csharp
private const string OnbekendType = "Dit tickettype bestaat niet.";

[HttpGet("ticket/{type}/{leeftijd}")]
public string GetTicket(string type, int leeftijd)
{
    return TicketberichtVoor(type, leeftijd);
}

private string TicketberichtVoor(string type, int leeftijd)
{
    decimal basisprijs;
    switch (type)
    {
        case "dag":
            basisprijs = 65;
            break;
        case "combi":
            basisprijs = 120;
            break;
        case "vip":
            basisprijs = 210;
            break;
        default:
            return OnbekendType;
    }

    if (leeftijd < 12)
    {
        return "Kinderen jonger dan 12 komen gratis binnen.";
    }

    decimal prijs = basisprijs;
    if (leeftijd <= 17)
    {
        prijs = basisprijs / 2;
    }

    return $"Een {type}ticket kost jou {prijs} euro.";
}
```

</details>

> [!WARNING]
> **Veelgemaakte fouten**
>
> - De leeftijd eerst controleren. Dan krijgt een kind van 8 met een onbekend type
>   toch *gratis*: de valkuil uit de opgave.
> - Met `int` rekenen. `65 / 2` is dan 32, en de klant betaalt 50 cent te weinig
>   (leerboek, hoofdstuk 2).

> [!NOTE]
> Een jongere met een dagticket betaalt 32,5 euro. Of je API `32,5` of `32.5` toont,
> hangt af van de taalinstelling van de computer waarop hij draait. Op een Belgische
> Windows-pc wordt het `32,5`. Het nakijkpunt hierboven toont de Engelse schrijfwijze.

---

## 4. Wat moet je meenemen

**Nakijkpunten**

| Verzoek | Status | Antwoord |
|---|---|---|
| <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> /api/festival/weer/31/85</code> | <code style="color:#37b24d;font-weight:600">200 OK</code> | <code style="color:#845ef7">Neem een regenjas mee.</code> |
| <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> /api/festival/weer/31/20</code> | <code style="color:#37b24d;font-weight:600">200 OK</code> | <code style="color:#845ef7">Neem water en zonnecrème mee.</code> |
| <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> /api/festival/weer/9/20</code> | <code style="color:#37b24d;font-weight:600">200 OK</code> | <code style="color:#845ef7">Trek een warme trui aan.</code> |
| <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> /api/festival/weer/20/20</code> | <code style="color:#37b24d;font-weight:600">200 OK</code> | <code style="color:#845ef7">Ideaal festivalweer.</code> |
| <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> /api/festival/weer/12/70</code> | <code style="color:#37b24d;font-weight:600">200 OK</code> | <code style="color:#845ef7">Ideaal festivalweer.</code> |
| <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> /api/festival/weer/28/71</code> | <code style="color:#37b24d;font-weight:600">200 OK</code> | <code style="color:#845ef7">Neem een regenjas mee.</code> |
| <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> /api/festival/weer/11/71</code> | <code style="color:#37b24d;font-weight:600">200 OK</code> | <code style="color:#845ef7">Neem een regenjas mee.</code> |

**Aanpak.** Eén reeks `if` en `else if`, in de volgorde van belangrijkheid uit de opgave. Wie
regen krijgt, krijgt het regenadvies, ook als het warm is.

<details>
<summary><span style="color:#845ef7"><b>Toon de code</b></span></summary>

```csharp
[HttpGet("weer/{graden}/{regenkans}")]
public string GetWeer(int graden, int regenkans)
{
    return WeeradviesVoor(graden, regenkans);
}

private string WeeradviesVoor(int graden, int regenkans)
{
    if (regenkans > 70)
    {
        return "Neem een regenjas mee.";
    }
    else if (graden > 28)
    {
        return "Neem water en zonnecrème mee.";
    }
    else if (graden < 12)
    {
        return "Trek een warme trui aan.";
    }
    else
    {
        return "Ideaal festivalweer.";
    }
}
```

</details>

> [!WARNING]
> **Veelgemaakte fouten**
>
> - `if`'s in een andere volgorde dan de opgave, bijvoorbeeld eerst de temperatuur.
>   Dan krijgt `31/85` het advies voor zonnecrème in plaats van de regenjas.

> [!TIP]
> **Zo kan het beter**
>
> - Losse `if`'s die elk meteen een `return` doen, werken ook. `else if` toont
>   duidelijker dat er maar één antwoord kan zijn.

---

## 5. Alles samen: het bezoekersplan

**Nakijkpunten**

| Verzoek | Status | Antwoord |
|---|---|---|
| <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> /api/festival/plan/combi/15/hoofdpodium/19</code> | <code style="color:#37b24d;font-weight:600">200 OK</code> | <code style="color:#845ef7">Een combiticket kost jou 60 euro. Op het hoofdpodium speelt nu de hoofdact. Neem een regenjas mee.</code> |
| <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> /api/festival/plan/dag/30/strand/14</code> | <code style="color:#37b24d;font-weight:600">200 OK</code> | <code style="color:#845ef7">Een dagticket kost jou 65 euro. Op het strandpodium speelt akoestische muziek. Neem water en zonnecrème mee.</code> |
| <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> /api/festival/plan/weekend/30/strand/14</code> | <code style="color:#37b24d;font-weight:600">200 OK</code> | <code style="color:#845ef7">Dit tickettype bestaat niet.</code> |
| <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> /api/festival/plan/vip/30/kelder/23</code> | <code style="color:#37b24d;font-weight:600">200 OK</code> | <code style="color:#845ef7">Een vipticket kost jou 210 euro. Dit podium staat niet op het terrein. Trek een warme trui aan.</code> |
| <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> /api/festival/plan/dag/8/tent/11</code> | <code style="color:#37b24d;font-weight:600">200 OK</code> | <code style="color:#845ef7">Kinderen jonger dan 12 komen gratis binnen. In de tent draaien de dj's van de dag. Ideaal festivalweer.</code> |

**Aanpak.** Het ticket eerst, omdat een onbekend type het hele antwoord stopt. Daarna zet
het uur twee dingen klaar: het podiumbericht, via de methode uit punt 2, en het weer,
via de tabel. Een onbekend podium hoeft geen aparte behandeling: `PodiumberichtVoor`
geeft dan al de juiste zin, en de andere twee blijven gewoon staan.

<details>
<summary><span style="color:#845ef7"><b>Toon de code</b></span></summary>

```csharp
[HttpGet("plan/{type}/{leeftijd}/{podium}/{uur}")]
public string GetPlan(string type, int leeftijd, string podium, int uur)
{
    string ticket = TicketberichtVoor(type, leeftijd);
    if (ticket == OnbekendType)
    {
        return OnbekendType;
    }

    int graden;
    int regenkans;

    if (uur < 12)
    {
        graden = 14;
        regenkans = 20;
    }
    else if (uur <= 17)
    {
        graden = 29;
        regenkans = 10;
    }
    else if (uur <= 21)
    {
        graden = 22;
        regenkans = 80;
    }
    else
    {
        graden = 11;
        regenkans = 30;
    }

    return $"{ticket} {PodiumberichtVoor(podium, uur)} {WeeradviesVoor(graden, regenkans)}";
}
```

</details>

> [!WARNING]
> **Veelgemaakte fouten**
>
> - Bij een onbekend podium stoppen. De opgave zegt dat de eerste en de derde
>   boodschap blijven staan.

> [!TIP]
> **Zo kan het beter**
>
> - De weersvoorspelling per uur kan ook in een eigen hulpmethode. Hier staat ze in
>   het endpoint, omdat enkel dit endpoint ze nodig heeft.

---

## Alles samen: de volledige bestanden

Hetzelfde als hierboven, maar in één stuk. Handig om naast je eigen bestanden te leggen.

<details>
<summary><span style="color:#845ef7"><b>Toon PodiumController.cs</b></span></summary>

```csharp
using Microsoft.AspNetCore.Mvc;

namespace Klankdal.Controllers;

[ApiController]
[Route("api/festival")]
public class PodiumController : ControllerBase
{
    [HttpGet]
    public string GetOverzicht()
    {
        return "Welkom op Klankdal. Drie podia, twee dagen, één weide.";
    }

    [HttpGet("podium/{podium}/{uur}")]
    public string GetPodium(string podium, int uur)
    {
        return PodiumberichtVoor(podium, uur);
    }

    private string PodiumberichtVoor(string podium, int uur)
    {
        if (podium == "hoofdpodium")
        {
            if (uur < 18)
            {
                return "Op het hoofdpodium spelen nu de opwarmers.";
            }
            else if (uur <= 21)
            {
                return "Op het hoofdpodium speelt nu de hoofdact.";
            }
            else
            {
                return "Het hoofdpodium is gesloten.";
            }
        }
        else if (podium == "tent")
        {
            if (uur < 22)
            {
                return "In de tent draaien de dj's van de dag.";
            }
            else
            {
                return "In de tent begint nu de nachtset.";
            }
        }
        else if (podium == "strand")
        {
            return "Op het strandpodium speelt akoestische muziek.";
        }
        else
        {
            return "Dit podium staat niet op het terrein.";
        }
    }

    private const string OnbekendType = "Dit tickettype bestaat niet.";

    [HttpGet("ticket/{type}/{leeftijd}")]
    public string GetTicket(string type, int leeftijd)
    {
        return TicketberichtVoor(type, leeftijd);
    }

    private string TicketberichtVoor(string type, int leeftijd)
    {
        decimal basisprijs;
        switch (type)
        {
            case "dag":
                basisprijs = 65;
                break;
            case "combi":
                basisprijs = 120;
                break;
            case "vip":
                basisprijs = 210;
                break;
            default:
                return OnbekendType;
        }

        if (leeftijd < 12)
        {
            return "Kinderen jonger dan 12 komen gratis binnen.";
        }

        decimal prijs = basisprijs;
        if (leeftijd <= 17)
        {
            prijs = basisprijs / 2;
        }

        return $"Een {type}ticket kost jou {prijs} euro.";
    }

    [HttpGet("weer/{graden}/{regenkans}")]
    public string GetWeer(int graden, int regenkans)
    {
        return WeeradviesVoor(graden, regenkans);
    }

    private string WeeradviesVoor(int graden, int regenkans)
    {
        if (regenkans > 70)
        {
            return "Neem een regenjas mee.";
        }
        else if (graden > 28)
        {
            return "Neem water en zonnecrème mee.";
        }
        else if (graden < 12)
        {
            return "Trek een warme trui aan.";
        }
        else
        {
            return "Ideaal festivalweer.";
        }
    }

    [HttpGet("plan/{type}/{leeftijd}/{podium}/{uur}")]
    public string GetPlan(string type, int leeftijd, string podium, int uur)
    {
        string ticket = TicketberichtVoor(type, leeftijd);
        if (ticket == OnbekendType)
        {
            return OnbekendType;
        }

        int graden;
        int regenkans;

        if (uur < 12)
        {
            graden = 14;
            regenkans = 20;
        }
        else if (uur <= 17)
        {
            graden = 29;
            regenkans = 10;
        }
        else if (uur <= 21)
        {
            graden = 22;
            regenkans = 80;
        }
        else
        {
            graden = 11;
            regenkans = 30;
        }

        return $"{ticket} {PodiumberichtVoor(podium, uur)} {WeeradviesVoor(graden, regenkans)}";
    }
}
```

</details>
