using Publishers.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Publishers.Data.Interface
{
    public interface IEmployeesRepository
    {
        public IEnumerable<Employee> OphalenEmployees();

        public IEnumerable<Employee> OphalenEmployeesMetUitgeverEnJob(string uitgever);

        public List<Employee> OphalenEmployeesViaHireDate(DateTime hiredate);

        public List<Employee> OphalenEmployeesViaJobId(int jobId);

        public List<Employee> OphalenEmployeesViaPublisherId(int id);

        public List<Employee> OphalenEmployeesViaPublisheridEnJobid(int pubId, int jobId);

        public Employee OphalenEmployeeViaId(int id);
    }
}