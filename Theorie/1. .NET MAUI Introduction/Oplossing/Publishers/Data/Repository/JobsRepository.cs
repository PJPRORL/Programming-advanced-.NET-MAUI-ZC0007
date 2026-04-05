using Dapper;
using Publishers.Models;
using System.Data;
using Microsoft.Data.SqlClient;
using Publishers.Data.Interface;

namespace Publishers.Data.Repository
{
    public class JobsRepository : BaseRepository, IJobsRepository
    {
        public IEnumerable<Job> OphalenJobs()
        {
            string sql = "SELECT * FROM job ORDER BY description";
            using (IDbConnection db = new SqlConnection(ConnectionString))
            {
                return db.Query<Job>(sql);
            }
        }
    }
}