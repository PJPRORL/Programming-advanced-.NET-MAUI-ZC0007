using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeMol.ViewModels
{
    public partial class VraagPageViewModel : BaseViewModel
    {
        private readonly IVraagRepository _vraagRepository;
        private readonly IDeelnemerRepository _deelnemerRepository;
        private List<string> _vragen;
        private int _currentVraagIndex = 0;

        [ObservableProperty]
        string currentVraag;

        [ObservableProperty]
        ObservableCollection<Deelnemer> opties;

        [ObservableProperty]
        Deelnemer selectedAntwoord;

        public VraagPageViewModel(IVraagRepository vraagRepository, IDeelnemerRepository deelnemerRepository)
        {
            _vraagRepository = vraagRepository;
            _deelnemerRepository = deelnemerRepository;
            Title = "Vragen";
            
            _vragen = _vraagRepository.GetVragen();
            Opties = new ObservableCollection<Deelnemer>(_deelnemerRepository.GetDeelnemers());
            
            LoadVraag();
        }

        private void LoadVraag()
        {
            if (_vragen != null && _vragen.Count > _currentVraagIndex)
            {
                CurrentVraag = _vragen[_currentVraagIndex];
            }
        }

        [RelayCommand]
        async Task VolgendeVraagAsync()
        {
            if (SelectedAntwoord == null)
                return; // Moet een antwoord selecteren

            _currentVraagIndex++;
            SelectedAntwoord = null;

            if (_currentVraagIndex < _vragen.Count)
            {
                LoadVraag();
            }
            else
            {
                // Navigate to ControlePage
                await Shell.Current.GoToAsync(nameof(ControlePage));
            }
        }
    }
}
