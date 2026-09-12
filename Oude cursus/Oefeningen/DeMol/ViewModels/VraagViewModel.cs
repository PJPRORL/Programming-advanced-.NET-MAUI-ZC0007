using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeMol.ViewModels
{
    [QueryProperty(nameof(Deelnemer),"Deelnemer")]
    [QueryProperty(nameof(Deelnemers),"Deelnemers")]
    public partial class VraagViewModel:BaseViewModel
    {
        [ObservableProperty]
        Vraag vraag;

        [ObservableProperty]
        Deelnemer deelnemer;

        [ObservableProperty]
        ObservableCollection<Deelnemer> deelnemers;

        [ObservableProperty]
        int antwoord;

        [ObservableProperty]
        bool isVisibleVolgende, isVisibleHome, isVisibleControle, isVisiblePasVraag, pasVraag;

        public List<Vraag> vragen;

        public int aantalVragen;

        private readonly IVraagRepository _vraagRepository;

        public VraagViewModel(IVraagRepository vraagRepository)
        {
            _vraagRepository = vraagRepository;
            Title = "Vragen";
            vragen = new List<Vraag>(_vraagRepository.GetVragen());
            Vraag = vragen[aantalVragen];
            Reset();
        }

        partial void OnDeelnemerChanged(Deelnemer value)
        {
            if (value.PasVragen > 0)
            {
                IsVisiblePasVraag = true;
            }
        }
        private void Reset() {
            Antwoord = 0;
            aantalVragen = 0;
            IsVisibleVolgende = true;
            IsVisibleHome = false;
            IsVisiblePasVraag = false;
            PasVraag = false;
        }

        [RelayCommand]
        private void VolgendeVraag()
        {
            if (Antwoord == null) return;
            if (Antwoord == Vraag.DeelnemerId )
            {
                Deelnemer.Score++;
            }

            if(PasVraag)
            {   
                Deelnemer.PasVragen--;
                PasVraag = false;
                Deelnemer.Score++;
                if (Deelnemer.PasVragen == 0) IsVisiblePasVraag = false;
            }


            aantalVragen++;

            if (aantalVragen == 5)
            {
                Deelnemers.FirstOrDefault(x => x.Id == Deelnemer.Id).NietIngevuld = false;

                int aantalDeelnemersNietIngevuld = Deelnemers.Where(x => x.NietIngevuld == true).Count();
                if (aantalDeelnemersNietIngevuld!=0)
                {
                    IsVisibleVolgende = false;
                    IsVisiblePasVraag = false;
                    IsVisibleHome = true;
                }
                else
                {
                    IsVisibleVolgende = false;
                    IsVisiblePasVraag = false;
                    IsVisibleControle = true;
                }
            }
            else
            {
                Vraag = vragen[aantalVragen];
            }
        }

        [RelayCommand]
        private async Task GoToHome()
        {
            if (Deelnemer == null || Deelnemers == null)
                return;
            await Shell.Current.GoToAsync("..", true, new Dictionary<string, object> {
                {"Deelnemer",Deelnemer },
                {"Deelnemers",Deelnemers}
            });
            Reset();
        }

        [RelayCommand]
        private async Task GoToControle()
        {
            if (Deelnemers == null)
                return;
            await Shell.Current.GoToAsync(nameof(ControlePage), true, new Dictionary<string, object> {
                {"Deelnemers",Deelnemers}
            });
            Reset();
        }
    }
}
