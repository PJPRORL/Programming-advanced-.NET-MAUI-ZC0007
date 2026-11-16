namespace ExamenMuziekschool.Models
{
    public class Lesgever
    {
        public int Id { get; set; }
        public string Naam { get; set; } = "";
        public string Voornaam { get; set; } = "";
        public string Email { get; set; } = "";
        public List<LesgeverInstrument>? LesgeverInstrumenten { get; set; }
    }
}
