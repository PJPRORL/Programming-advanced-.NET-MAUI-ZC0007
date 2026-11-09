# Hoofdmodel Onderdeel

**HOOFDMODEL ONDERDEEL · V1 · 14 OKTOBER 2026**

Je vroeg welke eigenschappen alle onderdeeltypes gemeen hebben, zodat ze één keer in het hoofdmodel Onderdeel kunnen. Hieronder staat de vergelijking, en daarna de klasse zelf.

## Het antwoord in het kort

Vier eigenschappen komen bij alle onderdelen terug, met overal dezelfde betekenis: **Id**, **Kraken-Id**, **Merk** en **Naam**. Die gaan in Onderdeel.

Verder is er niets dat alle tien de types delen. Kleur komt het dichtst in de buurt, met zeven van de tien. Andere eigenschappen lijken soms gedeeld omdat het woord terugkomt, zoals socket, type of TDP, maar de betekenis verschilt per type. Die blijven bij hun eigen type.

## Waarmee vergeleken

- **De analyse**, `Analyse Alternate Bouwcontrole - met hoofdmodel.docx`, zoals ze nu in Onderzoeksdocumentatie staat. Dat is de versie van 30 september. Wat je er daarna nog in gezet hebt, zit niet in deze vergelijking. Er staat een Word-vergrendelbestand van 9 oktober naast, dus het kan zijn dat het document nog open staat zonder dat het opgeslagen is.
- **Je C#-modellen** van vandaag: Onderdeel, Moederbord, Processor, ProcessorKoeler en Werkgeheugen. Behuizing, Casefan, Grafischekaart, HDD, SSD en Voeding zijn nog leeg. Bij die zes kan alleen de analyse iets zeggen.

De maatstaf is dezelfde als in hoofdstuk 6 van de analyse: een eigenschap gaat in het hoofdmodel als ze bij alle tien de types bestaat en overal hetzelfde betekent.

## Groep 1: bij alle tien, met dezelfde betekenis

In de tabel staat **A** als de eigenschap in de analyse staat, **C** als ze in je C#-model staat, **A+C** als het allebei is en **–** als ze nergens staat.

| Onderdeeltype | C#-model | Id | Kraken-Id | Naam | Merk | Kleur | Artikelcode |
|---|---|---|---|---|---|---|---|
| Moederbord | ingevuld | A+C | A | A+C | C | A | – |
| Processor | ingevuld | A | A+C | A+C | C | – | – |
| CPU-koeler | ingevuld | A | A+C | A+C | C | A | – |
| Geheugen | ingevuld | A | A+C | A+C | C | A | – |
| Videokaart | leeg | A | A | A | – | A | – |
| SSD | leeg | A | A | A | – | – | – |
| HDD | leeg | A | A | A | – | – | – |
| PC-behuizing | leeg | A | A | A | – | A | – |
| Voeding | leeg | A | A | A | – | A | – |
| Casefans | leeg | A | A | A | – | A | – |
| **Bij alle tien?** | | ✔ ja | ✔ ja | ✔ ja | ½ in je modellen | ✘ 7 van 10 | ✘ nergens |

Twee dingen om bij de tabel te weten.

- Id, Kraken-Id en Naam staan niet in de tabellen per onderdeel van hoofdstuk 3 en 5. Die tabellen tonen alleen wat een controle nodig heeft. Hoofdstuk 6 zegt dat elk type ze draagt, en dat volgt uit wat ze zijn: elk product heeft een nummer, een code en een naam.
- Bij het moederbord staat Kraken-Id niet in het model, maar je gebruikt het wel in ProductController. Id, Merk en Naam krijgt het moederbord nu al mee van Onderdeel.

Wat er in het hoofdmodel gaat:

| Eigenschap | Soort waarde | Waarom in het hoofdmodel | Wat er verandert tegenover nu |
|---|---|---|---|
| Id | geheel getal | Het volgnummer uit hoofdstuk 6. Het programma kent het zelf toe, het verandert nooit en het is uniek over alle types heen. | Niets. Staat al in Onderdeel. |
| KrakenId | tekst | De code waarop gescand en gezocht wordt, zonder dat het type vooraf bekend is. Tekst en geen getal: er wordt nooit mee gerekend, een scanner levert tekst, en als getal valt een voorloopnul weg. | Komt erbij. In Processor, ProcessorKoeler en Werkgeheugen is het nu een getal. |
| Merk | tekst | Staat in al je ingevulde modellen en betekent overal hetzelfde. | `Brand` wordt `Merk`. |
| Naam | tekst | Het scherm toont bij elke gescande regel de naam, welk type het ook is. | `Name` wordt `Naam`. |

> [!NOTE]
> **Merk is de enige van de vier die niet uit de analyse komt.** Hoofdstuk 6 liet het weg omdat geen enkele controle ervan afhangt, en het merk staat meestal al in de naam. Jij hebt het in elk model gezet, en de vraag was wat je modellen gemeen hebben. Daarom staat het erin. Wil je de analyse strikt volgen, dan is het één regel die weg moet.

## Groep 2: bij een deel van de types, met dezelfde betekenis

| Eigenschap | Waar het staat | Waar het ontbreekt | Wanneer het naar het hoofdmodel gaat |
|---|---|---|---|
| Kleur | analyse: moederbord, CPU-koeler, geheugen, videokaart, behuizing, voeding, casefans | processor, SSD en HDD, en in geen enkel C#-model | Als je beslist dat alle tien een kleur dragen, met "niet zichtbaar" als mogelijke waarde. Hoofdstuk 7 rekent SSD en HDD al bij de zichtbare onderdelen. Dan blijft alleen de processor over. |
| Artikelcode | nergens; in de analyse is het een open vraag | overal | Als je beslist dat hij naast het Kraken-Id blijft bestaan. Dan heeft elk onderdeel er één. In het Alpha-project zit hij wel. |
| Formaat 2,5″ of 3,5″ en de SATA-aansluiting | SSD en HDD | de andere acht types | Niet. Dit delen alleen de twee schijven. De analyse telt ze samen bij "schijven onderling", maar dat is iets tussen die twee, niet iets voor alle onderdelen. |

Kleur is dus de enige echte twijfel. Komt ze in het hoofdmodel, dan is het een vaste keuze zoals hoofdstuk 5 dat noemt: één lijst met kleuren die voor alle types dezelfde is. Anders vergelijk je straks "zwart" met "Zwart".

## Groep 3: hetzelfde woord, een andere betekenis

Deze lijken gedeeld omdat de naam terugkomt. Kijk je naar wat er telkens mee bedoeld wordt, dan is het bij elk type iets anders. In het hoofdmodel zouden ze bij elk type iets anders betekenen, of bij de meeste types niets.

| Woord | Waar het voorkomt | Wat het telkens betekent |
|---|---|---|
| Socket | moederbord, processor, CPU-koeler; in C# bij alle drie | Moederbord en processor hebben er één. De koeler heeft een lijst van sockets die hij ondersteunt. In ProcessorKoeler is het nu één tekst. |
| TDP en vermogen | processor, CPU-koeler, videokaart, voeding; in C# `TDP` bij Processor en ProcessorKoeler | Processor: de warmte die hij afgeeft. Koeler: de warmte die hij kan afvoeren. Videokaart: wat ze verbruikt. Voeding: wat ze levert. In je code heten de eerste twee allebei TDP, maar ze staan aan de twee kanten van dezelfde controle. |
| Type | CPU-koeler, geheugen, behuizing, SSD; in C# `Type`, `MemoryType` en `PCIeType` | Koeler: lucht of water. Geheugen: DDR4 of DDR5. Behuizing: horizontale of verticale uitbreidingssloten. SSD: M.2 of SATA. |
| Formfactor | moederbord, voeding, behuizing | Moederbord: ATX tot E-ATX. Voeding: ATX, SFX of SFX-L. Een ATX-voeding en een ATX-moederbord hebben niets met elkaar te maken, ook al staat er hetzelfde woord. De behuizing heeft van allebei een lijst. |
| Hoogte | CPU-koeler, geheugen, behuizing | De hoogte van de koeler, de vrije ruimte onder de koeler, de hoogte van een geheugenmodule en de maximale koelerhoogte van de behuizing. Vier verschillende maten. |
| Lengte | videokaart, voeding, behuizing | De lengte van de kaart, de lengte van de voeding, en van allebei een maximum in de behuizing. |
| Capaciteit | geheugen, moederbord; in C# `Capaciteit` en `MaximumSupportedMemorySize` | Bij het geheugen per module, bij het moederbord het totaal dat het bord aankan. |
| Aantal in de set | geheugen, casefans; in C# `module` | Het aantal modules in een kit, of het aantal fans in een pak. |
| Afmeting | casefans; in C# `Afmeting` bij ProcessorKoeler | Bij de fans 120 of 140 mm. Bij de koeler zegt de naam niet welke maat het is: de hoogte, het radiatorformaat of de radiatordikte uit de analyse. |
| Aansluiting | moederbord, videokaart, voeding, SSD, HDD, casefans; in C# `Aansluiting` bij ProcessorKoeler | Telkens een andere aansluiting: fan-headers, USB, ARGB, stroomkabels, M.2 of SATA. Voor de koeler staat er in de analyse geen aansluiting, dus het is niet duidelijk welke je bedoelt. |
| Chipset | in C# bij Moederbord en Processor; niet in de analyse | Het moederbord heeft een chipset. Een processor heeft er zelf geen, hij werkt samen met een aantal chipsets. Voor die controle gebruikt de analyse de generatie van de processor. |

## De klasse Onderdeel

Dit is de uitzondering die je vandaag goedgekeurd hebt. Ze geldt alleen voor het hoofdmodel. De andere modellen pas je zelf aan. Wat er daarbij verandert, staat in het volgende hoofdstuk.

```
namespace Alternate.Bouwcontrole.API.Models
{
    public abstract class Onderdeel
    {
        // Attributes && Properties
        public int Id { get; set; }

        public string KrakenId { get; set; } = "";

        public string Merk { get; set; } = "";

        public string Naam { get; set; } = "";

        // Constructor


        // Methods

    }
}
```

Waarom ze er zo uitziet:

- **Dezelfde opbouw als Laptop uit de cursus** (Programming Advanced les 02). Dezelfde commentaarblokken, en elke tekst krijgt een lege beginwaarde zodat Nullable geen waarschuwing geeft.
- **abstract**, zoals beslist op 30 september. Er bestaat geen onderdeel zonder soort. Wat je aanmaakt, is altijd een moederbord, een processor of een ander type.
- **Geen constructor.** Laptop heeft er ook geen. In ProductController maak je een moederbord aan met accolades, `new Moederbord { ... }`. Dat werkt alleen als er een constructor zonder parameters is, en zonder eigen constructor krijg je die vanzelf.
- **Nederlandse namen.** Projectafspraken §7: Nederlands voor het domein. Een onderdeel en zijn eigenschappen zijn domein.
- **Geen attributen zoals [Required].** Validatie komt op de Write-DTO's (projectafspraken §6), niet op het model.

## Wat dit verandert aan je andere modellen

Geen code, wel wat er gebeurt en waarom. Bij elk punt staat de melding die je in de Error List zult zien.

1. **Moederbord roept `base(id, brand, name)` aan.** Onderdeel heeft geen constructor met drie parameters meer. Melding: **CS1729**, 'Onderdeel' does not contain a constructor that takes 3 arguments. Kijk hoe Laptop het doet en hoe je het moederbord in de controller aanmaakt. Heb je die constructor dan nog nodig?
2. **Moederbord en Behuizing zijn abstract.** Van een abstracte klasse kan je geen object maken, dus `new Moederbord { ... }` lukt niet. Melding: **CS0144**, Cannot create an instance of the abstract type or interface 'Moederbord'. Alleen Onderdeel hoort abstract te zijn. Behuizing erft daarnaast nog niet van Onderdeel.
3. **Processor, ProcessorKoeler en Werkgeheugen erven nog niet van Onderdeel.** Zodra ze dat doen, moeten KrakenId, Brand en Name eruit, want die komen dan van Onderdeel. Laat je ze staan, dan heeft het type twee keer dezelfde eigenschap en verbergt de ene de andere. Melding: **CS0108**, '...' hides inherited member 'Onderdeel....'. Bij KrakenId is dat extra verraderlijk: het getal in het type verbergt dan de tekst uit Onderdeel.
4. **ProductController.** Daar staat `KrakenId = 100038250` en `Brand`. Met de nieuwe klasse is het Kraken-Id tekst en heet het merk Merk. Meldingen: **CS0029**, Cannot implicitly convert type 'int' to 'string', en **CS0117**, 'Moederbord' does not contain a definition for 'Brand'.
5. **PCIeType in Moederbord heeft geen beginwaarde.** Nu vult de constructor het in. Valt de constructor weg, dan krijg je waarschuwing **CS8618**, Non-nullable property 'PCIeType' must contain a non-null value when exiting constructor. De andere teksten in Moederbord hebben die beginwaarde al wel.
6. **Namen half Engels, half Nederlands.** In Moederbord staan `MemorySlots` en `SATAConnectors`, in Werkgeheugen `Capaciteit` en in ProcessorKoeler `Aansluiting`. Projectafspraken §7 noemt dit als de valkuil: niet de keuze zelf, maar ze half doorvoeren. Producttermen als socket, chipset en TDP mogen blijven zoals ze zijn.

## Om later op te letten

- **Het Kraken-Id moet uniek zijn, maar de klasse kan dat niet afdwingen.** Een klasse zegt welke eigenschappen er zijn, niet dat er geen twee onderdelen met dezelfde code mogen bestaan. Dat komt op een andere plaats: bij het toevoegen, of in de database vanaf les 04 van Programming Advanced.
- **Een lijst van het type Onderdeel toont alleen wat in Onderdeel staat.** Geef je zo'n lijst als JSON terug, dan schrijft System.Text.Json per onderdeel alleen Id, KrakenId, Merk en Naam uit, niet wat van het moederbord of de processor zelf is. Het begrip om op te zoeken wanneer je zover bent: polymorfe serialisatie. Het is hetzelfde probleem waarom je JSON-bestand per onderdeeltype gegroepeerd is (projectafspraken §6).

## Zelf nakijken

- **Bouwen** (Ctrl+Shift+B) en de Error List openen. Na je aanpassingen staan CS1729, CS0144, CS0029 en CS0117 er niet meer in. CS0108 en CS8618 zijn waarschuwingen. Die staan bij Warnings, niet bij Errors, dus zet dat filter ook aan.
- **De vraag bij elke nieuwe eigenschap:** heeft elk van de tien types dit, en betekent het overal hetzelfde? Is één van de twee antwoorden nee, dan blijft ze bij het type.
- **In Scalar**, zodra de controller werkt: een moederbord toont dan ook `id`, `krakenId`, `merk` en `naam`, naast wat van het moederbord zelf is.

Sla je de analyse op met je toevoegingen van na 30 september, dan maak ik een V2 waarin die ook meetellen.
