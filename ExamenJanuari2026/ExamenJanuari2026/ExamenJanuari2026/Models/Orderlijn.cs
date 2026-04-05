using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamenJanuari2026.Models
{
    public class Orderlijn
    {
        public int Id { get; set; }
        public Product Product { get; set; }
        public Order Order { get; set; }
    }
}
