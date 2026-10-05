# Oplossing 01_03

<span style="background:#c2255c;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>OPLOSSING</b></span> &nbsp;·&nbsp; <b>Hoofdstuk 01</b> &nbsp;·&nbsp; Twee routeparameters in één route &nbsp;·&nbsp; PC-onderdelen &nbsp;·&nbsp; bij `Oefening_01_03.md`

> [!IMPORTANT]
> Eerst zelf. Kijk per punt eerst naar de **nakijkpunten**. Haalt jouw API ze
> allemaal, dan is je punt juist, ook als je code er anders uitziet. Open de code
> pas daarna, om te vergelijken.

---

## De controller zelf

**Nakijkpunten**

- `CompatibiliteitController`, met `[Route("[controller]")]`: alle adressen beginnen met `/compatibiliteit`.

<details>
<summary><span style="color:#845ef7"><b>Toon de code</b></span></summary>

```csharp
using Microsoft.AspNetCore.Mvc;

namespace BitEnByte.Controllers;

[ApiController]
[Route("[controller]")]
public class CompatibiliteitController : ControllerBase
{

    // ... hier komen de endpoints uit de punten hieronder
}
```

</details>

---

## 1. Algemeen bericht

**Nakijkpunten**

| Verzoek | Status | Antwoord |
|---|---|---|
| <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> /compatibiliteit</code> | <code style="color:#37b24d;font-weight:600">200 OK</code> | <code style="color:#845ef7">Compatibiliteitsbalie Bit &amp; Byte. Geef twee onderdelen op om ze te vergelijken.</code> |

<details>
<summary><span style="color:#845ef7"><b>Toon de code</b></span></summary>

```csharp
[HttpGet]
public string GetBericht()
{
    return "Compatibiliteitsbalie Bit & Byte. Geef twee onderdelen op om ze te vergelijken.";
}
```

</details>

---

## 2. Twee sockets vergelijken

**Nakijkpunten**

| Verzoek | Status | Antwoord |
|---|---|---|
| <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> /compatibiliteit/socket/AM5/am5</code> | <code style="color:#37b24d;font-weight:600">200 OK</code> | <code style="color:#845ef7">De processor past op dit moederbord.</code> |
| <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> /compatibiliteit/socket/AM5/LGA1700</code> | <code style="color:#37b24d;font-weight:600">200 OK</code> | <code style="color:#845ef7">De processor past niet: AM5 tegenover LGA1700.</code> |
| <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> /compatibiliteit/socket/am5/AM5</code> | <code style="color:#37b24d;font-weight:600">200 OK</code> | <code style="color:#845ef7">De processor past op dit moederbord.</code> |

**Aanpak.** Twee namen tussen accolades in één route vullen twee parameters, op naam, niet
op volgorde. `Equals` met `StringComparison.OrdinalIgnoreCase` vergelijkt zonder op
hoofdletters te letten (leerboek, hoofdstuk 1). In het bericht bij een verschil gebruik
je de waarden zoals de gebruiker ze typte.

<details>
<summary><span style="color:#845ef7"><b>Toon de code</b></span></summary>

```csharp
[HttpGet("socket/{socketProcessor}/{socketMoederbord}")]
public string GetSocket(string socketProcessor, string socketMoederbord)
{
    return SocketoordeelVoor(socketProcessor, socketMoederbord);
}

private string SocketoordeelVoor(string socketProcessor, string socketMoederbord)
{
    if (socketProcessor.Equals(socketMoederbord, StringComparison.OrdinalIgnoreCase))
    {
        return "De processor past op dit moederbord.";
    }

    return $"De processor past niet: {socketProcessor} tegenover {socketMoederbord}.";
}
```

</details>

> [!WARNING]
> **Veelgemaakte fouten**
>
> - `socketProcessor == socketMoederbord`: dan zijn `AM5` en `am5` verschillend.
> - Eerst `ToUpper()` op beide, en die versie in het foutbericht zetten. Dan toont je
>   API `AM5 tegenover LGA1700` ook als de gebruiker kleine letters typte. Niet fout,
>   maar niet wat er gevraagd is.

---

## 3. Voeding nakijken

**Nakijkpunten**

| Verzoek | Status | Antwoord |
|---|---|---|
| <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> /compatibiliteit/voeding/550/620</code> | <code style="color:#37b24d;font-weight:600">200 OK</code> | <code style="color:#845ef7">De voeding is te zwak voor deze build.</code> |
| <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> /compatibiliteit/voeding/650/620</code> | <code style="color:#37b24d;font-weight:600">200 OK</code> | <code style="color:#845ef7">De voeding volstaat, maar de marge is krap.</code> |
| <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> /compatibiliteit/voeding/850/620</code> | <code style="color:#37b24d;font-weight:600">200 OK</code> | <code style="color:#845ef7">De voeding is ruim voldoende.</code> |
| <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> /compatibiliteit/voeding/620/620</code> | <code style="color:#37b24d;font-weight:600">200 OK</code> | <code style="color:#845ef7">De voeding volstaat, maar de marge is krap.</code> |
| <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> /compatibiliteit/voeding/719/620</code> | <code style="color:#37b24d;font-weight:600">200 OK</code> | <code style="color:#845ef7">De voeding volstaat, maar de marge is krap.</code> |
| <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> /compatibiliteit/voeding/720/620</code> | <code style="color:#37b24d;font-weight:600">200 OK</code> | <code style="color:#845ef7">De voeding is ruim voldoende.</code> |

**Aanpak.** Reken de marge één keer uit, `wattage - verbruik`, en beslis daarna op dat ene
getal: negatief is te zwak, onder de 100 is krap, de rest is ruim. Zo lees je de
regels uit de opgave letterlijk terug in je code.

<details>
<summary><span style="color:#845ef7"><b>Toon de code</b></span></summary>

```csharp
[HttpGet("voeding/{wattage}/{verbruik}")]
public string GetVoeding(int wattage, int verbruik)
{
    return VoedingsadviesVoor(wattage, verbruik);
}

private string VoedingsadviesVoor(int wattage, int verbruik)
{
    int marge = wattage - verbruik;

    if (marge < 0)
    {
        return "De voeding is te zwak voor deze build.";
    }
    else if (marge < 100)
    {
        return "De voeding volstaat, maar de marge is krap.";
    }
    else
    {
        return "De voeding is ruim voldoende.";
    }
}
```

</details>

> [!WARNING]
> **Veelgemaakte fouten**
>
> - `marge <= 100` in plaats van `marge < 100`. Bij precies 100 watt marge zegt de
>   opgave *ruim voldoende*.
> - Evenveel wattage als verbruik als *te zwak* zien. De opgave zegt *lager dan het
>   verbruik*, dus bij gelijk is het *krap*.

---

## 4. Vrije geheugensloten

**Nakijkpunten**

| Verzoek | Status | Antwoord |
|---|---|---|
| <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> /compatibiliteit/sloten/2/4</code> | <code style="color:#37b24d;font-weight:600">200 OK</code> | <code style="color:#845ef7">Er passen maar 2 modules in dit moederbord.</code> |
| <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> /compatibiliteit/sloten/4/4</code> | <code style="color:#37b24d;font-weight:600">200 OK</code> | <code style="color:#845ef7">Alle sloten worden gebruikt.</code> |
| <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> /compatibiliteit/sloten/4/2</code> | <code style="color:#37b24d;font-weight:600">200 OK</code> | <code style="color:#845ef7">Er blijven nog 2 sloten vrij.</code> |

**Aanpak.** Drie gevallen, drie takken: meer, evenveel, minder. Het aantal vrije sloten is
een berekening die enkel in de laatste tak nodig is, dus daar staat ze ook.

<details>
<summary><span style="color:#845ef7"><b>Toon de code</b></span></summary>

```csharp
[HttpGet("sloten/{aantalSloten}/{aantalModules}")]
public string GetSloten(int aantalSloten, int aantalModules)
{
    return SlotenberichtVoor(aantalSloten, aantalModules);
}

private string SlotenberichtVoor(int aantalSloten, int aantalModules)
{
    if (aantalModules > aantalSloten)
    {
        return $"Er passen maar {aantalSloten} modules in dit moederbord.";
    }
    else if (aantalModules == aantalSloten)
    {
        return "Alle sloten worden gebruikt.";
    }
    else
    {
        int vrij = aantalSloten - aantalModules;
        return $"Er blijven nog {vrij} sloten vrij.";
    }
}
```

</details>

> [!TIP]
> **Zo kan het beter**
>
> - Bij één vrij slot zegt je API *1 sloten*. Wil je dat netjes, dan kies je met een
>   korte `if` tussen *slot* en *sloten*. De opgave vraagt het niet.

---

## 5. Alles samen: het bouwrapport

**Nakijkpunten**

| Verzoek | Status | Antwoord |
|---|---|---|
| <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> /compatibiliteit/build/gaming/AM5/600/4</code> | <code style="color:#37b24d;font-weight:600">200 OK</code> | <code style="color:#845ef7">De processor past op dit moederbord. De voeding volstaat, maar de marge is krap. Alle sloten worden gebruikt.</code> |
| <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> /compatibiliteit/build/kantoor/LGA1700/450/4</code> | <code style="color:#37b24d;font-weight:600">200 OK</code> | <code style="color:#845ef7">De processor past niet: LGA1700 tegenover AM5. De voeding is ruim voldoende. Er passen maar 2 modules in dit moederbord.</code> |
| <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> /compatibiliteit/build/montage/AM5/600/4</code> | <code style="color:#37b24d;font-weight:600">200 OK</code> | <code style="color:#845ef7">De processor past niet: AM5 tegenover LGA1700. De voeding is te zwak voor deze build. Alle sloten worden gebruikt.</code> |
| <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> /compatibiliteit/build/server/AM5/600/4</code> | <code style="color:#37b24d;font-weight:600">200 OK</code> | <code style="color:#845ef7">We kennen deze build niet.</code> |

**Aanpak.** Eerst de gegevens van de build opzoeken, met een vroege `return` voor een build
die niet bestaat. Daarna roep je de drie hulpmethodes alle drie op, en plak je hun
antwoorden aan elkaar. Let op de volgorde van de sockets: de processor komt uit de
URL, het moederbord uit de tabel, en ze staan in dezelfde volgorde als in punt 2.

<details>
<summary><span style="color:#845ef7"><b>Toon de code</b></span></summary>

```csharp
[HttpGet("build/{build}/{socketProcessor}/{wattage}/{aantalModules}")]
public string GetBuild(string build, string socketProcessor, int wattage, int aantalModules)
{
    string socketMoederbord;
    int aantalSloten;
    int verbruik;

    switch (build)
    {
        case "kantoor":
            socketMoederbord = "AM5";
            aantalSloten = 2;
            verbruik = 180;
            break;
        case "gaming":
            socketMoederbord = "AM5";
            aantalSloten = 4;
            verbruik = 520;
            break;
        case "montage":
            socketMoederbord = "LGA1700";
            aantalSloten = 4;
            verbruik = 610;
            break;
        default:
            return "We kennen deze build niet.";
    }

    string socket = SocketoordeelVoor(socketProcessor, socketMoederbord);
    string voeding = VoedingsadviesVoor(wattage, verbruik);
    string sloten = SlotenberichtVoor(aantalSloten, aantalModules);

    return $"{socket} {voeding} {sloten}";
}
```

</details>

> [!WARNING]
> **Veelgemaakte fouten**
>
> - Stoppen na de eerste fout, met een `return` zodra de socket niet past. De opgave
>   zegt uitdrukkelijk dat de drie oordelen elkaar niet mogen beïnvloeden.
> - De twee sockets omgewisseld doorgeven. Dan zegt je API *AM5 tegenover LGA1700*
>   waar *LGA1700 tegenover AM5* hoort.

---

## Alles samen: de volledige bestanden

Hetzelfde als hierboven, maar in één stuk. Handig om naast je eigen bestanden te leggen.

<details>
<summary><span style="color:#845ef7"><b>Toon CompatibiliteitController.cs</b></span></summary>

```csharp
using Microsoft.AspNetCore.Mvc;

namespace BitEnByte.Controllers;

[ApiController]
[Route("[controller]")]
public class CompatibiliteitController : ControllerBase
{
    [HttpGet]
    public string GetBericht()
    {
        return "Compatibiliteitsbalie Bit & Byte. Geef twee onderdelen op om ze te vergelijken.";
    }

    [HttpGet("socket/{socketProcessor}/{socketMoederbord}")]
    public string GetSocket(string socketProcessor, string socketMoederbord)
    {
        return SocketoordeelVoor(socketProcessor, socketMoederbord);
    }

    private string SocketoordeelVoor(string socketProcessor, string socketMoederbord)
    {
        if (socketProcessor.Equals(socketMoederbord, StringComparison.OrdinalIgnoreCase))
        {
            return "De processor past op dit moederbord.";
        }

        return $"De processor past niet: {socketProcessor} tegenover {socketMoederbord}.";
    }

    [HttpGet("voeding/{wattage}/{verbruik}")]
    public string GetVoeding(int wattage, int verbruik)
    {
        return VoedingsadviesVoor(wattage, verbruik);
    }

    private string VoedingsadviesVoor(int wattage, int verbruik)
    {
        int marge = wattage - verbruik;

        if (marge < 0)
        {
            return "De voeding is te zwak voor deze build.";
        }
        else if (marge < 100)
        {
            return "De voeding volstaat, maar de marge is krap.";
        }
        else
        {
            return "De voeding is ruim voldoende.";
        }
    }

    [HttpGet("sloten/{aantalSloten}/{aantalModules}")]
    public string GetSloten(int aantalSloten, int aantalModules)
    {
        return SlotenberichtVoor(aantalSloten, aantalModules);
    }

    private string SlotenberichtVoor(int aantalSloten, int aantalModules)
    {
        if (aantalModules > aantalSloten)
        {
            return $"Er passen maar {aantalSloten} modules in dit moederbord.";
        }
        else if (aantalModules == aantalSloten)
        {
            return "Alle sloten worden gebruikt.";
        }
        else
        {
            int vrij = aantalSloten - aantalModules;
            return $"Er blijven nog {vrij} sloten vrij.";
        }
    }

    [HttpGet("build/{build}/{socketProcessor}/{wattage}/{aantalModules}")]
    public string GetBuild(string build, string socketProcessor, int wattage, int aantalModules)
    {
        string socketMoederbord;
        int aantalSloten;
        int verbruik;

        switch (build)
        {
            case "kantoor":
                socketMoederbord = "AM5";
                aantalSloten = 2;
                verbruik = 180;
                break;
            case "gaming":
                socketMoederbord = "AM5";
                aantalSloten = 4;
                verbruik = 520;
                break;
            case "montage":
                socketMoederbord = "LGA1700";
                aantalSloten = 4;
                verbruik = 610;
                break;
            default:
                return "We kennen deze build niet.";
        }

        string socket = SocketoordeelVoor(socketProcessor, socketMoederbord);
        string voeding = VoedingsadviesVoor(wattage, verbruik);
        string sloten = SlotenberichtVoor(aantalSloten, aantalModules);

        return $"{socket} {voeding} {sloten}";
    }
}
```

</details>
