using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Orders.Models;

namespace Orders.Data.Repository
{
    public interface IKlantRepository
    {
        public IEnumerable<Klant> KlantenOphalenMetOrders();

        public IEnumerable<Klant> KlantenOphalenMetOrdersEnOrderlijnen();

        public IEnumerable<Klant> KlantenOphalenMetOrdersEnOrderlijnenEnProduct();

        public IEnumerable<Klant> KlantenOphalenMetOrdersEnOrderlijnenEnProductEnWerknemers();
    }
}