# 06_04

<b>Hoofdstuk 06</b> &nbsp;·&nbsp; Een één-op-veel-relatie &nbsp;·&nbsp; Dierenasiel &nbsp;·&nbsp; niveau <span style="color:#37b24d">●○○</span> &nbsp;·&nbsp; ±90 min

## Leerdoel

Na deze oefening kan je twee modellen met elkaar verbinden in een één-op-veel-relatie, met een vreemde sleutel en navigation properties.

Je leert de relatie vastleggen met de Fluent API, kiezen wat er gebeurt als iemand de "één"-kant wil verwijderen, en je startgegevens via de Fluent API laten meekomen met je migraties.

Daarnaast merk je dat de database een relatie zelf bewaakt: wat de relatie verbiedt, moet je API vooraf netjes weigeren.

## 

## Opdracht

Knuffelhof hield tot nu toe enkel een nummer bij voor het verblijf van elk dier. Wat achter dat nummer zat, wist de API niet. Een verblijf wordt nu een eigen model: elk dier woont in precies één verblijf, en één verblijf kan meerdere dieren huisvesten.

Jouw taak is om een model `Verblijf` toe te voegen, het te verbinden met `Dier`, en de verblijven via de API beschikbaar te maken.

Via de API moet de website, net als in 05_04, alle dieren kunnen opvragen, filteren, zoeken en de fiche tonen. Daarnaast moet ze:

1. alle verblijven en één verblijf kunnen opvragen;
2. alle dieren van één verblijf kunnen opvragen;
3. een verblijf kunnen toevoegen;
4. een verblijf kunnen verwijderen, zolang het leeg is.

Implementeer onderstaande functionaliteiten.

### 1. Je vertrekpunt

Vertrek van je eigen oplossing van 05_04. Alle endpoints uit die oefening blijven werken. Elk dier verandert in deze oefening wel een beetje, en dat zie je ook in de JSON.

> [!NOTE]
> In deze oefening bouw je je database opnieuw op. Verwijder ze, en laat ze daarna volledig opbouwen door je migraties. Vanaf nu komen alle startgegevens via de Fluent API, zoals in les 06. Je SQL-scripts uit les 04 heb je niet meer nodig.

---

### 2. De modellen

Maak in de map Models een klasse `Verblijf`:

| Property   | C#-type      | Kolom in de database                     |
|------------|--------------|------------------------------------------|
| Id         | `int`        | de sleutel, de database telt zelf op     |
| Naam       | `string`     | tekst van hoogstens 30 tekens, verplicht |
| Capaciteit | `int`        | geheel getal, verplicht                  |
| Dieren     | `List<Dier>?` | navigation property, geen kolom          |

Pas `Dier` aan: de property `Verblijfsnummer` verdwijnt, en in de plaats komen:

| Property  | C#-type    | Kolom in de database                         |
|-----------|------------|----------------------------------------------|
| VerblijfId | `int`     | vreemde sleutel naar `Verblijven`, verplicht |
| Verblijf  | `Verblijf?` | navigation property, geen kolom             |

De tabel heet `Verblijven`. Voeg ze toe aan je `AsielContext`.

> [!NOTE]
> Let op het vraagteken bij de navigation properties: de client stuurt ze nooit mee. Scalar vult ze in zijn voorbeeld wel in. Haal ze daar weg voor je een verzoek verstuurt.

> [!NOTE]
> De maximale lengtes leg je in deze oefening enkel in je database vast. Een controle in je API vraagt ze niet: de testen sturen enkel teksten die passen.

---

### 3. De relatie

Leg in `OnModelCreating` met de Fluent API de relatie vast: een dier heeft één verblijf, een verblijf heeft veel dieren, en `VerblijfId` is de vreemde sleutel.

Een verblijf waarin nog dieren wonen, mag niet verwijderd kunnen worden. Ook niet rechtstreeks in de database.

> [!WARNING]
> Lees in les 06 wat Entity Framework Core uit zichzelf doet als je een verblijf verwijdert, en wat er dan met zijn dieren gebeurt. Dat is niet wat Knuffelhof wil.

---

### 4. De startgegevens

Leg met de Fluent API deze startgegevens vast, zodat ze met je migratie in de database komen:

| Id | Naam          | Capaciteit |
|----|---------------|------------|
| 1  | Kattenkamer   | 6          |
| 2  | Hondenren     | 4          |
| 3  | Konijnenhok   | 5          |
| 4  | Vogelkooi     | 10         |
| 5  | Quarantaine   | 2          |

De zes dieren uit 05_04 komen er ook in, met dit verblijf:

| Dier | VerblijfId |
|------|------------|
| 1, 2 | 1          |
| 3, 4 | 2          |
| 5    | 3          |
| 6    | 4          |

Verblijf 5 is leeg.

> [!TIP]
> Les 06 verdeelt `OnModelCreating` in twee methodes: één die de tabellen opbouwt en één die de startgegevens toevoegt. Doe dat ook. Zo blijft je context leesbaar, nu er een tweede tabel bij komt.

Maak een migratie met de naam `Verblijven` en bouw daarmee je database opnieuw op.

> [!WARNING]
> Startgegevens met een vaste Id vertellen de teller van PostgreSQL niet welke Id's al gebruikt zijn. Na het opbouwen van je database moet het eerste verblijf dat je via de API toevoegt, Id 6 krijgen. Je leerboek waarschuwt hiervoor bij de startgegevens. Kies een oplossing waarbij je startgegevens precies de Id's uit de tabel houden.

---

### 5. Verblijven opvragen

Maak een interface `IVerblijfRepository` en een klasse `VerblijfRepository`, asynchroon zoals in les 05, en een `VerblijfController` die luistert op <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/verblijf</code>.

| Verzoek | Antwoord |
|---------|----------|
| <span style="background:#1c7ed6;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>GET</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/verblijf</code> | <code style="color:#37b24d;font-weight:600">200 OK</code> met alle verblijven, op Id gesorteerd |
| <span style="background:#1c7ed6;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>GET</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/verblijf/<span style="color:#e64980"><b>{id}</b></span></code> | <code style="color:#37b24d;font-weight:600">200 OK</code> met het verblijf, of <code style="color:#f08c00;font-weight:600">404 Not Found</code> met <code style="color:#845ef7">We vonden geen verblijf met Id <span style="color:#e64980"><b>{id}</b></span>.</code> |
| <span style="background:#1c7ed6;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>GET</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/verblijf/<span style="color:#e64980"><b>{id}</b></span>/dieren</code> | <code style="color:#37b24d;font-weight:600">200 OK</code> met de dieren in dat verblijf, op Id gesorteerd, of <code style="color:#f08c00;font-weight:600">404 Not Found</code> met hetzelfde bericht als hierboven |

Een leeg verblijf geeft bij het derde endpoint een lege lijst, met <code style="color:#37b24d;font-weight:600">200 OK</code>.

<code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> api/verblijf/2</code> geeft als resultaat:

```json
{
  "id": 2,
  "naam": "Hondenren",
  "capaciteit": 4,
  "dieren": null
}
```

<code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> api/dier/4</code> geeft nu als resultaat:

```json
{
  "id": 4,
  "naam": "Bella",
  "soort": "hond",
  "leeftijdInJaren": 1,
  "isGechipt": true,
  "verblijfId": 2,
  "verblijf": null
}
```

> [!NOTE]
> De navigation properties zijn leeg in je JSON. Entity Framework Core haalt gerelateerde gegevens niet uit zichzelf op. Hoe je dat wél doet, zie je in les 08.

> [!WARNING]
> Krijg je bij <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> api/verblijf/2/dieren</code> een <code style="color:#f03e3e;font-weight:600">500</code> met een melding over een *object cycle*? Dan kent elk dier zijn verblijf, en kent dat verblijf zijn dieren weer, zonder einde. Les 08 legt uit waarom. Voor nu: haal een object niet op als je enkel wil weten óf het bestaat.

---

### 6. Een verblijf toevoegen

<span style="background:#2f9e44;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>POST</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/verblijf</code>

Het Id bepaalt de database. Slaagt de toevoeging, geef dan <code style="color:#37b24d;font-weight:600">201 Created</code> terug, met het nieuwe verblijf en het adres waarop het opgevraagd kan worden.

Is de naam leeg, geef dan <code style="color:#f08c00;font-weight:600">400 Bad Request</code> met <code style="color:#845ef7">Een verblijf moet een naam hebben.</code>

---

### 7. Alles samen: een verblijf verwijderen

<span style="background:#7048e8;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>SAMENGESTELDE OEFENING</b></span>

De relatie, de keuze bij het verwijderen, de startgegevens en de repository komen samen in één endpoint: een verblijf verwijderen. Het mag enkel lukken als er geen dier meer in woont.

<span style="background:#e03131;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>DELETE</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/verblijf/<span style="color:#e64980"><b>{id}</b></span></code>

- Het verblijf bestaat niet: <code style="color:#f08c00;font-weight:600">404 Not Found</code> met <code style="color:#845ef7">We vonden geen verblijf met Id <span style="color:#e64980"><b>{id}</b></span>.</code>
- Er wonen nog dieren in: <code style="color:#f08c00;font-weight:600">400 Bad Request</code> met <code style="color:#845ef7">Dit verblijf is niet leeg.</code>
- Anders: verwijder het verblijf en geef <code style="color:#37b24d;font-weight:600">204 No Content</code> terug.

> [!IMPORTANT]
> De database weigert zelf ook een verblijf met dieren, dankzij je keuze in punt 3. Maar laat het niet zover komen: dan krijgt de website een <code style="color:#f03e3e;font-weight:600">500</code>. Je API kijkt eerst, en antwoordt netjes.

#### Testscenario

Bouw je database opnieuw op, zodat ze exact de startgegevens bevat, en doorloop deze reeks:

1. Verwijder verblijf 1. Je krijgt <code style="color:#f08c00;font-weight:600">400</code> met het bericht hierboven. <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> api/dier/soort/kat</code> geeft nog altijd de dieren 1 en 2.
2. Probeer in pgAdmin of in VS Code verblijf 1 rechtstreeks uit de tabel te verwijderen. De database weigert, met een melding over de vreemde sleutel.
3. Verwijder verblijf 5. Je krijgt <code style="color:#37b24d;font-weight:600">204</code>. Doe het nog eens: nu krijg je <code style="color:#f08c00;font-weight:600">404</code>.
4. Voeg een verblijf toe met de naam `Opvangkamer` en een capaciteit van 3. Je krijgt <code style="color:#37b24d;font-weight:600">201</code>, en het nieuwe verblijf heeft Id 6. De header `Location` eindigt op `/api/verblijf/6`; hoofdletters tellen daarbij niet.
5. <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> api/verblijf</code> geeft de verblijven 1, 2, 3, 4 en 6, in die volgorde.
6. <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> api/verblijf/6/dieren</code> geeft `[]`.
7. <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> api/verblijf/99/dieren</code> geeft <code style="color:#f08c00;font-weight:600">404</code> met <code style="color:#845ef7">We vonden geen verblijf met Id 99.</code>
