namespace Examen_MVC.ViewModels.Cart
{
    public class GetItemInCartVM
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Image { get; set; }

        public double Price { get; set; }

        public int Quantity { get; set; }

        public double TotalPrice { get; set; }
    }
}