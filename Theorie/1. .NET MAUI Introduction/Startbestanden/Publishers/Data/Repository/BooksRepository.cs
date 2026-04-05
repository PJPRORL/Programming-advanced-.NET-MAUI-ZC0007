using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Publishers.Data.Interface;

namespace Publishers.Data.Repository
{
    public class BooksRepository : BaseRepository, IBooksRepository
    {
        public IEnumerable<Book> OphalenBoekenMetUitgever(string uitgeverZoekterm, string boekZoekterm)
        {
            var sql = @"SELECT B.*, P.*
                        FROM Book B
                        JOIN Publisher P ON B.publisherId = P.id
                        WHERE P.name LIKE '%' + @publisher + '%'
                            AND B.title LIKE '%' + @title + '%'
                        ORDER BY B.releaseDate";

            using (IDbConnection db = new SqlConnection(ConnectionString))
            {
                return db.Query<Book, Publisher, Book>(
                    sql,
                    (title, publisher) =>
                    {
                        title.Publisher = publisher;
                        return title;
                    },
                    new { publisher = uitgeverZoekterm, title = boekZoekterm }
                );
            }
        }
    }
}