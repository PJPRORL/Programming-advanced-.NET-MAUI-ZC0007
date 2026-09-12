using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Publishers.Data.Interface;
using Publishers.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publishers.ViewModels
{
    public partial class StoresViewModel : BaseViewModel
    {
        private IStoresRepository _storesRepository;

        [ObservableProperty]
        private ObservableCollection<Store> stores;

        [ObservableProperty]
        private string naam = "";

        [ObservableProperty]
        private string staat = "";

        [ObservableProperty]
        private string id = "";

        public StoresViewModel(StoresRepository storesRepository)
        {
            _storesRepository = storesRepository;
        }

        [RelayCommand]
        public void StoresOphalenViaNaam()
        {
            IsBusy = true;
            Stores = new ObservableCollection<Store>(_storesRepository.OphalenStoresViaNaam(Naam));
            IsBusy = false;
        }

        [RelayCommand]
        public void StoresOphalenViaStaat()
        {
            IsBusy = true;
            Stores = new ObservableCollection<Store>(_storesRepository.OphalenStoresViaStaat(Staat));
            IsBusy = false;
        }

        [RelayCommand]
        public void StoresOphalenViaNaamEnStaat()
        {
            IsBusy = true;
            Stores = new ObservableCollection<Store>(_storesRepository.OphalenStoresViaNaamEnStaat(Naam, Staat));
            IsBusy = false;
        }

        [RelayCommand]
        public void StoresOphalenViaId()
        {
            if (!int.TryParse(Id, out int id))
            {
                Shell.Current.DisplayAlert("Fout", "Geef een geldige ID.", "Sluiten");
                return;
            }
            IsBusy = true;
            var store = _storesRepository.OphalenStoreViaId(id);
            if (store == null)
                Shell.Current.DisplayAlert("Fout", $"Store met ID {id} werd niet gevonden.", "Sluiten");
            else
                Shell.Current.DisplayAlert("Store gevonden", store.Name, "Sluiten");

            IsBusy = false;
        }
    }
}