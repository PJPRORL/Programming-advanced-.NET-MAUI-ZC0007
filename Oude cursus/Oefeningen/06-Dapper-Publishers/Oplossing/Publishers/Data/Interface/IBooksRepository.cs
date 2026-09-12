using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publishers.Data.Interface
{
    public interface IBooksRepository
    {
        // H9 zoekterm toegevoegd
        public IEnumerable<Book> OphalenBoekenMetUitgever(string uitgeverZoekterm, string boekZoekterm, string auteurZoekterm);
    }
}