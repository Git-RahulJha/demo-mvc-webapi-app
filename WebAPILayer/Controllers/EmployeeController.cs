using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using ModelLayer;

namespace WebAPILayer.Controllers
{
    public class EmployeeController : ApiController
    {
        List<Employee> employees;
        public EmployeeController()
        {
            employees = new List<Employee>();
            employees.Add(new Employee { ID = 1, Name = "Rahul", ContactNumber = 9999999999, Address = "Test address" });
            employees.Add(new Employee { ID = 2, Name = "Jon", ContactNumber = 9900990099, Address = "Testing address" });
        }
        
       [Filters.CustomAuthentication]
        public IEnumerable<Employee> Get()
        {
           //string result = Newtonsoft.Json.JsonConvert.SerializeObject(employees);
            return employees;
        }

        [Filters.CustomAuthentication]
        public Employee Get(int id)
        {
            return employees.FirstOrDefault<Employee>(x => x.ID.Equals(id));
        }
    }
}
