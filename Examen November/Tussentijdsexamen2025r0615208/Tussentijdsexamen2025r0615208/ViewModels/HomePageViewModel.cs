using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Tussentijdsexamen2025r0615208.Models;

namespace Tussentijdsexamen2025r0615208.ViewModels
{
    public partial class HomePageViewModel : BaseViewModel
    {
        public HomePageViewModel()
        {
            [ObservableProperty]
            ObservableCollection<Docent> docenten;

            [ObservableProperty]
            Docent docent;


        }
    }
}
