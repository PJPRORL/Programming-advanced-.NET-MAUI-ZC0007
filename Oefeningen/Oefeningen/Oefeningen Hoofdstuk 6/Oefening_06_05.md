# 06_05

<b>Hoofdstuk 06</b> &nbsp;·&nbsp; Een veel-op-veel-relatie, in schoolstijl &nbsp;·&nbsp; Fietsverhuur &nbsp;·&nbsp; niveau <span style="color:#f08c00">●●○</span> &nbsp;·&nbsp; ±2,5 uur

## Leerdoel

Na deze oefening kan je twee modellen verbinden in een veel-op-veel-relatie, via een tussentabel die zelf een gegeven bewaart.

Je leert twee relaties naar dezelfde tussentabel vastleggen met de Fluent API, startgegevens voor drie tabellen meegeven, en in een query via een navigation property rekenen met een `decimal` en een `double` samen, zoals in de schoolopgaven van hoofdstuk 6.

Daarnaast vervang je een fiets met haar volledige uitrusting, zonder dat er een koppeling verloren gaat.

## 

## Opdracht

Bij een fiets van Trapdoor hoort vaak wat uitrusting: een slot, een helm, een kinderzitje, een fietstas. Een fiets kan meerdere accessoires hebben, en hetzelfde accessoire hoort bij meerdere fietsen, telkens in een bepaald aantal. Elk accessoire heeft ook een eigen huurprijs per dag, en de balie wil per fiets weten wat de uitrusting erbovenop kost.

Jouw taak is om een model `Accessoire` toe te voegen, het via een tussentabel te verbinden met `Fiets`, en de koppelingen via de API te beheren, in de stijl van de schoolopgaven.

Via de API moet de balie, net als in 05_05, fietsen beheren, verhuren en de cijfers zien. Daarnaast moet ze:

1. alle accessoires en één accessoire kunnen opvragen;
2. kunnen opvragen welke accessoires bij een fiets horen;
3. kunnen opvragen wat de uitrusting van een fiets per dag kost;
4. een nieuwe koppeling tussen een fiets en een accessoire kunnen leggen;
5. erop kunnen rekenen dat een fiets met accessoires niet zomaar verdwijnt.

Implementeer onderstaande functionaliteiten.

### 1. Je vertrekpunt

Vertrek van je eigen oplossing van 05_05. Alle endpoints uit die oefening blijven werken.

> [!NOTE]
> In deze oefening bouw je je database opnieuw op. Verwijder ze, maak de nieuwe migratie uit punt 4, en laat je database daarna volledig opbouwen door al je migraties.

---

### 2. De modellen

| Accessoire       | C#-type                  | Kolom in de database |
|------------------|--------------------------|----------------------|
| Id               | `int`                    | de sleutel |
| Naam             | `string`                 | tekst van hoogstens 50 tekens, verplicht |
| PrijsPerDag      | `decimal`                | getal met 6 cijfers, waarvan 2 na de komma, verplicht |
| FietsAccessoires | `List<FietsAccessoire>?` | navigation property, geen kolom |

| FietsAccessoire | C#-type       | Kolom in de database |
|-----------------|---------------|----------------------|
| Id              | `int`         | de sleutel |
| FietsId         | `int`         | vreemde sleutel naar `Fietsen`, verplicht |
| Fiets           | `Fiets?`      | navigation property, geen kolom |
| AccessoireId    | `int`         | vreemde sleutel naar `Accessoires`, verplicht |
| Accessoire      | `Accessoire?` | navigation property, geen kolom |
| Aantal          | `double`      | kommagetal, verplicht |

Voeg aan `Fiets` een navigation property `FietsAccessoires` toe, van het type `List<FietsAccessoire>?`. De tabellen heten `Accessoires` en `FietsAccessoires`. Voeg ze toe aan je `FietsverhuurContext`.

> [!NOTE]
> De prijs van een accessoire is een `decimal`, het aantal een `double`. Dat is geen vergissing: de schoolopgaven van hoofdstuk 6 doen precies hetzelfde, met de prijs van een product en het aantal op een orderlijn. Volg de types van de opgave, ook als ze verschillen van wat je elders gebruikt.

---

### 3. De relaties

Leg in `OnModelCreating` met de Fluent API twee relaties vast, elk met `HasOne`, `WithMany`, `HasForeignKey`, `OnDelete` en `IsRequired`:

- een koppeling hoort bij één fiets, en een fiets heeft veel koppelingen;
- een koppeling hoort bij één accessoire, en een accessoire heeft veel koppelingen.

Samen vormen ze de veel-op-veel-relatie tussen fietsen en accessoires. Een fiets of een accessoire waaraan nog een koppeling hangt, mag niet verwijderd kunnen worden, ook niet rechtstreeks in de database.

> [!WARNING]
> Kijk na het maken van je migratie bij elke vreemde sleutel welke `onDelete` er staat. Staat er bij één van de twee iets anders dan bij de andere, dan heb je die relatie niet zelf vastgelegd, en koos Entity Framework Core voor jou.

---

### 4. De startgegevens

Leg met `HasData` deze accessoires en koppelingen vast:

| Id | Naam        | PrijsPerDag |
|----|-------------|-------------|
| 1  | fietsslot   | 1.50        |
| 2  | helm        | 2.00        |
| 3  | kinderzitje | 4.50        |
| 4  | fietstas    | 1.25        |

| Id | FietsId | AccessoireId | Aantal |
|----|---------|--------------|--------|
| 1  | 1       | 1            | 1      |
| 2  | 1       | 4            | 2      |
| 3  | 2       | 1            | 1      |
| 4  | 2       | 2            | 1      |
| 5  | 4       | 3            | 2      |
| 6  | 4       | 1            | 1      |

Maak een migratie met de naam `Accessoires`, verwijder je database, en bouw ze opnieuw op met al je migraties.

> [!TIP]
> Nu staat er een `decimal` in je startgegevens. Lees in je leerboek, hoofdstuk 39, bij de regels van `HasData`, wat je dan achter het getal zet.

---

### 5. De repositories

Maak de interfaces en klassen `IAccessoireRepository` en `IFietsAccessoireRepository`, asynchroon, met je context in een `readonly` veld. Registreer ze met `AddScoped`.

| `IAccessoireRepository` | Parameters | Geeft terug |
|-------------------------|------------|-------------|
| `GetAllAsync` | geen | `Task<List<Accessoire>>` |
| `GetByIdAsync` | `int id` | `Task<Accessoire?>` |

| `IFietsAccessoireRepository` | Parameters | Geeft terug |
|------------------------------|------------|-------------|
| `GetByIdAsync` | `int id` | `Task<FietsAccessoire?>` |
| `CreateAsync` | `FietsAccessoire fietsAccessoire` | `Task<FietsAccessoire>` |
| `GetByFietsIdAsync` | `int fietsId` | `Task<List<FietsAccessoire>>` |
| `BestaatKoppelingAsync` | `int fietsId, int accessoireId` | `Task<bool>` |
| `HeeftKoppelingenAsync` | `int fietsId` | `Task<bool>` |
| `VerplaatsAsync` | `int oudeFietsId, int nieuweFietsId` | `Task` |
| `GetUitrustingsPrijsAsync` | `int fietsId` | `Task<double>` |

- De lijsten zijn op Id gesorteerd. `GetByFietsIdAsync` geeft een lege lijst als de fiets geen koppelingen heeft of niet bestaat.
- `CreateAsync` berekent de Id zelf: de hoogste Id plus één, of 1 in een lege tabel.
- `VerplaatsAsync` laat elke koppeling van de oude fiets voortaan naar de nieuwe wijzen.
- `GetUitrustingsPrijsAsync` geeft wat de uitrusting per dag kost: voor elke koppeling de prijs per dag van het accessoire maal het aantal, alles opgeteld, met `SumAsync`, of `0` als er geen koppelingen zijn.

> [!WARNING]
> C# vermenigvuldigt een `decimal` en een `double` niet zomaar met elkaar: je project bouwt dan niet. Kijk welk type je methode moet teruggeven, en zet het andere getal om naar dat type.

> [!TIP]
> Voor de uitrustingsprijs hoef je geen accessoire op te halen. In een query mag je via de navigation property van een koppeling naar de prijs van haar accessoire. Entity Framework Core maakt daar zelf een join van.

---

### 6. De endpoints

| Endpoint | Antwoord |
|----------|----------|
| <span style="background:#1c7ed6;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>GET</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">accessoires</code> | <code style="color:#37b24d;font-weight:600">200 OK</code> met alle accessoires, op Id gesorteerd |
| <span style="background:#1c7ed6;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>GET</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">accessoires/<span style="color:#e64980"><b>{id}</b></span></code> | <code style="color:#37b24d;font-weight:600">200 OK</code> met het accessoire, of <code style="color:#f08c00;font-weight:600">404 Not Found</code> zonder body |
| <span style="background:#1c7ed6;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>GET</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">fietsen/<span style="color:#e64980"><b>{id}</b></span>/accessoires</code> | <code style="color:#37b24d;font-weight:600">200 OK</code> met de koppelingen van die fiets, op Id gesorteerd |
| <span style="background:#1c7ed6;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>GET</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">fietsen/<span style="color:#e64980"><b>{id}</b></span>/uitrustingsprijs</code> | <code style="color:#37b24d;font-weight:600">200 OK</code> met een object met een property `totaalPrijs`, een `double` |
| <span style="background:#1c7ed6;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>GET</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">fietsaccessoires/<span style="color:#e64980"><b>{id}</b></span></code> | <code style="color:#37b24d;font-weight:600">200 OK</code> met de koppeling, of <code style="color:#f08c00;font-weight:600">404 Not Found</code> zonder body |
| <span style="background:#2f9e44;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>POST</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">fietsaccessoires</code> | <code style="color:#37b24d;font-weight:600">201 Created</code> met de nieuwe koppeling en haar adres, of <code style="color:#f08c00;font-weight:600">400 Bad Request</code> met de reden |

Een fiets zonder koppelingen, of een fiets die niet bestaat, geeft bij het derde endpoint `[]` en bij het vierde `{ "totaalPrijs": 0 }`, telkens met <code style="color:#37b24d;font-weight:600">200 OK</code>, zoals de schoolopgaven het vragen.

Bij een nieuwe koppeling controleer je in deze volgorde:

1. de fiets bestaat niet: <code style="color:#f08c00;font-weight:600">400 Bad Request</code> met <code style="color:#845ef7">We vonden geen fiets met Id <span style="color:#e64980"><b>{fietsId}</b></span>.</code>
2. het accessoire bestaat niet: <code style="color:#f08c00;font-weight:600">400 Bad Request</code> met <code style="color:#845ef7">We vonden geen accessoire met Id <span style="color:#e64980"><b>{accessoireId}</b></span>.</code>
3. het aantal is kleiner dan 1: <code style="color:#f08c00;font-weight:600">400 Bad Request</code> met <code style="color:#845ef7">Het aantal moet minstens 1 zijn.</code>
4. dat accessoire hoort al bij die fiets: <code style="color:#f08c00;font-weight:600">400 Bad Request</code> met <code style="color:#845ef7">Dit accessoire hoort al bij deze fiets.</code>

Pas ook het verwijderen van een fiets aan:

- een fiets verwijderen waarnaar nog een koppeling verwijst: <code style="color:#f08c00;font-weight:600">400 Bad Request</code> met <code style="color:#845ef7">Deze fiets heeft nog accessoires.</code>

<code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> accessoires/4</code> geeft als resultaat:

```json
{
  "id": 4,
  "naam": "fietstas",
  "prijsPerDag": 1.25,
  "fietsAccessoires": null
}
```

> [!NOTE]
> Een `decimal` onthoudt wel hoeveel cijfers er na de komma stonden: de helm kan als `2.00` terugkomen, waar een `double` `2` zou tonen. Vergelijk bedragen als getal.

> [!WARNING]
> Krijg je een <code style="color:#f03e3e;font-weight:600">500</code> met een melding over een *object cycle*? Kijk dan welke objecten je in één verzoek ophaalt, en of je ze allemaal nodig hebt.

---

### 7. Alles samen: een fiets vervangen met haar uitrusting

<span style="background:#7048e8;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>SAMENGESTELDE OEFENING</b></span>

Vervangen werkte tot nu toe als verwijderen en toevoegen. Maar een fiets met koppelingen mag je niet meer verwijderen. Bij een vervanging nemen de koppelingen mee: na de vervanging heeft de nieuwe fiets precies de uitrusting van de oude.

<span style="background:#d9822b;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>PUT</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">fietsen/vervang/<span style="color:#e64980"><b>{id}</b></span></code>

De controles en de statuscodes blijven zoals in 05_05. Faalt een controle, dan verandert er niets.

#### Testscenario

Bouw je database opnieuw op, zodat ze exact de startgegevens bevat, en doorloop deze reeks. Bedragen vergelijk je als getal.

1. Koppel accessoire 2 aan fiets 3, met aantal 1. Je krijgt <code style="color:#37b24d;font-weight:600">201</code>, en de nieuwe koppeling heeft Id 7.
2. Doe hetzelfde nog eens. Je krijgt <code style="color:#f08c00;font-weight:600">400</code> met <code style="color:#845ef7">Dit accessoire hoort al bij deze fiets.</code>
3. Koppel accessoire 99 aan fiets 1, met aantal 1. Je krijgt <code style="color:#f08c00;font-weight:600">400</code> met <code style="color:#845ef7">We vonden geen accessoire met Id 99.</code>
4. Verwijder fiets 2. Je krijgt <code style="color:#f08c00;font-weight:600">400</code> met <code style="color:#845ef7">Deze fiets heeft nog accessoires.</code>
5. <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> fietsen/1/uitrustingsprijs</code> geeft `{ "totaalPrijs": 4 }`, voor fiets 4 is dat `10.5`, en voor fiets 3 `2`.
6. Vervang fiets 3 door een geldige nieuwe fiets. Je krijgt <code style="color:#f08c00;font-weight:600">400</code> met <code style="color:#845ef7">Een verhuurde fiets kan je niet uit de vloot halen.</code>
7. Vervang fiets 1 door deze fiets:

   ```json
   { "type": "stadsfiets", "framemaat": 56, "prijsPerDag": 13.5 }
   ```

   Je krijgt <code style="color:#37b24d;font-weight:600">201</code>, en de nieuwe fiets heeft Id 5 en is beschikbaar.
8. <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> fietsen/5/accessoires</code> geeft de koppelingen 1 en 2, allebei met `fietsId` 5, en <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> fietsen/5/uitrustingsprijs</code> geeft `{ "totaalPrijs": 4 }`.
9. <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> fietsen/1</code> geeft <code style="color:#f08c00;font-weight:600">404</code>, <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> fietsen/1/accessoires</code> geeft `[]`, en <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> fietsen/1/uitrustingsprijs</code> geeft `{ "totaalPrijs": 0 }`. De verhuringen zijn nog altijd leeg.
10. Verwijder fiets 5. Je krijgt <code style="color:#f08c00;font-weight:600">400</code> met <code style="color:#845ef7">Deze fiets heeft nog accessoires.</code>
