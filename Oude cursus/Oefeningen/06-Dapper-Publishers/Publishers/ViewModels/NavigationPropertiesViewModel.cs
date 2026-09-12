using Publishers.Data.Interface;

namespace Publishers.ViewModels;

public partial class NavigationPropertiesViewModel : BaseViewModel
{
    [ObservableProperty]
    public string uitgeverZoekterm;

    [ObservableProperty]
    public string boekZoekterm;

    [ObservableProperty]
    public object selectedItem;

    [ObservableProperty]
    public ObservableCollection<object> items;

    private IEmployeesRepository _employeesRepository;
    private IPublishersRepository _publishersRepository;
    private IBooksRepository _booksRepository;
    private ISalesRepository _salesRepository;

    public NavigationPropertiesViewModel(
        EmployeesRepository employeesRepository,
        PublishersRepository publishersRepository,
        BooksRepository booksRepository,
        SalesRepository salesRepository)
    {
        _employeesRepository = employeesRepository;
        _publishersRepository = publishersRepository;
        _booksRepository = booksRepository;
        _salesRepository = salesRepository;

        UitgeverZoekterm = string.Empty;
        BoekZoekterm = string.Empty;
    }

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
        var books = _booksRepository.OphalenBoekenMetUitgever(UitgeverZoekterm, BoekZoekterm);
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