using DeMol.Data.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeMol.Data.Repositories
{
    public class DeelnemerRepository:IDeelnemerRepository
    {
        public List<Deelnemer> Deelnemers = new()
        {
            new Deelnemer() { Id = 1, Voornaam = "Tim", Achternaam = "Janssens", Score = 0, PasVragen = 0, Rol = Rol.Mol,  NietIngevuld = true },
            new Deelnemer() { Id = 2, Voornaam = "Elise", Achternaam = "Peeters",  Score = 0, PasVragen = 2 , Rol = Rol.Deelnemer, NietIngevuld = true },
            new Deelnemer() { Id = 3, Voornaam = "Mark", Achternaam = "Mathys",  Score = 0, PasVragen = 0, Rol=Rol.Deelnemer, NietIngevuld = true },
            new Deelnemer() { Id = 4, Voornaam = "Yasmine", Achternaam = "Bouhoudan",  Score = 0, PasVragen = 0, Rol=Rol.Deelnemer, NietIngevuld = true },
        };

        public List<Deelnemer> GetDeelnemers()
        {
            return Deelnemers;
        }
    }
}
