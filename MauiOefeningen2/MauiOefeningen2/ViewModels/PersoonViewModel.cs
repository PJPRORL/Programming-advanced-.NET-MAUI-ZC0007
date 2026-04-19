using CommunityToolkit.Mvvm.Input;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiOefeningen2.ViewModels
{
    public partial class PersoonViewModel: ViewModel
    {
        [ObservableProperty]
        ObservableCollection<Persoon> personen;

        [ObservableProperty]
        Persoon persoon;

        private readonly IPersoonRepository _persoonRepository;

        public PersoonViewModel(IPersoonRepository persoonRepository)
        {
            this._persoonRepository = persoonRepository;
            this.personen = new ObservableCollection<Persoon>(_persoonRepository.OphalenPersonen());
            this.Persoon = new();
            Title = "Personen Ingeven";
        }

        public async new Task GoToPersoon() =>
            await Shell.Current.GoToAsync(nameof(PersoonPage));

        [RelayCommand]
        public void NieuwePersoon()
        {
            Persoon = new();
        }

        [RelayCommand]
        public void ToevoegenPersoon()
        {
            Personen.Add(Persoon);
            Persoon = new();
        }

        [RelayCommand]
        public void VerwijderPersoon()
        {
            Personen.Remove(Persoon);
        }
    }
}
