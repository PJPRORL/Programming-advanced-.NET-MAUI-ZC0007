using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace DeMol.Data.IRepositories
{
    public interface IDeelnemerRepository
    {
        public List<Deelnemer> GetDeelnemers();
    }
}
