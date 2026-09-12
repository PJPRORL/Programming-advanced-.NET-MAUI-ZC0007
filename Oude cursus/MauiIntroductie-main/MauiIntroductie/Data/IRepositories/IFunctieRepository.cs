using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiIntroductie.Data.IRepositories
{
    public interface IFunctieRepository
    {
        public List<Functie> GetFuncties();
    }
}
