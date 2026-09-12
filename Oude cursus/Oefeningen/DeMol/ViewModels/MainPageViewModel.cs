
namespace DeMol.ViewModels
{
    [QueryProperty(nameof(Deelnemer),"Deelnemer")]
    [QueryProperty(nameof(Deelnemers),"Deelnemers")]
    public partial class MainPageViewModel:BaseViewModel
    {
        [ObservableProperty]
        ObservableCollection<Deelnemer> deelnemers;

        [ObservableProperty]
        Deelnemer deelnemer;

        private readonly IDeelnemerRepository _deelnemerRepository;

        public MainPageViewModel(IDeelnemerRepository deelnemerRepository)
        {
            Title = "Deelnemers";
            _deelnemerRepository = deelnemerRepository;
            Deelnemers = new ObservableCollection<Deelnemer>(_deelnemerRepository.GetDeelnemers());
            Deelnemer = new();
        }

        [RelayCommand]
        private async Task GoToVragen()
        {
            if (Deelnemer == null || Deelnemers== null || Deelnemer.NietIngevuld==false)
                return;
            await Shell.Current.GoToAsync(nameof(VraagPage), true, new Dictionary<string, object> {
                {"Deelnemer",Deelnemer },
                {"Deelnemers",Deelnemers}
            });
                   
        }
    }
}
