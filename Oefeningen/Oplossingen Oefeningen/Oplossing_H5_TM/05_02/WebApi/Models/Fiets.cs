namespace WebApi.Models;

public class Fiets
{
    public int Id { get; set; }
    public string Merk { get; set; } = "";
    public string Type { get; set; } = "";
    public string Kleur { get; set; } = "";
    public double Prijs { get; set; }
    public int Aantal { get; set; }
}
