namespace Examen_MVC.ViewModels.Cart
{
    public class CartVM
    {
        public GetItemInCartVM[] ItemsInCart { get; set; }

        public int ShippingCost
        {
            get
            {
                // TODO: Bereken Shipping Cost
                return 5;
            }
        }

        public double TotalPriceOfProducts
        {
            get
            {
                // TODO: Bereken Total price zonder verzending
                return 0;
            }
        }

        // TODO: Bereken Total price met verzending
        public double Total { get; }
    }
}