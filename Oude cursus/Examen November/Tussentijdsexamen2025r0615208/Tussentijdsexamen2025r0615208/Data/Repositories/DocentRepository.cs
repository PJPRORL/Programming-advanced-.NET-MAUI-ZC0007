using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tussentijdsexamen2025r0615208.Models;
using Tussentijdsexamen2025r0615208.Data.IRepositories;

namespace Tussentijdsexamen2025r0615208.Data.Repositories
{
    public class DocentRepository : IDocenten
    {
        public List<Docent> Docenten = new()
        {
            new Docent() { Voornaam = "Jan", Achternaam = "de Vries" },
            new Docent() { Voornaam = "Sophie", Achternaam = "Jansen" },
            new Docent() { Voornaam = "Mark", Achternaam = "van Dijk"},
        };

        public List<Docent> GetDocenten()
        {
            return Docenten;
        }
    }
}
