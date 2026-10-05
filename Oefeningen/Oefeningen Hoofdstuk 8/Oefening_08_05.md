# 08_05

<b>Hoofdstuk 08</b> &nbsp;·&nbsp; Twee niveaus diep met ThenInclude &nbsp;·&nbsp; Fietsverhuur &nbsp;·&nbsp; niveau <span style="color:#f08c00">●●○</span> &nbsp;·&nbsp; ±2 uur

## Leerdoel

Na deze oefening kan je met `Include` en `ThenInclude` gegevens over twee niveaus mee ophalen: door een tussentabel heen, naar het model aan de andere kant.

Je leert die methodes in specifieke repositories zetten en ze via je Unit of Work bereiken, en je leest in je terminal na dat alles in één query gebeurt.

Daarnaast merk je hoe `IgnoreCycles` een cyclus afbreekt die door een tussentabel loopt.

## 

## Opdracht

Wie een fiets huurt, wil weten wat erbij hoort: een slot, een helm, twee fietstassen. Vandaag ziet de balie enkel koppelingen met een accessoireId, en zoekt ze de rest er zelf bij.

Jouw taak is om drie endpoints te maken die een koppeling, een accessoire of een fiets teruggeven, samen met alles wat erbij hoort, telkens met één query naar de database.

Via de API moet de balie, net als in 07_05, fietsen kunnen beheren, vervangen, verhuren en terugbrengen, accessoires koppelen en uit het assortiment halen, en de verhuringen nalezen. Daarnaast moet ze:

1. één koppeling kunnen opvragen, met haar fiets en haar accessoire;
2. één accessoire kunnen opvragen, met de fietsen waar het bij hoort;
3. één fiets kunnen opvragen, met de accessoires die erbij horen.

Implementeer onderstaande functionaliteiten.

### 1. Je vertrekpunt

Vertrek van je eigen oplossing van 07_05. Alle endpoints uit die oefening blijven werken, met dezelfde controles, dezelfde berichten en dezelfde JSON. Daar blijven de navigation properties `null`: enkel de nieuwe endpoints vullen ze.

Je modellen en je database veranderen in deze oefening niet. De startgegevens blijven die van 06_05.

---

### 2. Het ERD en de cycli

Maak in de map van je project een bestand `ERD.md`. Beschrijf daarin elke relatie van je database met haar cardinaliteiten, in dezelfde vorm als het ERD in les 08: één regel per relatie, met een korte uitleg. Vermeld ook welk model los staat.

Stel daarna in Program.cs de JSON-serializer zo in dat hij circulaire referenties negeert, zoals les 08 dat doet.

> [!WARNING]
> Les 08 noemt dit zelf een tijdelijke oplossing: op het examen volstaat ze niet. In hoofdstuk 09 haal je deze instelling weer weg.

Het ophalen met `Include` gebeurt in specifieke repositories, niet in de generieke: wat je mee ophaalt, hangt af van het model. Laat je Unit of Work telkens het specifieke type teruggeven.

---

### 3. Een koppeling met haar twee kanten

<span style="background:#1c7ed6;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>GET</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/fietsaccessoire/<span style="color:#e64980"><b>{id}</b></span>/details</code>

- De koppeling bestaat niet: <code style="color:#f08c00;font-weight:600">404 Not Found</code> met <code style="color:#845ef7">We vonden geen koppeling met Id <span style="color:#e64980"><b>{id}</b></span>.</code>
- Anders: <code style="color:#37b24d;font-weight:600">200 OK</code> met de koppeling, en daarin haar fiets en haar accessoire.

<code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> api/fietsaccessoire/5/details</code> geeft als resultaat:

```json
{
  "id": 5,
  "fietsId": 4,
  "fiets": { "id": 4, "type": "bakfiets", "framemaat": 52, "prijsPerDag": 30.00, "isBeschikbaar": true, "fietsAccessoires": [null] },
  "accessoireId": 3,
  "accessoire": { "id": 3, "naam": "kinderzitje", "fietsAccessoires": [null] },
  "aantal": 2
}
```

> [!NOTE]
> Die `[null]` is geen fout. Entity Framework Core vult de lijsten van de fiets en het accessoire zelf aan met wat het in dit verzoek al opgehaald heeft. Hier is dat enkel de koppeling, en die koppeling is het begin van je JSON. `IgnoreCycles` schrijft ze daarom niet opnieuw uit, maar zet er `null`. In les 09 los je dat netter op.

---

### 4. Een accessoire met zijn fietsen

<span style="background:#1c7ed6;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>GET</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/accessoire/<span style="color:#e64980"><b>{id}</b></span>/details</code>

- Het accessoire bestaat niet: <code style="color:#f08c00;font-weight:600">404 Not Found</code> met <code style="color:#845ef7">We vonden geen accessoire met Id <span style="color:#e64980"><b>{id}</b></span>.</code>
- Anders: <code style="color:#37b24d;font-weight:600">200 OK</code> met het accessoire, en daarin al zijn koppelingen, elk met haar fiets. Een accessoire dat bij geen enkele fiets hoort, heeft een lege lijst.

<code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> api/accessoire/1/details</code> geeft als resultaat:

```json
{
  "id": 1,
  "naam": "fietsslot",
  "fietsAccessoires": [
    {
      "id": 1, "fietsId": 1, "accessoireId": 1, "accessoire": null, "aantal": 1,
      "fiets": { "id": 1, "type": "stadsfiets", "framemaat": 54, "prijsPerDag": 12.50, "isBeschikbaar": true, "fietsAccessoires": [null] }
    },
    {
      "id": 3, "fietsId": 2, "accessoireId": 1, "accessoire": null, "aantal": 1,
      "fiets": { "id": 2, "type": "mountainbike", "framemaat": 48, "prijsPerDag": 18.00, "isBeschikbaar": true, "fietsAccessoires": [null] }
    },
    {
      "id": 6, "fietsId": 4, "accessoireId": 1, "accessoire": null, "aantal": 1,
      "fiets": { "id": 4, "type": "bakfiets", "framemaat": 52, "prijsPerDag": 30.00, "isBeschikbaar": true, "fietsAccessoires": [null] }
    }
  ]
}
```

De volgorde van de koppelingen binnen een accessoire ligt niet vast.

---

### 5. Alles samen: een fiets met haar uitrusting

<span style="background:#7048e8;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>SAMENGESTELDE OEFENING</b></span>

<span style="background:#1c7ed6;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>GET</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/fiets/<span style="color:#e64980"><b>{id}</b></span>/details</code>

- De fiets bestaat niet: <code style="color:#f08c00;font-weight:600">404 Not Found</code> met <code style="color:#845ef7">We vonden geen fiets met Id <span style="color:#e64980"><b>{id}</b></span>.</code>
- Anders: <code style="color:#37b24d;font-weight:600">200 OK</code> met de fiets, en daarin al haar koppelingen, elk met haar accessoire, zoals in punt 4 maar van de andere kant bekeken. Een fiets zonder accessoires heeft een lege lijst.

> [!IMPORTANT]
> Je API stuurt voor dit verzoek precies één query naar de database, net als bij punt 3 en 4. Kijk in je terminal: Entity Framework Core toont elke query die het uitvoert. Zie je er meer, lees dan nog eens wat les 07 over het N+1-probleem zegt.

#### Testscenario

Bouw je database opnieuw op, zodat ze exact de startgegevens bevat, en doorloop deze reeks:

1. <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> api/fietsaccessoire/5/details</code> en <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> api/accessoire/1/details</code> geven de JSON uit punt 3 en 4.
2. <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> api/fiets/1/details</code> geeft fiets 1 met de koppelingen 1 en 2. Daarin zit accessoire 1 (`fietsslot`) met aantal 1, en accessoire 4 (`fietstas`) met aantal 2. Elk accessoire heeft `"fietsAccessoires": [null]`, en elke koppeling `"fiets": null`. In je terminal staat bij dit verzoek één query. <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> api/fiets/3/details</code> geeft `"fietsAccessoires": []`.
3. <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> api/fietsaccessoire/99/details</code>, <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> api/accessoire/99/details</code> en <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> api/fiets/99/details</code> geven <code style="color:#f08c00;font-weight:600">404</code>, met het bericht uit punt 3, 4 en 5.
4. <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> api/fiets/1</code> geeft nog altijd `"fietsAccessoires": null`, net als in 07_05.
5. Vervang fiets 1 door de fiets uit stap 6 van 06_05. Je krijgt <code style="color:#37b24d;font-weight:600">201</code>, en de nieuwe fiets heeft Id 5. <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> api/fiets/5/details</code> toont de koppelingen 1 en 2.
6. <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> api/accessoire/1/details</code> geeft de koppelingen 1, 3 en 6, met de fietsen 5, 2 en 4.
7. Haal koppeling 6 weg, met het endpoint uit 07_05. Je krijgt <code style="color:#37b24d;font-weight:600">204</code>. <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> api/fietsaccessoire/6/details</code> geeft <code style="color:#f08c00;font-weight:600">404</code>, en <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> api/fiets/4/details</code> geeft enkel koppeling 5, met accessoire 3.
8. Haal accessoire 2 uit het assortiment, met het endpoint uit 07_05. Je krijgt <code style="color:#37b24d;font-weight:600">204</code>. <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> api/accessoire/2/details</code> geeft <code style="color:#f08c00;font-weight:600">404</code>, en <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> api/fiets/2/details</code> geeft nu enkel koppeling 3.
9. Open je `ERD.md`. Staat elke relatie erin, met aan beide kanten de juiste cardinaliteit, en het model dat los staat?
