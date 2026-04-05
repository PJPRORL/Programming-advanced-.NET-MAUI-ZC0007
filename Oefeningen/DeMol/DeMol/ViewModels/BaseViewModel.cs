using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeMol.ViewModels
{
    public partial class BaseViewModel : ObservableObject
    {
        [ObservableProperty]
        string title;

        [RelayCommand]
        public static async Task GoBackAsync()
        {
            await Shell.Current.GoToAsync("..");
        }
    }
}
