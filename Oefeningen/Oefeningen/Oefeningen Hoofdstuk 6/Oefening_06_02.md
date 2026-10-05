# 06_02

<b>Hoofdstuk 06</b> &nbsp;·&nbsp; Een veel-op-veel-relatie &nbsp;·&nbsp; PC-onderdelen &nbsp;·&nbsp; niveau <span style="color:#f08c00">●●○</span> &nbsp;·&nbsp; ±2 uur

## Leerdoel

Na deze oefening kan je een veel-op-veel-relatie opbouwen met een tussentabel, en in die tussentabel ook zelf gegevens bewaren over de koppeling.

Je leert de twee één-op-veel-relaties van een tussentabel vastleggen met de Fluent API, en je startgegevens voor drie tabellen tegelijk via de Fluent API laten meekomen.

Daarnaast merk je dat een relatie meebepaalt in welke volgorde je API zijn werk mag doen.

## 

## Opdracht

Klanten vragen aan de balie geregeld welke ventilatoren in welke behuizing passen. Een behuizing heeft plaats voor meerdere ventilatoren, en één ventilator past in meerdere behuizingen. Per combinatie wil Bit & Byte ook weten hoeveel van die ventilatoren erin passen.

Jouw taak is om een model `Behuizing` toe te voegen, het via een tussentabel te verbinden met `Ventilator`, en de koppelingen via de API te beheren.

Via de API moet de inkoopafdeling, net als in 05_02, ventilatoren kunnen beheren en de historiek nalezen. Daarnaast moet ze:

1. alle behuizingen en één behuizing kunnen opvragen;
2. kunnen opvragen welke ventilatoren in een behuizing passen;
3. een nieuwe koppeling tussen een behuizing en een ventilator kunnen leggen;
4. erop kunnen rekenen dat een ventilator die nog in een behuizing past, niet zomaar verdwijnt.

Implementeer onderstaande functionaliteiten.

### 1. Je vertrekpunt

Vertrek van je eigen oplossing van 05_02. Alle endpoints uit die oefening blijven werken, met dezelfde controles en dezelfde berichten. Punt 6 en 7 voegen er een regel aan toe.

> [!NOTE]
> In deze oefening bouw je je database opnieuw op. Verwijder ze, en laat ze daarna volledig opbouwen door je migraties. Vanaf nu komen alle startgegevens via de Fluent API, zoals in les 06.

---

### 2. De modellen

Maak in de map Models een klasse `Behuizing` en een klasse `Compatibiliteit`. `Compatibiliteit` is de tussentabel: één rij zegt dat een bepaalde ventilator in een bepaalde behuizing past, en hoeveel keer.

| Behuizing          | C#-type                 | Kolom in de database                     |
|--------------------|-------------------------|------------------------------------------|
| Id                 | `int`                   | de sleutel, de database telt zelf op     |
| Naam               | `string`                | tekst van hoogstens 50 tekens, verplicht |
| Formaat            | `string`                | tekst van hoogstens 20 tekens, verplicht |
| Compatibiliteiten  | `List<Compatibiliteit>?` | navigation property, geen kolom          |

| Compatibiliteit | C#-type       | Kolom in de database                          |
|-----------------|---------------|-----------------------------------------------|
| Id              | `int`         | de sleutel, de database telt zelf op          |
| BehuizingId     | `int`         | vreemde sleutel naar `Behuizingen`, verplicht |
| Behuizing       | `Behuizing?`  | navigation property, geen kolom               |
| VentilatorId    | `int`         | vreemde sleutel naar `Ventilatoren`, verplicht |
| Ventilator      | `Ventilator?` | navigation property, geen kolom               |
| AantalPlaatsen  | `int`         | geheel getal, verplicht                       |

Voeg aan `Ventilator` een navigation property `Compatibiliteiten` toe, van het type `List<Compatibiliteit>?`. In de JSON van een ventilator zie je ze voortaan als `"compatibiliteiten": null`.

De tabellen heten `Behuizingen` en `Compatibiliteiten`. Voeg ze toe aan je `VentilatorContext`.

> [!NOTE]
> Let op het vraagteken bij de navigation properties: de client stuurt ze nooit mee. Scalar vult ze in zijn voorbeeld wel in. Haal ze daar weg voor je een verzoek verstuurt.

> [!NOTE]
> `Wijziging` krijgt geen relatie met `Ventilator`. Een wijziging moet blijven bestaan, ook als de ventilator al lang verwijderd is.

---

### 3. De relaties

Leg in `OnModelCreating` met de Fluent API de twee relaties van de tussentabel vast: een compatibiliteit hoort bij één behuizing en bij één ventilator, en een behuizing en een ventilator hebben elk veel compatibiliteiten.

Een behuizing of een ventilator waarnaar nog een compatibiliteit verwijst, mag niet verwijderd kunnen worden. Ook niet rechtstreeks in de database.

---

### 4. De startgegevens

Leg met de Fluent API deze startgegevens vast, in een aparte methode voor de startgegevens, zoals in les 06.

De vier ventilatoren uit 05_02, en deze behuizingen:

| Id | Naam         | Formaat    |
|----|--------------|------------|
| 1  | Silent Base  | midi tower |
| 2  | Airflow Mini | mini tower |
| 3  | Studio XL    | big tower  |

En deze compatibiliteiten:

| Id | BehuizingId | VentilatorId | AantalPlaatsen |
|----|-------------|--------------|----------------|
| 1  | 1           | 1            | 3              |
| 2  | 1           | 2            | 2              |
| 3  | 2           | 1            | 2              |
| 4  | 2           | 3            | 2              |
| 5  | 3           | 2            | 4              |
| 6  | 3           | 4            | 3              |

De historiek begint leeg.

Maak een migratie met de naam `Behuizingen` en bouw daarmee je database opnieuw op.

> [!WARNING]
> Startgegevens met een vaste Id vertellen de teller van PostgreSQL niet welke Id's al gebruikt zijn. Na het opbouwen van je database moet de eerste ventilator die je via de API toevoegt, Id 5 krijgen, en de eerste compatibiliteit Id 7. Je leerboek waarschuwt hiervoor bij de startgegevens. Kies een oplossing waarbij je startgegevens precies de Id's uit de tabel houden.

---

### 5. Behuizingen en compatibiliteiten opvragen

Maak de repositories en controllers die je nodig hebt, asynchroon zoals in les 05.

| Verzoek | Antwoord |
|---------|----------|
| <span style="background:#1c7ed6;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>GET</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/behuizing</code> | <code style="color:#37b24d;font-weight:600">200 OK</code> met alle behuizingen, op Id gesorteerd |
| <span style="background:#1c7ed6;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>GET</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/behuizing/<span style="color:#e64980"><b>{id}</b></span></code> | <code style="color:#37b24d;font-weight:600">200 OK</code> met de behuizing, of <code style="color:#f08c00;font-weight:600">404 Not Found</code> met <code style="color:#845ef7">We vonden geen behuizing met Id <span style="color:#e64980"><b>{id}</b></span>.</code> |
| <span style="background:#1c7ed6;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>GET</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/behuizing/<span style="color:#e64980"><b>{id}</b></span>/compatibiliteiten</code> | <code style="color:#37b24d;font-weight:600">200 OK</code> met de compatibiliteiten van die behuizing, op Id gesorteerd, of <code style="color:#f08c00;font-weight:600">404 Not Found</code> met hetzelfde bericht als hierboven |
| <span style="background:#1c7ed6;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>GET</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/compatibiliteit/<span style="color:#e64980"><b>{id}</b></span></code> | <code style="color:#37b24d;font-weight:600">200 OK</code> met de compatibiliteit, of <code style="color:#f08c00;font-weight:600">404 Not Found</code> met <code style="color:#845ef7">We vonden geen compatibiliteit met Id <span style="color:#e64980"><b>{id}</b></span>.</code> |

<code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> api/behuizing/2/compatibiliteiten</code> geeft als resultaat:

```json
[
  { "id": 3, "behuizingId": 2, "behuizing": null, "ventilatorId": 1, "ventilator": null, "aantalPlaatsen": 2 },
  { "id": 4, "behuizingId": 2, "behuizing": null, "ventilatorId": 3, "ventilator": null, "aantalPlaatsen": 2 }
]
```

> [!NOTE]
> De navigation properties zijn leeg in je JSON. Entity Framework Core haalt gerelateerde gegevens niet uit zichzelf op. Hoe je dat wél doet, zie je in les 08.

> [!WARNING]
> Krijg je een <code style="color:#f03e3e;font-weight:600">500</code> met een melding over een *object cycle*? Dan kennen twee objecten elkaar over en weer, zonder einde. Les 08 legt uit waarom. Voor nu: haal een object niet op als je enkel wil weten óf het bestaat.

---

### 6. Een koppeling leggen

<span style="background:#2f9e44;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>POST</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/compatibiliteit</code>

De client stuurt een `BehuizingId`, een `VentilatorId` en een `AantalPlaatsen`. Controleer in deze volgorde:

1. de behuizing bestaat niet: <code style="color:#f08c00;font-weight:600">400 Bad Request</code> met <code style="color:#845ef7">We vonden geen behuizing met Id <span style="color:#e64980"><b>{behuizingId}</b></span>.</code>
2. de ventilator bestaat niet: <code style="color:#f08c00;font-weight:600">400 Bad Request</code> met <code style="color:#845ef7">We vonden geen ventilator met Id <span style="color:#e64980"><b>{ventilatorId}</b></span>.</code>
3. het aantal plaatsen is kleiner dan 1: <code style="color:#f08c00;font-weight:600">400 Bad Request</code> met <code style="color:#845ef7">Het aantal plaatsen moet minstens 1 zijn.</code>
4. die ventilator is al aan die behuizing gekoppeld: <code style="color:#f08c00;font-weight:600">400 Bad Request</code> met <code style="color:#845ef7">Deze ventilator is al gekoppeld aan deze behuizing.</code>

Anders bewaar je de compatibiliteit en geef je <code style="color:#37b24d;font-weight:600">201 Created</code> terug, met de nieuwe compatibiliteit en het adres waarop ze opgevraagd kan worden.

> [!IMPORTANT]
> Een behuizing of ventilator die niet bestaat, is een fout in wat de client meestuurt, en geeft dus <code style="color:#f08c00;font-weight:600">400</code>. Laat het niet aan de database over om dat te merken: die antwoordt met een <code style="color:#f03e3e;font-weight:600">500</code>.

> [!TIP]
> De waarschuwing over een *object cycle* uit punt 5 geldt ook hier. Ook een toevoeging kan die opleveren, als je API daarvoor andere objecten heeft opgehaald.

Daarnaast verandert er één regel aan de ventilatoren:

- een ventilator verwijderen waarnaar nog een compatibiliteit verwijst: <code style="color:#f08c00;font-weight:600">400 Bad Request</code> met <code style="color:#845ef7">Deze ventilator past nog in een behuizing.</code>

Ook die weigering laat geen spoor na in de historiek.

---

### 7. Alles samen: een ventilator vervangen in alle behuizingen

<span style="background:#7048e8;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>SAMENGESTELDE OEFENING</b></span>

Vervangen bestond al sinds 02_02: een ventilator gaat weg, een nieuwe komt in de plaats. Maar nu verwijzen er compatibiliteiten naar de oude ventilator, en de relatie verbiedt dat je hem gewoon weghaalt. In dit laatste punt moet het vervangen dus slimmer worden.

<span style="background:#d9822b;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>PUT</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/ventilator/vervang/<span style="color:#e64980"><b>{id}</b></span></code>

Bij een geslaagde vervanging:

- komt de nieuwe ventilator erbij;
- gaan alle compatibiliteiten van de oude ventilator over naar de nieuwe. Het blijven dezelfde koppelingen, met hetzelfde Id, dezelfde behuizing en hetzelfde aantal plaatsen: enkel de ventilator verandert;
- verdwijnt de oude ventilator;
- krijgt de historiek, zoals vroeger, eerst `verwijderd` met het oude Id en dan `toegevoegd` met het nieuwe.

Mislukt de vervanging, met een <code style="color:#f08c00;font-weight:600">400</code> of een <code style="color:#f08c00;font-weight:600">404</code>, dan verandert er niets: niet aan de ventilatoren, niet aan de compatibiliteiten en niet aan de historiek.

#### Testscenario

Bouw je database opnieuw op, zodat ze exact de startgegevens bevat, en doorloop deze reeks:

1. Koppel ventilator 1 aan behuizing 3, met 2 plaatsen. Je krijgt <code style="color:#37b24d;font-weight:600">201</code>, en de nieuwe compatibiliteit heeft Id 7.
2. Doe hetzelfde nog eens. Je krijgt <code style="color:#f08c00;font-weight:600">400</code> met <code style="color:#845ef7">Deze ventilator is al gekoppeld aan deze behuizing.</code>
3. Koppel ventilator 99 aan behuizing 1, met 1 plaats. Je krijgt <code style="color:#f08c00;font-weight:600">400</code> met <code style="color:#845ef7">We vonden geen ventilator met Id 99.</code>
4. Verwijder ventilator 4. Je krijgt <code style="color:#f08c00;font-weight:600">400</code> met <code style="color:#845ef7">Deze ventilator past nog in een behuizing.</code>
5. Vervang ventilator 1 door deze ventilator. Je krijgt <code style="color:#37b24d;font-weight:600">201</code>, en de nieuwe ventilator heeft Id 5.

   ```json
   { "merk": "Noctua", "afmetingInMm": 120, "verlichting": "geen", "prijs": 31.90 }
   ```

6. <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> api/behuizing/1/compatibiliteiten</code> geeft als resultaat:

   ```json
   [
     { "id": 1, "behuizingId": 1, "behuizing": null, "ventilatorId": 5, "ventilator": null, "aantalPlaatsen": 3 },
     { "id": 2, "behuizingId": 1, "behuizing": null, "ventilatorId": 2, "ventilator": null, "aantalPlaatsen": 2 }
   ]
   ```

   Ook compatibiliteit 3 en 7 verwijzen nu naar ventilator 5.
7. <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> api/ventilator/1</code> geeft <code style="color:#f08c00;font-weight:600">404</code>. De historiek bevat twee regels: `verwijderd` met ventilatorId 1, en `toegevoegd` met ventilatorId 5.
8. Vervang ventilator 2 door een ventilator van 92 mm, met verder geldige gegevens. Je krijgt <code style="color:#f08c00;font-weight:600">400</code>. Compatibiliteit 2 en 5 verwijzen nog altijd naar ventilator 2, en de historiek bevat nog altijd twee regels.
9. Verwijder ventilator 5. Je krijgt <code style="color:#f08c00;font-weight:600">400</code> met <code style="color:#845ef7">Deze ventilator past nog in een behuizing.</code>

> [!WARNING]
> Krijg je bij stap 5 een <code style="color:#f03e3e;font-weight:600">500</code> over een vreemde sleutel? Dan probeerde je API de oude ventilator weg te halen terwijl er nog compatibiliteiten naar verwezen. Kijk naar de volgorde van je stappen.

> [!WARNING]
> Krijg je bij stap 5 een <code style="color:#f03e3e;font-weight:600">500</code> over een *object cycle*, terwijl de vervanging wel bewaard is? Dan kent de nieuwe ventilator ondertussen zijn compatibiliteiten, en die kennen hem weer. Denk na over welk object je in je antwoord terugstuurt.
