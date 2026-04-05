using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiIntroductie.ViewModels
{
    public partial class NamenViewModel : BaseViewModel
    {
        [ObservableProperty]
        string naam;

        [ObservableProperty]
        ObservableCollection<string> namen;

        public NamenViewModel()
        {
            Title = "Namen toevoegen";
            Naam = string.Empty;
            Namen = [];
        }

        [RelayCommand]
        private void NaamToevoegen()
        {
            Namen.Add(Naam);
            Naam = string.Empty;
        }
    }
}
