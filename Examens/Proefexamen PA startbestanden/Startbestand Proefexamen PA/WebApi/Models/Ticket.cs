using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace WebApi.Models
{
    public class Ticket
    {
        public int Id { get; set; }
        public int ConcertId { get; set; }
        public Concert? Concert { get; set; }
        public int BezoekerId { get; set; }
        public Bezoeker? Bezoeker { get; set; }
        public int Aantal { get; set; }
        public double Korting { get; set; }
    }
}
