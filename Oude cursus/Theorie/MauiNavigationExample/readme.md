# .NET MAUI Navigatie Voorbeeld: Persoon Oefening

Dit project (`MauiNavigationExample`) bevat de uitwerking voor de "Persoon" oefening. In dit **Readme** bestand leggen we stap voor stap uit hoe we het project hebben opgebouwd, en specifieker nog, hoe de navigatie en de datadoorgifte binnen .NET MAUI hier werken.

## 1. Projectopzet en Structuur

We hebben het project gebaseerd op het MVVM (Model-View-ViewModel) patroon met behulp van de handige **CommunityToolkit.Mvvm** package. 
Onze mappenstructuur bestaat uit:
* **Models**: Voor onze domein logica (de klasse `Persoon`).
* **ViewModels**: De presentatielogica waar .NET MAUI mee linkt.
* **Views**: De visuele schermen (XAML).

### Het Model
De klasse `Persoon` (in `Models/Persoon.cs`) is eenvoudig en definieert wat een persoon is in onze app:
```csharp
public class Persoon
{
    public string Voornaam { get; set; }
    public DateTime Geboortedatum { get; set; } = DateTime.Now;
}
```

## 2. Navigatiestructuur opzetten (AppShell)

Onze navigatie vertrekt vanuit `AppShell.xaml`. Dit werkt als het "hoofdmenu" en raamwerk van je app. 
Voor de allereerste (hoofd)weergave registreren we een vaste tab of route in onze XAML:

```xml
<ShellContent
    Title="Beheer"
    ContentTemplate="{DataTemplate views:PersonenBeheerView}"
    Route="PersonenBeheerView" />
```

Naast hoofdpagina's heb je ook schermen waar je pas *tijdens* het werken met de applicatie naar wilt doorklikken. Denk hierbij aan ons **Personen Overzicht** (of een detailpagina). Omdat die niet standaard in de opstart naviagtie zit, moeten we die intern "bekend maken" of registreren aan het systeem. 
De code dat specifieke routes registreert, staat in `AppShell.xaml.cs`:

> [!NOTE] 
> Let goed op de `Routing.RegisterRoute`! Als je deze vergeet, snapt het `GoToAsync` systeem niet waarnaar hij moet verwijzen.

```csharp
public AppShell()
{
    InitializeComponent();
    
    // Registreer routes naar pagina's die niet rechtstreeks in het vaste menu staan.
    Routing.RegisterRoute(nameof(PersonenOverzichtView), typeof(PersonenOverzichtView));
}
```


## 3. Navigeren en Argumenten doorgeven

In de **`PersonenBeheerViewModel`** voegen we via een RelayCommand met input personen toe aan een `ObservableCollection<Persoon> Personen`. Op het moment dat de gebruiker op de knop "Toon in Overzicht" drukt, vuren we de volgende method af:

```csharp
[RelayCommand]
private async Task Tonen()
{
    // 1. Sleutel / Waarde paren maken
    var parameters = new Dictionary<string, object>
    {
        { "MijnPersonenLijst", Personen } 
    };

    // 2. We geven deze Dictionary mee in Shell navigatie!
    await Shell.Current.GoToAsync(nameof(PersonenOverzichtView), true, parameters);
}
```

> [!TIP]
> Navigeren zonder parameters kan makkelijk met `await Shell.Current.GoToAsync(nameof(JeViewNaam));`. Omdat wij echter data (`PersonenLijst`) willen afgeven aan het volgende scherm, pakken we alle properties in via een flexibel `Dictionary<string, object>`.  De `true` geeft hierbij aan dat de 'Terug' animatie en historiek moet worden bijgehouden.

## 4. Gegevens ontvangen in nieuwe View

Zodra je met `.GoToAsync` genavigeerd bent, is de volgende vraag: hoe luistert `PersonenOverzichtView` om de data op te vangen?

Dit wordt in MAUI gedaan via het Data Annotation attribuut: **`[QueryProperty]`**.
Bovenaan in `PersonenOverzichtViewModel.cs` zetten we:

```csharp
[QueryProperty(nameof(PersonenLijst), "MijnPersonenLijst")]
public partial class PersonenOverzichtViewModel : BaseViewModel
{
    [ObservableProperty]
    private ObservableCollection<Persoon> personenLijst;
    
    //...
```

* `nameof(PersonenLijst)` refereert naar de *ontvangende* Property in je Model.
* `"MijnPersonenLijst"` is de exact matchende *naam/key* die je in je verzendende Dictionary hebt genoteerd.


### Databinding triggeren na het openen

Als de route geladen wordt, triggert de CommunityToolkit volautomatisch functionaliteit bij de property updates. Met de methode `OnPersonenLijstChanged(...)` vangen we netjes het moment af waarop de `PersonenLijst` gevuld wordt vanuit de UI en berekenen we on the fly het gemiddelde.

```csharp
partial void OnPersonenLijstChanged(ObservableCollection<Persoon> value)
{
    if (value != null && value.Any())
    {
        var totaalJaren = value.Sum(p => (DateTime.Now - p.Geboortedatum).Days / 365.25);
        GemiddeldeLeeftijd = totaalJaren / value.Count;
    }
    // ...
}
```

En dit geheel stelt de basisflow van Shell.Current navigatie model voor .NET MAUI voor!
