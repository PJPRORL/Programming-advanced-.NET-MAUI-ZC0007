# C# — leerboek

<b>Programming Advanced</b> &nbsp;·&nbsp; van tekst tot interface &nbsp;·&nbsp; 21 hoofdstukken &nbsp;·&nbsp; met voorspelvragen om jezelf te testen

## Hoe je dit gebruikt

Dit leerboek brengt twee dingen samen die je nodig hebt om vlot in C# te
programmeren: de **bouwstenen** — wat een `List`, een `Dictionary` of een `record`
ís — en de **methodes** — wat je ermee kan dóen. Ze staan in één volgorde waarin
elk hoofdstuk enkel steunt op wat ervoor kwam. Werk je het van voor naar achter
door, dan kom je nooit iets tegen dat nog niet uitgelegd is.

Er zijn twee soorten hoofdstukken.

- Een **bouwsteen** volgt altijd dezelfde stappen: wat het is, hoe het eruitziet,
  wat je ermee kan, wanneer wel en wanneer niet, en de veelgemaakte fouten.
- Een **methodehoofdstuk** is geordend op wat je wil doen: *ik wil een stuk uit een
  tekst halen*, *ik wil één item uit een lijst*. Per vraag staan de methodes die
  ervoor bestaan, en welke je in welk geval neemt.

Elk hoofdstuk eindigt met **Voorspel de uitkomst**. Lees de code, beslis wat er op
het scherm komt, en klik pas dan op het antwoord. Uitleg lezen voelt als
begrijpen, maar pas als je een uitkomst juist voorspelt, weet je of het zit. Heb
je er één fout, lees dan de uitleg bij het antwoord en probeer de code zelf uit
in een lege consoletoepassing.

Kom je later een naam tegen en wil je weten wat hij doet, gebruik dan de index
achteraan.

### Kleuren en labels

| Label of kleur | Betekenis |
|---|---|
| <span style="background:#d9822b;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>METHODES</b></span> | een hoofdstuk geordend op wat je wil doen |
| <span style="background:#1c7ed6;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>VERZAMELING</b></span> | een bouwsteen die meerdere dingen bewaart |
| <span style="background:#2f9e44;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>EIGEN TYPE</b></span> | een bouwsteen die je zelf maakt |
| <span style="background:#7048e8;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>CONCEPT</b></span> | een idee dat door alle hoofdstukken heen loopt |
| <span style="background:#0c8599;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>IN JE API</b></span> | wat je nodig hebt zodra je een API bouwt |
| <code style="color:#228be6;font-weight:600">methode</code> | de naam van een methode of eigenschap |
| <code style="color:#845ef7">resultaat</code> | wat eruit komt |
| <code style="color:#f03e3e">Exception</code> | het programma crasht, of compileert niet |
| <span style="color:#37b24d"><b>Wel</b></span> / <span style="color:#f03e3e"><b>Niet</b></span> | wanneer je een bouwsteen kiest, en wanneer niet |

Open dit bestand in VS Code en druk op `Ctrl+Shift+V` om het met kleuren te zien.

> [!NOTE]
> De voorbeelden zijn losse regels over fruit, steden, leerlingen, dieren en
> verkeerslichten, en hier en daar een onderdeel of een ventilator. Het zijn nooit
> stukken uit Bouwcontrole of uit je schooloefeningen. Welke bouwsteen je in je
> eigen projecten gebruikt, beslis je zelf — dit leerboek geeft je de kennis om die
> keuze te kunnen maken.

> [!NOTE]
> De hoofdletters doen ertoe. In C# is het `Find`, niet `find`, en `typeof`, niet
> `TypeOf`. Methodes beginnen met een hoofdletter, sleutelwoorden van de taal met
> een kleine letter. Kleine letters vooraan zijn meestal JavaScript.

---

## Inhoud

| Deel | Hoofdstuk | Wat je leert |
|---|---|---|
| **1. Tekst en getallen** | [1. Tekst](#1-tekst) | lengte, hoofdletters, knippen, zoeken, vergelijken |
| | [2. Getallen en omzetten](#2-getallen-en-omzetten) | tekst naar getal, afronden, delen met rest |
| **2. Verzamelingen** | [3. Array](#3-array) | een rij met vaste lengte |
| | [4. List](#4-list) | een lijst die groeit en krimpt |
| | [5. Dictionary](#5-dictionary) | opzoeken op een sleutel |
| | [6. HashSet](#6-hashset) | een verzameling zonder dubbels |
| | [7. Queue](#7-queue) | wie eerst komt, eerst geholpen |
| | [8. Stack](#8-stack) | het laatste eerst terug |
| **3. Zoeken en rekenen** | [9. LINQ](#9-linq) | zoeken, filteren, tellen en sorteren in elke verzameling |
| | [10. IEnumerable](#10-ienumerable) | wat LINQ eigenlijk teruggeeft |
| **4. Eigen types** | [11. class](#11-class) | een bouwplan voor je eigen objecten |
| | [12. record](#12-record) | een pakketje gegevens dat draait om de inhoud |
| | [13. struct](#13-struct) | een klein pakketje dat echt gekopieerd wordt |
| | [14. Referentie tegenover waarde](#14-referentie-tegenover-waarde) | het belangrijkste idee van dit leerboek |
| | [15. Omgaan met niets](#15-omgaan-met-niets) | `null`, `??`, `?.` en getallen die mogen ontbreken |
| | [16. enum](#16-enum) | een vaste lijst benoemde keuzes |
| | [17. interface](#17-interface) | een contract: wat iets moet kunnen |
| | [18. Types herkennen](#18-types-herkennen) | `is`, `as`, `typeof`, `nameof` |
| **5. In je API** | [19. De helpers van een controller](#19-de-helpers-van-een-controller) | `Ok`, `NotFound`, `BadRequest` en de rest |
| | [20. Asynchroon werken](#20-asynchroon-werken) | `async`, `await`, `Task` |
| | [21. JSON en bestanden](#21-json-en-bestanden) | lezen, schrijven, omzetten |
| **Achteraan** | [Veelgemaakte verwarringen](#veelgemaakte-verwarringen) | vijftien paren die op elkaar lijken |
| | [Index](#index) | alles alfabetisch |

---

## Keuzegids

### Welke bouwsteen neem ik?

| Ik wil… | Neem | Hoofdstuk |
|---|---|---|
| een vast aantal dingen dat nooit verandert | `array` | 3 |
| een aantal dingen waar ik bijvoeg en afhaal | `List` | 4 |
| iets terugvinden op een naam, code of nummer | `Dictionary` | 5 |
| zeker zijn dat er geen dubbels in zitten | `HashSet` | 6 |
| dingen afhandelen in de volgorde waarin ze binnenkwamen | `Queue` | 7 |
| het laatst toegevoegde als eerste terugnemen | `Stack` | 8 |
| een verzameling doorgeven zonder te zeggen welke soort | `IEnumerable` | 10 |
| een eigen ding met gegevens én gedrag | `class` | 11 |
| een pakketje gegevens dat gelijk is zodra de inhoud gelijk is | `record` | 12 |
| een klein pakketje dat bij kopiëren echt gekopieerd wordt | `struct` | 13 |
| een vaste lijst benoemde keuzes | `enum` | 16 |
| afspreken wat iets moet kunnen, zonder te zeggen hoe | `interface` | 17 |

### Waar vind ik hoe ik…

| Ik wil… | Hoofdstuk |
|---|---|
| een stuk uit een tekst halen, of tekst opsplitsen | 1 |
| twee teksten vergelijken zonder op hoofdletters te letten | 1 |
| tekst omzetten naar een getal zonder te crashen | 2 |
| iets verdelen: hoeveel per groep, hoeveel over | 2 |
| iets toevoegen aan of verwijderen uit een lijst | 4 |
| per soort tellen hoeveel er van zijn | 5 |
| één item uit een verzameling halen | 9 |
| weten of er iets in zit dat voldoet | 9 |
| filteren, sorteren, optellen, het grootste vinden | 9 |
| begrijpen waarom mijn resultaat plots veranderd is | 10 |
| begrijpen waarom mijn lijst na een methode veranderd is | 14 |
| iets doen met een waarde die `null` kan zijn | 15 |
| nagaan van welk type een object is | 18 |
| de juiste statuscode teruggeven in een API | 19 |
| een JSON-bestand inlezen | 21 |

---

# Deel 1 — Tekst en getallen

Wat je met tekst en getallen doet, heb je in elk ander hoofdstuk nodig. Daarom komt dit eerst.

---

## 1. Tekst

<span style="background:#d9822b;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>METHODES</b></span>

### Hoe lang is het, is het leeg?

| Methode | Wat het doet | Voorbeeld | Resultaat |
|---|---|---|---|
| <code style="color:#228be6;font-weight:600">Length</code> | aantal tekens; een eigenschap, geen methode, dus zonder haakjes | `"moederbord".Length` | <code style="color:#845ef7">10</code> |
| <code style="color:#228be6;font-weight:600">string.IsNullOrEmpty</code> | is het null of een lege tekst? | `string.IsNullOrEmpty("")` | <code style="color:#845ef7">true</code> |
| <code style="color:#228be6;font-weight:600">string.IsNullOrWhiteSpace</code> | is het null, leeg, of enkel spaties? | `string.IsNullOrWhiteSpace("   ")` | <code style="color:#845ef7">true</code> |

Voor invoer die van een gebruiker komt neem je bijna altijd
`IsNullOrWhiteSpace`. Iemand die een spatie intikt heeft niets ingevuld, maar
`IsNullOrEmpty` vindt dat wel een waarde.

### Hoofdletters, kleine letters, spaties weg

| Methode | Wat het doet | Voorbeeld | Resultaat |
|---|---|---|---|
| <code style="color:#228be6;font-weight:600">ToUpper</code> | alles hoofdletters | `"mb-b650".ToUpper()` | <code style="color:#845ef7">"MB-B650"</code> |
| <code style="color:#228be6;font-weight:600">ToLower</code> | alles kleine letters | `"MB-B650".ToLower()` | <code style="color:#845ef7">"mb-b650"</code> |
| <code style="color:#228be6;font-weight:600">Trim</code> | spaties vooraan en achteraan weg | `"  ATX  ".Trim()` | <code style="color:#845ef7">"ATX"</code> |
| <code style="color:#228be6;font-weight:600">TrimStart</code> | enkel vooraan | `"  ATX".TrimStart()` | <code style="color:#845ef7">"ATX"</code> |
| <code style="color:#228be6;font-weight:600">TrimEnd</code> | enkel achteraan | `"ATX  ".TrimEnd()` | <code style="color:#845ef7">"ATX"</code> |
| <code style="color:#228be6;font-weight:600">PadLeft</code> | vul aan tot een lengte, vooraan | `"7".PadLeft(3, '0')` | <code style="color:#845ef7">"007"</code> |
| <code style="color:#228be6;font-weight:600">PadRight</code> | vul aan tot een lengte, achteraan | `"AM5".PadRight(6, '.')` | <code style="color:#845ef7">"AM5..."</code> |

Deze methodes veranderen je tekst niet, ze geven een nieuwe terug. `naam.Trim()`
op een regel alleen doet niets; je moet het resultaat ergens opvangen.

### Een stuk uit een tekst halen

| Methode | Wat het doet | Voorbeeld | Resultaat |
|---|---|---|---|
| <code style="color:#228be6;font-weight:600">Substring</code> | vanaf een positie, eventueel een aantal tekens | `"moederbord".Substring(6)` | <code style="color:#845ef7">"bord"</code> |
| <code style="color:#228be6;font-weight:600">Substring</code> | met lengte erbij | `"moederbord".Substring(0, 6)` | <code style="color:#845ef7">"moeder"</code> |
| <code style="color:#228be6;font-weight:600">[..]</code> | hetzelfde, met de bereikoperator | `"moederbord"[0..6]` | <code style="color:#845ef7">"moeder"</code> |
| <code style="color:#228be6;font-weight:600">[^..]</code> | de laatste n tekens | `"LZ-2026-8820"[^4..]` | <code style="color:#845ef7">"8820"</code> |
| <code style="color:#228be6;font-weight:600">Split</code> | knip op een teken, geeft een array | `"ATX,mATX,ITX".Split(',')` | <code style="color:#845ef7">["ATX", "mATX", "ITX"]</code> |
| <code style="color:#228be6;font-weight:600">string.Join</code> | plak stukken aan elkaar met een scheider | `string.Join(" / ", delen)` | <code style="color:#845ef7">"ATX / mATX / ITX"</code> |
| <code style="color:#228be6;font-weight:600">Replace</code> | vervang alle voorkomens | `"MB-B650-01".Replace("-", "")` | <code style="color:#845ef7">"MBB65001"</code> |

> [!WARNING]
> `Substring` loopt stuk zodra je buiten de tekst grijpt. `"ATX".Substring(0, 5)`
> geeft geen lege tekst maar een uitzondering. Controleer eerst de `Length`, of
> gebruik `Split` wanneer je niet weet hoe lang iets is.

### Zit er iets in, en waar?

| Methode | Wat het doet | Voorbeeld | Resultaat |
|---|---|---|---|
| <code style="color:#228be6;font-weight:600">Contains</code> | zit het erin, ja of nee | `"MB-B650-01".Contains("650")` | <code style="color:#845ef7">true</code> |
| <code style="color:#228be6;font-weight:600">StartsWith</code> | begint het ermee | `"MB-B650".StartsWith("MB")` | <code style="color:#845ef7">true</code> |
| <code style="color:#228be6;font-weight:600">EndsWith</code> | eindigt het erop | `"MB-B650-01".EndsWith("01")` | <code style="color:#845ef7">true</code> |
| <code style="color:#228be6;font-weight:600">IndexOf</code> | positie van het eerste voorkomen | `"MB-B650-01".IndexOf("-")` | <code style="color:#845ef7">2</code> |
| <code style="color:#228be6;font-weight:600">IndexOf</code> | niet gevonden | `"MB-B650".IndexOf("x")` | <code style="color:#845ef7">-1</code> |
| <code style="color:#228be6;font-weight:600">LastIndexOf</code> | positie van het laatste voorkomen | `"MB-B650-01".LastIndexOf("-")` | <code style="color:#845ef7">7</code> |

`Contains` als je enkel wil weten óf het erin zit. `IndexOf` als je daarna nog
iets met de plaats wil doen, bijvoorbeeld alles ervoor of erna afsplitsen.

> [!WARNING]
> `IndexOf` geeft `-1` terug als er niets gevonden is, geen `null`. Vergeet je
> dat te controleren en steek je die `-1` in een `Substring`, dan loopt je code
> stuk op een plek die er onschuldig uitziet.

### Tekst vergelijken

| Methode | Wat het doet | Voorbeeld | Resultaat |
|---|---|---|---|
| <code style="color:#228be6;font-weight:600">==</code> | letterlijk gelijk, hoofdlettergevoelig | `"AM5" == "am5"` | <code style="color:#845ef7">false</code> |
| <code style="color:#228be6;font-weight:600">Equals</code> | idem, maar je kan de regels meegeven | `"AM5".Equals("am5", StringComparison.OrdinalIgnoreCase)` | <code style="color:#845ef7">true</code> |
| <code style="color:#228be6;font-weight:600">CompareTo</code> | welke komt eerst in de sortering | `"b".CompareTo("a")` | <code style="color:#845ef7">1</code> |

Wil je twee sockets vergelijken die de ene keer met hoofdletters en de andere
keer zonder binnenkomen, dan is `Equals` met `OrdinalIgnoreCase` het nette
antwoord. `ToUpper()` op allebei werkt ook, maar maakt twee nieuwe teksten om
er één vergelijking mee te doen.

### Tekst opbouwen met waarden erin

| Vorm | Wat het doet | Voorbeeld | Resultaat |
|---|---|---|---|
| <code style="color:#228be6;font-weight:600">&#36;"..."</code> | zet waarden rechtstreeks in de tekst | <code>&#36;"Socket {socket} past"</code> | <code style="color:#845ef7">"Socket AM5 past"</code> |
| <code style="color:#228be6;font-weight:600">+</code> | plakken | `"Socket " + socket` | <code style="color:#845ef7">"Socket AM5"</code> |
| <code style="color:#228be6;font-weight:600">string.Format</code> | oudere vorm met plaatshouders | `string.Format("Socket {0}", socket)` | <code style="color:#845ef7">"Socket AM5"</code> |

De <code>&#36;"..."</code>-vorm is wat je overal zal tegenkomen. Je mag er ook berekeningen in
zetten: <code>&#36;"Nog {sloten - modules} vrij"</code>.

### Voorspel de uitkomst

**Vraag 1**

```csharp
string s = "  Gent  ";
s.Trim();
Console.WriteLine("[" + s + "]");
```

<details>
<summary><span style="color:#845ef7"><b>Toon het antwoord</b></span></summary>

<code style="color:#845ef7">[  Gent  ]</code>

`Trim` verandert de tekst niet. Het geeft een nieuwe tekst terug, en die werd
hier nergens opgevangen. Wil je dat het werkt, dan schrijf je `s = s.Trim();`.

</details>

**Vraag 2**

```csharp
string code = "ABC-123";
Console.WriteLine(code.Substring(code.IndexOf("-") + 1));
```

<details>
<summary><span style="color:#845ef7"><b>Toon het antwoord</b></span></summary>

<code style="color:#845ef7">123</code>

Het streepje staat op positie 3. Eén verder is positie 4, en vanaf daar neemt
`Substring` alles tot het einde. Dit is het patroon om alles ná een scheidingsteken
te krijgen.

</details>

**Vraag 3**

```csharp
Console.WriteLine("appel" == "Appel");
Console.WriteLine("appel".Equals("Appel", StringComparison.OrdinalIgnoreCase));
```

<details>
<summary><span style="color:#845ef7"><b>Toon het antwoord</b></span></summary>

<code style="color:#845ef7">False</code> en daarna <code style="color:#845ef7">True</code>

`==` let op hoofdletters. Met `OrdinalIgnoreCase` zeg je uitdrukkelijk dat het
niet mag.

</details>

**Vraag 4**

```csharp
string[] delen = "rood;groen;;blauw".Split(';');
Console.WriteLine(delen.Length);
```

<details>
<summary><span style="color:#845ef7"><b>Toon het antwoord</b></span></summary>

<code style="color:#845ef7">4</code>

Tussen de twee puntkomma's staat niets, en dat niets telt ook mee als stuk: een
lege tekst. De vier stukken zijn `rood`, `groen`, een lege tekst en `blauw`.

</details>

---

## 2. Getallen en omzetten

<span style="background:#d9822b;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>METHODES</b></span>

### Tekst omzetten naar een getal

| Methode | Wat het doet | Voorbeeld | Resultaat |
|---|---|---|---|
| <code style="color:#228be6;font-weight:600">int.Parse</code> | zet om, of loopt stuk | `int.Parse("120")` | <code style="color:#845ef7">120</code> |
| <code style="color:#228be6;font-weight:600">int.Parse</code> | bij onzin | `int.Parse("abc")` | uitzondering |
| <code style="color:#228be6;font-weight:600">int.TryParse</code> | probeert om te zetten, zegt of het lukte | `int.TryParse("120", out int n)` | <code style="color:#845ef7">true</code>, <code style="color:#845ef7">n</code> is <code style="color:#845ef7">120</code> |
| <code style="color:#228be6;font-weight:600">int.TryParse</code> | bij onzin | `int.TryParse("abc", out int n)` | <code style="color:#845ef7">false</code>, <code style="color:#845ef7">n</code> is <code style="color:#845ef7">0</code> |
| <code style="color:#228be6;font-weight:600">double.Parse</code> | hetzelfde voor kommagetallen | `double.Parse("2.5")` | hangt af van je taalinstelling — zie hieronder |

> [!IMPORTANT]
> De regel is eenvoudig: komt de tekst van een gebruiker, gebruik `TryParse`.
> Komt ze uit je eigen code en weet je zeker dat het een getal is, dan mag
> `Parse`. Een `Parse` op gebruikersinvoer is een crash die op je zit te wachten.

> [!WARNING]
> `double.Parse` leest een kommagetal volgens de taalinstelling van je computer.
> Op een Belgische computer is de komma het decimaalteken en de punt het teken
> voor duizendtallen, zoals in 1.000. `double.Parse("2.5")` geeft daar `25` —
> zonder foutmelding. Komt je getal uit een bestand of een API, waar altijd een
> punt staat, geef dan `CultureInfo.InvariantCulture` mee:
> `double.Parse("2.5", CultureInfo.InvariantCulture)`.
>
> Omgekeerd toont `Console.WriteLine(2.5)` op jouw computer `2,5`. In dit leerboek
> staan kommagetallen in de schrijfwijze van C# zelf, met een punt.

### Rekenen

| Methode | Wat het doet | Voorbeeld | Resultaat |
|---|---|---|---|
| <code style="color:#228be6;font-weight:600">Math.Max</code> | de grootste van twee | `Math.Max(3, 7)` | <code style="color:#845ef7">7</code> |
| <code style="color:#228be6;font-weight:600">Math.Min</code> | de kleinste van twee | `Math.Min(3, 7)` | <code style="color:#845ef7">3</code> |
| <code style="color:#228be6;font-weight:600">Math.Abs</code> | zonder minteken | `Math.Abs(-5)` | <code style="color:#845ef7">5</code> |
| <code style="color:#228be6;font-weight:600">Math.Round</code> | afronden | `Math.Round(2.4)` | <code style="color:#845ef7">2</code> |
| <code style="color:#228be6;font-weight:600">Math.Ceiling</code> | altijd naar boven | `Math.Ceiling(2.1)` | <code style="color:#845ef7">3</code> |
| <code style="color:#228be6;font-weight:600">Math.Floor</code> | altijd naar beneden | `Math.Floor(2.9)` | <code style="color:#845ef7">2</code> |
| <code style="color:#228be6;font-weight:600">Math.Pow</code> | tot de macht | `Math.Pow(2, 3)` | <code style="color:#845ef7">8</code> |
| <code style="color:#228be6;font-weight:600">/</code> | delen van gehele getallen | `7 / 2` | <code style="color:#845ef7">3</code> |
| <code style="color:#228be6;font-weight:600">%</code> | de rest na het delen | `7 % 2` | <code style="color:#845ef7">1</code> |

> [!WARNING]
> Twee dingen die bijna iedereen één keer verkeerd doet. `7 / 2` geeft `3` en
> niet `3.5`, want twee gehele getallen delen geeft een geheel getal. En
> `Math.Round(2.5)` geeft `2`, niet `3` — C# rondt een halve eenheid naar het
> dichtstbijzijnde even getal. Wil je het schoolse afronden, dan geef je
> `MidpointRounding.AwayFromZero` mee.

Het deelteken en het restteken samen zijn precies wat je nodig hebt om iets te
verdelen: hoeveel per groep, en hoeveel blijven er over.

### Voorspel de uitkomst

**Vraag 1**

```csharp
Console.WriteLine(7 / 2);
Console.WriteLine(7 % 2);
```

<details>
<summary><span style="color:#845ef7"><b>Toon het antwoord</b></span></summary>

<code style="color:#845ef7">3</code> en daarna <code style="color:#845ef7">1</code>

Twee gehele getallen delen geeft een geheel getal: 2 past 3 keer in 7. Wat er
overblijft, 1, krijg je met `%`.

</details>

**Vraag 2**

```csharp
bool gelukt = int.TryParse("12a", out int getal);
Console.WriteLine(gelukt);
Console.WriteLine(getal);
```

<details>
<summary><span style="color:#845ef7"><b>Toon het antwoord</b></span></summary>

<code style="color:#845ef7">False</code> en daarna <code style="color:#845ef7">0</code>

`"12a"` is geen getal. `TryParse` crasht niet, geeft `false` terug, en zet
`getal` op nul.

</details>

**Vraag 3**

```csharp
Console.WriteLine(Math.Round(3.5));
Console.WriteLine(Math.Round(4.5));
```

<details>
<summary><span style="color:#845ef7"><b>Toon het antwoord</b></span></summary>

<code style="color:#845ef7">4</code> en daarna <code style="color:#845ef7">4</code>

Geen tikfout. C# rondt een halve eenheid af naar het dichtstbijzijnde **even**
getal: 3,5 wordt 4, en 4,5 wordt ook 4. Wil je dat 4,5 een 5 wordt, dan geef je
`MidpointRounding.AwayFromZero` mee.

</details>

**Vraag 4**

Op een computer met Belgische taalinstelling:

```csharp
double d = double.Parse("2.5");
Console.WriteLine(d);
```

<details>
<summary><span style="color:#845ef7"><b>Toon het antwoord</b></span></summary>

<code style="color:#845ef7">25</code>

Op een Belgische computer is de punt het teken voor duizendtallen, zoals in
1.000. `double.Parse` leest `"2.5"` dus als vijfentwintig, zonder foutmelding.
Met `double.Parse("2.5", CultureInfo.InvariantCulture)` krijg je wel twee en een
half. Zie de waarschuwing hierboven.

</details>

---

# Deel 2 — Verzamelingen

Zes manieren om meerdere dingen samen te bewaren. Ze verschillen in wat ze toelaten en in hoe snel je iets terugvindt.

## De verzamelingen naast elkaar

| | array | List | Dictionary | HashSet | Queue | Stack |
|---|---|---|---|---|---|---|
| **Grootte** | vast | groeit | groeit | groeit | groeit | groeit |
| **Volgorde bewaard** | ja | ja | niet gegarandeerd | niet gegarandeerd | ja, eerst in eerst uit | ja, laatst in eerst uit |
| **Dubbels** | mag | mag | sleutels nee, waarden wel | nee | mag | mag |
| **Opvragen op** | positie | positie | sleutel | — | enkel vooraan | enkel bovenaan |
| **Snel nagaan of iets erin zit** | nee | nee | ja, op sleutel | ja | nee | nee |

Die laatste rij verdient uitleg. Een `List` met tienduizend items moet bij
`Contains` in het slechtste geval alle tienduizend nakijken. Een `Dictionary` of
`HashSet` weet meteen waar hij moet kijken, hoe groot hij ook is. Bij tien items
merk je dat niet. Bij een miljoen wel.

---

## 3. Array

<span style="background:#1c7ed6;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>VERZAMELING</b></span>

### Wat het is

**Een rij vakjes met een vaste lengte.** Zoals een eierdoos: zes plaatsen, niet
meer en niet minder. Je kan een ei vervangen, maar je kan er geen zevende
plaats bij maken.

### Hoe het eruitziet

```csharp
string[] dagen = { "ma", "di", "wo", "do", "vr" };

Console.WriteLine(dagen[0]);        // ma
Console.WriteLine(dagen.Length);    // 5

dagen[4] = "vrijdag";               // vervangen mag

int[] scores = new int[3];          // drie vakjes, allemaal 0

foreach (string dag in dagen)
{
    Console.WriteLine(dag);
}
```

### Wanneer wel, wanneer niet

<span style="color:#37b24d"><b>Wel</b></span> — de hoeveelheid ligt vast en verandert nooit: de dagen van de week, de
maanden, de vakjes van een bordspel.

<span style="color:#f03e3e"><b>Niet</b></span> — zodra je iets wil bijvoegen of weghalen. Een array heeft geen `Add`
en geen `Remove`.

### Veelgemaakte fouten

> [!WARNING]
> - De eerste positie is `0`, dus de laatste is `Length - 1`. Vraag je
>   `dagen[5]` op in een array van vijf, dan crasht je programma.
> - Het is `Length`, niet `Count`. Bij een `List` is het net omgekeerd.
> - `new int[3]` geeft drie nullen. `new string[3]` geeft drie keer `null`, geen
>   drie lege teksten.

### Voorspel de uitkomst

**Vraag 1**

```csharp
int[] a = new int[3];
Console.WriteLine(a[1]);
```

<details>
<summary><span style="color:#845ef7"><b>Toon het antwoord</b></span></summary>

<code style="color:#845ef7">0</code>

Een nieuwe array van getallen is gevuld met de standaardwaarde, en die is voor
een `int` gelijk aan nul.

</details>

**Vraag 2**

```csharp
string[] s = { "a", "b", "c" };
Console.WriteLine(s[s.Length - 1]);
```

<details>
<summary><span style="color:#845ef7"><b>Toon het antwoord</b></span></summary>

<code style="color:#845ef7">c</code>

`Length` is 3, dus `Length - 1` is 2, en positie 2 is het derde vakje.

</details>

**Vraag 3**

```csharp
string[] s = { "a", "b", "c" };
Console.WriteLine(s[3]);
```

<details>
<summary><span style="color:#845ef7"><b>Toon het antwoord</b></span></summary>

<code style="color:#f03e3e">IndexOutOfRangeException</code> — het programma crasht.

Er zijn drie vakjes: 0, 1 en 2. Vakje 3 bestaat niet.

</details>

**Vraag 4**

```csharp
int[] a = { 1, 2, 3 };
int[] b = a;
b[0] = 99;
Console.WriteLine(a[0]);
```

<details>
<summary><span style="color:#845ef7"><b>Toon het antwoord</b></span></summary>

<code style="color:#845ef7">99</code>

Dit is de belangrijkste vraag van dit hoofdstuk. `b = a` maakt geen kopie van de
array. Het geeft dezelfde array een tweede naam. Verander je iets via `b`, dan
zie je dat ook via `a`, want het is één en dezelfde eierdoos. Zie ook
[hoofdstuk 14](#14-referentie-tegenover-waarde).

</details>

---

## 4. List

<span style="background:#1c7ed6;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>VERZAMELING</b></span>

### Wat het is

**Een array die kan groeien en krimpen.** Zoals een boodschappenlijstje: je
schrijft erbij, je streept door, en de volgorde blijft zoals je ze opschreef.

Tussen de punthaken staat wat erin mag. `List<string>` is een lijst van
teksten, `List<int>` een lijst van getallen, `List<Leerling>` een lijst van
leerlingen. Iets anders erin steken compileert niet.

### Hoe het eruitziet

```csharp
List<string> fruit = new List<string>();
fruit.Add("appel");
fruit.Add("peer");
fruit.Add("kiwi");

Console.WriteLine(fruit.Count);     // 3
Console.WriteLine(fruit[1]);        // peer

fruit.Remove("peer");
Console.WriteLine(fruit[1]);        // kiwi — alles schuift een plaats op

fruit.Insert(0, "banaan");          // vooraan bijzetten

List<string> kort = new List<string> { "appel", "peer" };   // meteen gevuld
```

Je zal ook de kortere schrijfwijze `List<string> kort = ["appel", "peer"];`
tegenkomen. Die doet hetzelfde.

### Wat je ermee kan

Dit zijn de methodes die op een `List<T>` zelf zitten. Ze werken enkel op een
lijst, niet op elke verzameling.

#### Toevoegen en verwijderen

| Methode | Wat het doet | Voorbeeld | Resultaat |
|---|---|---|---|
| <code style="color:#228be6;font-weight:600">Add</code> | zet er één achteraan bij | `fans.Add(nieuweFan)` | de lijst is één langer |
| <code style="color:#228be6;font-weight:600">AddRange</code> | zet er meerdere achteraan bij | `fans.AddRange(extraFans)` | de lijst groeit |
| <code style="color:#228be6;font-weight:600">Insert</code> | zet er één op een vaste plaats | `fans.Insert(0, nieuweFan)` | staat nu vooraan |
| <code style="color:#228be6;font-weight:600">Remove</code> | haal dit ene item eruit | `fans.Remove(fan)` | <code style="color:#845ef7">true</code> als het erin zat |
| <code style="color:#228be6;font-weight:600">RemoveAt</code> | haal weg op positie | `fans.RemoveAt(0)` | de eerste is weg |
| <code style="color:#228be6;font-weight:600">RemoveAll</code> | haal alles weg dat voldoet | `fans.RemoveAll(f => f.Prijs == 0)` | <code style="color:#845ef7">2</code> als er twee weg zijn |
| <code style="color:#228be6;font-weight:600">Clear</code> | maak leeg | `fans.Clear()` | de lijst is leeg |

`Remove` wil het item zelf, `RemoveAt` wil een positie, `RemoveAll` wil een
voorwaarde. Drie namen die op elkaar lijken en drie verschillende dingen willen.

#### Erin zoeken

| Methode | Wat het doet | Voorbeeld | Resultaat |
|---|---|---|---|
| <code style="color:#228be6;font-weight:600">Count</code> | hoeveel zitten erin; een eigenschap | `fans.Count` | <code style="color:#845ef7">4</code> |
| <code style="color:#228be6;font-weight:600">Contains</code> | zit dit exacte item erin | `fans.Contains(fan)` | <code style="color:#845ef7">true</code> |
| <code style="color:#228be6;font-weight:600">IndexOf</code> | op welke positie staat het | `fans.IndexOf(fan)` | <code style="color:#845ef7">2</code>, of <code style="color:#845ef7">-1</code> |
| <code style="color:#228be6;font-weight:600">Exists</code> | is er minstens één die voldoet | `fans.Exists(f => f.Prijs > 50)` | <code style="color:#845ef7">true</code> |
| <code style="color:#228be6;font-weight:600">Find</code> | geef de eerste die voldoet | `fans.Find(f => f.Id == 3)` | het item, of <code style="color:#845ef7">null</code> |
| <code style="color:#228be6;font-weight:600">FindAll</code> | geef allemaal die voldoen | `fans.FindAll(f => f.Merk == "NZXT")` | een nieuwe lijst |
| <code style="color:#228be6;font-weight:600">FindIndex</code> | op welke positie staat de eerste die voldoet | `fans.FindIndex(f => f.Id == 3)` | <code style="color:#845ef7">2</code>, of <code style="color:#845ef7">-1</code> |

#### Ordenen

| Methode | Wat het doet | Voorbeeld | Resultaat |
|---|---|---|---|
| <code style="color:#228be6;font-weight:600">Sort</code> | sorteer de lijst zelf | `getallen.Sort()` | de lijst staat op volgorde |
| <code style="color:#228be6;font-weight:600">Reverse</code> | keer de lijst om | `getallen.Reverse()` | omgekeerde volgorde |
| <code style="color:#228be6;font-weight:600">ToArray</code> | maak er een array van | `fans.ToArray()` | een array |

> [!IMPORTANT]
> `Sort` en `Reverse` veranderen je lijst zelf en geven niets terug. De
> LINQ-methodes uit hoofdstuk 9 doen het omgekeerde: die laten je
> lijst met rust en geven een nieuw resultaat. Dat verschil verklaart waarom
> `lijst.Sort()` werkt en `var x = lijst.Sort()` niet compileert.

### Wanneer wel, wanneer niet

<span style="color:#37b24d"><b>Wel</b></span> — de standaardkeuze voor "een aantal dingen". Twijfel je, begin dan met
een `List`.

<span style="color:#f03e3e"><b>Niet</b></span> — wanneer je telkens op dezelfde eigenschap zoekt (neem een
`Dictionary`), of wanneer dubbels verboden zijn (neem een `HashSet`).

### Veelgemaakte fouten

> [!WARNING]
> - Het is `Count`, niet `Length`.
> - Na `Remove` of `RemoveAt` schuift alles erachter een plaats op. Een positie
>   die je vóór het verwijderen onthield, wijst daarna naar iets anders.
> - Je mag een lijst niet veranderen terwijl je er met `foreach` doorheen loopt.
>   Dat crasht, ook als je code er onschuldig uitziet.
> - `List<string> lijst;` zonder `new` is geen lege lijst maar niets. `lijst.Add`
>   crasht dan.

### Voorspel de uitkomst

**Vraag 1**

```csharp
var l = new List<int> { 10, 20, 30 };
l.RemoveAt(0);
Console.WriteLine(l[0]);
```

<details>
<summary><span style="color:#845ef7"><b>Toon het antwoord</b></span></summary>

<code style="color:#845ef7">20</code>

De 10 op positie 0 is weg. De 20 schuift op naar positie 0.

</details>

**Vraag 2**

```csharp
var l = new List<string> { "a", "b", "a" };
l.Remove("a");
Console.WriteLine(l.Count);
```

<details>
<summary><span style="color:#845ef7"><b>Toon het antwoord</b></span></summary>

<code style="color:#845ef7">2</code>

`Remove` haalt enkel het **eerste** voorkomen weg. De lijst is nu `b`, `a`. Wil je
ze allemaal weg, dan heb je `RemoveAll` nodig.

</details>

**Vraag 3**

```csharp
var l = new List<int> { 1, 2, 3 };
foreach (int x in l)
{
    if (x == 2)
    {
        l.Remove(x);
    }
}
```

<details>
<summary><span style="color:#845ef7"><b>Toon het antwoord</b></span></summary>

<code style="color:#f03e3e">InvalidOperationException</code> — het programma crasht.

`foreach` merkt dat de lijst veranderd is terwijl hij erdoor liep, en weigert
verder te gaan. Wil je tijdens het doorlopen verwijderen, gebruik dan
`RemoveAll` met een voorwaarde, of loop door een kopie.

</details>

**Vraag 4**

```csharp
var l = new List<string> { "kiwi", "appel", "peer" };
l.Sort();
Console.WriteLine(l[0]);
```

<details>
<summary><span style="color:#845ef7"><b>Toon het antwoord</b></span></summary>

<code style="color:#845ef7">appel</code>

`Sort` zet de lijst zelf op alfabetische volgorde. Het geeft niets terug, het
verandert de lijst.

</details>

---

## 5. Dictionary

<span style="background:#1c7ed6;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>VERZAMELING</b></span>

### Wat het is

**Een woordenboek.** Je zoekt niet op paginanummer maar op het woord zelf. Elk
woord staat er maar één keer in, en bij elk woord hoort een uitleg.

In C# heet het woord de **sleutel** en de uitleg de **waarde**. Tussen de
punthaken staan ze allebei: `Dictionary<string, int>` heeft teksten als sleutel
en getallen als waarde.

### Hoe het eruitziet

```csharp
Dictionary<string, string> hoofdsteden = new Dictionary<string, string>();

hoofdsteden.Add("België", "Brussel");
hoofdsteden["Frankrijk"] = "Parijs";          // toevoegen, of overschrijven

Console.WriteLine(hoofdsteden["België"]);     // Brussel
Console.WriteLine(hoofdsteden.Count);         // 2

if (hoofdsteden.TryGetValue("Spanje", out string? stad))
{
    Console.WriteLine(stad);
}
else
{
    Console.WriteLine("Onbekend land");       // dit wordt getoond
}

foreach (KeyValuePair<string, string> paar in hoofdsteden)
{
    Console.WriteLine($"{paar.Key}: {paar.Value}");
}
```

Bij het doorlopen krijg je telkens een paar terug: `.Key` is de sleutel,
`.Value` de waarde.

### Het patroon dat je het vaakst zal gebruiken: tellen

```csharp
string[] dieren = { "kat", "hond", "kat", "vis", "kat" };
Dictionary<string, int> telling = new Dictionary<string, int>();

foreach (string dier in dieren)
{
    if (telling.ContainsKey(dier))
    {
        telling[dier]++;
    }
    else
    {
        telling[dier] = 1;
    }
}

Console.WriteLine(telling["kat"]);    // 3
Console.WriteLine(telling["vis"]);    // 1
```

Zodra je ergens *hoeveel van elk* moet weten, is dit het antwoord. Het werkt
voor woorden, letters, kleuren, stemmen — alles waar je per soort wil tellen.

### Wanneer wel, wanneer niet

<span style="color:#37b24d"><b>Wel</b></span> — je zoekt telkens op één en dezelfde eigenschap: een naam, een
code, een nummer. En je wil dat snel doen, hoe groot de verzameling ook is.

<span style="color:#f03e3e"><b>Niet</b></span> — wanneer de volgorde ertoe doet, of wanneer je afwisselend op
verschillende eigenschappen zoekt. Een woordenboek zoekt snel op het woord, maar
niet op de uitleg.

### Wat je ermee kan

| Methode | Wat het doet | Voorbeeld | Resultaat |
|---|---|---|---|
| <code style="color:#228be6;font-weight:600">[sleutel]</code> | haal op, of loopt stuk | `prijzen["dag"]` | <code style="color:#845ef7">65</code> |
| <code style="color:#228be6;font-weight:600">Add</code> | voeg toe, of loopt stuk bij een dubbele sleutel | `prijzen.Add("vip", 210)` | staat erin |
| <code style="color:#228be6;font-weight:600">TryAdd</code> | voeg toe als de sleutel vrij is | `prijzen.TryAdd("vip", 210)` | <code style="color:#845ef7">true</code> of <code style="color:#845ef7">false</code> |
| <code style="color:#228be6;font-weight:600">ContainsKey</code> | bestaat die sleutel | `prijzen.ContainsKey("vip")` | <code style="color:#845ef7">true</code> |
| <code style="color:#228be6;font-weight:600">TryGetValue</code> | haal op en zeg of het lukte | `prijzen.TryGetValue("vip", out int p)` | <code style="color:#845ef7">true</code>, <code style="color:#845ef7">p</code> is <code style="color:#845ef7">210</code> |
| <code style="color:#228be6;font-weight:600">Remove</code> | haal die sleutel weg | `prijzen.Remove("vip")` | <code style="color:#845ef7">true</code> |
| <code style="color:#228be6;font-weight:600">Keys</code> | alle sleutels | `prijzen.Keys` | <code style="color:#845ef7">["dag", "combi", "vip"]</code> |
| <code style="color:#228be6;font-weight:600">Values</code> | alle waarden | `prijzen.Values` | <code style="color:#845ef7">[65, 120, 210]</code> |

Dezelfde verhouding als bij `Parse` en `TryParse`: de vierkante haken lopen stuk
op een sleutel die niet bestaat, `TryGetValue` niet. Weet je niet zeker of de
sleutel er is, neem dan `TryGetValue`.

### Twee manieren om toe te voegen

| Schrijfwijze | Sleutel bestaat nog niet | Sleutel bestaat al |
|---|---|---|
| `d.Add(sleutel, waarde)` | voegt toe | crasht |
| `d[sleutel] = waarde` | voegt toe | overschrijft |

Kies bewust. `Add` is een bewaker: hij zegt je dat er iets dubbel binnenkomt.
De vierkante haken zijn soepel: ze overschrijven zonder te klagen.

### Veelgemaakte fouten

> [!WARNING]
> - `hoofdsteden["Spanje"]` lezen terwijl Spanje er niet in staat, crasht. Twijfel
>   je, gebruik dan `TryGetValue` of eerst `ContainsKey`.
> - Sleutels zijn hoofdlettergevoelig. `"Kat"` en `"kat"` zijn twee verschillende
>   sleutels. Wil je dat niet, geef dan `StringComparer.OrdinalIgnoreCase` mee bij
>   het aanmaken.
> - Reken niet op de volgorde bij het doorlopen. Vandaag komen ze toevallig terug
>   in de volgorde waarin je ze toevoegde, maar dat is geen belofte.

### Voorspel de uitkomst

**Vraag 1**

```csharp
var d = new Dictionary<string, int>();
d["a"] = 1;
d["a"] = 2;
Console.WriteLine(d["a"]);
Console.WriteLine(d.Count);
```

<details>
<summary><span style="color:#845ef7"><b>Toon het antwoord</b></span></summary>

<code style="color:#845ef7">2</code> en daarna <code style="color:#845ef7">1</code>

De tweede regel overschrijft de eerste. Er is nog altijd maar één sleutel.

</details>

**Vraag 2**

```csharp
var d = new Dictionary<string, int>();
d.Add("a", 1);
d.Add("a", 2);
```

<details>
<summary><span style="color:#845ef7"><b>Toon het antwoord</b></span></summary>

<code style="color:#f03e3e">ArgumentException</code> — het programma crasht.

`Add` weigert een sleutel die er al in zit. Dat is het verschil met de vierkante
haken uit vraag 1.

</details>

**Vraag 3**

```csharp
var d = new Dictionary<string, int> { ["Kat"] = 4 };
Console.WriteLine(d.ContainsKey("kat"));
```

<details>
<summary><span style="color:#845ef7"><b>Toon het antwoord</b></span></summary>

<code style="color:#845ef7">False</code>

`"Kat"` met hoofdletter en `"kat"` zonder zijn voor een gewone `Dictionary` twee
verschillende sleutels.

</details>

**Vraag 4**

```csharp
var d = new Dictionary<string, int> { ["x"] = 5 };
bool gevonden = d.TryGetValue("y", out int waarde);
Console.WriteLine(gevonden);
Console.WriteLine(waarde);
```

<details>
<summary><span style="color:#845ef7"><b>Toon het antwoord</b></span></summary>

<code style="color:#845ef7">False</code> en daarna <code style="color:#845ef7">0</code>

De sleutel `"y"` bestaat niet. `TryGetValue` crasht niet maar geeft `false`
terug, en zet `waarde` op de standaardwaarde van een `int`: nul.

</details>

---

## 6. HashSet

<span style="background:#1c7ed6;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>VERZAMELING</b></span>

### Wat het is

**Een verzameling waarin alles maar één keer voorkomt.** Zoals een
stickeralbum: een dubbele sticker plak je niet opnieuw in, je weet gewoon dat je
hem al hebt.

### Hoe het eruitziet

```csharp
HashSet<string> bezocht = new HashSet<string>();

bezocht.Add("Gent");
bezocht.Add("Brugge");
bool nieuw = bezocht.Add("Gent");            // false — stond er al in

Console.WriteLine(bezocht.Count);            // 2
Console.WriteLine(bezocht.Contains("Brugge"));   // True
```

Een `HashSet` kan ook rekenen met verzamelingen:

| Methode | Wat er overblijft |
|---|---|
| `a.UnionWith(b)` | alles wat in `a` óf `b` zit |
| `a.IntersectWith(b)` | enkel wat in `a` én `b` zit |
| `a.ExceptWith(b)` | wat in `a` zit maar niet in `b` |

Die drie veranderen `a` zelf.

### Wanneer wel, wanneer niet

<span style="color:#37b24d"><b>Wel</b></span> — dubbels mogen niet voorkomen, of je moet vaak en snel nagaan of
iets er al in zit.

<span style="color:#f03e3e"><b>Niet</b></span> — wanneer de volgorde ertoe doet, of wanneer je iets op een positie
wil opvragen. Een `HashSet` heeft geen `[0]`.

### Veelgemaakte fouten

> [!WARNING]
> - Een dubbele `Add` crasht niet, hij geeft gewoon `false` terug. Controleer je
>   dat niet, dan merk je nooit dat er iets dubbel binnenkwam.
> - Er bestaat geen eerste, tweede of derde. Wil je op positie opvragen, dan is
>   dit de verkeerde bouwsteen.

### Voorspel de uitkomst

**Vraag 1**

```csharp
var s = new HashSet<int> { 1, 2, 2, 3, 3, 3 };
Console.WriteLine(s.Count);
```

<details>
<summary><span style="color:#845ef7"><b>Toon het antwoord</b></span></summary>

<code style="color:#845ef7">3</code>

De dubbels worden stilzwijgend genegeerd. Er blijven drie verschillende getallen
over.

</details>

**Vraag 2**

```csharp
var s = new HashSet<string>();
Console.WriteLine(s.Add("a"));
Console.WriteLine(s.Add("a"));
```

<details>
<summary><span style="color:#845ef7"><b>Toon het antwoord</b></span></summary>

<code style="color:#845ef7">True</code> en daarna <code style="color:#845ef7">False</code>

De eerste keer is `"a"` nieuw. De tweede keer niet meer.

</details>

**Vraag 3**

```csharp
var a = new HashSet<int> { 1, 2, 3 };
var b = new HashSet<int> { 2, 3, 4 };
a.IntersectWith(b);
Console.WriteLine(a.Count);
```

<details>
<summary><span style="color:#845ef7"><b>Toon het antwoord</b></span></summary>

<code style="color:#845ef7">2</code>

Enkel wat in beide zit, blijft over in `a`: 2 en 3.

</details>

---

## 7. Queue

<span style="background:#1c7ed6;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>VERZAMELING</b></span>

### Wat het is

**Een wachtrij.** Wie eerst komt, wordt eerst geholpen. Je kan niet
voorsteken, en je kan niemand uit het midden halen.

### Hoe het eruitziet

```csharp
Queue<string> wachtrij = new Queue<string>();

wachtrij.Enqueue("Anna");       // achteraan aansluiten
wachtrij.Enqueue("Bram");
wachtrij.Enqueue("Cleo");

Console.WriteLine(wachtrij.Peek());     // Anna — kijken wie vooraan staat
string volgende = wachtrij.Dequeue();   // Anna — en nu is ze weg
Console.WriteLine(wachtrij.Count);      // 2
```

### Wanneer wel, wanneer niet

<span style="color:#37b24d"><b>Wel</b></span> — dingen afhandelen in de volgorde waarin ze binnenkwamen:
printopdrachten, berichten, klanten aan een loket.

<span style="color:#f03e3e"><b>Niet</b></span> — zodra je iets in het midden wil bereiken of wil overslaan.

### Veelgemaakte fouten

> [!WARNING]
> - `Dequeue` of `Peek` op een lege wachtrij crasht. Controleer eerst `Count`, of
>   gebruik `TryDequeue`.
> - `Peek` kijkt enkel, `Dequeue` haalt weg. Wie ze verwisselt, verwerkt de
>   eerste persoon eindeloos opnieuw.

### Voorspel de uitkomst

**Vraag 1**

```csharp
var q = new Queue<int>();
q.Enqueue(1);
q.Enqueue(2);
q.Enqueue(3);
q.Dequeue();
Console.WriteLine(q.Peek());
```

<details>
<summary><span style="color:#845ef7"><b>Toon het antwoord</b></span></summary>

<code style="color:#845ef7">2</code>

De 1 stond vooraan en is weggehaald. Nu staat de 2 vooraan.

</details>

**Vraag 2**

```csharp
var q = new Queue<int>();
q.Dequeue();
```

<details>
<summary><span style="color:#845ef7"><b>Toon het antwoord</b></span></summary>

<code style="color:#f03e3e">InvalidOperationException</code> — het programma crasht.

Er staat niemand in de rij.

</details>

---

## 8. Stack

<span style="background:#1c7ed6;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>VERZAMELING</b></span>

### Wat het is

**Een stapel borden.** Het bord dat je laatst neerlegt, neem je als eerste weer
op. Het onderste bord raak je pas als alles erboven weg is.

### Hoe het eruitziet

```csharp
Stack<string> stappen = new Stack<string>();

stappen.Push("typ tekst");      // bovenop leggen
stappen.Push("maak vet");
stappen.Push("wis regel");

Console.WriteLine(stappen.Pop());   // wis regel — bovenste eraf
Console.WriteLine(stappen.Peek());  // maak vet — kijken, niet wegnemen
```

### Wanneer wel, wanneer niet

<span style="color:#37b24d"><b>Wel</b></span> — ongedaan maken, de terug-knop van een browser, nagaan of haakjes
correct openen en sluiten. Overal waar het laatste het eerst weer aan de beurt
komt.

<span style="color:#f03e3e"><b>Niet</b></span> — wanneer iedereen eerlijk op zijn beurt moet wachten. Dan is het een
`Queue`.

### Veelgemaakte fouten

> [!WARNING]
> - `Pop` of `Peek` op een lege stapel crasht, net als bij een wachtrij.
> - Een `Stack` en een `Queue` met dezelfde inhoud geven hun items in omgekeerde
>   volgorde terug. Dat is het hele verschil, en het is groot.

### Voorspel de uitkomst

**Vraag 1**

```csharp
var s = new Stack<int>();
s.Push(1);
s.Push(2);
s.Push(3);
Console.WriteLine(s.Pop() + s.Pop());
```

<details>
<summary><span style="color:#845ef7"><b>Toon het antwoord</b></span></summary>

<code style="color:#845ef7">5</code>

De eerste `Pop` geeft 3, de tweede geeft 2. Samen 5. De 1 ligt nog onderaan.

</details>

**Vraag 2**

```csharp
var q = new Queue<char>();
var s = new Stack<char>();

foreach (char c in "abc")
{
    q.Enqueue(c);
    s.Push(c);
}

Console.WriteLine(q.Dequeue());
Console.WriteLine(s.Pop());
```

<details>
<summary><span style="color:#845ef7"><b>Toon het antwoord</b></span></summary>

<code style="color:#845ef7">a</code> en daarna <code style="color:#845ef7">c</code>

Dezelfde drie letters erin, in dezelfde volgorde. De wachtrij geeft de eerste
terug, de stapel de laatste.

</details>

---

# Deel 3 — Zoeken en rekenen

Nu je de verzamelingen kent: hoe haal je er iets uit, en wat krijg je dan eigenlijk terug?

---

## 9. LINQ

<span style="background:#d9822b;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>METHODES</b></span>

Dit is LINQ. Deze methodes werken op elke verzameling — een lijst, een array,
een resultaat van een andere LINQ-methode, en later ook op een databasetabel.
Ze veranderen nooit iets aan de bron; ze geven een nieuw resultaat.

Bijna allemaal willen ze een voorwaarde in de vorm `f => f.Prijs > 50`. Lees de
pijl als "geef mij, voor elke f, het antwoord op".

### Ik wil één item

| Methode | Wat het doet | Als er niets is | Als er meerdere zijn |
|---|---|---|---|
| <code style="color:#228be6;font-weight:600">First</code> | de eerste die voldoet | uitzondering | neemt de eerste |
| <code style="color:#228be6;font-weight:600">FirstOrDefault</code> | de eerste die voldoet | `null` | neemt de eerste |
| <code style="color:#228be6;font-weight:600">Single</code> | de enige die voldoet | uitzondering | uitzondering |
| <code style="color:#228be6;font-weight:600">SingleOrDefault</code> | de enige die voldoet | `null` | uitzondering |
| <code style="color:#228be6;font-weight:600">Last</code> | de laatste die voldoet | uitzondering | neemt de laatste |
| <code style="color:#228be6;font-weight:600">LastOrDefault</code> | de laatste die voldoet | `null` | neemt de laatste |

`fans.FirstOrDefault(f => f.Id == 3)` geeft de fan met Id 3, of `null`.

> [!IMPORTANT]
> Neem `FirstOrDefault` wanneer het normaal is dat er niets gevonden wordt — een
> Id dat niet bestaat, bijvoorbeeld. Dan controleer je op `null` en geef je een
> nette melding.
>
> Neem `SingleOrDefault` wanneer er hoogstens één mág zijn en twee een fout in
> je gegevens betekent. De uitzondering die je dan krijgt is geen hinder maar
> een alarm: er staan twee items met hetzelfde Id in je lijst.
>
> Neem `First` alleen wanneer je zeker weet dat er iets is. Anders crasht je
> API met een `500` op een geval dat eigenlijk een `404` moest zijn.

> [!WARNING]
> `FirstOrDefault` geeft niet altijd `null`. Bij een lijst van getallen geeft
> het `0`, en bij een lijst van waarheidswaarden `false`. De standaardwaarde van
> het type dus, niet noodzakelijk niets. Bij een lijst van objecten is het wel
> `null`.

### Ik wil weten of iets bestaat

| Methode | Wat het doet | Voorbeeld | Resultaat |
|---|---|---|---|
| <code style="color:#228be6;font-weight:600">Any</code> | is er minstens één | `fans.Any()` | <code style="color:#845ef7">true</code> als de lijst niet leeg is |
| <code style="color:#228be6;font-weight:600">Any</code> | met voorwaarde | `fans.Any(f => f.Prijs > 50)` | <code style="color:#845ef7">true</code> of <code style="color:#845ef7">false</code> |
| <code style="color:#228be6;font-weight:600">All</code> | voldoen ze allemaal | `fans.All(f => f.Prijs > 0)` | <code style="color:#845ef7">true</code> of <code style="color:#845ef7">false</code> |
| <code style="color:#228be6;font-weight:600">Contains</code> | zit deze waarde erin | `maten.Contains(120)` | <code style="color:#845ef7">true</code> of <code style="color:#845ef7">false</code> |

Wil je enkel weten óf er iets is, gebruik dan `Any` en niet `Count() > 0`. `Any`
stopt bij het eerste item dat voldoet, `Count` loopt de hele verzameling af.

> [!WARNING]
> `All` op een lege verzameling geeft `true`. Wiskundig klopt dat — er is geen
> enkel item dat níet voldoet — maar het is zelden wat je bedoelde. Controleer
> eerst of er wel iets in zit.

### Ik wil een deel van de verzameling

| Methode | Wat het doet | Voorbeeld | Resultaat |
|---|---|---|---|
| <code style="color:#228be6;font-weight:600">Where</code> | enkel wat voldoet | `fans.Where(f => f.Merk == "NZXT")` | de NZXT-fans |
| <code style="color:#228be6;font-weight:600">Select</code> | maak van elk item iets anders | `fans.Select(f => f.Merk)` | enkel de merken |
| <code style="color:#228be6;font-weight:600">Distinct</code> | gooi dubbels weg | `merken.Distinct()` | elk merk één keer |
| <code style="color:#228be6;font-weight:600">Take</code> | de eerste n | `fans.Take(3)` | drie items |
| <code style="color:#228be6;font-weight:600">Skip</code> | sla de eerste n over | `fans.Skip(3)` | de rest |
| <code style="color:#228be6;font-weight:600">SelectMany</code> | plat een lijst van lijsten | `borden.SelectMany(b => b.Sloten)` | alle sloten samen |

`Where` filtert, `Select` verbouwt. Dat is het hele verschil. Wil je de dure
fans, dan is dat `Where`. Wil je van elke fan enkel het merk, dan is dat
`Select`. Wil je de merken van de dure fans, dan is dat allebei achter elkaar.

### Ik wil tellen of rekenen

| Methode | Wat het doet | Voorbeeld | Resultaat |
|---|---|---|---|
| <code style="color:#228be6;font-weight:600">Count</code> | hoeveel er zijn | `fans.Count()` | <code style="color:#845ef7">4</code> |
| <code style="color:#228be6;font-weight:600">Count</code> | hoeveel er voldoen | `fans.Count(f => f.Prijs > 50)` | <code style="color:#845ef7">2</code> |
| <code style="color:#228be6;font-weight:600">Sum</code> | tel op | `lijnen.Sum(l => l.Aantal * l.StukPrijs)` | het totaal |
| <code style="color:#228be6;font-weight:600">Min</code> | de kleinste waarde | `fans.Min(f => f.Prijs)` | <code style="color:#845ef7">19.99</code> |
| <code style="color:#228be6;font-weight:600">Max</code> | de grootste waarde | `fans.Max(f => f.Prijs)` | <code style="color:#845ef7">89.99</code> |
| <code style="color:#228be6;font-weight:600">Average</code> | het gemiddelde | `fans.Average(f => f.Prijs)` | <code style="color:#845ef7">42.5</code> |
| <code style="color:#228be6;font-weight:600">MinBy</code> | het item met de kleinste waarde | `fans.MinBy(f => f.Prijs)` | de goedkoopste fan |
| <code style="color:#228be6;font-weight:600">MaxBy</code> | het item met de grootste waarde | `fans.MaxBy(f => f.Prijs)` | de duurste fan |

`Max` geeft het getal, `MaxBy` geeft het item. Wil je weten wat de hoogste prijs
is, dan is het `Max`. Wil je wéten wélke fan dat is, dan is het `MaxBy`.

### Ik wil ordenen of groeperen

| Methode | Wat het doet | Voorbeeld | Resultaat |
|---|---|---|---|
| <code style="color:#228be6;font-weight:600">OrderBy</code> | sorteer oplopend | `fans.OrderBy(f => f.Prijs)` | goedkoopste eerst |
| <code style="color:#228be6;font-weight:600">OrderByDescending</code> | sorteer aflopend | `fans.OrderByDescending(f => f.Prijs)` | duurste eerst |
| <code style="color:#228be6;font-weight:600">ThenBy</code> | tweede sorteersleutel | `fans.OrderBy(f => f.Merk).ThenBy(f => f.Prijs)` | per merk, dan op prijs |
| <code style="color:#228be6;font-weight:600">GroupBy</code> | maak groepen | `fans.GroupBy(f => f.Merk)` | een groep per merk |

### Ik wil het resultaat vastleggen

| Methode | Wat het doet | Voorbeeld | Resultaat |
|---|---|---|---|
| <code style="color:#228be6;font-weight:600">ToList</code> | maak er een echte lijst van | `fans.Where(f => f.Prijs > 50).ToList()` | een <code style="color:#845ef7">List</code> |
| <code style="color:#228be6;font-weight:600">ToArray</code> | maak er een array van | `fans.ToArray()` | een array |
| <code style="color:#228be6;font-weight:600">ToDictionary</code> | maak er een opzoektabel van | `fans.ToDictionary(f => f.Id)` | een <code style="color:#845ef7">Dictionary</code> |

> [!IMPORTANT]
> `Where`, `Select` en `OrderBy` voeren op het moment dat je ze schrijft nog niets
> uit. Wat dat betekent, en waarom het je kan verrassen, is het onderwerp van het
> volgende hoofdstuk. De vuistregel voor nu: wat je aan de client van een API
> teruggeeft, geef je pas terug na een `ToList()`.

### Voorspel de uitkomst

**Vraag 1**

```csharp
var getallen = new List<int> { 5, 8, 3, 8 };
Console.WriteLine(getallen.First(g => g > 4));
Console.WriteLine(getallen.Last(g => g > 4));
```

<details>
<summary><span style="color:#845ef7"><b>Toon het antwoord</b></span></summary>

<code style="color:#845ef7">5</code> en daarna <code style="color:#845ef7">8</code>

Groter dan 4 zijn 5, 8 en 8. `First` neemt de eerste daarvan, `Last` de laatste.

</details>

**Vraag 2**

```csharp
var getallen = new List<int> { 5, 8, 3 };
Console.WriteLine(getallen.FirstOrDefault(g => g > 10));
```

<details>
<summary><span style="color:#845ef7"><b>Toon het antwoord</b></span></summary>

<code style="color:#845ef7">0</code>

Er is geen getal groter dan 10. `FirstOrDefault` geeft dan de standaardwaarde van
het type terug, en bij een `int` is dat nul, niet `null`. Pas op: een 0 kan hier
dus zowel "niets gevonden" als "ik vond een nul" betekenen.

</details>

**Vraag 3**

```csharp
var getallen = new List<int> { 5, 8, 3, 8 };
var x = getallen.SingleOrDefault(g => g == 8);
```

<details>
<summary><span style="color:#845ef7"><b>Toon het antwoord</b></span></summary>

<code style="color:#f03e3e">InvalidOperationException</code> — het programma crasht.

Er zijn twee achten. `SingleOrDefault` mag er nul of één vinden, twee is voor hem
een fout. Dat is precies waarvoor je hem gebruikt: als alarm wanneer iets dat uniek
moet zijn het niet is.

</details>

**Vraag 4**

```csharp
var leeg = new List<int>();
Console.WriteLine(leeg.All(g => g > 100));
Console.WriteLine(leeg.Any());
```

<details>
<summary><span style="color:#845ef7"><b>Toon het antwoord</b></span></summary>

<code style="color:#845ef7">True</code> en daarna <code style="color:#845ef7">False</code>

In een lege lijst is er geen enkel getal dat níet groter is dan 100, dus `All`
zegt `true`. Tegelijk zit er niets in, dus `Any` zegt `false`. Twee antwoorden
die elkaar lijken tegen te spreken, en allebei correct.

</details>

**Vraag 5**

```csharp
var namen = new List<string> { "Bram", "Anna", "Cleo" };
var gesorteerd = namen.OrderBy(n => n);
Console.WriteLine(namen[0]);
```

<details>
<summary><span style="color:#845ef7"><b>Toon het antwoord</b></span></summary>

<code style="color:#845ef7">Bram</code>

`OrderBy` laat de oorspronkelijke lijst met rust. Het gesorteerde resultaat zit in
`gesorteerd`, niet in `namen`. Vergelijk met `Sort` in hoofdstuk 4, die de lijst
zelf verandert.

</details>

**Vraag 6**

```csharp
var getallen = new List<int> { 1, 2, 3, 4 };
Console.WriteLine(getallen.Where(g => g > 2).Count());
Console.WriteLine(getallen.Select(g => g > 2).Count());
```

<details>
<summary><span style="color:#845ef7"><b>Toon het antwoord</b></span></summary>

<code style="color:#845ef7">2</code> en daarna <code style="color:#845ef7">4</code>

`Where` houdt enkel 3 en 4 over. `Select` houdt alles over, maar maakt van elk
getal een ja of nee: `false`, `false`, `true`, `true`. Vier antwoorden, dus 4.

</details>

**Vraag 7**

```csharp
var woorden = new List<string> { "kat", "olifant", "muis" };
Console.WriteLine(woorden.Max(w => w.Length));
Console.WriteLine(woorden.MaxBy(w => w.Length));
```

<details>
<summary><span style="color:#845ef7"><b>Toon het antwoord</b></span></summary>

<code style="color:#845ef7">7</code> en daarna <code style="color:#845ef7">olifant</code>

`Max` geeft de grootste lengte. `MaxBy` geeft het woord dat die lengte heeft.

</details>

---

## 10. IEnumerable

<span style="background:#1c7ed6;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>VERZAMELING</b></span>

### Wat het is

**Geen verzameling, maar een belofte: "je kan mij één voor één doorlopen".** Zoals
een playlist: je weet dat je nummer na nummer kan afspelen, maar niet
noodzakelijk hoeveel er zijn of wat het vijfde is.

Elke verzameling uit deel 2 is óók een `IEnumerable`. Een array, een `List`, een
`HashSet` — ze kunnen allemaal doorlopen worden. En alles wat LINQ teruggeeft,
zoals het resultaat van `Where`, is een `IEnumerable`.

### Hoe het eruitziet

```csharp
List<int> getallen = new List<int> { 1, 2, 3, 4 };

IEnumerable<int> even = getallen.Where(g => g % 2 == 0);

getallen.Add(6);

Console.WriteLine(even.Count());    // 3 — de 6 telt mee!
```

Dat laatste verrast iedereen de eerste keer. `even` is geen lijst met 2 en 4. Het
is een vraag — *geef mij de even getallen uit die lijst* — die pas gesteld wordt
op het moment dat je het antwoord nodig hebt. En op dat moment zit de 6 er al in.

### Wanneer wel, wanneer niet

<span style="color:#37b24d"><b>Wel</b></span> — als parameter van een methode die enkel moet doorlopen. Wie die
methode aanroept, mag dan een array, een `List` of een `HashSet` meegeven.

<span style="color:#f03e3e"><b>Niet</b></span> — zodra je `Count`, een positie of `Add` nodig hebt. Maak er dan
eerst met `ToList()` een echte lijst van.

### Veelgemaakte fouten

> [!WARNING]
> - Elke keer dat je een `IEnumerable` doorloopt, wordt de vraag opnieuw gesteld.
>   Twee keer `Count()` op hetzelfde resultaat is twee keer het werk.
> - Verandert de bron tussendoor, dan verandert het antwoord mee.
> - Er is geen `[0]` en geen `.Count` zonder haakjes. Dat zijn dingen van een
>   `List`, niet van een belofte.

### Voorspel de uitkomst

**Vraag 1**

```csharp
var getallen = new List<int> { 1, 2, 3 };
var groot = getallen.Where(g => g > 1);
getallen.Add(10);
Console.WriteLine(groot.Count());
```

<details>
<summary><span style="color:#845ef7"><b>Toon het antwoord</b></span></summary>

<code style="color:#845ef7">3</code>

2, 3 en 10. De vraag werd pas gesteld bij `Count()`, en toen zat de 10 er al in.

</details>

**Vraag 2**

```csharp
var getallen = new List<int> { 1, 2, 3 };
var groot = getallen.Where(g => g > 1).ToList();
getallen.Add(10);
Console.WriteLine(groot.Count);
```

<details>
<summary><span style="color:#845ef7"><b>Toon het antwoord</b></span></summary>

<code style="color:#845ef7">2</code>

Enkel 2 en 3. `ToList()` stelde de vraag meteen en legde het antwoord vast. Wat
er nadien bijkomt, telt niet meer mee. Vergelijk met vraag 1: één woord verschil,
een ander antwoord.

</details>

---

# Deel 4 — Eigen types

Tot hier ging het over wat C# je geeft. Nu over de dingen die je zelf maakt — en over het idee dat bepaalt hoe ze zich gedragen.

---

## 11. class

<span style="background:#2f9e44;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>EIGEN TYPE</b></span>

### Wat het is

**Een bouwplan.** De klasse is het plan, het object is het huis dat ernaar
gebouwd is. Met één plan bouw je zoveel huizen als je wil, en elk huis heeft zijn
eigen bewoners, zijn eigen kleur deur.

### Hoe het eruitziet

```csharp
public class Leerling
{
    public string Naam { get; set; } = "";
    public int Leeftijd { get; set; }

    public bool IsVolwassen()
    {
        return Leeftijd >= 18;
    }
}
```

En zo gebruik je het:

```csharp
Leerling anna = new Leerling { Naam = "Anna", Leeftijd = 17 };

Console.WriteLine(anna.Naam);            // Anna
Console.WriteLine(anna.IsVolwassen());   // False

anna.Leeftijd = 18;                      // aanpassen mag
Console.WriteLine(anna.IsVolwassen());   // True
```

Wil je afdwingen dat een leerling altijd met een naam gemaakt wordt, dan geef
je de klasse een **constructor** — een methode met dezelfde naam als de klasse,
die draait op het moment van `new`:

```csharp
public class Leerling
{
    public string Naam { get; set; }

    public Leerling(string naam)
    {
        Naam = naam;
    }
}

Leerling bram = new Leerling("Bram");
```

### Wanneer wel, wanneer niet

<span style="color:#37b24d"><b>Wel</b></span> — de standaardkeuze. Iets met gegevens die in de loop van de tijd
veranderen, en met gedrag: een leerling die ouder wordt, een bankrekening
waarop je stort.

<span style="color:#f03e3e"><b>Niet</b></span> — wanneer het object enkel gegevens draagt en twee objecten met
dezelfde inhoud als gelijk moeten gelden. Dan is een `record` handiger.

### Veelgemaakte fouten

> [!WARNING]
> - `Leerling b = a;` maakt geen kopie. Het geeft hetzelfde object een tweede
>   naam, net als bij een array.
> - `a == b` zegt of het **hetzelfde** object is, niet of ze **dezelfde inhoud**
>   hebben. Twee leerlingen met dezelfde naam zijn voor een klasse niet gelijk.
> - Een klassevariabele zonder `new` is `null`. Ernaar vragen crasht.

### Voorspel de uitkomst

Deze vragen gebruiken de eerste versie van `Leerling`, zonder constructor.

**Vraag 1**

```csharp
var a = new Leerling { Naam = "Anna" };
var b = a;
b.Naam = "Bram";
Console.WriteLine(a.Naam);
```

<details>
<summary><span style="color:#845ef7"><b>Toon het antwoord</b></span></summary>

<code style="color:#845ef7">Bram</code>

`a` en `b` zijn twee namen voor één en dezelfde leerling. Hernoem je haar via `b`,
dan heet ze ook via `a` anders.

</details>

**Vraag 2**

```csharp
var a = new Leerling { Naam = "Anna" };
var b = new Leerling { Naam = "Anna" };
Console.WriteLine(a == b);
```

<details>
<summary><span style="color:#845ef7"><b>Toon het antwoord</b></span></summary>

<code style="color:#845ef7">False</code>

Twee keer `new` is twee objecten. Dezelfde naam maakt ze niet tot dezelfde
leerling — net zoals twee echte mensen die Anna heten niet dezelfde persoon zijn.

</details>

---

## 12. record

<span style="background:#2f9e44;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>EIGEN TYPE</b></span>

### Wat het is

**Een klasse die draait om haar inhoud.** Twee records met exact dezelfde
gegevens zijn gelijk. Zoals twee briefjes van tien euro: het maakt niet uit welk
exemplaar je hebt, enkel wat erop staat.

### Hoe het eruitziet

```csharp
public record Punt(int X, int Y);
```

Die ene regel maakt een type met twee eigenschappen, een constructor, en een
nette weergave. En zo gebruik je het:

```csharp
var p1 = new Punt(1, 2);
var p2 = new Punt(1, 2);

Console.WriteLine(p1 == p2);     // True — zelfde inhoud, dus gelijk
Console.WriteLine(p1);           // Punt { X = 1, Y = 2 }

var p3 = p1 with { Y = 5 };      // een kopie met één waarde anders
Console.WriteLine(p3);           // Punt { X = 1, Y = 5 }
Console.WriteLine(p1);           // Punt { X = 1, Y = 2 } — ongewijzigd
```

### Wanneer wel, wanneer niet

<span style="color:#37b24d"><b>Wel</b></span> — een pakketje gegevens dat je doorgeeft en niet meer verandert. In
les 09 van Programming Advanced kom je records tegen als een van de twee manieren
om DTO's te schrijven.

<span style="color:#f03e3e"><b>Niet</b></span> — wanneer het object in de loop van de tijd verandert en zijn eigen
identiteit heeft. Een leerling die ouder wordt blijft dezelfde leerling; dat is
een `class`.

### Veelgemaakte fouten

> [!WARNING]
> - De eigenschappen van `record Punt(int X, int Y)` kan je na het aanmaken niet
>   meer aanpassen. `p1.X = 5;` compileert niet.
> - `with` verandert niets aan het origineel. Het maakt een nieuw record. Wie het
>   resultaat niet opvangt, heeft niets gedaan.

### Voorspel de uitkomst

**Vraag 1**

```csharp
var p1 = new Punt(1, 2);
var p2 = p1 with { X = 9 };
Console.WriteLine(p1.X);
```

<details>
<summary><span style="color:#845ef7"><b>Toon het antwoord</b></span></summary>

<code style="color:#845ef7">1</code>

`with` maakte een kopie waarin `X` 9 is. Het origineel bleef zoals het was.

</details>

**Vraag 2**

```csharp
var p1 = new Punt(1, 2);
p1.X = 5;
```

<details>
<summary><span style="color:#845ef7"><b>Toon het antwoord</b></span></summary>

<code style="color:#f03e3e">Compileert niet.</code>

Bij deze korte schrijfwijze kan je de eigenschappen enkel invullen bij het
aanmaken. Wil je een ander punt, maak dan een nieuw — met `with` of met `new`.

</details>

**Vraag 3**

```csharp
Console.WriteLine(new Punt(3, 4));
```

<details>
<summary><span style="color:#845ef7"><b>Toon het antwoord</b></span></summary>

<code style="color:#845ef7">Punt { X = 3, Y = 4 }</code>

Een record weet uit zichzelf hoe het zich moet tonen. Een gewone klasse toont
enkel haar naam — dat is een van de redenen waarom records zo handig zijn om mee
te testen.

</details>

---

## 13. struct

<span style="background:#2f9e44;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>EIGEN TYPE</b></span>

### Wat het is

**Een klein pakketje waarden dat bij kopiëren écht gekopieerd wordt.** Zoals een
fotokopie: wat je op de kopie schrijft, staat niet op het origineel.

Je gebruikt ze al zonder het te weten. Een `int`, een `double` en een `bool` zijn
allemaal structs.

### Hoe het eruitziet

```csharp
public struct Kleur
{
    public int R;
    public int G;
    public int B;
}

Kleur a = new Kleur { R = 255, G = 0, B = 0 };
Kleur b = a;         // een echte kopie
b.R = 0;

Console.WriteLine(a.R);   // 255 — a is niet veranderd
Console.WriteLine(b.R);   // 0
```

Vergelijk dit met vraag 1 in hoofdstuk 11. Dezelfde drie regels, een tegengesteld
resultaat.

### Wanneer wel, wanneer niet

<span style="color:#37b24d"><b>Wel</b></span> — zelden zelf nodig. Een klein ding van een paar getallen dat zich
gedraagt als één waarde: een kleur, een coördinaat.

<span style="color:#f03e3e"><b>Niet</b></span> — bijna altijd. Twijfel je, neem een `class` of een `record`.

### Veelgemaakte fouten

> [!WARNING]
> - Een struct in een lijst aanpassen past de kopie aan, niet wat in de lijst
>   staat. Dat is de klassieke valkuil, en de reden waarom je ze best klein en
>   onveranderlijk houdt.

### Voorspel de uitkomst

**Vraag 1**

```csharp
int x = 5;
int y = x;
y = 10;
Console.WriteLine(x);
```

<details>
<summary><span style="color:#845ef7"><b>Toon het antwoord</b></span></summary>

<code style="color:#845ef7">5</code>

Een `int` is een struct. `y = x` kopieerde de waarde 5. Wat je daarna met `y`
doet, raakt `x` niet. Dit gedrag ken je al sinds je eerste les — het heeft nu
gewoon een naam.

</details>

---

## 14. Referentie tegenover waarde

<span style="background:#7048e8;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>CONCEPT</b></span>

Dit is het belangrijkste idee uit dit hele leerboek. Als je één ding onthoudt,
laat het dit zijn.

Er zijn twee soorten types in C#, en ze gedragen zich tegengesteld wanneer je ze
aan een tweede variabele geeft.

| | Referentietype | Waardetype |
|---|---|---|
| **Voorbeelden** | `class`, `record`, array, `List`, `Dictionary`, alle verzamelingen | `int`, `double`, `bool`, `struct`, `enum` |
| **`b = a` doet** | geeft hetzelfde ding een tweede naam | maakt een echte kopie |
| **Via `b` iets aanpassen** | zie je ook via `a` | raakt `a` niet |
| **Vergelijking** | een huissleutel: twee sleutels, één huis | een fotokopie: twee vellen, elk apart |
| **Kan `null` zijn** | ja | nee, tenzij je `?` achter het type zet |

> [!NOTE]
> `string` is technisch een referentietype, maar gedraagt zich als een waarde: je
> kan een tekst nooit aanpassen, enkel vervangen door een nieuwe. Daarom verrast
> hij je nooit op de manier waarop een lijst dat doet.

Test jezelf: kijk terug naar vraag 4 in hoofdstuk 3, vraag 1 in hoofdstuk 11 en
de vraag in hoofdstuk 13. Drie keer dezelfde drie regels, en het antwoord hangt
enkel af van de soort type. De vraag onderaan dit hoofdstuk brengt het samen.

### class, record en struct naast elkaar

| | `class` | `record` | `struct` |
|---|---|---|---|
| **Soort** | referentie | referentie | waarde |
| **`a == b` vergelijkt** | is het hetzelfde object | is de inhoud gelijk | is de inhoud gelijk |
| **Aanpassen na het maken** | ja | met de korte schrijfwijze: nee | ja, maar liever niet |
| **Zichzelf tonen** | enkel de naam van het type | alle eigenschappen | enkel de naam van het type |
| **Typisch gebruik** | iets met gedrag en een identiteit | een pakketje gegevens | een klein getalachtig ding |

### Voorspel de uitkomst

**Vraag 1**

Dit is de vraag die alles samenbrengt. Twee methodes: de ene krijgt een lijst mee,
de andere een getal.

```csharp
void VoegToe(List<int> lijst)
{
    lijst.Add(99);
}

void Verhoog(int getal)
{
    getal = getal + 1;
}

var l = new List<int> { 1 };
int n = 1;

VoegToe(l);
Verhoog(n);

Console.WriteLine(l.Count);
Console.WriteLine(n);
```

<details>
<summary><span style="color:#845ef7"><b>Toon het antwoord</b></span></summary>

<code style="color:#845ef7">2</code> en daarna <code style="color:#845ef7">1</code>

Een lijst is een referentietype. `VoegToe` krijgt geen kopie van de lijst, maar een
tweede sleutel tot dezelfde lijst. Wat de methode erin stopt, zit er daarna nog in.

Een `int` is een waardetype. `Verhoog` krijgt een fotokopie van het getal. Het
verhoogt die kopie, en de kopie verdwijnt zodra de methode klaar is. `n` blijft 1.

Dit is de reden waarom een lijst die je aan een methode meegeeft na afloop soms
"vanzelf" veranderd is.

</details>

---

## 15. Omgaan met niets

<span style="background:#7048e8;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>CONCEPT</b></span>

In het vorige hoofdstuk zag je dat een referentietype `null` kan zijn en een
waardetype niet — tenzij je er een vraagteken achter zet. Dit hoofdstuk gaat
over hoe je daar veilig mee omgaat.

| Vorm | Wat het doet | Voorbeeld | Resultaat |
|---|---|---|---|
| <code style="color:#228be6;font-weight:600">??</code> | neem dit, tenzij het null is | `naam ?? "onbekend"` | <code style="color:#845ef7">"onbekend"</code> als <code style="color:#845ef7">naam</code> null is |
| <code style="color:#228be6;font-weight:600">??=</code> | zet enkel als het nog null is | `_repo ??= new Repository()` | vult één keer in |
| <code style="color:#228be6;font-weight:600">?.</code> | roep enkel aan als het niet null is | `fan?.Merk` | <code style="color:#845ef7">null</code> in plaats van een crash |
| <code style="color:#228be6;font-weight:600">is null</code> | is het niets | `if (fan is null)` | <code style="color:#845ef7">true</code> of <code style="color:#845ef7">false</code> |
| <code style="color:#228be6;font-weight:600">is not null</code> | is het iets | `if (fan is not null)` | <code style="color:#845ef7">true</code> of <code style="color:#845ef7">false</code> |
| <code style="color:#228be6;font-weight:600">int?</code> | een getal dat ook niets mag zijn | `int? hoogte = null` | mag leeg blijven |
| <code style="color:#228be6;font-weight:600">HasValue</code> | zit er een waarde in | `hoogte.HasValue` | <code style="color:#845ef7">false</code> |
| <code style="color:#228be6;font-weight:600">GetValueOrDefault</code> | de waarde, of een terugval | `hoogte.GetValueOrDefault(0)` | <code style="color:#845ef7">0</code> |

Het vraagteken achter een type betekent: deze waarde mag ontbreken. Dat is de
manier waarop je in code het verschil vastlegt tussen "nul" en "niet ingevuld" —
precies het onderscheid waar een controle op stuk loopt als je het niet maakt.

### Voorspel de uitkomst

**Vraag 1**

```csharp
string? naam = null;
Console.WriteLine(naam ?? "onbekend");
```

<details>
<summary><span style="color:#845ef7"><b>Toon het antwoord</b></span></summary>

<code style="color:#845ef7">onbekend</code>

`naam` is null, dus `??` neemt wat rechts staat.

</details>

**Vraag 2**

```csharp
string? naam = null;
Console.WriteLine(naam?.Length);
```

<details>
<summary><span style="color:#845ef7"><b>Toon het antwoord</b></span></summary>

Een lege regel.

`naam?.Length` vraagt de lengte enkel op als `naam` niet null is. Anders is het
resultaat zelf null, en `Console.WriteLine` toont null als niets. Geen crash.

</details>

**Vraag 3**

```csharp
string? naam = null;
Console.WriteLine(naam.Length);
```

<details>
<summary><span style="color:#845ef7"><b>Toon het antwoord</b></span></summary>

<code style="color:#f03e3e">NullReferenceException</code> — het programma crasht.

Het enige verschil met vraag 2 is het vraagteken. Zonder `?.` vraag je de lengte op
van iets dat er niet is. Visual Studio waarschuwt je hier wel met een groene
kronkellijn, maar compileren gaat.

</details>

**Vraag 4**

```csharp
int? score = null;
Console.WriteLine(score.HasValue);
Console.WriteLine(score.GetValueOrDefault());
```

<details>
<summary><span style="color:#845ef7"><b>Toon het antwoord</b></span></summary>

<code style="color:#845ef7">False</code> en daarna <code style="color:#845ef7">0</code>

Er zit geen waarde in. `GetValueOrDefault` geeft dan de standaardwaarde van een
`int`: nul. Merk op dat je nu wél kan zien dat de score ontbrak, via `HasValue`.
Bij een gewone `int` kon dat niet.

</details>

**Vraag 5**

```csharp
string? a = null;
a ??= "eerste";
a ??= "tweede";
Console.WriteLine(a);
```

<details>
<summary><span style="color:#845ef7"><b>Toon het antwoord</b></span></summary>

<code style="color:#845ef7">eerste</code>

`??=` vult enkel in als er nog niets staat. De eerste keer was `a` null, de tweede
keer niet meer.

</details>

---

## 16. enum

<span style="background:#2f9e44;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>EIGEN TYPE</b></span>

### Wat het is

**Een vaste lijst benoemde keuzes.** Een verkeerslicht staat op rood, oranje of
groen. Niet op paars, niet op "roood" met drie o's.

### Hoe het eruitziet

```csharp
public enum Verkeerslicht
{
    Rood,
    Oranje,
    Groen
}
```

En zo gebruik je het:

```csharp
Verkeerslicht licht = Verkeerslicht.Rood;

if (licht == Verkeerslicht.Rood)
{
    Console.WriteLine("Stop");
}

switch (licht)
{
    case Verkeerslicht.Rood:
        Console.WriteLine("Wachten");
        break;
    case Verkeerslicht.Groen:
        Console.WriteLine("Rijden");
        break;
}

Console.WriteLine(licht);           // Rood
Console.WriteLine((int)licht);      // 0
```

Tekst omzetten naar een enum gaat met `Enum.TryParse`, net als bij getallen:

```csharp
if (Enum.TryParse("Groen", out Verkeerslicht gekozen))
{
    Console.WriteLine(gekozen);     // Groen
}
```

### Wanneer wel, wanneer niet

<span style="color:#37b24d"><b>Wel</b></span> — een eigenschap heeft een klein, vast aantal mogelijke waarden die
niet veranderen terwijl je programma draait: dagen van de week, een status, een
richting. Het grote voordeel tegenover een `string`: een tikfout wordt een
compileerfout in plaats van een fout die pas opduikt als het te laat is.

<span style="color:#f03e3e"><b>Niet</b></span> — wanneer de lijst van keuzes groeit terwijl het programma draait,
of door gebruikers aangevuld wordt. Een enum wijzig je enkel door je code te
herschrijven.

### Veelgemaakte fouten

> [!WARNING]
> - Onder de motorkap is elke keuze een getal: `Rood` is 0, `Oranje` 1, `Groen` 2.
>   Voeg je vooraan een keuze toe, dan schuiven alle getallen op. Staan die
>   getallen ergens opgeslagen, dan betekenen ze plots iets anders.
> - `(Verkeerslicht)7` compileert, ook al bestaat er geen zevende kleur.
> - `Enum.TryParse` is standaard hoofdlettergevoelig.

### Voorspel de uitkomst

**Vraag 1**

```csharp
Console.WriteLine((int)Verkeerslicht.Groen);
```

<details>
<summary><span style="color:#845ef7"><b>Toon het antwoord</b></span></summary>

<code style="color:#845ef7">2</code>

Tellen begint bij nul: Rood is 0, Oranje 1, Groen 2.

</details>

**Vraag 2**

```csharp
Verkeerslicht l = (Verkeerslicht)7;
Console.WriteLine(l);
```

<details>
<summary><span style="color:#845ef7"><b>Toon het antwoord</b></span></summary>

<code style="color:#845ef7">7</code>

C# laat het toe, en omdat er geen naam bij 7 hoort, toont het gewoon het getal.
Een enum beschermt je dus tegen tikfouten in je code, maar niet tegen elk getal
dat van buiten binnenkomt.

</details>

**Vraag 3**

```csharp
bool ok = Enum.TryParse("groen", out Verkeerslicht l);
Console.WriteLine(ok);
```

<details>
<summary><span style="color:#845ef7"><b>Toon het antwoord</b></span></summary>

<code style="color:#845ef7">False</code>

`"groen"` met kleine letter is niet `Groen`. Wil je dat het wel lukt, dan geef je
een extra `true` mee om hoofdletters te negeren.

</details>

---

## 17. interface

<span style="background:#2f9e44;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>EIGEN TYPE</b></span>

### Wat het is

**Een contract.** Het zegt wát iets moet kunnen, niet hóe. Zoals een
stopcontact: elk toestel met de juiste stekker past erin, of het nu een lamp is
of een waterkoker. Het stopcontact hoeft niet te weten wat je aansluit.

### Hoe het eruitziet

```csharp
public interface IGeluid
{
    string MaakGeluid();
}
```

Het contract zegt enkel: *wie mij belooft, kan een geluid maken*. Geen
uitwerking. Die komt in de klassen die de belofte doen:

```csharp
public class Hond : IGeluid
{
    public string MaakGeluid()
    {
        return "Woef";
    }
}

public class Kat : IGeluid
{
    public string MaakGeluid()
    {
        return "Miauw";
    }
}
```

En nu het nut ervan:

```csharp
List<IGeluid> dieren = new List<IGeluid> { new Hond(), new Kat() };

foreach (IGeluid dier in dieren)
{
    Console.WriteLine(dier.MaakGeluid());
}
```

De lus weet niet of hij een hond of een kat voor zich heeft. Dat hoeft ook niet:
hij weet dat ze allebei een geluid kunnen maken, en meer heeft hij niet nodig.

### Waarom dit in je cursus zo belangrijk is

In les 03 van Programming Advanced maak je een repository met een interface
ervoor. Dat is precies dit principe. De controller praat met het contract, niet
met de klasse. Daardoor kan de klasse achter het contract later vervangen worden
— een lijst in het geheugen wordt een database — zonder dat de controller één
letter verandert.

### Wanneer wel, wanneer niet

<span style="color:#37b24d"><b>Wel</b></span> — wanneer verschillende klassen hetzelfde moeten kunnen, of wanneer je
wil dat een deel van je programma later vervangbaar is zonder de rest aan te
passen.

<span style="color:#f03e3e"><b>Niet</b></span> — voor een klasse waarvan er maar één soort ooit zal bestaan en die
je nooit zal vervangen. Een contract met maar één ondertekenaar voegt weinig toe.

### Veelgemaakte fouten

> [!WARNING]
> - Van een interface kan je geen object maken. `new IGeluid()` compileert niet —
>   je kan geen stopcontact aansluiten op een ander stopcontact.
> - Belooft een klasse het contract, dan moet ze **alles** uitwerken wat erin
>   staat. Eén methode vergeten en het compileert niet.
> - Volgens de afspraak begint de naam met een hoofdletter `I`.

### Voorspel de uitkomst

**Vraag 1**

Wat verschijnt er op het scherm bij de lus hierboven?

<details>
<summary><span style="color:#845ef7"><b>Toon het antwoord</b></span></summary>

<code style="color:#845ef7">Woef</code> en daarna <code style="color:#845ef7">Miauw</code>

Elke klasse gebruikt haar eigen uitwerking, ook al weet de lus enkel dat ze
`IGeluid` beloven.

</details>

**Vraag 2**

```csharp
IGeluid g = new IGeluid();
```

<details>
<summary><span style="color:#845ef7"><b>Toon het antwoord</b></span></summary>

<code style="color:#f03e3e">Compileert niet.</code>

Een interface is een belofte zonder uitwerking. Er valt niets te bouwen.

</details>

**Vraag 3**

```csharp
IGeluid g = new Kat();
Console.WriteLine(g is Kat);
```

<details>
<summary><span style="color:#845ef7"><b>Toon het antwoord</b></span></summary>

<code style="color:#845ef7">True</code>

De variabele zegt enkel "iets dat een geluid maakt", maar het object dat erin
zit is nog altijd een kat. Met `is` kan je dat nagaan.

</details>

---

## 18. Types herkennen

<span style="background:#d9822b;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>METHODES</b></span>

De voorbeelden gebruiken `IGeluid`, `Hond` en `Kat` uit het vorige hoofdstuk.

| Vorm | Wat het doet | Voorbeeld | Resultaat |
|---|---|---|---|
| <code style="color:#228be6;font-weight:600">typeof</code> | het type zelf, als je de naam kent | `typeof(Kat)` | het type |
| <code style="color:#228be6;font-weight:600">GetType</code> | het type van een bestaand object | `dier.GetType()` | het type |
| <code style="color:#228be6;font-weight:600">is</code> | is dit object van dat type | `if (dier is Kat)` | <code style="color:#845ef7">true</code> of <code style="color:#845ef7">false</code> |
| <code style="color:#228be6;font-weight:600">is</code> met naam | idem, en geef het meteen die naam | `if (dier is Kat k)` | <code style="color:#845ef7">k</code> is bruikbaar |
| <code style="color:#228be6;font-weight:600">as</code> | behandel als dat type, of `null` | `dier as Kat` | het object of <code style="color:#845ef7">null</code> |
| <code style="color:#228be6;font-weight:600">nameof</code> | de naam van iets als tekst | `nameof(GetFanById)` | <code style="color:#845ef7">"GetFanById"</code> |

`nameof` lijkt onnozel tot je het nodig hebt. Het voordeel tegenover `"GetFanById"`
tussen aanhalingstekens is dat je code niet meer compileert wanneer je die
methode hernoemt. Een typfout in een naam vind je zo bij het bouwen in plaats
van bij het testen.

### Voorspel de uitkomst

**Vraag 1**

```csharp
IGeluid dier = new Hond();
Console.WriteLine(dier is Kat);
```

<details>
<summary><span style="color:#845ef7"><b>Toon het antwoord</b></span></summary>

<code style="color:#845ef7">False</code>

De variabele zegt enkel "iets dat een geluid maakt". Wat erin zit, is een hond.

</details>

**Vraag 2**

```csharp
IGeluid dier = new Hond();
Kat? k = dier as Kat;
Console.WriteLine(k is null);
```

<details>
<summary><span style="color:#845ef7"><b>Toon het antwoord</b></span></summary>

<code style="color:#845ef7">True</code>

`as` probeert het dier als kat te behandelen. Lukt dat niet, dan krijg je null in
plaats van een crash. Daarom controleer je na een `as` altijd op null.

</details>

**Vraag 3**

```csharp
Console.WriteLine(nameof(Kat));
Console.WriteLine(typeof(Kat).Name);
```

<details>
<summary><span style="color:#845ef7"><b>Toon het antwoord</b></span></summary>

<code style="color:#845ef7">Kat</code> en daarna <code style="color:#845ef7">Kat</code>

Twee wegen naar dezelfde tekst. `nameof` wordt al bij het compileren ingevuld,
`typeof(...).Name` pas als het programma draait. Voor een naam in een melding of
in `CreatedAtAction` is `nameof` de gewoonte.

</details>

---

# Deel 5 — In je API

Wat je nodig hebt zodra je van een consoletoepassing naar een API overstapt.

---

## 19. De helpers van een controller

<span style="background:#0c8599;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>IN JE API</b></span>

Deze erf je van `ControllerBase`. Ze maken een antwoord met de juiste
statuscode.

| Methode | Statuscode | Voorbeeld |
|---|---|---|
| <code style="color:#228be6;font-weight:600">Ok</code> | <code style="color:#37b24d;font-weight:600">200 OK</code> | `Ok(fans)` |
| <code style="color:#228be6;font-weight:600">CreatedAtAction</code> | <code style="color:#37b24d;font-weight:600">201 Created</code> | `CreatedAtAction(nameof(GetFanById), new { id = fan.Id }, fan)` |
| <code style="color:#228be6;font-weight:600">NoContent</code> | <code style="color:#37b24d;font-weight:600">204 No Content</code> | `NoContent()` |
| <code style="color:#228be6;font-weight:600">BadRequest</code> | <code style="color:#f08c00;font-weight:600">400 Bad Request</code> | `BadRequest("De prijs moet groter zijn dan 0.")` |
| <code style="color:#228be6;font-weight:600">NotFound</code> | <code style="color:#f08c00;font-weight:600">404 Not Found</code> | <code>NotFound(&#36;"Geen fan met Id {id}.")</code> |
| <code style="color:#228be6;font-weight:600">StatusCode</code> | wat je zelf opgeeft | `StatusCode(500)` |

Ze nemen allemaal een tekst of een object mee, en dat is wat de client in de
body ziet. Een `NotFound()` zonder tekst is geldig maar zegt niets; een
`NotFound()` met een zin erbij scheelt de volgende persoon tien minuten zoeken.

### Voorspel de uitkomst

Bij een API voorspel je geen tekst op het scherm, maar wat de client terugkrijgt:
een statuscode en een body.

**Vraag 1**

Een methode eindigt met:

```csharp
return NotFound("Onbekend");
```

<details>
<summary><span style="color:#845ef7"><b>Toon het antwoord</b></span></summary>

<code style="color:#f08c00;font-weight:600">404 Not Found</code>, met in de body de tekst <code style="color:#845ef7">Onbekend</code>.

</details>

**Vraag 2**

Na een geslaagde DELETE eindigt de methode met:

```csharp
return NoContent();
```

<details>
<summary><span style="color:#845ef7"><b>Toon het antwoord</b></span></summary>

<code style="color:#37b24d;font-weight:600">204 No Content</code>, met een lege body.

Dat is geen vergetelheid. Er valt na een verwijdering niets meer te tonen, en de
statuscode zegt al dat het gelukt is.

</details>

**Vraag 3**

Een methode zoekt met `First` en het gevraagde Id bestaat niet:

```csharp
var dier = dieren.First(d => d.Id == id);
return Ok(dier);
```

<details>
<summary><span style="color:#845ef7"><b>Toon het antwoord</b></span></summary>

<code style="color:#f03e3e;font-weight:600">500 Internal Server Error</code>

`First` crasht zodra er niets gevonden wordt, en die crash wordt nergens
opgevangen. De server meldt dan een fout aan zijn kant — terwijl de fout eigenlijk
bij de client lag, die een Id vroeg dat niet bestaat. Met `FirstOrDefault`, een
controle op null en een `NotFound` was dit een nette 404 geworden. Kijk nog eens
naar hoofdstuk 9.

</details>

---

## 20. Asynchroon werken

<span style="background:#0c8599;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>IN JE API</b></span>

| Vorm | Wat het doet |
|---|---|
| <code style="color:#228be6;font-weight:600">async</code> | markeert een methode die mag wachten |
| <code style="color:#228be6;font-weight:600">await</code> | wacht op het resultaat zonder de server te blokkeren |
| <code style="color:#228be6;font-weight:600">Task</code> | een belofte dat er ooit iets klaar is |
| <code style="color:#228be6;font-weight:600">Task<T></code> | een belofte dat er ooit een `T` klaar is |

De asynchrone tegenhangers die je in de latere lessen tegenkomt dragen dezelfde
naam met `Async` erachter: `ToListAsync`, `FirstOrDefaultAsync`, `FindAsync`,
`SaveChangesAsync`, `AnyAsync`, `CountAsync`. Ze doen hetzelfde als hun gewone
versie; het verschil zit in wat de server intussen mag doen.

> [!NOTE]
> Deze komen pas aan bod in les 05 van Programming Advanced. Ze staan hier
> zodat je ze herkent wanneer je ze in een voorbeeld ziet staan, niet om ze nu
> al te gebruiken.

### Voorspel de uitkomst

Stel dat er een methode bestaat met deze kop:

```csharp
Task<int> TelAsync()
```

**Vraag 1**

```csharp
var x = await TelAsync();
```

Van welk type is `x`?

<details>
<summary><span style="color:#845ef7"><b>Toon het antwoord</b></span></summary>

<code style="color:#845ef7">int</code>

`await` wacht tot de belofte ingelost is en geeft je wat erin zit: het getal.

</details>

**Vraag 2**

```csharp
var x = TelAsync();
```

Van welk type is `x` nu?

<details>
<summary><span style="color:#845ef7"><b>Toon het antwoord</b></span></summary>

<code style="color:#845ef7">Task&lt;int&gt;</code>

Zonder `await` krijg je de belofte zelf, niet het getal. Probeer je er daarna mee te
rekenen, dan compileert het niet. Een vergeten `await` is een van de meest
voorkomende fouten zodra je asynchroon begint te werken.

</details>

---

## 21. JSON en bestanden

<span style="background:#0c8599;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>IN JE API</b></span>

| Methode | Wat het doet |
|---|---|
| <code style="color:#228be6;font-weight:600">JsonSerializer.Serialize</code> | maak van een object een JSON-tekst |
| <code style="color:#228be6;font-weight:600">JsonSerializer.Deserialize</code> | maak van een JSON-tekst een object |
| <code style="color:#228be6;font-weight:600">File.ReadAllText</code> | lees een heel bestand als tekst |
| <code style="color:#228be6;font-weight:600">File.WriteAllText</code> | schrijf een tekst naar een bestand |
| <code style="color:#228be6;font-weight:600">File.Exists</code> | bestaat dat bestand |
| <code style="color:#228be6;font-weight:600">Path.Combine</code> | plak padstukken correct aan elkaar |

> [!WARNING]
> `JsonSerializer.Deserialize` is standaard hoofdlettergevoelig. Staat er
> `"krakenId"` in je bestand en heet je eigenschap `KrakenId`, dan blijft die
> leeg zonder één foutmelding. Dat gedrag zet je om met
> `PropertyNameCaseInsensitive`. Dat ASP.NET Core zelf wél soepel is met
> hoofdletters geldt enkel voor verzoeken die het framework zelf uitleest, niet
> voor een bestand dat je zelf inleest.

### Voorspel de uitkomst

**Vraag 1**

```csharp
var p = new { Naam = "Anna", Leeftijd = 17 };
Console.WriteLine(JsonSerializer.Serialize(p));
```

<details>
<summary><span style="color:#845ef7"><b>Toon het antwoord</b></span></summary>

<code style="color:#845ef7">{"Naam":"Anna","Leeftijd":17}</code>

Alles op één regel, en de namen precies zoals in je code, met hoofdletter. Dat een
API zijn JSON met kleine letters teruggeeft, komt doordat ASP.NET Core dat voor jou
instelt; `JsonSerializer` zelf doet het niet.

</details>

**Vraag 2**

```csharp
public class Persoon
{
    public string Naam { get; set; } = "";
}

var p = JsonSerializer.Deserialize<Persoon>("{\"naam\":\"Anna\"}");
Console.WriteLine("[" + p!.Naam + "]");
```

<details>
<summary><span style="color:#845ef7"><b>Toon het antwoord</b></span></summary>

<code style="color:#845ef7">[]</code>

In de JSON staat `naam` met kleine letter, in de klasse `Naam` met hoofdletter.
Standaard is dat voor `JsonSerializer` een andere naam, dus `Naam` blijft leeg. Er
komt geen enkele foutmelding. Met `PropertyNameCaseInsensitive = true` in de opties
lost je dit op.

</details>

---

# Veelgemaakte verwarringen

Vijftien paren die op elkaar lijken en iets anders doen. Dit is het deel waar je
het vaakst naar terug zal bladeren.

## `Length`, `Count` en `Count()`

Een array en een tekst zeggen `Length`. Een `List` zegt `Count`, zonder haakjes,
en weet het meteen. `Count()` met haakjes is LINQ: dat werkt op elke verzameling,
maar loopt ze daarvoor helemaal af. Op een lijst neem je de eigenschap.
Hoofdstukken [3](#3-array), [4](#4-list) en [9](#9-linq).

## Array tegenover List

Een array heeft een vaste lengte, een `List` groeit mee. In de praktijk kies je
bijna altijd de `List`, tenzij de hoeveelheid echt vastligt.
Hoofdstukken [3](#3-array) en [4](#4-list).

## List tegenover Dictionary

Zoek je in een `List` telkens met `Find` of `FirstOrDefault` op dezelfde
eigenschap — een naam, een code — dan wil je eigenlijk een `Dictionary` met die
eigenschap als sleutel. De `List` kijkt elk item na tot hij het vindt, de
`Dictionary` weet meteen waar hij moet zijn. Zoek je afwisselend op verschillende
eigenschappen, of doet de volgorde ertoe, dan blijft de `List` de betere keuze.
Hoofdstukken [4](#4-list) en [5](#5-dictionary).

## Queue tegenover Stack

Allebei laten ze je enkel aan één kant bij. Stel jezelf de vraag: *wie komt er als
volgende aan de beurt?* Wie het langst wacht — `Queue`. Wie het laatst kwam —
`Stack`. Hoofdstukken [7](#7-queue) en [8](#8-stack).

## `Remove`, `RemoveAt` en `RemoveAll`

`Remove` wil het item, `RemoveAt` wil een positie, `RemoveAll` wil een
voorwaarde. Ze geven ook iets anders terug: `Remove` een ja of nee, `RemoveAll`
het aantal dat weg is, `RemoveAt` niets. En `Remove` haalt enkel het eerste
voorkomen weg. Hoofdstuk [4](#4-list).

## `Parse` tegenover `TryParse`

`Parse` gelooft je en crasht als je ongelijk hebt. `TryParse` vraagt het na en
geeft `false`. Alles wat van buiten komt, gaat door `TryParse`.
Hoofdstuk [2](#2-getallen-en-omzetten).

## `==` tegenover `Equals` bij tekst

Allebei vergelijken ze letterlijk, hoofdletters inbegrepen. Het verschil is dat
`Equals` een tweede argument aanvaardt waarmee je die regel kan versoepelen.
Zodra hoofdletters er niet toe mogen doen, heb je `Equals` nodig.
Hoofdstuk [1](#1-tekst).

## `Find` tegenover `FirstOrDefault`

Op een lijst doen ze hetzelfde. `Find` bestaat enkel op een `List` en een array;
`FirstOrDefault` werkt op elke verzameling, ook op een databasequery. Omdat je
later met databases werkt en `Find` daar niet bestaat, went `FirstOrDefault`
beter aan. Hoofdstukken [4](#4-list) en [9](#9-linq).

## `First` tegenover `Single`

`First` zegt: er is er minstens één, geef mij de eerste. `Single` zegt: er is er
precies één, en als dat niet klopt is er iets grondig mis. Gebruik `Single` op een
Id, en `First` op een sortering. Hoofdstuk [9](#9-linq).

## `Where` tegenover `Select`

`Where` houdt items over, `Select` verbouwt ze. Krijg je mínder items terug dan je
erin stak, dan heb je gefilterd — dat is `Where`. Krijg je er evenveel terug maar
van een ander soort, bijvoorbeeld enkel de namen, dan heb je verbouwd — dat is
`Select`. Hoofdstuk [9](#9-linq).

## `Max` tegenover `MaxBy`

`Max` geeft de waarde, `MaxBy` geeft het item dat die waarde draagt. Dezelfde
verhouding geldt voor `Min` en `MinBy`. Hoofdstuk [9](#9-linq).

## `Any()` tegenover `Count() > 0`

Hetzelfde antwoord, maar `Any` stopt bij het eerste item dat voldoet. Op een lijst
van vijf maakt dat niets uit; op een databasetabel van vijftigduizend wel.
Hoofdstuk [9](#9-linq).

## Wat een methode teruggeeft, en wat ze verandert

`lijst.Sort()` verandert je lijst en geeft niets terug. `lijst.OrderBy(...)` laat
je lijst met rust en geeft een nieuw resultaat. Hetzelfde geldt voor `Trim` en
`ToUpper` bij tekst: die veranderen niets, ze geven een nieuwe tekst terug.
Compileert je regel niet, of lijkt hij niets te doen, dan heb je meestal de
verkeerde van de twee te pakken. Hoofdstukken [1](#1-tekst), [4](#4-list) en
[9](#9-linq).

## `IEnumerable` tegenover `List`

Een `IEnumerable` is een vraag die nog gesteld moet worden. Een `List` is een
antwoord dat al klaarligt. `Where` voert niets uit tot je het resultaat doorloopt;
verandert je bron intussen, dan verandert je antwoord mee. `ToList()` stelt de
vraag meteen en legt het antwoord vast. Hoofdstuk [10](#10-ienumerable).

## `-1`, `null` en `0`

`IndexOf` geeft `-1` wanneer er niets gevonden is. `FirstOrDefault` geeft `null`
bij objecten, maar `0` bij getallen. `TryGetValue` zet zijn uitkomst op de
standaardwaarde. Drie manieren om "niets" te zeggen, en elk vraagt een andere
controle. Hoofdstukken [1](#1-tekst), [5](#5-dictionary), [9](#9-linq) en
[15](#15-omgaan-met-niets).

---

# Index

Alfabetisch, met het hoofdstuk waar de uitleg staat.

| Naam | Hoofdstuk |
|---|---|
| <code style="color:#228be6;font-weight:600">Add</code> (dictionary) | [5](#5-dictionary) |
| <code style="color:#228be6;font-weight:600">Add</code> (lijst) | [4](#4-list) |
| <code style="color:#228be6;font-weight:600">AddRange</code> | [4](#4-list) |
| <code style="color:#228be6;font-weight:600">All</code> | [9](#9-linq) |
| <code style="color:#228be6;font-weight:600">Any</code> | [9](#9-linq) |
| array | [3](#3-array) |
| <code style="color:#228be6;font-weight:600">as</code> | [18](#18-types-herkennen) |
| <code style="color:#228be6;font-weight:600">async</code> | [20](#20-asynchroon-werken) |
| <code style="color:#228be6;font-weight:600">Average</code> | [9](#9-linq) |
| <code style="color:#228be6;font-weight:600">await</code> | [20](#20-asynchroon-werken) |
| <code style="color:#228be6;font-weight:600">BadRequest</code> | [19](#19-de-helpers-van-een-controller) |
| <code style="color:#228be6;font-weight:600">class</code> | [11](#11-class) |
| <code style="color:#228be6;font-weight:600">Clear</code> | [4](#4-list) |
| <code style="color:#228be6;font-weight:600">CompareTo</code> | [1](#1-tekst) |
| constructor | [11](#11-class) |
| <code style="color:#228be6;font-weight:600">Contains</code> (lijst) | [4](#4-list) |
| <code style="color:#228be6;font-weight:600">Contains</code> (tekst) | [1](#1-tekst) |
| <code style="color:#228be6;font-weight:600">ContainsKey</code> | [5](#5-dictionary) |
| <code style="color:#228be6;font-weight:600">Count</code> (eigenschap) | [4](#4-list) |
| <code style="color:#228be6;font-weight:600">Count</code> (LINQ) | [9](#9-linq) |
| <code style="color:#228be6;font-weight:600">CreatedAtAction</code> | [19](#19-de-helpers-van-een-controller) |
| <code style="color:#228be6;font-weight:600">CultureInfo.InvariantCulture</code> | [2](#2-getallen-en-omzetten) |
| <code style="color:#228be6;font-weight:600">Dequeue</code> | [7](#7-queue) |
| <code style="color:#228be6;font-weight:600">Dictionary</code> | [5](#5-dictionary) |
| <code style="color:#228be6;font-weight:600">Distinct</code> | [9](#9-linq) |
| <code style="color:#228be6;font-weight:600">double.Parse</code> | [2](#2-getallen-en-omzetten) |
| <code style="color:#228be6;font-weight:600">EndsWith</code> | [1](#1-tekst) |
| <code style="color:#228be6;font-weight:600">Enqueue</code> | [7](#7-queue) |
| <code style="color:#228be6;font-weight:600">enum</code> | [16](#16-enum) |
| <code style="color:#228be6;font-weight:600">Enum.TryParse</code> | [16](#16-enum) |
| <code style="color:#228be6;font-weight:600">Equals</code> | [1](#1-tekst) |
| <code style="color:#228be6;font-weight:600">ExceptWith</code> | [6](#6-hashset) |
| <code style="color:#228be6;font-weight:600">Exists</code> | [4](#4-list) |
| <code style="color:#228be6;font-weight:600">File.Exists</code> | [21](#21-json-en-bestanden) |
| <code style="color:#228be6;font-weight:600">File.ReadAllText</code> | [21](#21-json-en-bestanden) |
| <code style="color:#228be6;font-weight:600">File.WriteAllText</code> | [21](#21-json-en-bestanden) |
| <code style="color:#228be6;font-weight:600">Find</code> | [4](#4-list) |
| <code style="color:#228be6;font-weight:600">FindAll</code> | [4](#4-list) |
| <code style="color:#228be6;font-weight:600">FindIndex</code> | [4](#4-list) |
| <code style="color:#228be6;font-weight:600">First</code> | [9](#9-linq) |
| <code style="color:#228be6;font-weight:600">FirstOrDefault</code> | [9](#9-linq) |
| <code style="color:#228be6;font-weight:600">GetType</code> | [18](#18-types-herkennen) |
| <code style="color:#228be6;font-weight:600">GetValueOrDefault</code> | [15](#15-omgaan-met-niets) |
| <code style="color:#228be6;font-weight:600">GroupBy</code> | [9](#9-linq) |
| <code style="color:#228be6;font-weight:600">HashSet</code> | [6](#6-hashset) |
| <code style="color:#228be6;font-weight:600">HasValue</code> | [15](#15-omgaan-met-niets) |
| <code style="color:#228be6;font-weight:600">IEnumerable</code> | [10](#10-ienumerable) |
| <code style="color:#228be6;font-weight:600">IndexOf</code> (lijst) | [4](#4-list) |
| <code style="color:#228be6;font-weight:600">IndexOf</code> (tekst) | [1](#1-tekst) |
| <code style="color:#228be6;font-weight:600">Insert</code> | [4](#4-list) |
| <code style="color:#228be6;font-weight:600">int.Parse</code> | [2](#2-getallen-en-omzetten) |
| <code style="color:#228be6;font-weight:600">int.TryParse</code> | [2](#2-getallen-en-omzetten) |
| <code style="color:#228be6;font-weight:600">interface</code> | [17](#17-interface) |
| <code style="color:#228be6;font-weight:600">IntersectWith</code> | [6](#6-hashset) |
| <code style="color:#228be6;font-weight:600">is</code> | [18](#18-types-herkennen) |
| <code style="color:#228be6;font-weight:600">IsNullOrEmpty</code> | [1](#1-tekst) |
| <code style="color:#228be6;font-weight:600">IsNullOrWhiteSpace</code> | [1](#1-tekst) |
| <code style="color:#228be6;font-weight:600">JsonSerializer.Deserialize</code> | [21](#21-json-en-bestanden) |
| <code style="color:#228be6;font-weight:600">JsonSerializer.Serialize</code> | [21](#21-json-en-bestanden) |
| <code style="color:#228be6;font-weight:600">Keys</code> | [5](#5-dictionary) |
| <code style="color:#228be6;font-weight:600">KeyValuePair</code> | [5](#5-dictionary) |
| <code style="color:#228be6;font-weight:600">Last</code> | [9](#9-linq) |
| <code style="color:#228be6;font-weight:600">LastIndexOf</code> | [1](#1-tekst) |
| <code style="color:#228be6;font-weight:600">LastOrDefault</code> | [9](#9-linq) |
| <code style="color:#228be6;font-weight:600">Length</code> | [1](#1-tekst) |
| <code style="color:#228be6;font-weight:600">List</code> | [4](#4-list) |
| <code style="color:#228be6;font-weight:600">Math.Abs</code> | [2](#2-getallen-en-omzetten) |
| <code style="color:#228be6;font-weight:600">Math.Ceiling</code> | [2](#2-getallen-en-omzetten) |
| <code style="color:#228be6;font-weight:600">Math.Floor</code> | [2](#2-getallen-en-omzetten) |
| <code style="color:#228be6;font-weight:600">Math.Max</code> | [2](#2-getallen-en-omzetten) |
| <code style="color:#228be6;font-weight:600">Math.Min</code> | [2](#2-getallen-en-omzetten) |
| <code style="color:#228be6;font-weight:600">Math.Pow</code> | [2](#2-getallen-en-omzetten) |
| <code style="color:#228be6;font-weight:600">Math.Round</code> | [2](#2-getallen-en-omzetten) |
| <code style="color:#228be6;font-weight:600">Max</code> | [9](#9-linq) |
| <code style="color:#228be6;font-weight:600">MaxBy</code> | [9](#9-linq) |
| <code style="color:#228be6;font-weight:600">MidpointRounding</code> | [2](#2-getallen-en-omzetten) |
| <code style="color:#228be6;font-weight:600">Min</code> | [9](#9-linq) |
| <code style="color:#228be6;font-weight:600">MinBy</code> | [9](#9-linq) |
| <code style="color:#228be6;font-weight:600">nameof</code> | [18](#18-types-herkennen) |
| <code style="color:#228be6;font-weight:600">NoContent</code> | [19](#19-de-helpers-van-een-controller) |
| <code style="color:#228be6;font-weight:600">NotFound</code> | [19](#19-de-helpers-van-een-controller) |
| <code style="color:#228be6;font-weight:600">Ok</code> | [19](#19-de-helpers-van-een-controller) |
| <code style="color:#228be6;font-weight:600">OrderBy</code> | [9](#9-linq) |
| <code style="color:#228be6;font-weight:600">OrderByDescending</code> | [9](#9-linq) |
| <code style="color:#228be6;font-weight:600">PadLeft</code> | [1](#1-tekst) |
| <code style="color:#228be6;font-weight:600">PadRight</code> | [1](#1-tekst) |
| <code style="color:#228be6;font-weight:600">Path.Combine</code> | [21](#21-json-en-bestanden) |
| <code style="color:#228be6;font-weight:600">Peek</code> | [7](#7-queue), [8](#8-stack) |
| <code style="color:#228be6;font-weight:600">Pop</code> | [8](#8-stack) |
| <code style="color:#228be6;font-weight:600">Push</code> | [8](#8-stack) |
| <code style="color:#228be6;font-weight:600">Queue</code> | [7](#7-queue) |
| <code style="color:#228be6;font-weight:600">record</code> | [12](#12-record) |
| referentietype | [14](#14-referentie-tegenover-waarde) |
| <code style="color:#228be6;font-weight:600">Remove</code> (dictionary) | [5](#5-dictionary) |
| <code style="color:#228be6;font-weight:600">Remove</code> (lijst) | [4](#4-list) |
| <code style="color:#228be6;font-weight:600">RemoveAll</code> | [4](#4-list) |
| <code style="color:#228be6;font-weight:600">RemoveAt</code> | [4](#4-list) |
| <code style="color:#228be6;font-weight:600">Replace</code> | [1](#1-tekst) |
| <code style="color:#228be6;font-weight:600">Reverse</code> | [4](#4-list) |
| <code style="color:#228be6;font-weight:600">Select</code> | [9](#9-linq) |
| <code style="color:#228be6;font-weight:600">SelectMany</code> | [9](#9-linq) |
| <code style="color:#228be6;font-weight:600">Single</code> | [9](#9-linq) |
| <code style="color:#228be6;font-weight:600">SingleOrDefault</code> | [9](#9-linq) |
| <code style="color:#228be6;font-weight:600">Skip</code> | [9](#9-linq) |
| <code style="color:#228be6;font-weight:600">Sort</code> | [4](#4-list) |
| <code style="color:#228be6;font-weight:600">Split</code> | [1](#1-tekst) |
| <code style="color:#228be6;font-weight:600">Stack</code> | [8](#8-stack) |
| <code style="color:#228be6;font-weight:600">StartsWith</code> | [1](#1-tekst) |
| <code style="color:#228be6;font-weight:600">StatusCode</code> | [19](#19-de-helpers-van-een-controller) |
| <code style="color:#228be6;font-weight:600">string.Format</code> | [1](#1-tekst) |
| <code style="color:#228be6;font-weight:600">string.Join</code> | [1](#1-tekst) |
| <code style="color:#228be6;font-weight:600">struct</code> | [13](#13-struct) |
| <code style="color:#228be6;font-weight:600">Substring</code> | [1](#1-tekst) |
| <code style="color:#228be6;font-weight:600">Sum</code> | [9](#9-linq) |
| <code style="color:#228be6;font-weight:600">Take</code> | [9](#9-linq) |
| <code style="color:#228be6;font-weight:600">ThenBy</code> | [9](#9-linq) |
| <code style="color:#228be6;font-weight:600">ToArray</code> | [4](#4-list) |
| <code style="color:#228be6;font-weight:600">ToDictionary</code> | [9](#9-linq) |
| <code style="color:#228be6;font-weight:600">ToList</code> | [9](#9-linq) |
| <code style="color:#228be6;font-weight:600">ToLower</code> | [1](#1-tekst) |
| <code style="color:#228be6;font-weight:600">ToUpper</code> | [1](#1-tekst) |
| <code style="color:#228be6;font-weight:600">Trim</code> | [1](#1-tekst) |
| <code style="color:#228be6;font-weight:600">TrimEnd</code> | [1](#1-tekst) |
| <code style="color:#228be6;font-weight:600">TrimStart</code> | [1](#1-tekst) |
| <code style="color:#228be6;font-weight:600">TryAdd</code> | [5](#5-dictionary) |
| <code style="color:#228be6;font-weight:600">TryDequeue</code> | [7](#7-queue) |
| <code style="color:#228be6;font-weight:600">TryGetValue</code> | [5](#5-dictionary) |
| <code style="color:#228be6;font-weight:600">typeof</code> | [18](#18-types-herkennen) |
| <code style="color:#228be6;font-weight:600">UnionWith</code> | [6](#6-hashset) |
| <code style="color:#228be6;font-weight:600">Values</code> | [5](#5-dictionary) |
| waardetype | [14](#14-referentie-tegenover-waarde) |
| <code style="color:#228be6;font-weight:600">Where</code> | [9](#9-linq) |
| <code style="color:#228be6;font-weight:600">with</code> | [12](#12-record) |
| <code style="color:#228be6;font-weight:600">%</code> | [2](#2-getallen-en-omzetten) |
| <code style="color:#228be6;font-weight:600">?.</code> | [15](#15-omgaan-met-niets) |
| <code style="color:#228be6;font-weight:600">??</code> en <code style="color:#228be6;font-weight:600">??=</code> | [15](#15-omgaan-met-niets) |
