namespace Orders.Models;

public class Werknemer
{
    public string Avatar { get; set; }
    public string Achternaam { get; set; }
    public string Functie { get; set; }
    public DateTime? Geboortedatum { get; set; }
    public int Id { get; set; }
    public DateTime? InDienst { get; set; }
    public string Voornaam { get; set; }

    public string VolledigeNaam => $"{Voornaam} {Achternaam}";

    public override string ToString()
    {
        return VolledigeNaam;
    }
}