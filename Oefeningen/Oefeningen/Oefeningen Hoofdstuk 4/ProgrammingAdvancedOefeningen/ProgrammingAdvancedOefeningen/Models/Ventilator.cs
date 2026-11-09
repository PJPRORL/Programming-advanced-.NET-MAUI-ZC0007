namespace ProgrammingAdvancedOefeningen.Models
{
    public class Ventilator
    {
        public int Id { get; set; }
        public string Merk { get; set; } = "";
        public int AfmetingInMm { get; set; }
        public string Verlichting { get; set; } = "";
        public double Prijs { get; set; }
    }
}
