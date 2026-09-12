using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeMol.Data.Repository
{
    public class VraagRepository : IVraagRepository
    {
        private List<string> _vragen = new List<string>()
        {
             "Wie heeft de proef 'Op het water' gesaboteerd?",
             "Wie heeft de meeste pasvragen gekocht?",
             "Wie was als laatste bij de auto?"
        };

        public VraagRepository()
        {

        }

        public List<string> GetVragen()
        {
            return _vragen;
        }
    }
}
