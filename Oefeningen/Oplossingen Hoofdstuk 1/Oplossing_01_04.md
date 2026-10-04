# Oplossing 01_04

<span style="background:#c2255c;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>OPLOSSING</b></span> &nbsp;·&nbsp; <b>Hoofdstuk 01</b> &nbsp;·&nbsp; Eigen route en geneste beslissingen &nbsp;·&nbsp; PC-onderdelen &nbsp;·&nbsp; bij `Oefening_01_04.md`

> [!IMPORTANT]
> Eerst zelf. Kijk per punt eerst naar de **nakijkpunten**. Haalt jouw API ze
> allemaal, dan is je punt juist, ook als je code er anders uitziet. Open de code
> pas daarna, om te vergelijken.

---

## De controller zelf

**Nakijkpunten**

| Verzoek | Status | Antwoord |
|---|---|---|
| <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> /api/advies</code> | <code style="color:#f08c00;font-weight:600">404 Not Found</code> | geen endpoint |

- De klasse heet `AdviesController`, maar de route staat er letterlijk boven:
  `[Route("api/bouwadvies")]`. Zonder `[controller]` speelt de klassenaam geen rol meer.
- `/api/advies` geeft niets terug: er bestaat geen enkel endpoint op dat adres.

<details>
<summary><span style="color:#845ef7"><b>Toon de code</b></span></summary>

```csharp
using Microsoft.AspNetCore.Mvc;

namespace BitEnByte.Controllers;

[ApiController]
[Route("api/bouwadvies")]
public class AdviesController : ControllerBase
{

    // ... hier komen de endpoints uit de punten hieronder
}
```

</details>

> [!WARNING]
> **Veelgemaakte fouten**
>
> - `[Route("api/[controller]")]` laten staan en de klasse `BouwadviesController`
>   noemen. Dat werkt, maar de opgave wil net dat de klasse anders heet dan de route.

---

## 1. Overzicht

**Nakijkpunten**

| Verzoek | Status | Antwoord |
|---|---|---|
| <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> /api/bouwadvies</code> | <code style="color:#37b24d;font-weight:600">200 OK</code> | <code style="color:#845ef7">Adviespaneel Bit &amp; Byte. Vraag advies op over je budget, je doel of je koeling.</code> |

<details>
<summary><span style="color:#845ef7"><b>Toon de code</b></span></summary>

```csharp
[HttpGet]
public string GetOverzicht()
{
    return "Adviespaneel Bit & Byte. Vraag advies op over je budget, je doel of je koeling.";
}
```

</details>

---

## 2. Advies op basis van budget

**Nakijkpunten**

| Verzoek | Status | Antwoord |
|---|---|---|
| <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> /api/bouwadvies/budget/599</code> | <code style="color:#37b24d;font-weight:600">200 OK</code> | <code style="color:#845ef7">Met dit budget kies je best een tweedehandstoestel.</code> |
| <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> /api/bouwadvies/budget/600</code> | <code style="color:#37b24d;font-weight:600">200 OK</code> | <code style="color:#845ef7">Met dit budget bouw je een degelijke kantoor-pc.</code> |
| <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> /api/bouwadvies/budget/1199</code> | <code style="color:#37b24d;font-weight:600">200 OK</code> | <code style="color:#845ef7">Met dit budget bouw je een degelijke kantoor-pc.</code> |
| <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> /api/bouwadvies/budget/1200</code> | <code style="color:#37b24d;font-weight:600">200 OK</code> | <code style="color:#845ef7">Met dit budget bouw je een volwaardige gaming-pc.</code> |
| <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> /api/bouwadvies/budget/2499</code> | <code style="color:#37b24d;font-weight:600">200 OK</code> | <code style="color:#845ef7">Met dit budget bouw je een volwaardige gaming-pc.</code> |
| <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> /api/bouwadvies/budget/2500</code> | <code style="color:#37b24d;font-weight:600">200 OK</code> | <code style="color:#845ef7">Met dit budget kan je vrijwel alles bouwen.</code> |

**Aanpak.** Vier groepen, dus drie grenzen. Met `<` en een oplopende volgorde hoef je enkel
de bovengrens van elke groep te schrijven: wie bij `bedrag < 1200` aankomt, zit
zeker al boven de 599.

<details>
<summary><span style="color:#845ef7"><b>Toon de code</b></span></summary>

```csharp
[HttpGet("budget/{bedrag}")]
public string GetBudget(int bedrag)
{
    return BudgetadviesVoor(bedrag);
}

private string BudgetadviesVoor(int bedrag)
{
    if (bedrag < 600)
    {
        return "Met dit budget kies je best een tweedehandstoestel.";
    }
    else if (bedrag < 1200)
    {
        return "Met dit budget bouw je een degelijke kantoor-pc.";
    }
    else if (bedrag < 2500)
    {
        return "Met dit budget bouw je een volwaardige gaming-pc.";
    }
    else
    {
        return "Met dit budget kan je vrijwel alles bouwen.";
    }
}
```

</details>

> [!WARNING]
> **Veelgemaakte fouten**
>
> - `bedrag <= 1200` voor de kantoorgroep. De opgave zegt *600 tot en met 1199*, dus
>   1200 hoort al bij gaming.

---

## 3. Advies op basis van doel en budget

**Nakijkpunten**

| Verzoek | Status | Antwoord |
|---|---|---|
| <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> /api/bouwadvies/doel/gaming/900</code> | <code style="color:#37b24d;font-weight:600">200 OK</code> | <code style="color:#845ef7">Voor een gaming-pc reken je op minstens 1200 euro. Je komt 300 euro te kort.</code> |
| <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> /api/bouwadvies/doel/kantoor/900</code> | <code style="color:#37b24d;font-weight:600">200 OK</code> | <code style="color:#845ef7">Een kantoor-pc is haalbaar binnen dit budget.</code> |
| <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> /api/bouwadvies/doel/server/900</code> | <code style="color:#37b24d;font-weight:600">200 OK</code> | <code style="color:#845ef7">We kennen dit doel niet.</code> |
| <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> /api/bouwadvies/doel/montage/1500</code> | <code style="color:#37b24d;font-weight:600">200 OK</code> | <code style="color:#845ef7">Een montage-pc is haalbaar binnen dit budget.</code> |

**Aanpak.** `MinimumVoor` zet een doel om naar zijn minimumbudget, en geeft 0 terug voor een
doel dat niet bestaat. Het endpoint kijkt dus eerst of het doel bekend is, en pas
daarna naar het bedrag. Het tekort is minimum min bedrag.

<details>
<summary><span style="color:#845ef7"><b>Toon de code</b></span></summary>

```csharp
private const string OnbekendDoel = "We kennen dit doel niet.";

[HttpGet("doel/{doel}/{bedrag}")]
public string GetDoel(string doel, int bedrag)
{
    int minimum = MinimumVoor(doel);
    if (minimum == 0)
    {
        return OnbekendDoel;
    }

    return DoeladviesVoor(doel, minimum, bedrag);
}

// Geeft 0 terug voor een doel dat we niet kennen.
private int MinimumVoor(string doel)
{
    switch (doel)
    {
        case "kantoor":
            return 600;
        case "gaming":
            return 1200;
        case "montage":
            return 1500;
        default:
            return 0;
    }
}

private string DoeladviesVoor(string doel, int minimum, int bedrag)
{
    if (bedrag < minimum)
    {
        int tekort = minimum - bedrag;
        return $"Voor een {doel}-pc reken je op minstens {minimum} euro. Je komt {tekort} euro te kort.";
    }

    return $"Een {doel}-pc is haalbaar binnen dit budget.";
}
```

</details>

> [!WARNING]
> **Veelgemaakte fouten**
>
> - Het tekort omgekeerd berekenen, bedrag min minimum. Dan staat er een negatief
>   getal in je bericht.
> - Eerst naar het bedrag kijken. Dan geeft *server* met 900 euro een antwoord over
>   een pc die je niet kent.

> [!TIP]
> **Zo kan het beter**
>
> - 0 als teken voor *onbekend* werkt omdat geen enkel echt minimum 0 is. Een `int?`
>   met `null` (leerboek, hoofdstuk 17) zegt duidelijker dat er geen antwoord is.

---

## 4. Advies over de koeling

**Nakijkpunten**

| Verzoek | Status | Antwoord |
|---|---|---|
| <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> /api/bouwadvies/koeling/95/lucht</code> | <code style="color:#37b24d;font-weight:600">200 OK</code> | <code style="color:#845ef7">Een gewone luchtkoeler volstaat.</code> |
| <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> /api/bouwadvies/koeling/96/lucht</code> | <code style="color:#37b24d;font-weight:600">200 OK</code> | <code style="color:#845ef7">Kies een zware luchtkoeler.</code> |
| <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> /api/bouwadvies/koeling/150/lucht</code> | <code style="color:#37b24d;font-weight:600">200 OK</code> | <code style="color:#845ef7">Kies een zware luchtkoeler.</code> |
| <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> /api/bouwadvies/koeling/151/lucht</code> | <code style="color:#37b24d;font-weight:600">200 OK</code> | <code style="color:#845ef7">Luchtkoeling volstaat niet, ga voor waterkoeling.</code> |
| <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> /api/bouwadvies/koeling/250/water</code> | <code style="color:#37b24d;font-weight:600">200 OK</code> | <code style="color:#845ef7">Een waterkoeling van 240 mm volstaat.</code> |
| <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> /api/bouwadvies/koeling/251/water</code> | <code style="color:#37b24d;font-weight:600">200 OK</code> | <code style="color:#845ef7">Kies een radiator van 360 mm of groter.</code> |
| <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> /api/bouwadvies/koeling/50/stikstof</code> | <code style="color:#37b24d;font-weight:600">200 OK</code> | <code style="color:#845ef7">We kennen dit soort koeling niet.</code> |
| <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> /api/bouwadvies/koeling/500/stikstof</code> | <code style="color:#37b24d;font-weight:600">200 OK</code> | <code style="color:#845ef7">We kennen dit soort koeling niet.</code> |

**Aanpak.** De buitenste `if` kijkt naar het soort koeling, de binnenste naar het getal.
Zo komt een onbekende koeling nooit bij een getalgrens terecht, en krijgt ze altijd
hetzelfde antwoord, hoe groot het getal ook is.

<details>
<summary><span style="color:#845ef7"><b>Toon de code</b></span></summary>

```csharp
private const string OnbekendeKoeling = "We kennen dit soort koeling niet.";

[HttpGet("koeling/{tdp}/{koeling}")]
public string GetKoeling(int tdp, string koeling)
{
    return KoeladviesVoor(tdp, koeling);
}

private string KoeladviesVoor(int tdp, string koeling)
{
    if (koeling == "lucht")
    {
        if (tdp <= 95)
        {
            return "Een gewone luchtkoeler volstaat.";
        }
        else if (tdp <= 150)
        {
            return "Kies een zware luchtkoeler.";
        }
        else
        {
            return "Luchtkoeling volstaat niet, ga voor waterkoeling.";
        }
    }
    else if (koeling == "water")
    {
        if (tdp <= 250)
        {
            return "Een waterkoeling van 240 mm volstaat.";
        }
        else
        {
            return "Kies een radiator van 360 mm of groter.";
        }
    }
    else
    {
        return OnbekendeKoeling;
    }
}
```

</details>

> [!WARNING]
> **Veelgemaakte fouten**
>
> - Eerst naar het getal kijken: `if (tdp <= 95)` als buitenste controle. Dan krijgt
>   `50/stikstof` het advies voor een luchtkoeler. Dat is de valkuil uit de opgave.

---

## 5. Alles samen: het volledige advies

**Nakijkpunten**

| Verzoek | Status | Antwoord |
|---|---|---|
| <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> /api/bouwadvies/volledig/gaming/1500/140/lucht</code> | <code style="color:#37b24d;font-weight:600">200 OK</code> | <code style="color:#845ef7">Met dit budget bouw je een volwaardige gaming-pc. Een gaming-pc is haalbaar binnen dit budget. Kies een zware luchtkoeler.</code> |
| <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> /api/bouwadvies/volledig/montage/1000/200/lucht</code> | <code style="color:#37b24d;font-weight:600">200 OK</code> | <code style="color:#845ef7">Met dit budget bouw je een degelijke kantoor-pc. Voor een montage-pc reken je op minstens 1500 euro. Je komt 500 euro te kort. Luchtkoeling volstaat niet, ga voor waterkoeling. Voor een montage-pc raden we water aan.</code> |
| <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> /api/bouwadvies/volledig/server/1000/100/lucht</code> | <code style="color:#37b24d;font-weight:600">200 OK</code> | <code style="color:#845ef7">We kennen dit doel niet.</code> |
| <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> /api/bouwadvies/volledig/gaming/1500/140/stikstof</code> | <code style="color:#37b24d;font-weight:600">200 OK</code> | <code style="color:#845ef7">Met dit budget bouw je een volwaardige gaming-pc. Een gaming-pc is haalbaar binnen dit budget. We kennen dit soort koeling niet.</code> |
| <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> /api/bouwadvies/volledig/kantoor/700/65/water</code> | <code style="color:#37b24d;font-weight:600">200 OK</code> | <code style="color:#845ef7">Met dit budget bouw je een degelijke kantoor-pc. Een kantoor-pc is haalbaar binnen dit budget. Een waterkoeling van 240 mm volstaat. Voor een kantoor-pc raden we lucht aan.</code> |

**Aanpak.** Eerst het doel: onbekend is meteen klaar. Daarna de drie adviezen, allemaal uit
de hulpmethodes van de vorige punten. De vierde zin komt er enkel bij als de
koeling bekend is **en** verschilt van de aangeraden koeling. `MinimumVoor` uit punt 3
doet hier twee dingen: het zegt of het doel bestaat, en het levert het minimum.

<details>
<summary><span style="color:#845ef7"><b>Toon de code</b></span></summary>

```csharp
[HttpGet("volledig/{doel}/{bedrag}/{tdp}/{koeling}")]
public string GetVolledig(string doel, int bedrag, int tdp, string koeling)
{
    int minimum = MinimumVoor(doel);
    if (minimum == 0)
    {
        return OnbekendDoel;
    }

    string advies = $"{BudgetadviesVoor(bedrag)} {DoeladviesVoor(doel, minimum, bedrag)} {KoeladviesVoor(tdp, koeling)}";

    bool koelingBekend = koeling == "lucht" || koeling == "water";
    string aangeraden = AangeradenKoelingVoor(doel);

    if (koelingBekend && koeling != aangeraden)
    {
        advies += $" Voor een {doel}-pc raden we {aangeraden} aan.";
    }

    return advies;
}

// Wordt enkel opgeroepen voor een doel dat we kennen.
private string AangeradenKoelingVoor(string doel)
{
    if (doel == "montage")
    {
        return "water";
    }

    return "lucht";
}
```

</details>

> [!WARNING]
> **Veelgemaakte fouten**
>
> - De vierde zin ook toevoegen bij een onbekende koeling. De opgave zegt dat hij dan
>   wegvalt.
> - Een aparte `switch` voor de aangeraden koeling die ook onbekende doelen behandelt.
>   Dat is overbodig: wie daar aankomt, heeft al een bekend doel.

---

## Alles samen: de volledige bestanden

Hetzelfde als hierboven, maar in één stuk. Handig om naast je eigen bestanden te leggen.

<details>
<summary><span style="color:#845ef7"><b>Toon AdviesController.cs</b></span></summary>

```csharp
using Microsoft.AspNetCore.Mvc;

namespace BitEnByte.Controllers;

[ApiController]
[Route("api/bouwadvies")]
public class AdviesController : ControllerBase
{
    [HttpGet]
    public string GetOverzicht()
    {
        return "Adviespaneel Bit & Byte. Vraag advies op over je budget, je doel of je koeling.";
    }

    [HttpGet("budget/{bedrag}")]
    public string GetBudget(int bedrag)
    {
        return BudgetadviesVoor(bedrag);
    }

    private string BudgetadviesVoor(int bedrag)
    {
        if (bedrag < 600)
        {
            return "Met dit budget kies je best een tweedehandstoestel.";
        }
        else if (bedrag < 1200)
        {
            return "Met dit budget bouw je een degelijke kantoor-pc.";
        }
        else if (bedrag < 2500)
        {
            return "Met dit budget bouw je een volwaardige gaming-pc.";
        }
        else
        {
            return "Met dit budget kan je vrijwel alles bouwen.";
        }
    }

    private const string OnbekendDoel = "We kennen dit doel niet.";

    [HttpGet("doel/{doel}/{bedrag}")]
    public string GetDoel(string doel, int bedrag)
    {
        int minimum = MinimumVoor(doel);
        if (minimum == 0)
        {
            return OnbekendDoel;
        }

        return DoeladviesVoor(doel, minimum, bedrag);
    }

    // Geeft 0 terug voor een doel dat we niet kennen.
    private int MinimumVoor(string doel)
    {
        switch (doel)
        {
            case "kantoor":
                return 600;
            case "gaming":
                return 1200;
            case "montage":
                return 1500;
            default:
                return 0;
        }
    }

    private string DoeladviesVoor(string doel, int minimum, int bedrag)
    {
        if (bedrag < minimum)
        {
            int tekort = minimum - bedrag;
            return $"Voor een {doel}-pc reken je op minstens {minimum} euro. Je komt {tekort} euro te kort.";
        }

        return $"Een {doel}-pc is haalbaar binnen dit budget.";
    }

    private const string OnbekendeKoeling = "We kennen dit soort koeling niet.";

    [HttpGet("koeling/{tdp}/{koeling}")]
    public string GetKoeling(int tdp, string koeling)
    {
        return KoeladviesVoor(tdp, koeling);
    }

    private string KoeladviesVoor(int tdp, string koeling)
    {
        if (koeling == "lucht")
        {
            if (tdp <= 95)
            {
                return "Een gewone luchtkoeler volstaat.";
            }
            else if (tdp <= 150)
            {
                return "Kies een zware luchtkoeler.";
            }
            else
            {
                return "Luchtkoeling volstaat niet, ga voor waterkoeling.";
            }
        }
        else if (koeling == "water")
        {
            if (tdp <= 250)
            {
                return "Een waterkoeling van 240 mm volstaat.";
            }
            else
            {
                return "Kies een radiator van 360 mm of groter.";
            }
        }
        else
        {
            return OnbekendeKoeling;
        }
    }

    [HttpGet("volledig/{doel}/{bedrag}/{tdp}/{koeling}")]
    public string GetVolledig(string doel, int bedrag, int tdp, string koeling)
    {
        int minimum = MinimumVoor(doel);
        if (minimum == 0)
        {
            return OnbekendDoel;
        }

        string advies = $"{BudgetadviesVoor(bedrag)} {DoeladviesVoor(doel, minimum, bedrag)} {KoeladviesVoor(tdp, koeling)}";

        bool koelingBekend = koeling == "lucht" || koeling == "water";
        string aangeraden = AangeradenKoelingVoor(doel);

        if (koelingBekend && koeling != aangeraden)
        {
            advies += $" Voor een {doel}-pc raden we {aangeraden} aan.";
        }

        return advies;
    }

    // Wordt enkel opgeroepen voor een doel dat we kennen.
    private string AangeradenKoelingVoor(string doel)
    {
        if (doel == "montage")
        {
            return "water";
        }

        return "lucht";
    }
}
```

</details>
