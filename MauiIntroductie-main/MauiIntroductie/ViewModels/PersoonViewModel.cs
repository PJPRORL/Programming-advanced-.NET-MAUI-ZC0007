using System.ComponentModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace MauiIntroductie.ViewModels
{
    public partial class PersoonViewModel : ObservableObject
    {
        [ObservableProperty]
        public string naam;
    }
}
