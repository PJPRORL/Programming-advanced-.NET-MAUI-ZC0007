using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamenJanuari2026.Models
{
    public class Klant
    {
        public int KlantId { get; set; }
        public string Bedrijfsnaam { get; set; }
        public string Adres { get; set; }
        public string Postcode { get; set; }
        public string Land { get; set; }
        public string Telefoonnummer { get; set; }
        public List<Order> Orders { get; set; }
    }
}
