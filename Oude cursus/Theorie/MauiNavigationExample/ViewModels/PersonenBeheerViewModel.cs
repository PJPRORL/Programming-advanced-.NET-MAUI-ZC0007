using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MauiNavigationExample.Models;
using System.Collections.ObjectModel;
using MauiNavigationExample.Views;

namespace MauiNavigationExample.ViewModels;

public partial class PersonenBeheerViewModel : BaseViewModel
{
    [ObservableProperty]
    private string voornaam;

    [ObservableProperty]
    private DateTime geboortedatum = DateTime.Now;

    public ObservableCollection<Persoon> Personen { get; set; } = new();

    [RelayCommand]
    private void Toevoegen()
    {
        if (!string.IsNullOrWhiteSpace(Voornaam))
        {
            Personen.Add(new Persoon 
            { 
                Voornaam = this.Voornaam, 
                Geboortedatum = this.Geboortedatum 
            });
            Voornaam = string.Empty;
            Geboortedatum = DateTime.Now;
        }
    }

    [RelayCommand]
    private async Task Tonen()
    {
        // 1. We maken een Dictionary aan voor de data die we willen doorgeven.
        var parameters = new Dictionary<string, object>
        {
            { "MijnPersonenLijst", Personen }
        };

        // 2. We navigeren via Shell.Current.GoToAsync
        //    We gebruiken 'nameof(PersonenOverzichtView)' dat via AppShell geregistreerd staat.
        await Shell.Current.GoToAsync(nameof(PersonenOverzichtView), true, parameters);
    }
}
