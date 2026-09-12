using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiOefeningen2.ViewModels
{
    public partial class GetallenRijViewModel : BaseViewModel
    {
        [ObservableProperty]
        private string input = string.Empty;

        [ObservableProperty]
        private string output;

        [ObservableProperty]
        private string getal;

        // lijst || array
        public ObservableCollection<int> Getallen { get; } = new ObservableCollection<int>();

        public GetallenRijViewModel()
        {
            Input = string.Empty;
            Getal = null;
            Output = string.Empty;
        }

        [RelayCommand]
        public async Task VoegGetallenToe()
        {
            if (!int.TryParse(Input, out int TijdelijkGetal) && TijdelijkGetal > 20)
            {
                await Shell.Current.DisplayAlert("Foutieve invoer", "Getal mag niet groter zijn dan 20", "OK");
            }


            Getallen.Add(TijdelijkGetal);

            Output = string.Join(", ", Getallen);
        }
    }
}
