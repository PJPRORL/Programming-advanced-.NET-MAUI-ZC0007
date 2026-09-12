using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeMol.ViewModels
{
    public partial class MainPageViewModel : BaseViewModel
    {
        private readonly IDeelnemerRepository _deelnemersRepository;

        [ObservableProperty]
        ObservableCollection<Deelnemer> deelnemers;

        [ObservableProperty]
        Deelnemer selectedDeelnemer;

        public MainPageViewModel(IDeelnemerRepository deelnemersRepository)
        {
            Title = "Deelnemers";
            _deelnemersRepository = deelnemersRepository;
            Deelnemers = new ObservableCollection<Deelnemer>(_deelnemersRepository.GetDeelnemers());

        }

        [RelayCommand]
        async Task StartAsync()
        {
            if (SelectedDeelnemer == null)
                return;
                
            await Shell.Current.GoToAsync(nameof(VraagPage));
        }
    }
}
