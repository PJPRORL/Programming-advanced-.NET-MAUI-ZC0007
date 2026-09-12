using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TussenTijdsExamen.NETMAUI.Data.Repository
{
    public class DocentRepository : IDocentRepository
    {
        public List<Docent> Docenten = new()
        {
            new Docent { Voornaam = "Jan", Achternaam = "Jansen" },
            new Docent { Voornaam = "Piet", Achternaam = "Pietersen" },
            new Docent { Voornaam = "Klaas", Achternaam = "Klaassen" }
        };

        public List<Docent> GetAllDocenten()
        {
            return Docenten;
        }
    }
}
