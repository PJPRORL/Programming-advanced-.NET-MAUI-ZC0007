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
    }
}