# 06_02

<b>Hoofdstuk 06</b> &nbsp;·&nbsp; Een veel-op-veel-relatie &nbsp;·&nbsp; PC-onderdelen &nbsp;·&nbsp; niveau <span style="color:#f08c00">●●○</span> &nbsp;·&nbsp; ±2,5 uur

## Leerdoel

Na deze oefening kan je twee modellen verbinden in een veel-op-veel-relatie, via een tussentabel die zelf een gegeven bewaart.

Je leert twee relaties naar dezelfde tussentabel vastleggen met de Fluent API, startgegevens voor drie tabellen meegeven, en in een query via een navigation property rekenen met gegevens uit een andere tabel.

Daarnaast vervang je een item in alle koppelingen tegelijk, zonder dat er een koppeling verloren gaat.

## 

## Opdracht

Klanten vragen aan de balie geregeld welke ventilatoren in welke behuizing passen. Een behuizing heeft plaats voor meerdere ventilatoren, en één ventilator past in meerdere behuizingen. Per combinatie wil Bit & Byte ook weten hoeveel van die ventilatoren erin passen, en wat het kost om een behuizing helemaal vol te zetten.

Jouw taak is om een model `Behuizing` toe te voegen, het via een tussentabel te verbinden met `Ventilator`, en de koppelingen via de API te beheren.

Via de API moet de inkoopafdeling, net als in 05_02, ventilatoren beheren, de historiek nalezen en de prijzen volgen. Daarnaast moet ze:

1. alle behuizingen en één behuizing kunnen opvragen;
2. kunnen opvragen welke ventilatoren in een behuizing passen;
3. kunnen opvragen wat het kost om een behuizing vol te zetten;
4. een nieuwe koppeling tussen een behuizing en een ventilator kunnen leggen;
5. erop kunnen rekenen dat een ventilator die nog in een behuizing past, niet zomaar verdwijnt.

Implementeer onderstaande functionaliteiten.

### 1. Je vertrekpunt

Vertrek van je eigen oplossing van 05_02. Alle endpoints uit die oefening blijven werken.

> [!NOTE]
> In deze oefening bouw je je database opnieuw op. Verwijder ze, maak de nieuwe migratie uit punt 4, en laat je database daarna volledig opbouwen door al je migraties.

---

### 2. De modellen

| Behuizing         | C#-type                  | Kolom in de database |
|-------------------|--------------------------|----------------------|
| Id                | `int`                    | de sleutel, de database telt zelf op |
| Naam              | `string`                 | tekst van hoogstens 50 tekens, verplicht |
| Formaat           | `string`                 | tekst van hoogstens 20 tekens, verplicht |
| Compatibiliteiten | `List<Compatibiliteit>?` | navigation property, geen kolom |

| Compatibiliteit | C#-type       | Kolom in de database |
|-----------------|---------------|----------------------|
| Id              | `int`         | de sleutel, de database telt zelf op |
| BehuizingId     | `int`         | vreemde sleutel naar `Behuizingen`, verplicht |
| Behuizing       | `Behuizing?`  | navigation property, geen kolom |
| VentilatorId    | `int`         | vreemde sleutel naar `Ventilatoren`, verplicht |
| Ventilator      | `Ventilator?` | navigation property, geen kolom |
| AantalPlaatsen  | `int`         | geheel getal, verplicht |

Voeg aan `Ventilator` een navigation property `Compatibiliteiten` toe, van het type `List<Compatibiliteit>?`. De tabellen heten `Behuizingen` en `Compatibiliteiten`. Voeg ze toe aan je `VentilatorContext`.

---

### 3. De relaties

Leg in `OnModelCreating` met de Fluent API twee relaties vast, elk verplicht:

- een compatibiliteit hoort bij één behuizing, en een behuizing heeft veel compatibiliteiten;
- een compatibiliteit hoort bij één ventilator, en een ventilator heeft veel compatibiliteiten.

Samen vormen ze de veel-op-veel-relatie tussen behuizingen en ventilatoren. Een behuizing of een ventilator waaraan nog een compatibiliteit hangt, mag niet verwijderd kunnen worden, ook niet rechtstreeks in de database.

> [!WARNING]
> Kijk na het maken van je migratie bij elke vreemde sleutel welke `onDelete` er staat. Staat er bij één van de twee iets anders dan bij de andere, dan heb je die relatie niet zelf vastgelegd, en koos Entity Framework Core voor jou.

---

### 4. De startgegevens

Leg met `HasData` deze behuizingen en compatibiliteiten vast:

| Id | Naam         | Formaat    |
|----|--------------|------------|
| 1  | Silent Base  | midi tower |
| 2  | Airflow Mini | mini tower |
| 3  | Studio XL    | big tower  |

| Id | BehuizingId | VentilatorId | AantalPlaatsen |
|----|-------------|--------------|----------------|
| 1  | 1           | 1            | 3              |
| 2  | 1           | 2            | 2              |
| 3  | 2           | 1            | 2              |
| 4  | 2           | 3            | 2              |
| 5  | 3           | 2            | 4              |
| 6  | 3           | 4            | 3              |

Maak een migratie met de naam `Behuizingen`, verwijder je database, en bouw ze opnieuw op met al je migraties. Zorg dat de teller van elke tabel daarna klopt, zoals in 04_02.

---

### 5. Behuizingen en compatibiliteiten opvragen

Maak de interfaces en klassen `IBehuizingRepository` en `ICompatibiliteitRepository`, asynchroon zoals in les 05. Registreer ze met `AddScoped`.

| `IBehuizingRepository` | Parameters | Geeft terug |
|------------------------|------------|-------------|
| `GetAllAsync` | geen | `Task<List<Behuizing>>` |
| `GetByIdAsync` | `int id` | `Task<Behuizing?>` |
| `GetCompatibiliteitenAsync` | `int behuizingId` | `Task<List<Compatibiliteit>>` |
| `GetVolPrijsAsync` | `int behuizingId` | `Task<double>` |

| `ICompatibiliteitRepository` | Parameters | Geeft terug |
|------------------------------|------------|-------------|
| `GetByIdAsync` | `int id` | `Task<Compatibiliteit?>` |
| `CreateAsync` | `Compatibiliteit compatibiliteit` | `Task<Compatibiliteit>` |
| `BestaatKoppelingAsync` | `int behuizingId, int ventilatorId` | `Task<bool>` |
| `HeeftKoppelingenAsync` | `int ventilatorId` | `Task<bool>` |
| `VerplaatsAsync` | `int oudeVentilatorId, int nieuweVentilatorId` | `Task` |

`GetVolPrijsAsync` geeft wat het kost om de behuizing vol te zetten: voor elke compatibiliteit de prijs van de ventilator maal het aantal plaatsen, alles opgeteld, met `SumAsync`, of `0` als er geen compatibiliteiten zijn. Rond de som af op twee cijfers na de komma, zoals in 04_03. `VerplaatsAsync` laat elke compatibiliteit van de oude ventilator voortaan naar de nieuwe wijzen.

> [!TIP]
> Voor de volle prijs hoef je geen ventilator op te halen. In een query mag je via de navigation property van een compatibiliteit naar de prijs van haar ventilator. Entity Framework Core maakt daar zelf een join van. Kijk in je terminal naar de SQL.

| Verzoek | Antwoord |
|---------|----------|
| <span style="background:#1c7ed6;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>GET</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/behuizing</code> | <code style="color:#37b24d;font-weight:600">200 OK</code> met alle behuizingen, op Id gesorteerd |
| <span style="background:#1c7ed6;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>GET</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/behuizing/<span style="color:#e64980"><b>{id}</b></span></code> | <code style="color:#37b24d;font-weight:600">200 OK</code> met de behuizing, of <code style="color:#f08c00;font-weight:600">404 Not Found</code> met <code style="color:#845ef7">We vonden geen behuizing met Id <span style="color:#e64980"><b>{id}</b></span>.</code> |
| <span style="background:#1c7ed6;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>GET</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/behuizing/<span style="color:#e64980"><b>{id}</b></span>/compatibiliteiten</code> | <code style="color:#37b24d;font-weight:600">200 OK</code> met de compatibiliteiten van die behuizing, op Id gesorteerd, of <code style="color:#f08c00;font-weight:600">404 Not Found</code> met hetzelfde bericht als hierboven |
| <span style="background:#1c7ed6;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>GET</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/behuizing/<span style="color:#e64980"><b>{id}</b></span>/volprijs</code> | <code style="color:#37b24d;font-weight:600">200 OK</code> met een object met een property `totaalPrijs`, een `double`, of <code style="color:#f08c00;font-weight:600">404 Not Found</code> met hetzelfde bericht als hierboven |
| <span style="background:#1c7ed6;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>GET</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/compatibiliteit/<span style="color:#e64980"><b>{id}</b></span></code> | <code style="color:#37b24d;font-weight:600">200 OK</code> met de compatibiliteit, of <code style="color:#f08c00;font-weight:600">404 Not Found</code> met <code style="color:#845ef7">We vonden geen compatibiliteit met Id <span style="color:#e64980"><b>{id}</b></span>.</code> |

<code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> api/behuizing/2/compatibiliteiten</code> geeft als resultaat:

```json
[
  {
    "id": 3,
    "behuizingId": 2,
    "behuizing": null,
    "ventilatorId": 1,
    "ventilator": null,
    "aantalPlaatsen": 2
  },
  {
    "id": 4,
    "behuizingId": 2,
    "behuizing": null,
    "ventilatorId": 3,
    "ventilator": null,
    "aantalPlaatsen": 2
  }
]
```

<code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> api/behuizing/1/volprijs</code> geeft `{ "totaalPrijs": 139.5 }`, voor behuizing 2 is dat `129.6` en voor behuizing 3 `159.57`.

> [!WARNING]
> Krijg je een <code style="color:#f03e3e;font-weight:600">500</code> met een melding over een *object cycle*? Dan kent een compatibiliteit haar behuizing, en kent die behuizing haar compatibiliteiten weer, zonder einde. Dat gebeurt als je in hetzelfde verzoek eerst de behuizing ophaalt en daarna haar compatibiliteiten. Kijk welke objecten je in één verzoek ophaalt, en of je ze allemaal nodig hebt.

---

### 6. Een koppeling leggen

<span style="background:#2f9e44;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>POST</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/compatibiliteit</code>

Controleer in deze volgorde, en geef bij de eerste fout die je vindt <code style="color:#f08c00;font-weight:600">400 Bad Request</code> terug:

1. de behuizing bestaat niet: <code style="color:#f08c00;font-weight:600">400 Bad Request</code> met <code style="color:#845ef7">We vonden geen behuizing met Id <span style="color:#e64980"><b>{behuizingId}</b></span>.</code>
2. de ventilator bestaat niet: <code style="color:#f08c00;font-weight:600">400 Bad Request</code> met <code style="color:#845ef7">We vonden geen ventilator met Id <span style="color:#e64980"><b>{ventilatorId}</b></span>.</code>
3. het aantal plaatsen is kleiner dan 1: <code style="color:#f08c00;font-weight:600">400 Bad Request</code> met <code style="color:#845ef7">Het aantal plaatsen moet minstens 1 zijn.</code>
4. die ventilator is al aan die behuizing gekoppeld: <code style="color:#f08c00;font-weight:600">400 Bad Request</code> met <code style="color:#845ef7">Deze ventilator is al gekoppeld aan deze behuizing.</code>

Anders: <code style="color:#37b24d;font-weight:600">201 Created</code> met de nieuwe compatibiliteit en haar adres.

Pas ook het verwijderen van een ventilator aan:

- een ventilator verwijderen waarnaar nog een compatibiliteit verwijst: <code style="color:#f08c00;font-weight:600">400 Bad Request</code> met <code style="color:#845ef7">Deze ventilator past nog in een behuizing.</code>

> [!IMPORTANT]
> De database weigert zelf ook zo'n ventilator. Laat het niet zover komen: je API kijkt eerst, en antwoordt netjes.

---

### 7. Alles samen: een ventilator vervangen in alle behuizingen

<span style="background:#7048e8;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>SAMENGESTELDE OEFENING</b></span>

Vervangen werkte tot nu toe als verwijderen en toevoegen. Maar een ventilator met compatibiliteiten mag je niet meer verwijderen. Bij een vervanging nemen de compatibiliteiten mee: na de vervanging past de nieuwe ventilator in elke behuizing waar de oude paste, met hetzelfde aantal plaatsen.

<span style="background:#d9822b;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>PUT</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/ventilator/vervang/<span style="color:#e64980"><b>{id}</b></span></code>

De controles, de statuscodes en de historiek blijven zoals in 05_02. Faalt een controle, dan verandert er niets: geen nieuwe ventilator, geen verplaatste compatibiliteit, geen wijziging in de historiek.

#### Testscenario

Bouw je database opnieuw op, zodat ze exact de startgegevens bevat, en doorloop deze reeks. Bedragen vergelijk je als getal.

1. Koppel ventilator 1 aan behuizing 3, met 2 plaatsen. Je krijgt <code style="color:#37b24d;font-weight:600">201</code>, en de nieuwe compatibiliteit heeft Id 7.
2. Doe hetzelfde nog eens. Je krijgt <code style="color:#f08c00;font-weight:600">400</code> met <code style="color:#845ef7">Deze ventilator is al gekoppeld aan deze behuizing.</code>
3. Koppel ventilator 99 aan behuizing 1, met 1 plaats. Je krijgt <code style="color:#f08c00;font-weight:600">400</code> met <code style="color:#845ef7">We vonden geen ventilator met Id 99.</code>
4. Verwijder ventilator 4. Je krijgt <code style="color:#f08c00;font-weight:600">400</code> met <code style="color:#845ef7">Deze ventilator past nog in een behuizing.</code>
5. Vervang ventilator 1 door deze ventilator:

   ```json
   { "merk": "Noctua", "afmetingInMm": 120, "verlichting": "geen", "prijs": 32.9 }
   ```

   Je krijgt <code style="color:#37b24d;font-weight:600">201</code>, en de nieuwe ventilator heeft Id 5.
6. <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> api/behuizing/1/compatibiliteiten</code> geeft de compatibiliteiten 1 en 2, waarbij compatibiliteit 1 nu naar ventilator 5 wijst, met nog altijd 3 plaatsen.
7. De volle prijs van behuizing 1 is nu `148.5`, die van behuizing 3 `225.37`.
8. <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> api/ventilator/1</code> geeft <code style="color:#f08c00;font-weight:600">404</code>. De historiek bevat twee regels: `verwijderd` met ventilatorId 1, en `toegevoegd` met ventilatorId 5.
9. Vervang ventilator 2 door een ventilator van 92 mm, met verder geldige gegevens. Je krijgt <code style="color:#f08c00;font-weight:600">400</code>. Compatibiliteit 2 en 5 wijzen nog altijd naar ventilator 2, en de historiek bevat nog altijd twee regels.
10. Verwijder ventilator 5. Je krijgt <code style="color:#f08c00;font-weight:600">400</code> met <code style="color:#845ef7">Deze ventilator past nog in een behuizing.</code>
11. <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> api/behuizing/99/volprijs</code> geeft <code style="color:#f08c00;font-weight:600">404</code> met <code style="color:#845ef7">We vonden geen behuizing met Id 99.</code>
