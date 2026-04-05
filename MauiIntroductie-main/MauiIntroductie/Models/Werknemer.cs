using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiIntroductie.Models
{
    public class Werknemer
    {
        public int Id { get; set; }

        public string Voornaam { get; set; }

        public string Achternaam { get; set; }

        public Functie Functie { get; set; }

        public DateTime Geboortedatum { get; set; } = new DateTime(1990,01,01);
        public DateTime InDienst { get; set; } = DateTime.Now;

        public string VolledigeNaam => $"{Voornaam} {Achternaam}";
    }
}
