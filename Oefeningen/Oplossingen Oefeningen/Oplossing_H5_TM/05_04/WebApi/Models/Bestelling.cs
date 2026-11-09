namespace WebApi.Models;

public class Bestelling
{
    public int Id { get; set; }
    public string KlantNaam { get; set; } = "";
    public int Tafelnummer { get; set; }
    public string Gerechten { get; set; } = "";
    public string Status { get; set; } = "";
    public double TotaalPrijs { get; set; }
}
