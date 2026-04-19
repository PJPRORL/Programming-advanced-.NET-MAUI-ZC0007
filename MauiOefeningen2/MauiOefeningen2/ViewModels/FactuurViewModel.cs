using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiOefeningen2.ViewModels
{
    public partial class FactuurViewModel : BaseViewModel
    {
        // Properties
        //[ObservableProperty]
        //double? prijs;

        //[ObservableProperty]
        //int? aantal;

        [ObservableProperty]
        ObservableCollection<Product> producten;

        [ObservableProperty]
        Product product;

        // Constructor
        public FactuurViewModel()
        {
            Title = "Factuur";
            Producten = [];
            Product = new();
        }

        // Methods

        [RelayCommand]
        public void ProductToevoegen()
        {
            Product.Totaal = Product.Prijs * Product.Aantal;
            Producten.Add(Product);
            Product = new();
        }
    }
}
