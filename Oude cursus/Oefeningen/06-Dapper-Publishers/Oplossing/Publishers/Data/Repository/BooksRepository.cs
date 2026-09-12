
namespace Publishers.Data.Repository
{
    public class BooksRepository : BaseRepository, IBooksRepository
    {
        //H9 <---
        public IEnumerable<Book> OphalenBoekenMetUitgever(string uitgeverZoekterm, string boekZoekterm, string auteurZoekterm)
        {
            var sql = @"SELECT B.*, '' AS SplitCol, P.*, '' AS SplitCol, A.*
                        FROM Book B
                        JOIN Publisher P ON B.publisherId = P.id
                        JOIN TitleAuthor AT ON AT.BookId = B.Id
                        JOIN Author A ON A.Id = AT.AuthorId
                        WHERE P.name LIKE '%' + @publisher + '%'
                            AND B.title LIKE '%' + @title + '%'
                            AND (A.firstName LIKE '%' + @name + '%'
                            OR A.lastName LIKE '%' + @name + '%')
                        ORDER BY B.releaseDate";

            using (IDbConnection db = new SqlConnection(ConnectionString))
            {
                var boeken = db.Query<Book, Publisher, Author, Book>(
                    sql,
                    (title, publisher, author) =>
                    {
                        title.Publisher = publisher;
                        title.Authors = [author];
                        return title;
                    },
                    new { publisher = uitgeverZoekterm, title = boekZoekterm, name = auteurZoekterm }, splitOn: "SplitCol"
                );

                return GroepeerBoeken(boeken);
            }
        }

        private static IEnumerable<Book> GroepeerBoeken(IEnumerable<Book> boeken)
        {
            var gegroepeerd = boeken.GroupBy(boek => boek.Title);

            List<Book> boekenMetAuteurs = new List<Book>();

            foreach (var groep in gegroepeerd)
            {
                var boek = groep.First();
                List<Author> alleAuteurs = new List<Author>();

                foreach (var b in groep)
                {
                    alleAuteurs.Add(b.Authors.First());
                }

                boek.Authors = alleAuteurs;
                boekenMetAuteurs.Add(boek);
            }

            return boekenMetAuteurs;
        }
    }
}