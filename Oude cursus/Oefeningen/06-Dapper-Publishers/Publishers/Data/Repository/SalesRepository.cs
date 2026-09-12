using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Publishers.Data.Interface;

namespace Publishers.Data.Repository
{
    public class SalesRepository : BaseRepository, ISalesRepository
    {
        public IEnumerable<Sale> OphalenSalesVoorBoek(int bookId)
        {
            var sql = @"SELECT Sa.*, St.*, B.*
                        FROM Sale Sa
                        JOIN Store St ON Sa.storeId = St.id
                        JOIN Book B ON Sa.bookId = B.id
                        WHERE B.id = @bookId
                        ORDER BY Sa.orderDate";

            using (IDbConnection db = new SqlConnection(ConnectionString))
            {
                return db.Query<Sale, Store, Book, Sale>(
                    sql,
                    (sale, store, book) =>
                    {
                        sale.Store = store;
                        sale.Book = book;
                        return sale;
                    },
                    new { bookId = bookId }
                );
            }
        }

        public IEnumerable<Sale> OphalenSalesVoorPublisher(int publisherId)
        {
            var sql = @"SELECT Sa.*, St.*, B.*
                        FROM Sale Sa
                        JOIN Store St ON Sa.storeId = St.id
                        JOIN Book B ON Sa.bookId = B.id
                        WHERE B.publisherId = @publisherId
                        ORDER BY Sa.orderDate";

            using (IDbConnection db = new SqlConnection(ConnectionString))
            {
                return db.Query<Sale, Store, Book, Sale>(
                    sql,
                    (sale, store, book) =>
                    {
                        sale.Store = store;
                        sale.Book = book;
                        return sale;
                    },
                    new { publisherId = publisherId }
                );
            }
        }
    }
}