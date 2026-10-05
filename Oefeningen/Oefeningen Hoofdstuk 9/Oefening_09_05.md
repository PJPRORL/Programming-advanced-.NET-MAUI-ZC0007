# 09_05

<b>Hoofdstuk 09</b> &nbsp;·&nbsp; DTO's, Mapster en een tussentabel platslaan &nbsp;·&nbsp; Fietsverhuur &nbsp;·&nbsp; niveau <span style="color:#f08c00">●●○</span> &nbsp;·&nbsp; ±2,5 uur

## Leerdoel

Na deze oefening kan je vier modellen afschermen met read- en write-DTO's, en een tussentabel in je JSON laten verdwijnen: de client ziet fietsen en accessoires, geen koppelingen.

Je leert in je `MapperProfile` een eigen mapping schrijven voor properties die niet dezelfde naam hebben of een niveau dieper zitten, zoals les 09 dat doet met een orderlijn en haar product.

Daarnaast merk je dat Mapster zwijgt als het iets niet kan mappen: een veld dat leeg blijft, geeft geen foutmelding.

## 

## Opdracht

De balie leest vandaag koppelingen met daarin een fiets met daarin een lijst met `[null]`. Ze wil gewoon zien: bij deze fiets horen deze accessoires, zoveel stuks.

Jouw taak is om elk endpoint om te bouwen naar DTO's, met Mapster, en de details-endpoints uit 08_05 een eigen, platte vorm te geven.

Via de API moet de balie, net als in 08_05, fietsen kunnen beheren, vervangen, verhuren en terugbrengen, accessoires koppelen en uit het assortiment halen, de verhuringen nalezen en de details tonen. Daarnaast moet ze:

1. bij een fiets meteen zien hoeveel stuks uitrusting erbij horen.

Implementeer onderstaande functionaliteiten.

### 1. Je vertrekpunt

Vertrek van je eigen oplossing van 08_05. Alle endpoints blijven werken, met dezelfde routes, dezelfde controles en dezelfde berichten. Enkel hun JSON verandert, zoals hieronder beschreven.

Je modellen en je database veranderen in deze oefening niet. De startgegevens blijven die van 06_05.

Haal de instelling `IgnoreCycles` uit Program.cs weg, zoals les 09 vraagt.

---

### 2. Mapster en de structuur

Installeer Mapster in je project. Maak een map `DTOs`, met daarin een map per model, en een map `Configuration` met een klasse `MapperProfile`, zoals in les 09. Zorg dat Mapster je `MapperProfile` vindt bij het opstarten.

Of je DTO's klassen of records zijn, kies je zelf. Kies je voor records, denk dan na over hoe je de berekende velden invult: een record pas je na het aanmaken niet zomaar meer aan.

> [!IMPORTANT]
> Een DTO bevat nooit een model, en een model nooit een DTO. Een DTO mag wel een andere DTO bevatten, zolang die niet terugverwijst.

---

### 3. De read-DTO's

Elk endpoint dat vroeger een model teruggaf, geeft nu een read-DTO terug. Die heeft dezelfde properties als het model, **zonder de navigation properties**. Dat geldt voor alle vier de modellen, ook voor de verhuringen.

<code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> api/fietsaccessoire/5</code> geeft nu als resultaat:

```json
{ "id": 5, "fietsId": 4, "accessoireId": 3, "aantal": 2 }
```

---

### 4. De write-DTO's

De client stuurt voortaan enkel nog wat hij mag invullen:

| Verzoek | Velden in de body |
|---------|-------------------|
| <span style="background:#2f9e44;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>POST</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/fiets</code>, <span style="background:#d9822b;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>PUT</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/fiets/<span style="color:#e64980"><b>{id}</b></span></code> en <span style="background:#d9822b;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>PUT</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/fiets/vervang/<span style="color:#e64980"><b>{id}</b></span></code> | `type`, `framemaat`, `prijsPerDag` |
| <span style="background:#2f9e44;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>POST</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/fietsaccessoire</code> | `fietsId`, `accessoireId`, `aantal` |

Al de rest in de body negeert je API, gewoon omdat de DTO het niet kent. De controles, de berichten en de regels over beschikbaarheid blijven dezelfde: een nieuwe fiets is altijd beschikbaar, en bijwerken verandert dat nooit.

> [!TIP]
> Een DTO om bij te werken bevat minder dan je model. Vergelijk na een <span style="background:#d9822b;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>PUT</b></span> elk veld met de toestand ervoor, ook de velden die niet in je DTO staan.

> [!WARNING]
> Les 09 vermeldt data annotations op write-DTO's. Gebruik ze hier niet voor je controles: ze geven een eigen <code style="color:#f08c00;font-weight:600">400</code>, met een ander bericht dan je API nu belooft.

---

### 5. De details, zonder tussentabel

<span style="background:#1c7ed6;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>GET</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/fietsaccessoire/<span style="color:#e64980"><b>{id}</b></span>/details</code> geeft de koppeling plat in één object:

```json
{
  "id": 5,
  "aantal": 2,
  "fietsId": 4,
  "fietsType": "bakfiets",
  "accessoireId": 3,
  "accessoireNaam": "kinderzitje"
}
```

<span style="background:#1c7ed6;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>GET</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/accessoire/<span style="color:#e64980"><b>{id}</b></span>/details</code> geeft het accessoire met de fietsen waar het bij hoort. Elke koppeling wordt één regel, met de gegevens van de fiets:

```json
{
  "id": 1,
  "naam": "fietsslot",
  "fietsen": [
    { "fietsId": 1, "type": "stadsfiets", "framemaat": 54, "aantal": 1 },
    { "fietsId": 2, "type": "mountainbike", "framemaat": 48, "aantal": 1 },
    { "fietsId": 4, "type": "bakfiets", "framemaat": 52, "aantal": 1 }
  ]
}
```

De volgorde binnen zo'n lijst ligt niet vast. De berichten bij een <code style="color:#f08c00;font-weight:600">404</code> blijven die van 08_05.

> [!WARNING]
> Blijft een veld leeg, of staat er `0` waar je een waarde verwachtte? Mapster geeft geen foutmelding als het een property niet kan vullen. Kijk of de naam en het type overeenkomen, en of je de mapping in je `MapperProfile` hebt vastgelegd. Kijk in les 09 ook naar de twee configuraties voor `OrderLijn` naar `BesteldProductDto`, en zoek uit welke van de twee Mapster gebruikt.

---

### 6. Alles samen: een fiets zoals de balie ze wil zien

<span style="background:#7048e8;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>SAMENGESTELDE OEFENING</b></span>

<span style="background:#1c7ed6;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>GET</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/fiets/<span style="color:#e64980"><b>{id}</b></span>/details</code> geeft de fiets met de accessoires die erbij horen, zoals punt 5 het van de andere kant doet, en met het totaal aantal stuks:

```json
{
  "id": 1,
  "type": "stadsfiets",
  "framemaat": 54,
  "prijsPerDag": 12.50,
  "isBeschikbaar": true,
  "aantalStuks": 3,
  "uitrusting": [
    { "accessoireId": 1, "naam": "fietsslot", "aantal": 1 },
    { "accessoireId": 4, "naam": "fietstas", "aantal": 2 }
  ]
}
```

`aantalStuks` is de som van het `aantal` van alle koppelingen van de fiets. Je API stuurt voor dit verzoek nog altijd één query naar de database.

> [!TIP]
> Les 09 raadt af om te rekenen in je `MapperProfile`. Beslis zelf waar `aantalStuks` berekend wordt.

#### Testscenario

Bouw je database opnieuw op, zodat ze exact de startgegevens bevat, en doorloop deze reeks:

1. <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> api/fietsaccessoire/5</code>, <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> api/fietsaccessoire/5/details</code>, <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> api/accessoire/1/details</code> en <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> api/fiets/1/details</code> geven de JSON uit punt 3, 5 en 6.
2. <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> api/fiets/2</code> geeft de fiets zonder veld `fietsAccessoires`. <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> api/fiets/4/details</code> geeft `"aantalStuks": 3`.
3. Voeg deze fiets toe. Je krijgt <code style="color:#37b24d;font-weight:600">201</code>, met een fiets met Id 5, die beschikbaar is en geen veld `fietsAccessoires` heeft. De verhuringen zijn nog altijd leeg.

   ```json
   {
     "id": 99,
     "type": "stadsfiets",
     "framemaat": 50,
     "prijsPerDag": 11.00,
     "isBeschikbaar": false,
     "fietsAccessoires": [ { "accessoireId": 1, "aantal": 1 } ]
   }
   ```

4. <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> api/fiets/5/details</code> geeft `"aantalStuks": 0` en `"uitrusting": []`. <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> api/accessoire/1/details</code> geeft nog altijd drie fietsen.
5. Koppel accessoire 2 aan fiets 5, met aantal 1. Je krijgt <code style="color:#37b24d;font-weight:600">201</code>, met `{ "id": 7, "fietsId": 5, "accessoireId": 2, "aantal": 1 }`. <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> api/fiets/5/details</code> geeft nu `"aantalStuks": 1`.
6. Vervang fiets 1 door de fiets uit stap 6 van 06_05. Je krijgt <code style="color:#37b24d;font-weight:600">201</code>, met een fiets met Id 6 die beschikbaar is. <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> api/fiets/6/details</code> geeft de accessoires 1 en 4, met 1 en 2 stuks.
7. Werk fiets 2 bij met type `mountainbike`, framemaat 50, een prijs per dag van 19.00 en `"isBeschikbaar": false`. Je krijgt <code style="color:#37b24d;font-weight:600">204</code>. <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> api/fiets/2</code> toont framemaat 50 en een prijs per dag van 19.00, en fiets 2 is nog altijd beschikbaar.
8. Verhuur fiets 2. Je krijgt <code style="color:#37b24d;font-weight:600">204</code>. <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> api/fiets/verhuringen</code> geeft één regel, met dezelfde velden als in 08_05: `verhuurd`, met fietsId 2.
