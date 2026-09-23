# 01_01

## Leerdoel

Na deze oefening kan je een eenvoudige ASP.NET Core API-controller maken met verschillende GET-endpoints.

Je leert routes definiëren, waarden uit een URL als parameter ontvangen en deze waarden gebruiken in gewone C#-logica.

Daarnaast leer je dat een gebruiker van een API jouw C#-methoden niet rechtstreeks aanspreekt. De communicatie verloopt via de HTTP-routes die jij aan je controller en methoden koppelt.

## 

## Opdracht

De fictieve hogeschool Northwind College wil nieuwe studenten verwelkomen met een eenvoudige informatie-API.

Jouw taak is om een CampusController te maken met verschillende GET-endpoints.

Via de API moeten studenten:

1. een algemeen welkomstbericht kunnen opvragen;
2. een persoonlijk welkomstbericht kunnen krijgen;
3. informatie over een campusgebouw kunnen opvragen;
4. advies kunnen krijgen op basis van het aantal minuten tot hun volgende les.

Implementeer onderstaande functionaliteiten.

### 1. Algemeen welkomstbericht

Voorzie een GET-endpoint op:

`/campus`

Dit endpoint geeft volgende tekst terug:

`Welkom bij Northwind College!`

### 2. Persoonlijk welkomstbericht

Voorzie een GET-endpoint op:

`/campus/welkom/{naam}`

De naam van de student wordt meegegeven via de URL.

Bijvoorbeeld:

`GET /campus/welkom/Amina`

geeft als resultaat:

`Welkom bij Northwind College, Amina!`

De naam moet afkomstig zijn uit de routeparameter. Je mag dus geen specifieke studentennamen hardcoderen.

### 3. Informatie over een campusgebouw

Voorzie een GET-endpoint op:

`/campus/gebouw/{gebouw}`

Het endpoint ontvangt de naam van een gebouw via de route.

De API moet drie gebouwen herkennen:

- bibliotheek
- sport
- technologie

Geef voor ieder gebouw het bijbehorende bericht terug:

| Gebouw      | Bericht                                                                 |
|------------|-------------------------------------------------------------------------|
| bibliotheek | In de bibliotheek kan je studeren en boeken ontlenen.
| sport       | In het sportgebouw vind je de fitnessruimte en indoor sportzalen.
| technologie | In het technologiegebouw vind je de computerlokalen.

Wordt een onbekend gebouw opgegeven, geef dan het bericht terug:

`Sorry, we hebben geen informatie over dit gebouw.`

### 4. Advies op basis van het aantal minuten tot de volgende les

Voorzie een GET-endpoint op:

`/campus/les/{minuten}`

De routeparameter `{minuten}` stelt het aantal minuten voor tot de volgende les van de student.

Geef het volgende advies:

- Minder dan 10 minuten: `Ga nu naar je leslokaal.`
- 10 tot en met 30 minuten: `Je hebt nog even tijd voor je les begint.`
- Meer dan 30 minuten: `Je hebt nog ruim voldoende tijd voor je les.`

Bijvoorbeeld:

`GET /campus/les/5` geeft als resultaat: `Ga nu naar je leslokaal.`
`GET /campus/les/15` geeft als resultaat: `Je hebt nog even tijd voor je les begint.`
`GET /campus/les/45` geeft als resultaat: `Je hebt nog ruim voldoende tijd voor je les.`