using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tussentijdsexamen2025r0615208.Models;

namespace Tussentijdsexamen2025r0615208.Data.IRepositories
{
    public interface IStudenten
    {
        List<Student> GetStudenten();
    }
}
