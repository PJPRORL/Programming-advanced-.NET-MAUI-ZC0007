using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeMol.ViewModels
{
    public partial class ControleViewModel : BaseViewModel
    {
        [ObservableProperty]
        string verdachteNaam;

        [RelayCommand]
        async Task ControleerAsync()
        {
            if (string.IsNullOrWhiteSpace(VerdachteNaam))
                return;
                
            await Shell.Current.DisplayAlert("Controle", $"Je verdenkt {VerdachteNaam}.", "OK");
            
            // Navigate back to start or whatever the flow is.
            // await Shell.Current.GoToAsync("//MainPage");
        }
    }
}
