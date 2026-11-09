namespace ProgrammingAdvancedOefeningen.Models
{
    public class Bestellijn
    {
        public int Id { get; set; }
        public string Omschrijving { get; set; } = "";
        public int Aantal { get; set; }
        public double StukPrijs { get; set; }
        public bool IsActief { get; set; }
    }
}
