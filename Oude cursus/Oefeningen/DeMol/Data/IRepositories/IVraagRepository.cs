using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeMol.Data.IRepositories
{
    public interface IVraagRepository
    {
        public List<Vraag> GetVragen();
    }
}
