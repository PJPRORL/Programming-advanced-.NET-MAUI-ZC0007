# C#-bouwstenen — studiedocument

<b>Programming Advanced</b> &nbsp;·&nbsp; verzamelingen en eigen types &nbsp;·&nbsp; met voorspelvragen om jezelf te testen

## Hoe je dit gebruikt

Het methodenoverzicht ging over de werkwoorden: wat je met iets kan dóen. Dit
document gaat over de zelfstandige naamwoorden: wat een `Dictionary`, een `List`
of een `record` eigenlijk ís, en waarom je de ene kiest boven de andere.

Elk onderwerp volgt dezelfde vijf stappen:

1. **Wat het is** — in één zin, met een vergelijking uit het dagelijks leven.
2. **Hoe het eruitziet** — een klein voorbeeld: aanmaken, vullen, uitlezen, doorlopen.
3. **Wanneer wel, wanneer niet.**
4. **Veelgemaakte fouten.**
5. **Voorspel de uitkomst** — korte vragen. Lees de code, beslis wat er op het
   scherm komt, en klik pas dan op het antwoord.

Die laatste stap is de belangrijkste. Uitleg lezen voelt als begrijpen, maar pas
als je een uitkomst juist voorspelt, weet je of het zit. Heb je er één fout, lees
dan de uitleg bij het antwoord en probeer de code zelf uit in een lege
consoletoepassing.

> [!NOTE]
> De voorbeelden gaan bewust over fruit, steden, leerlingen en verkeerslichten.
> Welke bouwsteen je in je eigen projecten gebruikt, beslis je zelf — dit document
> geeft je de kennis om die keuze te kunnen maken.

---

## Keuzegids — welke bouwsteen neem ik?

| Ik wil… | Neem |
|---|---|
| een vast aantal dingen dat nooit verandert | `array` |
| een aantal dingen waar ik bijvoeg en afhaal | `List` |
| iets terugvinden op een naam, code of nummer | `Dictionary` |
| zeker zijn dat er geen dubbels in zitten | `HashSet` |
| dingen afhandelen in de volgorde waarin ze binnenkwamen | `Queue` |
| het laatst toegevoegde als eerste terugnemen | `Stack` |
| een verzameling doorgeven zonder te zeggen welke soort | `IEnumerable` |
| een eigen ding met gegevens én gedrag | `class` |
| een pakketje gegevens dat gelijk is zodra de inhoud gelijk is | `record` |
| een klein pakketje dat bij kopiëren echt gekopieerd wordt | `struct` |
| een vaste lijst benoemde keuzes | `enum` |
| afspreken wat iets moet kunnen, zonder te zeggen hoe | `interface` |

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

# Deel 1 — Verzamelingen

## 1. Array

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
[Referentie tegenover waarde](#referentie-tegenover-waarde).

</details>

---

## 2. List

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

## 3. Dictionary

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

## 4. HashSet

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

## 5. Queue

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

## 6. Stack

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

## 7. IEnumerable

<span style="background:#1c7ed6;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>VERZAMELING</b></span>

### Wat het is

**Geen verzameling, maar een belofte: "je kan mij één voor één doorlopen".** Zoals
een playlist: je weet dat je nummer na nummer kan afspelen, maar niet
noodzakelijk hoeveel er zijn of wat het vijfde is.

Elke verzameling hierboven is óók een `IEnumerable`. Een array, een `List`, een
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

# Deel 2 — Eigen types

Tot hier ging het over verzamelingen die C# je geeft. Nu over de dingen die je
zelf maakt.

## 8. class

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

## 9. record

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

## 10. struct

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

Vergelijk dit met vraag 1 bij de klasse. Dezelfde drie regels, een tegengesteld
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

## 11. enum

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

## 12. interface

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

# Verschillen die je moet kennen

## Referentie tegenover waarde

Dit is het belangrijkste idee uit dit hele document. Als je één ding onthoudt,
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

Test jezelf: kijk terug naar vraag 4 bij de array, vraag 1 bij de klasse en de
vraag bij de struct. Drie keer dezelfde drie regels, en het antwoord hangt enkel
af van de soort type.

## class, record en struct naast elkaar

| | `class` | `record` | `struct` |
|---|---|---|---|
| **Soort** | referentie | referentie | waarde |
| **`a == b` vergelijkt** | is het hetzelfde object | is de inhoud gelijk | is de inhoud gelijk |
| **Aanpassen na het maken** | ja | met de korte schrijfwijze: nee | ja, maar liever niet |
| **Zichzelf tonen** | enkel de naam van het type | alle eigenschappen | enkel de naam van het type |
| **Typisch gebruik** | iets met gedrag en een identiteit | een pakketje gegevens | een klein getalachtig ding |

## Array tegenover List

Een array heeft een vaste lengte en is net iets sneller. Een `List` groeit mee.
In de praktijk kies je bijna altijd de `List`, tenzij de hoeveelheid echt
vastligt. De ene zegt `Length`, de andere `Count`.

## List tegenover Dictionary

Zoek je in een `List` telkens met `Find` of `FirstOrDefault` op dezelfde
eigenschap — een naam, een code — dan wil je eigenlijk een `Dictionary` met die
eigenschap als sleutel. De `List` kijkt elk item na tot hij het vindt. De
`Dictionary` weet meteen waar hij moet zijn.

Zoek je afwisselend op verschillende eigenschappen, of doet de volgorde ertoe,
dan blijft de `List` de betere keuze.

## Queue tegenover Stack

Allebei laten ze je enkel aan één kant bij. De `Queue` geeft terug wat het eerst
binnenkwam, de `Stack` wat het laatst binnenkwam. Stel jezelf de vraag: *wie komt
er als volgende aan de beurt?* Wie het langst wacht — `Queue`. Wie het laatst
kwam — `Stack`.

## IEnumerable tegenover List

Een `IEnumerable` is een vraag die nog gesteld moet worden. Een `List` is een
antwoord dat al klaarligt. Geef je iets door dat enkel doorlopen moet worden,
dan volstaat de vraag. Heb je een aantal, een positie of `Add` nodig, maak er dan
met `ToList()` een antwoord van.

---

# Index

| Naam | Hoofdstuk |
|---|---|
| array | [1](#1-array) |
| `class` | [8](#8-class) |
| constructor | [8](#8-class) |
| `Dequeue` | [5](#5-queue) |
| `Dictionary` | [3](#3-dictionary) |
| `enum` | [11](#11-enum) |
| `Enqueue` | [5](#5-queue) |
| `Enum.TryParse` | [11](#11-enum) |
| `ExceptWith` | [4](#4-hashset) |
| `HashSet` | [4](#4-hashset) |
| `IEnumerable` | [7](#7-ienumerable) |
| `interface` | [12](#12-interface) |
| `IntersectWith` | [4](#4-hashset) |
| `KeyValuePair` | [3](#3-dictionary) |
| `List` | [2](#2-list) |
| `Peek` | [5](#5-queue), [6](#6-stack) |
| `Pop` | [6](#6-stack) |
| `Push` | [6](#6-stack) |
| `Queue` | [5](#5-queue) |
| `record` | [9](#9-record) |
| referentietype | [Referentie tegenover waarde](#referentie-tegenover-waarde) |
| `Stack` | [6](#6-stack) |
| `struct` | [10](#10-struct) |
| `TryDequeue` | [5](#5-queue) |
| `TryGetValue` | [3](#3-dictionary) |
| `UnionWith` | [4](#4-hashset) |
| waardetype | [Referentie tegenover waarde](#referentie-tegenover-waarde) |
| `with` | [9](#9-record) |

Zoek je een methode zoals `Where`, `FirstOrDefault` of `Split`, dan staat die in
het methodenoverzicht.
