using CommunityToolkit.Mvvm.ComponentModel;
using MauiNavigationExample.Models;
using System.Collections.ObjectModel;
using System.Linq;

namespace MauiNavigationExample.ViewModels;

// 3. Hier vangen we de dictionary property "MijnPersonenLijst" op, 
// en koppelen we die aan de lokale property 'PersonenLijst'
[QueryProperty(nameof(PersonenLijst), "MijnPersonenLijst")]
public partial class PersonenOverzichtViewModel : BaseViewModel
{
    [ObservableProperty]
    private ObservableCollection<Persoon> personenLijst;

    [ObservableProperty]
    private double gemiddeldeLeeftijd;

    // 4. OnPropertyChange handler. Hiermee detecteren we wanneer .NET MAUI 
    // het argument uit de navigatie heeft ingeladen. Dit gebeurt NA de constructor.
    partial void OnPersonenLijstChanged(ObservableCollection<Persoon> value)
    {
        if (value != null && value.Any())
        {
            var totaalJaren = value.Sum(p => (DateTime.Now - p.Geboortedatum).Days / 365.25);
            GemiddeldeLeeftijd = totaalJaren / value.Count;
        }
        else
        {
            GemiddeldeLeeftijd = 0;
        }
    }
}
