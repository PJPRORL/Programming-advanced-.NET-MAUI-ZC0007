namespace Orders.Models;

public class Orderlijn
{

    public short Hoeveelheid { get; set; }
    public int Id { get; set; }
    public int ProductId { get; set; }

    public Product Product { get; set; }
    public Order Order { get; set; }

    public override string ToString()
    {
        return $"Orderlijn {Id}";
    }
}
