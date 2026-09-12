using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamenJanuari2026.Models
{
    public class Product
    {
        public int ProductId { get; set; }
        public string Naam { get; set; }
        public string Verpakking { get; set; }
        public double Prijs { get; set; }
        public int Voorraad { get; set; }
    }
}
