using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeMol.Data.IRepository
{
    public interface IDeelnemerRepository
    {
        public List<Deelnemer> GetDeelnemers();
    }
}
