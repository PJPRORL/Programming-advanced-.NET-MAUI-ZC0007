# 06_05

<b>Hoofdstuk 06</b> &nbsp;·&nbsp; Een veel-op-veel-relatie &nbsp;·&nbsp; Fietsverhuur &nbsp;·&nbsp; niveau <span style="color:#f08c00">●●○</span> &nbsp;·&nbsp; ±2 uur

## Leerdoel

Na deze oefening kan je een veel-op-veel-relatie opbouwen met een tussentabel, en in die tussentabel ook zelf gegevens bewaren over de koppeling.

Je leert de twee één-op-veel-relaties van een tussentabel vastleggen met de Fluent API, en je startgegevens voor drie tabellen tegelijk via de Fluent API laten meekomen.

Daarnaast merk je dat een relatie meebepaalt in welke volgorde je API zijn werk mag doen.

## 

## Opdracht

Bij elke fiets van Trapdoor hoort een vaste uitrusting: een slot, een fietstas, soms een kinderzitje. Eén accessoire kan bij meerdere fietsen horen, en één fiets heeft meerdere accessoires. Per combinatie wil de balie ook weten hoeveel stuks erbij horen.

Jouw taak is om een model `Accessoire` toe te voegen, het via een tussentabel te verbinden met `Fiets`, en de koppelingen via de API te beheren.

Via de API moet de balie, net als in 05_05, fietsen kunnen beheren, verhuren en terugbrengen, en de verhuringen nalezen. Daarnaast moet ze:

1. alle accessoires en één accessoire kunnen opvragen;
2. kunnen opvragen welke accessoires bij een fiets horen;
3. een nieuwe koppeling tussen een fiets en een accessoire kunnen leggen;
4. erop kunnen rekenen dat een fiets met accessoires niet zomaar verdwijnt.

Implementeer onderstaande functionaliteiten.

### 1. Je vertrekpunt

Vertrek van je eigen oplossing van 05_05. Alle endpoints uit die oefening blijven werken, met dezelfde controles en dezelfde berichten. Punt 6 en 7 voegen er een regel aan toe.

> [!NOTE]
> In deze oefening bouw je je database opnieuw op. Verwijder ze, en laat ze daarna volledig opbouwen door je migraties. Vanaf nu komen alle startgegevens via de Fluent API, zoals in les 06.

---

### 2. De modellen

Maak in de map Models een klasse `Accessoire` en een klasse `FietsAccessoire`. `FietsAccessoire` is de tussentabel: één rij zegt dat een bepaald accessoire bij een bepaalde fiets hoort, en hoeveel stuks.

| Accessoire        | C#-type                 | Kolom in de database                     |
|-------------------|-------------------------|------------------------------------------|
| Id                | `int`                   | de sleutel, de database telt zelf op     |
| Naam              | `string`                | tekst van hoogstens 50 tekens, verplicht |
| FietsAccessoires  | `List<FietsAccessoire>?` | navigation property, geen kolom          |

| FietsAccessoire | C#-type       | Kolom in de database                          |
|-----------------|---------------|-----------------------------------------------|
| Id              | `int`         | de sleutel, de database telt zelf op          |
| FietsId         | `int`         | vreemde sleutel naar `Fietsen`, verplicht     |
| Fiets           | `Fiets?`      | navigation property, geen kolom               |
| AccessoireId    | `int`         | vreemde sleutel naar `Accessoires`, verplicht |
| Accessoire      | `Accessoire?` | navigation property, geen kolom               |
| Aantal          | `int`         | geheel getal, verplicht                       |

Voeg aan `Fiets` een navigation property `FietsAccessoires` toe, van het type `List<FietsAccessoire>?`. In de JSON van een fiets zie je ze voortaan als `"fietsAccessoires": null`.

De tabellen heten `Accessoires` en `FietsAccessoires`. Voeg ze toe aan je `FietsverhuurContext`.

> [!NOTE]
> Let op het vraagteken bij de navigation properties: de client stuurt ze nooit mee. Scalar vult ze in zijn voorbeeld wel in. Haal ze daar weg voor je een verzoek verstuurt.

> [!NOTE]
> `Verhuring` krijgt geen relatie met `Fiets`. Een verhuring moet blijven bestaan, ook als de fiets al lang uit de vloot is.

---

### 3. De relaties

Leg in `OnModelCreating` met de Fluent API de twee relaties van de tussentabel vast: een koppeling hoort bij één fiets en bij één accessoire, en een fiets en een accessoire hebben elk veel koppelingen.

Een fiets of een accessoire waarnaar nog een koppeling verwijst, mag niet verwijderd kunnen worden. Ook niet rechtstreeks in de database.

---

### 4. De startgegevens

Leg met de Fluent API deze startgegevens vast, in een aparte methode voor de startgegevens, zoals in les 06.

De vier fietsen uit 05_05, en deze accessoires:

| Id | Naam         |
|----|--------------|
| 1  | fietsslot    |
| 2  | helm         |
| 3  | kinderzitje  |
| 4  | fietstas     |

En deze koppelingen:

| Id | FietsId | AccessoireId | Aantal |
|----|---------|--------------|--------|
| 1  | 1       | 1            | 1      |
| 2  | 1       | 4            | 2      |
| 3  | 2       | 1            | 1      |
| 4  | 2       | 2            | 1      |
| 5  | 4       | 3            | 2      |
| 6  | 4       | 1            | 1      |

Fiets 3, de e-bike, heeft nog geen accessoires, en is nog altijd verhuurd. De verhuringen beginnen leeg.

Maak een migratie met de naam `Accessoires` en bouw daarmee je database opnieuw op.

> [!WARNING]
> Startgegevens met een vaste Id vertellen de teller van PostgreSQL niet welke Id's al gebruikt zijn. Na het opbouwen van je database moet de eerste fiets die je via de API toevoegt, Id 5 krijgen, en de eerste koppeling Id 7. Je leerboek waarschuwt hiervoor bij de startgegevens. Kies een oplossing waarbij je startgegevens precies de Id's uit de tabel houden.

---

### 5. Accessoires en koppelingen opvragen

Maak de repositories en controllers die je nodig hebt, asynchroon zoals in les 05.

| Verzoek | Antwoord |
|---------|----------|
| <span style="background:#1c7ed6;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>GET</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/accessoire</code> | <code style="color:#37b24d;font-weight:600">200 OK</code> met alle accessoires, op Id gesorteerd |
| <span style="background:#1c7ed6;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>GET</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/accessoire/<span style="color:#e64980"><b>{id}</b></span></code> | <code style="color:#37b24d;font-weight:600">200 OK</code> met het accessoire, of <code style="color:#f08c00;font-weight:600">404 Not Found</code> met <code style="color:#845ef7">We vonden geen accessoire met Id <span style="color:#e64980"><b>{id}</b></span>.</code> |
| <span style="background:#1c7ed6;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>GET</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/fiets/<span style="color:#e64980"><b>{id}</b></span>/accessoires</code> | <code style="color:#37b24d;font-weight:600">200 OK</code> met de koppelingen van die fiets, op Id gesorteerd, of <code style="color:#f08c00;font-weight:600">404 Not Found</code> met <code style="color:#845ef7">We vonden geen fiets met Id <span style="color:#e64980"><b>{id}</b></span>.</code> |
| <span style="background:#1c7ed6;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>GET</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/fietsaccessoire/<span style="color:#e64980"><b>{id}</b></span></code> | <code style="color:#37b24d;font-weight:600">200 OK</code> met de koppeling, of <code style="color:#f08c00;font-weight:600">404 Not Found</code> met <code style="color:#845ef7">We vonden geen koppeling met Id <span style="color:#e64980"><b>{id}</b></span>.</code> |

<code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> api/fiets/2/accessoires</code> geeft als resultaat:

```json
[
  { "id": 3, "fietsId": 2, "fiets": null, "accessoireId": 1, "accessoire": null, "aantal": 1 },
  { "id": 4, "fietsId": 2, "fiets": null, "accessoireId": 2, "accessoire": null, "aantal": 1 }
]
```

> [!NOTE]
> De navigation properties zijn leeg in je JSON. Entity Framework Core haalt gerelateerde gegevens niet uit zichzelf op. Hoe je dat wél doet, zie je in les 08.

> [!WARNING]
> Krijg je een <code style="color:#f03e3e;font-weight:600">500</code> met een melding over een *object cycle*? Dan kennen twee objecten elkaar over en weer, zonder einde. Les 08 legt uit waarom. Voor nu: haal een object niet op als je enkel wil weten óf het bestaat.

---

### 6. Een koppeling leggen

<span style="background:#2f9e44;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>POST</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/fietsaccessoire</code>

De client stuurt een `FietsId`, een `AccessoireId` en een `Aantal`. Controleer in deze volgorde:

1. de fiets bestaat niet: <code style="color:#f08c00;font-weight:600">400 Bad Request</code> met <code style="color:#845ef7">We vonden geen fiets met Id <span style="color:#e64980"><b>{fietsId}</b></span>.</code>
2. het accessoire bestaat niet: <code style="color:#f08c00;font-weight:600">400 Bad Request</code> met <code style="color:#845ef7">We vonden geen accessoire met Id <span style="color:#e64980"><b>{accessoireId}</b></span>.</code>
3. het aantal is kleiner dan 1: <code style="color:#f08c00;font-weight:600">400 Bad Request</code> met <code style="color:#845ef7">Het aantal moet minstens 1 zijn.</code>
4. dat accessoire hoort al bij die fiets: <code style="color:#f08c00;font-weight:600">400 Bad Request</code> met <code style="color:#845ef7">Dit accessoire hoort al bij deze fiets.</code>

Anders bewaar je de koppeling en geef je <code style="color:#37b24d;font-weight:600">201 Created</code> terug, met de nieuwe koppeling en het adres waarop ze opgevraagd kan worden.

> [!IMPORTANT]
> Een fiets die niet bestaat of een accessoire dat niet bestaat, is een fout in wat de client meestuurt, en geeft dus <code style="color:#f08c00;font-weight:600">400</code>. Laat het niet aan de database over om dat te merken: die antwoordt met een <code style="color:#f03e3e;font-weight:600">500</code>.

> [!TIP]
> De waarschuwing over een *object cycle* uit punt 5 geldt ook hier. Ook een toevoeging kan die opleveren, als je API daarvoor andere objecten heeft opgehaald.

Daarnaast verandert er één regel aan de fietsen:

- een fiets verwijderen waarnaar nog een koppeling verwijst: <code style="color:#f08c00;font-weight:600">400 Bad Request</code> met <code style="color:#845ef7">Deze fiets heeft nog accessoires.</code>

Bij een verwijdering geldt deze volgorde: eerst of de fiets bestaat, dan of ze verhuurd is, en dan of ze nog accessoires heeft.

---

### 7. Alles samen: een fiets vervangen met haar uitrusting

<span style="background:#7048e8;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>SAMENGESTELDE OEFENING</b></span>

Vervangen bestond al sinds 02_05: een fiets gaat uit de vloot, een nieuwe komt in de plaats. Maar nu verwijzen er koppelingen naar de oude fiets, en de relatie verbiedt dat je de oude fiets gewoon weghaalt. In dit laatste punt moet het vervangen dus slimmer worden.

<span style="background:#d9822b;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>PUT</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/fiets/vervang/<span style="color:#e64980"><b>{id}</b></span></code>

Bij een geslaagde vervanging:

- komt de nieuwe fiets erbij, beschikbaar zoals altijd;
- gaan alle koppelingen van de oude fiets over naar de nieuwe. Het blijven dezelfde koppelingen, met hetzelfde Id, hetzelfde accessoire en hetzelfde aantal: enkel de fiets verandert;
- verdwijnt de oude fiets.

Een verhuurde fiets kan je nog altijd niet vervangen, zoals in 03_05. Mislukt de vervanging, met een <code style="color:#f08c00;font-weight:600">400</code> of een <code style="color:#f08c00;font-weight:600">404</code>, dan verandert er niets: niet aan de fietsen, niet aan de koppelingen en niet aan de verhuringen.

#### Testscenario

Bouw je database opnieuw op, zodat ze exact de startgegevens bevat, en doorloop deze reeks:

1. Koppel accessoire 2 aan fiets 3, met aantal 1. Je krijgt <code style="color:#37b24d;font-weight:600">201</code>, en de nieuwe koppeling heeft Id 7.
2. Doe hetzelfde nog eens. Je krijgt <code style="color:#f08c00;font-weight:600">400</code> met <code style="color:#845ef7">Dit accessoire hoort al bij deze fiets.</code>
3. Koppel accessoire 99 aan fiets 1, met aantal 1. Je krijgt <code style="color:#f08c00;font-weight:600">400</code> met <code style="color:#845ef7">We vonden geen accessoire met Id 99.</code>
4. Verwijder fiets 2. Je krijgt <code style="color:#f08c00;font-weight:600">400</code> met <code style="color:#845ef7">Deze fiets heeft nog accessoires.</code>
5. Vervang fiets 3 door een geldige nieuwe fiets. Je krijgt <code style="color:#f08c00;font-weight:600">400</code> met <code style="color:#845ef7">Een verhuurde fiets kan je niet uit de vloot halen.</code>
6. Vervang fiets 1 door deze fiets. Je krijgt <code style="color:#37b24d;font-weight:600">201</code>, en de nieuwe fiets heeft Id 5 en is beschikbaar.

   ```json
   { "type": "stadsfiets", "framemaat": 56, "prijsPerDag": 13.50 }
   ```

7. <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> api/fiets/5/accessoires</code> geeft als resultaat:

   ```json
   [
     { "id": 1, "fietsId": 5, "fiets": null, "accessoireId": 1, "accessoire": null, "aantal": 1 },
     { "id": 2, "fietsId": 5, "fiets": null, "accessoireId": 4, "accessoire": null, "aantal": 2 }
   ]
   ```

8. <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> api/fiets/1</code> geeft <code style="color:#f08c00;font-weight:600">404</code>. De verhuringen zijn nog altijd leeg.
9. Verwijder fiets 5. Je krijgt <code style="color:#f08c00;font-weight:600">400</code> met <code style="color:#845ef7">Deze fiets heeft nog accessoires.</code>

> [!WARNING]
> Krijg je bij stap 6 een <code style="color:#f03e3e;font-weight:600">500</code> over een vreemde sleutel? Dan probeerde je API de oude fiets weg te halen terwijl er nog koppelingen naar verwezen. Kijk naar de volgorde van je stappen.

> [!WARNING]
> Krijg je bij stap 6 een <code style="color:#f03e3e;font-weight:600">500</code> over een *object cycle*, terwijl de vervanging wel bewaard is? Dan kent de nieuwe fiets ondertussen haar koppelingen, en die kennen haar weer. Denk na over welk object je in je antwoord terugstuurt.
