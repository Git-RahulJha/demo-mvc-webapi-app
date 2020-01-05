using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ModelLayer;

namespace EntityFrameworkLayer
{
    public class EmployeeManager
    {
        public IEnumerable<Employee> GetAllEmployee()
        {
            List<Employee> employees = null;
            using(PRACCONEntities entities = new PRACCONEntities())
            {
                employees = entities.EMPLOYEEs.AsEnumerable().Select(x => new Employee { ID = x.ID, Name = x.FirstName + " " + x.LastName,
                ContactNumber = Convert.ToInt64(x.ContactNumber), Address = x.Address}).ToList();
            }
            return employees;
        }

        public Employee GetEmployeeByID(int id)
        {
            Employee employee = null;
            using (PRACCONEntities entities = new PRACCONEntities())
            {
                employee = entities.EMPLOYEEs.AsEnumerable().Where(x=> x.ID == id).Select(x => new Employee
                {
                    ID = x.ID,
                    Name = x.FirstName + " " + x.LastName,
                    ContactNumber = Convert.ToInt64(x.ContactNumber),
                    Address = x.Address
                }).FirstOrDefault();
            }
            return employee;
        }
    }
}
