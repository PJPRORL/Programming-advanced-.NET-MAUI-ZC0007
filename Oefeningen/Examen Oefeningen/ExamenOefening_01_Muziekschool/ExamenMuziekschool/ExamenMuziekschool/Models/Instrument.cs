namespace ExamenMuziekschool.Models
{
    public class Instrument
    {
        public int Id { get; set; }
        public string Naam { get; set; } = "";
        public string Categorie { get; set; } = "";
        public List<Leerling>? Leerlingen { get; set; }
        public List<LesgeverInstrument>? LesgeverInstrumenten { get; set; }
    }
}
