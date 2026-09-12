using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TussenTijdsExamen.NETMAUI.Models
{
    public class Docent
    {
        public string Voornaam { get; set; }
        public string Achternaam { get; set; }
        public string VolledigeNaam => $"{Voornaam} {Achternaam}";
    }
}
