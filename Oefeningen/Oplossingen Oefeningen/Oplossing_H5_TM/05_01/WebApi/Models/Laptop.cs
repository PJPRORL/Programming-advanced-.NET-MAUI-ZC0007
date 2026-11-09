namespace WebApi.Models;

public class Laptop
{
    public int Id { get; set; }
    public string Merk { get; set; } = "";
    public string Processor { get; set; } = "";
    public int RamInGB { get; set; }
    public double Prijs { get; set; }
    public string GPU { get; set; } = "";
}
