namespace WebApi.Models
{
    public class Zaal
    {
        public int Id { get; set; }
        public string Naam { get; set; } = "";
        public int Capaciteit { get; set; }
        public List<Concert>? Concerten { get; set; }
    }
}
