# MAUI Element Catalog (Uitgebreid)

Deze catalogus bevat een uitgebreid overzicht van .NET MAUI concepten, elementen en theorie-toepassingen, samengesteld uit oefeningen en examenmateriaal. Gebruik dit als ultiem naslagwerk voor de ontwikkeling van applicaties en User Interfaces.

---

## 🏗️ 1. Architectuur & MVVM (CommunityToolkit.Mvvm)

De Model-View-ViewModel (MVVM) architectuur scheidt de logica van de user interface. In combinatie met de `CommunityToolkit.Mvvm` bespaart dit veel "boilerplate" code.

### Belangrijke Attributen in ViewModels (C#)

| Attribuut / Concept | Beschrijving | Voorbeeld (C#) |
|-----------------|--------------|----------------|
| `[ObservableProperty]` | Genereert automatisch INotifyPropertyChanged ondersteuning voor een veld. | `[ObservableProperty] private string _naam;` |
| `[RelayCommand]` | Genereert een IRelayCommand van een method, eventueel asynchroon of met parameters. | `[RelayCommand] private async Task SaveAsync() { ... }` |

### XAML Databinding Syntax

| Attribuut | Type / Concept | Beschrijving | Voorbeeld |
|-----------|---|-------------|---------|
| `BindingContext` | `Object` | De koppeling naar het ViewModel vanuit de View. | `BindingContext="{x:Reference naamViewModel}"` |
| `{Binding ...}` | `Expression` | Verbindt een UI eigenschap aan een property in het ViewModel. | `Text="{Binding Naam}"` |
| `StringFormat` | `String` | Formatteert de gebinde data rechtstreeks in XAML. | `Text="{Binding Prijs, StringFormat='€ {0:F2}'}"` |
| `x:DataType` | `Type` | Compiled bindings. Zorgt voor snellere code en intellisense. | `x:DataType="viewmodels:MijnViewModel"` |

---

## 🧭 2. Navigatie (Shell)

MAUI Shell versimpelt de visuele hiërarchie (Flyout, Tabs) en biedt een URI-gebaseerd navigatiesysteem.

| Onderdeel / Eigenschap | Type | Beschrijving | Voorbeeld (C# of XAML) |
|--------------------|---|-------------|---------|
| `Routing.RegisterRoute()`| `Method` | Registreert unieke pagina routes in AppShell.xaml.cs. | `Routing.RegisterRoute(nameof(DetailPage), typeof(DetailPage));` |
| `Shell.Current.GoToAsync`| `Task` | Navigeert naar de geregistreerde target URI. | `await Shell.Current.GoToAsync(nameof(DetailPage));` |
| `[QueryProperty]` | `Attribute` | Vangt een parameter op die via de navigatie querystring wordt meegegeven. | `[QueryProperty(nameof(ItemId), "id")]` |
| Navigatie parameters | `Dictionary` | Complexere data meenemen tijdens de navigatie. | `await Shell.Current.GoToAsync("...", new Dictionary<string, object> { { "Klant", geselecteerdeKlant } });` |

---

## 💾 3. Data Toegang (Dapper & Repositories)

Om SQL-data op te halen/bewaren, maken we gebruik van Repository patronen in combinatie met Dapper.

### Repository Patroon

| Component | Beschrijving | Voorbeeld |
|-----------|-------------|----------|
| **Interface** (e.g., `IKlantRepository`) | Contract dat bepaalt welke databases-methodes er moeten bestaan. | `Task<IEnumerable<Klant>> GetKlantenAsync();` |
| **Repository Class** | De echte implementatie die met Dapper de queries uitvoert. | `public class KlantRepository : IKlantRepository` |

### Dapper Basis Functionaliteit

| Methode | Beschrijving | Voorbeeld (C#) |
|---------|-------------|---------------|
| `QueryAsync<T>` | Haalt resultaten op als IEnumerable. Geschikt voor SELECT queries, ook voor JOINs. | `await connection.QueryAsync<Klant>("SELECT * FROM Klanten");` |
| `ExecuteAsync` | Voert een CUD bewerking uit (Insert, Update, Delete) en retourneert iteraties. | `await connection.ExecuteAsync("DELETE FROM...", new { Id = id });` |
| Query Parameters | Gebiedt veiligheid (SQL Injection protectie) met anonieme objecten. | `new { UserId = 5 }` op basis van `@UserId` in de query. |

---

## 🎨 4. Geavanceerde Layouts & Resources

Voor een professionele applicatie is styling en hergebruik van kleuren onmisbaar, zoals getoond in het examen.

| Element | Beschrijving | Voorbeeld |
|---------|-------------|----------|
| `ResourceDictionary` | Locatie voor gemeenschappelijke resources zoals kleuren en stijlen. Vaak apart toegevoegd via `MergedDictionaries`. | `<ResourceDictionary Source="Styles/Colors.xaml"/>` |
| `SolidColorBrush` | Eigenschap om kwasten te benoemen, via app statische kleuren. | `<SolidColorBrush x:Key="PrimaryBrush" Color="{StaticResource Primary}"/>` |
| `Style` | Voorgedefinieerde verzameling properties/setters. | `<Style TargetType="Button" x:Key="PrimaryBtn">` |
| `Setter` | Wijs eigenschappen toe in een Style. | `<Setter Property="BackgroundColor" Value="{StaticResource Primary}"/>` |

### VisualStateManager
Biedt states aan componenten afhankelijk van hun modus (bijv. Normaal, Gefocust, Geselecteerd).
```xml
<VisualStateManager.VisualStateGroups>
    <VisualStateGroupList>
        <VisualStateGroup x:Name="CommonStates">
            <VisualState x:Name="Normal" />
            <VisualState x:Name="Selected">
                <VisualState.Setters>
                    <Setter Property="BackgroundColor" Value="LightBlue" />
                </VisualState.Setters>
            </VisualState>
        </VisualStateGroup>
    </VisualStateGroupList>
</VisualStateManager.VisualStateGroups>
```

---

## 🎛️ 5. Nieuwe Data Modellen, Forms & Input Controls

Belangrijke elementen geput uit de CRUD en 'De Mol' opdrachten.

### Geavanceerde CollectionView
Uitbreidingen op de gewone weergave van datasets.

| Property | Type | Beschrijving | Voorbeeld |
|----------|------|-------------|---------|
| `SelectionMode` | `SelectionMode` | Controleert of items selecteerbaar zijn. | `SelectionMode="Single"` |
| `SelectedItem` | `Object` | (Two-way) bindt naar het currently selected item in VM. | `SelectedItem="{Binding GeselecteerdeKlant}"` |
| `SelectionChangedCommand` | `ICommand` | Triggert een opdracht vanuit de interactie (zonder code-behind `Event`). | `SelectionChangedCommand="{Binding GaNaarDetailCommand}"` |

### Picker (Dropdown)
Maak een selectie binnen gekoppelde SQL-data (b.v. een Publisher of Land kiezen).

| Property | Type | Beschrijving | Voorbeeld |
|----------|------|-------------|---------|
| `ItemsSource` | `IEnumerable` | De lijst opties zichtbaar in de picker. | `ItemsSource="{Binding LandenLijst}"` |
| `ItemDisplayBinding` | `BindingBase` | Bepaalt de tekst/property die visueel getoond wordt. | `ItemDisplayBinding="{Binding LandNaam}"` |
| `SelectedItem` | `Object` | Welk object door de gebruiker gekozen is. | `SelectedItem="{Binding GeselecteerdLand}"` |

### Switch, RadioButton & CheckBox
Vaak gebruikt in enquêtes / vragenformulieren.

| Control | Beschrijving | Belangrijkste Properties |
|---------|-------------|-----------------------|
| `RadioButton` | Keuze uit één enkele optie binnen een groep. | `Content="Optie 1"`, `GroupName="Q1"`, `IsChecked="{Binding IsOptieGekozen}"` |
| `CheckBox` | Ter goedkeuring van individuele statussen (Ja/Nee). | `IsChecked="{Binding Akkoord}"` |
| `Switch` | Een aan/uit slider, geschikt voor configuratie/settings (bool values). | `IsToggled="{Binding IsActief}"` |

---

<br/><hr/><br/>

## 📚 6. Basis .NET MAUI Catalogus (Standaard Elementen)

*Dit is de fundamentele theorie en de originele catalogus voor de absolute basis properties en attributen.*

### 📐 Common Layout Properties
These properties apply to almost all visual elements (Layouts and Controls).

| Property | Type | Description | Example |
|----------|------|-------------|---------|
| `Margin` | `Thickness` | Space *outside* the element. | `Margin="10"` or `Margin="10,5,10,5"` |
| `Padding` | `Thickness` | Space *inside* the element (mostly for Layouts). | `Padding="20"` |
| `BackgroundColor` | `Color` | Background color of the element. | `BackgroundColor="LightGray"` |
| `WidthRequest` | `Double` | Desired width. | `WidthRequest="100"` |
| `HeightRequest` | `Double` | Desired height. | `HeightRequest="50"` |
| `HorizontalOptions` | `LayoutOptions` | Alignment horizontally. | `Start`, `Center`, `End`, `Fill` |
| `VerticalOptions` | `LayoutOptions` | Alignment vertically. | `Start`, `Center`, `End`, `Fill` |
| `IsVisible` | `Boolean` | Shows or hides the element. | `IsVisible="True"` |
| `Opacity` | `Double` | Transparency (0.0 to 1.0). | `Opacity="0.5"` |

---

### 📦 Layouts

**VerticalStackLayout / HorizontalStackLayout**
Organizes children sequentially.

| Property | Type | Description |
|----------|------|-------------|
| `Spacing` | `Double` | Space between children. |

**Grid**
Organizes children in rows and columns.

| Property | Type | Description | Example |
|----------|------|-------------|---------|
| `RowDefinitions` | `String` | Height of rows. | `RowDefinitions="Auto, *, 100"` |
| `ColumnDefinitions` | `String` | Width of columns. | `ColumnDefinitions="*, 2*"` |
| `RowSpacing` | `Double` | Space between rows. | `RowSpacing="10"` |
| `ColumnSpacing` | `Double` | Space between columns. | `ColumnSpacing="10"` |

**Attached Properties (on children):**
- `Grid.Row="0"`
- `Grid.Column="1"`
- `Grid.RowSpan="2"`
- `Grid.ColumnSpan="2"`

---

### 🎮 Controls

**Label**
Displays text.

| Property | Type | Description |
|----------|------|-------------|
| `Text` | `String` | The text to display. |
| `TextColor` | `Color` | Color of the text. |
| `FontSize` | `Double` | Size of the text. |
| `FontAttributes` | `FontAttributes` | `Bold`, `Italic`, `None`. |
| `HorizontalTextAlignment` | `TextAlignment` | `Start`, `Center`, `End`. |
| `VerticalTextAlignment` | `TextAlignment` | `Start`, `Center`, `End`. |
| `LineBreakMode` | `LineBreakMode` | `NoWrap`, `WordWrap`, `TailTruncation`. |

**Button**
Clickable button.

| Property | Type | Description |
|----------|------|-------------|
| `Text` | `String` | Button label. |
| `Command` | `ICommand` | Bindable command to execute on click. |
| `CommandParameter` | `Object` | Parameter to pass to the command. |
| `Clicked` | `Event` | Event handler for click (Code-behind). |
| `CornerRadius` | `Int` | Roundness of corners. |
| `BorderColor` | `Color` | Color of the border. |
| `BorderWidth` | `Double` | Width of the border. |
| `ImageSource` | `ImageSource` | Icon to display on the button. |

**Entry**
Single-line text input.

| Property | Type | Description |
|----------|------|-------------|
| `Text` | `String` | The input text (Two-way binding). |
| `Placeholder` | `String` | Hint text when empty. |
| `PlaceholderColor` | `Color` | Color of placeholder text. |
| `IsPassword` | `Boolean` | Hides text (for passwords). |
| `Keyboard` | `Keyboard` | `Text`, `Numeric`, `Email`, `Telephone`. |
| `IsReadOnly` | `Boolean` | Prevents editing. |
| `MaxLength` | `Int` | Max characters allowed. |
| `ReturnType` | `ReturnType` | `Done`, `Next`, `Search`, `Send`. |

**Image**
Displays an image.

| Property | Type | Description |
|----------|------|-------------|
| `Source` | `ImageSource` | URL or filename of the image. |
| `Aspect` | `Aspect` | `AspectFit`, `AspectFill`, `Fill`. |

---

### 📚 Data Views

**CollectionView**
Displays a list of items.

| Property | Type | Description |
|----------|------|-------------|
| `ItemsSource` | `IEnumerable` | List of data to display. |
| `SelectionMode` | `SelectionMode` | `None`, `Single`, `Multiple`. |
| `EmptyView` | `Object` | Content to show when list is empty. |
| `Header` | `Object` | Content at the top of the list. |
| `Footer` | `Object` | Content at the bottom of the list. |

**ItemTemplate:**
Defines how each item looks.
```xml
<CollectionView.ItemTemplate>
    <DataTemplate>
        <StackLayout>
            <Label Text="{Binding Name}" />
        </StackLayout>
    </DataTemplate>
</CollectionView.ItemTemplate>
```
