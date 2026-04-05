

namespace Orders.Data.Repository
{
    public interface IOrderRepository
    {
        public IEnumerable<Order> OrdersOphalen();

        public IEnumerable<Order> OrdersOphalenVoorKlant(string bedrijfsnaam);
    }
}