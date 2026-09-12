using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TussenTijdsExamen.NETMAUI.Models;

namespace TussenTijdsExamen.NETMAUI.Data.IRepository
{
    public interface ILeerlingRepository
    {
        public List<Leerling> GetAllLeerlingen();
    }
}
