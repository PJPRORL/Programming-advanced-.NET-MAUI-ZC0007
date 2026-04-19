using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiOefeningen2.Data.Repository
{
    public class PersoonRepository : IPersoonRepository
    {
        private List<Persoon> personen = new List<Persoon>();

        public List<Persoon> OphalenPersonen()
        {
            return personen;
        }
    }
}
