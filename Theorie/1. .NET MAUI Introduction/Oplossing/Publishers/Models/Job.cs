using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publishers.Models
{
    public class Job
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public byte MinLevel { get; set; }
        public byte MaxLevel { get; set; }

        public override string ToString()
        {
            return Description;
        }
    }
}