using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiIntroductie.Data.Repositories
{
    public class WerknemerRepository : IWerknemerRepository
    {
    
        public List<Werknemer> Werknemers = new()
        {
            new Werknemer() { Id = 1, Voornaam = "John", Achternaam = "Doe", Functie = new Functie(){Naam="Manager" }, Geboortedatum = new DateTime(1987,08,15) , InDienst = new DateTime(2020,09,01) },
            new Werknemer() { Id = 2, Voornaam = "Linn", Achternaam = "Kendal", Functie = new Functie(){Naam="Developer" }, Geboortedatum = new DateTime(2002,07,13) , InDienst = new DateTime(2023,09,01)  },
            new Werknemer() { Id = 3, Voornaam = "Mark", Achternaam = "Mathys", Functie = new Functie(){Naam="Designer" }, Geboortedatum = new DateTime(2001,10,03) , InDienst = new DateTime(2024, 10, 20)},
            new Werknemer() { Id = 4, Voornaam = "Lenno", Achternaam = "Brimelow", Functie = new Functie(){Naam="Designer" }, Geboortedatum = new DateTime(1978,09,09) , InDienst = new DateTime(1999, 01, 10)},
            new Werknemer() { Id = 5, Voornaam = "Theresa", Achternaam = "Korf", Functie = new Functie(){Naam="Designer" }, Geboortedatum = new DateTime(1996,12,27) , InDienst = new DateTime(2007,07,15)  },
            new Werknemer() { Id = 6, Voornaam = "Natalie", Achternaam = "Spurway", Functie = new Functie(){Naam="Developer" }, Geboortedatum = new DateTime(1980,03,24) , InDienst = new DateTime(2003, 12, 01)}
        };


        public List<Werknemer> GetWerknemers()
        {
            return Werknemers;
        }
    }
}
