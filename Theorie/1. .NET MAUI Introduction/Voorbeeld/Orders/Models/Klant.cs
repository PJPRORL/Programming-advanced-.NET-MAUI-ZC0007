namespace Orders.Models;

public class Klant
{
    public string Adres { get; set; }
    public string Bedrijf { get; set; }
    public int Id { get; set; }
    public string Land { get; set; }
    public string Plaats { get; set; }
    public string Postcode { get; set; }
    public string Telefoonnummer { get; set; }

    public ICollection<Order> Orders { get; set; }

    public override string ToString()
    {
        return Bedrijf;
    }

}