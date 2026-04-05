using Publishers.Models;
using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Linq;
using Dapper;
using Publishers.Data.Interface;

namespace Publishers.Data.Repository
{
    public class StoresRepository : BaseRepository, IStoresRepository
    {
        public List<Store> OphalenStoresViaNaam(string naam)
        {
            string sql = "SELECT * FROM Store";
            sql += " WHERE name like '%'+ @naam +'%'";
            sql += " ORDER BY name";

            var parameters = new { @naam = naam };

            using (IDbConnection db = new SqlConnection(ConnectionString))
            {
                return db.Query<Store>(sql, parameters).ToList();
            }
        }

        public List<Store> OphalenStoresViaNaamEnStaat(string naam, string staat)
        {
            string sql = "SELECT * FROM Store";
            sql += " WHERE name like '%'+ @naam +'%'";
            sql += " AND state=@staat";
            sql += " ORDER BY name";

            var parameters = new { @naam = naam, @staat = staat };

            using (IDbConnection db = new SqlConnection(ConnectionString))
            {
                return db.Query<Store>(sql, parameters).ToList();
            }
        }

        public List<Store> OphalenStoresViaStaat(string staat)
        {
            string sql = "SELECT * FROM Store WHERE state=@staat ORDER BY name";

            var parameters = new { @staat = staat };

            using (IDbConnection db = new SqlConnection(ConnectionString))
            {
                return db.Query<Store>(sql, parameters).ToList();
            }
        }

        public Store OphalenStoreViaId(int id)
        {
            string sql = "SELECT * FROM Store WHERE Id=@id";

            var parameters = new { @id = id };

            using (IDbConnection db = new SqlConnection(ConnectionString))
            {
                return db.Query<Store>(sql, parameters).SingleOrDefault();
            }
        }

        //H9 <---
        public IEnumerable<Store> OphalenStoresSalesBoeken()
        {
            string sql = @"SELECT St.*, '' AS SplitCol, Sa.*, '' AS SplitCol, B.* 
                           FROM Store St
                           LEFT JOIN Sale Sa ON Sa.StoreId = St.Id
                           LEFT JOIN Book B ON B.Id = Sa.BookId";

            using (IDbConnection db = new SqlConnection(ConnectionString))
            {
                var stores = db.Query<Store, Sale, Book, Store>(
                    sql,
                    (store, sale, book) =>
                    {
                        sale.Store = store;
                        sale.Book = book;
                        store.Sales = [sale];
                        return store;
                    },
                    splitOn: "SplitCol"
                    );

                return GroepeerWinkels(stores);
            }
        }

        private static IEnumerable<Store> GroepeerWinkels(IEnumerable<Store> stores)
        {
            var gegroepeerd = stores.GroupBy(store => store.Name);

            List<Store> winkelsMetVerkopen = new List<Store>();

            foreach (var groep in gegroepeerd)
            {
                var winkel = groep.First();
                List<Sale> alleVerkopen = new List<Sale>();

                foreach (var s in groep)
                {
                    alleVerkopen.Add(s.Sales.First());
                }

                winkel.Sales = alleVerkopen;
                winkelsMetVerkopen.Add(winkel);
            }

            return winkelsMetVerkopen;
        }
        // --->
    }
}