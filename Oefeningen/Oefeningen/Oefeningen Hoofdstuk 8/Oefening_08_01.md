# 08_01

<b>Hoofdstuk 08</b> &nbsp;·&nbsp; Gerelateerde gegevens ophalen met Include &nbsp;·&nbsp; PC-onderdelen &nbsp;·&nbsp; niveau <span style="color:#37b24d">●○○</span> &nbsp;·&nbsp; ±90 min

## Leerdoel

Na deze oefening kan je met `Include` gerelateerde gegevens in één keer mee ophalen, in beide richtingen van een één-op-veel-relatie.

Je leert die methodes in een specifieke repository zetten, ze via je Unit of Work bereiken, en een circulaire referentie tijdelijk laten afbreken zoals les 08 dat doet.

Daarnaast merk je in je terminal hoeveel query's één verzoek kost, en zie je wat `IgnoreCycles` precies met je JSON doet.

## 

## Opdracht

De website toont bij een artikel graag van wie het komt, en bij een leverancier wat hij levert. Tot nu toe moest ze daarvoor twee of meer verzoeken sturen, en puzzelde ze zelf de stukken samen.

Jouw taak is om drie endpoints te maken die een artikel of een leverancier teruggeven, samen met de gegevens die erbij horen, telkens met één query naar de database.

Via de API moet de website, net als in 07_01, alle artikelen en leveranciers kunnen opvragen, filteren, de fiche tonen en bijwerken, en artikelen bij een andere leverancier onderbrengen. Daarnaast moet ze:

1. één artikel kunnen opvragen, samen met zijn leverancier;
2. één leverancier kunnen opvragen, samen met al zijn artikelen;
3. alle leveranciers kunnen opvragen, elk met al zijn artikelen.

Implementeer onderstaande functionaliteiten.

### 1. Je vertrekpunt

Vertrek van je eigen oplossing van 07_01. Alle endpoints uit die oefening blijven werken, met dezelfde controles, dezelfde berichten en dezelfde JSON. Daar blijven de navigation properties `null`: enkel de nieuwe endpoints vullen ze.

Je modellen en je database veranderen in deze oefening niet. De startgegevens blijven die van 06_01.

---

### 2. Het ERD

Maak in de map van je project een bestand `ERD.md`. Beschrijf daarin de relatie van je database met haar cardinaliteiten, in dezelfde vorm als het ERD in les 08: één regel per relatie, met een korte uitleg.

---

### 3. Circulaire referenties afbreken

Stel in Program.cs de JSON-serializer zo in dat hij circulaire referenties negeert, zoals les 08 dat doet.

> [!WARNING]
> Les 08 noemt dit zelf een tijdelijke oplossing: op het examen volstaat ze niet. In hoofdstuk 09 haal je deze instelling weer weg.

---

### 4. Een artikel met zijn leverancier

<span style="background:#1c7ed6;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>GET</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/artikel/<span style="color:#e64980"><b>{id}</b></span>/details</code>

- Het artikel bestaat niet: <code style="color:#f08c00;font-weight:600">404 Not Found</code> met <code style="color:#845ef7">We vonden geen artikel met Id <span style="color:#e64980"><b>{id}</b></span>.</code>
- Anders: <code style="color:#37b24d;font-weight:600">200 OK</code> met het artikel, en daarin zijn leverancier.

Het ophalen gebeurt in een specifieke repository, niet in de generieke: wat je mee ophaalt, hangt af van het model. Laat je Unit of Work het specifieke type teruggeven.

<code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> api/artikel/4/details</code> geeft als resultaat:

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
  "leverancier": {
    "id": 3,
    "naam": "ChipCentraal",
    "land": "Duitsland",
    "artikelen": [null]
  }
}
```

> [!NOTE]
> Die `[null]` is geen fout. Entity Framework Core vult de lijst van de leverancier zelf aan met wat het in dit verzoek al opgehaald heeft. Hier is dat enkel het artikel, en dat artikel is het begin van je JSON. `IgnoreCycles` schrijft het daarom niet opnieuw uit, maar zet er `null`. In les 09 los je dat netter op.

---

### 5. Een leverancier met zijn artikelen

<span style="background:#1c7ed6;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>GET</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/leverancier/<span style="color:#e64980"><b>{id}</b></span>/details</code>

- De leverancier bestaat niet: <code style="color:#f08c00;font-weight:600">404 Not Found</code> met <code style="color:#845ef7">We vonden geen leverancier met Id <span style="color:#e64980"><b>{id}</b></span>.</code>
- Anders: <code style="color:#37b24d;font-weight:600">200 OK</code> met de leverancier, en daarin al zijn artikelen. Een leverancier zonder artikelen heeft een lege lijst.

<code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> api/leverancier/3/details</code> geeft als resultaat:

```json
{
  "id": 3,
  "naam": "ChipCentraal",
  "land": "Duitsland",
  "artikelen": [
    { "id": 3, "naam": "Ryzen 5 7600X", "merk": "AMD", "soort": "processor", "prijs": 229.50, "aantalOpVoorraad": 3, "garantiejaren": 3, "leverancierId": 3, "leverancier": null },
    { "id": 4, "naam": "Ryzen 7 7800X3D", "merk": "AMD", "soort": "processor", "prijs": 389.00, "aantalOpVoorraad": 0, "garantiejaren": 3, "leverancierId": 3, "leverancier": null }
  ]
}
```

De volgorde van de artikelen binnen een leverancier ligt niet vast.

---

### 6. Alles samen: alle leveranciers met hun artikelen

<span style="background:#7048e8;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>SAMENGESTELDE OEFENING</b></span>

<span style="background:#1c7ed6;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>GET</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/leverancier/details</code>

Dit endpoint geeft <code style="color:#37b24d;font-weight:600">200 OK</code> met alle leveranciers, op Id gesorteerd, elk met al zijn artikelen, zoals in punt 5.

> [!IMPORTANT]
> Je API stuurt voor dit verzoek precies één query naar de database. Kijk in je terminal: Entity Framework Core toont elke query die het uitvoert. Zie je er meer, lees dan nog eens wat les 07 over het N+1-probleem zegt.

#### Testscenario

Bouw je database opnieuw op, zodat ze exact de startgegevens bevat, en doorloop deze reeks:

1. <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> api/artikel/4/details</code> en <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> api/leverancier/3/details</code> geven de JSON uit punt 4 en 5.
2. <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> api/leverancier/4/details</code> geeft leverancier 4 met `"artikelen": []`.
3. <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> api/artikel/99/details</code> geeft <code style="color:#f08c00;font-weight:600">404</code> met <code style="color:#845ef7">We vonden geen artikel met Id 99.</code> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> api/leverancier/99/details</code> geeft <code style="color:#f08c00;font-weight:600">404</code> met <code style="color:#845ef7">We vonden geen leverancier met Id 99.</code>
4. <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> api/artikel/4</code> geeft nog altijd `"leverancier": null`, net als in 07_01.
5. <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> api/leverancier/details</code> geeft de leveranciers 1, 2, 3 en 4, in die volgorde. Leverancier 1 levert de artikelen 1 en 5, leverancier 2 de artikelen 2 en 6, leverancier 3 de artikelen 3 en 4, en leverancier 4 niets. In je terminal staat bij dit verzoek één query.
6. Breng artikel 4 onder bij leverancier 4, met het endpoint uit 07_01. Je krijgt <code style="color:#37b24d;font-weight:600">204</code>. <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> api/leverancier/details</code> toont nu enkel artikel 3 bij leverancier 3, en artikel 4 bij leverancier 4.
7. <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> api/leverancier/3/artikelen</code> geeft nog altijd enkel artikel 3, met `"leverancier": null`, net als in 07_01.
8. Open je `ERD.md`. Staat de relatie erin, met aan beide kanten de juiste cardinaliteit?
