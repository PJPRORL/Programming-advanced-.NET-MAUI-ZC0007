# 09_02

<b>Hoofdstuk 09</b> &nbsp;·&nbsp; DTO's, Mapster en een tussentabel platslaan &nbsp;·&nbsp; PC-onderdelen &nbsp;·&nbsp; niveau <span style="color:#f08c00">●●○</span> &nbsp;·&nbsp; ±2,5 uur

## Leerdoel

Na deze oefening kan je vier modellen afschermen met read- en write-DTO's, en een tussentabel in je JSON laten verdwijnen: de client ziet behuizingen en ventilatoren, geen compatibiliteiten.

Je leert in je `MapperProfile` een eigen mapping schrijven voor properties die niet dezelfde naam hebben of een niveau dieper zitten, zoals les 09 dat doet met een orderlijn en haar product.

Daarnaast merk je dat Mapster zwijgt als het iets niet kan mappen: een veld dat leeg blijft, geeft geen foutmelding.

## 

## Opdracht

De balie leest vandaag compatibiliteiten met daarin een behuizing met daarin een lijst met `[null]`. Ze wil gewoon zien: in deze behuizing passen deze ventilatoren, zoveel keer.

Jouw taak is om elk endpoint om te bouwen naar DTO's, met Mapster, en de details-endpoints uit 08_02 een eigen, platte vorm te geven.

Via de API moet de inkoopafdeling, net als in 08_02, ventilatoren kunnen beheren, vervangen en koppelen, behuizingen uit het gamma halen, de historiek nalezen en de details tonen. Daarnaast moet ze:

1. bij een behuizing meteen zien hoeveel ventilatoren er in totaal in passen.

Implementeer onderstaande functionaliteiten.

### 1. Je vertrekpunt

Vertrek van je eigen oplossing van 08_02. Alle endpoints blijven werken, met dezelfde routes, dezelfde controles en dezelfde berichten. Enkel hun JSON verandert, zoals hieronder beschreven.

Je modellen en je database veranderen in deze oefening niet. De startgegevens blijven die van 06_02.

Haal de instelling `IgnoreCycles` uit Program.cs weg, zoals les 09 vraagt.

---

### 2. Mapster en de structuur

Installeer Mapster in je project. Maak een map `DTOs`, met daarin een map per model, en een map `Configuration` met een klasse `MapperProfile`, zoals in les 09. Zorg dat Mapster je `MapperProfile` vindt bij het opstarten.

Of je DTO's klassen of records zijn, kies je zelf. Kies je voor records, denk dan na over hoe je de berekende velden invult: een record pas je na het aanmaken niet zomaar meer aan.

> [!IMPORTANT]
> Een DTO bevat nooit een model, en een model nooit een DTO. Een DTO mag wel een andere DTO bevatten, zolang die niet terugverwijst.

---

### 3. De read-DTO's

Elk endpoint dat vroeger een model teruggaf, geeft nu een read-DTO terug. Die heeft dezelfde properties als het model, **zonder de navigation properties**. Dat geldt voor alle vier de modellen, ook voor de historiek.

<code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> api/compatibiliteit/4</code> geeft nu als resultaat:

```json
{ "id": 4, "behuizingId": 2, "ventilatorId": 3, "aantalPlaatsen": 2 }
```

---

### 4. De write-DTO's

De client stuurt voortaan enkel nog wat hij mag invullen:

| Verzoek | Velden in de body |
|---------|-------------------|
| <span style="background:#2f9e44;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>POST</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/ventilator</code>, <span style="background:#d9822b;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>PUT</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/ventilator/<span style="color:#e64980"><b>{id}</b></span></code> en <span style="background:#d9822b;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>PUT</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/ventilator/vervang/<span style="color:#e64980"><b>{id}</b></span></code> | `merk`, `afmetingInMm`, `verlichting`, `prijs` |
| <span style="background:#2f9e44;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>POST</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/compatibiliteit</code> | `behuizingId`, `ventilatorId`, `aantalPlaatsen` |

Al de rest in de body negeert je API, gewoon omdat de DTO het niet kent. De controles, de berichten en de historiek blijven dezelfde.

> [!WARNING]
> Les 09 vermeldt data annotations op write-DTO's. Gebruik ze hier niet voor je controles: ze geven een eigen <code style="color:#f08c00;font-weight:600">400</code>, met een ander bericht dan je API nu belooft.

---

### 5. De details, zonder tussentabel

<span style="background:#1c7ed6;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>GET</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/compatibiliteit/<span style="color:#e64980"><b>{id}</b></span>/details</code> geeft de koppeling plat in één object:

```json
{
  "id": 4,
  "aantalPlaatsen": 2,
  "behuizingId": 2,
  "behuizingNaam": "Airflow Mini",
  "ventilatorId": 3,
  "ventilatorMerk": "Corsair"
}
```

<span style="background:#1c7ed6;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>GET</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/ventilator/<span style="color:#e64980"><b>{id}</b></span>/details</code> geeft de ventilator met de behuizingen waarin hij past. Elke compatibiliteit wordt één regel, met de gegevens van de behuizing:

```json
{
  "id": 2,
  "merk": "be quiet!",
  "afmetingInMm": 140,
  "verlichting": "geen",
  "prijs": 24.90,
  "behuizingen": [
    { "behuizingId": 1, "naam": "Silent Base", "formaat": "midi tower", "aantalPlaatsen": 2 },
    { "behuizingId": 3, "naam": "Studio XL", "formaat": "big tower", "aantalPlaatsen": 4 }
  ]
}
```

De volgorde binnen zo'n lijst ligt niet vast. De berichten bij een <code style="color:#f08c00;font-weight:600">404</code> blijven die van 08_02.

> [!WARNING]
> Blijft een veld leeg, of staat er `0` waar je een waarde verwachtte? Mapster geeft geen foutmelding als het een property niet kan vullen. Kijk of de naam en het type overeenkomen, en of je de mapping in je `MapperProfile` hebt vastgelegd. Kijk in les 09 ook naar de twee configuraties voor `OrderLijn` naar `BesteldProductDto`, en zoek uit welke van de twee Mapster gebruikt.

---

### 6. Alles samen: een behuizing zoals de balie ze wil zien

<span style="background:#7048e8;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>SAMENGESTELDE OEFENING</b></span>

<span style="background:#1c7ed6;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>GET</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/behuizing/<span style="color:#e64980"><b>{id}</b></span>/details</code> geeft de behuizing met de ventilatoren die erin passen, zoals punt 5 het van de andere kant doet, en met het totaal aantal plaatsen:

```json
{
  "id": 1,
  "naam": "Silent Base",
  "formaat": "midi tower",
  "totaalPlaatsen": 5,
  "ventilatoren": [
    { "ventilatorId": 1, "merk": "Noctua", "afmetingInMm": 120, "prijs": 29.90, "aantalPlaatsen": 3 },
    { "ventilatorId": 2, "merk": "be quiet!", "afmetingInMm": 140, "prijs": 24.90, "aantalPlaatsen": 2 }
  ]
}
```

`totaalPlaatsen` is de som van de `aantalPlaatsen` van alle compatibiliteiten van de behuizing. Je API stuurt voor dit verzoek nog altijd één query naar de database.

> [!TIP]
> Les 09 raadt af om te rekenen in je `MapperProfile`. Beslis zelf waar `totaalPlaatsen` berekend wordt.

#### Testscenario

Bouw je database opnieuw op, zodat ze exact de startgegevens bevat, en doorloop deze reeks:

1. <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> api/compatibiliteit/4</code>, <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> api/compatibiliteit/4/details</code>, <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> api/ventilator/2/details</code> en <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> api/behuizing/1/details</code> geven de JSON uit punt 3, 5 en 6.
2. <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> api/ventilator/2</code> geeft de ventilator zonder veld `compatibiliteiten`. <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> api/behuizing/3/details</code> geeft `"totaalPlaatsen": 7`.
3. Voeg deze ventilator toe. Je krijgt <code style="color:#37b24d;font-weight:600">201</code>, met een ventilator met Id 5 en zonder veld `compatibiliteiten`. De historiek bevat één regel: `toegevoegd`, met ventilatorId 5.

   ```json
   {
     "id": 99,
     "merk": "Arctic",
     "afmetingInMm": 120,
     "verlichting": "geen",
     "prijs": 9.99,
     "compatibiliteiten": [ { "behuizingId": 1, "aantalPlaatsen": 4 } ]
   }
   ```

4. <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> api/ventilator/5/details</code> geeft `"behuizingen": []`. <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> api/behuizing/1/details</code> geeft nog altijd `"totaalPlaatsen": 5`.
5. Koppel ventilator 5 aan behuizing 1, met 4 plaatsen. Je krijgt <code style="color:#37b24d;font-weight:600">201</code>, met `{ "id": 7, "behuizingId": 1, "ventilatorId": 5, "aantalPlaatsen": 4 }`. <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> api/behuizing/1/details</code> geeft nu `"totaalPlaatsen": 9` en drie ventilatoren.
6. Vervang ventilator 1 door de ventilator uit stap 5 van 06_02. Je krijgt <code style="color:#37b24d;font-weight:600">201</code>, met een ventilator met Id 6. <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> api/ventilator/6/details</code> geeft de behuizingen 1 en 2, met 3 en 2 plaatsen.
7. <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> api/ventilator/historiek</code> geeft drie regels, met dezelfde velden als in 08_02: `toegevoegd` met 5, `verwijderd` met 1 en `toegevoegd` met 6.
8. Werk ventilator 2 bij met een afmeting van 92 mm, en verder geldige gegevens. Je krijgt <code style="color:#f08c00;font-weight:600">400</code> met <code style="color:#845ef7">We verkopen enkel ventilatoren van 120 of 140 mm.</code>
