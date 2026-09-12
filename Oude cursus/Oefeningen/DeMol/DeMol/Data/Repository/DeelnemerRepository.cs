using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeMol.Data.Repository
{
    public class DeelnemerRepository : IDeelnemerRepository
    {
        private List<Deelnemer> Deelnemers = new ()
        {
            new Deelnemer() {Voornaam = "Jeroen", Achternaam = "Piussi"},
            new Deelnemer() {Voornaam = "Manon", Achternaam = "Looyens"},
            new Deelnemer() {Voornaam = "Annabelle", Achternaam = "Looyens"},
            new Deelnemer() {Voornaam = "Bart", Achternaam = "Dergent"},
        };

        public List<Deelnemer> GetDeelnemers()
        {
            return Deelnemers;
        }
    }
}
