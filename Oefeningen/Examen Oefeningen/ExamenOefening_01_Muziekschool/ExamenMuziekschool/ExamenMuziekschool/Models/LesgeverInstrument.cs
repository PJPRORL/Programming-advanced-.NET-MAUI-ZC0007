namespace ExamenMuziekschool.Models
{
    public class LesgeverInstrument
    {
        public int Id { get; set; }
        public int InstrumentId { get; set; }
        public Instrument? Instrument { get; set; }
        public int LesgeverId { get; set; }
        public Lesgever? Lesgever { get; set; }
    }
}
