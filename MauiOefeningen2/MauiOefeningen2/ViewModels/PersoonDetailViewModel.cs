using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiOefeningen2.ViewModels
{
    [QueryProperty(nameof(GeselecteerdePersoon), "PersoonObject")]
    public partial class PersoonDetailViewModel : ViewModel
    {
        private Persoon _geselecteerdePersoon;

        public Persoon GeselecteerdePersoon
        {
            get => _geselecteerdePersoon;
            set
            {
                _geselecteerdePersoon = value;
                OnPropertyChanged();
            }
        }

        public PersoonDetailViewModel()
        {
            Title = "Personen tonen";
        }
    }
}
