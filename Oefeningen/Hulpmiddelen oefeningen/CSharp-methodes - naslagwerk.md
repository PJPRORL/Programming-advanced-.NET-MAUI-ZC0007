# C#-methodes — naslagwerk

<b>Programming Advanced</b> &nbsp;·&nbsp; wat je in dit traject tegenkomt &nbsp;·&nbsp; geordend op wat je wil doen

## Hoe je dit gebruikt

Dit is geen woordenboek op alfabet. Dat heb je al, op Microsoft Learn. Dit is
geordend op de vraag die je jezelf stelt terwijl je zit te typen: *ik wil één
item uit die lijst halen* — en dan staat er welke methodes daarvoor bestaan en
welke je in welk geval neemt.

Kom je een naam tegen en wil je weten wat hij doet, gebruik dan de index
onderaan.

In de tabellen staat de <code style="color:#228be6;font-weight:600">methode</code> in het blauw en het
<code style="color:#845ef7">resultaat</code> in het paars. Wat daartussen staat is
gewone code, en blijft neutraal.

Per methode staat er een voorbeeld en het resultaat. De voorbeelden zijn losse
regels, geen stukjes uit je project. Waar een keuze te maken valt, staat er een
alinea bij die zegt wanneer je welke neemt — dat is het deel dat je in de
documentatie niet vindt.

> [!NOTE]
> De hoofdletters doen ertoe. In C# is het `Find`, niet `find`, en `typeof`,
> niet `TypeOf`. Methodes beginnen met een hoofdletter, sleutelwoorden van de
> taal met een kleine letter. Kleine letters vooraan zijn meestal JavaScript.

---

# 1. Tekst

## Hoe lang is het, is het leeg?

| Methode | Wat het doet | Voorbeeld | Resultaat |
|---|---|---|---|
| <code style="color:#228be6;font-weight:600">Length</code> | aantal tekens; een eigenschap, geen methode, dus zonder haakjes | `"moederbord".Length` | <code style="color:#845ef7">10</code> |
| <code style="color:#228be6;font-weight:600">string.IsNullOrEmpty</code> | is het null of een lege tekst? | `string.IsNullOrEmpty("")` | <code style="color:#845ef7">true</code> |
| <code style="color:#228be6;font-weight:600">string.IsNullOrWhiteSpace</code> | is het null, leeg, of enkel spaties? | `string.IsNullOrWhiteSpace("   ")` | <code style="color:#845ef7">true</code> |

Voor invoer die van een gebruiker komt neem je bijna altijd
`IsNullOrWhiteSpace`. Iemand die een spatie intikt heeft niets ingevuld, maar
`IsNullOrEmpty` vindt dat wel een waarde.

## Hoofdletters, kleine letters, spaties weg

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

## Een stuk uit een tekst halen

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

## Zit er iets in, en waar?

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

## Tekst vergelijken

| Methode | Wat het doet | Voorbeeld | Resultaat |
|---|---|---|---|
| <code style="color:#228be6;font-weight:600">==</code> | letterlijk gelijk, hoofdlettergevoelig | `"AM5" == "am5"` | <code style="color:#845ef7">false</code> |
| <code style="color:#228be6;font-weight:600">Equals</code> | idem, maar je kan de regels meegeven | `"AM5".Equals("am5", StringComparison.OrdinalIgnoreCase)` | <code style="color:#845ef7">true</code> |
| <code style="color:#228be6;font-weight:600">CompareTo</code> | welke komt eerst in de sortering | `"b".CompareTo("a")` | <code style="color:#845ef7">1</code> |

Wil je twee sockets vergelijken die de ene keer met hoofdletters en de andere
keer zonder binnenkomen, dan is `Equals` met `OrdinalIgnoreCase` het nette
antwoord. `ToUpper()` op allebei werkt ook, maar maakt twee nieuwe teksten om
er één vergelijking mee te doen.

## Tekst opbouwen met waarden erin

| Vorm | Wat het doet | Voorbeeld | Resultaat |
|---|---|---|---|
| <code style="color:#228be6;font-weight:600">&#36;"..."</code> | zet waarden rechtstreeks in de tekst | <code>&#36;"Socket {socket} past"</code> | <code style="color:#845ef7">"Socket AM5 past"</code> |
| <code style="color:#228be6;font-weight:600">+</code> | plakken | `"Socket " + socket` | <code style="color:#845ef7">"Socket AM5"</code> |
| <code style="color:#228be6;font-weight:600">string.Format</code> | oudere vorm met plaatshouders | `string.Format("Socket {0}", socket)` | <code style="color:#845ef7">"Socket AM5"</code> |

De <code>&#36;"..."</code>-vorm is wat je overal zal tegenkomen. Je mag er ook berekeningen in
zetten: <code>&#36;"Nog {sloten - modules} vrij"</code>.

---

# 2. Getallen en omzetten

## Tekst omzetten naar een getal

| Methode | Wat het doet | Voorbeeld | Resultaat |
|---|---|---|---|
| <code style="color:#228be6;font-weight:600">int.Parse</code> | zet om, of loopt stuk | `int.Parse("120")` | <code style="color:#845ef7">120</code> |
| <code style="color:#228be6;font-weight:600">int.Parse</code> | bij onzin | `int.Parse("abc")` | uitzondering |
| <code style="color:#228be6;font-weight:600">int.TryParse</code> | probeert om te zetten, zegt of het lukte | `int.TryParse("120", out int n)` | <code style="color:#845ef7">true</code>, <code style="color:#845ef7">n</code> is <code style="color:#845ef7">120</code> |
| <code style="color:#228be6;font-weight:600">int.TryParse</code> | bij onzin | `int.TryParse("abc", out int n)` | <code style="color:#845ef7">false</code>, <code style="color:#845ef7">n</code> is <code style="color:#845ef7">0</code> |
| <code style="color:#228be6;font-weight:600">double.Parse</code> | hetzelfde voor kommagetallen | `double.Parse("2.5")` | <code style="color:#845ef7">2.5</code> |

> [!IMPORTANT]
> De regel is eenvoudig: komt de tekst van een gebruiker, gebruik `TryParse`.
> Komt ze uit je eigen code en weet je zeker dat het een getal is, dan mag
> `Parse`. Een `Parse` op gebruikersinvoer is een crash die op je zit te wachten.

## Rekenen

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

---

# 3. Lijsten

Dit zijn de methodes die op een `List<T>` zelf zitten. Ze werken enkel op een
lijst, niet op elke verzameling.

## Toevoegen en verwijderen

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

## Erin zoeken

| Methode | Wat het doet | Voorbeeld | Resultaat |
|---|---|---|---|
| <code style="color:#228be6;font-weight:600">Count</code> | hoeveel zitten erin; een eigenschap | `fans.Count` | <code style="color:#845ef7">4</code> |
| <code style="color:#228be6;font-weight:600">Contains</code> | zit dit exacte item erin | `fans.Contains(fan)` | <code style="color:#845ef7">true</code> |
| <code style="color:#228be6;font-weight:600">IndexOf</code> | op welke positie staat het | `fans.IndexOf(fan)` | <code style="color:#845ef7">2</code>, of <code style="color:#845ef7">-1</code> |
| <code style="color:#228be6;font-weight:600">Exists</code> | is er minstens één die voldoet | `fans.Exists(f => f.Prijs > 50)` | <code style="color:#845ef7">true</code> |
| <code style="color:#228be6;font-weight:600">Find</code> | geef de eerste die voldoet | `fans.Find(f => f.Id == 3)` | het item, of <code style="color:#845ef7">null</code> |
| <code style="color:#228be6;font-weight:600">FindAll</code> | geef allemaal die voldoen | `fans.FindAll(f => f.Merk == "NZXT")` | een nieuwe lijst |
| <code style="color:#228be6;font-weight:600">FindIndex</code> | op welke positie staat de eerste die voldoet | `fans.FindIndex(f => f.Id == 3)` | <code style="color:#845ef7">2</code>, of <code style="color:#845ef7">-1</code> |

## Ordenen

| Methode | Wat het doet | Voorbeeld | Resultaat |
|---|---|---|---|
| <code style="color:#228be6;font-weight:600">Sort</code> | sorteer de lijst zelf | `getallen.Sort()` | de lijst staat op volgorde |
| <code style="color:#228be6;font-weight:600">Reverse</code> | keer de lijst om | `getallen.Reverse()` | omgekeerde volgorde |
| <code style="color:#228be6;font-weight:600">ToArray</code> | maak er een array van | `fans.ToArray()` | een array |

> [!IMPORTANT]
> `Sort` en `Reverse` veranderen je lijst zelf en geven niets terug. De
> LINQ-methodes uit het volgende hoofdstuk doen het omgekeerde: die laten je
> lijst met rust en geven een nieuw resultaat. Dat verschil verklaart waarom
> `lijst.Sort()` werkt en `var x = lijst.Sort()` niet compileert.

---

# 4. Zoeken en rekenen in een verzameling

Dit is LINQ. Deze methodes werken op elke verzameling — een lijst, een array,
een resultaat van een andere LINQ-methode, en later ook op een databasetabel.
Ze veranderen nooit iets aan de bron; ze geven een nieuw resultaat.

Bijna allemaal willen ze een voorwaarde in de vorm `f => f.Prijs > 50`. Lees de
pijl als "geef mij, voor elke f, het antwoord op".

## Ik wil één item

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

## Ik wil weten of iets bestaat

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

## Ik wil een deel van de verzameling

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

## Ik wil tellen of rekenen

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

## Ik wil ordenen of groeperen

| Methode | Wat het doet | Voorbeeld | Resultaat |
|---|---|---|---|
| <code style="color:#228be6;font-weight:600">OrderBy</code> | sorteer oplopend | `fans.OrderBy(f => f.Prijs)` | goedkoopste eerst |
| <code style="color:#228be6;font-weight:600">OrderByDescending</code> | sorteer aflopend | `fans.OrderByDescending(f => f.Prijs)` | duurste eerst |
| <code style="color:#228be6;font-weight:600">ThenBy</code> | tweede sorteersleutel | `fans.OrderBy(f => f.Merk).ThenBy(f => f.Prijs)` | per merk, dan op prijs |
| <code style="color:#228be6;font-weight:600">GroupBy</code> | maak groepen | `fans.GroupBy(f => f.Merk)` | een groep per merk |

## Ik wil het resultaat vastleggen

| Methode | Wat het doet | Voorbeeld | Resultaat |
|---|---|---|---|
| <code style="color:#228be6;font-weight:600">ToList</code> | maak er een echte lijst van | `fans.Where(f => f.Prijs > 50).ToList()` | een <code style="color:#845ef7">List</code> |
| <code style="color:#228be6;font-weight:600">ToArray</code> | maak er een array van | `fans.ToArray()` | een array |
| <code style="color:#228be6;font-weight:600">ToDictionary</code> | maak er een opzoektabel van | `fans.ToDictionary(f => f.Id)` | een <code style="color:#845ef7">Dictionary</code> |

> [!IMPORTANT]
> Dit is het lastigste stuk van LINQ. `Where` voert niets uit op het moment dat
> je het schrijft. Het onthoudt enkel de vraag, en stelt ze pas zodra je het
> resultaat doorloopt. Verandert je lijst intussen, dan verandert je antwoord
> mee.
>
> `ToList()` stelt de vraag meteen en legt het antwoord vast. Doe dat zodra je
> het resultaat meer dan één keer gebruikt, of zodra je de bron erna nog
> aanpast. Bij een API is de vuistregel: alles wat je teruggeeft aan de client,
> eerst `ToList()`.

---

# 5. Opzoektabellen

Een `Dictionary` is een lijst waarin je niet op positie zoekt maar op een
sleutel. Zodra je in een lijst zit te zoeken met `Find` op hetzelfde veld, is
een `Dictionary` waarschijnlijk wat je wil.

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

---

# 6. Omgaan met niets

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

---

# 7. Types herkennen

| Vorm | Wat het doet | Voorbeeld | Resultaat |
|---|---|---|---|
| <code style="color:#228be6;font-weight:600">typeof</code> | het type zelf, als je de naam kent | `typeof(Ventilator)` | het type |
| <code style="color:#228be6;font-weight:600">GetType</code> | het type van een bestaand object | `fan.GetType()` | het type |
| <code style="color:#228be6;font-weight:600">is</code> | is dit object van dat type | `if (koeler is Waterkoeling)` | <code style="color:#845ef7">true</code> of <code style="color:#845ef7">false</code> |
| <code style="color:#228be6;font-weight:600">is</code> met naam | idem, en geef het meteen die naam | `if (koeler is Waterkoeling w)` | <code style="color:#845ef7">w</code> is bruikbaar |
| <code style="color:#228be6;font-weight:600">as</code> | behandel als dat type, of `null` | `koeler as Waterkoeling` | het object of <code style="color:#845ef7">null</code> |
| <code style="color:#228be6;font-weight:600">nameof</code> | de naam van iets als tekst | `nameof(GetFanById)` | <code style="color:#845ef7">"GetFanById"</code> |

`nameof` lijkt onnozel tot je het nodig hebt. Het voordeel tegenover `"GetFanById"`
tussen aanhalingstekens is dat je code niet meer compileert wanneer je die
methode hernoemt. Een typfout in een naam vind je zo bij het bouwen in plaats
van bij het testen.

---

# 8. De helpers van een controller

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

---

# 9. Asynchroon werken

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

---

# 10. JSON en bestanden

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

---

# Veelgemaakte verwarringen

Twaalf paren die op elkaar lijken en iets anders doen. Dit is het hoofdstuk waar
je het vaakst zal terugkomen.

## `Find` tegenover `FirstOrDefault`

Op een lijst doen ze hetzelfde. `Find` bestaat enkel op `List` en op een array;
`FirstOrDefault` werkt op elke verzameling, ook op een databasequery. Omdat je
later met databases gaat werken en `Find` daar niet bestaat, went `FirstOrDefault`
beter aan.

## `First` tegenover `Single`

`First` zegt: er is er minstens één, geef mij de eerste. `Single` zegt: er is er
precies één, en als dat niet klopt is er iets grondig mis. Gebruik `Single` op
een Id, en `First` op een sortering.

## `Parse` tegenover `TryParse`

`Parse` gelooft je en crasht als je ongelijk hebt. `TryParse` vraagt het na en
geeft `false`. Alles wat van buiten komt gaat door `TryParse`.

## `Count` als eigenschap tegenover `Count()` als methode

`lijst.Count` zonder haakjes is een eigenschap van `List` en is meteen bekend.
`lijst.Count()` met haakjes is LINQ en loopt de verzameling af. Op een lijst
neem je de eigenschap.

## `Remove`, `RemoveAt` en `RemoveAll`

`Remove` wil het item, `RemoveAt` wil een positie, `RemoveAll` wil een
voorwaarde. Ze geven ook iets anders terug: `Remove` een ja of nee, `RemoveAll`
het aantal dat weg is, `RemoveAt` niets.

## `==` tegenover `Equals` bij tekst

Allebei vergelijken ze letterlijk, hoofdletters inbegrepen. Het verschil is dat
`Equals` een tweede argument aanvaardt waarmee je die regel kan versoepelen.
Zodra hoofdletters er niet toe mogen doen, heb je `Equals` nodig.

## `Where` tegenover `Select`

`Where` houdt items over, `Select` maakt van elk item iets anders. Krijg je een
lijst terug waar je een lijst verwachtte, dan zocht je waarschijnlijk `Select`.
Krijg je evenveel items terug als je erin stak, dan had je `Where` moeten nemen.

## `Max` tegenover `MaxBy`

`Max` geeft de waarde, `MaxBy` geeft het item dat die waarde draagt. Dezelfde
verhouding geldt voor `Min` en `MinBy`.

## `Any()` tegenover `Count() > 0`

Hetzelfde antwoord, maar `Any` stopt bij het eerste item dat voldoet. Op een
lijst van vijf maakt dat niets uit; op een databasetabel van vijftigduizend wel.

## Uitgestelde uitvoering

`Where` voert niets uit tot je het resultaat doorloopt. Verandert je lijst
intussen, dan verandert je antwoord. `ToList()` legt het antwoord vast op het
moment dat je het vraagt.

## Wat een methode teruggeeft, en wat ze verandert

`lijst.Sort()` verandert je lijst en geeft niets terug. `lijst.OrderBy(...)`
laat je lijst met rust en geeft een nieuw resultaat. Hetzelfde verschil bij
`Reverse` op een lijst tegenover `Reverse` in LINQ. Compileert je regel niet,
dan heb je meestal de verkeerde van de twee te pakken.

## `-1` tegenover `null`

`IndexOf` geeft `-1` wanneer er niets gevonden is. `FirstOrDefault` geeft
`null`, of `0` bij getallen. Drie verschillende manieren om "niets" te zeggen,
en elk vraagt een andere controle.

---

# Index

Alfabetisch, met het hoofdstuk waar de uitleg staat.

| Naam | Hoofdstuk |
|---|---|
| <code style="color:#228be6;font-weight:600">Add</code> (lijst) | 3 |
| <code style="color:#228be6;font-weight:600">Add</code> (dictionary) | 5 |
| <code style="color:#228be6;font-weight:600">AddRange</code> | 3 |
| <code style="color:#228be6;font-weight:600">All</code> | 4 |
| <code style="color:#228be6;font-weight:600">Any</code> | 4 |
| <code style="color:#228be6;font-weight:600">as</code> | 7 |
| <code style="color:#228be6;font-weight:600">async</code> | 9 |
| <code style="color:#228be6;font-weight:600">Average</code> | 4 |
| <code style="color:#228be6;font-weight:600">await</code> | 9 |
| <code style="color:#228be6;font-weight:600">BadRequest</code> | 8 |
| <code style="color:#228be6;font-weight:600">Clear</code> | 3 |
| <code style="color:#228be6;font-weight:600">CompareTo</code> | 1 |
| <code style="color:#228be6;font-weight:600">Contains</code> (tekst) | 1 |
| <code style="color:#228be6;font-weight:600">Contains</code> (lijst) | 3 |
| <code style="color:#228be6;font-weight:600">ContainsKey</code> | 5 |
| <code style="color:#228be6;font-weight:600">Count</code> (eigenschap) | 3 |
| <code style="color:#228be6;font-weight:600">Count</code> (LINQ) | 4 |
| <code style="color:#228be6;font-weight:600">CreatedAtAction</code> | 8 |
| <code style="color:#228be6;font-weight:600">Distinct</code> | 4 |
| <code style="color:#228be6;font-weight:600">double.Parse</code> | 2 |
| <code style="color:#228be6;font-weight:600">EndsWith</code> | 1 |
| <code style="color:#228be6;font-weight:600">Equals</code> | 1 |
| <code style="color:#228be6;font-weight:600">Exists</code> | 3 |
| <code style="color:#228be6;font-weight:600">File.Exists</code> | 10 |
| <code style="color:#228be6;font-weight:600">File.ReadAllText</code> | 10 |
| <code style="color:#228be6;font-weight:600">File.WriteAllText</code> | 10 |
| <code style="color:#228be6;font-weight:600">Find</code> | 3 |
| <code style="color:#228be6;font-weight:600">FindAll</code> | 3 |
| <code style="color:#228be6;font-weight:600">FindIndex</code> | 3 |
| <code style="color:#228be6;font-weight:600">First</code> | 4 |
| <code style="color:#228be6;font-weight:600">FirstOrDefault</code> | 4 |
| <code style="color:#228be6;font-weight:600">GetType</code> | 7 |
| <code style="color:#228be6;font-weight:600">GetValueOrDefault</code> | 6 |
| <code style="color:#228be6;font-weight:600">GroupBy</code> | 4 |
| <code style="color:#228be6;font-weight:600">HasValue</code> | 6 |
| <code style="color:#228be6;font-weight:600">IndexOf</code> (tekst) | 1 |
| <code style="color:#228be6;font-weight:600">IndexOf</code> (lijst) | 3 |
| <code style="color:#228be6;font-weight:600">Insert</code> | 3 |
| <code style="color:#228be6;font-weight:600">int.Parse</code> | 2 |
| <code style="color:#228be6;font-weight:600">int.TryParse</code> | 2 |
| <code style="color:#228be6;font-weight:600">is</code> | 7 |
| <code style="color:#228be6;font-weight:600">IsNullOrEmpty</code> | 1 |
| <code style="color:#228be6;font-weight:600">IsNullOrWhiteSpace</code> | 1 |
| <code style="color:#228be6;font-weight:600">JsonSerializer.Deserialize</code> | 10 |
| <code style="color:#228be6;font-weight:600">JsonSerializer.Serialize</code> | 10 |
| <code style="color:#228be6;font-weight:600">Keys</code> | 5 |
| <code style="color:#228be6;font-weight:600">Last</code> | 4 |
| <code style="color:#228be6;font-weight:600">LastIndexOf</code> | 1 |
| <code style="color:#228be6;font-weight:600">LastOrDefault</code> | 4 |
| <code style="color:#228be6;font-weight:600">Length</code> | 1 |
| <code style="color:#228be6;font-weight:600">Math.Abs</code> | 2 |
| <code style="color:#228be6;font-weight:600">Math.Ceiling</code> | 2 |
| <code style="color:#228be6;font-weight:600">Math.Floor</code> | 2 |
| <code style="color:#228be6;font-weight:600">Math.Max</code> | 2 |
| <code style="color:#228be6;font-weight:600">Math.Min</code> | 2 |
| <code style="color:#228be6;font-weight:600">Math.Pow</code> | 2 |
| <code style="color:#228be6;font-weight:600">Math.Round</code> | 2 |
| <code style="color:#228be6;font-weight:600">Max</code> | 4 |
| <code style="color:#228be6;font-weight:600">MaxBy</code> | 4 |
| <code style="color:#228be6;font-weight:600">Min</code> | 4 |
| <code style="color:#228be6;font-weight:600">MinBy</code> | 4 |
| <code style="color:#228be6;font-weight:600">nameof</code> | 7 |
| <code style="color:#228be6;font-weight:600">NoContent</code> | 8 |
| <code style="color:#228be6;font-weight:600">NotFound</code> | 8 |
| <code style="color:#228be6;font-weight:600">Ok</code> | 8 |
| <code style="color:#228be6;font-weight:600">OrderBy</code> | 4 |
| <code style="color:#228be6;font-weight:600">OrderByDescending</code> | 4 |
| <code style="color:#228be6;font-weight:600">PadLeft</code> | 1 |
| <code style="color:#228be6;font-weight:600">PadRight</code> | 1 |
| <code style="color:#228be6;font-weight:600">Path.Combine</code> | 10 |
| <code style="color:#228be6;font-weight:600">Remove</code> (lijst) | 3 |
| <code style="color:#228be6;font-weight:600">Remove</code> (dictionary) | 5 |
| <code style="color:#228be6;font-weight:600">RemoveAll</code> | 3 |
| <code style="color:#228be6;font-weight:600">RemoveAt</code> | 3 |
| <code style="color:#228be6;font-weight:600">Replace</code> | 1 |
| <code style="color:#228be6;font-weight:600">Reverse</code> | 3 |
| <code style="color:#228be6;font-weight:600">Select</code> | 4 |
| <code style="color:#228be6;font-weight:600">SelectMany</code> | 4 |
| <code style="color:#228be6;font-weight:600">Single</code> | 4 |
| <code style="color:#228be6;font-weight:600">SingleOrDefault</code> | 4 |
| <code style="color:#228be6;font-weight:600">Skip</code> | 4 |
| <code style="color:#228be6;font-weight:600">Sort</code> | 3 |
| <code style="color:#228be6;font-weight:600">Split</code> | 1 |
| <code style="color:#228be6;font-weight:600">StartsWith</code> | 1 |
| <code style="color:#228be6;font-weight:600">StatusCode</code> | 8 |
| <code style="color:#228be6;font-weight:600">string.Format</code> | 1 |
| <code style="color:#228be6;font-weight:600">string.Join</code> | 1 |
| <code style="color:#228be6;font-weight:600">Substring</code> | 1 |
| <code style="color:#228be6;font-weight:600">Sum</code> | 4 |
| <code style="color:#228be6;font-weight:600">Take</code> | 4 |
| <code style="color:#228be6;font-weight:600">ThenBy</code> | 4 |
| <code style="color:#228be6;font-weight:600">ToArray</code> | 3 |
| <code style="color:#228be6;font-weight:600">ToDictionary</code> | 4 |
| <code style="color:#228be6;font-weight:600">ToList</code> | 4 |
| <code style="color:#228be6;font-weight:600">ToLower</code> | 1 |
| <code style="color:#228be6;font-weight:600">ToUpper</code> | 1 |
| <code style="color:#228be6;font-weight:600">Trim</code> | 1 |
| <code style="color:#228be6;font-weight:600">TrimEnd</code> | 1 |
| <code style="color:#228be6;font-weight:600">TrimStart</code> | 1 |
| <code style="color:#228be6;font-weight:600">TryAdd</code> | 5 |
| <code style="color:#228be6;font-weight:600">TryGetValue</code> | 5 |
| <code style="color:#228be6;font-weight:600">typeof</code> | 7 |
| <code style="color:#228be6;font-weight:600">Values</code> | 5 |
| <code style="color:#228be6;font-weight:600">Where</code> | 4 |
| <code style="color:#228be6;font-weight:600">??</code> en <code style="color:#228be6;font-weight:600">??=</code> | 6 |
| <code style="color:#228be6;font-weight:600">?.</code> | 6 |
| <code style="color:#228be6;font-weight:600">%</code> | 2 |
