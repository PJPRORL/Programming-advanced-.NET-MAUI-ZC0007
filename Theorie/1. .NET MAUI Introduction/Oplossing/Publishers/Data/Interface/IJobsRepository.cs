using Publishers.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Publishers.Data.Interface
{
    public interface IJobsRepository
    {
        public IEnumerable<Job> OphalenJobs();
    }
}