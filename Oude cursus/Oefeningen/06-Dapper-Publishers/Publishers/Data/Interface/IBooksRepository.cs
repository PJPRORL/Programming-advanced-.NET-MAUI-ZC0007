using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publishers.Data.Interface
{
    public interface IBooksRepository
    {
        public IEnumerable<Book> OphalenBoekenMetUitgever(string uitgeverZoekterm, string boekZoekterm);
    }
}