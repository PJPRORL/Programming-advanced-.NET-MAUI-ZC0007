using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publishers.Data.Interface
{
    public interface ISalesRepository
    {
        public IEnumerable<Sale> OphalenSalesVoorBoek(int bookId);

        public IEnumerable<Sale> OphalenSalesVoorPublisher(int publisherId);
    }
}