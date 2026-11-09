namespace WebApi.Models;

public class Boek
{
    public int Id { get; set; }
    public string Titel { get; set; } = "";
    public string Auteur { get; set; } = "";
    public string Genre { get; set; } = "";
    public double Prijs { get; set; }
    public string Uitgeverij { get; set; } = "";
    public int Voorraad { get; set; }
}
