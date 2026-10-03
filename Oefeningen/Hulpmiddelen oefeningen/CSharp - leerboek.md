# C# — leerboek

<b>Programming Advanced</b> &nbsp;·&nbsp; van tekst tot database &nbsp;·&nbsp; 34 hoofdstukken &nbsp;·&nbsp; met voorspelvragen om jezelf te testen

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

Volg je de lessen van Programming Advanced, dan zegt de
[leerwijzer](#leerwijzer-per-les) welke hoofdstukken bij elke les horen.

### Kleuren en labels

| Label of kleur | Betekenis |
|---|---|
| <span style="background:#d9822b;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>METHODES</b></span> | een hoofdstuk geordend op wat je wil doen |
| <span style="background:#1c7ed6;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>VERZAMELING</b></span> | een bouwsteen die meerdere dingen bewaart |
| <span style="background:#2f9e44;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>EIGEN TYPE</b></span> | een bouwsteen die je zelf maakt |
| <span style="background:#7048e8;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>CONCEPT</b></span> | een idee dat door alle hoofdstukken heen loopt |
| <span style="background:#0c8599;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>IN JE API</b></span> | wat je nodig hebt zodra je een API bouwt |
| <span style="background:#c2255c;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>DATA</b></span> | werken met een database |
| <code style="color:#228be6;font-weight:600">methode</code> | de naam van een methode of eigenschap |
| <code style="color:#845ef7">resultaat</code> | wat eruit komt |
| <code style="color:#f03e3e">Exception</code> | het programma crasht, of compileert niet |
| <span style="color:#37b24d"><b>Wel</b></span> / <span style="color:#f03e3e"><b>Niet</b></span> | wanneer je een bouwsteen kiest, en wanneer niet |

Open dit bestand in VS Code en druk op `Ctrl+Shift+V` om het met kleuren te zien.

> [!NOTE]
> De voorbeelden zijn losse regels over fruit, steden, leerlingen, dieren, recepten
> en verkeerslichten, en hier en daar een onderdeel of een ventilator. Het zijn nooit
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
| **Vooraan** | [Leerwijzer per les](#leerwijzer-per-les) | welke hoofdstukken bij welke les horen |
| | [Keuzegids](#keuzegids) | welke bouwsteen je neemt, en waar je iets vindt |
| **1. De basis** | [1. Tekst](#1-tekst) | lengte, hoofdletters, knippen, zoeken, vergelijken |
| | [2. Getallen en omzetten](#2-getallen-en-omzetten) | tekst naar getal, afronden, delen met rest |
| | [3. Datum en tijd](#3-datum-en-tijd) | datums maken, rekenen, tonen |
| | [4. Beslissen en herhalen](#4-beslissen-en-herhalen) | `if`, `switch`, `for`, `while` |
| **2. Verzamelingen** | [5. Array](#5-array) | een rij met vaste lengte |
| | [6. List](#6-list) | een lijst die groeit en krimpt |
| | [7. Dictionary](#7-dictionary) | opzoeken op een sleutel |
| | [8. HashSet](#8-hashset) | een verzameling zonder dubbels |
| | [9. Queue](#9-queue) | wie eerst komt, eerst geholpen |
| | [10. Stack](#10-stack) | het laatste eerst terug |
| **3. Zoeken en rekenen** | [11. LINQ](#11-linq) | zoeken, filteren, tellen en sorteren in elke verzameling |
| | [12. IEnumerable](#12-ienumerable) | wat LINQ eigenlijk teruggeeft |
| **4. Eigen types** | [13. class](#13-class) | een bouwplan voor je eigen objecten |
| | [14. record](#14-record) | een pakketje gegevens dat draait om de inhoud |
| | [15. struct](#15-struct) | een klein pakketje dat echt gekopieerd wordt |
| | [16. Referentie tegenover waarde](#16-referentie-tegenover-waarde) | het belangrijkste idee van dit leerboek |
| | [17. Omgaan met niets](#17-omgaan-met-niets) | `null`, `??`, `?.` en getallen die mogen ontbreken |
| | [18. enum](#18-enum) | een vaste lijst benoemde keuzes |
| | [19. interface](#19-interface) | een contract: wat iets moet kunnen |
| | [20. Overerving en abstracte klassen](#20-overerving-en-abstracte-klassen) | verder bouwen op een andere klasse, `abstract`, `override` |
| | [21. Generics](#21-generics) | een type met een open plaats, zoals `List<T>` |
| | [22. Types herkennen](#22-types-herkennen) | `is`, `as`, `typeof`, `nameof` |
| | [23. Uitzonderingen](#23-uitzonderingen) | `try`, `catch`, `throw`: fouten opvangen en melden |
| | [24. static, const en readonly](#24-static-const-en-readonly) | wat bij de klasse hoort, en wat niet meer verandert |
| **5. In je API** | [25. Attributen en routes](#25-attributen-en-routes) | welke URL bij welke methode uitkomt |
| | [26. De helpers van een controller](#26-de-helpers-van-een-controller) | `Ok`, `NotFound`, `BadRequest` en de rest |
| | [27. Program.cs en dependency injection](#27-programcs-en-dependency-injection) | opstarten, diensten registreren en opvragen |
| | [28. Logging](#28-logging) | meldingen in het terminalvenster |
| | [29. Asynchroon werken](#29-asynchroon-werken) | `async`, `await`, `Task` |
| | [30. JSON en bestanden](#30-json-en-bestanden) | lezen, schrijven, omzetten |
| **6. Data** | [31. EF Core: model en database](#31-ef-core-model-en-database) | tabellen, relaties en migraties |
| | [32. EF Core: opvragen en bewaren](#32-ef-core-opvragen-en-bewaren) | ophalen, toevoegen, wijzigen, `Include` |
| | [33. DTO's en Mapster](#33-dtos-en-mapster) | enkel doorgeven wat de client nodig heeft |
| | [34. Dapper](#34-dapper) | zelf SQL schrijven, met een micro-ORM |
| **Achteraan** | [Veelgemaakte verwarringen](#veelgemaakte-verwarringen) | dertig paren die op elkaar lijken |
| | [Index](#index) | alles alfabetisch |

---

## Leerwijzer per les

Welke hoofdstukken horen bij welke les van Programming Advanced? Lees ze vóór of
naast de les. Deel 1 heb je in elke les nodig; de tabel noemt de hoofdstukken die
in die les nieuw zijn of het meest terugkomen.

| Les | Onderwerp | Hoofdstukken |
|---|---|---|
| 01 | Introductie | [13](#13-class), [20](#20-overerving-en-abstracte-klassen), [25](#25-attributen-en-routes), [26](#26-de-helpers-van-een-controller), [27](#27-programcs-en-dependency-injection) |
| 02 | HTTP Verbs | [6](#6-list), [11](#11-linq), [17](#17-omgaan-met-niets), [24](#24-static-const-en-readonly), [25](#25-attributen-en-routes), [26](#26-de-helpers-van-een-controller) |
| 03 | Repositories | [17](#17-omgaan-met-niets), [19](#19-interface), [24](#24-static-const-en-readonly), [27](#27-programcs-en-dependency-injection), [28](#28-logging) |
| 04 | Entity Framework Core | [20](#20-overerving-en-abstracte-klassen), [27](#27-programcs-en-dependency-injection), [31](#31-ef-core-model-en-database) |
| 05 | Asynchroon programmeren | [12](#12-ienumerable), [23](#23-uitzonderingen), [29](#29-asynchroon-werken), [32](#32-ef-core-opvragen-en-bewaren) |
| 06 | Relaties | [2](#2-getallen-en-omzetten), [3](#3-datum-en-tijd), [17](#17-omgaan-met-niets), [31](#31-ef-core-model-en-database) |
| 07 | Generics & Unit of Work | [17](#17-omgaan-met-niets), [19](#19-interface), [20](#20-overerving-en-abstracte-klassen), [21](#21-generics), [32](#32-ef-core-opvragen-en-bewaren) |
| 08 | Include statements | [7](#7-dictionary), [13](#13-class), [32](#32-ef-core-opvragen-en-bewaren), [34](#34-dapper) |
| 09 | DTO's | [11](#11-linq), [14](#14-record), [33](#33-dtos-en-mapster) |

> [!NOTE]
> De tabel loopt tot les 09. Komt er een les bij, zoek dan de namen die je erin
> tegenkomt op in de index achteraan.

---

## Keuzegids

### Welke bouwsteen neem ik?

| Ik wil… | Neem | Hoofdstuk |
|---|---|---|
| een bedrag of prijs, exact tot op de cent | `decimal` | 2 |
| een datum of een tijdstip | `DateTime` | 3 |
| een vast aantal dingen dat nooit verandert | `array` | 5 |
| een aantal dingen waar ik bijvoeg en afhaal | `List` | 6 |
| iets terugvinden op een naam, code of nummer | `Dictionary` | 7 |
| zeker zijn dat er geen dubbels in zitten | `HashSet` | 8 |
| dingen afhandelen in de volgorde waarin ze binnenkwamen | `Queue` | 9 |
| het laatst toegevoegde als eerste terugnemen | `Stack` | 10 |
| een verzameling doorgeven zonder te zeggen welke soort | `IEnumerable` | 12 |
| een eigen ding met gegevens én gedrag | `class` | 13 |
| een pakketje gegevens dat gelijk is zodra de inhoud gelijk is | `record` | 14 |
| een klein pakketje dat bij kopiëren echt gekopieerd wordt | `struct` | 15 |
| een vaste lijst benoemde keuzes | `enum` | 18 |
| afspreken wat iets moet kunnen, zonder te zeggen hoe | `interface` | 19 |
| verwante klassen die gegevens en uitgewerkte methodes delen | `abstract class` | 20 |
| één stuk code dat werkt voor elk type dat je invult | generics, `<T>` | 21 |
| iets dat bij de klasse hoort, niet bij één object | `static` | 24 |
| een waarde die na het aanmaken nooit meer verandert | `const` of `readonly` | 24 |
| gegevens bewaren in een database, zonder zelf SQL te schrijven | EF Core | 31 |
| aan de client enkel geven wat hij nodig heeft | een DTO | 33 |
| zelf de SQL schrijven voor een snelle leesvraag | Dapper | 34 |

### Waar vind ik hoe ik…

| Ik wil… | Hoofdstuk |
|---|---|
| een stuk uit een tekst halen, of tekst opsplitsen | 1 |
| twee teksten vergelijken zonder op hoofdletters te letten | 1 |
| tekst omzetten naar een getal zonder te crashen | 2 |
| iets verdelen: hoeveel per groep, hoeveel over | 2 |
| met datums rekenen, of een datum tonen | 3 |
| kiezen tussen meerdere mogelijkheden | 4 |
| iets een aantal keer herhalen | 4 |
| iets toevoegen aan of verwijderen uit een lijst | 6 |
| per soort tellen hoeveel er van zijn | 7 |
| één item uit een verzameling halen | 11 |
| weten of er iets in zit dat voldoet | 11 |
| filteren, sorteren, optellen, het grootste vinden | 11 |
| begrijpen waarom mijn resultaat plots veranderd is | 12 |
| begrijpen waarom mijn lijst na een methode veranderd is | 16 |
| iets doen met een waarde die `null` kan zijn | 17 |
| begrijpen wat `= default!` achter een eigenschap doet | 17 |
| een methode van een basisklasse anders laten werken | 20 |
| één klasse schrijven die voor elk type werkt | 21 |
| nagaan van welk type een object is | 22 |
| een fout opvangen, zodat mijn programma niet stopt | 23 |
| zelf een fout melden vanuit een methode | 23 |
| een lijst bewaren tussen twee verzoeken | 24 |
| een methode aan een URL koppelen | 25 |
| een waarde uit de URL in mijn methode krijgen | 25 |
| de juiste statuscode teruggeven in een API | 26 |
| mijn repository aan mijn controller geven | 27 |
| iets melden in het terminalvenster terwijl de API draait | 28 |
| een methode asynchroon maken | 29 |
| een JSON-bestand inlezen | 30 |
| een tabel maken voor mijn klasse | 31 |
| een relatie tussen twee tabellen vastleggen | 31 |
| mijn database bijwerken na een wijziging aan een klasse | 31 |
| gegevens ophalen, toevoegen, wijzigen of verwijderen in de database | 32 |
| gerelateerde gegevens meeladen | 32 |
| een entiteit omzetten naar een DTO | 33 |

---

# Deel 1 — De basis

Tekst, getallen, datums en beslissingen heb je in elk ander hoofdstuk nodig. Daarom komen ze eerst.

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

### Kommagetallen: double tegenover decimal

C# heeft twee soorten kommagetallen die je vaak tegenkomt. Een `double` is snel,
maar bewaart sommige getallen net niet exact. Een `decimal` is trager, maar
rekent tot op de cent juist. Vanaf les 06 staat een prijs in de cursus als
`decimal`.

| Vorm | Wat het doet | Voorbeeld | Resultaat |
|---|---|---|---|
| <code style="color:#228be6;font-weight:600">double</code> | snel kommagetal, kan een heel klein beetje afwijken | `0.1 + 0.2 == 0.3` | <code style="color:#845ef7">false</code> |
| <code style="color:#228be6;font-weight:600">decimal</code> | exact kommagetal, voor geld en prijzen | `0.1m + 0.2m == 0.3m` | <code style="color:#845ef7">true</code> |
| <code style="color:#228be6;font-weight:600">m</code> achter een getal | maakt er een `decimal` van | `decimal prijs = 19.99m;` | compileert |
| zonder <code style="color:#228be6;font-weight:600">m</code> | een kommagetal zonder letter is een `double` | `decimal prijs = 19.99;` | <code style="color:#f03e3e">compileert niet</code> |
| <code style="color:#228be6;font-weight:600">(double)</code> | zet een `decimal` om naar een `double` | `(double)19.99m` | <code style="color:#845ef7">19.99</code> |
| <code style="color:#228be6;font-weight:600">Math.Round</code> | afronden op een aantal cijfers na de komma | `Math.Round(19.987m, 2)` | <code style="color:#845ef7">19.99</code> |

> [!WARNING]
> Een `decimal` en een `double` mag je niet zomaar door elkaar gebruiken.
> `prijs * 1.21` compileert niet als `prijs` een `decimal` is, want `1.21` is een
> `double`. Schrijf `prijs * 1.21m`.

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

**Vraag 5**

```csharp
Console.WriteLine(0.1 + 0.2 == 0.3);
Console.WriteLine(0.1m + 0.2m == 0.3m);
```

<details>
<summary><span style="color:#845ef7"><b>Toon het antwoord</b></span></summary>

<code style="color:#845ef7">False</code> en daarna <code style="color:#845ef7">True</code>

Een `double` bewaart 0,1 en 0,2 niet exact, en de som ligt een heel klein beetje
naast 0,3. Een `decimal` rekent met tientallen, zoals jij op papier, en komt wel
precies op 0,3 uit. Daarom is een prijs een `decimal`.

</details>

**Vraag 6**

```csharp
decimal prijs = 10m;
var metBtw = prijs * 1.21;
```

<details>
<summary><span style="color:#845ef7"><b>Toon het antwoord</b></span></summary>

<code style="color:#f03e3e">Compileert niet.</code>

`1.21` zonder letter is een `double`, en C# weigert een `decimal` met een
`double` te vermenigvuldigen. Het zou niet weten welk van de twee soorten het
resultaat moet worden. Met `1.21m` lukt het wel.

</details>

---

## 3. Datum en tijd

<span style="background:#d9822b;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>METHODES</b></span>

> [!NOTE]
> In les 06 en 09 krijgen je modellen een datum, zoals de dag waarop iets
> aangemaakt werd. Daarvoor gebruik je `DateTime`.

### Een datum maken

| Vorm | Wat het doet | Voorbeeld | Resultaat |
|---|---|---|---|
| <code style="color:#228be6;font-weight:600">new DateTime(j, m, d)</code> | een vaste datum | `new DateTime(2026, 10, 3)` | 3 oktober 2026, om middernacht |
| <code style="color:#228be6;font-weight:600">new DateTime(j, m, d, u, min, s)</code> | een vaste datum met uur | `new DateTime(2026, 10, 3, 14, 30, 0)` | 3 oktober 2026, 14.30 uur |
| <code style="color:#228be6;font-weight:600">DateTime.Now</code> | nu, op de klok van de computer | `DateTime.Now` | het huidige moment |
| <code style="color:#228be6;font-weight:600">DateTime.Today</code> | vandaag, om middernacht | `DateTime.Today` | de datum van vandaag, zonder uur |
| <code style="color:#228be6;font-weight:600">DateTime.UtcNow</code> | nu, in wereldtijd | `DateTime.UtcNow` | in België één of twee uur vroeger dan `Now` |
| <code style="color:#228be6;font-weight:600">DateOnly</code> | enkel een datum, zonder uur | `new DateOnly(2026, 10, 3)` | 3 oktober 2026 |

Maanden tellen hier vanaf 1, niet vanaf 0 zoals de posities in een array.
Januari is 1, december 12.

### Een stuk eruit halen

| Vorm | Wat het doet | Voorbeeld | Resultaat |
|---|---|---|---|
| <code style="color:#228be6;font-weight:600">Year</code> | het jaar | `d.Year` | <code style="color:#845ef7">2026</code> |
| <code style="color:#228be6;font-weight:600">Month</code> | de maand, van 1 tot 12 | `d.Month` | <code style="color:#845ef7">10</code> |
| <code style="color:#228be6;font-weight:600">Day</code> | de dag van de maand | `d.Day` | <code style="color:#845ef7">3</code> |
| <code style="color:#228be6;font-weight:600">Hour</code>, <code style="color:#228be6;font-weight:600">Minute</code> | het uur en de minuut | `d.Hour` | <code style="color:#845ef7">14</code> |
| <code style="color:#228be6;font-weight:600">DayOfWeek</code> | de dag van de week, als enum | `d.DayOfWeek` | <code style="color:#845ef7">Saturday</code> |
| <code style="color:#228be6;font-weight:600">Date</code> | dezelfde dag, met het uur op middernacht | `d.Date` | 3 oktober 2026, 00.00 uur |

`DayOfWeek` is een enum zoals in hoofdstuk 18, met Engelse namen: `Monday` tot en
met `Sunday`. Dat blijft zo, ook op een Nederlandstalige computer.

### Rekenen met datums

| Vorm | Wat het doet | Voorbeeld | Resultaat |
|---|---|---|---|
| <code style="color:#228be6;font-weight:600">AddDays</code> | een aantal dagen erbij, of eraf met een minteken | `d.AddDays(7)` | een week later |
| <code style="color:#228be6;font-weight:600">AddMonths</code> | een aantal maanden erbij | `d.AddMonths(1)` | een maand later |
| <code style="color:#228be6;font-weight:600">AddHours</code> | een aantal uren erbij | `d.AddHours(-2)` | twee uur vroeger |
| <code style="color:#228be6;font-weight:600">-</code> | het verschil tussen twee datums | `eind - begin` | een `TimeSpan` |
| <code style="color:#228be6;font-weight:600">Days</code> | het aantal hele dagen in een `TimeSpan` | `(eind - begin).Days` | <code style="color:#845ef7">14</code> |
| <code style="color:#228be6;font-weight:600">TotalHours</code> | het verschil in uren, met komma | `(eind - begin).TotalHours` | <code style="color:#845ef7">336</code> |
| <code style="color:#228be6;font-weight:600">&lt;</code>, <code style="color:#228be6;font-weight:600">&gt;</code>, <code style="color:#228be6;font-weight:600">==</code> | vergelijken: welke komt eerst | `begin < eind` | <code style="color:#845ef7">true</code> |

Net als bij tekst veranderen deze methodes de datum zelf niet. `d.AddDays(1)` op
een regel alleen doet niets; je moet het resultaat opvangen.

### Tonen en inlezen

| Vorm | Wat het doet | Voorbeeld | Resultaat |
|---|---|---|---|
| <code style="color:#228be6;font-weight:600">ToString("yyyy-MM-dd")</code> | toon in een vaste vorm | `d.ToString("yyyy-MM-dd")` | <code style="color:#845ef7">"2026-10-03"</code> |
| <code style="color:#228be6;font-weight:600">ToString("HH:mm")</code> | enkel het uur | `d.ToString("HH:mm")` | <code style="color:#845ef7">"14:30"</code> |
| <code style="color:#228be6;font-weight:600">DateTime.TryParse</code> | probeer een tekst om te zetten, volgens de taalinstelling | `DateTime.TryParse("03/10/2026", out DateTime d)` | op een Belgische computer: 3 oktober |
| <code style="color:#228be6;font-weight:600">DateTime.ParseExact</code> | zet om volgens een vorm die jij opgeeft | `DateTime.ParseExact("2026-10-03", "yyyy-MM-dd", CultureInfo.InvariantCulture)` | 3 oktober 2026 |

> [!WARNING]
> In een opmaak betekent `MM` maand en `mm` minuten. `HH` is het uur van 0 tot
> 23, `hh` het uur van 1 tot 12. Eén verkeerde hoofdletter geeft een datum die
> er geloofwaardig uitziet en toch fout is.
>
> En net als bij `double.Parse` in hoofdstuk 2 leest `DateTime.TryParse` volgens
> de taalinstelling. `"03/10/2026"` is in België 3 oktober, in de Verenigde Staten
> 10 maart. Ken je de vorm, gebruik dan `ParseExact`.

### Voorspel de uitkomst

**Vraag 1**

```csharp
var d = new DateTime(2026, 1, 31);
Console.WriteLine(d.AddMonths(1).Day);
```

<details>
<summary><span style="color:#845ef7"><b>Toon het antwoord</b></span></summary>

<code style="color:#845ef7">28</code>

Februari 2026 heeft geen 31ste. `AddMonths` blijft dan op de laatste dag van die
maand staan, in plaats van door te schuiven naar maart.

</details>

**Vraag 2**

```csharp
var begin = new DateTime(2026, 10, 1);
var eind = new DateTime(2026, 10, 15);
Console.WriteLine((eind - begin).Days);
```

<details>
<summary><span style="color:#845ef7"><b>Toon het antwoord</b></span></summary>

<code style="color:#845ef7">14</code>

Van de eerste tot de vijftiende zijn het veertien dagen. Het verschil tussen twee
datums is een `TimeSpan`, en `Days` geeft daarvan het aantal hele dagen.

</details>

**Vraag 3**

```csharp
var d = new DateTime(2026, 10, 3);
d.AddDays(1);
Console.WriteLine(d.Day);
```

<details>
<summary><span style="color:#845ef7"><b>Toon het antwoord</b></span></summary>

<code style="color:#845ef7">3</code>

`AddDays` geeft een nieuwe datum terug en laat `d` met rust, net zoals `Trim` bij
tekst. Wil je dat het werkt, dan schrijf je `d = d.AddDays(1);`.

</details>

**Vraag 4**

```csharp
var d = new DateTime(2026, 10, 3, 14, 5, 0);
Console.WriteLine(d.ToString("yyyy-mm-dd"));
```

<details>
<summary><span style="color:#845ef7"><b>Toon het antwoord</b></span></summary>

<code style="color:#845ef7">2026-05-03</code>

`mm` met kleine letters zijn de minuten, en dat is hier 5. Voor de maand had er
`MM` moeten staan.

</details>

**Vraag 5**

```csharp
var d = new DateTime(2026, 2, 29);
```

<details>
<summary><span style="color:#845ef7"><b>Toon het antwoord</b></span></summary>

<code style="color:#f03e3e">ArgumentOutOfRangeException</code> — het programma crasht.

2026 is geen schrikkeljaar, dus 29 februari bestaat niet. Het compileert wel,
want de compiler rekent geen kalenders na. De fout komt pas als de regel draait.

</details>

---

## 4. Beslissen en herhalen

<span style="background:#d9822b;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>METHODES</b></span>

> [!NOTE]
> Ken je dit al uit een andere taal, lees dan vooral de tabellen en de
> waarschuwingen. Een paar dingen werken in C# net iets anders, zoals de
> switch-expressie.

### Vergelijken en combineren

| Vorm | Wat het doet | Voorbeeld | Resultaat |
|---|---|---|---|
| <code style="color:#228be6;font-weight:600">==</code> | is gelijk aan | `5 == 5` | <code style="color:#845ef7">true</code> |
| <code style="color:#228be6;font-weight:600">!=</code> | is niet gelijk aan | `5 != 3` | <code style="color:#845ef7">true</code> |
| <code style="color:#228be6;font-weight:600">&lt;</code>, <code style="color:#228be6;font-weight:600">&lt;=</code> | kleiner dan, kleiner dan of gelijk aan | `3 <= 3` | <code style="color:#845ef7">true</code> |
| <code style="color:#228be6;font-weight:600">&gt;</code>, <code style="color:#228be6;font-weight:600">&gt;=</code> | groter dan, groter dan of gelijk aan | `2 > 3` | <code style="color:#845ef7">false</code> |
| <code style="color:#228be6;font-weight:600">&amp;&amp;</code> | en: allebei waar | `leeftijd >= 3 && leeftijd <= 17` | <code style="color:#845ef7">true</code> voor 3 tot en met 17 |
| <code style="color:#228be6;font-weight:600">&#124;&#124;</code> | of: minstens één waar | <code>dag == "za" &#124;&#124; dag == "zo"</code> | <code style="color:#845ef7">true</code> in het weekend |
| <code style="color:#228be6;font-weight:600">!</code> | niet: draait om | `!gevonden` | <code style="color:#845ef7">true</code> als er niets gevonden is |

`&&` en `||` stoppen zodra het antwoord vastligt. Bij `s != null && s.Length > 3`
wordt de lengte enkel opgevraagd als `s` niet `null` is. Dat is geen toeval: zo
schrijf je een controle die niet crasht.

### Iets doen als…

```csharp
int leeftijd = 15;

if (leeftijd < 3)
{
    Console.WriteLine("Gratis");
}
else if (leeftijd < 18)
{
    Console.WriteLine("Kindertarief");
}
else
{
    Console.WriteLine("Volle prijs");
}
```

C# loopt de voorwaarden van boven naar onder af en neemt de **eerste** die klopt.
De rest wordt niet meer bekeken. De volgorde van je `else if` bepaalt dus het
antwoord.

Voor een keuze tussen twee waarden bestaat er een korte vorm:

```csharp
string tekst = leeftijd >= 18 ? "volwassen" : "minderjarig";
```

Lees het als: *klopt de voorwaarde? Dan het eerste, anders het tweede.*

### Kiezen uit veel mogelijkheden: switch

Vergelijk je één waarde met een reeks vaste mogelijkheden, dan is een `switch`
overzichtelijker dan een lange rij `else if`:

```csharp
string dag = "za";

switch (dag)
{
    case "za":
    case "zo":
        Console.WriteLine("Weekend");
        break;
    case "wo":
        Console.WriteLine("Halve dag");
        break;
    default:
        Console.WriteLine("Weekdag");
        break;
}
```

Moet er gewoon een waarde uitkomen, dan is de **switch-expressie** korter. Ze
werkt ook met grenzen:

```csharp
int graden = 25;

string advies = graden switch
{
    < 10 => "Neem een jas",
    < 20 => "Neem een trui",
    _ => "Een t-shirt volstaat"
};
```

De `_` betekent *al de rest*. Ook hier geldt: de eerste die past, wint.

### Herhalen

```csharp
for (int i = 0; i < 3; i++)
{
    Console.WriteLine(i);           // 0, 1, 2
}

int teller = 10;
while (teller > 0)
{
    teller -= 3;                    // 7, 4, 1, -2: dan stopt het
}

foreach (string naam in namen)
{
    Console.WriteLine(naam);        // elk item één keer
}
```

### Wat je ermee kan

| Vorm | Wat het doet | Voorbeeld | Resultaat |
|---|---|---|---|
| <code style="color:#228be6;font-weight:600">if</code> / <code style="color:#228be6;font-weight:600">else if</code> / <code style="color:#228be6;font-weight:600">else</code> | doe iets als een voorwaarde klopt | `if (x > 0) { ... }` | de eerste die klopt, wint |
| <code style="color:#228be6;font-weight:600">? :</code> | kies tussen twee waarden | `x > 0 ? "plus" : "min"` | <code style="color:#845ef7">"plus"</code> of <code style="color:#845ef7">"min"</code> |
| <code style="color:#228be6;font-weight:600">switch</code> | voer een blok uit per mogelijkheid | `case "za": ... break;` | elk blok eindigt met `break` |
| <code style="color:#228be6;font-weight:600">switch</code>-expressie | geef een waarde per mogelijkheid | `x switch { 1 => "een", _ => "veel" }` | <code style="color:#845ef7">"een"</code> of <code style="color:#845ef7">"veel"</code> |
| <code style="color:#228be6;font-weight:600">&lt; 10 =&gt;</code> | een grens in een switch-expressie | `< 10 => "koud"` | alles onder 10 |
| <code style="color:#228be6;font-weight:600">and</code>, <code style="color:#228be6;font-weight:600">or</code> | grenzen combineren in een switch-expressie | `>= 3 and <= 17 => "kind"` | 3 tot en met 17 |
| <code style="color:#228be6;font-weight:600">_ =&gt;</code> | al de rest | `_ => "onbekend"` | wat nergens anders past |
| <code style="color:#228be6;font-weight:600">for</code> | herhaal een vast aantal keer | `for (int i = 0; i < 5; i++)` | <code style="color:#845ef7">i</code> gaat van 0 tot 4 |
| <code style="color:#228be6;font-weight:600">while</code> | herhaal zolang iets klopt | `while (teller > 0)` | misschien nul keer |
| <code style="color:#228be6;font-weight:600">do</code> … <code style="color:#228be6;font-weight:600">while</code> | herhaal, en kijk pas achteraf | `do { ... } while (x > 0);` | minstens één keer |
| <code style="color:#228be6;font-weight:600">foreach</code> | elk item van een verzameling | `foreach (var n in namen)` | elk item één keer |
| <code style="color:#228be6;font-weight:600">break</code> | stop de lus nu | `if (gevonden) break;` | de lus is voorbij |
| <code style="color:#228be6;font-weight:600">continue</code> | sla de rest van deze ronde over | `if (x < 0) continue;` | door naar de volgende ronde |

### Veelgemaakte fouten

> [!WARNING]
> - `=` geeft een waarde, `==` vergelijkt. `if (x = 5)` compileert in C# niet,
>   want een toewijzing is geen ja of nee.
> - Een switch-expressie zonder `_` compileert met een waarschuwing. Past de
>   waarde nergens, dan crasht ze terwijl het programma draait.
> - `for (int i = 0; i <= lijst.Count; i++)` loopt één keer te veel. De laatste
>   positie is `Count - 1`, dus het is `<`, niet `<=`.
> - Een `while` waarvan de voorwaarde nooit vals wordt, stopt nooit. Kijk na of
>   er in de lus iets verandert aan wat je controleert.
> - Teksten vergelijken met `==` let op hoofdletters. Komt een waarde van een
>   gebruiker, denk dan aan `Equals` met `OrdinalIgnoreCase` uit hoofdstuk 1.

### Voorspel de uitkomst

**Vraag 1**

```csharp
int leeftijd = 70;

if (leeftijd >= 18)
{
    Console.WriteLine("volwassen");
}
else if (leeftijd >= 65)
{
    Console.WriteLine("senior");
}
else
{
    Console.WriteLine("kind");
}
```

<details>
<summary><span style="color:#845ef7"><b>Toon het antwoord</b></span></summary>

<code style="color:#845ef7">volwassen</code>

70 is groter dan 18, dus de eerste voorwaarde klopt al. De tweede wordt niet
meer bekeken, ook al zou die ook kloppen. Wil je dat senioren apart behandeld
worden, dan moet de controle op 65 eerst komen.

</details>

**Vraag 2**

```csharp
int graden = 15;

string advies = graden switch
{
    < 10 => "jas",
    < 20 => "trui",
    _ => "t-shirt"
};

Console.WriteLine(advies);
```

<details>
<summary><span style="color:#845ef7"><b>Toon het antwoord</b></span></summary>

<code style="color:#845ef7">trui</code>

15 is niet kleiner dan 10, maar wel kleiner dan 20. De tweede regel past als
eerste.

</details>

**Vraag 3**

```csharp
for (int i = 1; i <= 5; i++)
{
    if (i == 2)
    {
        continue;
    }

    if (i == 4)
    {
        break;
    }

    Console.Write(i);
}
```

<details>
<summary><span style="color:#845ef7"><b>Toon het antwoord</b></span></summary>

<code style="color:#845ef7">13</code>

Bij 1 wordt er geschreven. Bij 2 slaat `continue` de rest over. Bij 3 wordt er
weer geschreven. Bij 4 stopt `break` de hele lus, dus 4 en 5 komen er nooit.
`Console.Write` zet alles op één regel.

</details>

**Vraag 4**

```csharp
int n = 10;

do
{
    Console.WriteLine(n);
}
while (n < 5);
```

<details>
<summary><span style="color:#845ef7"><b>Toon het antwoord</b></span></summary>

<code style="color:#845ef7">10</code>

Een `do`-lus voert het blok eerst uit en kijkt pas daarna naar de voorwaarde. 10
is niet kleiner dan 5, dus het blijft bij die ene keer. Met een gewone `while`
was er niets op het scherm gekomen.

</details>

**Vraag 5**

```csharp
int x = 5;

string woord = x switch
{
    1 => "een",
    2 => "twee"
};

Console.WriteLine(woord);
```

<details>
<summary><span style="color:#845ef7"><b>Toon het antwoord</b></span></summary>

<code style="color:#f03e3e">SwitchExpressionException</code> — het programma crasht.

Het compileert, met een waarschuwing dat niet elke waarde gedekt is. Als het
programma draait en `x` is 5, past er geen enkele regel, en dan weet C# niet wat
`woord` moet worden. Een laatste regel met `_` voorkomt dat.

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

## 5. Array

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
[hoofdstuk 16](#16-referentie-tegenover-waarde).

</details>

---

## 6. List

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
> LINQ-methodes uit hoofdstuk 11 doen het omgekeerde: die laten je
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

## 7. Dictionary

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

## 8. HashSet

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

## 9. Queue

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

## 10. Stack

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

## 11. LINQ

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
`gesorteerd`, niet in `namen`. Vergelijk met `Sort` in hoofdstuk 6, die de lijst
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

## 12. IEnumerable

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

## 13. class

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

### Eigenschappen die zichzelf uitrekenen: =>

Een eigenschap hoeft geen waarde te bewaren. Met `=>` rekent ze haar waarde uit,
telkens opnieuw wanneer je erom vraagt:

```csharp
public class Persoon
{
    public string Voornaam { get; set; } = "";
    public string Achternaam { get; set; } = "";

    public string VolledigeNaam => Voornaam + " " + Achternaam;
}
```

`VolledigeNaam` heeft geen `set`: je kan ze lezen, maar niet invullen. Verander
je de voornaam, dan verandert de volledige naam vanzelf mee. Dezelfde pijl kan
ook een korte methode schrijven: `public bool IsVolwassen() => Leeftijd >= 18;`
doet hetzelfde als de versie met `return` hierboven.

In les 07 kom je deze schrijfwijze tegen bij de eigenschappen van de Unit of
Work.

### Waar een klasse woont: namespace en using

Elke klasse staat in een **namespace**, een soort map voor code. Zo kunnen twee
klassen dezelfde naam hebben, zolang ze in een andere namespace staan.

```csharp
namespace School.Models;

public class Leerling
{
    // ...
}
```

Wil je die klasse in een ander bestand gebruiken, dan zeg je bovenaan met
`using` in welke map C# moet kijken:

```csharp
using School.Models;

var anna = new Leerling();
```

Je zal twee schrijfwijzen tegenkomen. `namespace School.Models;` met een
puntkomma geldt voor het hele bestand. De oudere vorm zet de klasse tussen
accolades: `namespace School.Models { ... }`. Ze doen hetzelfde.

Een paar namespaces staan in een nieuw project al klaar zonder dat je ze typt,
zoals `System`, `System.Linq` en `System.Collections.Generic`. Daarom kan je
`List` en `Where` gebruiken zonder één `using`.

### Een object zonder klasse: anonieme objecten

Soms wil je even een paar waarden samen doorgeven zonder er een klasse voor te
schrijven:

```csharp
var p = new { Naam = "Anna", Leeftijd = 17 };

Console.WriteLine(p.Naam);       // Anna
// p.Naam = "Bram";              compileert niet: enkel lezen
```

Het type heeft geen naam, daarom moet de variabele `var` zijn. In les 02 zie je
dit bij `CreatedAtAction(..., new { id = laptop.Id }, ...)`: dat kleine object
zegt welke waarde er in de route moet komen.

### Wat je ermee kan

| Vorm | Wat het doet | Voorbeeld | Resultaat |
|---|---|---|---|
| <code style="color:#228be6;font-weight:600">{ get; set; }</code> | een eigenschap die je kan lezen en invullen | `public int Leeftijd { get; set; }` | `anna.Leeftijd = 18;` mag |
| <code style="color:#228be6;font-weight:600">{ get; }</code> | enkel lezen; invullen kan alleen in de constructor | `public string Naam { get; }` | `anna.Naam = "x";` compileert niet |
| <code style="color:#228be6;font-weight:600">=&gt;</code> (eigenschap) | een eigenschap die zichzelf uitrekent | `public string VolledigeNaam => ...` | rekent telkens opnieuw |
| <code style="color:#228be6;font-weight:600">=&gt;</code> (methode) | een methode van één regel | `public bool IsVolwassen() => Leeftijd >= 18;` | zoals met `return` |
| <code style="color:#228be6;font-weight:600">new Klasse { ... }</code> | maak een object en vul meteen eigenschappen in | `new Leerling { Naam = "Anna" }` | een leerling met een naam |
| constructor | draait bij `new` en kan iets afdwingen | `public Leerling(string naam) { ... }` | `new Leerling()` compileert niet meer |
| <code style="color:#228be6;font-weight:600">namespace</code> | de map waarin een klasse woont | `namespace School.Models;` | volledige naam `School.Models.Leerling` |
| <code style="color:#228be6;font-weight:600">using</code> | zeg waar C# moet zoeken | `using School.Models;` | `Leerling` is bruikbaar |
| <code style="color:#228be6;font-weight:600">new { ... }</code> | een anoniem object | `new { id = 5 }` | enkel te lezen |

> [!WARNING]
> Krijg je de fout dat een type of namespace niet gevonden wordt, dan ontbreekt
> meestal een `using`. Visual Studio stelt die zelf voor: zet je cursor op de rode
> naam en druk op `Ctrl+.`.

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

**Vraag 3**

Deze vraag gebruikt de klasse `Persoon` van hierboven.

```csharp
var p = new Persoon { Voornaam = "Anna", Achternaam = "Peeters" };
p.Voornaam = "Bram";
Console.WriteLine(p.VolledigeNaam);
```

<details>
<summary><span style="color:#845ef7"><b>Toon het antwoord</b></span></summary>

<code style="color:#845ef7">Bram Peeters</code>

`VolledigeNaam` bewaart niets. Ze wordt pas samengesteld op het moment dat je
erom vraagt, en dan is de voornaam al Bram.

</details>

**Vraag 4**

```csharp
var p = new { Naam = "Anna" };
p.Naam = "Bram";
```

<details>
<summary><span style="color:#845ef7"><b>Toon het antwoord</b></span></summary>

<code style="color:#f03e3e">Compileert niet.</code>

De eigenschappen van een anoniem object kan je enkel lezen. Moet een waarde
kunnen veranderen, schrijf dan een echte klasse.

</details>

---

## 14. record

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
om DTO's te schrijven; zie [hoofdstuk 33](#33-dtos-en-mapster).

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

## 15. struct

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

Vergelijk dit met vraag 1 in hoofdstuk 13. Dezelfde drie regels, een tegengesteld
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

## 16. Referentie tegenover waarde

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

Test jezelf: kijk terug naar vraag 4 in hoofdstuk 5, vraag 1 in hoofdstuk 13 en
de vraag in hoofdstuk 15. Drie keer dezelfde drie regels, en het antwoord hangt
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

## 17. Omgaan met niets

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
| <code style="color:#228be6;font-weight:600">!</code> achter een waarde | zeg tegen de compiler: dit is niet null | `naam!.Length` | geen waarschuwing; is het tóch null, dan crasht het |
| <code style="color:#228be6;font-weight:600">= default!</code> | begin met niets, zonder waarschuwing | `public Stad Stad { get; set; } = default!;` | de eigenschap is null tot iemand ze invult |

Het vraagteken achter een type betekent: deze waarde mag ontbreken. Dat is de
manier waarop je in code het verschil vastlegt tussen "nul" en "niet ingevuld" —
precies het onderscheid waar een controle op stuk loopt als je het niet maakt.

### Het uitroepteken: vertrouw me

In een nieuw project waarschuwt de compiler zodra een referentietype zonder
vraagteken `null` zou kunnen zijn, zoals een `string`-eigenschap zonder beginwaarde.
Het uitroepteken zet die waarschuwing uit. Aan wat er gebeurt terwijl het programma
draait, verandert het niets: het is een belofte aan de compiler, geen controle.

In de cursus zie je het op twee plaatsen:

- `= default!` achter een eigenschap, vanaf les 06. De eigenschap begint als null,
  maar wordt later ingevuld, bijvoorbeeld door EF Core bij het ophalen. Zie
  [hoofdstuk 32](#32-ef-core-opvragen-en-bewaren). `= null!` betekent hetzelfde.
- `src.Product!.Naam` in een regel voor Mapster, in les 09. Daar zet het enkel de
  waarschuwing uit. Mapster kijkt zelf na of het product er is, en laat de naam
  leeg als het ontbreekt. Zie [hoofdstuk 33](#33-dtos-en-mapster).

In gewone code geldt: klopt de belofte niet, dan krijg je dezelfde
`NullReferenceException` als zonder uitroepteken, alleen zonder waarschuwing
vooraf.

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

**Vraag 6**

```csharp
string? naam = null;
Console.WriteLine(naam!.Length);
```

<details>
<summary><span style="color:#845ef7"><b>Toon het antwoord</b></span></summary>

<code style="color:#f03e3e">NullReferenceException</code> — het programma crasht.

Hetzelfde als bij vraag 3, maar nu zonder groene kronkellijn. Het uitroepteken
zegt de compiler dat `naam` niet null is. De compiler gelooft het, en zwijgt. Het
programma zelf kijkt niet na, en loopt vast op de lengte van iets dat er niet is.

</details>

---

## 18. enum

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

## 19. interface

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

Moeten verwante klassen niet alleen hetzelfde kunnen, maar ook gegevens of
uitgewerkte methodes delen, dan is een abstracte klasse het betere antwoord. Zie
[hoofdstuk 20](#20-overerving-en-abstracte-klassen).

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

## 20. Overerving en abstracte klassen

<span style="background:#2f9e44;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>EIGEN TYPE</b></span>

### Wat het is

**Een klasse die verder bouwt op een andere.** Zoals een basisrecept voor deeg:
pizza en brood beginnen allebei met hetzelfde deeg, en voegen er elk hun eigen
stappen aan toe. Wie het basisrecept aanpast, past het voor allebei aan.

De klasse waarop je verder bouwt, heet de **basisklasse**. De klasse die erop
verder bouwt, heet de **afgeleide klasse**. Die krijgt alles mee wat de
basisklasse heeft, en voegt er haar eigen dingen aan toe.

### Hoe het eruitziet

```csharp
public class Voertuig
{
    public int Wielen { get; set; }

    public string Toeter()
    {
        return "Tuut";
    }
}

public class Fiets : Voertuig
{
    public bool HeeftBel { get; set; }
}
```

Het dubbelpunt na `Fiets` betekent: *een fiets is een voertuig*. Een fiets heeft
dus ook `Wielen` en kan ook `Toeter()`, zonder dat die er opnieuw in staan:

```csharp
Fiets f = new Fiets { Wielen = 2, HeeftBel = true };

Console.WriteLine(f.Wielen);      // 2 — komt van Voertuig
Console.WriteLine(f.Toeter());    // Tuut — komt van Voertuig
Console.WriteLine(f.HeeftBel);    // True — van de fiets zelf

Voertuig v = f;                   // mag: een fiets ís een voertuig
Console.WriteLine(v.Wielen);      // 2
// v.HeeftBel compileert niet: via v zie je enkel wat elk voertuig heeft
```

### Een basisklasse die zelf nooit bestaat: abstract

Een "voertuig" op zich rijdt nergens rond. Wat op de weg staat, is altijd een
fiets, een bus of iets anders. Met `abstract` leg je dat vast: van de
basisklasse zelf kan je geen object meer maken, enkel van wat ervan erft.

```csharp
public abstract class Voertuig
{
    public int Wielen { get; }
    protected int Kilometers { get; set; }

    protected Voertuig(int wielen)
    {
        Wielen = wielen;
    }

    public void Rij(int km)
    {
        Kilometers += km;
    }

    public virtual string Toeter()
    {
        return "Tuut";
    }

    public abstract int MaxPassagiers();
}

public class Fiets : Voertuig
{
    public Fiets() : base(2) { }

    public override string Toeter()
    {
        return "Tring";
    }

    public override int MaxPassagiers()
    {
        return 1;
    }
}

public class Bus : Voertuig
{
    public Bus() : base(6) { }

    public override int MaxPassagiers()
    {
        return 50;
    }
}
```

Wat daar gebeurt:

- `abstract` vóór `class`: een object maken van `Voertuig` zelf compileert niet
  meer.
- `protected Voertuig(int wielen)` is de constructor van de basisklasse. Met
  `: base(2)` roept de fiets hem aan met twee wielen.
- `protected int Kilometers` is zichtbaar in `Voertuig` en in elke klasse die
  ervan erft, maar niet daarbuiten.
- `virtual` geeft `Toeter` een standaardversie die een afgeleide klasse mag
  vervangen. De fiets doet dat met `override`, de bus niet.
- `abstract` vóór een methode betekent: geen uitwerking. Elke afgeleide klasse
  moet ze zelf uitwerken, met `override`.

En het nut ervan:

```csharp
List<Voertuig> vloot = new List<Voertuig> { new Fiets(), new Bus() };

foreach (Voertuig v in vloot)
{
    Console.WriteLine($"{v.Wielen} wielen, {v.Toeter()}, max {v.MaxPassagiers()}");
}
// 2 wielen, Tring, max 1
// 6 wielen, Tuut, max 50
```

De lus weet niet of ze een fiets of een bus voor zich heeft. Dat hoeft ook niet:
elk voertuig heeft wielen, kan toeteren en weet hoeveel passagiers erin passen.
Het is hetzelfde idee als bij de interface in [hoofdstuk 19](#19-interface), met
één verschil: de basisklasse geeft ook gegevens en uitgewerkte methodes mee.

### Wat je ermee kan

| Vorm | Wat het doet | Voorbeeld | Resultaat |
|---|---|---|---|
| <code style="color:#228be6;font-weight:600">: Basisklasse</code> | erf van een klasse | `class Fiets : Voertuig` | een fiets heeft alles van een voertuig |
| <code style="color:#228be6;font-weight:600">: Basisklasse, IContract</code> | erf van één klasse en beloof daarnaast contracten | `class Fiets : Voertuig, IGeluid` | eerst de klasse, dan de interfaces |
| <code style="color:#228be6;font-weight:600">base(...)</code> | roep de constructor van de basisklasse aan | `public Fiets() : base(2) { }` | <code style="color:#845ef7">Wielen</code> is <code style="color:#845ef7">2</code> |
| <code style="color:#228be6;font-weight:600">protected</code> | zichtbaar in de klasse en in wie ervan erft, niet daarbuiten | `protected int Kilometers` | `fiets.Kilometers` compileert niet |
| <code style="color:#228be6;font-weight:600">virtual</code> | deze methode mag vervangen worden | `public virtual string Toeter()` | de bus gebruikt de standaardversie |
| <code style="color:#228be6;font-weight:600">override</code> | vervang een methode die `virtual` of `abstract` is | `public override string Toeter()` | <code style="color:#845ef7">"Tring"</code> |
| <code style="color:#228be6;font-weight:600">base.Methode()</code> | gebruik de versie van de basisklasse, binnen je eigen versie | `return base.Toeter() + "!";` | <code style="color:#845ef7">"Tuut!"</code> |
| <code style="color:#228be6;font-weight:600">abstract class</code> | een klasse waarvan je geen object kan maken | `new Voertuig(4)` | compileert niet |
| <code style="color:#228be6;font-weight:600">abstract</code> (methode) | een methode zonder uitwerking; elke afgeleide klasse moet ze uitwerken | `public abstract int MaxPassagiers();` | vergeet je ze, dan compileert het niet |
| <code style="color:#228be6;font-weight:600">sealed</code> | van deze klasse mag niemand meer erven | `public sealed class Bus : Voertuig` | `class Dubbeldekker : Bus` compileert niet |
| <code style="color:#228be6;font-weight:600">is</code> | is dit object van dat type, of ervan afgeleid | `new Fiets() is Voertuig` | <code style="color:#845ef7">true</code> |
| <code style="color:#228be6;font-weight:600">GetType</code> | het echte type van het object, niet dat van de variabele | `Voertuig v = new Fiets();` en dan `v.GetType().Name` | <code style="color:#845ef7">"Fiets"</code> |

### Abstracte klasse tegenover interface

| | Abstracte klasse | Interface |
|---|---|---|
| **Zegt** | wat iets **is** | wat iets **kan** |
| **Bewaart gegevens** | ja | nee — ze kan enkel zeggen dát er een eigenschap moet zijn |
| **Uitgewerkte methodes** | ja, naast de abstracte | nee, op een zeldzame uitzondering na die je in deze cursus niet tegenkomt |
| **Constructor** | ja | nee |
| **Hoeveel per klasse** | één | zoveel je wil |
| **Een object van maken** | nee | nee |
| **Typisch gebruik** | verwante klassen die gegevens en werk delen: een fiets en een bus | klassen die hetzelfde moeten kunnen, ook als ze niets met elkaar te maken hebben: een hond en een deurbel maken allebei geluid |

### Waar je dit in de cursus tegenkomt

| Les | Wat je ziet | Wat er gebeurt |
|---|---|---|
| 01 | een controller die erft van `ControllerBase` | je controller erft `Ok`, `NotFound` en de rest uit [hoofdstuk 26](#26-de-helpers-van-een-controller) |
| 04 | `: base(options)` in je context | de opties gaan door naar de constructor van `DbContext`; zie [hoofdstuk 31](#31-ef-core-model-en-database) |
| 04 | `protected override void OnModelCreating(...)` | je vervangt een methode die `DbContext` daarvoor openzet |
| 07 | een specifieke repository die erft van de generieke | ze krijgt alle gewone methodes mee; `protected` maakt de context ook voor haar zichtbaar |

### Wanneer wel, wanneer niet

<span style="color:#37b24d"><b>Wel</b></span> — verwante klassen die echt hetzelfde zijn en gegevens of
uitgewerkte methodes delen. De test: klinkt *een X is een Y* juist? Een fiets is
een voertuig: ja. Maak de basisklasse `abstract` zodra ze op zichzelf niets
betekent.

<span style="color:#f03e3e"><b>Niet</b></span> — enkel om een paar methodes te hergebruiken tussen klassen die
niets met elkaar te maken hebben. Een leerling is geen klaslokaal, ook al hebben
ze allebei een naam. En gaat het enkel om wat iets kan, dan is een interface
lichter.

### Veelgemaakte fouten

> [!WARNING]
> - Wat `private` is in de basisklasse, kan de afgeleide klasse niet zien. Heeft
>   ze het nodig, maak het dan `protected` — niet `public`, want dan kan iedereen
>   erbij.
> - Heeft de basisklasse enkel een constructor met parameters, dan moet elke
>   afgeleide klasse `: base(...)` aanroepen. Vergeet je dat, dan compileert het
>   niet.
> - `override` kan enkel op een methode die `virtual` of `abstract` is. Schrijf je
>   in de afgeleide klasse gewoon een methode met dezelfde naam, dan vervang je
>   niets: je verstopt de oude alleen. Visual Studio waarschuwt je, maar het
>   compileert. Zie vraag 4.
> - Een klasse erft van hoogstens één klasse. Interfaces mag je er zoveel bij
>   zetten als je wil.
> - Via een variabele van de basisklasse zie je enkel wat de basisklasse heeft,
>   ook als het object meer kan. Met `is` uit [hoofdstuk 22](#22-types-herkennen)
>   ga je na wat het echt is.

### Voorspel de uitkomst

Vragen 1, 2, 3 en 6 gebruiken de tweede versie van `Voertuig`, met `abstract`,
en de klassen `Fiets` en `Bus` die erbij horen.

**Vraag 1**

```csharp
Voertuig v = new Fiets();
Console.WriteLine(v.Toeter());
```

<details>
<summary><span style="color:#845ef7"><b>Toon het antwoord</b></span></summary>

<code style="color:#845ef7">Tring</code>

De variabele zegt enkel "een voertuig", maar het object is een fiets. Omdat
`Toeter` `virtual` is en de fiets ze met `override` vervangt, telt de versie van
het object, niet die van de variabele.

</details>

**Vraag 2**

```csharp
Voertuig v = new Bus();
Console.WriteLine(v.Toeter());
```

<details>
<summary><span style="color:#845ef7"><b>Toon het antwoord</b></span></summary>

<code style="color:#845ef7">Tuut</code>

De bus vervangt `Toeter` niet. Ze gebruikt dus de standaardversie uit
`Voertuig`. Een `virtual` methode mag je vervangen, je moet niet.

</details>

**Vraag 3**

```csharp
Voertuig v = new Voertuig(4);
```

<details>
<summary><span style="color:#845ef7"><b>Toon het antwoord</b></span></summary>

<code style="color:#f03e3e">Compileert niet.</code>

`Voertuig` is `abstract`. Van de basisklasse zelf kan je geen object maken, enkel
van een klasse die ervan erft: een fiets of een bus.

</details>

**Vraag 4**

```csharp
public class Basis
{
    public string Wie()
    {
        return "basis";
    }
}

public class Afgeleid : Basis
{
    public string Wie()
    {
        return "afgeleid";
    }
}

Basis b = new Afgeleid();
Afgeleid a = new Afgeleid();
Console.WriteLine(b.Wie());
Console.WriteLine(a.Wie());
```

<details>
<summary><span style="color:#845ef7"><b>Toon het antwoord</b></span></summary>

<code style="color:#845ef7">basis</code> en daarna <code style="color:#845ef7">afgeleid</code>

Twee keer hetzelfde object, twee verschillende antwoorden. Zonder `virtual` en
`override` vervangt `Afgeleid` de methode niet, maar verstopt ze haar enkel.
Welke versie je krijgt, hangt dan af van de variabele, niet van het object. Zet
`virtual` bij `Basis` en `override` bij `Afgeleid`, en je krijgt twee keer
`afgeleid`.

</details>

**Vraag 5**

```csharp
public class A
{
    public A()
    {
        Console.WriteLine("A");
    }
}

public class B : A
{
    public B()
    {
        Console.WriteLine("B");
    }
}

new B();
```

<details>
<summary><span style="color:#845ef7"><b>Toon het antwoord</b></span></summary>

<code style="color:#845ef7">A</code> en daarna <code style="color:#845ef7">B</code>

Eerst de fundering, dan de muren. Vóór de constructor van `B` draait die van de
basisklasse. Staat er geen `: base(...)`, dan roept C# zelf de constructor zonder
parameters van `A` aan.

</details>

**Vraag 6**

```csharp
Voertuig v = new Fiets();
Console.WriteLine(v is Voertuig);
Console.WriteLine(v.GetType() == typeof(Voertuig));
Console.WriteLine(v.GetType().Name);
```

<details>
<summary><span style="color:#845ef7"><b>Toon het antwoord</b></span></summary>

<code style="color:#845ef7">True</code>, <code style="color:#845ef7">False</code> en daarna <code style="color:#845ef7">Fiets</code>

`is` vraagt: *is dit een voertuig, of iets dat ervan erft?* Een fiets is dat.
`GetType()` geeft het echte type van het object, en dat is precies `Fiets`, niet
`Voertuig`. Wil je weten of iets tot een familie behoort, gebruik dan `is`.

</details>

---

## 21. Generics

<span style="background:#2f9e44;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>EIGEN TYPE</b></span>

> [!NOTE]
> Zelf generics schrijven komt aan bod in les 07 van Programming Advanced. Het
> staat hier al, zodat je het klaar hebt wanneer je het nodig hebt.

### Wat het is

**Een type met een open plaats.** Zoals een doos met een etiket *inhoud: ___*. De
doos werkt hetzelfde voor schoenen, boeken of speelgoed; pas wanneer je hem
gebruikt, vul je in wat erin gaat.

Je gebruikt het al sinds [hoofdstuk 6](#6-list). `List<T>` is één klasse die werkt
voor `List<string>`, `List<int>` en `List<Leerling>`. De `T` is de open plaats. In
dit hoofdstuk maak je zelf zo'n type.

### Hoe het eruitziet

```csharp
public class Doos<T>
{
    private T _inhoud;

    public Doos(T inhoud)
    {
        _inhoud = inhoud;
    }

    public T Open()
    {
        return _inhoud;
    }
}
```

Overal waar `T` staat, komt straks het type dat je invult:

```csharp
Doos<string> brief = new Doos<string>("Proficiat!");
Doos<int> getal = new Doos<int>(42);

string tekst = brief.Open();      // Proficiat!
int n = getal.Open();             // 42 — een echt getal, je kan ermee rekenen

// new Doos<int>("42") compileert niet: dat is een tekst, geen getal
```

Ook een losse methode kan een open plaats hebben:

```csharp
T Eerste<T>(List<T> lijst)
{
    return lijst[0];
}

string s = Eerste(new List<string> { "kiwi", "peer" });   // kiwi
int g = Eerste(new List<int> { 7, 3 });                   // 7
```

C# leidt zelf af wat `T` is uit wat je meegeeft. Je hoeft `Eerste<string>(...)`
niet uit te schrijven, al mag het.

### De open plaats beperken: where

Zonder meer weet C# niets over `T`. Het kan een getal zijn, een tekst of een
leerling, dus je mag er niets mee doen wat niet voor álle types werkt. Met `where`
beperk je wat er ingevuld mag worden — en daar krijg je iets voor terug:

```csharp
public class Garage<T> where T : Voertuig
{
    private List<T> _voertuigen = new List<T>();

    public void Zet(T voertuig)
    {
        _voertuigen.Add(voertuig);
    }

    public int TotaalWielen()
    {
        return _voertuigen.Sum(v => v.Wielen);   // mag: elke T is een voertuig
    }
}

Garage<Fiets> stalling = new Garage<Fiets>();    // mag: een fiets is een voertuig
```

`Voertuig` en `Fiets` komen uit [hoofdstuk 20](#20-overerving-en-abstracte-klassen).
Zonder `where T : Voertuig` zou `v.Wielen` niet compileren, want dan kan `T` ook
een tekst zijn.

### Wat je ermee kan

| Vorm | Wat het doet | Voorbeeld | Resultaat |
|---|---|---|---|
| <code style="color:#228be6;font-weight:600">class Naam&lt;T&gt;</code> | een klasse met een open plaats | `class Doos<T>` | `Doos<int>`, `Doos<string>`, … |
| <code style="color:#228be6;font-weight:600">Methode&lt;T&gt;(...)</code> | een methode met een open plaats | `T Eerste<T>(List<T> lijst)` | `Eerste(namen)` geeft een tekst |
| <code style="color:#228be6;font-weight:600">interface INaam&lt;T&gt;</code> | een contract met een open plaats | `interface IHouder<T>` | elk type belooft het voor zijn eigen `T` |
| <code style="color:#228be6;font-weight:600">class X&lt;T&gt; : IY&lt;T&gt;</code> | geef de open plaats door aan het contract | `class Doos<T> : IHouder<T>` | de doos belooft het contract, voor elke `T` |
| <code style="color:#228be6;font-weight:600">class X : IY&lt;int&gt;</code> | vul de open plaats meteen in | `class Spaarpot : IHouder<int>` | enkel voor getallen |
| <code style="color:#228be6;font-weight:600">&lt;T1, T2&gt;</code> | meerdere open plaatsen | `class Paar<TLinks, TRechts>` | zoals `Dictionary<TKey, TValue>` |
| <code style="color:#228be6;font-weight:600">where T : class</code> | `T` moet een referentietype zijn | `class Doos<T> where T : class` | `Doos<int>` compileert niet |
| <code style="color:#228be6;font-weight:600">where T : struct</code> | `T` moet een waardetype zijn | `class Doos<T> where T : struct` | `Doos<string>` compileert niet |
| <code style="color:#228be6;font-weight:600">where T : Basisklasse</code> | `T` moet die klasse zijn, of ervan erven | `where T : Voertuig` | binnenin mag je `Wielen` gebruiken |
| <code style="color:#228be6;font-weight:600">where T : IContract</code> | `T` moet dat contract beloven | `where T : IGeluid` | binnenin mag je `MaakGeluid()` aanroepen |
| <code style="color:#228be6;font-weight:600">where T : new()</code> | `T` moet een constructor zonder parameters hebben | `where T : new()` | binnenin mag je `new T()` schrijven |
| <code style="color:#228be6;font-weight:600">default(T)</code> | de standaardwaarde van `T` | `default(int)` | <code style="color:#845ef7">0</code>; bij een klasse <code style="color:#845ef7">null</code>, bij een bool <code style="color:#845ef7">false</code> |
| <code style="color:#228be6;font-weight:600">typeof(T)</code> | het type dat ingevuld werd | `typeof(T).Name` in een `Doos<int>` | <code style="color:#845ef7">"Int32"</code> |

Meerdere beperkingen zet je achter elkaar, gescheiden door een komma:
`where T : class, new()`.

> [!NOTE]
> `string` is een klasse, en dus een referentietype. Bij `where T : class` mag je
> `string` dus wél invullen. Enkel waardetypes zoals `int`, `double` en `bool`
> vallen erbuiten.

### Waarom dit in je cursus zo belangrijk is

In les 07 van Programming Advanced schrijf je één repository die werkt voor al je
entiteiten, in plaats van één per entiteit. De open plaats is de entiteit; de
cursus noemt ze `TEntity` in plaats van `T`. De beperking `where TEntity : class`
staat erbij omdat EF Core enkel met klassen werkt. Specifieke repositories erven
daarna van die generieke, zoals in [hoofdstuk 20](#20-overerving-en-abstracte-klassen). Hoe zo'n repository
aan de juiste tabel komt, staat in [hoofdstuk 32](#32-ef-core-opvragen-en-bewaren).

### Wanneer wel, wanneer niet

<span style="color:#37b24d"><b>Wel</b></span> — dezelfde code werkt voor meerdere types, en enkel het type
verschilt. Zonder generics zou je dezelfde klasse per type kopiëren.

<span style="color:#f03e3e"><b>Niet</b></span> — wanneer de code per type iets anders moet doen. Schrijf je in een
generieke methode `if (typeof(T) == typeof(int))`, dan heb je geen open plaats
nodig maar aparte methodes, of overerving.

### Veelgemaakte fouten

> [!WARNING]
> - Zonder `where` kan je binnen de klasse niets specifieks met `T` doen.
>   `voertuig.Wielen` compileert pas met `where T : Voertuig`, ook al stop je er
>   in de praktijk enkel voertuigen in.
> - `return null;` compileert niet als `T` ook een getal kan zijn. Gebruik
>   `default`, of beperk `T` met `where T : class`.
> - `Doos<Fiets>` is geen `Doos<Voertuig>`, ook al is een fiets een voertuig.
>   Hetzelfde geldt voor `List<Fiets>` en `List<Voertuig>`. Zie vraag 4.
> - Volgens de afspraak begint de naam van een open plaats met een `T`: `T`,
>   `TSleutel`, `TEntity`.

### Voorspel de uitkomst

De vragen gebruiken `Doos<T>` en `Garage<T>` van hierboven, en `Voertuig` en
`Fiets` uit hoofdstuk 20.

**Vraag 1**

```csharp
var a = new Doos<int>(5);
var b = new Doos<string>("5");
Console.WriteLine(a.Open() + 1);
Console.WriteLine(b.Open() + 1);
```

<details>
<summary><span style="color:#845ef7"><b>Toon het antwoord</b></span></summary>

<code style="color:#845ef7">6</code> en daarna <code style="color:#845ef7">51</code>

Dezelfde klasse, twee verschillende types. De eerste doos geeft een echt getal
terug, en 5 + 1 is 6. De tweede geeft een tekst terug, en een tekst + 1 plakt de
1 erachter.

</details>

**Vraag 2**

```csharp
Console.WriteLine(default(int));
Console.WriteLine(default(bool));
Console.WriteLine(default(string) == null);
```

<details>
<summary><span style="color:#845ef7"><b>Toon het antwoord</b></span></summary>

<code style="color:#845ef7">0</code>, <code style="color:#845ef7">False</code> en daarna <code style="color:#845ef7">True</code>

Elk type heeft een standaardwaarde: nul voor getallen, `false` voor een bool en
`null` voor referentietypes. Het is dezelfde waarde die `FirstOrDefault` uit
[hoofdstuk 11](#11-linq) teruggeeft wanneer het niets vindt.

</details>

**Vraag 3**

```csharp
var g = new Garage<string>();
```

<details>
<summary><span style="color:#845ef7"><b>Toon het antwoord</b></span></summary>

<code style="color:#f03e3e">Compileert niet.</code>

`Garage<T>` eist met `where T : Voertuig` dat `T` een voertuig is. Een tekst is
dat niet. De fout valt bij het bouwen, niet pas wanneer het programma draait.

</details>

**Vraag 4**

```csharp
Doos<Voertuig> d = new Doos<Fiets>(new Fiets());
```

<details>
<summary><span style="color:#845ef7"><b>Toon het antwoord</b></span></summary>

<code style="color:#f03e3e">Compileert niet.</code>

Voor C# zijn `Doos<Fiets>` en `Doos<Voertuig>` twee aparte types, zonder verband.
Dat een fiets een voertuig is, maakt een doos met een fiets nog geen doos met een
voertuig. Wat wel mag: een `Doos<Voertuig>` maken en daar een fiets in stoppen,
met `new Doos<Voertuig>(new Fiets())`.

</details>

**Vraag 5**

```csharp
string Soort<T>(T waarde)
{
    return typeof(T).Name;
}

Console.WriteLine(Soort(42));
Console.WriteLine(Soort("hallo"));
```

<details>
<summary><span style="color:#845ef7"><b>Toon het antwoord</b></span></summary>

<code style="color:#845ef7">Int32</code> en daarna <code style="color:#845ef7">String</code>

C# leidt `T` af uit wat je meegeeft: eerst een `int`, daarna een `string`. Dat de
namen er anders uitzien, komt doordat `int` en `string` korte namen zijn voor
`Int32` en `String`. Het zijn dezelfde types.

</details>

---

## 22. Types herkennen

<span style="background:#d9822b;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>METHODES</b></span>

De voorbeelden gebruiken `IGeluid`, `Hond` en `Kat` uit hoofdstuk 19.

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

## 23. Uitzonderingen

<span style="background:#7048e8;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>CONCEPT</b></span>

### Wat het is

**Een noodrem.** Wanneer een methode iets gevraagd wordt dat ze niet kan — een
tekst omzetten die geen getal is, een sleutel opzoeken die er niet is — dan stopt
ze meteen en gooit ze een **uitzondering**. Die gaat terug naar wie de methode
aanriep, en naar wie díe aanriep, tot iemand ze opvangt. Vangt niemand ze op, dan
stopt het hele programma.

Je bent ze in dit leerboek al vaak tegengekomen, in het rood. Dit hoofdstuk gaat
over wat je ermee doet.

### Hoe het eruitziet

```csharp
try
{
    int leeftijd = int.Parse("twaalf");
    Console.WriteLine($"Leeftijd: {leeftijd}");
}
catch (FormatException)
{
    Console.WriteLine("Dat is geen getal.");
}
finally
{
    Console.WriteLine("Klaar.");
}
```

`int.Parse` gooit een `FormatException`. De rest van het `try`-blok wordt
overgeslagen, het `catch`-blok vangt de uitzondering op, en `finally` draait
altijd — fout of geen fout. Op het scherm komt `Dat is geen getal.` en daarna
`Klaar.`

Je kan ook zelf een uitzondering gooien, wanneer je methode iets gevraagd wordt
dat ze niet kan of mag doen:

```csharp
void ZetThermostaat(int graden)
{
    if (graden < 5 || graden > 30)
    {
        throw new ArgumentException("Kies tussen 5 en 30 graden.");
    }

    Console.WriteLine($"Thermostaat op {graden} graden.");
}

try
{
    ZetThermostaat(40);
}
catch (ArgumentException ex)
{
    Console.WriteLine(ex.Message);     // Kies tussen 5 en 30 graden.
}
```

`throw` werkt zoals `return`: de methode stopt daar. Het verschil is dat er geen
waarde teruggaat maar een uitzondering, en dat wie de methode aanriep ze moet
opvangen — of zelf ook stopt.

### Wat je ermee kan

| Vorm | Wat het doet | Voorbeeld | Resultaat |
|---|---|---|---|
| <code style="color:#228be6;font-weight:600">try</code> | probeer dit blok | `try { ... }` | loopt tot de eerste uitzondering |
| <code style="color:#228be6;font-weight:600">catch (Soort)</code> | vang deze soort op | `catch (FormatException)` | het programma loopt verder na het blok |
| <code style="color:#228be6;font-weight:600">catch (Soort ex)</code> | vang op en geef ze een naam | `catch (FormatException ex)` | `ex` is bruikbaar in het blok |
| <code style="color:#228be6;font-weight:600">catch (Exception)</code> | vang elke soort op | `catch (Exception ex)` | vangt alles, ook wat je niet verwachtte |
| <code style="color:#228be6;font-weight:600">finally</code> | draait altijd, fout of geen fout | `finally { ... }` | ook na een `return` in het `try`-blok |
| <code style="color:#228be6;font-weight:600">throw new</code> | gooi zelf een uitzondering | `throw new ArgumentException("Te warm.")` | de methode stopt hier |
| <code style="color:#228be6;font-weight:600">throw;</code> | gooi dezelfde uitzondering verder, binnen een `catch` | `catch (Exception) { ...; throw; }` | wie hoger staat, krijgt ze alsnog |
| <code style="color:#228be6;font-weight:600">Message</code> | de uitleg bij de uitzondering | `ex.Message` | <code style="color:#845ef7">"Te warm."</code> |
| <code style="color:#228be6;font-weight:600">GetType</code> | welke soort het is | `ex.GetType().Name` | <code style="color:#845ef7">"FormatException"</code> |

### De uitzonderingen die je al kent

| Uitzondering | Wanneer | Hoofdstuk |
|---|---|---|
| <code style="color:#f03e3e">FormatException</code> | `int.Parse` op een tekst die geen getal is | [2](#2-getallen-en-omzetten) |
| <code style="color:#f03e3e">IndexOutOfRangeException</code> | een positie buiten een array | [5](#5-array) |
| <code style="color:#f03e3e">ArgumentOutOfRangeException</code> | een positie buiten een `List`, of `Substring` buiten de tekst | [1](#1-tekst), [6](#6-list) |
| <code style="color:#f03e3e">ArgumentException</code> | `Add` met een sleutel die al in een `Dictionary` zit | [7](#7-dictionary) |
| <code style="color:#f03e3e">KeyNotFoundException</code> | `[sleutel]` die niet bestaat | [7](#7-dictionary) |
| <code style="color:#f03e3e">InvalidOperationException</code> | een lijst veranderen in een `foreach`, `Dequeue` of `Pop` op iets leegs, `First` zonder resultaat, `Single` met twee | [6](#6-list), [9](#9-queue), [10](#10-stack), [11](#11-linq) |
| <code style="color:#f03e3e">NullReferenceException</code> | iets opvragen van `null` | [17](#17-omgaan-met-niets) |
| <code style="color:#f03e3e">FileNotFoundException</code> | `File.ReadAllText` op een bestand dat niet bestaat | [30](#30-json-en-bestanden) |
| <code style="color:#f03e3e">JsonException</code> | `JsonSerializer.Deserialize` op tekst die geen geldige JSON is | [30](#30-json-en-bestanden) |
| <code style="color:#f03e3e">NotImplementedException</code> | een methode die nog niet uitgewerkt is | les 05 van de cursus |

Ontbreekt niet alleen het bestand maar ook de map waarin het moet staan, dan
krijg je een `DirectoryNotFoundException` in plaats van een
`FileNotFoundException`.

### Voorkomen tegenover opvangen

Veel uitzonderingen kan je voorkomen door eerst te kijken. Dat is bijna altijd
beter: het zegt duidelijker wat je bedoelt, en een `catch` die te veel opvangt,
verbergt fouten die je had moeten zien.

| In plaats van opvangen | Kijk vooraf met | Hoofdstuk |
|---|---|---|
| `FormatException` van `Parse` | `TryParse` | [2](#2-getallen-en-omzetten) |
| `KeyNotFoundException` | `TryGetValue` of `ContainsKey` | [7](#7-dictionary) |
| `InvalidOperationException` van `First` | `FirstOrDefault` en een controle op `null` | [11](#11-linq) |
| `NullReferenceException` | `?.` of `is null` | [17](#17-omgaan-met-niets) |

Opvangen blijft nodig bij alles wat van buiten je programma komt: een bestand, een
netwerk, een database. Of een bestand er nog staat op het moment dat je het leest,
en of de inhoud geldige JSON is, kan je vooraf niet zeker weten.

### De volgorde van je catch-blokken

Alle uitzonderingen erven van `Exception` — overerving uit
[hoofdstuk 20](#20-overerving-en-abstracte-klassen). Daarom vangt
`catch (Exception)` elke soort op. Zet je meerdere `catch`-blokken onder elkaar,
dan loopt C# ze van boven naar onder af en neemt het het eerste dat past. De
specifieke soorten komen dus eerst, de algemene als laatste:

```csharp
try
{
    int aantal = int.Parse(invoer);
    Console.WriteLine(100 / aantal);
}
catch (FormatException)
{
    Console.WriteLine("Geef een getal.");
}
catch (DivideByZeroException)
{
    Console.WriteLine("Nul mag niet.");
}
catch (Exception ex)
{
    Console.WriteLine($"Onverwacht: {ex.Message}");
}
```

### In je API

Een uitzondering die in een controller niemand opvangt, legt niet de hele server
plat. ASP.NET Core vangt ze op het laatste moment op en stuurt
<code style="color:#f03e3e;font-weight:600">500 Internal Server Error</code> terug — dat zag je in vraag 3 van
[hoofdstuk 26](#26-de-helpers-van-een-controller). Voor de client betekent 500:
*de server heeft een fout gemaakt*. Lag de fout eigenlijk bij de client — een Id
dat niet bestaat, een ongeldige waarde — dan hoort daar een 404 of een 400 bij.

### Wanneer wel, wanneer niet

<span style="color:#37b24d"><b>Wel</b></span> — `try` en `catch` bij alles wat van buiten je programma komt en
wat je vooraf niet volledig kan nakijken: bestanden, netwerk, een database, JSON
van elders. En `throw` wanneer je methode iets gevraagd wordt dat niet kan, en ze
zelf niet weet wat ze dan moet doen.

<span style="color:#f03e3e"><b>Niet</b></span> — als vervanger van een controle die je vooraf kan doen. En nooit om
een fout stil te laten verdwijnen.

### Veelgemaakte fouten

> [!WARNING]
> - Een lege `catch { }` slikt de fout in. Het programma loopt verder alsof er
>   niets gebeurd is, met verkeerde of ontbrekende gegevens, en niemand weet
>   waarom.
> - `catch (Exception)` vangt ook de fouten die je niet verwachtte, zoals een
>   `NullReferenceException` uit je eigen code. Vang op wat je verwacht en waar je
>   iets zinnigs mee kan doen.
> - Een variabele die je in het `try`-blok maakt, bestaat niet meer na het blok.
>   Heb je hem nadien nodig, maak hem dan vóór de `try`.
> - Code die in hetzelfde blok na een `throw` staat, wordt nooit uitgevoerd.

### Voorspel de uitkomst

**Vraag 1**

```csharp
try
{
    Console.WriteLine("A");
    int.Parse("x");
    Console.WriteLine("B");
}
catch (FormatException)
{
    Console.WriteLine("C");
}
finally
{
    Console.WriteLine("D");
}
Console.WriteLine("E");
```

<details>
<summary><span style="color:#845ef7"><b>Toon het antwoord</b></span></summary>

<code style="color:#845ef7">A</code>, <code style="color:#845ef7">C</code>, <code style="color:#845ef7">D</code> en daarna <code style="color:#845ef7">E</code>

`A` komt nog op het scherm. Daarna gooit `int.Parse` een uitzondering, en de rest
van het `try`-blok — de `B` — wordt overgeslagen. Het `catch`-blok toont `C`,
`finally` toont `D`, en omdat de uitzondering opgevangen is, loopt het programma
gewoon verder met `E`.

</details>

**Vraag 2**

```csharp
try
{
    var d = new Dictionary<string, int>();
    Console.WriteLine(d["x"]);
}
catch (FormatException)
{
    Console.WriteLine("opgevangen");
}
```

<details>
<summary><span style="color:#845ef7"><b>Toon het antwoord</b></span></summary>

<code style="color:#f03e3e">KeyNotFoundException</code> — het programma crasht.

De sleutel `"x"` bestaat niet, en dat geeft een `KeyNotFoundException`. Het
`catch`-blok vangt enkel een `FormatException` op. Een andere soort gaat er
gewoon langs.

</details>

**Vraag 3**

```csharp
try
{
    int.Parse("x");
}
catch (Exception)
{
    Console.WriteLine("algemeen");
}
catch (FormatException)
{
    Console.WriteLine("formaat");
}
```

<details>
<summary><span style="color:#845ef7"><b>Toon het antwoord</b></span></summary>

<code style="color:#f03e3e">Compileert niet.</code>

`FormatException` erft van `Exception`. Het eerste blok vangt dus al alles op, en
het tweede kan nooit aan de beurt komen. C# weigert dat. Draai de volgorde om: de
specifieke soort eerst, de algemene als laatste.

</details>

**Vraag 4**

```csharp
try
{
    throw new InvalidOperationException("Rij is leeg");
}
catch (Exception ex)
{
    Console.WriteLine(ex.GetType().Name);
    Console.WriteLine(ex.Message);
}
```

<details>
<summary><span style="color:#845ef7"><b>Toon het antwoord</b></span></summary>

<code style="color:#845ef7">InvalidOperationException</code> en daarna <code style="color:#845ef7">Rij is leeg</code>

`catch (Exception ex)` vangt elke soort op, maar `ex` blijft wat het echt is. Net
als bij de fiets in hoofdstuk 20 geeft `GetType()` het echte type, niet dat van
de variabele.

</details>

**Vraag 5**

```csharp
string Test()
{
    try
    {
        return "uit try";
    }
    finally
    {
        Console.WriteLine("finally");
    }
}

Console.WriteLine(Test());
```

<details>
<summary><span style="color:#845ef7"><b>Toon het antwoord</b></span></summary>

<code style="color:#845ef7">finally</code> en daarna <code style="color:#845ef7">uit try</code>

De `return` legt het antwoord klaar, maar vóór de methode echt stopt, draait
`finally` nog. Pas daarna komt het antwoord bij `Console.WriteLine` aan. Dat is
wat *altijd* betekent.

</details>

**Vraag 6**

```csharp
void Binnen()
{
    throw new ArgumentException("mis");
}

void Midden()
{
    Binnen();
    Console.WriteLine("Midden klaar");
}

try
{
    Midden();
}
catch (ArgumentException ex)
{
    Console.WriteLine($"Opgevangen: {ex.Message}");
}
```

<details>
<summary><span style="color:#845ef7"><b>Toon het antwoord</b></span></summary>

<code style="color:#845ef7">Opgevangen: mis</code>

`Binnen` gooit, en `Midden` vangt niets op. De uitzondering gaat dus meteen verder
naar boven, en `Midden klaar` komt nooit op het scherm. Pas het `catch`-blok
helemaal bovenaan vangt ze op.

</details>

---

## 24. static, const en readonly

<span style="background:#7048e8;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>CONCEPT</b></span>

### Wat het is

**Wat bij de klasse hoort, en wat bij één object.** Denk aan een school. Elk
klaslokaal heeft zijn eigen bord: wat erop staat, verschilt per lokaal. Maar er
is maar één schoolbel, en als die gaat, hoort iedereen hem.

Een gewoon veld is het bord: elk object heeft er zijn eigen exemplaar van. Een
`static` veld is de bel: er is er één, en die hoort bij de klasse zelf.

Daarnaast zijn er twee manieren om te zeggen dat iets niet meer mag veranderen:
`const` en `readonly`.

### Hoe het eruitziet

```csharp
public class Teller
{
    public static int Totaal = 0;    // één voor alle tellers samen
    public int Eigen = 0;            // één per teller

    public void Tik()
    {
        Totaal++;
        Eigen++;
    }
}
```

```csharp
var a = new Teller();
var b = new Teller();

a.Tik();
a.Tik();
b.Tik();

Console.WriteLine(Teller.Totaal);   // 3 — via de klasse, niet via a of b
Console.WriteLine(a.Eigen);         // 2
Console.WriteLine(b.Eigen);         // 1
```

Je kent `static` al zonder het te weten. `Math.Max`, `int.Parse` en
`Console.WriteLine` roep je op via de naam van de klasse, zonder eerst een object
te maken. Dat zijn `static` methodes.

En zo zien `const` en `readonly` eruit:

```csharp
public class Bestelling
{
    public const int MaxAantal = 10;                 // ligt vast bij het compileren

    private readonly DateTime _aangemaakt;            // ligt vast na de constructor

    public Bestelling()
    {
        _aangemaakt = DateTime.Now;                  // hier mag het nog
    }

    public void Wijzig()
    {
        // _aangemaakt = DateTime.Now;                compileert niet
    }
}
```

### Waarom dit in je cursus zo belangrijk is

**Les 02.** De lijst in de controller is daar `private static List<Laptop>`. ASP.NET
Core maakt voor elk verzoek een nieuw controller-object. Zonder `static` hoort de
lijst bij dat ene object en begint elk verzoek met een verse lijst: wat je met een
POST toevoegde, is bij de volgende GET verdwenen. Met `static` hoort de lijst bij
de klasse, en blijft ze bestaan zolang de applicatie draait.

**Vanaf les 03.** Een repository of logger die je via de constructor krijgt, bewaar
je in een `private readonly` veld. Zo kan niemand dat veld later in de klasse nog
vervangen. Hoe dat precies werkt, staat in
[hoofdstuk 27](#27-programcs-en-dependency-injection).

### Wat je ermee kan

| Vorm | Wat het doet | Voorbeeld | Resultaat |
|---|---|---|---|
| <code style="color:#228be6;font-weight:600">static</code> veld | één waarde voor de hele klasse | `public static int Totaal;` | alle objecten delen dezelfde |
| <code style="color:#228be6;font-weight:600">static</code> methode | oproepen zonder object | `Teller.Reset()` | zoals `Math.Max` |
| <code style="color:#228be6;font-weight:600">static class</code> | een klasse met enkel `static` leden | `public static class Omrekenen` | `new Omrekenen()` compileert niet |
| Klasse.Lid | een `static` lid gebruik je via de klassenaam | `Teller.Totaal` | <code style="color:#845ef7">3</code> |
| <code style="color:#228be6;font-weight:600">const</code> | een vaste waarde, al bekend bij het compileren | `const int MaxAantal = 10;` | kan nooit veranderen |
| <code style="color:#228be6;font-weight:600">readonly</code> | invullen bij de declaratie of in de constructor, daarna nooit meer | `private readonly ILogger _logger;` | toewijzen in een andere methode compileert niet |
| <code style="color:#228be6;font-weight:600">static readonly</code> | één vaste waarde voor de hele klasse, die geen `const` kan zijn | `static readonly DateTime Start = DateTime.Now;` | wordt één keer ingevuld |

### Wanneer wel, wanneer niet

<span style="color:#37b24d"><b>Wel</b></span> — `static` voor wat echt bij de klasse hoort: een teller over alle
objecten, een hulpmethode die geen gegevens van een object nodig heeft. `const`
voor een waarde die nooit verandert, zoals een maximum. `readonly` voor een veld
dat je één keer invult en daarna met rust laat, zoals alles wat je via de
constructor krijgt.

<span style="color:#f03e3e"><b>Niet</b></span> — `static` voor gegevens die bij één object horen. De `static` lijst
uit les 02 is een tijdelijke oplossing: zodra er een database is, verdwijnt ze.

### Veelgemaakte fouten

> [!WARNING]
> - Een `static` methode kan niet aan de gewone velden van de klasse. Ze hoort bij
>   de klasse, niet bij één object, dus ze weet niet wélk object je bedoelt.
> - `readonly` beschermt het veld, niet wat erin zit. Een `readonly` lijst kan je
>   niet vervangen door een andere lijst, maar je kan er wel nog items aan
>   toevoegen.
> - `const` werkt enkel met waarden die al vastliggen bij het compileren:
>   getallen, tekst, `true` en `false`. `const List<int>` of
>   `const DateTime` compileert niet. Gebruik dan `static readonly`.
> - Een `static` lid roep je op via de klassenaam. `a.Totaal` compileert niet,
>   `Teller.Totaal` wel.

### Voorspel de uitkomst

**Vraag 1**

Deze vraag gebruikt de klasse `Teller` van hierboven.

```csharp
var a = new Teller();
var b = new Teller();
var c = new Teller();

a.Tik();
b.Tik();
b.Tik();

Console.WriteLine(Teller.Totaal);
Console.WriteLine(c.Eigen);
```

<details>
<summary><span style="color:#845ef7"><b>Toon het antwoord</b></span></summary>

<code style="color:#845ef7">3</code> en daarna <code style="color:#845ef7">0</code>

Er werd drie keer getikt, en het totaal is van iedereen samen. `c` heeft zelf
nooit getikt, dus zijn eigen teller staat nog op nul.

</details>

**Vraag 2**

```csharp
public class Mand
{
    private List<string> _items = new List<string>();
    private static List<string> _gedeeld = new List<string>();

    public void Voeg(string item)
    {
        _items.Add(item);
        _gedeeld.Add(item);
    }

    public string Tel()
    {
        return _items.Count + " en " + _gedeeld.Count;
    }
}

new Mand().Voeg("appel");
new Mand().Voeg("peer");
Console.WriteLine(new Mand().Tel());
```

<details>
<summary><span style="color:#845ef7"><b>Toon het antwoord</b></span></summary>

<code style="color:#845ef7">0 en 2</code>

Elke `new Mand()` is een nieuwe mand met een lege eigen lijst. De gedeelde lijst
hoort bij de klasse en onthoudt alles. Dit is precies wat er in een controller
gebeurt: elk verzoek krijgt een nieuw object, en enkel wat `static` is, blijft.

</details>

**Vraag 3**

```csharp
public class Klas
{
    private readonly List<string> _namen = new List<string>();

    public void Voeg(string naam)
    {
        _namen.Add(naam);
    }

    public int Aantal()
    {
        return _namen.Count;
    }
}

var k = new Klas();
k.Voeg("Anna");
k.Voeg("Bram");
Console.WriteLine(k.Aantal());
```

<details>
<summary><span style="color:#845ef7"><b>Toon het antwoord</b></span></summary>

<code style="color:#845ef7">2</code>

`readonly` zegt enkel dat `_namen` altijd naar dezelfde lijst wijst. Wat er in
die lijst zit, mag gewoon veranderen. `_namen = new List<string>();` in `Voeg`
had niet gecompileerd.

</details>

**Vraag 4**

```csharp
public class Rekenhulp
{
    private int _laatste = 0;

    public static int Dubbel(int x)
    {
        _laatste = x * 2;
        return _laatste;
    }
}
```

<details>
<summary><span style="color:#845ef7"><b>Toon het antwoord</b></span></summary>

<code style="color:#f03e3e">Compileert niet.</code>

`Dubbel` is `static` en hoort bij de klasse. `_laatste` is een gewoon veld en
hoort bij één object. Als je `Rekenhulp.Dubbel(5)` oproept zonder object, is er
geen `_laatste` om in te schrijven.

</details>

---

# Deel 5 — In je API

Wat je nodig hebt zodra je van een consoletoepassing naar een API overstapt.

---

## 25. Attributen en routes

<span style="background:#0c8599;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>IN JE API</b></span>

### Wat het is

**Een etiket op je code.** Een attribuut staat tussen vierkante haken boven een
klasse, een methode of een parameter. Het verandert niets aan wat de code doet,
maar het vertelt het framework iets erover. `[HttpGet]` boven een methode zegt:
*deze methode antwoordt op een GET-verzoek*.

Voor een API is het belangrijkste wat attributen regelen de **route**: welke URL
bij welke methode uitkomt.

### Hoe het eruitziet

```csharp
[ApiController]
[Route("api/[controller]")]
public class ReceptController : ControllerBase
{
    [HttpGet]
    public ActionResult<List<Recept>> GetAlle() { /* ... */ }

    [HttpGet("{id}")]
    public ActionResult<Recept> GetById(int id) { /* ... */ }

    [HttpGet("keuken/{keuken}")]
    public ActionResult<List<Recept>> GetPerKeuken(string keuken) { /* ... */ }

    [HttpPost]
    public ActionResult<Recept> VoegToe(Recept recept) { /* ... */ }

    [HttpPut("{id}")]
    public IActionResult Wijzig(int id, Recept recept) { /* ... */ }

    [HttpDelete("{id}")]
    public IActionResult Verwijder(int id) { /* ... */ }
}
```

De route van de klasse en die van de methode worden aan elkaar geplakt:

| Verzoek | Komt uit bij | Met |
|---|---|---|
| `GET api/recept` | `GetAlle` | — |
| `GET api/recept/5` | `GetById` | `id` is 5 |
| `GET api/recept/keuken/italiaans` | `GetPerKeuken` | `keuken` is `"italiaans"` |
| `POST api/recept` | `VoegToe` | `recept` komt uit de JSON in de body |
| `PUT api/recept/5` | `Wijzig` | `id` uit de route, `recept` uit de body |
| `DELETE api/recept/5` | `Verwijder` | `id` is 5 |

`[controller]` wordt vervangen door de naam van de klasse zonder het woord
*Controller*: van `ReceptController` blijft `Recept` over. Hoofdletters maken in
een route niet uit: `api/Recept` en `api/recept` komen bij dezelfde methode uit.
De waarde zelf behoudt wel haar hoofdletters: bij `keuken/Italiaans` is `keuken`
gelijk aan `"Italiaans"`.

### Waar een parameter vandaan komt

Met `[ApiController]` op de klasse beslist ASP.NET Core zelf waar elke parameter
vandaan komt:

1. Staat de naam tussen accolades in de route, dan komt hij uit de route.
2. Is het een object, zoals een `Recept`, dan komt het uit de body.
3. Al de rest komt uit de querystring: het stuk na het vraagteken, zoals
   `?zoek=pasta`.

Je kan het ook zelf zeggen met `[FromRoute]`, `[FromBody]` of `[FromQuery]` vóór
de parameter. Met `[ApiController]` heb je dat zelden nodig.

### Wat je ermee kan

| Vorm | Wat het doet | Voorbeeld | Resultaat |
|---|---|---|---|
| <code style="color:#228be6;font-weight:600">[ApiController]</code> | zet het API-gedrag aan: parameters herkennen, automatisch 400 bij ongeldige invoer | boven de klasse | — |
| <code style="color:#228be6;font-weight:600">[Route("api/[controller]")]</code> | de basisroute van de klasse | op `ReceptController` | `api/recept` |
| <code style="color:#228be6;font-weight:600">[Route("api/keuken")]</code> | een vaste route, los van de klassenaam | op `ReceptController` | `api/keuken` |
| <code style="color:#228be6;font-weight:600">[HttpGet]</code> | antwoordt op GET, op de basisroute | `GetAlle()` | `GET api/recept` |
| <code style="color:#228be6;font-weight:600">[HttpGet("{id}")]</code> | met een routeparameter; de naam tussen accolades moet die van de parameter zijn | `GetById(int id)` | `GET api/recept/5` |
| <code style="color:#228be6;font-weight:600">[HttpGet("{id:int}")]</code> | routeparameter met een beperking: enkel een geheel getal | `GetById(int id)` | `GET api/recept/abc` geeft 404 |
| <code style="color:#228be6;font-weight:600">[HttpGet("vast/{x}")]</code> | een vast stuk en een parameter | `keuken/{keuken}` | `GET api/recept/keuken/thais` |
| <code style="color:#228be6;font-weight:600">[HttpGet("{a}/{b}")]</code> | twee parameters in één route | `Bereken(int a, int b)` | `GET api/recept/2/3` |
| <code style="color:#228be6;font-weight:600">[HttpPost]</code> | antwoordt op POST | `VoegToe(Recept r)` | `POST api/recept` |
| <code style="color:#228be6;font-weight:600">[HttpPut("{id}")]</code> | antwoordt op PUT | `Wijzig(int id, Recept r)` | `PUT api/recept/5` |
| <code style="color:#228be6;font-weight:600">[HttpDelete("{id}")]</code> | antwoordt op DELETE | `Verwijder(int id)` | `DELETE api/recept/5` |
| <code style="color:#228be6;font-weight:600">[FromBody]</code> | haal deze parameter uit de body | `VoegToe([FromBody] Recept r)` | de JSON wordt een `Recept` |
| <code style="color:#228be6;font-weight:600">[FromQuery]</code> | haal deze parameter uit de querystring | `Zoek([FromQuery] string zoek)` | `api/recept?zoek=pasta` |
| <code style="color:#228be6;font-weight:600">[FromRoute]</code> | haal deze parameter uit de route | `GetById([FromRoute] int id)` | zoals zonder attribuut |

### Veelgemaakte fouten

> [!WARNING]
> - Twee methodes met hetzelfde verb en dezelfde route compileren, maar zodra
>   een verzoek binnenkomt, weet ASP.NET Core niet welke het moet nemen. Dat geeft
>   <code style="color:#f03e3e;font-weight:600">500</code> met een
>   `AmbiguousMatchException`. `{id}` en `{naam}` op dezelfde plaats tellen als
>   dezelfde route.
> - De naam tussen accolades moet exact die van de parameter zijn.
>   `[HttpGet("{id}")]` met `GetById(int nummer)` vult `nummer` niet uit de
>   route, en dan blijft het op 0.
> - Een route in een methode-attribuut die begint met `/` negeert de route van de
>   klasse. `[HttpGet("/zoek")]` is gewoon `zoek`, zonder `api/recept` ervoor.
> - Een waarde uit de URL behoudt haar hoofdletters. Vergelijk je ze met een vaste
>   tekst, denk dan aan `Equals` met `OrdinalIgnoreCase` uit hoofdstuk 1.

### Voorspel de uitkomst

Bij een API voorspel je wat de client terugkrijgt. Deze vragen gebruiken de
`ReceptController` van hierboven.

**Vraag 1**

```text
GET api/recept/abc
```

<details>
<summary><span style="color:#845ef7"><b>Toon het antwoord</b></span></summary>

<code style="color:#f08c00;font-weight:600">400 Bad Request</code>

De route `{id}` past: er staat iets na `api/recept/`. Maar `abc` kan geen `int`
worden. `[ApiController]` merkt dat en stuurt zelf een 400 terug, nog voor
`GetById` draait.

</details>

**Vraag 2**

Dezelfde controller, maar nu met `[HttpGet("{id:int}")]` boven `GetById`.

```text
GET api/recept/abc
```

<details>
<summary><span style="color:#845ef7"><b>Toon het antwoord</b></span></summary>

<code style="color:#f08c00;font-weight:600">404 Not Found</code>

Door de beperking `:int` past de route niet meer bij `abc`. Er is dus geen enkele
methode voor dit adres, en dat is een 404. Vergelijk met vraag 1: een kleine
beperking, een andere statuscode.

</details>

**Vraag 3**

Iemand voegt deze methode toe aan de `ReceptController`:

```csharp
[HttpGet("{naam}")]
public ActionResult<Recept> GetByNaam(string naam) { /* ... */ }
```

```text
GET api/recept/5
```

<details>
<summary><span style="color:#845ef7"><b>Toon het antwoord</b></span></summary>

<code style="color:#f03e3e;font-weight:600">500 Internal Server Error</code>

`api/recept/{id}` en `api/recept/{naam}` zijn voor de route hetzelfde: een stuk
tekst na `api/recept/`. Er passen nu twee methodes, en ASP.NET Core weigert te
kiezen. Het compileerde nochtans zonder fout.

</details>

**Vraag 4**

```text
GET api/RECEPT/keuken/Thais
```

<details>
<summary><span style="color:#845ef7"><b>Toon het antwoord</b></span></summary>

`GetPerKeuken` draait, met `keuken` gelijk aan <code style="color:#845ef7">"Thais"</code>.

De hoofdletters in `RECEPT` maken voor de route niets uit. De waarde `Thais`
komt wel letterlijk in de parameter, met hoofdletter. Vergelijk je die met
`"thais"`, dan geeft `==` vals.

</details>

---

## 26. De helpers van een controller

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

### `ActionResult<T>` tegenover `IActionResult`

Een methode die een van deze helpers teruggeeft, heeft een terugkeertype nodig
dat zowel een statuscode als gegevens kan dragen. In de cursus kom je er twee
tegen: `ActionResult<T>` vanaf les 02, en `IActionResult` vanaf les 06, bij PUT en
DELETE.

| | `ActionResult<T>` | `IActionResult` |
|---|---|---|
| **Zegt wat er terugkomt** | ja: een `T` | nee |
| **`return recept;`** | mag, wordt <code style="color:#37b24d;font-weight:600">200 OK</code> | <code style="color:#f03e3e">compileert niet</code> — schrijf `return Ok(recept);` |
| **`return NotFound();`** | mag | mag |
| **Scalar toont het type van het antwoord** | ja | nee |
| **Typisch gebruik** | GET en POST, die iets teruggeven | PUT en DELETE, die enkel een statuscode teruggeven |

> [!WARNING]
> `ActionResult<T>` kan een waarde enkel rechtstreeks teruggeven als `T` een
> klasse is, geen interface. Bij `ActionResult<IEnumerable<Recept>>` compileert
> `return resultaat;` niet als `resultaat` zelf als `IEnumerable` getypt is. Maak
> er eerst met `ToList()` een lijst van, of kies meteen `ActionResult<List<Recept>>`.

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
naar hoofdstuk 11.

</details>

**Vraag 4**

Twee methodes, allebei met een recept `r` dat gevonden werd:

```csharp
public ActionResult<Recept> GetEen(int id)
{
    // ...
    return r;
}

public IActionResult GetAnder(int id)
{
    // ...
    return r;
}
```

<details>
<summary><span style="color:#845ef7"><b>Toon het antwoord</b></span></summary>

De eerste geeft <code style="color:#37b24d;font-weight:600">200 OK</code> met het recept. De tweede <code style="color:#f03e3e">compileert niet</code>.

`ActionResult<Recept>` weet dat er een recept terug mag komen en maakt er zelf
een 200 van. `IActionResult` kent geen recepten, enkel antwoorden. Daar moet je
het recept zelf inpakken met `Ok(r)`.

</details>

---

## 27. Program.cs en dependency injection

<span style="background:#0c8599;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>IN JE API</b></span>

### Wat het is

**Het startpunt en de schakelkast van je API.** `Program.cs` draait één keer, bij
het opstarten. Het doet vier dingen, altijd in deze volgorde:

1. een **bouwer** maken;
2. bij die bouwer **registreren** wat je API nodig heeft: de diensten;
3. de app **bouwen**;
4. vastleggen welke weg elk verzoek aflegt, en de app **starten**.

### Hoe het eruitziet

```csharp
var builder = WebApplication.CreateBuilder(args);

// 2. Diensten registreren
builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddScoped<IReceptRepository, ReceptRepository>();

// 3. Bouwen
var app = builder.Build();

// 4. De weg van een verzoek, en starten
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
```

Alles wat met `builder.Services` begint, staat vóór `builder.Build()`. Alles wat
met `app.` begint, staat erna.

### Dependency injection: vragen in plaats van zelf maken

Een controller heeft vaak iets anders nodig, zoals een repository. Hij maakt dat
niet zelf met `new`. Hij **vraagt** erom in zijn constructor:

```csharp
public class ReceptController : ControllerBase
{
    private readonly IReceptRepository _repository;

    public ReceptController(IReceptRepository repository)
    {
        _repository = repository;
    }
}
```

Komt er een verzoek binnen, dan ziet ASP.NET Core dat de controller een
`IReceptRepository` nodig heeft. Het zoekt in de registraties welke klasse daarbij
hoort, maakt er een object van en geeft dat mee. De controller kent enkel het
contract uit [hoofdstuk 19](#19-interface). Zo kan je later een andere klasse
registreren, bijvoorbeeld een die een database gebruikt, zonder één letter in de
controller te veranderen.

Het `private readonly` veld uit [hoofdstuk 24](#24-static-const-en-readonly)
bewaart wat je kreeg, zodat geen andere methode het nog kan vervangen.

### Hoe lang een dienst leeft

| Registratie | Hoe vaak er een nieuw object komt | Typisch voor |
|---|---|---|
| `AddTransient<I, K>()` | elke keer dat iemand erom vraagt | kleine diensten zonder eigen gegevens |
| `AddScoped<I, K>()` | één per verzoek | repositories en de databasecontext; de cursus gebruikt dit |
| `AddSingleton<I, K>()` | één voor de hele levensduur van de app | iets dat iedereen moet delen |

> [!IMPORTANT]
> Bij `AddScoped` krijgt elk verzoek een nieuwe repository. Bewaart die repository
> haar gegevens in een gewone lijst, dan begint elk verzoek met een verse lijst:
> wat je met een POST toevoegde, is bij de volgende GET weg. Dat is hetzelfde
> probleem als bij de controller in [hoofdstuk 24](#24-static-const-en-readonly).
> Met een database speelt het niet, want die onthoudt de gegevens zelf.

### Wat je ermee kan

| Vorm | Wat het doet | Voorbeeld | Resultaat |
|---|---|---|---|
| <code style="color:#228be6;font-weight:600">WebApplication.CreateBuilder</code> | maak de bouwer | `var builder = WebApplication.CreateBuilder(args);` | de bouwer, met de standaardinstellingen |
| <code style="color:#228be6;font-weight:600">AddControllers</code> | zet controllers aan | `builder.Services.AddControllers();` | zonder dit bestaan je controllers niet |
| <code style="color:#228be6;font-weight:600">AddOpenApi</code> | beschrijf je API voor Scalar | `builder.Services.AddOpenApi();` | — |
| <code style="color:#228be6;font-weight:600">AddScoped</code> | registreer: één object per verzoek | `AddScoped<IReceptRepository, ReceptRepository>()` | wie het contract vraagt, krijgt de klasse |
| <code style="color:#228be6;font-weight:600">AddTransient</code> | registreer: elke keer een nieuw object | `AddTransient<IKlok, Klok>()` | — |
| <code style="color:#228be6;font-weight:600">AddSingleton</code> | registreer: één object voor de hele app | `AddSingleton<ITeller, Teller>()` | — |
| <code style="color:#228be6;font-weight:600">GetConnectionString</code> | lees een verbinding uit `appsettings.json` | `builder.Configuration.GetConnectionString("Standaard")` | de tekst onder `ConnectionStrings` |
| <code style="color:#228be6;font-weight:600">Build</code> | bouw de app; daarna liggen de registraties vast | `var app = builder.Build();` | de app |
| <code style="color:#228be6;font-weight:600">IsDevelopment</code> | draait de app op je eigen computer | `if (app.Environment.IsDevelopment())` | <code style="color:#845ef7">true</code> tijdens het ontwikkelen |
| <code style="color:#228be6;font-weight:600">MapOpenApi</code>, <code style="color:#228be6;font-weight:600">MapScalarApiReference</code> | maak de beschrijving en de Scalar-pagina bereikbaar | `app.MapScalarApiReference();` | de Scalar-pagina is bereikbaar |
| <code style="color:#228be6;font-weight:600">UseHttpsRedirection</code> | stuur `http` door naar `https` | `app.UseHttpsRedirection();` | — |
| <code style="color:#228be6;font-weight:600">UseAuthorization</code> | controleer wie wat mag | `app.UseAuthorization();` | belangrijk vanaf de beveiliging |
| <code style="color:#228be6;font-weight:600">MapControllers</code> | koppel de routes van je controllers | `app.MapControllers();` | zonder dit geeft elke route 404 |
| <code style="color:#228be6;font-weight:600">Run</code> | start de app en blijf luisteren | `app.Run();` | wat erna staat, draait pas als de app stopt |

> [!NOTE]
> In nieuwere code zie je soms een kortere schrijfwijze, een *primary
> constructor*: `public class ReceptController(IReceptRepository repository) : ControllerBase`.
> De parameter staat dan meteen achter de klassenaam, en het veld en de
> constructor vallen weg. Het werkt op dezelfde manier.

### Veelgemaakte fouten

> [!WARNING]
> - Vraagt een controller iets dat niet geregistreerd is, dan compileert alles.
>   Pas wanneer er een verzoek binnenkomt, krijg je een
>   `InvalidOperationException`: *Unable to resolve service for type…* De client
>   ziet <code style="color:#f03e3e;font-weight:600">500</code>.
> - Een registratie na `builder.Build()` komt te laat. Zet ze er altijd vóór.
> - Vergeet je `app.MapControllers()`, dan start de app gewoon, maar geeft elke
>   route 404.
> - `new ReceptRepository()` in een controller werkt, maar breekt het hele idee.
>   De controller hangt dan weer vast aan één klasse.

### Voorspel de uitkomst

**Vraag 1**

Een repository bewaart recepten in een gewone lijst, geen `static`. Ze is
geregistreerd met `AddScoped`. Je doet een POST die één recept toevoegt, en
daarna een GET die alle recepten opvraagt. De lijst begon leeg.

Hoeveel recepten geeft de GET terug? En als de repository met `AddSingleton`
geregistreerd was?

<details>
<summary><span style="color:#845ef7"><b>Toon het antwoord</b></span></summary>

Met `AddScoped`: <code style="color:#845ef7">0</code>. Met `AddSingleton`: <code style="color:#845ef7">1</code>.

`AddScoped` geeft elk verzoek een nieuwe repository, met een nieuwe, lege lijst.
Het recept van de POST zat in de repository van dat ene verzoek. Bij
`AddSingleton` is er één repository voor de hele app, en die onthoudt het recept.

</details>

**Vraag 2**

In `Program.cs` ontbreekt de regel met `AddScoped<IReceptRepository, ReceptRepository>()`.
De `ReceptController` vraagt in zijn constructor een `IReceptRepository`. De app
start, en je doet `GET api/recept`.

<details>
<summary><span style="color:#845ef7"><b>Toon het antwoord</b></span></summary>

<code style="color:#f03e3e;font-weight:600">500 Internal Server Error</code>

De app start gewoon, want niemand heeft de controller al nodig. Bij het eerste
verzoek wil ASP.NET Core er een maken, vindt geen klasse voor
`IReceptRepository`, en geeft een `InvalidOperationException`.

</details>

**Vraag 3**

```csharp
app.MapControllers();

app.Run();

Console.WriteLine("De API draait.");
```

<details>
<summary><span style="color:#845ef7"><b>Toon het antwoord</b></span></summary>

Zolang de API draait, verschijnt er niets.

`app.Run()` keert pas terug wanneer de app stopt. Wil je iets melden bij het
opstarten, dan moet het vóór `app.Run()` staan.

</details>

---

## 28. Logging

<span style="background:#0c8599;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>IN JE API</b></span>

### Wat het is

**Een logboek dat je API zelf bijhoudt.** Terwijl de API draait, schrijft hij in
het terminalvenster wat er gebeurt: welk verzoek er binnenkwam, wat er niet
gevonden werd, wat er misliep. Zo zie je achteraf wat er gebeurd is, ook als niemand
op dat moment keek.

Elke melding krijgt een **niveau**, van een detail dat enkel tijdens het
ontwikkelen nuttig is, tot een fout die meteen aandacht vraagt.

### Hoe het eruitziet

```csharp
public class ReceptController : ControllerBase
{
    private readonly ILogger<ReceptController> _logger;

    public ReceptController(ILogger<ReceptController> logger)
    {
        _logger = logger;
    }

    [HttpGet("{id}")]
    public ActionResult<Recept> GetById(int id)
    {
        _logger.LogInformation("Recept {Id} opgevraagd", id);

        // ...

        _logger.LogWarning("Recept {Id} niet gevonden", id);
        return NotFound();
    }
}
```

De logger vraag je in de constructor, net als een repository in
[hoofdstuk 27](#27-programcs-en-dependency-injection). Registreren hoeft niet:
ASP.NET Core heeft hem al klaarstaan. Tussen de punthaken staat de klasse die
logt. Die naam komt mee in het logboek, zodat je ziet waar een melding vandaan
komt.

### De niveaus

| Methode | Wanneer | Standaard zichtbaar |
|---|---|---|
| <code style="color:#228be6;font-weight:600">LogTrace</code> | de kleinste details | nee |
| <code style="color:#228be6;font-weight:600">LogDebug</code> | details die enkel tijdens het ontwikkelen helpen | nee |
| <code style="color:#228be6;font-weight:600">LogInformation</code> | de normale werking: een verzoek, een geslaagde actie | ja |
| <code style="color:#228be6;font-weight:600">LogWarning</code> | iets onverwachts, maar de app werkt verder: een Id dat niet bestaat | ja |
| <code style="color:#228be6;font-weight:600">LogError</code> | iets is misgelopen: een actie lukte niet | ja |
| <code style="color:#228be6;font-weight:600">LogCritical</code> | de app zelf zit in de problemen | ja |

Welke niveaus je ziet, staat in `appsettings.json`, onder `Logging` en `LogLevel`.
Een nieuw project zet `Default` op `Information`: alles vanaf dat niveau verschijnt,
`Debug` en `Trace` niet.

### Twee schrijfwijzen

In les 03 schrijft de cursus de melding met een <code>&#36;</code> ervoor, zoals in hoofdstuk 1:
de waarden staan meteen in de tekst. Dat werkt.

Microsoft raadt een andere vorm aan, met een **sjabloon**: de tekst bevat
plaatshouders tussen accolades, en de waarden komen erachter, gescheiden door
komma's. Bijvoorbeeld `_logger.LogInformation("Recept {Id} opgevraagd", id);`. In
het terminalvenster zie je hetzelfde. Het verschil is dat de logger de waarden
apart bewaart, zodat een loggingprogramma later kan zoeken op *alle meldingen over
recept 5*. Visual Studio stelt daarom soms voor om de eerste vorm om te zetten.

### Wat je ermee kan

| Vorm | Wat het doet | Voorbeeld | Resultaat |
|---|---|---|---|
| <code style="color:#228be6;font-weight:600">ILogger&lt;T&gt;</code> | een logger voor klasse `T`, via de constructor | `ILogger<ReceptController> logger` | de logger staat klaar |
| <code style="color:#228be6;font-weight:600">LogInformation</code> | meld de normale werking | `_logger.LogInformation("Alle recepten opgevraagd");` | <code style="color:#845ef7">info: ... Alle recepten opgevraagd</code> |
| <code style="color:#228be6;font-weight:600">LogWarning</code> | meld iets onverwachts | `_logger.LogWarning("Recept {Id} niet gevonden", id);` | <code style="color:#845ef7">warn: ... Recept 5 niet gevonden</code> |
| <code style="color:#228be6;font-weight:600">LogError</code> | meld een fout | `_logger.LogError("Verwijderen van {Id} mislukt", id);` | <code style="color:#845ef7">fail: ...</code> |
| <code style="color:#228be6;font-weight:600">LogError</code> met uitzondering | meld een fout en geef de uitzondering mee | `_logger.LogError(ex, "Bestand niet gelezen");` | de melding, met de details van `ex` eronder |
| <code style="color:#228be6;font-weight:600">{Naam}</code> | plaatshouder in een sjabloon | `"{Aantal} recepten"`, `aantal` | <code style="color:#845ef7">12 recepten</code> |

### Veelgemaakte fouten

> [!WARNING]
> - In een sjabloon telt de **volgorde** van de waarden, niet de naam tussen de
>   accolades. De eerste waarde komt in de eerste plaatshouder, wat er ook tussen
>   de accolades staat.
> - Een `LogDebug` die je nooit ziet, is meestal geen fout in je code. Het niveau
>   staat in `appsettings.json` hoger ingesteld.
> - Zet nooit wachtwoorden of andere geheime gegevens in een logboek. Wie het
>   logboek kan lezen, kan ze dan ook lezen.

### Voorspel de uitkomst

**Vraag 1**

```csharp
_logger.LogInformation("{Peren} peren en {Appels} appels", 3, 8);
```

<details>
<summary><span style="color:#845ef7"><b>Toon het antwoord</b></span></summary>

<code style="color:#845ef7">3 peren en 8 appels</code>

De eerste waarde gaat naar de eerste plaatshouder, de tweede naar de tweede. Dat
de namen `Peren` en `Appels` heten, speelt geen rol bij welke waarde waar komt.

</details>

**Vraag 2**

In `appsettings.json` staat `"Default": "Information"`. Een methode bevat:

```csharp
_logger.LogDebug("Begin van de methode");
_logger.LogWarning("Geen recepten gevonden");
```

Wat verschijnt er in het terminalvenster?

<details>
<summary><span style="color:#845ef7"><b>Toon het antwoord</b></span></summary>

Enkel <code style="color:#845ef7">Geen recepten gevonden</code>, als waarschuwing.

`Debug` ligt lager dan `Information`, dus die melding valt weg. `Warning` ligt
hoger en komt erdoor.

</details>

---

## 29. Asynchroon werken

<span style="background:#0c8599;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>IN JE API</b></span>

| Vorm | Wat het doet |
|---|---|
| <code style="color:#228be6;font-weight:600">async</code> | markeert een methode die mag wachten |
| <code style="color:#228be6;font-weight:600">await</code> | wacht op het resultaat zonder de server te blokkeren |
| <code style="color:#228be6;font-weight:600">Task</code> | een belofte dat er ooit iets klaar is |
| <code style="color:#228be6;font-weight:600">Task&lt;T&gt;</code> | een belofte dat er ooit een `T` klaar is |

De asynchrone tegenhangers die je in de latere lessen tegenkomt dragen dezelfde
naam met `Async` erachter: `ToListAsync`, `FirstOrDefaultAsync`, `FindAsync`,
`SaveChangesAsync`, `AnyAsync`, `CountAsync`. Ze doen hetzelfde als hun gewone
versie; het verschil zit in wat de server intussen mag doen.

> [!NOTE]
> Deze komen pas aan bod in les 05 van Programming Advanced. Ze staan hier
> zodat je ze herkent wanneer je ze in een voorbeeld ziet staan, niet om ze nu
> al te gebruiken.
> Hoe je ze met een database gebruikt, staat in [hoofdstuk 32](#32-ef-core-opvragen-en-bewaren).

### Hoe het eruitziet

```csharp
async Task<int> TelLangzaamAsync()
{
    await Task.Delay(1000);    // wacht een seconde, zonder de thread vast te houden
    return 42;
}

int n = await TelLangzaamAsync();
Console.WriteLine(n);          // 42, na een seconde
```

In een controller ziet dat er zo uit:

```csharp
[HttpGet]
public async Task<ActionResult<List<Recept>>> GetAlle()
{
    List<Recept> recepten = await _repository.GetAllAsync();
    return recepten;
}
```

Drie dingen veranderen tegenover de gewone versie: `async` voor het
terugkeertype, het terugkeertype zelf in een `Task<...>`, en `await` voor elke
aanroep die tijd kost.

### Async is besmettelijk

Les 05 noemt het het domino-effect. Gebruik je ergens `await`, dan moet die
methode `async` worden en een `Task` teruggeven. Wie die methode oproept, moet
dan zelf ook `await` gebruiken, en dus ook `async` worden. Zo schuift het naar
boven: van de repository, via de interface, tot in de controller.

### Wat je ermee kan

| Vorm | Wat het doet | Voorbeeld | Resultaat |
|---|---|---|---|
| <code style="color:#228be6;font-weight:600">async Task</code> | een asynchrone methode die niets teruggeeft | `async Task BewaarAsync()` | — |
| <code style="color:#228be6;font-weight:600">async Task&lt;T&gt;</code> | een asynchrone methode die een `T` teruggeeft | `async Task<int> TelAsync()` | `await TelAsync()` geeft een `int` |
| <code style="color:#228be6;font-weight:600">await</code> | wacht op een `Task` en pak het resultaat uit | `int n = await TelAsync();` | <code style="color:#845ef7">n</code> is een getal |
| <code style="color:#228be6;font-weight:600">Task.Delay</code> | wacht een aantal milliseconden, zonder te blokkeren | `await Task.Delay(500);` | een halve seconde later gaat het verder |
| <code style="color:#228be6;font-weight:600">Async</code> achter de naam | de afspraak voor asynchrone methodes | `GetAllAsync`, `SaveChangesAsync` | je ziet meteen dat er een `await` bij hoort |
| <code style="color:#228be6;font-weight:600">.Result</code>, <code style="color:#228be6;font-weight:600">.Wait()</code> | wacht door de thread vast te houden | `TelAsync().Result` | werkt, maar blokkeert; niet doen |
| <code style="color:#228be6;font-weight:600">async void</code> | een asynchrone methode zonder `Task` | `async void Bewaar()` | fouten erin kan niemand opvangen; niet doen |

### Veelgemaakte fouten

> [!WARNING]
> - Een vergeten `await` compileert vaak gewoon, met een waarschuwing. Je code
>   gaat dan verder voor het werk klaar is.
> - `.Result` en `.Wait()` maken van asynchrone code weer gewone, wachtende code.
>   Alle winst is weg, en in sommige toepassingen loopt het programma vast, omdat
>   twee stukken op elkaar wachten (een *deadlock*). Les 05 zegt het kort: hou het
>   bij `async` en `await`.
> - `async void` geeft geen `Task` terug. Wie de methode oproept, kan er dus niet
>   op wachten en geen fout opvangen. Gebruik altijd `async Task` of
>   `async Task<T>`.

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

**Vraag 3**

```csharp
async Task SchrijfAsync()
{
    Console.WriteLine("B");
    await Task.Delay(100);
    Console.WriteLine("D");
}

Console.WriteLine("A");
Task t = SchrijfAsync();
Console.WriteLine("C");
await t;
Console.WriteLine("E");
```

<details>
<summary><span style="color:#845ef7"><b>Toon het antwoord</b></span></summary>

<code style="color:#845ef7">A</code>, <code style="color:#845ef7">B</code>, <code style="color:#845ef7">C</code>, <code style="color:#845ef7">D</code> en daarna <code style="color:#845ef7">E</code>

`SchrijfAsync` begint meteen en schrijft `B`. Bij de eerste `await` die echt moet
wachten, geeft de methode de beurt terug. Daardoor komt `C` vóór `D`. Pas bij
`await t` wacht het hoofdprogramma tot de methode helemaal klaar is, en dus komt
`E` als laatste.

</details>

**Vraag 4**

```csharp
async Task<int> GeefAsync()
{
    await Task.Delay(10);
    return 5;
}

int x = GeefAsync() + 1;
```

<details>
<summary><span style="color:#845ef7"><b>Toon het antwoord</b></span></summary>

<code style="color:#f03e3e">Compileert niet.</code>

Zonder `await` is `GeefAsync()` een `Task<int>`, een belofte, en een belofte
plus 1 bestaat niet. Met `await GeefAsync() + 1` lukt het wel: eerst het getal
uitpakken, dan optellen.

</details>

---

## 30. JSON en bestanden

<span style="background:#0c8599;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>IN JE API</b></span>

| Methode | Wat het doet |
|---|---|
| <code style="color:#228be6;font-weight:600">JsonSerializer.Serialize</code> | maak van een object een JSON-tekst |
| <code style="color:#228be6;font-weight:600">JsonSerializer.Deserialize</code> | maak van een JSON-tekst een object |
| <code style="color:#228be6;font-weight:600">File.ReadAllText</code> | lees een heel bestand als tekst |
| <code style="color:#228be6;font-weight:600">File.WriteAllText</code> | schrijf een tekst naar een bestand |
| <code style="color:#228be6;font-weight:600">File.Exists</code> | bestaat dat bestand |
| <code style="color:#228be6;font-weight:600">Path.Combine</code> | plak padstukken correct aan elkaar |

Ontbreekt het bestand, of staat er geen geldige JSON in, dan krijg je een
uitzondering. Hoe je die opvangt, staat in [hoofdstuk 23](#23-uitzonderingen).

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

# Deel 6 — Data

Van gegevens in het geheugen naar gegevens in een database: bewaren, opvragen, en
enkel doorgeven wat nodig is.

---

## 31. EF Core: model en database

<span style="background:#c2255c;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>DATA</b></span>

### Wat het is

**Een tolk tussen je klassen en je tabellen.** Entity Framework Core, kort EF Core,
zet je C#-klassen om naar tabellen in een database, en de rijen uit die tabellen
weer naar objecten. Je schrijft zelf geen SQL. Je beschrijft je klassen, en EF Core
maakt de tabellen erbij. De cursus noemt die werkwijze *code first*: eerst de code,
dan de database.

Drie stukken werken samen:

1. je **entiteiten**: gewone klassen, één per tabel;
2. de **context**: één klasse die erft van `DbContext`, met per tabel een `DbSet`;
3. de **migraties**: scripts die je database gelijkzetten met je klassen.

Dit hoofdstuk gaat over het model: hoe je tabellen en relaties vastlegt. Hoe je
gegevens opvraagt en bewaart, staat in [hoofdstuk 32](#32-ef-core-opvragen-en-bewaren).

### Hoe het eruitziet

Twee entiteiten: een keuken heeft veel recepten, een recept hoort bij één keuken.

```csharp
public class Keuken
{
    public int Id { get; set; }
    public string Naam { get; set; } = default!;

    public List<Recept> Recepten { get; set; } = new();
}

public class Recept
{
    public int Id { get; set; }
    public string Titel { get; set; } = default!;
    public decimal Kostprijs { get; set; }

    public int KeukenId { get; set; }                  // de vreemde sleutel
    public Keuken Keuken { get; set; } = default!;     // de navigatie-eigenschap
}
```

De context, met de verfijning in `OnModelCreating`:

```csharp
public class ReceptContext : DbContext
{
    public ReceptContext(DbContextOptions<ReceptContext> options) : base(options) { }

    public DbSet<Keuken> Keukens { get; set; }
    public DbSet<Recept> Recepten { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Recept>(entity =>
        {
            entity.Property(r => r.Titel).IsRequired().HasMaxLength(100);
            entity.Property(r => r.Kostprijs).HasPrecision(8, 2);

            entity.HasOne(r => r.Keuken)
                  .WithMany(k => k.Recepten)
                  .HasForeignKey(r => r.KeukenId)
                  .OnDelete(DeleteBehavior.Restrict);
        });
    }
}
```

De registratie in `Program.cs`, met de verbinding uit `appsettings.json`:

```csharp
var connectionString = builder.Configuration.GetConnectionString("Standaard");
builder.Services.AddDbContext<ReceptContext>(options => options.UseNpgsql(connectionString));
```

```json
"ConnectionStrings": {
  "Standaard": "Host=localhost;Port=5432;Database=ReceptenDb;Username=postgres;Password=..."
}
```

`AddDbContext` registreert de context zoals `AddScoped` uit
[hoofdstuk 27](#27-programcs-en-dependency-injection): één context per verzoek.
Een repository vraagt hem gewoon in haar constructor.

### Wat EF Core zelf afleidt

Veel hoef je niet te zeggen. EF Core volgt **conventies**: vaste afspraken over
namen en types.

| Je schrijft | EF Core maakt ervan |
|---|---|
| `DbSet<Recept> Recepten` in de context | een tabel `Recepten` |
| een eigenschap `Id` of `ReceptId` | de primaire sleutel; bij een `int` telt de database zelf op |
| `string` | een verplichte kolom van onbeperkte lengte |
| `string?`, `int?` | een kolom die leeg mag blijven |
| `decimal` | een kolom voor kommagetallen, exact, zonder afrondingsfouten |
| `int KeukenId` naast `Keuken Keuken` | een vreemde sleutel naar de tabel `Keukens` |
| `int KeukenId` | een **verplichte** relatie: elk recept heeft een keuken |
| `int? KeukenId` | een **optionele** relatie: een recept mag zonder keuken |

Alles wat afwijkt van die afspraken, of wat je strenger wil, zet je in
`OnModelCreating`. Die schrijfwijze, met methodes achter elkaar, heet de **Fluent
API**. `override` en `base` ken je uit
[hoofdstuk 20](#20-overerving-en-abstracte-klassen).

### Relaties

Een **navigatie-eigenschap** is een eigenschap die naar een ander object wijst,
zoals `Recept.Keuken`, of naar een lijst objecten, zoals `Keuken.Recepten`. De
**vreemde sleutel** is het getal in de tabel dat die koppeling bewaart, zoals
`KeukenId`.

| Soort | Voorbeeld | Waar de vreemde sleutel staat | Navigatie-eigenschappen |
|---|---|---|---|
| één op één | een leerling en zijn kluisje | aan één kant, die je zelf kiest | aan elke kant één object |
| één op veel | een keuken en haar recepten | aan de *veel*-kant: `Recept.KeukenId` | `Recept.Keuken` en de lijst `Keuken.Recepten` |
| veel op veel | recepten en ingrediënten | in een aparte koppeltabel, met twee vreemde sleutels | elke kant een lijst koppelobjecten |

Bij veel op veel maakt de cursus de koppeltabel als een eigen klasse, met twee
één-op-veel-relaties. Zo kan de koppeling zelf ook gegevens dragen, zoals de
hoeveelheid van een ingrediënt in een recept.

Een relatie in de Fluent API lees je vanuit de entiteit die je configureert:

| Regel | Lees als |
|---|---|
| `HasOne(r => r.Keuken)` | een recept heeft één keuken |
| `.WithMany(k => k.Recepten)` | die keuken heeft veel recepten |
| `.HasForeignKey(r => r.KeukenId)` | de koppeling zit in `KeukenId` |
| `.OnDelete(DeleteBehavior.Restrict)` | een keuken met recepten mag niet weg |

### Verwijderen: Cascade of Restrict

| | `Cascade` | `Restrict` |
|---|---|---|
| **Standaard bij** | een verplichte relatie, zoals `int KeukenId` | nooit, je kiest het zelf |
| **Een keuken met recepten verwijderen** | de recepten verdwijnen mee | `SaveChanges` weigert, met een uitzondering |
| **Het gevaar** | gegevens verdwijnen zonder dat iemand het merkt | geen; je ruimt eerst zelf op |

Les 06 vertelt het verhaal van klant Gerrit, die verwijderd wil worden. Met
`Cascade` gaan al zijn bestellingen stilletjes mee. Met `Restrict` houdt de database
tegen.

### Migraties

Een migratie is een script dat beschrijft hoe de database moet veranderen om weer
bij je klassen te passen. Elk script heeft een `Up`, die de wijziging uitvoert, en
een `Down`, die ze terugdraait.

| Package Manager Console (Visual Studio) | Terminal | Wat het doet |
|---|---|---|
| `Add-Migration Naam` | `dotnet ef migrations add Naam` | maak een nieuw script in de map `Migrations` |
| `Update-Database` | `dotnet ef database update` | voer de scripts uit die de database nog niet kent |
| `Remove-Migration` | `dotnet ef migrations remove` | verwijder het laatste script, zolang de database het nog niet uitvoerde |

De vaste cyclus, telkens je een entiteit toevoegt of aanpast:

1. pas je klasse aan, en voeg indien nodig een `DbSet` toe;
2. `Add-Migration` met een naam die zegt wat er verandert;
3. kijk het script na: heeft EF Core begrepen wat je bedoelde?
4. `Update-Database`, met Docker aan.

Je hebt drie packages nodig: `Npgsql.EntityFrameworkCore.PostgreSQL`,
`Microsoft.EntityFrameworkCore.Design` en, voor de Package Manager Console,
`Microsoft.EntityFrameworkCore.Tools`. EF Core zelf komt mee met het eerste. Les 04
installeert het ook apart, en dat kan geen kwaad. In de terminal installeer je
eenmalig ook het programma zelf, met `dotnet tool install --global dotnet-ef`.

> [!WARNING]
> In les 04 staat op een paar plaatsen `dotnet Entity Framework migrations add` en
> `dotnet Entity Framework database update`. De commando's heten
> `dotnet ef migrations add` en `dotnet ef database update`. De lange vorm herkent de
> terminal niet.

### Startgegevens met HasData

Met `HasData` geef je rijen mee die de migratie meteen in de tabel zet. Zo heeft
je database na `Update-Database` al gegevens om mee te testen. Twee regels:
je vult het `Id` zelf in, ook al telt de database het anders zelf op, en een
relatie leg je via de vreemde sleutel, niet via de navigatie-eigenschap.

```csharp
modelBuilder.Entity<Keuken>().HasData(
    new Keuken { Id = 1, Naam = "Italiaans" },
    new Keuken { Id = 2, Naam = "Thais" });
```

> [!WARNING]
> PostgreSQL telt de Ids met een teller die niet weet dat jouw startgegevens 1 en 2
> al gebruiken. De eerste keuken die je daarna via de API toevoegt, krijgt ook `Id`
> 1, en dat geeft een fout over een dubbele sleutel: een
> <code style="color:#f03e3e;font-weight:600">500</code>. De documentatie van
> Npgsql noemt twee uitwegen: de teller laten beginnen boven je startgegevens, met
> `HasIdentityOptions`, of je startgegevens negatieve Ids geven.

### Wat je ermee kan

| Vorm | Wat het doet | Voorbeeld | Resultaat |
|---|---|---|---|
| <code style="color:#228be6;font-weight:600">DbContext</code> | de basisklasse van je context | `class ReceptContext : DbContext` | de brug naar de database |
| <code style="color:#228be6;font-weight:600">DbSet&lt;T&gt;</code> | één tabel | `public DbSet<Recept> Recepten { get; set; }` | de tabel `Recepten` |
| <code style="color:#228be6;font-weight:600">: base(options)</code> | geef de opties door aan `DbContext` | in de constructor van de context | de context weet welke database |
| <code style="color:#228be6;font-weight:600">AddDbContext</code> | registreer de context, één per verzoek | `builder.Services.AddDbContext<ReceptContext>(...)` | repositories kunnen hem vragen |
| <code style="color:#228be6;font-weight:600">UseNpgsql</code> | gebruik PostgreSQL, met deze verbinding | `options.UseNpgsql(connectionString)` | — |
| <code style="color:#228be6;font-weight:600">OnModelCreating</code> | de plaats voor de Fluent API | `protected override void OnModelCreating(ModelBuilder modelBuilder)` | — |
| <code style="color:#228be6;font-weight:600">Entity&lt;T&gt;</code> | configureer één entiteit | `modelBuilder.Entity<Recept>(entity => { ... })` | — |
| <code style="color:#228be6;font-weight:600">ToTable</code> | kies zelf de naam van de tabel | `entity.ToTable("Recepten")` | — |
| <code style="color:#228be6;font-weight:600">HasKey</code> | kies zelf de primaire sleutel | `entity.HasKey(r => r.Code)` | nodig als de sleutel geen `Id` heet |
| <code style="color:#228be6;font-weight:600">Property</code> | configureer één eigenschap | `entity.Property(r => r.Titel)` | — |
| <code style="color:#228be6;font-weight:600">IsRequired</code> | de kolom mag niet leeg zijn | `.IsRequired()` | — |
| <code style="color:#228be6;font-weight:600">HasMaxLength</code> | een maximale lengte voor tekst | `.HasMaxLength(100)` | hoogstens 100 tekens |
| <code style="color:#228be6;font-weight:600">HasPrecision</code> | het aantal cijfers van een kommagetal | `.HasPrecision(8, 2)` | 8 cijfers, waarvan 2 na de komma |
| <code style="color:#228be6;font-weight:600">HasOne</code> + <code style="color:#228be6;font-weight:600">WithMany</code> | één op veel, gelezen vanaf de *veel*-kant | `entity.HasOne(r => r.Keuken).WithMany(k => k.Recepten)` | — |
| <code style="color:#228be6;font-weight:600">HasOne</code> + <code style="color:#228be6;font-weight:600">WithOne</code> | één op één | `.HasOne(l => l.Kluisje).WithOne(k => k.Leerling)` | — |
| <code style="color:#228be6;font-weight:600">HasForeignKey</code> | welke eigenschap de vreemde sleutel is | `.HasForeignKey(r => r.KeukenId)` | bij één op één met het type erbij: `HasForeignKey<Kluisje>(k => k.LeerlingId)` |
| <code style="color:#228be6;font-weight:600">OnDelete</code> | wat er gebeurt als de andere kant verdwijnt | `.OnDelete(DeleteBehavior.Restrict)` | — |
| <code style="color:#228be6;font-weight:600">HasData</code> | startgegevens via een migratie | `modelBuilder.Entity<Keuken>().HasData(...)` | rijen in de tabel na `Update-Database` |

### Veelgemaakte fouten

> [!WARNING]
> - Een klasse aangepast, maar geen migratie gemaakt of `Update-Database`
>   vergeten? Alles compileert, maar bij het eerste verzoek meldt PostgreSQL dat een
>   kolom of tabel niet bestaat. De client ziet
>   <code style="color:#f03e3e;font-weight:600">500</code>.
> - Heet de sleutel geen `Id`, en ook niet de naam van de klasse met `Id` erachter,
>   en staat er geen `HasKey`, dan stopt `Add-Migration` met *The entity type '…'
>   requires a primary key to be defined.*
> - Pas de database nooit met de hand aan. De volgende migratie gaat ervan uit dat
>   de database er nog uitziet zoals de vorige migratie hem achterliet.
> - Draait Docker niet, dan kan `Update-Database` de database niet bereiken.
> - Zet `base.OnModelCreating(modelBuilder)` altijd bovenaan. Bij een gewone
>   `DbContext` merk je het verschil niet, maar erft je context later van een
>   klasse die zelf tabellen configureert, zoals bij de beveiliging, dan krijg je
>   zonder die regel fouten over ontbrekende sleutels.

### Voorspel de uitkomst

**Vraag 1**

De context bevat `public DbSet<Ingredient> Ingredienten { get; set; }`, en verder
niets over ingrediënten.

```csharp
public class Ingredient
{
    public int Nummer { get; set; }
    public string Naam { get; set; } = default!;
}
```

```text
Add-Migration Ingredienten
```

<details>
<summary><span style="color:#845ef7"><b>Toon het antwoord</b></span></summary>

<code style="color:#f03e3e">Er komt geen migratie.</code> EF Core meldt dat `Ingredient` een primaire sleutel nodig heeft.

EF Core zoekt een eigenschap die `Id` of `IngredientId` heet. `Nummer` past bij
geen van beide. Hernoem de eigenschap, of wijs ze aan met `HasKey`.

</details>

**Vraag 2**

`Recept` heeft `public int KeukenId`, en in `OnModelCreating` staat niets over
`OnDelete`. De keuken met Id 1 heeft drie recepten. Je verwijdert die keuken en
roept `SaveChangesAsync` op.

Hoeveel recepten van die keuken staan er daarna nog in de database?

<details>
<summary><span style="color:#845ef7"><b>Toon het antwoord</b></span></summary>

<code style="color:#845ef7">0</code>

`int KeukenId` kan niet leeg zijn, dus de relatie is verplicht. Bij een verplichte
relatie kiest EF Core standaard `Cascade`: de recepten gaan mee met hun keuken.
Geen foutmelding, geen waarschuwing.

</details>

**Vraag 3**

Hetzelfde, maar nu met `.OnDelete(DeleteBehavior.Restrict)` op de relatie. Het
verwijderen gebeurt in een DELETE-methode van een controller, zonder `try` en
`catch`.

<details>
<summary><span style="color:#845ef7"><b>Toon het antwoord</b></span></summary>

<code style="color:#f03e3e;font-weight:600">500 Internal Server Error</code>, en de keuken staat nog in de database, met haar drie recepten.

`SaveChangesAsync` gooit een uitzondering, omdat er nog recepten aan de keuken
hangen. Er wordt dus niets verwijderd. Wil je de client een duidelijker antwoord
geven, kijk dan vóór het verwijderen of er nog recepten zijn, en geef een
<code style="color:#f08c00;font-weight:600">400 Bad Request</code> met uitleg.

</details>

**Vraag 4**

Je voegt `public int Bereidingstijd { get; set; }` toe aan `Recept`. Je maakt een
migratie met `Add-Migration`, maar vergeet `Update-Database`. Dan start je de API en
doe je `GET api/recept`.

<details>
<summary><span style="color:#845ef7"><b>Toon het antwoord</b></span></summary>

<code style="color:#f03e3e;font-weight:600">500 Internal Server Error</code>

De migratie staat klaar in je project, maar de database kent ze nog niet. EF Core
vraagt de kolom `Bereidingstijd` op, en PostgreSQL meldt dat die niet bestaat. Het
script maken en het script uitvoeren zijn twee aparte stappen.

</details>

---

## 32. EF Core: opvragen en bewaren

<span style="background:#c2255c;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>DATA</b></span>

### Wat het is

**Een winkelmandje.** Wat je erin legt, eruit haalt of omruilt, is nog niet
betaald. Pas aan de kassa gaat alles in één keer door. Bij EF Core is de context
het mandje: hij noteert wat je toevoegt, wijzigt en verwijdert. Dat noteren heet
de **change tracker**. De kassa is `SaveChangesAsync`: pas dan maakt EF Core er SQL
van en stuurt hij alles naar de database.

Opvragen werkt anders. Een vraag gaat meteen naar de database, op het moment dat
je het antwoord opeist.

De voorbeelden gebruiken de `ReceptContext` uit
[hoofdstuk 31](#31-ef-core-model-en-database), bewaard in een veld `_context`.

### Hoe het eruitziet

```csharp
// opvragen
List<Recept> alle = await _context.Recepten.ToListAsync();
Recept? vijf = await _context.Recepten.FindAsync(5);
List<Recept> goedkoop = await _context.Recepten
    .Where(r => r.Kostprijs < 5)
    .OrderBy(r => r.Titel)
    .ToListAsync();

// bewaren
_context.Recepten.Add(nieuwRecept);
await _context.SaveChangesAsync();
```

`Where`, `OrderBy` en `Select` ken je uit [hoofdstuk 11](#11-linq). Ze werken hier
net zo, met één verschil: EF Core vertaalt ze naar SQL, en de database doet het
filteren. De asynchrone methodes zoals `ToListAsync` staan in de namespace
`Microsoft.EntityFrameworkCore`. Zonder die `using` bovenaan kent je code ze niet.

### Opvragen: de vraag vertrekt pas als je het antwoord opeist

`_context.Recepten.Where(...)` geeft een `IQueryable<Recept>`. Net als de
`IEnumerable` uit [hoofdstuk 12](#12-ienumerable) is dat een vraag die nog niet
gesteld is. Het verschil: een `IQueryable` wordt vertaald naar SQL, zodat de
database het werk doet in plaats van je programma.

| Bouwt de vraag verder | Stuurt de vraag naar de database |
|---|---|
| `Where`, `OrderBy`, `Select`, `Include`, `Take`, `Skip` | `ToListAsync`, `FirstOrDefaultAsync`, `FindAsync`, `CountAsync`, `AnyAsync`, een `foreach` |

Elke keer dat je dezelfde `IQueryable` uitvoert, vertrekt er een nieuwe vraag naar
de database. Les 05 noemt dit *deferred execution*: uitgestelde uitvoering.

> [!NOTE]
> Geeft een repository een lijst terug, en filter je daarna in de controller met
> `Where`, dan gebeurt dat filteren in het geheugen, op alles wat de database
> stuurde. Zet een filter dus in de vraag zelf, vóór `ToListAsync`.

### Bewaren: wat de context noteert

| Je doet | De context noteert | Bij `SaveChangesAsync` |
|---|---|---|
| `Add(recept)` | nieuw | een `INSERT` |
| een opgevraagd object aanpassen | gewijzigd, vanzelf | een `UPDATE` van de gewijzigde kolommen |
| `Update(recept)`, of `Attach` met `State = EntityState.Modified` | gewijzigd | een `UPDATE` van alle kolommen |
| `Remove(recept)` | te verwijderen | een `DELETE` |
| niets | ongewijzigd | niets |

`SaveChangesAsync` geeft het aantal rijen terug dat het schreef. Een nieuw object
heeft na `Add` nog geen `Id`: de database kiest dat getal pas bij het bewaren.
Daarom staat een `CreatedAtAction` met het nieuwe `Id` altijd ná `SaveChangesAsync`.

`FindAsync` zoekt op de primaire sleutel, en kijkt eerst of de context dat object
al kent. Is dat zo, dan vraagt hij niets aan de database.

### Gerelateerde gegevens: Include en ThenInclude

Vraag je een recept op, dan komt alleen het recept mee. `recept.Keuken` blijft
`null`. EF Core laadt gerelateerde gegevens niet vanzelf: je zegt het met
`Include`.

Stel dat een recept ook een lijst `ReceptIngredienten` heeft, de koppelklasse uit
hoofdstuk 31, en dat elk koppelobject zijn `Ingredient` kent:

```csharp
Recept? recept = await _context.Recepten
    .Include(r => r.Keuken)
    .Include(r => r.ReceptIngredienten)
        .ThenInclude(ri => ri.Ingredient)
    .FirstOrDefaultAsync(r => r.Id == id);
```

`Include` vertrekt altijd van het recept. `ThenInclude` gaat één stap verder vanaf
de vorige `Include`: van het koppelobject naar zijn ingrediënt. Alles samen wordt
één vraag aan de database.

Zonder `Include` zou je per recept een aparte vraag moeten stellen om de keuken op
te halen: één vraag voor de lijst, en dan nog eens N vragen, één per recept. Les 08
noemt dat het **N+1-probleem**. Het gebeurt ongemerkt bij *lazy loading*, waarbij
gegevens pas geladen worden zodra je de eigenschap gebruikt. In EF Core staat lazy
loading standaard uit.

> [!NOTE]
> Bovenaan les 08 staat dat EF Core standaard werkt "via het Lazy Loading principe".
> Daar bedoelt de les: gelinkte gegevens komen niet vanzelf mee. Verderop in de les,
> en in de documentatie van Microsoft, betekent *lazy loading* iets anders: vanzelf
> laden zodra je de eigenschap aanraakt. Dat doet EF Core standaard niet. Zonder
> `Include` blijft de eigenschap gewoon `null`.

### Kringlopen in JSON

Een recept wijst naar zijn keuken, en die keuken heeft een lijst met haar recepten,
waaronder hetzelfde recept. Geef je zo'n object rechtstreeks terug uit een
controller, dan probeert de JSON-omzetter die kring eindeloos te volgen. Hij stopt
met een `JsonException`: *A possible object cycle was detected.*

Les 08 lost dat snel op met één regel in `Program.cs`:
`.AddJsonOptions(o => o.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles)`
achter `AddControllers()`, met `using System.Text.Json.Serialization;` bovenaan. Les
08 schrijft de volledige naam uit, dan heb je die `using` niet nodig. Waar de kring
zich sluit, komt dan `null`.

> [!IMPORTANT]
> Les 08 zegt het zelf: `IgnoreCycles` is een snelle oplossing, maar op het examen
> verwacht men DTO's. Die staan in [hoofdstuk 33](#33-dtos-en-mapster).

### In een generieke repository en een Unit of Work

Les 07 maakt één repository voor alle entiteiten, met de open plaats uit
[hoofdstuk 21](#21-generics). Omdat de repository niet weet welke tabel ze
krijgt, schrijft ze niet `_context.Recepten` maar `_context.Set<TEntity>()`: de
`DbSet` van het type dat ingevuld werd.

De repositories roepen zelf geen `SaveChangesAsync` meer op. Dat doet de **Unit of
Work**: één klasse die alle repositories aanbiedt en ze allemaal dezelfde context
meegeeft. Omdat ze één context delen, noteert die ene context alle wijzigingen. Eén
`SaveChangesAsync` op de Unit of Work bewaart ze dan samen, of geen enkele.

### Wat je ermee kan

| Vorm | Wat het doet | Voorbeeld | Resultaat |
|---|---|---|---|
| <code style="color:#228be6;font-weight:600">ToListAsync</code> | voer de vraag uit, alles in een lijst | `await _context.Recepten.ToListAsync()` | een `List<Recept>` |
| <code style="color:#228be6;font-weight:600">FindAsync</code> | zoek op de primaire sleutel | `await _context.Recepten.FindAsync(5)` | het recept, of <code style="color:#845ef7">null</code> |
| <code style="color:#228be6;font-weight:600">FirstOrDefaultAsync</code> | de eerste die voldoet | `.FirstOrDefaultAsync(r => r.Titel == "Soep")` | het recept, of <code style="color:#845ef7">null</code> |
| <code style="color:#228be6;font-weight:600">AnyAsync</code> | is er minstens één | `.AnyAsync(r => r.KeukenId == 3)` | <code style="color:#845ef7">true</code> of <code style="color:#845ef7">false</code> |
| <code style="color:#228be6;font-weight:600">CountAsync</code> | hoeveel zijn er | `.CountAsync(r => r.Kostprijs > 10)` | een getal |
| <code style="color:#228be6;font-weight:600">Add</code> | noteer als nieuw | `_context.Recepten.Add(recept)` | het `Id` is nog <code style="color:#845ef7">0</code> |
| <code style="color:#228be6;font-weight:600">Update</code> | noteer als gewijzigd | `_context.Recepten.Update(recept)` | — |
| <code style="color:#228be6;font-weight:600">Attach</code> | laat de context een object volgen | `_context.Set<TEntity>().Attach(entity)` | gevolgd als ongewijzigd |
| <code style="color:#228be6;font-weight:600">Entry(...).State</code> | zet zelf de toestand van een object | `_context.Entry(entity).State = EntityState.Modified;` | gevolgd als gewijzigd |
| <code style="color:#228be6;font-weight:600">Remove</code> | noteer om te verwijderen | `_context.Recepten.Remove(recept)` | — |
| <code style="color:#228be6;font-weight:600">SaveChangesAsync</code> | stuur alle genoteerde wijzigingen naar de database | `int n = await _context.SaveChangesAsync();` | het aantal geschreven rijen |
| <code style="color:#228be6;font-weight:600">Include</code> | laad een navigatie-eigenschap mee | `.Include(r => r.Keuken)` | `recept.Keuken` is ingevuld |
| <code style="color:#228be6;font-weight:600">ThenInclude</code> | ga één stap dieper vanaf de vorige `Include` | `.ThenInclude(ri => ri.Ingredient)` | — |
| <code style="color:#228be6;font-weight:600">Set&lt;T&gt;</code> | de `DbSet` van een type dat je pas later kent | `_context.Set<TEntity>()` | in een generieke repository |
| <code style="color:#228be6;font-weight:600">IQueryable&lt;T&gt;</code> | een vraag die naar SQL vertaald wordt | `var vraag = _context.Recepten.Where(...);` | nog niets uitgevoerd |

### Veelgemaakte fouten

> [!WARNING]
> - `Add`, `Update` of `Remove` zonder `SaveChangesAsync` erachter: er gebeurt niets
>   in de database, en je krijgt geen enkele foutmelding.
> - Het `Id` van een nieuw object gebruiken vóór `SaveChangesAsync`. Het staat dan
>   nog op 0.
> - `recept.Keuken.Naam` zonder `Include`: de keuken is niet geladen, en je krijgt
>   een `NullReferenceException`.
> - `Include` werkt niet samen met `FindAsync`. Wil je gerelateerde gegevens mee,
>   gebruik dan `FirstOrDefaultAsync` met een voorwaarde op het `Id`.
> - Een eigen C#-methode in een `Where` kan EF Core niet naar SQL vertalen. Het
>   compileert, maar bij het uitvoeren krijg je *The LINQ expression … could not be
>   translated.*
> - Een `await` vergeten bij een EF-methode, en meteen een tweede oproepen? Dan
>   werkt de context aan twee dingen tegelijk, en dat weigert hij met een
>   uitzondering. Zet `await` voor elke asynchrone EF-methode.

### Voorspel de uitkomst

**Vraag 1**

De tabel `Recepten` is net aangemaakt en nog leeg.

```csharp
var r = new Recept { Titel = "Soep", KeukenId = 1 };
_context.Recepten.Add(r);
Console.WriteLine(r.Id);
await _context.SaveChangesAsync();
Console.WriteLine(r.Id);
```

<details>
<summary><span style="color:#845ef7"><b>Toon het antwoord</b></span></summary>

<code style="color:#845ef7">0</code> en daarna <code style="color:#845ef7">1</code>

Na `Add` is het recept enkel genoteerd. Het `Id` kiest de database, en die heeft het
recept nog niet gezien. Na `SaveChangesAsync` vult EF Core het `Id` in met wat de
database koos.

</details>

**Vraag 2**

De tabel `Recepten` bevat vier recepten.

```csharp
_context.Recepten.Add(new Recept { Titel = "Curry", KeukenId = 2 });
int aantal = await _context.Recepten.CountAsync();
Console.WriteLine(aantal);
```

<details>
<summary><span style="color:#845ef7"><b>Toon het antwoord</b></span></summary>

<code style="color:#845ef7">4</code>

`CountAsync` stelt de vraag aan de database, en daar staat de curry nog niet in. Het
recept ligt nog in het mandje, want `SaveChangesAsync` is niet opgeroepen.

</details>

**Vraag 3**

```csharp
var r = await _context.Recepten.FindAsync(2);
r!.Titel = "Tomatensoep";
await _context.SaveChangesAsync();
```

Er staat nergens `Update`. Heet recept 2 nu in de database *Tomatensoep*?

<details>
<summary><span style="color:#845ef7"><b>Toon het antwoord</b></span></summary>

<span style="color:#37b24d"><b>Ja.</b></span>

De context heeft het recept zelf opgehaald, en volgt het sindsdien. Hij merkt dat
de titel veranderd is, en `SaveChangesAsync` stuurt een `UPDATE`. `Update` heb je
enkel nodig voor een object dat de context niet zelf ophaalde, zoals een object dat
uit de body van een verzoek komt.

</details>

**Vraag 4**

In een nieuwe context, waarin nog niets geladen werd:

```csharp
var r = await _context.Recepten.FirstOrDefaultAsync(x => x.Id == 2);
Console.WriteLine(r!.Keuken.Naam);
```

<details>
<summary><span style="color:#845ef7"><b>Toon het antwoord</b></span></summary>

<code style="color:#f03e3e">NullReferenceException</code> — het programma crasht.

Het recept is gevonden, maar zijn keuken niet meegeladen. `r.Keuken` is `null`,
ook al staat er `= default!` achter de eigenschap. Met `.Include(x => x.Keuken)`
vóór `FirstOrDefaultAsync` lukt het wel.

</details>

**Vraag 5**

```csharp
var duur = _context.Recepten.Where(x => x.Kostprijs > 10);
int aantal = await duur.CountAsync();
List<Recept> lijst = await duur.ToListAsync();
```

Hoeveel keer vraagt deze code iets aan de database?

<details>
<summary><span style="color:#845ef7"><b>Toon het antwoord</b></span></summary>

<code style="color:#845ef7">2</code>

`duur` is een vraag, geen antwoord. `CountAsync` stelt ze één keer, `ToListAsync`
stelt ze opnieuw. Komt er tussen die twee een recept bij, dan kan `aantal` zelfs
verschillen van `lijst.Count`. Heb je beide nodig, vraag dan de lijst op en tel
die.

</details>

**Vraag 6**

Een GET-methode geeft een recept terug, opgehaald met `.Include(x => x.Keuken)`.
De klasse `Keuken` heeft een lijst `Recepten`. In `Program.cs` staat niets over
`ReferenceHandler`.

<details>
<summary><span style="color:#845ef7"><b>Toon het antwoord</b></span></summary>

<code style="color:#f03e3e;font-weight:600">500 Internal Server Error</code>

Het recept wijst naar de keuken, en de keuken heeft het recept in haar lijst
`Recepten`: EF Core vult die lijst aan met wat hij al geladen heeft. De
JSON-omzetter loopt in een kring en stopt met een `JsonException`. Met
`IgnoreCycles` krijg je wel een antwoord, met `null` waar de kring zich sluit. Met
een DTO heb je het probleem niet.

</details>

---

## 33. DTO's en Mapster

<span style="background:#c2255c;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>DATA</b></span>

### Wat het is

**De etalage, niet het magazijn.** Je entiteit is het magazijn: alles wat je
bewaart, met alle relaties en velden die niemand buiten je API hoeft te zien. Een
**DTO**, een *Data Transfer Object*, is de etalage: een klasse of record met enkel
wat de client te zien krijgt, in de vorm die hem past.

Een DTO lost vier dingen tegelijk op:

- geen kringlopen in je JSON, want de DTO wijst niet terug naar zijn ouder (zie
  [hoofdstuk 32](#32-ef-core-opvragen-en-bewaren));
- je stuurt niet meer dan nodig;
- velden die binnen moeten blijven, komen er gewoon niet in;
- je database mag veranderen zonder dat de client het merkt.

Van een entiteit een DTO maken is veel overtypen: `Titel = recept.Titel`, en zo
verder voor elke eigenschap. **Mapster** doet dat voor je.

### Hoe het eruitziet

```csharp
public class ReceptDto
{
    public int Id { get; set; }
    public string Titel { get; set; } = default!;
    public string KeukenNaam { get; set; } = default!;
}
```

Les 09 toont ook de kortere vorm als `record`, uit [hoofdstuk 14](#14-record):

```csharp
public record ReceptDto(int Id, string Titel, string KeukenNaam);
```

Omzetten, na `dotnet add package Mapster` en `using Mapster;`:

```csharp
ReceptDto dto = recept.Adapt<ReceptDto>();
List<ReceptDto> dtos = recepten.Adapt<List<ReceptDto>>();
```

`Adapt` is een *extension method*: een methode die Mapster toevoegt aan elk object,
zodat je ze kan oproepen alsof ze er altijd al stond.

### Hoe Mapster een eigenschap vult

Mapster overloopt elke eigenschap van de DTO en zoekt in de bron:

1. een eigenschap met **exact dezelfde naam**, hoofdletters inbegrepen: `Titel` uit
   `Titel`;
2. een naam die aan elkaar geplakt is uit een eigenschap en de eigenschap daarvan:
   `KeukenNaam` uit `Keuken.Naam`. Dat heet **platslaan** (*flattening*);
3. vindt hij niets, dan laat hij de eigenschap op haar standaardwaarde staan, zonder
   foutmelding.

Wat in de bron staat en niet in de DTO, laat hij gewoon liggen.

> [!NOTE]
> Platslaan werkt enkel als de keuken ook geladen is. Zonder `Include` is de keuken
> `null`. Mapster vangt dat op, zonder crash, en laat `KeukenNaam` gewoon leeg.

### Eigen regels: een MapperProfile

Past een naam niet, of moet een waarde ergens anders vandaan komen, dan schrijf je
een regel. Les 09 zet die regels samen in één klasse die `IRegister` belooft.
Stel een `ReceptDetailDto` met een tekst `Herkomst` en een lijst `Ingredienten`:

```csharp
public class MapperProfile : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Recept, ReceptDetailDto>()
            .Map(dest => dest.Herkomst, src => src.Keuken.Naam)
            .Map(dest => dest.Ingredienten, src => src.ReceptIngredienten);
    }
}
```

Lees `.Map(dest => dest.Herkomst, src => src.Keuken.Naam)` als: *vul in het doel
`Herkomst` met de naam van de keuken uit de bron*. Wat wel overeenkomt, blijft
Mapster zelf doen. Voor de lijst `Ingredienten` zoekt Mapster op zijn beurt een
regel om één koppelobject om te zetten.

Die klasse doet pas iets als `Program.cs` haar laat zoeken, met één regel:

```csharp
TypeAdapterConfig.GlobalSettings.Scan(Assembly.GetExecutingAssembly());
```

`Scan` doorzoekt je project naar klassen die `IRegister` beloven, en leest hun
regels. Bovenaan heb je `using Mapster;` en `using System.Reflection;` nodig.

> [!NOTE]
> Les 09 raadt aan om in een profiel geen berekeningen te doen, zoals een totaalprijs.
> Code in een profiel is moeilijk te testen en te debuggen. Hou het profiel bij
> overzetten; het rekenwerk hoort in een controller, een service of een hulpklasse.

### Wanneer wel, wanneer niet

<span style="color:#37b24d"><b>Wel</b></span> — alles wat je API teruggeeft aan de
client. Voor wat binnenkomt maak je vaak een aparte DTO zonder `Id`, want dat kiest
de database.

<span style="color:#f03e3e"><b>Niet</b></span> — binnen je eigen code, tussen
repository en database. Daar werk je met de entiteiten zelf.

### Wat je ermee kan

| Vorm | Wat het doet | Voorbeeld | Resultaat |
|---|---|---|---|
| <code style="color:#228be6;font-weight:600">Adapt&lt;T&gt;</code> | maak een nieuw object van type `T` | `recept.Adapt<ReceptDto>()` | een `ReceptDto` |
| <code style="color:#228be6;font-weight:600">Adapt</code> op een lijst | zet elk item om | `recepten.Adapt<List<ReceptDto>>()` | een `List<ReceptDto>` |
| <code style="color:#228be6;font-weight:600">bron.Adapt(doel)</code> | kopieer in een object dat al bestaat | `dto.Adapt(recept)` | `recept` krijgt de waarden van `dto` |
| <code style="color:#228be6;font-weight:600">IRegister</code> | de interface voor een klasse met eigen regels | `class MapperProfile : IRegister` | — |
| <code style="color:#228be6;font-weight:600">Register</code> | de methode waarin je de regels zet | `public void Register(TypeAdapterConfig config)` | — |
| <code style="color:#228be6;font-weight:600">NewConfig</code> | begin een regel voor dit paar | `config.NewConfig<Recept, ReceptDto>()` | vervangt een eerdere regel voor hetzelfde paar |
| <code style="color:#228be6;font-weight:600">ForType</code> | maak of vul een regel aan | `config.ForType<Recept, ReceptDto>()` | een eerdere regel blijft gelden |
| <code style="color:#228be6;font-weight:600">Map</code> | vul deze doel-eigenschap met dit | `.Map(dest => dest.Herkomst, src => src.Keuken.Naam)` | — |
| <code style="color:#228be6;font-weight:600">Ignore</code> | laat deze doel-eigenschap met rust | `.Ignore(dest => dest.Id)` | `Id` wordt niet overschreven |
| <code style="color:#228be6;font-weight:600">Scan</code> | zoek alle `IRegister`-klassen en lees hun regels | `GlobalSettings.Scan(...)` | één regel in `Program.cs`, zie hierboven |

### Veelgemaakte fouten

> [!WARNING]
> - Een naam die net anders is, zoals `Naam` in de bron en `Titel` in de DTO, geeft
>   geen fout. De eigenschap blijft gewoon leeg. Kijk bij een lege eigenschap eerst
>   naar de namen.
> - De `Scan`-regel vergeten in `Program.cs`: alles compileert en draait, maar je eigen
>   regels worden nooit gelezen. Enkel wat vanzelf overeenkomt, wordt ingevuld.
> - Twee keer `NewConfig` voor hetzelfde paar: de tweede vervangt de eerste helemaal.
>   Wil je aanvullen, gebruik dan `ForType`, of zet alles in één regel.
> - DTO's gebruiken én `IgnoreCycles` laten staan. Les 09 haalt die regel weer weg:
>   een DTO zonder terugwijzende eigenschappen heeft hem niet nodig.

### Voorspel de uitkomst

Deze vragen gebruiken gewone klassen met eigenschappen, zonder database.

**Vraag 1**

```csharp
public class Dier
{
    public int Id { get; set; }
    public string Naam { get; set; } = "";
    public string Soort { get; set; } = "";
}

public class DierDto
{
    public string Naam { get; set; } = "";
    public string Ras { get; set; } = "";
}

var dto = new Dier { Id = 3, Naam = "Bo", Soort = "hond" }.Adapt<DierDto>();
Console.WriteLine("[" + dto.Naam + "] [" + dto.Ras + "]");
```

<details>
<summary><span style="color:#845ef7"><b>Toon het antwoord</b></span></summary>

<code style="color:#845ef7">[Bo] []</code>

`Naam` staat aan beide kanten, dus die wordt gekopieerd. `Ras` vindt geen bron en
houdt zijn beginwaarde, een lege tekst. `Id` en `Soort` hebben geen plaats in de DTO
en vallen weg. Nergens een foutmelding.

</details>

**Vraag 2**

```csharp
public class Stad
{
    public string Naam { get; set; } = "";
}

public class Leerling
{
    public string Naam { get; set; } = "";
    public Stad Stad { get; set; } = new();
}

public class LeerlingDto
{
    public string Naam { get; set; } = "";
    public string StadNaam { get; set; } = "";
}

var anna = new Leerling { Naam = "Anna", Stad = new Stad { Naam = "Gent" } };
var dto = anna.Adapt<LeerlingDto>();
Console.WriteLine(dto.StadNaam);
```

<details>
<summary><span style="color:#845ef7"><b>Toon het antwoord</b></span></summary>

<code style="color:#845ef7">Gent</code>

`StadNaam` bestaat niet in `Leerling`, maar `Stad` gevolgd door `Naam` wel. Mapster
slaat dat vanzelf plat. Een eigen regel heb je pas nodig als de naam dat patroon
niet volgt, zoals bij `Woonplaats`.

</details>

**Vraag 3**

`LeerlingDto` krijgt er twee teksten bij: `Woonplaats` en `Klas`. `Leerling` krijgt
een eigenschap `Klasgroep`, met daarin een `Code`. Het profiel bevat:

```csharp
config.NewConfig<Leerling, LeerlingDto>()
    .Map(dest => dest.Woonplaats, src => src.Stad.Naam);

config.NewConfig<Leerling, LeerlingDto>()
    .Map(dest => dest.Klas, src => src.Klasgroep.Code);
```

Welke van de twee wordt ingevuld?

<details>
<summary><span style="color:#845ef7"><b>Toon het antwoord</b></span></summary>

Enkel <code style="color:#845ef7">Klas</code>.

De tweede `NewConfig` voor hetzelfde paar gooit de eerste regel weg. `Woonplaats`
blijft leeg. Zet beide `Map`-regels achter één `NewConfig`, of begin de tweede met
`ForType`.

</details>

**Vraag 4**

Het profiel van vraag 3 is verbeterd en zet beide regels achter één `NewConfig`.
Maar in `Program.cs` ontbreekt de regel met `Scan`.

Wat staat er in `Naam`, `Woonplaats` en `Klas` van de DTO?

<details>
<summary><span style="color:#845ef7"><b>Toon het antwoord</b></span></summary>

`Naam` is ingevuld, `Woonplaats` en `Klas` blijven leeg.

Zonder `Scan` leest niemand het profiel. Mapster doet enkel wat het zelf kan:
eigenschappen met dezelfde naam kopiëren, en platslaan. Er is geen enkele
foutmelding die je op het spoor zet.

</details>

---

## 34. Dapper

<span style="background:#c2255c;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>DATA</b></span>

> [!NOTE]
> Les 08 zegt het duidelijk: op het examen ligt de focus op EF Core. Dapper hoef je
> niet zelf te kunnen schrijven, maar je moet kunnen uitleggen wat het is, en
> waarom en wanneer iemand ervoor kiest. Dit hoofdstuk helpt je de code te lezen en
> het verschil te begrijpen.

### Wat het is

**Zelf de taal van de database spreken.** EF Core is een tolk: jij schrijft C#, hij
maakt er SQL van. Met Dapper schrijf je de SQL zelf, en Dapper doet enkel de laatste
stap: de rijen die terugkomen omzetten in objecten. Zo'n kleine tussenpersoon heet
een **micro-ORM**. EF Core is een volledige ORM.

| | EF Core | Dapper |
|---|---|---|
| **SQL** | maakt het zelf, uit LINQ | schrijf je zelf |
| **Wijzigingen bijhouden** | ja, de change tracker | nee |
| **Tabellen en migraties** | ja | nee; de database moet al bestaan |
| **Gerelateerde gegevens** | `Include` | een `JOIN` in je SQL, en zelf de objecten koppelen |
| **Sterk in** | schrijven, en de structuur van je database | snel lezen, ingewikkelde rapporten |

Grotere toepassingen combineren ze vaak. EF Core doet het schrijven, met change
tracking en een Unit of Work. Dapper doet het lezen waar elke milliseconde telt.
Les 08 noemt dat het hybride patroon, naar het principe *Command Query Separation*.

### Hoe het eruitziet

```csharp
using Dapper;

string sql = """
    SELECT * FROM "Recepten"
    WHERE "Id" = @Id
    """;

Recept? recept = await _dbConnection.QueryFirstOrDefaultAsync<Recept>(sql, new { Id = id });
```

`_dbConnection` is een `IDbConnection`: een verbinding met de database, die de
repository in haar constructor krijgt. Dapper voegt zijn methodes toe aan die
verbinding.

De drie aanhalingstekens openen een *raw string*: een tekst, op één regel of over
meerdere regels, waarin gewone aanhalingstekens gewoon mogen staan. Dat komt van pas, want PostgreSQL wil de
namen van tabellen en kolommen die EF Core maakte tussen aanhalingstekens. Zie de
waarschuwing verderop.

### Parameters: altijd met @

`@Id` in de SQL is een plaatshouder. De waarde komt uit het **anonieme object**
`new { Id = id }` uit [hoofdstuk 13](#13-class): de naam na `@` moet de naam van een
eigenschap in dat object zijn. Dapper geeft de waarde apart door aan de database,
nooit als stuk tekst in de SQL.

Plak je een waarde zelf in de SQL, met een <code>&#36;</code> en accolades zoals in
[hoofdstuk 1](#1-tekst), dan kan wie die waarde aanlevert, eigen SQL meesturen. Dat
heet **SQL-injectie**. Vraag 1 toont hoe weinig daarvoor nodig is.

### Een rij in twee objecten: multi-mapping

Met een `JOIN` komen een recept en zijn keuken samen op één rij. Dapper moet weten
waar het recept ophoudt en de keuken begint:

```csharp
string sql = """
    SELECT r.*, k.*
    FROM "Recepten" r
    INNER JOIN "Keukens" k ON r."KeukenId" = k."Id"
    WHERE r."Id" = @Id
    """;

var resultaat = await _dbConnection.QueryAsync<Recept, Keuken, Recept>(
    sql,
    (recept, keuken) =>
    {
        recept.Keuken = keuken;
        return recept;
    },
    param: new { Id = id },
    splitOn: "Id");
```

- Tussen de punthaken staan de types per stuk van de rij, en als laatste wat je
  terugkrijgt.
- `splitOn: "Id"` zegt: zodra je opnieuw een kolom `Id` tegenkomt, begint het
  volgende object. `Id` is ook wat Dapper aanneemt als je niets opgeeft.
- De lambda koppelt de stukken aan elkaar, zoals `Include` dat bij EF Core voor je
  doet.

Bij één op veel staat de ouder op elke rij opnieuw, één keer per kind. Les 08 houdt
daarom een `Dictionary` bij uit [hoofdstuk 7](#7-dictionary), met het `Id` als
sleutel, zodat elke ouder maar één keer in het resultaat komt.

### Wat je ermee kan

In de voorbeelden is `db` de verbinding, `sql` de tekst van de query en `param` een
anoniem object met de parameters, zoals `new { Id = 5 }`. Voor elke oproep staat
nog een `await`.

| Vorm | Wat het doet | Voorbeeld | Resultaat |
|---|---|---|---|
| <code style="color:#228be6;font-weight:600">QueryAsync&lt;T&gt;</code> | alle rijen, elk als object | `db.QueryAsync<Recept>(sql)` | een `IEnumerable<Recept>` |
| <code style="color:#228be6;font-weight:600">QueryFirstOrDefaultAsync&lt;T&gt;</code> | de eerste rij, of niets | `db.QueryFirstOrDefaultAsync<Recept>(sql, param)` | het recept, of <code style="color:#845ef7">null</code> |
| <code style="color:#228be6;font-weight:600">QueryFirstAsync&lt;T&gt;</code> | de eerste rij, die er moet zijn | `db.QueryFirstAsync<Recept>(sql, param)` | geen rij: <code style="color:#f03e3e">InvalidOperationException</code> |
| <code style="color:#228be6;font-weight:600">QuerySingleOrDefaultAsync&lt;T&gt;</code> | precies één rij, of niets | `db.QuerySingleOrDefaultAsync<Recept>(sql, param)` | meer dan één rij: <code style="color:#f03e3e">InvalidOperationException</code> |
| <code style="color:#228be6;font-weight:600">ExecuteAsync</code> | voer een `INSERT`, `UPDATE` of `DELETE` uit | `db.ExecuteAsync(sql, param)` | het aantal geraakte rijen |
| <code style="color:#228be6;font-weight:600">ExecuteScalarAsync&lt;T&gt;</code> | voer uit en geef één waarde terug | `db.ExecuteScalarAsync<int>(sql)` | één getal of tekst |
| <code style="color:#228be6;font-weight:600">@Naam</code> + `new { Naam = ... }` | een parameter | `WHERE "Titel" = @Titel` met `new { Titel = titel }` | veilig voor SQL-injectie |
| <code style="color:#228be6;font-weight:600">QueryAsync&lt;A, B, A&gt;</code> | één rij verdelen over twee objecten | met een lambda en `splitOn` | het object `A`, met `B` eraan gekoppeld |
| <code style="color:#228be6;font-weight:600">splitOn</code> | de kolom waar het volgende object begint | `splitOn: "Id"` | — |

Dapper koppelt elke kolom aan de eigenschap met dezelfde naam. Anders dan bij
Mapster maken hoofdletters daarbij niet uit: een kolom `titel` vult `Titel`. Vindt
hij geen eigenschap, dan slaat hij de kolom over, zonder foutmelding.

### Veelgemaakte fouten

> [!WARNING]
> - **Een waarde in de SQL plakken** met <code>&#36;</code> en accolades. Gebruik altijd `@Naam` en
>   een anoniem object.
> - **Namen zonder aanhalingstekens.** Tabellen die EF Core in PostgreSQL maakte,
>   heten exact `Recepten`, met hoofdletter. PostgreSQL maakt van elke naam zonder
>   aanhalingstekens kleine letters, en zoekt dan `recepten`. Die bestaat niet.
>   Schrijf `"Recepten"` en `"KeukenId"`.
> - **`SCOPE_IDENTITY()` in PostgreSQL.** Het voorbeeld in les 08 haalt het nieuwe
>   `Id` op met `SELECT CAST(SCOPE_IDENTITY() as int)`. Dat is SQL Server. In
>   PostgreSQL zet je `RETURNING "Id"` achter de `INSERT`, en lees je dat getal met
>   `ExecuteScalarAsync<int>`.
> - **Een `IDbConnection` vragen die niet geregistreerd is.** Les 08 toont niet hoe
>   die in `Program.cs` komt. Je registreert ze met `AddScoped`, met een lambda die
>   een `NpgsqlConnection` maakt met je connection string. Zonder registratie krijg je
>   dezelfde fout als in [hoofdstuk 27](#27-programcs-en-dependency-injection).

### Voorspel de uitkomst

`db` is een verbinding met een database waarin EF Core de tabel `"Recepten"` maakte.
Die bevat twaalf recepten, en geen enkel recept heeft een vreemde titel.

**Vraag 1**

```csharp
string titel = "' OR '1'='1";
string sql = $"SELECT * FROM \"Recepten\" WHERE \"Titel\" = '{titel}'";
var recepten = await db.QueryAsync<Recept>(sql);
Console.WriteLine(recepten.Count());
```

<details>
<summary><span style="color:#845ef7"><b>Toon het antwoord</b></span></summary>

<code style="color:#845ef7">12</code>

Na het invullen staat er `WHERE "Titel" = '' OR '1'='1'`. Het tweede deel is altijd
waar, dus elke rij voldoet. Met `WHERE "Titel" = @Titel` en `new { Titel = titel }`
zoekt de database letterlijk naar die vreemde titel, en vindt ze er
<code style="color:#845ef7">0</code>.

</details>

**Vraag 2**

```csharp
int n = await db.ExecuteAsync(
    """DELETE FROM "Recepten" WHERE "Id" = @Id""",
    new { Id = 999 });
Console.WriteLine(n);
```

<details>
<summary><span style="color:#845ef7"><b>Toon het antwoord</b></span></summary>

<code style="color:#845ef7">0</code>

Er is geen recept 999, dus er wordt niets verwijderd. Dat is geen fout: de
database meldt gewoon dat nul rijen geraakt zijn. In een controller is dat het
moment voor een `NotFound`.

</details>

**Vraag 3**

```csharp
var r = await db.QueryFirstOrDefaultAsync<Recept>(
    """SELECT "Id", "Titel" AS naam FROM "Recepten" WHERE "Id" = 1""");
Console.WriteLine("[" + r!.Titel + "]");
```

<details>
<summary><span style="color:#845ef7"><b>Toon het antwoord</b></span></summary>

<code style="color:#845ef7">[]</code>

De kolom heet in het antwoord `naam`, en `Recept` heeft geen eigenschap met die
naam. Dapper slaat de kolom over. `Titel` krijgt dus niets, en blijft `null`. Het
`Id` is wel ingevuld.

</details>

**Vraag 4**

```csharp
var alle = await db.QueryAsync<Recept>("SELECT * FROM Recepten");
```

<details>
<summary><span style="color:#845ef7"><b>Toon het antwoord</b></span></summary>

<code style="color:#f03e3e">Een uitzondering van PostgreSQL</code>: *relation "recepten" does not exist*. In een API wordt dat een <code style="color:#f03e3e;font-weight:600">500</code>.

Zonder aanhalingstekens maakt PostgreSQL er `recepten` van, met kleine letters. EF
Core maakte de tabel als `"Recepten"`. Voor PostgreSQL zijn dat twee verschillende
namen.

</details>

**Vraag 5**

```csharp
var r = await db.QueryFirstOrDefaultAsync<Recept>(
    """SELECT * FROM "Recepten" WHERE "Id" = @Id""",
    new { Id = 999 });
Console.WriteLine(r is null);
```

<details>
<summary><span style="color:#845ef7"><b>Toon het antwoord</b></span></summary>

<code style="color:#845ef7">True</code>

Geen rij gevonden, dus `null`, net als `FirstOrDefault` in
[hoofdstuk 11](#11-linq). Met `QueryFirstAsync` was het een
`InvalidOperationException` geweest.

</details>

---

# Veelgemaakte verwarringen

Dertig paren die op elkaar lijken en iets anders doen. Dit is het deel waar je
het vaakst naar terug zal bladeren.

## `Length`, `Count` en `Count()`

Een array en een tekst zeggen `Length`. Een `List` zegt `Count`, zonder haakjes,
en weet het meteen. `Count()` met haakjes is LINQ: dat werkt op elke verzameling,
maar loopt ze daarvoor helemaal af. Op een lijst neem je de eigenschap.
Hoofdstukken [5](#5-array), [6](#6-list) en [11](#11-linq).

## Array tegenover List

Een array heeft een vaste lengte, een `List` groeit mee. In de praktijk kies je
bijna altijd de `List`, tenzij de hoeveelheid echt vastligt.
Hoofdstukken [5](#5-array) en [6](#6-list).

## List tegenover Dictionary

Zoek je in een `List` telkens met `Find` of `FirstOrDefault` op dezelfde
eigenschap — een naam, een code — dan wil je eigenlijk een `Dictionary` met die
eigenschap als sleutel. De `List` kijkt elk item na tot hij het vindt, de
`Dictionary` weet meteen waar hij moet zijn. Zoek je afwisselend op verschillende
eigenschappen, of doet de volgorde ertoe, dan blijft de `List` de betere keuze.
Hoofdstukken [6](#6-list) en [7](#7-dictionary).

## Queue tegenover Stack

Allebei laten ze je enkel aan één kant bij. Stel jezelf de vraag: *wie komt er als
volgende aan de beurt?* Wie het langst wacht — `Queue`. Wie het laatst kwam —
`Stack`. Hoofdstukken [9](#9-queue) en [10](#10-stack).

## `Remove`, `RemoveAt` en `RemoveAll`

`Remove` wil het item, `RemoveAt` wil een positie, `RemoveAll` wil een
voorwaarde. Ze geven ook iets anders terug: `Remove` een ja of nee, `RemoveAll`
het aantal dat weg is, `RemoveAt` niets. En `Remove` haalt enkel het eerste
voorkomen weg. Hoofdstuk [6](#6-list).

## `Parse` tegenover `TryParse`

`Parse` gelooft je en crasht als je ongelijk hebt. `TryParse` vraagt het na en
geeft `false`. Alles wat van buiten komt, gaat door `TryParse`.
Hoofdstuk [2](#2-getallen-en-omzetten).

## `TryParse` tegenover `try`/`catch`

Allebei voorkomen ze dat je programma stopt op een tekst die geen getal is.
`TryParse` kijkt vooraf en geeft `false`; `try`/`catch` laat de fout gebeuren en
vangt ze daarna op. Kan je vooraf kijken, doe dat dan: het is sneller en zegt
duidelijker wat je bedoelt. Houd `try`/`catch` voor wat je vooraf niet kan
nakijken, zoals een bestand. Hoofdstukken [2](#2-getallen-en-omzetten) en
[23](#23-uitzonderingen).

## `=` tegenover `==`

`=` geeft een variabele een waarde, `==` vraagt of twee waarden gelijk zijn. In
een `if` hoort altijd `==`. C# weigert `if (x = 5)`, want een toewijzing is geen
ja of nee. Hoofdstuk [4](#4-beslissen-en-herhalen).

## `decimal` tegenover `double`

Allebei zijn het kommagetallen. Een `double` is snel, maar wijkt soms een heel
klein beetje af. Een `decimal` rekent tot op de cent juist. Voor geld en prijzen
neem je `decimal`, en dan schrijf je een `m` achter elk getal: `19.99m`.
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
beter aan. Hoofdstukken [6](#6-list) en [11](#11-linq).

## `First` tegenover `Single`

`First` zegt: er is er minstens één, geef mij de eerste. `Single` zegt: er is er
precies één, en als dat niet klopt is er iets grondig mis. Gebruik `Single` op een
Id, en `First` op een sortering. Hoofdstuk [11](#11-linq).

## `Where` tegenover `Select`

`Where` houdt items over, `Select` verbouwt ze. Krijg je mínder items terug dan je
erin stak, dan heb je gefilterd — dat is `Where`. Krijg je er evenveel terug maar
van een ander soort, bijvoorbeeld enkel de namen, dan heb je verbouwd — dat is
`Select`. Hoofdstuk [11](#11-linq).

## `Max` tegenover `MaxBy`

`Max` geeft de waarde, `MaxBy` geeft het item dat die waarde draagt. Dezelfde
verhouding geldt voor `Min` en `MinBy`. Hoofdstuk [11](#11-linq).

## `Any()` tegenover `Count() > 0`

Hetzelfde antwoord, maar `Any` stopt bij het eerste item dat voldoet. Op een lijst
van vijf maakt dat niets uit; op een databasetabel van vijftigduizend wel.
Hoofdstuk [11](#11-linq).

## Wat een methode teruggeeft, en wat ze verandert

`lijst.Sort()` verandert je lijst en geeft niets terug. `lijst.OrderBy(...)` laat
je lijst met rust en geeft een nieuw resultaat. Hetzelfde geldt voor `Trim` en
`ToUpper` bij tekst: die veranderen niets, ze geven een nieuwe tekst terug.
Compileert je regel niet, of lijkt hij niets te doen, dan heb je meestal de
verkeerde van de twee te pakken. Hoofdstukken [1](#1-tekst), [6](#6-list) en
[11](#11-linq).

## `IEnumerable` tegenover `List`

Een `IEnumerable` is een vraag die nog gesteld moet worden. Een `List` is een
antwoord dat al klaarligt. `Where` voert niets uit tot je het resultaat doorloopt;
verandert je bron intussen, dan verandert je antwoord mee. `ToList()` stelt de
vraag meteen en legt het antwoord vast. Hoofdstuk [12](#12-ienumerable).

## Abstracte klasse tegenover `interface`

Een abstracte klasse zegt wat iets **is**, en geeft gegevens en uitgewerkte
methodes mee. Een interface zegt wat iets **kan**, en geeft niets mee. Een klasse
erft van hoogstens één abstracte klasse, maar kan zoveel interfaces beloven als ze
wil. Delen je klassen gegevens, dan is het een abstracte klasse; delen ze enkel een
vaardigheid, dan een interface. Hoofdstukken [19](#19-interface) en
[20](#20-overerving-en-abstracte-klassen).

## `private` tegenover `protected`

Allebei verbergen ze iets voor de buitenwereld. Het verschil zit bij wie erft:
`private` is enkel zichtbaar in de klasse zelf, `protected` ook in elke klasse die
ervan erft. Kies `private`, tenzij een afgeleide klasse het echt nodig heeft.
Hoofdstuk [20](#20-overerving-en-abstracte-klassen).

## `static` tegenover een gewoon veld

Een gewoon veld heeft elk object apart. Een `static` veld is er één keer, voor de
hele klasse. Daarom onthoudt een `static` lijst in een controller wat er
toegevoegd werd, terwijl een gewone lijst bij elk verzoek opnieuw leeg begint.
Hoofdstuk [24](#24-static-const-en-readonly).

## `const` tegenover `readonly`

Allebei veranderen ze niet meer. Een `const` ligt al vast bij het compileren en
kan enkel een getal, een tekst of een `bool` zijn. Een `readonly` veld vul je één
keer in, bij de declaratie of in de constructor. Het kan dus ook iets zijn dat
pas bekend is als het programma draait, zoals een repository.
Hoofdstuk [24](#24-static-const-en-readonly).

## `{id}` tegenover `{id:int}`

Allebei halen ze een waarde uit de route. Zonder beperking past de route bij elke
tekst, en geeft een tekst die geen getal is een 400. Met `:int` past de route
enkel bij een geheel getal, en geeft al de rest een 404.
Hoofdstuk [25](#25-attributen-en-routes).

## `ActionResult<T>` tegenover `IActionResult`

`ActionResult<T>` zegt wat er terugkomt, en laat je dat ook rechtstreeks
teruggeven. `IActionResult` kent enkel antwoorden, dus daar pak je je gegevens zelf
in met `Ok(...)`. Geeft een methode gegevens terug, neem dan `ActionResult<T>`.
Geeft ze enkel een statuscode terug, dan volstaat `IActionResult`.
Hoofdstuk [26](#26-de-helpers-van-een-controller).

## `AddScoped` tegenover `AddSingleton`

`AddScoped` maakt één object per verzoek, `AddSingleton` één voor de hele app.
Bewaart een dienst zelf gegevens in het geheugen, dan is een scoped dienst ze na
elk verzoek kwijt, terwijl een singleton ze onthoudt. `AddTransient` maakt elke
keer een nieuw object. Hoofdstuk [27](#27-programcs-en-dependency-injection).

## `await` tegenover `.Result`

Allebei geven ze je wat er in een `Task` zit. `await` laat de thread intussen vrij
voor ander werk. `.Result` houdt hem vast tot het antwoord er is, en kan een
programma zelfs laten vastlopen. In een API gebruik je altijd `await`.
Hoofdstuk [29](#29-asynchroon-werken).

## `IEnumerable` tegenover `IQueryable`

Allebei zijn het vragen die nog niet gesteld zijn. Een `IEnumerable` voert je
programma zelf uit, op wat al in het geheugen zit. Een `IQueryable` wordt naar SQL
vertaald, en de database voert hem uit. Filter je pas na `ToListAsync`, dan komt
eerst alles uit de database, en filtert je programma daarna.
Hoofdstukken [12](#12-ienumerable) en [32](#32-ef-core-opvragen-en-bewaren).

## `Add` tegenover `SaveChangesAsync`

`Add` legt iets in het mandje, `SaveChangesAsync` rekent af. Na `Add` staat er nog
niets in de database, en heeft het object nog geen `Id`. Hetzelfde geldt voor
`Update` en `Remove`. Hoofdstuk [32](#32-ef-core-opvragen-en-bewaren).

## `Include` tegenover `ThenInclude`

`Include` vertrekt altijd van het object dat je opvraagt. `ThenInclude` gaat één
stap verder vanaf de vorige `Include`. De keuken van een recept is een `Include`.
De ingrediënten achter de koppelobjecten van een recept zijn een `Include`, gevolgd
door een `ThenInclude`. Hoofdstuk [32](#32-ef-core-opvragen-en-bewaren).

## `Cascade` tegenover `Restrict`

Allebei zeggen ze wat er gebeurt als je iets verwijdert waar nog andere rijen aan
hangen. `Cascade` neemt die rijen mee, zonder iets te vragen. `Restrict` weigert,
en laat jou eerst opruimen. Bij een verplichte relatie is `Cascade` de standaard.
Hoofdstuk [31](#31-ef-core-model-en-database).

## `-1`, `null` en `0`

`IndexOf` geeft `-1` wanneer er niets gevonden is. `FirstOrDefault` geeft `null`
bij objecten, maar `0` bij getallen. `TryGetValue` zet zijn uitkomst op de
standaardwaarde. Drie manieren om "niets" te zeggen, en elk vraagt een andere
controle. Hoofdstukken [1](#1-tekst), [7](#7-dictionary), [11](#11-linq) en
[17](#17-omgaan-met-niets).

---

# Index

Alfabetisch, met het hoofdstuk waar de uitleg staat.

| Naam | Hoofdstuk |
|---|---|
| <code style="color:#228be6;font-weight:600">abstract</code> | [20](#20-overerving-en-abstracte-klassen) |
| <code style="color:#228be6;font-weight:600">ActionResult&lt;T&gt;</code> | [26](#26-de-helpers-van-een-controller) |
| <code style="color:#228be6;font-weight:600">Adapt</code> | [33](#33-dtos-en-mapster) |
| <code style="color:#228be6;font-weight:600">Add</code> (database) | [32](#32-ef-core-opvragen-en-bewaren) |
| <code style="color:#228be6;font-weight:600">Add</code> (dictionary) | [7](#7-dictionary) |
| <code style="color:#228be6;font-weight:600">Add</code> (lijst) | [6](#6-list) |
| <code style="color:#228be6;font-weight:600">Add-Migration</code> | [31](#31-ef-core-model-en-database) |
| <code style="color:#228be6;font-weight:600">AddDays</code> | [3](#3-datum-en-tijd) |
| <code style="color:#228be6;font-weight:600">AddDbContext</code> | [31](#31-ef-core-model-en-database) |
| <code style="color:#228be6;font-weight:600">AddMonths</code> | [3](#3-datum-en-tijd) |
| <code style="color:#228be6;font-weight:600">AddRange</code> | [6](#6-list) |
| <code style="color:#228be6;font-weight:600">AddScoped</code> | [27](#27-programcs-en-dependency-injection) |
| <code style="color:#228be6;font-weight:600">AddSingleton</code> | [27](#27-programcs-en-dependency-injection) |
| <code style="color:#228be6;font-weight:600">AddTransient</code> | [27](#27-programcs-en-dependency-injection) |
| <code style="color:#228be6;font-weight:600">All</code> | [11](#11-linq) |
| anoniem object | [13](#13-class) |
| <code style="color:#228be6;font-weight:600">Any</code> | [11](#11-linq) |
| <code style="color:#228be6;font-weight:600">AnyAsync</code> | [32](#32-ef-core-opvragen-en-bewaren) |
| <code style="color:#228be6;font-weight:600">[ApiController]</code> | [25](#25-attributen-en-routes) |
| array | [5](#5-array) |
| <code style="color:#228be6;font-weight:600">as</code> | [22](#22-types-herkennen) |
| <code style="color:#228be6;font-weight:600">async</code> | [29](#29-asynchroon-werken) |
| <code style="color:#228be6;font-weight:600">Attach</code> | [32](#32-ef-core-opvragen-en-bewaren) |
| attribuut | [25](#25-attributen-en-routes) |
| <code style="color:#228be6;font-weight:600">Average</code> | [11](#11-linq) |
| <code style="color:#228be6;font-weight:600">await</code> | [29](#29-asynchroon-werken) |
| <code style="color:#228be6;font-weight:600">BadRequest</code> | [26](#26-de-helpers-van-een-controller) |
| <code style="color:#228be6;font-weight:600">base</code> | [20](#20-overerving-en-abstracte-klassen) |
| basisklasse | [20](#20-overerving-en-abstracte-klassen) |
| <code style="color:#228be6;font-weight:600">break</code> | [4](#4-beslissen-en-herhalen) |
| <code style="color:#228be6;font-weight:600">Build</code> | [27](#27-programcs-en-dependency-injection) |
| <code style="color:#228be6;font-weight:600">Cascade</code> | [31](#31-ef-core-model-en-database) |
| <code style="color:#228be6;font-weight:600">catch</code> | [23](#23-uitzonderingen) |
| change tracker | [32](#32-ef-core-opvragen-en-bewaren) |
| <code style="color:#228be6;font-weight:600">class</code> | [13](#13-class) |
| <code style="color:#228be6;font-weight:600">Clear</code> | [6](#6-list) |
| <code style="color:#228be6;font-weight:600">CompareTo</code> | [1](#1-tekst) |
| <code style="color:#228be6;font-weight:600">const</code> | [24](#24-static-const-en-readonly) |
| constructor | [13](#13-class) |
| <code style="color:#228be6;font-weight:600">Contains</code> (lijst) | [6](#6-list) |
| <code style="color:#228be6;font-weight:600">Contains</code> (tekst) | [1](#1-tekst) |
| <code style="color:#228be6;font-weight:600">ContainsKey</code> | [7](#7-dictionary) |
| <code style="color:#228be6;font-weight:600">continue</code> | [4](#4-beslissen-en-herhalen) |
| <code style="color:#228be6;font-weight:600">Count</code> (eigenschap) | [6](#6-list) |
| <code style="color:#228be6;font-weight:600">Count</code> (LINQ) | [11](#11-linq) |
| <code style="color:#228be6;font-weight:600">CountAsync</code> | [32](#32-ef-core-opvragen-en-bewaren) |
| <code style="color:#228be6;font-weight:600">CreatedAtAction</code> | [26](#26-de-helpers-van-een-controller) |
| <code style="color:#228be6;font-weight:600">CultureInfo.InvariantCulture</code> | [2](#2-getallen-en-omzetten) |
| Dapper | [34](#34-dapper) |
| <code style="color:#228be6;font-weight:600">DateOnly</code> | [3](#3-datum-en-tijd) |
| <code style="color:#228be6;font-weight:600">DateTime</code> | [3](#3-datum-en-tijd) |
| <code style="color:#228be6;font-weight:600">DayOfWeek</code> | [3](#3-datum-en-tijd) |
| <code style="color:#228be6;font-weight:600">DbContext</code> | [31](#31-ef-core-model-en-database) |
| <code style="color:#228be6;font-weight:600">DbSet</code> | [31](#31-ef-core-model-en-database) |
| <code style="color:#228be6;font-weight:600">decimal</code> | [2](#2-getallen-en-omzetten) |
| <code style="color:#228be6;font-weight:600">default</code> | [21](#21-generics) |
| dependency injection | [27](#27-programcs-en-dependency-injection) |
| <code style="color:#228be6;font-weight:600">Dequeue</code> | [9](#9-queue) |
| <code style="color:#228be6;font-weight:600">Dictionary</code> | [7](#7-dictionary) |
| <code style="color:#228be6;font-weight:600">Distinct</code> | [11](#11-linq) |
| <code style="color:#228be6;font-weight:600">do</code> | [4](#4-beslissen-en-herhalen) |
| <code style="color:#228be6;font-weight:600">dotnet ef</code> | [31](#31-ef-core-model-en-database) |
| <code style="color:#228be6;font-weight:600">double.Parse</code> | [2](#2-getallen-en-omzetten) |
| DTO | [33](#33-dtos-en-mapster) |
| <code style="color:#228be6;font-weight:600">EndsWith</code> | [1](#1-tekst) |
| <code style="color:#228be6;font-weight:600">Enqueue</code> | [9](#9-queue) |
| <code style="color:#228be6;font-weight:600">EntityState</code> | [32](#32-ef-core-opvragen-en-bewaren) |
| <code style="color:#228be6;font-weight:600">Entry</code> | [32](#32-ef-core-opvragen-en-bewaren) |
| <code style="color:#228be6;font-weight:600">enum</code> | [18](#18-enum) |
| <code style="color:#228be6;font-weight:600">Enum.TryParse</code> | [18](#18-enum) |
| <code style="color:#228be6;font-weight:600">Equals</code> | [1](#1-tekst) |
| <code style="color:#228be6;font-weight:600">Exception</code> | [23](#23-uitzonderingen) |
| <code style="color:#228be6;font-weight:600">ExceptWith</code> | [8](#8-hashset) |
| <code style="color:#228be6;font-weight:600">ExecuteAsync</code> | [34](#34-dapper) |
| <code style="color:#228be6;font-weight:600">ExecuteScalarAsync</code> | [34](#34-dapper) |
| <code style="color:#228be6;font-weight:600">Exists</code> | [6](#6-list) |
| <code style="color:#228be6;font-weight:600">File.Exists</code> | [30](#30-json-en-bestanden) |
| <code style="color:#228be6;font-weight:600">File.ReadAllText</code> | [30](#30-json-en-bestanden) |
| <code style="color:#228be6;font-weight:600">File.WriteAllText</code> | [30](#30-json-en-bestanden) |
| <code style="color:#228be6;font-weight:600">finally</code> | [23](#23-uitzonderingen) |
| <code style="color:#228be6;font-weight:600">Find</code> | [6](#6-list) |
| <code style="color:#228be6;font-weight:600">FindAll</code> | [6](#6-list) |
| <code style="color:#228be6;font-weight:600">FindAsync</code> | [32](#32-ef-core-opvragen-en-bewaren) |
| <code style="color:#228be6;font-weight:600">FindIndex</code> | [6](#6-list) |
| <code style="color:#228be6;font-weight:600">First</code> | [11](#11-linq) |
| <code style="color:#228be6;font-weight:600">FirstOrDefault</code> | [11](#11-linq) |
| <code style="color:#228be6;font-weight:600">FirstOrDefaultAsync</code> | [32](#32-ef-core-opvragen-en-bewaren) |
| Fluent API | [31](#31-ef-core-model-en-database) |
| <code style="color:#228be6;font-weight:600">for</code> | [4](#4-beslissen-en-herhalen) |
| <code style="color:#228be6;font-weight:600">ForType</code> | [33](#33-dtos-en-mapster) |
| <code style="color:#228be6;font-weight:600">[FromBody]</code> | [25](#25-attributen-en-routes) |
| <code style="color:#228be6;font-weight:600">[FromQuery]</code> | [25](#25-attributen-en-routes) |
| generics | [21](#21-generics) |
| <code style="color:#228be6;font-weight:600">GetConnectionString</code> | [27](#27-programcs-en-dependency-injection) |
| <code style="color:#228be6;font-weight:600">GetType</code> | [22](#22-types-herkennen) |
| <code style="color:#228be6;font-weight:600">GetValueOrDefault</code> | [17](#17-omgaan-met-niets) |
| <code style="color:#228be6;font-weight:600">GroupBy</code> | [11](#11-linq) |
| <code style="color:#228be6;font-weight:600">HasData</code> | [31](#31-ef-core-model-en-database) |
| <code style="color:#228be6;font-weight:600">HasForeignKey</code> | [31](#31-ef-core-model-en-database) |
| <code style="color:#228be6;font-weight:600">HashSet</code> | [8](#8-hashset) |
| <code style="color:#228be6;font-weight:600">HasKey</code> | [31](#31-ef-core-model-en-database) |
| <code style="color:#228be6;font-weight:600">HasMaxLength</code> | [31](#31-ef-core-model-en-database) |
| <code style="color:#228be6;font-weight:600">HasOne</code> | [31](#31-ef-core-model-en-database) |
| <code style="color:#228be6;font-weight:600">HasPrecision</code> | [31](#31-ef-core-model-en-database) |
| <code style="color:#228be6;font-weight:600">HasValue</code> | [17](#17-omgaan-met-niets) |
| <code style="color:#228be6;font-weight:600">[HttpDelete]</code> | [25](#25-attributen-en-routes) |
| <code style="color:#228be6;font-weight:600">[HttpGet]</code> | [25](#25-attributen-en-routes) |
| <code style="color:#228be6;font-weight:600">[HttpPost]</code> | [25](#25-attributen-en-routes) |
| <code style="color:#228be6;font-weight:600">[HttpPut]</code> | [25](#25-attributen-en-routes) |
| <code style="color:#228be6;font-weight:600">IActionResult</code> | [26](#26-de-helpers-van-een-controller) |
| <code style="color:#228be6;font-weight:600">IEnumerable</code> | [12](#12-ienumerable) |
| <code style="color:#228be6;font-weight:600">if</code> | [4](#4-beslissen-en-herhalen) |
| <code style="color:#228be6;font-weight:600">Ignore</code> (Mapster) | [33](#33-dtos-en-mapster) |
| <code style="color:#228be6;font-weight:600">IgnoreCycles</code> | [32](#32-ef-core-opvragen-en-bewaren) |
| <code style="color:#228be6;font-weight:600">ILogger</code> | [28](#28-logging) |
| <code style="color:#228be6;font-weight:600">Include</code> | [32](#32-ef-core-opvragen-en-bewaren) |
| <code style="color:#228be6;font-weight:600">IndexOf</code> (lijst) | [6](#6-list) |
| <code style="color:#228be6;font-weight:600">IndexOf</code> (tekst) | [1](#1-tekst) |
| <code style="color:#228be6;font-weight:600">Insert</code> | [6](#6-list) |
| <code style="color:#228be6;font-weight:600">int.Parse</code> | [2](#2-getallen-en-omzetten) |
| <code style="color:#228be6;font-weight:600">int.TryParse</code> | [2](#2-getallen-en-omzetten) |
| <code style="color:#228be6;font-weight:600">interface</code> | [19](#19-interface) |
| <code style="color:#228be6;font-weight:600">IntersectWith</code> | [8](#8-hashset) |
| <code style="color:#228be6;font-weight:600">IQueryable</code> | [32](#32-ef-core-opvragen-en-bewaren) |
| <code style="color:#228be6;font-weight:600">IRegister</code> | [33](#33-dtos-en-mapster) |
| <code style="color:#228be6;font-weight:600">is</code> | [22](#22-types-herkennen) |
| <code style="color:#228be6;font-weight:600">IsNullOrEmpty</code> | [1](#1-tekst) |
| <code style="color:#228be6;font-weight:600">IsNullOrWhiteSpace</code> | [1](#1-tekst) |
| <code style="color:#228be6;font-weight:600">IsRequired</code> | [31](#31-ef-core-model-en-database) |
| <code style="color:#228be6;font-weight:600">JsonSerializer.Deserialize</code> | [30](#30-json-en-bestanden) |
| <code style="color:#228be6;font-weight:600">JsonSerializer.Serialize</code> | [30](#30-json-en-bestanden) |
| <code style="color:#228be6;font-weight:600">Keys</code> | [7](#7-dictionary) |
| <code style="color:#228be6;font-weight:600">KeyValuePair</code> | [7](#7-dictionary) |
| <code style="color:#228be6;font-weight:600">Last</code> | [11](#11-linq) |
| <code style="color:#228be6;font-weight:600">LastIndexOf</code> | [1](#1-tekst) |
| <code style="color:#228be6;font-weight:600">LastOrDefault</code> | [11](#11-linq) |
| lazy loading | [32](#32-ef-core-opvragen-en-bewaren) |
| <code style="color:#228be6;font-weight:600">Length</code> | [1](#1-tekst) |
| <code style="color:#228be6;font-weight:600">List</code> | [6](#6-list) |
| <code style="color:#228be6;font-weight:600">LogError</code> | [28](#28-logging) |
| <code style="color:#228be6;font-weight:600">LogInformation</code> | [28](#28-logging) |
| <code style="color:#228be6;font-weight:600">LogWarning</code> | [28](#28-logging) |
| <code style="color:#228be6;font-weight:600">Map</code> (Mapster) | [33](#33-dtos-en-mapster) |
| <code style="color:#228be6;font-weight:600">MapControllers</code> | [27](#27-programcs-en-dependency-injection) |
| Mapster | [33](#33-dtos-en-mapster) |
| <code style="color:#228be6;font-weight:600">Math.Abs</code> | [2](#2-getallen-en-omzetten) |
| <code style="color:#228be6;font-weight:600">Math.Ceiling</code> | [2](#2-getallen-en-omzetten) |
| <code style="color:#228be6;font-weight:600">Math.Floor</code> | [2](#2-getallen-en-omzetten) |
| <code style="color:#228be6;font-weight:600">Math.Max</code> | [2](#2-getallen-en-omzetten) |
| <code style="color:#228be6;font-weight:600">Math.Min</code> | [2](#2-getallen-en-omzetten) |
| <code style="color:#228be6;font-weight:600">Math.Pow</code> | [2](#2-getallen-en-omzetten) |
| <code style="color:#228be6;font-weight:600">Math.Round</code> | [2](#2-getallen-en-omzetten) |
| <code style="color:#228be6;font-weight:600">Max</code> | [11](#11-linq) |
| <code style="color:#228be6;font-weight:600">MaxBy</code> | [11](#11-linq) |
| <code style="color:#228be6;font-weight:600">Message</code> | [23](#23-uitzonderingen) |
| micro-ORM | [34](#34-dapper) |
| <code style="color:#228be6;font-weight:600">MidpointRounding</code> | [2](#2-getallen-en-omzetten) |
| migratie | [31](#31-ef-core-model-en-database) |
| <code style="color:#228be6;font-weight:600">Min</code> | [11](#11-linq) |
| <code style="color:#228be6;font-weight:600">MinBy</code> | [11](#11-linq) |
| N+1-probleem | [32](#32-ef-core-opvragen-en-bewaren) |
| <code style="color:#228be6;font-weight:600">nameof</code> | [22](#22-types-herkennen) |
| <code style="color:#228be6;font-weight:600">namespace</code> | [13](#13-class) |
| navigatie-eigenschap | [31](#31-ef-core-model-en-database) |
| <code style="color:#228be6;font-weight:600">new()</code> (beperking) | [21](#21-generics) |
| <code style="color:#228be6;font-weight:600">NewConfig</code> | [33](#33-dtos-en-mapster) |
| <code style="color:#228be6;font-weight:600">NoContent</code> | [26](#26-de-helpers-van-een-controller) |
| <code style="color:#228be6;font-weight:600">NotFound</code> | [26](#26-de-helpers-van-een-controller) |
| <code style="color:#228be6;font-weight:600">Ok</code> | [26](#26-de-helpers-van-een-controller) |
| <code style="color:#228be6;font-weight:600">OnDelete</code> | [31](#31-ef-core-model-en-database) |
| <code style="color:#228be6;font-weight:600">OnModelCreating</code> | [31](#31-ef-core-model-en-database) |
| <code style="color:#228be6;font-weight:600">OrderBy</code> | [11](#11-linq) |
| <code style="color:#228be6;font-weight:600">OrderByDescending</code> | [11](#11-linq) |
| overerving | [20](#20-overerving-en-abstracte-klassen) |
| <code style="color:#228be6;font-weight:600">override</code> | [20](#20-overerving-en-abstracte-klassen) |
| <code style="color:#228be6;font-weight:600">PadLeft</code> | [1](#1-tekst) |
| <code style="color:#228be6;font-weight:600">PadRight</code> | [1](#1-tekst) |
| <code style="color:#228be6;font-weight:600">ParseExact</code> | [3](#3-datum-en-tijd) |
| <code style="color:#228be6;font-weight:600">Path.Combine</code> | [30](#30-json-en-bestanden) |
| <code style="color:#228be6;font-weight:600">Peek</code> | [9](#9-queue), [10](#10-stack) |
| platslaan | [33](#33-dtos-en-mapster) |
| <code style="color:#228be6;font-weight:600">Pop</code> | [10](#10-stack) |
| primaire sleutel | [31](#31-ef-core-model-en-database) |
| <code style="color:#228be6;font-weight:600">protected</code> | [20](#20-overerving-en-abstracte-klassen) |
| <code style="color:#228be6;font-weight:600">Push</code> | [10](#10-stack) |
| <code style="color:#228be6;font-weight:600">QueryAsync</code> | [34](#34-dapper) |
| <code style="color:#228be6;font-weight:600">QueryFirstOrDefaultAsync</code> | [34](#34-dapper) |
| <code style="color:#228be6;font-weight:600">Queue</code> | [9](#9-queue) |
| <code style="color:#228be6;font-weight:600">readonly</code> | [24](#24-static-const-en-readonly) |
| <code style="color:#228be6;font-weight:600">record</code> | [14](#14-record) |
| referentietype | [16](#16-referentie-tegenover-waarde) |
| <code style="color:#228be6;font-weight:600">Remove</code> (database) | [32](#32-ef-core-opvragen-en-bewaren) |
| <code style="color:#228be6;font-weight:600">Remove</code> (dictionary) | [7](#7-dictionary) |
| <code style="color:#228be6;font-weight:600">Remove</code> (lijst) | [6](#6-list) |
| <code style="color:#228be6;font-weight:600">Remove-Migration</code> | [31](#31-ef-core-model-en-database) |
| <code style="color:#228be6;font-weight:600">RemoveAll</code> | [6](#6-list) |
| <code style="color:#228be6;font-weight:600">RemoveAt</code> | [6](#6-list) |
| <code style="color:#228be6;font-weight:600">Replace</code> | [1](#1-tekst) |
| <code style="color:#228be6;font-weight:600">Restrict</code> | [31](#31-ef-core-model-en-database) |
| <code style="color:#228be6;font-weight:600">.Result</code> | [29](#29-asynchroon-werken) |
| <code style="color:#228be6;font-weight:600">Reverse</code> | [6](#6-list) |
| <code style="color:#228be6;font-weight:600">[Route]</code> | [25](#25-attributen-en-routes) |
| routeparameter | [25](#25-attributen-en-routes) |
| <code style="color:#228be6;font-weight:600">SaveChangesAsync</code> | [32](#32-ef-core-opvragen-en-bewaren) |
| <code style="color:#228be6;font-weight:600">Scan</code> | [33](#33-dtos-en-mapster) |
| <code style="color:#228be6;font-weight:600">sealed</code> | [20](#20-overerving-en-abstracte-klassen) |
| <code style="color:#228be6;font-weight:600">Select</code> | [11](#11-linq) |
| <code style="color:#228be6;font-weight:600">SelectMany</code> | [11](#11-linq) |
| <code style="color:#228be6;font-weight:600">Set&lt;T&gt;</code> | [32](#32-ef-core-opvragen-en-bewaren) |
| <code style="color:#228be6;font-weight:600">Single</code> | [11](#11-linq) |
| <code style="color:#228be6;font-weight:600">SingleOrDefault</code> | [11](#11-linq) |
| <code style="color:#228be6;font-weight:600">Skip</code> | [11](#11-linq) |
| <code style="color:#228be6;font-weight:600">Sort</code> | [6](#6-list) |
| <code style="color:#228be6;font-weight:600">Split</code> | [1](#1-tekst) |
| <code style="color:#228be6;font-weight:600">splitOn</code> | [34](#34-dapper) |
| SQL-injectie | [34](#34-dapper) |
| <code style="color:#228be6;font-weight:600">Stack</code> | [10](#10-stack) |
| <code style="color:#228be6;font-weight:600">StartsWith</code> | [1](#1-tekst) |
| <code style="color:#228be6;font-weight:600">static</code> | [24](#24-static-const-en-readonly) |
| <code style="color:#228be6;font-weight:600">StatusCode</code> | [26](#26-de-helpers-van-een-controller) |
| <code style="color:#228be6;font-weight:600">string.Format</code> | [1](#1-tekst) |
| <code style="color:#228be6;font-weight:600">string.Join</code> | [1](#1-tekst) |
| <code style="color:#228be6;font-weight:600">struct</code> | [15](#15-struct) |
| <code style="color:#228be6;font-weight:600">Substring</code> | [1](#1-tekst) |
| <code style="color:#228be6;font-weight:600">Sum</code> | [11](#11-linq) |
| <code style="color:#228be6;font-weight:600">switch</code> | [4](#4-beslissen-en-herhalen) |
| <code style="color:#228be6;font-weight:600">Take</code> | [11](#11-linq) |
| <code style="color:#228be6;font-weight:600">Task.Delay</code> | [29](#29-asynchroon-werken) |
| <code style="color:#228be6;font-weight:600">ThenBy</code> | [11](#11-linq) |
| <code style="color:#228be6;font-weight:600">ThenInclude</code> | [32](#32-ef-core-opvragen-en-bewaren) |
| <code style="color:#228be6;font-weight:600">throw</code> | [23](#23-uitzonderingen) |
| <code style="color:#228be6;font-weight:600">TimeSpan</code> | [3](#3-datum-en-tijd) |
| <code style="color:#228be6;font-weight:600">ToArray</code> | [6](#6-list) |
| <code style="color:#228be6;font-weight:600">ToDictionary</code> | [11](#11-linq) |
| <code style="color:#228be6;font-weight:600">ToList</code> | [11](#11-linq) |
| <code style="color:#228be6;font-weight:600">ToListAsync</code> | [32](#32-ef-core-opvragen-en-bewaren) |
| <code style="color:#228be6;font-weight:600">ToLower</code> | [1](#1-tekst) |
| <code style="color:#228be6;font-weight:600">ToTable</code> | [31](#31-ef-core-model-en-database) |
| <code style="color:#228be6;font-weight:600">ToUpper</code> | [1](#1-tekst) |
| <code style="color:#228be6;font-weight:600">Trim</code> | [1](#1-tekst) |
| <code style="color:#228be6;font-weight:600">TrimEnd</code> | [1](#1-tekst) |
| <code style="color:#228be6;font-weight:600">TrimStart</code> | [1](#1-tekst) |
| <code style="color:#228be6;font-weight:600">try</code> | [23](#23-uitzonderingen) |
| <code style="color:#228be6;font-weight:600">TryAdd</code> | [7](#7-dictionary) |
| <code style="color:#228be6;font-weight:600">TryDequeue</code> | [9](#9-queue) |
| <code style="color:#228be6;font-weight:600">TryGetValue</code> | [7](#7-dictionary) |
| <code style="color:#228be6;font-weight:600">typeof</code> | [22](#22-types-herkennen) |
| uitzondering | [23](#23-uitzonderingen) |
| <code style="color:#228be6;font-weight:600">UnionWith</code> | [8](#8-hashset) |
| Unit of Work | [32](#32-ef-core-opvragen-en-bewaren) |
| <code style="color:#228be6;font-weight:600">Update</code> (database) | [32](#32-ef-core-opvragen-en-bewaren) |
| <code style="color:#228be6;font-weight:600">Update-Database</code> | [31](#31-ef-core-model-en-database) |
| <code style="color:#228be6;font-weight:600">UseNpgsql</code> | [31](#31-ef-core-model-en-database) |
| <code style="color:#228be6;font-weight:600">using</code> | [13](#13-class) |
| <code style="color:#228be6;font-weight:600">Values</code> | [7](#7-dictionary) |
| <code style="color:#228be6;font-weight:600">virtual</code> | [20](#20-overerving-en-abstracte-klassen) |
| vreemde sleutel | [31](#31-ef-core-model-en-database) |
| waardetype | [16](#16-referentie-tegenover-waarde) |
| <code style="color:#228be6;font-weight:600">Where</code> | [11](#11-linq) |
| <code style="color:#228be6;font-weight:600">where</code> (beperking) | [21](#21-generics) |
| <code style="color:#228be6;font-weight:600">while</code> | [4](#4-beslissen-en-herhalen) |
| <code style="color:#228be6;font-weight:600">with</code> | [14](#14-record) |
| <code style="color:#228be6;font-weight:600">WithMany</code> | [31](#31-ef-core-model-en-database) |
| <code style="color:#228be6;font-weight:600">WithOne</code> | [31](#31-ef-core-model-en-database) |
| <code style="color:#228be6;font-weight:600">%</code> | [2](#2-getallen-en-omzetten) |
| <code style="color:#228be6;font-weight:600">?.</code> | [17](#17-omgaan-met-niets) |
| <code style="color:#228be6;font-weight:600">??</code> en <code style="color:#228be6;font-weight:600">??=</code> | [17](#17-omgaan-met-niets) |
| <code style="color:#228be6;font-weight:600">&amp;&amp;</code> en <code style="color:#228be6;font-weight:600">&#124;&#124;</code> | [4](#4-beslissen-en-herhalen) |
| <code style="color:#228be6;font-weight:600">? :</code> | [4](#4-beslissen-en-herhalen) |
| <code style="color:#228be6;font-weight:600">=&gt;</code> | [13](#13-class) |
| <code style="color:#228be6;font-weight:600">!</code> en <code style="color:#228be6;font-weight:600">= default!</code> | [17](#17-omgaan-met-niets) |
| <code style="color:#228be6;font-weight:600">"""</code> | [34](#34-dapper) |
