# 03_02

<b>Hoofdstuk 03</b> &nbsp;·&nbsp; CRUD via een repository &nbsp;·&nbsp; PC-onderdelen &nbsp;·&nbsp; niveau <span style="color:#f08c00">●●○</span> &nbsp;·&nbsp; ±90 min

## Leerdoel

Na deze oefening kan je een volledige CRUD-controller laten werken via een repository.

Je leert dat het toevoegen, wijzigen en verwijderen zelf in de repository gebeurt, terwijl de controller blijft beslissen of een verzoek geldig is en welke statuscode erbij hoort.

Daarnaast merk je wat er met je gegevens gebeurt tussen twee verzoeken, nu je lijst niet meer in de controller leeft, en geef je één controller meer dan één eigen repository.

## 

## Opdracht

Bit & Byte wil van de ventilator-API uit 02_02 hetzelfde maken als van de artikel-API: de lijst uit de controller, achter een interface. De inkoopafdeling wil daarnaast kunnen nalezen wat er allemaal aan het assortiment veranderd is.

Jouw taak is om de VentilatorController te refactoren naar een repository, en er een wijzigingshistoriek aan toe te voegen.

Via de API moet de inkoopafdeling, net als in 02_02:

1. alle ventilatoren kunnen opvragen;
2. één ventilator kunnen opvragen;
3. een nieuwe ventilator kunnen toevoegen;
4. een bestaande ventilator kunnen bijwerken;
5. een ventilator kunnen verwijderen;
6. een ventilator kunnen vervangen;

en daarnaast:

7. kunnen nalezen welke wijzigingen er gebeurd zijn.

Implementeer onderstaande functionaliteiten.

### 1. Je vertrekpunt

Vertrek van je eigen oplossing van 02_02. Na deze oefening moeten de volgende endpoints nog precies hetzelfde antwoorden als vandaag:

| Verzoek | Antwoord |
|---------|----------|
| <span style="background:#1c7ed6;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>GET</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/ventilator</code> | <code style="color:#37b24d;font-weight:600">200 OK</code> met de volledige lijst |
| <span style="background:#1c7ed6;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>GET</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/ventilator/<span style="color:#e64980"><b>{id}</b></span></code> | <code style="color:#37b24d;font-weight:600">200 OK</code> met de ventilator, of <code style="color:#f08c00;font-weight:600">404 Not Found</code> met <code style="color:#845ef7">We vonden geen ventilator met Id <span style="color:#e64980"><b>{id}</b></span>.</code> |
| <span style="background:#2f9e44;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>POST</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/ventilator</code> | <code style="color:#37b24d;font-weight:600">201 Created</code> met de nieuwe ventilator en zijn adres, of <code style="color:#f08c00;font-weight:600">400 Bad Request</code> met de reden |
| <span style="background:#d9822b;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>PUT</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/ventilator/<span style="color:#e64980"><b>{id}</b></span></code> | <code style="color:#37b24d;font-weight:600">204 No Content</code>, <code style="color:#f08c00;font-weight:600">404 Not Found</code> met <code style="color:#845ef7">Kan geen ventilator bijwerken met Id <span style="color:#e64980"><b>{id}</b></span>, omdat deze niet bestaat.</code>, of <code style="color:#f08c00;font-weight:600">400 Bad Request</code> met de reden |
| <span style="background:#e03131;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>DELETE</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/ventilator/<span style="color:#e64980"><b>{id}</b></span></code> | <code style="color:#37b24d;font-weight:600">204 No Content</code>, of <code style="color:#f08c00;font-weight:600">404 Not Found</code> met <code style="color:#845ef7">Kan ventilator met Id <span style="color:#e64980"><b>{id}</b></span> niet verwijderen, omdat deze niet gevonden is.</code> |
| <span style="background:#d9822b;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>PUT</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/ventilator/vervang/<span style="color:#e64980"><b>{id}</b></span></code> | <code style="color:#37b24d;font-weight:600">201 Created</code> met de nieuwe ventilator en zijn adres, <code style="color:#f08c00;font-weight:600">404 Not Found</code> met <code style="color:#845ef7">Kan ventilator met Id <span style="color:#e64980"><b>{id}</b></span> niet vervangen, omdat deze niet gevonden is.</code>, of <code style="color:#f08c00;font-weight:600">400 Bad Request</code> met de reden |

De drie redenen voor een <code style="color:#f08c00;font-weight:600">400</code> blijven dezelfde als in 02_02:

- het merk is leeg: <code style="color:#845ef7">Een ventilator moet een merk hebben.</code>
- de afmeting is niet 120 of 140: <code style="color:#845ef7">We verkopen enkel ventilatoren van 120 of 140 mm.</code>
- de prijs is 0 of lager: <code style="color:#845ef7">De prijs moet groter zijn dan 0.</code>

Bij een vervanging kijk je eerst of de oude ventilator bestaat, en pas daarna of de nieuwe door de drie controles geraakt.

> [!TIP]
> Doe al deze verzoeken één keer vóór je begint, en noteer wat je terugkrijgt. Na je refactoring doe je exact dezelfde verzoeken opnieuw.

---

### 2. Het contract

Maak in de map Repositories een interface `IVentilatorRepository`.

De interface beschrijft wat je repository moet kunnen:

- alle ventilatoren teruggeven;
- één ventilator teruggeven op basis van zijn Id, of niets als hij niet bestaat;
- een nieuwe ventilator toevoegen en hem teruggeven, met het Id dat hij gekregen heeft;
- een bestaande ventilator bijwerken;
- een ventilator verwijderen.

---

### 3. De repository

Maak een klasse `InMemoryVentilatorRepository` die `IVentilatorRepository` implementeert, en verhuis je lijst met ventilatoren naar deze klasse.

Het Id van een nieuwe ventilator bepaalt vanaf nu de repository. Stuurt de client toch een Id mee, dan wordt dat nog altijd overschreven.

---

### 4. Wie beslist wat

> [!IMPORTANT]
> De drie controles uit 02_02, en de controle of een ventilator bestaat, blijven in de controller. De repository voert uit wat de controller hem vraagt en beslist zelf niets: hij weigert geen invoer en kiest geen statuscode.

Pas de VentilatorController aan, zodat hij een `IVentilatorRepository` en een `ILogger<VentilatorController>` binnenkrijgt via zijn constructor. Leg in Program.cs vast welke klasse hij krijgt.

Log in elk endpoint minstens één regel:

- elk binnenkomend verzoek op niveau Information;
- elke <code style="color:#f08c00;font-weight:600">404</code> op niveau Warning;
- elke <code style="color:#f08c00;font-weight:600">400</code> op niveau Warning, met de reden in het bericht.

---

### 5. Gegevens tussen twee verzoeken

Voeg een ventilator toe via POST en vraag daarna de volledige lijst op.

> [!WARNING]
> Staat je nieuwe ventilator er niet meer bij, terwijl dat in 02_02 wel zo was? Dan leeft je lijst niet lang genoeg. Zoek uit hoe vaak ASP.NET Core een nieuwe repository aanmaakt, en wat er dan gebeurt met de lijst die erin zit. In les 02 had je hetzelfde probleem al eens, toen de lijst nog in de controller stond.

Test daarna elk endpoint opnieuw en controleer in het bijzonder:

- of een POST met een afmeting van 92 mm <code style="color:#f08c00;font-weight:600">400</code> geeft, niet in de lijst belandt, en een Warning met de reden in je log zet;
- of een DELETE op een Id dat je net verwijderd hebt, de tweede keer <code style="color:#f08c00;font-weight:600">404</code> geeft;
- of een ventilator die je bijwerkt, bij een volgend verzoek nog steeds de nieuwe gegevens heeft.

---

### 6. Alles samen: de wijzigingshistoriek

<span style="background:#7048e8;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>SAMENGESTELDE OEFENING</b></span>

De inkoopafdeling wil weten wat er met het assortiment gebeurd is: welke ventilatoren erbij kwamen, welke gewijzigd werden en welke verdwenen. Daarvoor bouw je een tweede repository, die je naast de eerste in dezelfde controller gebruikt.

#### Het model

Maak in de map Models een klasse `Wijziging` met volgende gegevens:

| Gegeven      | Soort waarde |
|--------------|--------------|
| Id           | geheel getal |
| Actie        | tekst        |
| VentilatorId | geheel getal |

#### De tweede repository

Maak een interface `IWijzigingRepository` en een klasse `InMemoryWijzigingRepository` die ze implementeert. Deze repository moet:

- een wijziging kunnen bewaren, waarbij hij zelf het Id bepaalt;
- alle wijzigingen kunnen teruggeven, de oudste eerst.

De VentilatorController krijgt ook deze repository via zijn constructor.

> [!WARNING]
> Je historiek begint leeg, je ventilatoren niet. Kijk na of de manier waarop je een nieuw Id bepaalt, ook werkt voor de allereerste wijziging.

#### Wanneer er een wijziging bijkomt

Na elke geslaagde actie bewaar je een wijziging:

| Geslaagde actie | Actie                        | VentilatorId                       |
|-----------------|------------------------------|------------------------------------|
| POST            | `toegevoegd`                 | het Id van de nieuwe ventilator    |
| PUT (bijwerken) | `bijgewerkt`                 | het Id uit de URL                  |
| DELETE          | `verwijderd`                 | het Id uit de URL                  |
| PUT (vervangen) | eerst `verwijderd`, dan `toegevoegd` | eerst het oude Id, dan het nieuwe Id |

> [!IMPORTANT]
> Een actie die mislukt, met een <code style="color:#f08c00;font-weight:600">400</code> of een <code style="color:#f08c00;font-weight:600">404</code>, laat geen enkel spoor na in de historiek.

#### De historiek opvragen

Voorzie een GET-endpoint op:

<span style="background:#1c7ed6;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>GET</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/ventilator/historiek</code>

Dit endpoint geeft alle wijzigingen terug, de oudste eerst, met statuscode <code style="color:#37b24d;font-weight:600">200 OK</code>. Is er nog niets gewijzigd, dan krijg je een lege lijst, ook met <code style="color:#37b24d;font-weight:600">200 OK</code>.

#### Testscenario

Herstart je API, zodat je met een lege historiek begint, en doorloop deze reeks. Vraag na elke stap de historiek op.

1. De historiek is leeg.
2. Voeg een geldige ventilator toe. Je krijgt <code style="color:#37b24d;font-weight:600">201</code>. De historiek bevat één wijziging: `toegevoegd`, met het Id van je nieuwe ventilator.
3. Voeg een ventilator van 92 mm toe. Je krijgt <code style="color:#f08c00;font-weight:600">400</code>. De historiek is niet veranderd.
4. Werk de ventilator uit stap 2 bij met geldige gegevens. Je krijgt <code style="color:#37b24d;font-weight:600">204</code>. De historiek bevat twee wijzigingen.
5. Vervang een andere bestaande ventilator door een geldige nieuwe. Je krijgt <code style="color:#37b24d;font-weight:600">201</code>. De historiek bevat vier wijzigingen; de laatste twee zijn `verwijderd` met het oude Id en `toegevoegd` met het nieuwe.
6. Verwijder een Id dat niet bestaat. Je krijgt <code style="color:#f08c00;font-weight:600">404</code>. De historiek bevat nog altijd vier wijzigingen.
7. Vraag de volledige lijst met ventilatoren op. Ze moet kloppen met wat de historiek vertelt.

> [!WARNING]
> Blijven je ventilatoren bewaard tussen twee verzoeken, maar is je historiek telkens weer leeg? Dan heb je de vraag uit punt 5 voor één van je twee repositories opgelost, maar niet voor de andere.
