using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiIntroductie.ViewModels
{
    public partial class StackLayoutViewModel : ObservableObject
    {
        // Properties
        [ObservableProperty]
        string naam, email, telefoon, uitvoer, geslacht;

        // Constructor
        public StackLayoutViewModel()
        {
            this.Naam = string.Empty;
            this.Email = string.Empty;
            this.Telefoon = string.Empty;
            this.Uitvoer = string.Empty;
        }

        // Methods
        [RelayCommand]
        public void UitvoerTonen()
        {
            Uitvoer = $"Dag {this.Naam}, je emailadres is {this.Email}, je geslacht is {this.Geslacht} en ik kan je bellen op het nummer {this.Telefoon}.";
        }
    }
}
