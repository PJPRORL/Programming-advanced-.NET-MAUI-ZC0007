using DeMol.Data.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeMol.Data.Repositories
{
    public class VraagRepository:IVraagRepository
    {
        public List<Vraag> Vragen = new()
        {
            new Vraag() { Vraagtekst = "Wie heeft de proef 'Op het water' gesaboteerd?" , DeelnemerId=1 },
            new Vraag() { Vraagtekst = "Wie heeft de proef 'In de lucht' gesaboteerd?" , DeelnemerId=1 },
            new Vraag() { Vraagtekst = "Wie heeft de proef 'Op het land' gesaboteerd?" , DeelnemerId=1 },
            new Vraag() { Vraagtekst = "Wie heeft de proef 'Door het vuur' gesaboteerd?" , DeelnemerId=1 },
            new Vraag() { Vraagtekst = "Wie is de Mol?" , DeelnemerId=1 },
        };

        public List<Vraag> GetVragen()
        {
            return Vragen;
        }

    }
}
