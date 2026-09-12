using Dapper;
using Publishers.Models;
using System.Data;
using Microsoft.Data.SqlClient;
using Publishers.Data.Interface;

namespace Publishers.Data.Repository
{
    public class PublishersRepository : BaseRepository, IPublishersRepository
    {
        public IEnumerable<Publisher> OphalenPublishers()
        {
            string sql = "SELECT * FROM Publisher ORDER BY name";
            using (IDbConnection db = new SqlConnection(ConnectionString))
            {
                return db.Query<Publisher>(sql);
            }
        }

        public IEnumerable<Publisher> OphalenPublishers(string zoekterm)
        {
            string sql = "SELECT * FROM Publisher WHERE name LIKE '%' + @zoekterm + '%' ORDER BY name";
            using (IDbConnection db = new SqlConnection(ConnectionString))
            {
                return db.Query<Publisher>(sql, new { zoekterm = zoekterm });
            }
        }
    }
}