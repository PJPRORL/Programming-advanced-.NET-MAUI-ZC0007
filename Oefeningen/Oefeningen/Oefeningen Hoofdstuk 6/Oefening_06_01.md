# 06_01

<b>Hoofdstuk 06</b> &nbsp;·&nbsp; Een één-op-veel-relatie &nbsp;·&nbsp; PC-onderdelen &nbsp;·&nbsp; niveau <span style="color:#37b24d">●○○</span> &nbsp;·&nbsp; ±90 min

## Leerdoel

Na deze oefening kan je twee modellen met elkaar verbinden in een één-op-veel-relatie, met een vreemde sleutel en navigation properties.

Je leert de relatie vastleggen met de Fluent API, kiezen wat er gebeurt als iemand de "één"-kant wil verwijderen, en je startgegevens via de Fluent API laten meekomen met je migraties.

Daarnaast merk je dat de database een relatie zelf bewaakt: wat de relatie verbiedt, moet je API vooraf netjes weigeren.

## 

## Opdracht

Bit & Byte koopt zijn artikelen bij een handvol leveranciers. Tot nu toe stond nergens bij wie. Elk artikel komt voortaan van precies één leverancier, en één leverancier levert meerdere artikelen.

Jouw taak is om een model `Leverancier` toe te voegen, het te verbinden met `Artikel`, en de leveranciers via de API beschikbaar te maken.

Via de API moet de website, net als in 05_01, alle artikelen kunnen opvragen, filteren en de fiche tonen. Daarnaast moet ze:

1. alle leveranciers en één leverancier kunnen opvragen;
2. alle artikelen van één leverancier kunnen opvragen;
3. een leverancier kunnen toevoegen;
4. een leverancier kunnen verwijderen, zolang die geen artikelen meer levert.

Implementeer onderstaande functionaliteiten.

### 1. Je vertrekpunt

Vertrek van je eigen oplossing van 05_01. Alle endpoints uit die oefening blijven werken. Elk artikel krijgt er in deze oefening wel twee gegevens bij, en die zie je ook in de JSON.

> [!NOTE]
> In deze oefening bouw je je database opnieuw op. Verwijder ze, en laat ze daarna volledig opbouwen door je migraties. Vanaf nu komen alle startgegevens via de Fluent API, zoals in les 06. Je SQL-scripts uit les 04 heb je niet meer nodig.

---

### 2. De modellen

Maak in de map Models een klasse `Leverancier`:

| Property  | C#-type          | Kolom in de database                     |
|-----------|------------------|------------------------------------------|
| Id        | `int`            | de sleutel, de database telt zelf op     |
| Naam      | `string`         | tekst van hoogstens 50 tekens, verplicht |
| Land      | `string`         | tekst van hoogstens 30 tekens, verplicht |
| Artikelen | `List<Artikel>?` | navigation property, geen kolom          |

Voeg aan `Artikel` toe:

| Property      | C#-type        | Kolom in de database                        |
|---------------|----------------|---------------------------------------------|
| LeverancierId | `int`          | vreemde sleutel naar `Leveranciers`, verplicht |
| Leverancier   | `Leverancier?` | navigation property, geen kolom             |

De tabel heet `Leveranciers`. Voeg ze toe aan je `ArtikelContext`.

> [!NOTE]
> Let op het vraagteken bij de navigation properties: de client stuurt ze nooit mee. Scalar vult ze in zijn voorbeeld wel in. Haal ze daar weg voor je een verzoek verstuurt.

> [!NOTE]
> De maximale lengtes leg je in deze oefening enkel in je database vast. Een controle in je API vraagt ze niet: de testen sturen enkel teksten die passen.

---

### 3. De relatie

Leg in `OnModelCreating` met de Fluent API de relatie vast: een artikel heeft één leverancier, een leverancier heeft veel artikelen, en `LeverancierId` is de vreemde sleutel.

Een leverancier die nog artikelen levert, mag niet verwijderd kunnen worden. Ook niet rechtstreeks in de database.

> [!WARNING]
> Lees in les 06 wat Entity Framework Core uit zichzelf doet als je een leverancier verwijdert, en wat er dan met zijn artikelen gebeurt. Dat is niet wat Bit & Byte wil.

---

### 4. De startgegevens

Leg met de Fluent API deze startgegevens vast, zodat ze met je migratie in de database komen:

| Id | Naam            | Land      |
|----|-----------------|-----------|
| 1  | TechDistri      | België    |
| 2  | Componentenhuis | Nederland |
| 3  | ChipCentraal    | Duitsland |
| 4  | Koelwerk        | België    |

De zes artikelen uit 05_01 komen er ook in, met deze leverancier:

| Artikel | LeverancierId |
|---------|---------------|
| 1, 5    | 1             |
| 2, 6    | 2             |
| 3, 4    | 3             |

Leverancier 4 levert nog niets.

> [!TIP]
> Les 06 verdeelt `OnModelCreating` in twee methodes: één die de tabellen opbouwt en één die de startgegevens toevoegt. Doe dat ook. Zo blijft je context leesbaar, nu er een tweede tabel bij komt.

Maak een migratie met de naam `Leveranciers` en bouw daarmee je database opnieuw op.

> [!WARNING]
> Startgegevens met een vaste Id vertellen de teller van PostgreSQL niet welke Id's al gebruikt zijn. Na het opbouwen van je database moet de eerste leverancier die je via de API toevoegt, Id 5 krijgen. Je leerboek waarschuwt hiervoor bij de startgegevens. Kies een oplossing waarbij je startgegevens precies de Id's uit de tabel houden.

---

### 5. Leveranciers opvragen

Maak een interface `ILeverancierRepository` en een klasse `LeverancierRepository`, asynchroon zoals in les 05, en een `LeverancierController` die luistert op <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/leverancier</code>.

| Verzoek | Antwoord |
|---------|----------|
| <span style="background:#1c7ed6;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>GET</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/leverancier</code> | <code style="color:#37b24d;font-weight:600">200 OK</code> met alle leveranciers, op Id gesorteerd |
| <span style="background:#1c7ed6;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>GET</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/leverancier/<span style="color:#e64980"><b>{id}</b></span></code> | <code style="color:#37b24d;font-weight:600">200 OK</code> met de leverancier, of <code style="color:#f08c00;font-weight:600">404 Not Found</code> met <code style="color:#845ef7">We vonden geen leverancier met Id <span style="color:#e64980"><b>{id}</b></span>.</code> |
| <span style="background:#1c7ed6;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>GET</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/leverancier/<span style="color:#e64980"><b>{id}</b></span>/artikelen</code> | <code style="color:#37b24d;font-weight:600">200 OK</code> met de artikelen van die leverancier, op Id gesorteerd, of <code style="color:#f08c00;font-weight:600">404 Not Found</code> met hetzelfde bericht als hierboven |

Een leverancier zonder artikelen geeft bij het derde endpoint een lege lijst, met <code style="color:#37b24d;font-weight:600">200 OK</code>.

<code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> api/leverancier/3</code> geeft als resultaat:

```json
{
  "id": 3,
  "naam": "ChipCentraal",
  "land": "Duitsland",
  "artikelen": null
}
```

<code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> api/artikel/4</code> geeft nu als resultaat:

```json
{
  "id": 4,
  "naam": "Ryzen 7 7800X3D",
  "merk": "AMD",
  "soort": "processor",
  "prijs": 389.00,
  "aantalOpVoorraad": 0,
  "garantiejaren": 3,
  "leverancierId": 3,
  "leverancier": null
}
```

> [!NOTE]
> De navigation properties zijn leeg in je JSON. Entity Framework Core haalt gerelateerde gegevens niet uit zichzelf op. Hoe je dat wél doet, zie je in les 08.

> [!WARNING]
> Krijg je bij <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> api/leverancier/3/artikelen</code> een <code style="color:#f03e3e;font-weight:600">500</code> met een melding over een *object cycle*? Dan kent elk artikel zijn leverancier, en kent die leverancier zijn artikelen weer, zonder einde. Les 08 legt uit waarom. Voor nu: haal een object niet op als je enkel wil weten óf het bestaat.

---

### 6. Een leverancier toevoegen

<span style="background:#2f9e44;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>POST</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/leverancier</code>

Het Id bepaalt de database. Slaagt de toevoeging, geef dan <code style="color:#37b24d;font-weight:600">201 Created</code> terug, met de nieuwe leverancier en het adres waarop ze opgevraagd kan worden.

Is de naam leeg, geef dan <code style="color:#f08c00;font-weight:600">400 Bad Request</code> met <code style="color:#845ef7">Een leverancier moet een naam hebben.</code>

---

### 7. Alles samen: een leverancier verwijderen

<span style="background:#7048e8;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>SAMENGESTELDE OEFENING</b></span>

De relatie, de keuze bij het verwijderen, de startgegevens en de repository komen samen in één endpoint: een leverancier verwijderen. Het mag enkel lukken als niets in de database er nog naar verwijst.

<span style="background:#e03131;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>DELETE</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/leverancier/<span style="color:#e64980"><b>{id}</b></span></code>

- De leverancier bestaat niet: <code style="color:#f08c00;font-weight:600">404 Not Found</code> met <code style="color:#845ef7">We vonden geen leverancier met Id <span style="color:#e64980"><b>{id}</b></span>.</code>
- De leverancier levert nog artikelen: <code style="color:#f08c00;font-weight:600">400 Bad Request</code> met <code style="color:#845ef7">Deze leverancier levert nog artikelen.</code>
- Anders: verwijder de leverancier en geef <code style="color:#37b24d;font-weight:600">204 No Content</code> terug.

> [!IMPORTANT]
> De database weigert zelf ook een leverancier met artikelen, dankzij je keuze in punt 3. Maar laat het niet zover komen: dan krijgt de client een <code style="color:#f03e3e;font-weight:600">500</code>. Je API kijkt eerst, en antwoordt netjes.

#### Testscenario

Bouw je database opnieuw op, zodat ze exact de startgegevens bevat, en doorloop deze reeks:

1. Verwijder leverancier 3. Je krijgt <code style="color:#f08c00;font-weight:600">400</code> met het bericht hierboven. <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> api/artikel/soort/processor</code> geeft nog altijd de artikelen 3 en 4.
2. Probeer in pgAdmin of in VS Code leverancier 3 rechtstreeks uit de tabel te verwijderen. De database weigert, met een melding over de vreemde sleutel.
3. Verwijder leverancier 4. Je krijgt <code style="color:#37b24d;font-weight:600">204</code>. Doe het nog eens: nu krijg je <code style="color:#f08c00;font-weight:600">404</code>.
4. Voeg een leverancier toe met de naam `Kabelkoning` en als land `België`. Je krijgt <code style="color:#37b24d;font-weight:600">201</code>, en de nieuwe leverancier heeft Id 5. De header `Location` eindigt op `/api/leverancier/5`; hoofdletters tellen daarbij niet.
5. <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> api/leverancier</code> geeft de leveranciers 1, 2, 3 en 5, in die volgorde.
6. <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> api/leverancier/5/artikelen</code> geeft `[]`.
7. <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> api/leverancier/99/artikelen</code> geeft <code style="color:#f08c00;font-weight:600">404</code> met <code style="color:#845ef7">We vonden geen leverancier met Id 99.</code>
