using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamenJanuari2026.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public DateTime OrderDatum { get; set; }
        public Klant Klant { get; set; }
        public List<Orderlijn> Orderlijnen { get; set; }
    }
}
