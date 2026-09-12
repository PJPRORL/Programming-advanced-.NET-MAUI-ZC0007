# Feedback Navigatie: MauiIntroductie vs MauiOefeningen2

Deze README bevat feedback en observaties over waarom de navigatie in je `MauiOefeningen2` project (naar de `PersoonDetailPage`) niet correct werkt. De observaties zijn gebaseerd op een vergelijking met je werkende `MauiIntroductie` project (waar de navigatie naar `DetailsPage` wel functioneert).

Gebruik deze tips om zelfstandig de problemen in je code op te lossen!

---

## 1. Dependency Injection (Registratie in MauiProgram.cs)

**Observatie in MauiIntroductie:**
In `MauiProgram.cs` zie je dat zowel de lijstpagina als de detailpagina netjes zijn geregistreerd in de service container (bijvoorbeeld met `AddTransient` of `AddSingleton`):
```csharp
// Detailspage Werknemers
builder.Services.AddTransient<DetailsPage>();
builder.Services.AddTransient<DetailsViewModel>();
```

**Observatie in MauiOefeningen2:**
In `MauiOefeningen2/MauiProgram.cs` zijn je `PersoonPage` en `PersoonViewModel` wel geregistreerd (onder Hoofdstuk 5), maar **`PersoonDetailPage` (en een eventueel bijhorend ViewModel) ontbreken**.
* **Tip**: Als je in .NET MAUI Shell navigeert naar een pagina die zijn eigen afhankelijkheden via de constructor opvraagt, moet deze pagina eerst geregistreerd zijn in `MauiProgram.cs`. 

---

## 2. Navigatie Commando's: Waar horen ze thuis?

**Observatie in MauiIntroductie:**
Het afhandelen van de klikgebeurtenis gebeurt in het `WerknemerViewModel` via een `[RelayCommand]`:
```csharp
[RelayCommand]
private async Task GoToDetails()
{
    // ... controle en GoToAsync()
}
```

**Observatie in MauiOefeningen2:**
In je `MauiOefeningen2` heb je een `[RelayCommand]` op een methode gezet, maar je hebt dit gedaan in de **Code-Behind van je View** (`PersoonPage.xaml.cs`). 
```csharp
[RelayCommand]
public async Task GoToPersoon()
```
* **Tip**: Een `[RelayCommand]` attribuut uit de CommunityToolkit.Mvvm genereert enkel een ICommand als de klasse erft van `ObservableObject`. Een `ContentPage` (zoals `PersoonPage`) doet dit standaard niet, waardoor deze code niet werkt zoals bedoeld. Verplaats de logica voor navigatie naar je `PersoonViewModel.cs` zodat je mooi het MVVM-patroon volgt, net zoals je in de Introductie hebt gedaan.

---

## 3. De Klik Triggeren in de XAML

**Observatie in MauiIntroductie:**
Zelfs als je een navigatie-commando hebt, moet je XAML weten _wanneer_ dat commando moet worden uitgevoerd. In `WerknemerPage.xaml` zie je dat er aan elk element uit de `CollectionView` een `TapGestureRecognizer` hangt die het ViewModel-commando aanroept en de geselecteerde werknemer doorgeeft:
```xml
<Grid.GestureRecognizers>
    <TapGestureRecognizer
        Command="{Binding GoToDetailsCommand, Source={RelativeSource AncestorType={x:Type viewmodel:WerknemerViewModel}}}"
        CommandParameter="{Binding .}"/>
</Grid.GestureRecognizers>
```

**Observatie in MauiOefeningen2:**
In je `PersoonPage.xaml` heb je in je `CollectionView` een mooie `DataTemplate` gemaakt (met de verschillende labels), maar **er is geen enkele trigger (zoals een GestureRecognizer of een SelectionChanged-event)** om te vertellen dat er op een item geklikt is. 
* **Tip**: Voeg een actie toe aan je item template zodat je op een rij kan klikken. Dit triggert dan vervolgens het commando dat je in tip 2 naar het ViewModel hebt verplaatst.

---

## 4. Gegevens Doorgeven en Ontvangen (Optioneel)

Zodra je de vorige drie punten hebt opgelost, zal de applicatie succesvol naar je nieuwe scherm navigeren. 

Kijk daarna nog even naar hoe het werkende project de gegevens van het geselecteerde item meeneemt:
1. In je **Introductie ViewModel** roep je `Shell.Current.GoToAsync` op en stuur je een _Dictionary_ mee met de gekozen werknemer.
2. In je **Introductie Detail ViewModel** gebruik je het attribuut `[QueryProperty]` bovenaan je klasse om aan te geven hoe je deze data weer moet oppikken.

Controleer of je deze twee stappen ook hebt toegepast voor je Persoon wanneer je detailpagina wordt ingeladen in `MauiOefeningen2`.

Succes met het debuggen!
