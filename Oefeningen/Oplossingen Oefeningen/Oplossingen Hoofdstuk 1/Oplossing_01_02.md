# Oplossing 01_02

<span style="background:#c2255c;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>OPLOSSING</b></span> &nbsp;·&nbsp; <b>Hoofdstuk 01</b> &nbsp;·&nbsp; Controller en routeparameters &nbsp;·&nbsp; PC-onderdelen &nbsp;·&nbsp; bij `Oefening_01_02.md`

> [!IMPORTANT]
> Eerst zelf. Kijk per punt eerst naar de **nakijkpunten**. Haalt jouw API ze
> allemaal, dan is je punt juist, ook als je code er anders uitziet. Open de code
> pas daarna, om te vergelijken.

---

## De controller zelf

**Nakijkpunten**

- De klasse heet `OnderdelenController` en erft van `ControllerBase`.
- Boven de klasse staan `[ApiController]` en `[Route("[controller]")]`. ASP.NET Core
  vervangt `[controller]` door de klassenaam zonder *Controller*, dus alle adressen
  beginnen met `/onderdelen`. Hoofdletters in de URL maken voor de route niets uit.

<details>
<summary><span style="color:#845ef7"><b>Toon de code</b></span></summary>

```csharp
using Microsoft.AspNetCore.Mvc;

namespace BitEnByte.Controllers;

[ApiController]
[Route("[controller]")]
public class OnderdelenController : ControllerBase
{

    // ... hier komen de endpoints uit de punten hieronder
}
```

</details>

> [!NOTE]
> In les 01 staat `api/[controller]`. Dan beginnen al je adressen met `/api/...`. Dat is ook goed, de cursus noemt het zelfs de beste gewoonte. De opgave vraagt adressen zonder `api`, dus hier staat `[controller]` alleen.

---

## 1. Openingsbericht

**Nakijkpunten**

| Verzoek | Status | Antwoord |
|---|---|---|
| <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> /onderdelen</code> | <code style="color:#37b24d;font-weight:600">200 OK</code> | <code style="color:#845ef7">Welkom bij de onderdelenbalie van Bit &amp; Byte.</code> |

**Aanpak.** `[HttpGet]` zonder tekst tussen haakjes antwoordt op de basisroute van de
klasse. Een methode die een `string` teruggeeft, antwoordt altijd met
<code style="color:#37b24d;font-weight:600">200 OK</code> en die tekst als body.

<details>
<summary><span style="color:#845ef7"><b>Toon de code</b></span></summary>

```csharp
[HttpGet]
public string GetOpeningsbericht()
{
    return "Welkom bij de onderdelenbalie van Bit & Byte.";
}
```

</details>

> [!WARNING]
> **Veelgemaakte fouten**
>
> - `[HttpGet("onderdelen")]` boven de methode. De route van de klasse en die van de
>   methode worden aan elkaar geplakt, dus dan luistert ze op
>   `/onderdelen/onderdelen`.

---

## 2. Uitleg over een soort onderdeel

**Nakijkpunten**

| Verzoek | Status | Antwoord |
|---|---|---|
| <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> /onderdelen/soort/moederbord</code> | <code style="color:#37b24d;font-weight:600">200 OK</code> | <code style="color:#845ef7">Het moederbord verbindt alle onderdelen met elkaar.</code> |
| <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> /onderdelen/soort/processor</code> | <code style="color:#37b24d;font-weight:600">200 OK</code> | <code style="color:#845ef7">De processor voert alle berekeningen uit.</code> |
| <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> /onderdelen/soort/videokaart</code> | <code style="color:#37b24d;font-weight:600">200 OK</code> | <code style="color:#845ef7">De videokaart tekent het beeld voor je scherm.</code> |
| <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> /onderdelen/soort/voeding</code> | <code style="color:#37b24d;font-weight:600">200 OK</code> | <code style="color:#845ef7">Sorry, dit soort onderdeel verkopen wij niet.</code> |

**Aanpak.** `{soort}` in de route vult de parameter `string soort`. Een `switch` kiest het
bericht, en `default` vangt alles op wat niet herkend wordt. Het kiezen staat in
een aparte, private methode `UitlegVoor`: het endpoint geeft enkel door. Zo kan punt 5
dezelfde methode oproepen, zonder één bericht opnieuw te typen. De tekst voor een
onbekende soort staat in een `const` (leerboek, hoofdstuk 24), omdat punt 5 hem ook
nodig heeft.

<details>
<summary><span style="color:#845ef7"><b>Toon de code</b></span></summary>

```csharp
private const string OnbekendeSoort = "Sorry, dit soort onderdeel verkopen wij niet.";

[HttpGet("soort/{soort}")]
public string GetUitleg(string soort)
{
    return UitlegVoor(soort);
}

private string UitlegVoor(string soort)
{
    switch (soort)
    {
        case "moederbord":
            return "Het moederbord verbindt alle onderdelen met elkaar.";
        case "processor":
            return "De processor voert alle berekeningen uit.";
        case "videokaart":
            return "De videokaart tekent het beeld voor je scherm.";
        default:
            return OnbekendeSoort;
    }
}
```

</details>

> [!WARNING]
> **Veelgemaakte fouten**
>
> - De naam tussen accolades verschilt van de parameter, bijvoorbeeld `{soort}` met
>   `string type`. Dan komt `type` niet uit de route. ASP.NET Core zoekt hem in de
>   querystring, vindt hem niet, en antwoordt zelf met een 400, nog voor je methode
>   draait.
> - Een `if` zonder `else` voor de onbekende soort: dan compileert de methode niet,
>   want niet elk pad geeft iets terug.

> [!TIP]
> **Zo kan het beter**
>
> - Met `soort.ToLower()` in de `switch` werkt ook `Processor` of `PROCESSOR`. De
>   opgave vraagt het hier niet, in 01_03 wel.
> - Een `switch`-expressie (leerboek, hoofdstuk 4) maakt `UitlegVoor` korter: één
>   regel per soort, met `_` voor de rest.

---

## 3. Voorraadstatus

**Nakijkpunten**

| Verzoek | Status | Antwoord |
|---|---|---|
| <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> /onderdelen/voorraad/0</code> | <code style="color:#37b24d;font-weight:600">200 OK</code> | <code style="color:#845ef7">Niet op voorraad, bestel bij de leverancier.</code> |
| <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> /onderdelen/voorraad/3</code> | <code style="color:#37b24d;font-weight:600">200 OK</code> | <code style="color:#845ef7">Beperkt op voorraad, hou dit in het oog.</code> |
| <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> /onderdelen/voorraad/12</code> | <code style="color:#37b24d;font-weight:600">200 OK</code> | <code style="color:#845ef7">Ruim op voorraad.</code> |
| <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> /onderdelen/voorraad/1</code> | <code style="color:#37b24d;font-weight:600">200 OK</code> | <code style="color:#845ef7">Beperkt op voorraad, hou dit in het oog.</code> |
| <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> /onderdelen/voorraad/4</code> | <code style="color:#37b24d;font-weight:600">200 OK</code> | <code style="color:#845ef7">Beperkt op voorraad, hou dit in het oog.</code> |
| <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> /onderdelen/voorraad/5</code> | <code style="color:#37b24d;font-weight:600">200 OK</code> | <code style="color:#845ef7">Ruim op voorraad.</code> |
| <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> /onderdelen/voorraad/abc</code> | <code style="color:#f08c00;font-weight:600">400 Bad Request</code> | foutmelding van ASP.NET Core zelf |

**Aanpak.** Omdat de parameter een `int` is, zet ASP.NET Core de tekst uit de URL zelf om
naar een getal. Daarna loop je de grenzen af van klein naar groot: eerst `<= 0`, dan
`<= 4`, de rest is ruim. Door `<= 0` in plaats van `== 0` te schrijven, vang je meteen
een negatief getal op.

<details>
<summary><span style="color:#845ef7"><b>Toon de code</b></span></summary>

```csharp
[HttpGet("voorraad/{aantal}")]
public string GetVoorraad(int aantal)
{
    return VoorraadstatusVoor(aantal);
}

private string VoorraadstatusVoor(int aantal)
{
    if (aantal <= 0)
    {
        return "Niet op voorraad, bestel bij de leverancier.";
    }
    else if (aantal <= 4)
    {
        return "Beperkt op voorraad, hou dit in het oog.";
    }
    else
    {
        return "Ruim op voorraad.";
    }
}
```

</details>

> [!WARNING]
> **Veelgemaakte fouten**
>
> - `aantal < 4` in plaats van `aantal <= 4`. Dan is 4 stuks al *ruim*, terwijl de
>   opgave *1 tot en met 4* zegt.
> - De controles in de verkeerde volgorde. Begin je met `aantal > 0`, dan krijgt alles
>   vanaf 1 stuk hetzelfde antwoord en kom je nooit bij *ruim*.

> [!TIP]
> **Zo kan het beter**
>
> - Met `{aantal:int}` in de route geeft `/onderdelen/voorraad/abc` een 404 in plaats
>   van een 400 (leerboek, hoofdstuk 25). Allebei zijn verdedigbaar; het verschil
>   kennen is wat telt.

---

## 4. Artikelcode nakijken

**Nakijkpunten**

| Verzoek | Status | Antwoord |
|---|---|---|
| <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> /onderdelen/code/mb-b650-01</code> | <code style="color:#37b24d;font-weight:600">200 OK</code> | <code style="color:#845ef7">Artikelcode MB-B650-01 telt 10 tekens.</code> |

**Aanpak.** `ToUpper()` geeft een **nieuwe** tekst terug in hoofdletters, en `Length` telt
alle tekens, koppeltekens inbegrepen. Met een <code>&#36;</code> voor de tekst zet je beide
rechtstreeks in het bericht.

<details>
<summary><span style="color:#845ef7"><b>Toon de code</b></span></summary>

```csharp
[HttpGet("code/{artikelcode}")]
public string GetCode(string artikelcode)
{
    return CodeberichtVoor(artikelcode);
}

private string CodeberichtVoor(string artikelcode)
{
    return $"Artikelcode {artikelcode.ToUpper()} telt {artikelcode.Length} tekens.";
}
```

</details>

> [!WARNING]
> **Veelgemaakte fouten**
>
> - `artikelcode.ToUpper();` op een eigen regel, en daarna `artikelcode` gebruiken.
>   Een tekst verandert nooit zelf: zonder het resultaat te bewaren blijft de code in
>   kleine letters (leerboek, hoofdstuk 1).

---

## 5. Alles samen: de onderdelenfiche

**Nakijkpunten**

| Verzoek | Status | Antwoord |
|---|---|---|
| <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> /onderdelen/fiche/processor</code> | <code style="color:#37b24d;font-weight:600">200 OK</code> | <code style="color:#845ef7">De processor voert alle berekeningen uit. Beperkt op voorraad, hou dit in het oog. Artikelcode CPU-7600X-01 telt 12 tekens.</code> |
| <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> /onderdelen/fiche/videokaart</code> | <code style="color:#37b24d;font-weight:600">200 OK</code> | <code style="color:#845ef7">De videokaart tekent het beeld voor je scherm. Niet op voorraad, bestel bij de leverancier. Artikelcode GPU-4070-01 telt 11 tekens.</code> |
| <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> /onderdelen/fiche/moederbord</code> | <code style="color:#37b24d;font-weight:600">200 OK</code> | <code style="color:#845ef7">Het moederbord verbindt alle onderdelen met elkaar. Ruim op voorraad. Artikelcode MB-B650-01 telt 10 tekens.</code> |
| <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> /onderdelen/fiche/voeding</code> | <code style="color:#37b24d;font-weight:600">200 OK</code> | <code style="color:#845ef7">Sorry, dit soort onderdeel verkopen wij niet.</code> |

**Aanpak.** Twee stappen. Eerst zoek je de gegevens van de soort op: een `switch` vult twee
lokale variabelen, en bij een onbekende soort stop je meteen met het sorry-bericht.
Daarna bouw je het antwoord met de drie hulpmethodes uit punt 2, 3 en 4. Geen enkel
bericht staat twee keer in je code.

<details>
<summary><span style="color:#845ef7"><b>Toon de code</b></span></summary>

```csharp
[HttpGet("fiche/{soort}")]
public string GetFiche(string soort)
{
    string artikelcode;
    int voorraad;

    switch (soort)
    {
        case "moederbord":
            artikelcode = "MB-B650-01";
            voorraad = 12;
            break;
        case "processor":
            artikelcode = "CPU-7600X-01";
            voorraad = 3;
            break;
        case "videokaart":
            artikelcode = "GPU-4070-01";
            voorraad = 0;
            break;
        default:
            return OnbekendeSoort;
    }

    return $"{UitlegVoor(soort)} {VoorraadstatusVoor(voorraad)} {CodeberichtVoor(artikelcode)}";
}
```

</details>

> [!WARNING]
> **Veelgemaakte fouten**
>
> - De negen berichten per soort opnieuw uittypen. Het werkt, maar verandert er één
>   tekst, dan moet je hem op meerdere plaatsen aanpassen.
> - De spatie tussen de drie berichten vergeten. Dan plakken de zinnen aan elkaar.

> [!TIP]
> **Zo kan het beter**
>
> - De `switch` met gegevens per soort is een tijdelijke oplossing. In hoofdstuk 02
>   komt er een klasse met een lijst in de plaats, en dat is precies wat 02_01 doet.

---

## Alles samen: de volledige bestanden

Hetzelfde als hierboven, maar in één stuk. Handig om naast je eigen bestanden te leggen.

<details>
<summary><span style="color:#845ef7"><b>Toon OnderdelenController.cs</b></span></summary>

```csharp
using Microsoft.AspNetCore.Mvc;

namespace BitEnByte.Controllers;

[ApiController]
[Route("[controller]")]
public class OnderdelenController : ControllerBase
{
    [HttpGet]
    public string GetOpeningsbericht()
    {
        return "Welkom bij de onderdelenbalie van Bit & Byte.";
    }

    private const string OnbekendeSoort = "Sorry, dit soort onderdeel verkopen wij niet.";

    [HttpGet("soort/{soort}")]
    public string GetUitleg(string soort)
    {
        return UitlegVoor(soort);
    }

    private string UitlegVoor(string soort)
    {
        switch (soort)
        {
            case "moederbord":
                return "Het moederbord verbindt alle onderdelen met elkaar.";
            case "processor":
                return "De processor voert alle berekeningen uit.";
            case "videokaart":
                return "De videokaart tekent het beeld voor je scherm.";
            default:
                return OnbekendeSoort;
        }
    }

    [HttpGet("voorraad/{aantal}")]
    public string GetVoorraad(int aantal)
    {
        return VoorraadstatusVoor(aantal);
    }

    private string VoorraadstatusVoor(int aantal)
    {
        if (aantal <= 0)
        {
            return "Niet op voorraad, bestel bij de leverancier.";
        }
        else if (aantal <= 4)
        {
            return "Beperkt op voorraad, hou dit in het oog.";
        }
        else
        {
            return "Ruim op voorraad.";
        }
    }

    [HttpGet("code/{artikelcode}")]
    public string GetCode(string artikelcode)
    {
        return CodeberichtVoor(artikelcode);
    }

    private string CodeberichtVoor(string artikelcode)
    {
        return $"Artikelcode {artikelcode.ToUpper()} telt {artikelcode.Length} tekens.";
    }

    [HttpGet("fiche/{soort}")]
    public string GetFiche(string soort)
    {
        string artikelcode;
        int voorraad;

        switch (soort)
        {
            case "moederbord":
                artikelcode = "MB-B650-01";
                voorraad = 12;
                break;
            case "processor":
                artikelcode = "CPU-7600X-01";
                voorraad = 3;
                break;
            case "videokaart":
                artikelcode = "GPU-4070-01";
                voorraad = 0;
                break;
            default:
                return OnbekendeSoort;
        }

        return $"{UitlegVoor(soort)} {VoorraadstatusVoor(voorraad)} {CodeberichtVoor(artikelcode)}";
    }
}
```

</details>
