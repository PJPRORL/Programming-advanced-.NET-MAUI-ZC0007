
namespace Orders.ViewModels
{
    public partial class OrdersExtendedViewModel : BaseViewModel
    {
        [ObservableProperty]
        public string orderOutput;

        private readonly string _scheiding = "----------------------------------------";
        private IOrderRepository _orderRepository;
        private IKlantRepository _klantRepository;

        [ObservableProperty]
        private string bedrijfsnaam;

        public OrdersExtendedViewModel(OrderRepository orderRepository, KlantRepository klantRepository)
        {
            _orderRepository = orderRepository;
            _klantRepository = klantRepository;
        }

        [RelayCommand]
        public void AlleOrdersOphalen()
        {
            IsBusy = true;
            var orders = _orderRepository.OrdersOphalen();
            OrderOutput = string.Empty;

            foreach (var order in orders)
            {
                OrderOutput += $"Order {order.Id} voor klant {order.Klant.Bedrijf}:\n";
                OrderOutput += $"Behandeld door {order.Werknemer}\n";
                OrderOutput += $"{_scheiding}\n";
            }

            IsBusy = false;
        }

        [RelayCommand]
        public void AlleOrdersOphalenVoorKlant()
        {
            IsBusy = true;
            var orders = _orderRepository.OrdersOphalenVoorKlant(Bedrijfsnaam);
            OrderOutput = string.Empty;

            foreach (var order in orders)
            {
                OrderOutput += $"Order {order.Id} voor klant {order.Klant.Bedrijf}:\n";
                OrderOutput += $"Behandeld door {order.Werknemer}\n";
                OrderOutput += $"{_scheiding}\n";
            }

            IsBusy = false;
        }

        [RelayCommand]
        public void KlantenOphalenMetOrders()
        {
            IsBusy = true;
            var klanten = _klantRepository.KlantenOphalenMetOrders();
            OrderOutput = string.Empty;

            foreach (var klant in klanten)
            {
                OrderOutput += $"Alle orders voor klant {klant.Bedrijf}:\n";
                klant.Orders.ToList().ForEach(o =>
                {
                    OrderOutput += o.ToString() + "\n";
                });
                OrderOutput += $"{_scheiding}\n";
            }

            IsBusy = false;
        }

        [RelayCommand]
        public void KlantenOphalenMetOrdersEnOrderlijnen()
        {
            IsBusy = true;
            var klanten = _klantRepository.KlantenOphalenMetOrdersEnOrderlijnen();
            OrderOutput = string.Empty;

            foreach (var klant in klanten)
            {
                OrderOutput += $"Alle orders voor klant {klant.Bedrijf}:\n";
                klant.Orders.ToList().ForEach(o =>
                {
                    OrderOutput += o.ToString() + "\n";
                    o.Orderlijnen.ToList().ForEach(ol =>
                    {
                        OrderOutput += ol.ToString() + "\n";
                    });
                });
                OrderOutput += $"{_scheiding}\n";
            }

            IsBusy = false;
        }

        [RelayCommand]
        public void KlantenOphalenMetOrdersEnOrderlijnenEnProduct()
        {
            IsBusy = true;
            var klanten = _klantRepository.KlantenOphalenMetOrdersEnOrderlijnenEnProduct();
            OrderOutput = string.Empty;

            foreach (var klant in klanten)
            {
                OrderOutput += $"Alle orders voor klant {klant.Bedrijf}:\n";
                klant.Orders.ToList().ForEach(o =>
                {
                    OrderOutput += o.ToString() + "\n";
                    o.Orderlijnen.ToList().ForEach(ol =>
                    {
                        OrderOutput += ol.ToString() + "\n";
                        OrderOutput += ol.Product.ToString() + "\n";
                    });
                });
                OrderOutput += $"{_scheiding}\n";
            }

            IsBusy = false;
        }
        [RelayCommand]
        public void KlantenOphalenMetOrdersEnOrderlijnenEnProductEnWerknemer()
        {
            IsBusy = true;
            var klanten = _klantRepository.KlantenOphalenMetOrdersEnOrderlijnenEnProductEnWerknemers();
            OrderOutput = string.Empty;

            foreach (var klant in klanten)
            {
                OrderOutput += $"Alle orders voor klant {klant.Bedrijf}:\n";
                klant.Orders.ToList().ForEach(o =>
                {
                    OrderOutput += o.ToString() + " behandeld door " + o.Werknemer.ToString() + "\n";
                    o.Orderlijnen.ToList().ForEach(ol =>
                    {
                        OrderOutput += ol.ToString() + "\n";
                        OrderOutput += ol.Product.ToString() + "\n";
                    });
                });
                OrderOutput += $"{_scheiding}\n";
            }

            IsBusy = false;
        }
    }
}