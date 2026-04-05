using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeMol.Models
{
    public class Deelnemer
    {
        public string Voornaam { get; set; }
        public string Achternaam { get; set; }
        public string VolledigeNaam 
        { 
            get 
            { 
                return $"{Voornaam} {Achternaam}";
            }
        }
    }
}
