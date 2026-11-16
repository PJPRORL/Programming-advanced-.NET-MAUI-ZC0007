namespace ExamenMuziekschool.Models
{
    public class Leerling
    {
        public int Id { get; set; }
        public string Naam { get; set; } = "";
        public string Voornaam { get; set; } = "";
        public string Email { get; set; } = "";
        public DateTime Inschrijfdatum { get; set; }
        public Instrument? Instrument { get; set; }
        public int InstrumentId { get; set; }
    }
}
