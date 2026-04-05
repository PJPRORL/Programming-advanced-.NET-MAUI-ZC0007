using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TussenTijdsExamen.NETMAUI.ViewModels
{
    public partial class BaseViewModel : ObservableObject
    {
        [ObservableProperty]
        string title;

        public async Task GoBackAsync()
        {
            await Shell.Current.GoToAsync("..");
        }
    }
}
