using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiIntroductie.Models
{
    public class Functie
    {
        public string Naam {  get; set; }

        public override bool Equals(object? obj)
        {
            return obj is Functie functie && functie.Naam == Naam;
        }

        public override string ToString()
        {
            return Naam;
        }
    }

}
