using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeMol.ViewModels
{
    [QueryProperty(nameof(Deelnemers),"Deelnemers")]
    public partial class ControleViewModel:BaseViewModel
    {
        [ObservableProperty]
        ObservableCollection<Deelnemer> deelnemers;

        [ObservableProperty]
        Color achtergrondkleur;

        [ObservableProperty]
        string voornaam;

        public int minsteScore;
        public ControleViewModel() {
            Title = "Controle";
            Voornaam = string.Empty;
            Achtergrondkleur = Colors.Beige;
            minsteScore = 0;
        }

        partial void OnDeelnemersChanged(ObservableCollection<Deelnemer> value)
        {
            minsteScore = Deelnemers.Min(x => x.Score);
        }

        [RelayCommand]
        private void Controleer()
        {
            if (Voornaam == null) return;

            Deelnemer deelnemer = Deelnemers.FirstOrDefault(x=>x.Voornaam == Voornaam);
            if(deelnemer.Score == minsteScore && deelnemer.Rol != Rol.Mol )
            {
                Achtergrondkleur = Colors.Red;
            }
            else
            {
                Achtergrondkleur = Colors.Green;
            }
        }
    }
}
