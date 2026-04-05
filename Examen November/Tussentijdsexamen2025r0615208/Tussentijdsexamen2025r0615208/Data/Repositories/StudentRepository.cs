using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tussentijdsexamen2025r0615208.Models;
using Tussentijdsexamen2025r0615208.Data.IRepositories;

namespace Tussentijdsexamen2025r0615208.Data.Repositories
{
    public class StudentRepository : IStudenten
    {
        public List<Student> Studenten = new()
        {
            new Student() { Voornaam = "Lisa", Achternaam = "Bakker", Docent = new Docent(){ Voornaam = "Jan", Achternaam = "de Vries" } },
            new Student() { Voornaam = "Tom", Achternaam = "Verhoeven", Docent = new Docent(){ Voornaam = "Jan", Achternaam = "de Vries" }  },
            new Student() { Voornaam = "Emma", Achternaam = "Smit", Docent = new Docent(){ Voornaam = "Jan", Achternaam = "de Vries" }  },
            new Student() { Voornaam = "Daan", Achternaam = "Willems", Docent = new Docent(){ Voornaam = "Sophie", Achternaam = "Jansen" } },
            new Student() { Voornaam = "Julia", Achternaam = "de Groot", Docent = new Docent(){ Voornaam = "Sophie", Achternaam = "Jansen" } },
            new Student() {  Voornaam = "Max", Achternaam = "Peters", Docent = new Docent(){ Voornaam = "Mark", Achternaam = "van Dijk"} },
            new Student() { Voornaam = "Sanne", Achternaam = "Mulder", Docent = new Docent(){ Voornaam = "Mark", Achternaam = "van Dijk"} },
         };

        public List<Student> GetStudenten()
        {
            return Studenten;
        }
    }
}
