using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiIntroductie.ViewModels
{
    public partial class WerknemerViewModel : BaseViewModel
    {
        // Properties
        [ObservableProperty]
        ObservableCollection<Functie> functies;

        [ObservableProperty]
        Werknemer werknemer;

        [ObservableProperty]
        public ObservableCollection<Werknemer> werknemers;

        [ObservableProperty]
        string resultaat;

        private readonly IWerknemerRepository _werknemerRepository;
        private readonly IFunctieRepository _functieRepository;


        // Constructor
        public WerknemerViewModel(IWerknemerRepository werknemerRepository, IFunctieRepository functieRepository)
        {
            Title = "Werknemer toevoegen";
            _werknemerRepository = werknemerRepository;
            _functieRepository = functieRepository;
            Werknemers = new ObservableCollection<Werknemer>(_werknemerRepository.GetWerknemers());
            Functies = new ObservableCollection<Functie> (_functieRepository.GetFuncties());
            Werknemer = new();
        }

        [RelayCommand]
        // Methods
        public async Task WerknemerToevoegen()
        {
            if (string.IsNullOrEmpty(Werknemer.Voornaam) || string.IsNullOrEmpty(Werknemer.Achternaam))
            {
                await Shell.Current.DisplayAlert("Fout", "Voornaam en Achternaam zijn verplicht.", "OK");
                return;
            }

            Werknemers.Add(Werknemer);
            Werknemer = new();
        }

        [RelayCommand]
        private async Task GoToDetails()
        {
            if (werknemer == null)
            {
                return;
            }

            await Shell.Current.GoToAsync(nameof(DetailsPage), true, new Dictionary<string, object>
                {
                    {"Werknemer", werknemer }
                });
        }
    }
}