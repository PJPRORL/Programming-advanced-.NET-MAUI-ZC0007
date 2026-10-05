# 04_02

<b>Hoofdstuk 04</b> &nbsp;·&nbsp; Twee tabellen, één context &nbsp;·&nbsp; PC-onderdelen &nbsp;·&nbsp; niveau <span style="color:#f08c00">●●○</span> &nbsp;·&nbsp; ±90 min

## Leerdoel

Na deze oefening kan je meer dan één model naar dezelfde database brengen, via één DbContext en meerdere migraties.

Je leert dat de regels in je database en de regels in je API moeten overeenkomen: een tekst die te lang is voor de database, moet je API al eerder netjes weigeren.

Daarnaast zorg je ervoor dat je API en je database hetzelfde verhaal vertellen, zodat de overstap in les 05 niets verandert voor wie je API gebruikt.

## 

## Opdracht

De ventilator-API van Bit & Byte uit 03_02 werkt met twee repositories in het geheugen: één voor de ventilatoren en één voor de historiek. Beide moeten later naar een database verhuizen.

Jouw taak is om naast je bestaande API een database op te zetten met een tabel voor de ventilatoren en een tabel voor de wijzigingen, en je API zo aan te passen dat hij dezelfde regels volgt als die database.

In deze oefening:

1. zet je een database op en koppel je je project eraan;
2. leg je de tabel voor de ventilatoren vast en maak je ze via een migratie;
3. doe je hetzelfde voor de wijzigingen, in een tweede migratie;
4. zet je de startgegevens in de database én in je API;
5. laat je je API weigeren wat de database zou weigeren.

> [!NOTE]
> Je API gebruikt de database in deze les nog niet. Hij blijft werken met je twee in-memory repositories. De overstap gebeurt in les 05.

Implementeer onderstaande functionaliteiten.

### 1. Je vertrekpunt

Vertrek van je eigen oplossing van 03_02. Alle endpoints uit die oefening blijven werken zoals ze nu werken.

Vanaf nu liggen de modellen vast, met deze C#-types:

| Ventilator   | C#-type   |     | Wijziging    | C#-type  |
|--------------|-----------|-----|--------------|----------|
| Id           | `int`     |     | Id           | `int`    |
| Merk         | `string`  |     | Actie        | `string` |
| AfmetingInMm | `int`     |     | VentilatorId | `int`    |
| Verlichting  | `string`  |     |              |          |
| Prijs        | `decimal` |     |              |          |

> [!TIP]
> Was een bedrag bij jou een `double`, maak er dan nu een `decimal` van. Een `double` kan een bedrag als 0,10 niet exact bewaren, een `decimal` wel. Voor geld kies je daarom altijd `decimal`.

---

### 2. De database en de koppeling

Gebruik je PostgreSQL-container uit les 04, of start er een. Deze oefening krijgt een eigen database met de naam `VentilatorDb`.

Installeer de packages uit les 04 en zet in `appsettings.json` een connectiestring met de naam `PostgresConnection`, die naar `VentilatorDb` wijst.

Maak een map Data, met daarin een klasse `VentilatorContext` die erft van `DbContext`, en registreer ze in Program.cs.

---

### 3. De tabel voor de ventilatoren

Voeg aan je context een `DbSet<Ventilator>` toe met de naam `Ventilatoren`, en leg met de Fluent API vast:

| Property     | Kolom in de database                                   |
|--------------|--------------------------------------------------------|
| Id           | de sleutel, de database telt zelf op                   |
| Merk         | tekst van hoogstens 50 tekens, verplicht               |
| AfmetingInMm | geheel getal, verplicht                                |
| Verlichting  | tekst van hoogstens 20 tekens, verplicht               |
| Prijs        | getal met 8 cijfers, waarvan 2 na de komma, verplicht  |

De tabel heet `Ventilatoren`.

Maak een migratie met de naam `VentilatorTabel` en werk de database bij.

---

### 4. De tabel voor de wijzigingen

Voeg daarna een `DbSet<Wijziging>` toe met de naam `Wijzigingen`:

| Property     | Kolom in de database                     |
|--------------|------------------------------------------|
| Id           | de sleutel, de database telt zelf op     |
| Actie        | tekst van hoogstens 20 tekens, verplicht |
| VentilatorId | geheel getal, verplicht                  |

De tabel heet `Wijzigingen`.

Maak een tweede migratie met de naam `WijzigingTabel` en werk de database opnieuw bij.

> [!NOTE]
> `VentilatorId` verwijst naar een ventilator, maar de database weet dat nog niet: er is geen relatie tussen de twee tabellen. Relaties komen in les 06. Een wijziging moet bovendien kunnen blijven bestaan voor een ventilator die al verwijderd is.

> [!TIP]
> Open de tweede migratie voor je de database bijwerkt. Ze mag enkel over de tabel `Wijzigingen` gaan. Zie je er ook iets van de ventilatoren in terug, dan is er tussen de twee migraties iets aan je model veranderd.

---

### 5. De startgegevens

Zet deze vier ventilatoren in de tabel `Ventilatoren`, met een SQL-script dat je zelf schrijft:

| Id | Merk      | AfmetingInMm | Verlichting | Prijs |
|----|-----------|--------------|-------------|-------|
| 1  | Noctua    | 120          | geen        | 29.90 |
| 2  | be quiet! | 140          | geen        | 24.90 |
| 3  | Corsair   | 120          | RGB         | 34.90 |
| 4  | Arctic    | 140          | ARGB        | 19.99 |

De tabel `Wijzigingen` blijft leeg.

Zet ook de startlijst van je `InMemoryVentilatorRepository` gelijk aan deze tabel. Je `InMemoryWijzigingRepository` begint leeg, zoals in 03_02.

> [!TIP]
> Schrijf de bedragen in je startlijst met twee cijfers na de komma, zoals in de tabel. Een `decimal` onthoudt hoeveel cijfers na de komma je schreef, en dat zie je terug in de JSON: `34.9m` en `34.90m` zijn even groot, maar je API toont ze anders. De database geeft ze altijd met twee cijfers terug.

> [!WARNING]
> Zet je de Id's zelf in je INSERT, dan weet de teller van PostgreSQL daar niets van. Je merkt dat pas in les 05, bij de eerste POST. Denk dus nu al na over hoe je ervoor zorgt dat de Id's in je tabel overeenkomen met de tabel hierboven. Met SQL mag je daarvoor alles doen wat de gegevens raakt, ook de teller zelf bijstellen. Enkel de structuur van de tabel blijft voor migraties.

---

### 6. Alles samen: twee bronnen, één verhaal

<span style="background:#7048e8;color:#fff;padding:1px 7px;border-radius:4px;font-size:90%"><b>SAMENGESTELDE OEFENING</b></span>

Je hebt nu twee plaatsen waar ventilatoren bestaan: het geheugen van je API en je database. In les 05 vervang je de ene door de andere. Dat lukt enkel als ze vandaag al exact hetzelfde vertellen en dezelfde regels volgen.

#### Dezelfde gegevens

Herstart je API en vergelijk het antwoord op <code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> api/ventilator</code> met de inhoud van de tabel `Ventilatoren`. Elk veld van elke ventilator moet gelijk zijn.

<code style="color:#0c8599;background:rgba(34,184,207,.18);white-space:nowrap"><b>GET</b> api/ventilator/3</code> geeft als resultaat:

```json
{
  "id": 3,
  "merk": "Corsair",
  "afmetingInMm": 120,
  "verlichting": "RGB",
  "prijs": 34.90
}
```

#### Dezelfde regels

Je database weigert een merk van meer dan 50 tekens en een verlichting van meer dan 20 tekens. Doet je API dat niet, dan krijgt de client in les 05 een <code style="color:#f03e3e;font-weight:600">500</code> in plaats van een nette fout.

Voeg daarom aan de controles uit 03_02 deze twee toe, bij toevoegen, bijwerken en vervangen:

- het merk is langer dan 50 tekens: <code style="color:#f08c00;font-weight:600">400 Bad Request</code> met <code style="color:#845ef7">Het merk mag hoogstens 50 tekens lang zijn.</code>
- de verlichting is langer dan 20 tekens: <code style="color:#f08c00;font-weight:600">400 Bad Request</code> met <code style="color:#845ef7">De verlichting mag hoogstens 20 tekens lang zijn.</code>

Een lege verlichting blijft toegelaten, zoals in 03_02. Is een verzoek op meer dan één punt ongeldig, dan kies je zelf welk bericht je teruggeeft.

> [!NOTE]
> Ook een bedrag heeft in de database een grens: hoeveel cijfers het mag tellen. Met de prijs van een ventilator kom je daar nooit in de buurt, dus die laat je hier rusten.

> [!IMPORTANT]
> Ook een geweigerde ventilator laat geen spoor na in de historiek, net als in 03_02.

#### Testscenario

Herstart je API, zodat je met de startgegevens en een lege historiek begint. Alle ventilatoren die je in deze reeks doorstuurt, zijn op elk ander punt geldig.

1. Vraag de historiek op. Ze is leeg.
2. Voeg een ventilator toe met een merk van precies 50 tekens. Je krijgt <code style="color:#37b24d;font-weight:600">201</code>.
3. Voeg een ventilator toe met een merk van 51 tekens. Je krijgt <code style="color:#f08c00;font-weight:600">400</code> met <code style="color:#845ef7">Het merk mag hoogstens 50 tekens lang zijn.</code>
4. Werk ventilator 1 bij met een verlichting van 21 tekens. Je krijgt <code style="color:#f08c00;font-weight:600">400</code> met <code style="color:#845ef7">De verlichting mag hoogstens 20 tekens lang zijn.</code> Vraag ventilator 1 op: hij is niet veranderd.
5. Vraag de historiek op. Ze bevat één wijziging: die van stap 2.
6. Controleer in je database dat de geschiedenis van je migraties precies twee migraties bevat, `VentilatorTabel` en `WijzigingTabel`, en dat beide tabellen bestaan met de kolommen uit punt 3 en 4.

> [!NOTE]
> De ventilator uit stap 2 staat enkel in het geheugen van je API, niet in je database. Dat is normaal in deze les. Herstart je API, dan vertellen ze weer hetzelfde verhaal.
