using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiOefeningen2.Models
{
    public class Game
    {
        private int _moeilijkheidsgraad;

        public int Id { get; set; }
        public string Naam { get; set; }
        public int Moeilijkheidsgraad
        {
            get
            {
                return _moeilijkheidsgraad;
            }
            set
            {
                if (_moeilijkheidsgraad >= 1 || _moeilijkheidsgraad <= 5)
                {
                    _moeilijkheidsgraad = value;
                }
            }
        }
        public string Uitgever { get; set; }
    }
}
