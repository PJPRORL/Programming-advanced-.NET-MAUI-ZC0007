using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiOefeningen2.ViewModels
{
    public partial class ProgrammingAdvancedViewModel : BaseViewModel
    {
        // Properties
        [ObservableProperty]
        string naam, voornaam, campus, uitvoer, lookaalnr;

        [ObservableProperty]
        int getal;

        [ObservableProperty]
        DateTime datum = DateTime.Today;

        // Constructor
        public ProgrammingAdvancedViewModel()
        {
            this.Naam = naam;
            this.Voornaam = voornaam;
            this.Campus = campus;
            this.Lookaalnr = lookaalnr;
            this.Getal = getal;
            this.Uitvoer = uitvoer;
            this.Datum = datum;
        }

        // Methods
        [RelayCommand]
        public void ToonGegevens()
        {
            Uitvoer = $"Welkom {this.Voornaam} {this.Naam}\n" +
                $"Je vast lokaal is {this.Lookaalnr} in {this.Campus}\n" +
                $"Je eerste les is op {Datum:dd/MM/yyyy}\n" +
                $"Je hoopt {this.Getal}/20 te scoren";
        }
    }
}
