namespace ExamenNovember.Models
{
    public class DocentKlas
    {
        public int Id { get; set; }
        public int DocentId { get; set; }
        public Docent? Docent { get; set; }
        public int KlasId { get; set; }
        public Klas? Klas { get; set; }
    }
}
