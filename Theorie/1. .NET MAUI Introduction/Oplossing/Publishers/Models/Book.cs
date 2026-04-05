using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publishers.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public string Type { get; set; }
        public int PublisherId { get; set; }
        public double Price { get; set; }
        public double Advance { get; set; }
        public int Royalty { get; set; }
        public int Sales { get; set; }
        public DateTime ReleaseDate { get; set; }
        public Publisher Publisher { get; set; }

        public IEnumerable<Author> Authors { get; set; }

        public override string ToString()
        {
            return $"{Title}\nUitgever: {Publisher}\nUitgave datum: {ReleaseDate.ToShortDateString()}";
        }

        public string AuthorInfo()
        {
            string result = "";

            if (this.Authors.Count() >= 1)
            {
                foreach (Author author in this.Authors)
                {
                    result += $"\n - {author.FirstName} {author.LastName}";
                }
            }

            return result;
        }
    }
}
