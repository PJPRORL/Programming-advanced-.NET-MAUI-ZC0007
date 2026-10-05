# Oplossing 01_05

<span style="background:#c2255c;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>OPLOSSING</b></span> &nbsp;·&nbsp; <b>Hoofdstuk 01</b> &nbsp;·&nbsp; Controller en routeparameters &nbsp;·&nbsp; Bibliotheek &nbsp;·&nbsp; bij `Oefening_01_05.md`

> [!IMPORTANT]
> Eerst zelf. Kijk per punt eerst naar de **nakijkpunten**. Haalt jouw API ze
> allemaal, dan is je punt juist, ook als je code er anders uitziet. Open de code
> pas daarna, om te vergelijken.

---

## De controller zelf

**Nakijkpunten**

- `BibliotheekController`, met `[Route("[controller]")]`: alle adressen beginnen met `/bibliotheek`.

<details>
<summary><span style="color:#845ef7"><b>Toon de code</b></span></summary>

```csharp
using Microsoft.AspNetCore.Mvc;

namespace DeLeeszaal.Controllers;

[ApiController]
[Route("[controller]")]
public class BibliotheekController : ControllerBase
{

    // ... hier komen de endpoints uit de punten hieronder
}
```

</details>

> [!NOTE]
> In les 01 staat `api/[controller]`. Dan beginnen al je adressen met `/api/...`. Dat is ook goed, de cursus noemt het zelfs de beste gewoonte. De opgave vraagt adressen zonder `api`, dus hier staat `[controller]` alleen.

---

## 1. Welkomstbericht

**Nakijkpunten**

| Verzoek | Status | Antwoord |
|---|---|---|
| <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> /bibliotheek</code> | <code style="color:#37b24d;font-weight:600">200 OK</code> | <code style="color:#845ef7">Welkom in De Leeszaal. Wij zijn open van dinsdag tot zaterdag.</code> |

<details>
<summary><span style="color:#845ef7"><b>Toon de code</b></span></summary>

```csharp
[HttpGet]
public string GetWelkom()
{
    return "Welkom in De Leeszaal. Wij zijn open van dinsdag tot zaterdag.";
}
```

</details>

---

## 2. Informatie over een afdeling

**Nakijkpunten**

| Verzoek | Status | Antwoord |
|---|---|---|
| <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> /bibliotheek/afdeling/jeugd</code> | <code style="color:#37b24d;font-weight:600">200 OK</code> | <code style="color:#845ef7">De jeugdafdeling vind je op het gelijkvloers, achteraan links.</code> |
| <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> /bibliotheek/afdeling/strips</code> | <code style="color:#37b24d;font-weight:600">200 OK</code> | <code style="color:#845ef7">De stripafdeling vind je op de eerste verdieping.</code> |
| <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> /bibliotheek/afdeling/studie</code> | <code style="color:#37b24d;font-weight:600">200 OK</code> | <code style="color:#845ef7">De studiezaal vind je op de tweede verdieping en is stiltezone.</code> |
| <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> /bibliotheek/afdeling/kelder</code> | <code style="color:#37b24d;font-weight:600">200 OK</code> | <code style="color:#845ef7">Sorry, deze afdeling bestaat niet in onze bibliotheek.</code> |

**Aanpak.** Een `switch` op de afdeling, met `default` voor wat niet bestaat. Het kiezen staat
in `AfdelingsinfoVoor`, zodat de lenersfiche het kan hergebruiken.

<details>
<summary><span style="color:#845ef7"><b>Toon de code</b></span></summary>

```csharp
[HttpGet("afdeling/{afdeling}")]
public string GetAfdeling(string afdeling)
{
    return AfdelingsinfoVoor(afdeling);
}

private string AfdelingsinfoVoor(string afdeling)
{
    switch (afdeling)
    {
        case "jeugd":
            return "De jeugdafdeling vind je op het gelijkvloers, achteraan links.";
        case "strips":
            return "De stripafdeling vind je op de eerste verdieping.";
        case "studie":
            return "De studiezaal vind je op de tweede verdieping en is stiltezone.";
        default:
            return "Sorry, deze afdeling bestaat niet in onze bibliotheek.";
    }
}
```

</details>

> [!WARNING]
> **Veelgemaakte fouten**
>
> - `afdeling/{afdeling}` met een parameter die anders heet. Dan komt de parameter
>   niet uit de route, en antwoordt ASP.NET Core zelf met een 400.

---

## 3. Boete bij te late teruggave

**Nakijkpunten**

| Verzoek | Status | Antwoord |
|---|---|---|
| <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> /bibliotheek/boete/0</code> | <code style="color:#37b24d;font-weight:600">200 OK</code> | <code style="color:#845ef7">Je bent op tijd, er is geen boete.</code> |
| <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> /bibliotheek/boete/4</code> | <code style="color:#37b24d;font-weight:600">200 OK</code> | <code style="color:#845ef7">Je betaalt een boete van 1 euro.</code> |
| <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> /bibliotheek/boete/20</code> | <code style="color:#37b24d;font-weight:600">200 OK</code> | <code style="color:#845ef7">Je betaalt een boete van 5 euro en je lidkaart wordt geblokkeerd.</code> |
| <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> /bibliotheek/boete/7</code> | <code style="color:#37b24d;font-weight:600">200 OK</code> | <code style="color:#845ef7">Je betaalt een boete van 1 euro.</code> |
| <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> /bibliotheek/boete/8</code> | <code style="color:#37b24d;font-weight:600">200 OK</code> | <code style="color:#845ef7">Je betaalt een boete van 5 euro en je lidkaart wordt geblokkeerd.</code> |

**Aanpak.** Drie groepen, van klein naar groot. `dagen <= 0` vangt meteen een negatief
getal op, dat kan je API anders nooit beantwoorden.

<details>
<summary><span style="color:#845ef7"><b>Toon de code</b></span></summary>

```csharp
[HttpGet("boete/{dagen}")]
public string GetBoete(int dagen)
{
    return BoeteberichtVoor(dagen);
}

private string BoeteberichtVoor(int dagen)
{
    if (dagen <= 0)
    {
        return "Je bent op tijd, er is geen boete.";
    }
    else if (dagen <= 7)
    {
        return "Je betaalt een boete van 1 euro.";
    }
    else
    {
        return "Je betaalt een boete van 5 euro en je lidkaart wordt geblokkeerd.";
    }
}
```

</details>

> [!WARNING]
> **Veelgemaakte fouten**
>
> - `dagen < 7` voor de boete van 1 euro. De opgave zegt *1 tot en met 7*, dus 7 dagen
>   kost nog 1 euro.

---

## 4. Controlecode van een lidkaart

**Nakijkpunten**

| Verzoek | Status | Antwoord |
|---|---|---|
| <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> /bibliotheek/lidkaart/LZ-2026-4471</code> | <code style="color:#37b24d;font-weight:600">200 OK</code> | <code style="color:#845ef7">Lidkaart LZ-2026-4471 heeft controlecode 4471.</code> |

**Aanpak.** De laatste vier tekens begin je te knippen op positie `Length - 4`. `Substring`
met één getal knipt vanaf daar tot het einde (leerboek, hoofdstuk 1).

<details>
<summary><span style="color:#845ef7"><b>Toon de code</b></span></summary>

```csharp
[HttpGet("lidkaart/{nummer}")]
public string GetLidkaart(string nummer)
{
    return LidkaartberichtVoor(nummer);
}

private string LidkaartberichtVoor(string nummer)
{
    string controlecode = nummer.Substring(nummer.Length - 4);
    return $"Lidkaart {nummer} heeft controlecode {controlecode}.";
}
```

</details>

> [!WARNING]
> **Veelgemaakte fouten**
>
> - `Substring(8)`. Voor het voorbeeld klopt het, maar enkel omdat dat nummer
>   toevallig twaalf tekens telt. Een langer of korter nummer geeft een verkeerde
>   code. Zonder het te merken heb je de lengte van één nummer gehardcodeerd.

> [!TIP]
> **Zo kan het beter**
>
> - Een nummer korter dan vier tekens, zoals `/bibliotheek/lidkaart/AB1`, laat
>   `Substring` crashen: de client krijgt een 500. Kijk eerst of `nummer.Length`
>   minstens 4 is, en geef anders een duidelijk bericht. De opgave vraagt het niet,
>   maar een API die crasht op invoer van de gebruiker is nooit af.

---

## 5. Alles samen: de lenersfiche

**Nakijkpunten**

| Verzoek | Status | Antwoord |
|---|---|---|
| <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> /bibliotheek/lener/LZ-2026-8820</code> | <code style="color:#37b24d;font-weight:600">200 OK</code> | <code style="color:#845ef7">Lidkaart LZ-2026-8820 heeft controlecode 8820. De stripafdeling vind je op de eerste verdieping. Je betaalt een boete van 1 euro.</code> |
| <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> /bibliotheek/lener/LZ-2026-4471</code> | <code style="color:#37b24d;font-weight:600">200 OK</code> | <code style="color:#845ef7">Lidkaart LZ-2026-4471 heeft controlecode 4471. De jeugdafdeling vind je op het gelijkvloers, achteraan links. Je bent op tijd, er is geen boete.</code> |
| <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> /bibliotheek/lener/LZ-2025-1093</code> | <code style="color:#37b24d;font-weight:600">200 OK</code> | <code style="color:#845ef7">Lidkaart LZ-2025-1093 heeft controlecode 1093. De studiezaal vind je op de tweede verdieping en is stiltezone. Je betaalt een boete van 5 euro en je lidkaart wordt geblokkeerd.</code> |
| <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> /bibliotheek/lener/LZ-2026-0000</code> | <code style="color:#37b24d;font-weight:600">200 OK</code> | <code style="color:#845ef7">We kennen dit lidkaartnummer niet.</code> |

**Aanpak.** Zoek eerst de lener op: een `switch` op het nummer vult de afdeling en het aantal
dagen, en een onbekend nummer stopt meteen. Daarna bouw je het antwoord met de drie
hulpmethodes. Het nummer zelf gaat rechtstreeks door naar `LidkaartberichtVoor`.

<details>
<summary><span style="color:#845ef7"><b>Toon de code</b></span></summary>

```csharp
[HttpGet("lener/{nummer}")]
public string GetLener(string nummer)
{
    string afdeling;
    int dagenTeLaat;

    switch (nummer)
    {
        case "LZ-2026-4471":
            afdeling = "jeugd";
            dagenTeLaat = 0;
            break;
        case "LZ-2026-8820":
            afdeling = "strips";
            dagenTeLaat = 4;
            break;
        case "LZ-2025-1093":
            afdeling = "studie";
            dagenTeLaat = 19;
            break;
        default:
            return "We kennen dit lidkaartnummer niet.";
    }

    return $"{LidkaartberichtVoor(nummer)} {AfdelingsinfoVoor(afdeling)} {BoeteberichtVoor(dagenTeLaat)}";
}
```

</details>

> [!WARNING]
> **Veelgemaakte fouten**
>
> - De controlecode opnieuw uitrekenen in de fiche, in plaats van de methode uit punt
>   4 te gebruiken.

---

## Alles samen: de volledige bestanden

Hetzelfde als hierboven, maar in één stuk. Handig om naast je eigen bestanden te leggen.

<details>
<summary><span style="color:#845ef7"><b>Toon BibliotheekController.cs</b></span></summary>

```csharp
using Microsoft.AspNetCore.Mvc;

namespace DeLeeszaal.Controllers;

[ApiController]
[Route("[controller]")]
public class BibliotheekController : ControllerBase
{
    [HttpGet]
    public string GetWelkom()
    {
        return "Welkom in De Leeszaal. Wij zijn open van dinsdag tot zaterdag.";
    }

    [HttpGet("afdeling/{afdeling}")]
    public string GetAfdeling(string afdeling)
    {
        return AfdelingsinfoVoor(afdeling);
    }

    private string AfdelingsinfoVoor(string afdeling)
    {
        switch (afdeling)
        {
            case "jeugd":
                return "De jeugdafdeling vind je op het gelijkvloers, achteraan links.";
            case "strips":
                return "De stripafdeling vind je op de eerste verdieping.";
            case "studie":
                return "De studiezaal vind je op de tweede verdieping en is stiltezone.";
            default:
                return "Sorry, deze afdeling bestaat niet in onze bibliotheek.";
        }
    }

    [HttpGet("boete/{dagen}")]
    public string GetBoete(int dagen)
    {
        return BoeteberichtVoor(dagen);
    }

    private string BoeteberichtVoor(int dagen)
    {
        if (dagen <= 0)
        {
            return "Je bent op tijd, er is geen boete.";
        }
        else if (dagen <= 7)
        {
            return "Je betaalt een boete van 1 euro.";
        }
        else
        {
            return "Je betaalt een boete van 5 euro en je lidkaart wordt geblokkeerd.";
        }
    }

    [HttpGet("lidkaart/{nummer}")]
    public string GetLidkaart(string nummer)
    {
        return LidkaartberichtVoor(nummer);
    }

    private string LidkaartberichtVoor(string nummer)
    {
        string controlecode = nummer.Substring(nummer.Length - 4);
        return $"Lidkaart {nummer} heeft controlecode {controlecode}.";
    }

    [HttpGet("lener/{nummer}")]
    public string GetLener(string nummer)
    {
        string afdeling;
        int dagenTeLaat;

        switch (nummer)
        {
            case "LZ-2026-4471":
                afdeling = "jeugd";
                dagenTeLaat = 0;
                break;
            case "LZ-2026-8820":
                afdeling = "strips";
                dagenTeLaat = 4;
                break;
            case "LZ-2025-1093":
                afdeling = "studie";
                dagenTeLaat = 19;
                break;
            default:
                return "We kennen dit lidkaartnummer niet.";
        }

        return $"{LidkaartberichtVoor(nummer)} {AfdelingsinfoVoor(afdeling)} {BoeteberichtVoor(dagenTeLaat)}";
    }
}
```

</details>
