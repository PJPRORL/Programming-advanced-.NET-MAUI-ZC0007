# Oplossing 01_06

<span style="background:#c2255c;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>OPLOSSING</b></span> &nbsp;·&nbsp; <b>Hoofdstuk 01</b> &nbsp;·&nbsp; Twee routeparameters in één route &nbsp;·&nbsp; Zwembad &nbsp;·&nbsp; bij `Oefening_01_06.md`

> [!IMPORTANT]
> Eerst zelf. Kijk per punt eerst naar de **nakijkpunten**. Haalt jouw API ze
> allemaal, dan is je punt juist, ook als je code er anders uitziet. Open de code
> pas daarna, om te vergelijken.

---

## De controller zelf

**Nakijkpunten**

- `ZwembadController`, met `[Route("[controller]")]`: alle adressen beginnen met `/zwembad`.

<details>
<summary><span style="color:#845ef7"><b>Toon de code</b></span></summary>

```csharp
using Microsoft.AspNetCore.Mvc;

namespace DeWaterval.Controllers;

[ApiController]
[Route("[controller]")]
public class ZwembadController : ControllerBase
{

    // ... hier komen de endpoints uit de punten hieronder
}
```

</details>

---

## 1. Openingsuren

**Nakijkpunten**

| Verzoek | Status | Antwoord |
|---|---|---|
| <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> /zwembad</code> | <code style="color:#37b24d;font-weight:600">200 OK</code> | <code style="color:#845ef7">Zwembad De Waterval is elke dag open van 7 tot 21 uur.</code> |

<details>
<summary><span style="color:#845ef7"><b>Toon de code</b></span></summary>

```csharp
[HttpGet]
public string GetOpeningsuren()
{
    return "Zwembad De Waterval is elke dag open van 7 tot 21 uur.";
}
```

</details>

---

## 2. Tarief berekenen

**Nakijkpunten**

| Verzoek | Status | Antwoord |
|---|---|---|
| <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> /zwembad/tarief/2/weekend</code> | <code style="color:#37b24d;font-weight:600">200 OK</code> | <code style="color:#845ef7">Gratis.</code> |
| <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> /zwembad/tarief/30/week</code> | <code style="color:#37b24d;font-weight:600">200 OK</code> | <code style="color:#845ef7">Je betaalt 4 euro.</code> |
| <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> /zwembad/tarief/30/weekend</code> | <code style="color:#37b24d;font-weight:600">200 OK</code> | <code style="color:#845ef7">Je betaalt 5 euro.</code> |
| <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> /zwembad/tarief/3/week</code> | <code style="color:#37b24d;font-weight:600">200 OK</code> | <code style="color:#845ef7">Je betaalt 2 euro.</code> |
| <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> /zwembad/tarief/17/weekend</code> | <code style="color:#37b24d;font-weight:600">200 OK</code> | <code style="color:#845ef7">Je betaalt 3 euro.</code> |
| <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> /zwembad/tarief/64/week</code> | <code style="color:#37b24d;font-weight:600">200 OK</code> | <code style="color:#845ef7">Je betaalt 4 euro.</code> |
| <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> /zwembad/tarief/65/week</code> | <code style="color:#37b24d;font-weight:600">200 OK</code> | <code style="color:#845ef7">Je betaalt 3 euro.</code> |
| <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> /zwembad/tarief/65/weekend</code> | <code style="color:#37b24d;font-weight:600">200 OK</code> | <code style="color:#845ef7">Je betaalt 4 euro.</code> |
| <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> /zwembad/tarief/2/maandag</code> | <code style="color:#37b24d;font-weight:600">200 OK</code> | <code style="color:#845ef7">We kennen deze dag niet.</code> |

**Aanpak.** Eerst de dag: is die onbekend, dan maakt de leeftijd niet meer uit. Daarna de
gratis groep, die geen prijs heeft. Voor de andere groepen kies je eerst de
weekprijs, en tel je er in het weekend 1 bij. Zo staat elk tarief maar één keer in je
code, in plaats van acht keer.

<details>
<summary><span style="color:#845ef7"><b>Toon de code</b></span></summary>

```csharp
private const string OnbekendeDag = "We kennen deze dag niet.";

[HttpGet("tarief/{leeftijd}/{dag}")]
public string GetTarief(int leeftijd, string dag)
{
    return TariefVoor(leeftijd, dag);
}

private string TariefVoor(int leeftijd, string dag)
{
    if (dag != "week" && dag != "weekend")
    {
        return OnbekendeDag;
    }

    if (leeftijd < 3)
    {
        return "Gratis.";
    }

    int prijs;
    if (leeftijd <= 17)
    {
        prijs = 2;
    }
    else if (leeftijd <= 64)
    {
        prijs = 4;
    }
    else
    {
        prijs = 3;
    }

    if (dag == "weekend")
    {
        prijs = prijs + 1;
    }

    return $"Je betaalt {prijs} euro.";
}
```

</details>

> [!WARNING]
> **Veelgemaakte fouten**
>
> - Eerst naar de leeftijd kijken. Dan krijgt een kind van 2 op `maandag` *Gratis.* in
>   plaats van *We kennen deze dag niet.*
> - Ook bij *gratis* 1 euro bijtellen in het weekend, of de acht prijzen elk apart
>   uitschrijven en er één verkeerd typen.

---

## 3. Watertemperatuur

**Nakijkpunten**

| Verzoek | Status | Antwoord |
|---|---|---|
| <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> /zwembad/temperatuur/23</code> | <code style="color:#37b24d;font-weight:600">200 OK</code> | <code style="color:#845ef7">Het water is fris, neem je tijd om in te stappen.</code> |
| <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> /zwembad/temperatuur/24</code> | <code style="color:#37b24d;font-weight:600">200 OK</code> | <code style="color:#845ef7">Het water heeft een aangename temperatuur.</code> |
| <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> /zwembad/temperatuur/28</code> | <code style="color:#37b24d;font-weight:600">200 OK</code> | <code style="color:#845ef7">Het water heeft een aangename temperatuur.</code> |
| <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> /zwembad/temperatuur/29</code> | <code style="color:#37b24d;font-weight:600">200 OK</code> | <code style="color:#845ef7">Het water is warm, ideaal voor de kleinsten.</code> |

**Aanpak.** Drie groepen met grenzen op 24 en 28. Let op het woord *tot en met*: 28 graden is nog aangenaam.

<details>
<summary><span style="color:#845ef7"><b>Toon de code</b></span></summary>

```csharp
[HttpGet("temperatuur/{graden}")]
public string GetTemperatuur(int graden)
{
    return TemperatuurberichtVoor(graden);
}

private string TemperatuurberichtVoor(int graden)
{
    if (graden < 24)
    {
        return "Het water is fris, neem je tijd om in te stappen.";
    }
    else if (graden <= 28)
    {
        return "Het water heeft een aangename temperatuur.";
    }
    else
    {
        return "Het water is warm, ideaal voor de kleinsten.";
    }
}
```

</details>

---

## 4. Drukte in de banen

**Nakijkpunten**

| Verzoek | Status | Antwoord |
|---|---|---|
| <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> /zwembad/banen/4/3</code> | <code style="color:#37b24d;font-weight:600">200 OK</code> | <code style="color:#845ef7">Iedereen krijgt een eigen baan.</code> |
| <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> /zwembad/banen/4/8</code> | <code style="color:#37b24d;font-weight:600">200 OK</code> | <code style="color:#845ef7">Er zwemmen 2 zwemmers per baan.</code> |
| <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> /zwembad/banen/4/10</code> | <code style="color:#37b24d;font-weight:600">200 OK</code> | <code style="color:#845ef7">Er zwemmen 2 zwemmers per baan, 2 banen krijgen er één extra.</code> |
| <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> /zwembad/banen/4/4</code> | <code style="color:#37b24d;font-weight:600">200 OK</code> | <code style="color:#845ef7">Iedereen krijgt een eigen baan.</code> |

**Aanpak.** Eerst het eenvoudige geval: genoeg banen. Daarna twee berekeningen met gehele
getallen: `/` geeft hoeveel zwemmers er per baan zijn, `%` hoeveel er overblijven
(leerboek, hoofdstuk 2). Die rest is precies het aantal banen dat één zwemmer extra
krijgt.

<details>
<summary><span style="color:#845ef7"><b>Toon de code</b></span></summary>

```csharp
[HttpGet("banen/{aantalBanen}/{aantalZwemmers}")]
public string GetBanen(int aantalBanen, int aantalZwemmers)
{
    return DrukteberichtVoor(aantalBanen, aantalZwemmers);
}

private string DrukteberichtVoor(int aantalBanen, int aantalZwemmers)
{
    if (aantalZwemmers <= aantalBanen)
    {
        return "Iedereen krijgt een eigen baan.";
    }

    int perBaan = aantalZwemmers / aantalBanen;
    int rest = aantalZwemmers % aantalBanen;

    if (rest == 0)
    {
        return $"Er zwemmen {perBaan} zwemmers per baan.";
    }

    return $"Er zwemmen {perBaan} zwemmers per baan, {rest} banen krijgen er één extra.";
}
```

</details>

> [!WARNING]
> **Veelgemaakte fouten**
>
> - Met `double` delen. Dan krijg je *2,5 zwemmers per baan*, en een rest die nergens
>   op slaat.

> [!TIP]
> **Zo kan het beter**
>
> - Met 0 banen, `/zwembad/banen/0/3`, deelt je code door nul en crasht ze: een 500.
>   Een korte controle vooraf voorkomt dat. De opgave vraagt het niet, maar het is een
>   goede gewoonte.

---

## 5. Alles samen: het bezoekrapport

**Nakijkpunten**

| Verzoek | Status | Antwoord |
|---|---|---|
| <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> /zwembad/bezoek/30/weekend/26/14</code> | <code style="color:#37b24d;font-weight:600">200 OK</code> | <code style="color:#845ef7">Je betaalt 5 euro. Het water heeft een aangename temperatuur. Er zwemmen 2 zwemmers per baan, 2 banen krijgen er één extra.</code> |
| <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> /zwembad/bezoek/2/week/22/4</code> | <code style="color:#37b24d;font-weight:600">200 OK</code> | <code style="color:#845ef7">Gratis. Het water is fris, neem je tijd om in te stappen. Iedereen krijgt een eigen baan.</code> |
| <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> /zwembad/bezoek/30/feestdag/26/14</code> | <code style="color:#37b24d;font-weight:600">200 OK</code> | <code style="color:#845ef7">We kennen deze dag niet.</code> |

**Aanpak.** Het aantal banen is een `const` van de controller: het hoort bij het zwembad, niet
bij het verzoek. Het tarief komt eerst, omdat een onbekende dag het hele antwoord
stopt. Dat herken je aan de tekst die `TariefVoor` teruggeeft, dezelfde `const` als in
punt 2.

<details>
<summary><span style="color:#845ef7"><b>Toon de code</b></span></summary>

```csharp
private const int AantalBanen = 6;

[HttpGet("bezoek/{leeftijd}/{dag}/{graden}/{aantalZwemmers}")]
public string GetBezoek(int leeftijd, string dag, int graden, int aantalZwemmers)
{
    string tarief = TariefVoor(leeftijd, dag);
    if (tarief == OnbekendeDag)
    {
        return OnbekendeDag;
    }

    return $"{tarief} {TemperatuurberichtVoor(graden)} {DrukteberichtVoor(AantalBanen, aantalZwemmers)}";
}
```

</details>

> [!WARNING]
> **Veelgemaakte fouten**
>
> - Het aantal banen alsnog in de URL zetten. De opgave zegt dat de controller het
>   weet.

> [!TIP]
> **Zo kan het beter**
>
> - Vergelijken met de tekst van een bericht werkt, omdat die tekst in een `const`
>   staat. Verander je het bericht, dan verandert de vergelijking mee. Een aparte
>   controle op de dag, vooraan in `GetBezoek`, is een even goede keuze.

---

## Alles samen: de volledige bestanden

Hetzelfde als hierboven, maar in één stuk. Handig om naast je eigen bestanden te leggen.

<details>
<summary><span style="color:#845ef7"><b>Toon ZwembadController.cs</b></span></summary>

```csharp
using Microsoft.AspNetCore.Mvc;

namespace DeWaterval.Controllers;

[ApiController]
[Route("[controller]")]
public class ZwembadController : ControllerBase
{
    [HttpGet]
    public string GetOpeningsuren()
    {
        return "Zwembad De Waterval is elke dag open van 7 tot 21 uur.";
    }

    private const string OnbekendeDag = "We kennen deze dag niet.";

    [HttpGet("tarief/{leeftijd}/{dag}")]
    public string GetTarief(int leeftijd, string dag)
    {
        return TariefVoor(leeftijd, dag);
    }

    private string TariefVoor(int leeftijd, string dag)
    {
        if (dag != "week" && dag != "weekend")
        {
            return OnbekendeDag;
        }

        if (leeftijd < 3)
        {
            return "Gratis.";
        }

        int prijs;
        if (leeftijd <= 17)
        {
            prijs = 2;
        }
        else if (leeftijd <= 64)
        {
            prijs = 4;
        }
        else
        {
            prijs = 3;
        }

        if (dag == "weekend")
        {
            prijs = prijs + 1;
        }

        return $"Je betaalt {prijs} euro.";
    }

    [HttpGet("temperatuur/{graden}")]
    public string GetTemperatuur(int graden)
    {
        return TemperatuurberichtVoor(graden);
    }

    private string TemperatuurberichtVoor(int graden)
    {
        if (graden < 24)
        {
            return "Het water is fris, neem je tijd om in te stappen.";
        }
        else if (graden <= 28)
        {
            return "Het water heeft een aangename temperatuur.";
        }
        else
        {
            return "Het water is warm, ideaal voor de kleinsten.";
        }
    }

    [HttpGet("banen/{aantalBanen}/{aantalZwemmers}")]
    public string GetBanen(int aantalBanen, int aantalZwemmers)
    {
        return DrukteberichtVoor(aantalBanen, aantalZwemmers);
    }

    private string DrukteberichtVoor(int aantalBanen, int aantalZwemmers)
    {
        if (aantalZwemmers <= aantalBanen)
        {
            return "Iedereen krijgt een eigen baan.";
        }

        int perBaan = aantalZwemmers / aantalBanen;
        int rest = aantalZwemmers % aantalBanen;

        if (rest == 0)
        {
            return $"Er zwemmen {perBaan} zwemmers per baan.";
        }

        return $"Er zwemmen {perBaan} zwemmers per baan, {rest} banen krijgen er één extra.";
    }

    private const int AantalBanen = 6;

    [HttpGet("bezoek/{leeftijd}/{dag}/{graden}/{aantalZwemmers}")]
    public string GetBezoek(int leeftijd, string dag, int graden, int aantalZwemmers)
    {
        string tarief = TariefVoor(leeftijd, dag);
        if (tarief == OnbekendeDag)
        {
            return OnbekendeDag;
        }

        return $"{tarief} {TemperatuurberichtVoor(graden)} {DrukteberichtVoor(AantalBanen, aantalZwemmers)}";
    }
}
```

</details>
