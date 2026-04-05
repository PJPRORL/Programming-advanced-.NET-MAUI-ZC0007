using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TussenTijdsExamen.NETMAUI.Data.IRepository;
using TussenTijdsExamen.NETMAUI.Models;

namespace TussenTijdsExamen.NETMAUI.Data.Repository
{
    public class LeerlingRepository : ILeerlingRepository
    {
        private List<Leerling> Leerlingen = new()
        {
            new Leerling { Voornaam = "Alice", Achternaam = "Alberts"},
            new Leerling { Voornaam = "Bob", Achternaam = "Bakker"},
            new Leerling { Voornaam = "Charlie", Achternaam = "Claessen"}
        };

        public List<Leerling> GetAllLeerlingen()
        {
             return Leerlingen;
        }
    }
}
