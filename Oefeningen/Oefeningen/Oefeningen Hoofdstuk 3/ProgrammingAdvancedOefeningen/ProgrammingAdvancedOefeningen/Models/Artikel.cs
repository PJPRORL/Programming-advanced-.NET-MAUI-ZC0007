namespace ProgrammingAdvancedOefeningen.Models
{
    public class Artikel
    {
        public int Id { get; set; }
        public string Naam { get; set; } = "";
        public string Merk { get; set; } = "";
        public string Soort { get; set; } = "";
        public double Prijs { get; set; }
        public int AantalOpVoorraad { get; set; }
    }
}
