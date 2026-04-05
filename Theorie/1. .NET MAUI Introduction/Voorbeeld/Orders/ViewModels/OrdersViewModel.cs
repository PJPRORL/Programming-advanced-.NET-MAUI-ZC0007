

namespace Orders.ViewModels
{
    public partial class OrdersViewModel : BaseViewModel
    {
        [ObservableProperty]
        public ObservableCollection<Order> orders;

        private IOrderRepository _orderRepository;

        [ObservableProperty]
        private string bedrijfsnaam;

        public OrdersViewModel(OrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
            Bedrijfsnaam = "";
        }

        [RelayCommand]
        public void AlleOrdersOphalen()
        {
            IsBusy = true;
            Orders = new ObservableCollection<Order>(_orderRepository.OrdersOphalen());
            IsBusy = false;
        }

        [RelayCommand]
        public void AlleOrdersOphalenVoorKlant()
        {
            IsBusy = true;
            Orders = new ObservableCollection<Order>(_orderRepository.OrdersOphalenVoorKlant(Bedrijfsnaam));
            IsBusy = false;
        }
    }
}