using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publishers.Models
{
    public class Sale
    {
        public int Id { get; set; }
        public int StoreId { get; set; }
        public int BookId { get; set; }
        public string OrderNumber { get; set; }
        public DateTime OrderDate { get; set; }
        public int Amount { get; set; }

        public Book Book { get; set; }
        public Store Store { get; set; }

        public override string ToString()
        {
            return $"Verkoop in {Store} op {OrderDate.ToShortDateString()}\n{Amount}x {Book}";
        }
    }
}