using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.Input;

namespace MauiOefeningen2.ViewModels
{
    public partial class VragenlijstViewModel : BaseViewModel
    {
        [ObservableProperty]
        private string name = string.Empty;

        [ObservableProperty]
        private string resultaat;

        [ObservableProperty]
        private int getal;

        [ObservableProperty]
        private bool isNameGiven = false;

        public ObservableCollection<string> Eten { get; } = new() { "Ja", "Nee" };
        public ObservableCollection<string> Drinken { get; } = new() { "Ja", "Nee" };

        [ObservableProperty]
        private string selectedEten;

        [ObservableProperty]
        private string selectedDrinken;

        public VragenlijstViewModel()
        {
            OproepenNaam();
        }

        public async Task OproepenNaam()
        {
            do
            {
                Name = await Shell.Current.DisplayPromptAsync("Naam ingeven", "Geef je naam in.", "Doorgaan", "Cancel");

                if (string.IsNullOrEmpty(Name))
                {
                    await Shell.Current.DisplayAlert("Fout", "Je moet verplicht een naam ingeven", "OK");
                }
            }
            while (string.IsNullOrEmpty(Name));

            IsNameGiven = true;
        }

        [RelayCommand]
        public void Controleren()
        {
            if (string.IsNullOrEmpty(SelectedEten) || string.IsNullOrEmpty(SelectedDrinken))
            {
                Shell.Current.DisplayAlert("Fout", "Maak een keuze bij alle vragen", "OK");
                return;
            }

            StringBuilder sb = new StringBuilder();

            sb.Append(SelectedEten == "Ja" ? "Je eet voldoende, " : "Je eet onvoldoende. ");
            sb.Append(SelectedDrinken == "Ja" ? "Je drinkt voldoende, " : "Je drinkt onvoldoende. ");

            string sportStatus = Getal switch
            {
                <= 2 => "onvoldoende",
                <= 4 => "matig",
                <= 7 => "voldoende"
            };

            sb.Append($" en je sport {sportStatus}.");

            Resultaat = sb.ToString();
        }

        [RelayCommand]

        public async void Herstarten()
        {
            SelectedEten = null;
            SelectedDrinken = null;
            Getal = 0;
            Resultaat = string.Empty;
        }
    }
}