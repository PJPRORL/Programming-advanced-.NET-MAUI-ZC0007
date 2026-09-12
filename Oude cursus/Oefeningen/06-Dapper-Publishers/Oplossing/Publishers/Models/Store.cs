namespace Publishers.Models;

public class Store
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Address { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public string Zip { get; set; }

    public IEnumerable<Sale> Sales { get; set; }
    public override string ToString()
    {
        return $"{Name} ({Address}, {City} {Zip})";
    }

    public string SalesInfo()
    {
        string result = "";

        if (this.Sales.Count() > 1)
        {
            foreach (Sale sale in this.Sales)
            {
                result += $"{sale.OrderNumber} - {sale.Book.Title} x {sale.Amount}\n";
            }
        }
        else
        {
            result += $"{this.Name} heeft geen verkopen.";
        }
        return result;
    }
}