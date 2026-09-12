using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiIntroductie.ViewModels
{
    [QueryProperty(nameof(Werknemer), "Werknemer")]

    public partial class DetailsViewModel : BaseViewModel
    {
        [ObservableProperty]
        Werknemer werknemer;

        partial void OnWerknemerChanged(Werknemer value)
        {
            if (value == null)
            {
                Shell.Current.DisplayAlert("Fout", "De werknemer is niet correct geladen", "Doorgaan");
            }
        }
    }
}
