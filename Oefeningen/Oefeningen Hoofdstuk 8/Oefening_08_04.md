# 08_04

<b>Hoofdstuk 08</b> &nbsp;·&nbsp; Gerelateerde gegevens ophalen met Include &nbsp;·&nbsp; Dierenasiel &nbsp;·&nbsp; niveau <span style="color:#37b24d">●○○</span> &nbsp;·&nbsp; ±90 min

## Leerdoel

Na deze oefening kan je met `Include` gerelateerde gegevens in één keer mee ophalen, in beide richtingen van een één-op-veel-relatie.

Je leert die methodes in een specifieke repository zetten, ze via je Unit of Work bereiken, en een circulaire referentie tijdelijk laten afbreken zoals les 08 dat doet.

Daarnaast merk je in je terminal hoeveel query's één verzoek kost, en zie je wat `IgnoreCycles` precies met je JSON doet.

## 

## Opdracht

De website toont bij een dier graag waar het woont, en bij een verblijf wie er woont. Tot nu toe moest ze daarvoor twee of meer verzoeken sturen, en puzzelde ze zelf de stukken samen.

Jouw taak is om drie endpoints te maken die een dier of een verblijf teruggeven, samen met de gegevens die erbij horen, telkens met één query naar de database.

Via de API moet de website, net als in 07_04, alle dieren en verblijven kunnen opvragen, filteren, zoeken, de fiche tonen, bijwerken en dieren verhuizen. Daarnaast moet ze:

1. één dier kunnen opvragen, samen met zijn verblijf;
2. één verblijf kunnen opvragen, samen met al zijn dieren;
3. alle verblijven kunnen opvragen, elk met al zijn dieren.

Implementeer onderstaande functionaliteiten.

### 1. Je vertrekpunt

Vertrek van je eigen oplossing van 07_04. Alle endpoints uit die oefening blijven werken, met dezelfde controles, dezelfde berichten en dezelfde JSON. Daar blijven de navigation properties `null`: enkel de nieuwe endpoints vullen ze.

Je modellen en je database veranderen in deze oefening niet. De startgegevens blijven die van 06_04.

---

### 2. Het ERD

Maak in de map van je project een bestand `ERD.md`. Beschrijf daarin de relatie van je database met haar cardinaliteiten, in dezelfde vorm als het ERD in les 08: één regel per relatie, met een korte uitleg.

---

### 3. Circulaire referenties afbreken

Stel in Program.cs de JSON-serializer zo in dat hij circulaire referenties negeert, zoals les 08 dat doet.

> [!WARNING]
> Les 08 noemt dit zelf een tijdelijke oplossing: op het examen volstaat ze niet. In hoofdstuk 09 haal je deze instelling weer weg.

---

### 4. Een dier met zijn verblijf

<span style="background:#1c7ed6;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>GET</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/dier/<span style="color:#e64980"><b>{id}</b></span>/details</code>

- Het dier bestaat niet: <code style="color:#f08c00;font-weight:600">404 Not Found</code> met <code style="color:#845ef7">We vonden geen dier met Id <span style="color:#e64980"><b>{id}</b></span>.</code>
- Anders: <code style="color:#37b24d;font-weight:600">200 OK</code> met het dier, en daarin zijn verblijf.

Het ophalen gebeurt in een specifieke repository, niet in de generieke: wat je mee ophaalt, hangt af van het model. Laat je Unit of Work het specifieke type teruggeven.

<code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> api/dier/4/details</code> geeft als resultaat:

```json
{
  "id": 4,
  "naam": "Bella",
  "soort": "hond",
  "leeftijdInJaren": 1,
  "isGechipt": true,
  "verblijfId": 2,
  "verblijf": {
    "id": 2,
    "naam": "Hondenren",
    "capaciteit": 4,
    "dieren": [null]
  }
}
```

> [!NOTE]
> Die `[null]` is geen fout. Entity Framework Core vult de lijst van het verblijf zelf aan met wat het in dit verzoek al opgehaald heeft. Hier is dat enkel het dier, en dat dier is het begin van je JSON. `IgnoreCycles` schrijft het daarom niet opnieuw uit, maar zet er `null`. In les 09 los je dat netter op.

---

### 5. Een verblijf met zijn dieren

<span style="background:#1c7ed6;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>GET</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/verblijf/<span style="color:#e64980"><b>{id}</b></span>/details</code>

- Het verblijf bestaat niet: <code style="color:#f08c00;font-weight:600">404 Not Found</code> met <code style="color:#845ef7">We vonden geen verblijf met Id <span style="color:#e64980"><b>{id}</b></span>.</code>
- Anders: <code style="color:#37b24d;font-weight:600">200 OK</code> met het verblijf, en daarin al zijn dieren. Een leeg verblijf heeft een lege lijst.

<code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> api/verblijf/2/details</code> geeft als resultaat:

```json
{
  "id": 2,
  "naam": "Hondenren",
  "capaciteit": 4,
  "dieren": [
    { "id": 3, "naam": "Rex", "soort": "hond", "leeftijdInJaren": 7, "isGechipt": true, "verblijfId": 2, "verblijf": null },
    { "id": 4, "naam": "Bella", "soort": "hond", "leeftijdInJaren": 1, "isGechipt": true, "verblijfId": 2, "verblijf": null }
  ]
}
```

De volgorde van de dieren binnen een verblijf ligt niet vast.

---

### 6. Alles samen: alle verblijven met hun dieren

<span style="background:#7048e8;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>SAMENGESTELDE OEFENING</b></span>

<span style="background:#1c7ed6;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>GET</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/verblijf/details</code>

Dit endpoint geeft <code style="color:#37b24d;font-weight:600">200 OK</code> met alle verblijven, op Id gesorteerd, elk met al zijn dieren, zoals in punt 5.

> [!IMPORTANT]
> Je API stuurt voor dit verzoek precies één query naar de database. Kijk in je terminal: Entity Framework Core toont elke query die het uitvoert. Zie je er meer, lees dan nog eens wat les 07 over het N+1-probleem zegt.

#### Testscenario

Bouw je database opnieuw op, zodat ze exact de startgegevens bevat, en doorloop deze reeks:

1. <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> api/dier/4/details</code> en <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> api/verblijf/2/details</code> geven de JSON uit punt 4 en 5.
2. <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> api/verblijf/5/details</code> geeft verblijf 5 met `"dieren": []`.
3. <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> api/dier/99/details</code> geeft <code style="color:#f08c00;font-weight:600">404</code> met <code style="color:#845ef7">We vonden geen dier met Id 99.</code> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> api/verblijf/99/details</code> geeft <code style="color:#f08c00;font-weight:600">404</code> met <code style="color:#845ef7">We vonden geen verblijf met Id 99.</code>
4. <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> api/dier/4</code> geeft nog altijd `"verblijf": null`, net als in 07_04.
5. <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> api/verblijf/details</code> geeft de verblijven 1, 2, 3, 4 en 5, in die volgorde. In verblijf 1 wonen de dieren 1 en 2, in verblijf 2 de dieren 3 en 4, in verblijf 3 dier 5, in verblijf 4 dier 6, en verblijf 5 is leeg. In je terminal staat bij dit verzoek één query.
6. Verhuis dier 4 naar verblijf 5, met het endpoint uit 07_04. Je krijgt <code style="color:#37b24d;font-weight:600">204</code>. <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> api/verblijf/details</code> toont nu enkel dier 3 in verblijf 2, en dier 4 in verblijf 5.
7. <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> api/verblijf/2/dieren</code> geeft nog altijd enkel dier 3, met `"verblijf": null`, net als in 07_04.
8. Open je `ERD.md`. Staat de relatie erin, met aan beide kanten de juiste cardinaliteit?
