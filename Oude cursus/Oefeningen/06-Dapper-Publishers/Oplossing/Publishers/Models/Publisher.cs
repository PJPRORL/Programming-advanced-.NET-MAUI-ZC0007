using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publishers.Models
{
    public class Publisher
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }

        public IEnumerable<Book> Books { get; set; }

        public IEnumerable<Employee> Employees { get; set; }
        public override string ToString()
        {
            return $"{Name} ({City}, {Country})";
        }

        public string TitleInfo()
        {
            string result = "";

            if (this.Books.Count() >= 1)
            {
                foreach (Book book in this.Books)
                {
                    result += $"{book.Title} {book.AuthorInfo()} \n\n";
                }
            }
            else
            {
                result += $"{this.Name} heeft geen publicaties.";
            }
            return result;
        }
    }
}