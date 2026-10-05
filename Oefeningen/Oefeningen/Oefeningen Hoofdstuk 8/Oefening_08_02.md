# 08_02

<b>Hoofdstuk 08</b> &nbsp;·&nbsp; Twee niveaus diep met ThenInclude &nbsp;·&nbsp; PC-onderdelen &nbsp;·&nbsp; niveau <span style="color:#f08c00">●●○</span> &nbsp;·&nbsp; ±2 uur

## Leerdoel

Na deze oefening kan je met `Include` en `ThenInclude` gegevens over twee niveaus mee ophalen: door een tussentabel heen, naar het model aan de andere kant.

Je leert die methodes in specifieke repositories zetten en ze via je Unit of Work bereiken, en je leest in je terminal na dat alles in één query gebeurt.

Daarnaast merk je hoe `IgnoreCycles` een cyclus afbreekt die door een tussentabel loopt.

## 

## Opdracht

Een klant aan de balie wil weten welke ventilatoren in zijn behuizing passen, met merk en prijs erbij. Vandaag ziet de balie enkel compatibiliteiten met een ventilatorId, en zoekt ze de rest er zelf bij.

Jouw taak is om drie endpoints te maken die een compatibiliteit, een ventilator of een behuizing teruggeven, samen met alles wat erbij hoort, telkens met één query naar de database.

Via de API moet de inkoopafdeling, net als in 07_02, ventilatoren kunnen beheren, vervangen en koppelen, behuizingen uit het gamma halen en de historiek nalezen. Daarnaast moet ze:

1. één compatibiliteit kunnen opvragen, met haar behuizing en haar ventilator;
2. één ventilator kunnen opvragen, met de behuizingen waarin hij past;
3. één behuizing kunnen opvragen, met de ventilatoren die erin passen.

Implementeer onderstaande functionaliteiten.

### 1. Je vertrekpunt

Vertrek van je eigen oplossing van 07_02. Alle endpoints uit die oefening blijven werken, met dezelfde controles, dezelfde berichten en dezelfde JSON. Daar blijven de navigation properties `null`: enkel de nieuwe endpoints vullen ze.

Je modellen en je database veranderen in deze oefening niet. De startgegevens blijven die van 06_02.

---

### 2. Het ERD en de cycli

Maak in de map van je project een bestand `ERD.md`. Beschrijf daarin elke relatie van je database met haar cardinaliteiten, in dezelfde vorm als het ERD in les 08: één regel per relatie, met een korte uitleg. Vermeld ook welk model los staat.

Stel daarna in Program.cs de JSON-serializer zo in dat hij circulaire referenties negeert, zoals les 08 dat doet.

> [!WARNING]
> Les 08 noemt dit zelf een tijdelijke oplossing: op het examen volstaat ze niet. In hoofdstuk 09 haal je deze instelling weer weg.

Het ophalen met `Include` gebeurt in specifieke repositories, niet in de generieke: wat je mee ophaalt, hangt af van het model. Laat je Unit of Work telkens het specifieke type teruggeven.

---

### 3. Een compatibiliteit met haar twee kanten

<span style="background:#1c7ed6;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>GET</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/compatibiliteit/<span style="color:#e64980"><b>{id}</b></span>/details</code>

- De compatibiliteit bestaat niet: <code style="color:#f08c00;font-weight:600">404 Not Found</code> met <code style="color:#845ef7">We vonden geen compatibiliteit met Id <span style="color:#e64980"><b>{id}</b></span>.</code>
- Anders: <code style="color:#37b24d;font-weight:600">200 OK</code> met de compatibiliteit, en daarin haar behuizing en haar ventilator.

<code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> api/compatibiliteit/4/details</code> geeft als resultaat:

```json
{
  "id": 4,
  "behuizingId": 2,
  "behuizing": { "id": 2, "naam": "Airflow Mini", "formaat": "mini tower", "compatibiliteiten": [null] },
  "ventilatorId": 3,
  "ventilator": { "id": 3, "merk": "Corsair", "afmetingInMm": 120, "verlichting": "RGB", "prijs": 34.90, "compatibiliteiten": [null] },
  "aantalPlaatsen": 2
}
```

> [!NOTE]
> Die `[null]` is geen fout. Entity Framework Core vult de lijsten van de behuizing en de ventilator zelf aan met wat het in dit verzoek al opgehaald heeft. Hier is dat enkel de compatibiliteit, en die compatibiliteit is het begin van je JSON. `IgnoreCycles` schrijft ze daarom niet opnieuw uit, maar zet er `null`. In les 09 los je dat netter op.

---

### 4. Een ventilator met zijn behuizingen

<span style="background:#1c7ed6;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>GET</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/ventilator/<span style="color:#e64980"><b>{id}</b></span>/details</code>

- De ventilator bestaat niet: <code style="color:#f08c00;font-weight:600">404 Not Found</code> met <code style="color:#845ef7">We vonden geen ventilator met Id <span style="color:#e64980"><b>{id}</b></span>.</code>
- Anders: <code style="color:#37b24d;font-weight:600">200 OK</code> met de ventilator, en daarin al zijn compatibiliteiten, elk met haar behuizing. Een ventilator die nergens in past, heeft een lege lijst.

<code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> api/ventilator/2/details</code> geeft als resultaat:

```json
{
  "id": 2,
  "merk": "be quiet!",
  "afmetingInMm": 140,
  "verlichting": "geen",
  "prijs": 24.90,
  "compatibiliteiten": [
    {
      "id": 2, "behuizingId": 1, "ventilatorId": 2, "ventilator": null, "aantalPlaatsen": 2,
      "behuizing": { "id": 1, "naam": "Silent Base", "formaat": "midi tower", "compatibiliteiten": [null] }
    },
    {
      "id": 5, "behuizingId": 3, "ventilatorId": 2, "ventilator": null, "aantalPlaatsen": 4,
      "behuizing": { "id": 3, "naam": "Studio XL", "formaat": "big tower", "compatibiliteiten": [null] }
    }
  ]
}
```

De volgorde van de compatibiliteiten binnen een ventilator ligt niet vast.

---

### 5. Alles samen: een behuizing met haar ventilatoren

<span style="background:#7048e8;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>SAMENGESTELDE OEFENING</b></span>

<span style="background:#1c7ed6;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>GET</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/behuizing/<span style="color:#e64980"><b>{id}</b></span>/details</code>

- De behuizing bestaat niet: <code style="color:#f08c00;font-weight:600">404 Not Found</code> met <code style="color:#845ef7">We vonden geen behuizing met Id <span style="color:#e64980"><b>{id}</b></span>.</code>
- Anders: <code style="color:#37b24d;font-weight:600">200 OK</code> met de behuizing, en daarin al haar compatibiliteiten, elk met haar ventilator, zoals in punt 4 maar van de andere kant bekeken. Een behuizing zonder compatibiliteiten heeft een lege lijst.

> [!IMPORTANT]
> Je API stuurt voor dit verzoek precies één query naar de database, net als bij punt 3 en 4. Kijk in je terminal: Entity Framework Core toont elke query die het uitvoert. Zie je er meer, lees dan nog eens wat les 07 over het N+1-probleem zegt.

#### Testscenario

Bouw je database opnieuw op, zodat ze exact de startgegevens bevat, en doorloop deze reeks:

1. <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> api/compatibiliteit/4/details</code> en <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> api/ventilator/2/details</code> geven de JSON uit punt 3 en 4.
2. <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> api/behuizing/1/details</code> geeft behuizing 1 met de compatibiliteiten 1 en 2. Daarin zit ventilator 1 (`Noctua`) met 3 plaatsen, en ventilator 2 (`be quiet!`) met 2 plaatsen. Elke ventilator heeft `"compatibiliteiten": [null]`, en elke compatibiliteit `"behuizing": null`. In je terminal staat bij dit verzoek één query.
3. <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> api/compatibiliteit/99/details</code>, <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> api/ventilator/99/details</code> en <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> api/behuizing/99/details</code> geven <code style="color:#f08c00;font-weight:600">404</code>, met het bericht uit punt 3, 4 en 5.
4. <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> api/ventilator/2</code> geeft nog altijd `"compatibiliteiten": null`, net als in 07_02.
5. Vervang ventilator 1 door de ventilator uit stap 5 van 06_02. Je krijgt <code style="color:#37b24d;font-weight:600">201</code>, en de nieuwe ventilator heeft Id 5. <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> api/behuizing/1/details</code> toont nu ventilator 5 in compatibiliteit 1.
6. <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> api/ventilator/5/details</code> geeft de compatibiliteiten 1 en 3, met de behuizingen 1 en 2.
7. Haal compatibiliteit 6 weg en verwijder daarna ventilator 4. Je krijgt telkens <code style="color:#37b24d;font-weight:600">204</code>. <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> api/ventilator/4/details</code> geeft <code style="color:#f08c00;font-weight:600">404</code>, en <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> api/behuizing/3/details</code> geeft enkel compatibiliteit 5, met ventilator 2.
8. Haal behuizing 2 uit het gamma, met het endpoint uit 07_02. Je krijgt <code style="color:#37b24d;font-weight:600">204</code>. <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> api/ventilator/5/details</code> geeft nu enkel compatibiliteit 1. <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> api/ventilator/3/details</code> geeft `"compatibiliteiten": []`.
9. Open je `ERD.md`. Staat elke relatie erin, met aan beide kanten de juiste cardinaliteit, en het model dat los staat?
