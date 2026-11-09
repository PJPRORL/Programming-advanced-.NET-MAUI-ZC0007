# 05_02

<b>Hoofdstuk 05</b> &nbsp;·&nbsp; Twee asynchrone repositories en de cijfers &nbsp;·&nbsp; PC-onderdelen &nbsp;·&nbsp; niveau <span style="color:#f08c00">●●○</span> &nbsp;·&nbsp; ±2 uur

## Leerdoel

Na deze oefening kan je twee asynchrone repositories op één DbContext laten werken, elk achter een interface die je letterlijk volgt.

Je leert gemiddelden, minima, maxima en tellingen door de database laten berekenen met `AverageAsync`, `MinAsync`, `MaxAsync` en `CountAsync`, en wat elk van die methodes doet als er niets te berekenen valt.

Daarnaast merk je wat ASP.NET Core doet met het achtervoegsel `Async` in de naam van een controlleractie.

## 

## Opdracht

In 04_02 bewaarde je ventilator-API alles in de database, met een controller die zelf met de context werkte. De inkoopafdeling wil die code achter repositories, en wil daarnaast de prijzen van het assortiment kunnen volgen: wat kost een ventilator gemiddeld, wat is de goedkoopste en de duurste, en hoe vaak gebeurde welke wijziging.

Jouw taak is om twee asynchrone repositories op één context te schrijven, je controller erop te laten overstappen, en de cijfers als endpoints beschikbaar te maken.

Via de API moet de inkoopafdeling, net als in 04_02, ventilatoren beheren en de historiek nalezen. Daarnaast moet ze:

1. de gemiddelde, de laagste en de hoogste prijs kunnen opvragen;
2. de gemiddelde prijs van één soort verlichting kunnen opvragen;
3. kunnen tellen hoe vaak een actie in de historiek voorkomt.

Implementeer onderstaande functionaliteiten.

### 1. Je vertrekpunt

Vertrek van je eigen oplossing van 04_02. Je model en je database veranderen in deze oefening niet. Alle endpoints uit 04_02 moeten nog precies hetzelfde antwoorden.

---

### 2. De contracten

Vervang je oude interfaces uit les 03 door asynchrone versies, in de map Repositories.

`IVentilatorRepository`:

| Methode | Parameters | Geeft terug |
|---------|------------|-------------|
| `GetAllAsync` | geen | `Task<List<Ventilator>>` |
| `GetByIdAsync` | `int id` | `Task<Ventilator?>` |
| `CreateAsync` | `Ventilator ventilator` | `Task<Ventilator>` |
| `UpdateAsync` | `int id, Ventilator ventilator` | `Task` |
| `DeleteAsync` | `int id` | `Task` |
| `GetGemiddeldePrijsAsync` | geen | `Task<double>` |
| `GetLaagstePrijsAsync` | geen | `Task<double>` |
| `GetHoogstePrijsAsync` | geen | `Task<double>` |
| `GetGemiddeldePrijsByVerlichtingAsync` | `string verlichting` | `Task<double>` |

`IWijzigingRepository`:

| Methode | Parameters | Geeft terug |
|---------|------------|-------------|
| `CreateAsync` | `Wijziging wijziging` | `Task<Wijziging>` |
| `GetAllAsync` | geen | `Task<List<Wijziging>>` |
| `CountByActieAsync` | `string actie` | `Task<int>` |

> [!IMPORTANT]
> Neem de namen, de parameters en de types letterlijk over. Op het examen krijg je de interfaces zo.

---

### 3. Twee repositories op één context

Maak de klassen `VentilatorRepository` en `WijzigingRepository`. Beide krijgen je `VentilatorContext` via hun constructor, en bewaren die in een `readonly` veld. Registreer ze met `AddScoped`.

Wat de methodes doen:

- `GetAllAsync` geeft de ventilatoren op Id gesorteerd, en de wijzigingen de oudste eerst.
- `CreateAsync` bewaart het nieuwe item en geeft het terug, met de Id die de database gaf.
- `UpdateAsync` overschrijft alle gegevens van de ventilator met die Id, behalve de Id. `DeleteAsync` verwijdert hem. Bestaat de ventilator niet, dan doen ze allebei niets.
- De gemiddelde, de laagste en de hoogste prijs berekent de database, met `AverageAsync`, `MinAsync` en `MaxAsync`. Bij de verlichting negeer je hoofdletters.
- `CountByActieAsync` telt de wijzigingen met die actie, met `CountAsync`, zonder op hoofdletters te letten.

Elke methode die een getal berekent, geeft `0` als er niets te berekenen valt.

> [!WARNING]
> `AverageAsync`, `MinAsync` en `MaxAsync` geven geen `0` als ze niets vinden. Probeer het met een verlichting die niet bestaat, en lees wat er misloopt. Zorg dat je methode in dat geval `0` teruggeeft. `CountAsync` heeft dat probleem niet: waarom?

> [!NOTE]
> Dat `UpdateAsync` en `DeleteAsync` niets doen als het item niet bestaat, is wat de schoolopgaven vragen. De repository beslist niets: de controller kijkt eerst of de ventilator bestaat, en kiest de statuscode.

---

### 4. De controller op de repositories

Pas je `VentilatorController` aan: hij krijgt via zijn constructor een `IVentilatorRepository` en een `IWijzigingRepository` binnen in plaats van de context, naast zijn logger, en elk endpoint werkt asynchroon.

De controles, de berichten, de regels voor de historiek en de logging blijven precies zoals in 04_02.

> [!WARNING]
> Krijg je bij een POST of een vervanging een <code style="color:#f03e3e;font-weight:600">500</code> met *No route matches the supplied values*, terwijl de ventilator wel in je database staat? Lees in je leerboek, hoofdstuk 38, wat ASP.NET Core met het achtervoegsel `Async` in de naam van een controlleractie doet.

---

### 5. De cijfers

| Endpoint | Antwoord |
|----------|----------|
| <span style="background:#1c7ed6;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>GET</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/ventilator/prijzen</code> | <code style="color:#37b24d;font-weight:600">200 OK</code> met een object met de properties `gemiddelde`, `laagste` en `hoogste`, drie `double`'s |
| <span style="background:#1c7ed6;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>GET</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/ventilator/prijzen/verlichting/<span style="color:#e64980"><b>{verlichting}</b></span></code> | <code style="color:#37b24d;font-weight:600">200 OK</code> met een object met een property `gemiddelde`, een `double` |
| <span style="background:#1c7ed6;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>GET</b></span> <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap">api/ventilator/historiek/tellen/<span style="color:#e64980"><b>{actie}</b></span></code> | <code style="color:#37b24d;font-weight:600">200 OK</code> met een object met een property `count`, een `int` |

Vinden ze niets, dan staat er `0` in het object, met <code style="color:#37b24d;font-weight:600">200 OK</code>. Log elk verzoek op niveau Information.

<code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> api/ventilator/prijzen</code> geeft met de startgegevens als resultaat een `gemiddelde` dat begint met `27.42`, en:

```json
{
  "gemiddelde": 27.4225,
  "laagste": 19.99,
  "hoogste": 34.9
}
```

> [!NOTE]
> Rond een gemiddelde niet af. De testen bij de schoolopgaven controleren een gemiddelde tussen twee grenzen, zoals 28,45 en 28,46. Een gemiddelde dat je zelf afrondt, kan daar net buiten vallen. Een `double` kan bij een gemiddelde wel een staartje ver achter de komma tonen. Vergelijk daarom enkel de eerste cijfers.

---

### 6. Alles samen: cijfers die kloppen

<span style="background:#7048e8;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>SAMENGESTELDE OEFENING</b></span>

De repositories, de controller en de cijfers komen samen in één reeks. Elke actie verandert de cijfers, en een actie die mislukt verandert niets.

#### Testscenario

Begin met een database die exact de startgegevens bevat: verwijder ze, en werk ze opnieuw bij met je migraties. Bedragen vergelijk je als getal.

1. <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> api/ventilator/prijzen/verlichting/GEEN</code> geeft een `gemiddelde` dat begint met `27.4`. <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> api/ventilator/prijzen/verlichting/neon</code> geeft `{ "gemiddelde": 0 }`, en geen <code style="color:#f03e3e;font-weight:600">500</code>.
2. Voeg een ventilator toe van `Arctic`, 120 mm, zonder verlichting (`geen`), voor 14.9. Je krijgt <code style="color:#37b24d;font-weight:600">201</code>, met Id 5.
3. <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> api/ventilator/prijzen</code> geeft een `gemiddelde` dat begint met `24.91`, `laagste` `14.9` en `hoogste` `34.9`.
4. Werk ventilator 99 bij. Je krijgt <code style="color:#f08c00;font-weight:600">404</code> met <code style="color:#845ef7">Kan geen ventilator bijwerken met Id 99, omdat deze niet bestaat.</code>
5. Vervang ventilator 3 door een ventilator van `Corsair`, 120 mm, met `RGB`, voor 39.9. Je krijgt <code style="color:#37b24d;font-weight:600">201</code>, met Id 6, en een header `Location` die eindigt op `/api/ventilator/6`.
6. <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> api/ventilator/prijzen</code> geeft `hoogste` `39.9`.
7. Voeg een ventilator van 92 mm toe, met verder geldige gegevens. Je krijgt <code style="color:#f08c00;font-weight:600">400</code> met <code style="color:#845ef7">We verkopen enkel ventilatoren van 120 of 140 mm.</code>
8. <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> api/ventilator/historiek/tellen/toegevoegd</code> geeft `{ "count": 2 }`, met `VERWIJDERD` krijg je `{ "count": 1 }`, en met `bijgewerkt` `{ "count": 0 }`.
9. Stop je API en start hem opnieuw. De historiek bevat drie wijzigingen: `toegevoegd` 5, `verwijderd` 3 en `toegevoegd` 6, in die volgorde.

> [!WARNING]
> Is je telling in stap 8 te hoog? Dan laat een mislukte actie toch een wijziging achter. Kijk na waar je controller de wijziging bewaart.
