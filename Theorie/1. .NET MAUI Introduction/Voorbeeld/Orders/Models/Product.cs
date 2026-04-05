namespace Orders.Models;

public class Product
{

    public int Id { get; set; }
    public string Naam { get; set; }
    public decimal? Prijs { get; set; }
    public string Verpakking { get; set; }
    public short? Voorraad { get; set; }

    public override string ToString()
    {
        return $"Product {Id} - {Naam}";
    }
}
