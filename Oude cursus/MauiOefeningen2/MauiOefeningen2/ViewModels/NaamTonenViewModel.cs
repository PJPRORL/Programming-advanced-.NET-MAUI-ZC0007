using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiOefeningen2.ViewModels
{
    public partial class NaamTonenViewModel : ViewModel
    {
        // Properties
        [ObservableProperty]
        string naam, uitvoer;

        [ObservableProperty]
        int? getal;

        // Constructor
        public NaamTonenViewModel()
        {
            ResultaatLeegmaken();
        }

        // Methods
        [RelayCommand]
        public void ResultaatLeegmaken()
        {
            Getal = null;
            Naam = string.Empty;
            Uitvoer = string.Empty;
        }

        [RelayCommand]
        public void NaamTonen()
        {
            StringBuilder sb = new StringBuilder();

            if (Getal.HasValue && !string.IsNullOrWhiteSpace(Naam))
            {
                for (int i = 0; i < Getal; i++)
                {

                    sb.Append(Naam);

                    if (i < Getal - 1)
                    {
                        sb.Append(", ");
                    }
                }
            }

            Uitvoer = sb.ToString();
        }
    }
}
