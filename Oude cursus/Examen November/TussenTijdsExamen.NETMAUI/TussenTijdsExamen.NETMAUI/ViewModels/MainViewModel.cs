using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TussenTijdsExamen.NETMAUI.Data.Repository;

namespace TussenTijdsExamen.NETMAUI.ViewModels
{
    public partial class MainViewModel : BaseViewModel
    {
        [ObservableProperty]
        Leerling leerling;

        public ObservableCollection<Leerling> Leerlingen; 

        [ObservableProperty]
        Docent docent;

        public ObservableCollection<Docent> Docenten;

        private readonly ILeerlingRepository _leerlingRepository;
        private readonly IDocentRepository _docentRepository;

        public MainViewModel(IDocentRepository docentRepository, ILeerlingRepository leerlingRepository)
        {
            Title = "Homepage";
            // Leerlngen ophalen
            _leerlingRepository = leerlingRepository;            
            Leerlingen = new ObservableCollection<Leerling>(_leerlingRepository.GetAllLeerlingen());
            Leerling = new();

            // Docenten ophalen
            _docentRepository = docentRepository;
            Docenten = new ObservableCollection<Docent>(_docentRepository.GetAllDocenten());
            Docent = new();
        }

        public void AddLeerling()
        {
            if (Leerling == null)
            {
                Leerlingen.Add(Leerling);
                Leerling = new ();
            }
        }

        public void AddDocenten()
        {
            if (Docent == null)
            {
                Docenten.Add(Docent);
                Docent = new ();
            }
        }
    }
}
