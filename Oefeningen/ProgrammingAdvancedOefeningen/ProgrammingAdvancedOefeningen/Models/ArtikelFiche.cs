namespace ProgrammingAdvancedOefeningen.Models
{
    public class ArtikelFiche
    {
        public Artikel Artikel { get; set; }
        public bool IsVoorradig { get; set; }
        public int AantalVanDezelfdeSoort { get; set; }
        public bool IsGoedkoopsteVanDeSoort { get; set; }
    }
}
