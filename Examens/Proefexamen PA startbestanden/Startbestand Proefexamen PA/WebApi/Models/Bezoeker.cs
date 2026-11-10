namespace WebApi.Models
{
    public class Bezoeker
    {
        public int Id { get; set; }
        public string Voornaam { get; set; } = "";
        public string Naam { get; set; } = "";
        public string Email { get; set; } = "";
        public List<Ticket>? Tickets { get; set; }
    }
}
