namespace WebApi.Models
{
    public class Concert
    {
        public int Id { get; set; }
        public string Titel { get; set; } = "";
        public string Artiest { get; set; } = "";
        public DateTime Datum { get; set; }
        public double TicketPrijs { get; set; }
        public int ZaalId { get; set; }
        public Zaal? Zaal { get; set; }
        public List<Ticket>? Tickets { get; set; }
    }
}
