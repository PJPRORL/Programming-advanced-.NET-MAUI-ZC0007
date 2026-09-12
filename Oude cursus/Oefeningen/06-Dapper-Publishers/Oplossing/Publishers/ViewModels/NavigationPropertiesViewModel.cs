using Publishers.Data.Interface;

namespace Publishers.ViewModels;

public partial class NavigationPropertiesViewModel : BaseViewModel
{
    [ObservableProperty]
    public string uitgeverZoekterm;

    [ObservableProperty]
    public string boekZoekterm;    
    
    // H9 <---
    [ObservableProperty]
    public string auteurZoekterm;

    [ObservableProperty]
    public string stringOutput;

    //---->

    [ObservableProperty]
    public object selectedItem;

    [ObservableProperty]
    public ObservableCollection<object> items;

    private IEmployeesRepository _employeesRepository;
    private IPublishersRepository _publishersRepository;
    private IBooksRepository _booksRepository;
    private ISalesRepository _salesRepository;
    private IStoresRepository _storesRepository;

    public NavigationPropertiesViewModel(
        EmployeesRepository employeesRepository,
        PublishersRepository publishersRepository,
        BooksRepository booksRepository,
        StoresRepository storesRepository,
        SalesRepository salesRepository)
    {
        _employeesRepository = employeesRepository;
        _publishersRepository = publishersRepository;
        _booksRepository = booksRepository;
        _storesRepository = storesRepository;
        _salesRepository = salesRepository;

        UitgeverZoekterm = string.Empty;
        BoekZoekterm = string.Empty;

        //H9 <---
        AuteurZoekterm = string.Empty;
        StringOutput = string.Empty;

        // ----->
    }

    // H9 <---
    partial void OnSelectedItemChanging(object value)
    {
        var selectedItem = value;

        if (selectedItem is Publisher)
        {
            StringOutput = "Publicaties:";
            var publisher = (Publisher)selectedItem;

            StringOutput += publisher.TitleInfo();
        }

        if (selectedItem is Book)
        {
            StringOutput = "Geschreven door:";
            var book = (Book)selectedItem;

            StringOutput += book.AuthorInfo();
        }

        if (selectedItem is Store)
        {
            StringOutput = "Verkopen:";
            var store = (Store)selectedItem;

            StringOutput += store.SalesInfo();
        }
    }

    [RelayCommand]
    public void WinkelOphalen()
    {
        var winkels = _storesRepository.OphalenStoresSalesBoeken();
        Title = "Winkels";
        Items = new ObservableCollection<object>(winkels);
        stringOutput = string.Empty;
    }
    // ------>


    [RelayCommand]
    public void WerknemersOphalen()
    {
        var employees = _employeesRepository.OphalenEmployeesMetUitgeverEnJob(UitgeverZoekterm);
        Title = "Werknemers";
        Items = new ObservableCollection<object>(employees);
    }

    [RelayCommand]
    public void UitgeversOphalen()
    {
        var publishers = _publishersRepository.OphalenPublishers(UitgeverZoekterm);
        Title = "Uitgevers";
        Items = new ObservableCollection<object>(publishers);
    }

    [RelayCommand]
    public void BoekenOphalen()
    {
        //H9 --> zoekterm toegevoegd
        var books = _booksRepository.OphalenBoekenMetUitgever(UitgeverZoekterm, BoekZoekterm, AuteurZoekterm);
        Title = "Boeken";
        Items = new ObservableCollection<object>(books);
    }

    [RelayCommand]
    public void SalesTonenPerBoek()
    {
        if (SelectedItem == null || SelectedItem is not Book book)
        {
            Shell.Current.DisplayAlert("Fout", "Selecteer eerst een boek via de knop \"Boeken ophalen\"", "Ok");
            return;
        }

        var sales = _salesRepository.OphalenSalesVoorBoek(book.Id);
        Title = "Verkopen per boek";
        Items = new ObservableCollection<object>(sales);
    }

    [RelayCommand]
    public void SalesTonenPerUitgever()
    {
        if (SelectedItem == null || SelectedItem is not Publisher publisher)
        {
            Shell.Current.DisplayAlert("Fout", "Selecteer eerst een uitgever via de knop \"Uitgevers ophalen\"", "Ok");
            return;
        }

        var sales = _salesRepository.OphalenSalesVoorPublisher(publisher.Id);
        Title = "Verkopen per uitgever";
        Items = new ObservableCollection<object>(sales);
    }
}