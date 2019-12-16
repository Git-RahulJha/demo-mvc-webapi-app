using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using System.Net.Http;
using ModelLayer;
using System.Threading.Tasks;

namespace MVCPresentationLayer.Controllers
{
    public class EmployeeController : Controller
    {
        // GET: Employee
        public ActionResult Index()
        {
            IEnumerable<Employee> employees = GetEmployee();
            //or
            //IEnumerable<Employee> employees = GetEmployee(true).Result;
            return View(employees);
        }

        public ActionResult Get(int id)
        {
            Employee employee = GetEmployeeByID(id);
            List<Employee> employees = new List<Employee>();
            employees.Add(employee);
            return View("Index", employees);
        }

        Employee GetEmployeeByID(int id)
        {
            Employee employee = null;
            using(HttpClient client = new HttpClient())
            {
                string url = "http://localhost:9111/api/employee?id=" + id;
                Task<HttpResponseMessage> result = client.GetAsync(url);
                if (result.Result.IsSuccessStatusCode)
                {
                    Task<string> serializedResult = result.Result.Content.ReadAsStringAsync();
                    employee = Newtonsoft.Json.JsonConvert.DeserializeObject<Employee>(serializedResult.Result);
                }
            }
            return employee;
        }
        /// <summary>
        /// Way 1 - Using task to get result
        /// </summary>
        /// <returns></returns>
        IEnumerable<Employee> GetEmployee()
        {
            IEnumerable<Employee> employees = null;
            using (HttpClient client = new HttpClient())
            {
                string url = "http://localhost:9111/api/employee";
                Uri uri = new Uri(url);
                string input = "test:pass";
                byte[] array = System.Text.Encoding.ASCII.GetBytes(input);
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers
                    .AuthenticationHeaderValue("Bearer", Convert.ToBase64String(array));
               System.Threading.Tasks.Task<HttpResponseMessage> result = client.GetAsync(uri);
                if (result.Result.IsSuccessStatusCode)  
                { 
                    System.Threading.Tasks.Task<string> response =  result.Result.Content.ReadAsStringAsync();
                    employees = Newtonsoft.Json.JsonConvert.DeserializeObject<IEnumerable<Employee>>(response.Result);
                    
                }
            }
            return employees;
        }

        /// <summary>
        /// Way 2 - using async and return task
        /// </summary>
        /// <returns></returns>
        async System.Threading.Tasks.Task<IEnumerable<Employee>> GetEmployee(bool dummyParameter)
        {
            IEnumerable<Employee> employees = null;
            using (HttpClient client = new HttpClient())
            {
                string url = "https://localhost:9111/api/employee";
                Uri uri = new Uri(url);

                HttpResponseMessage result = await client.GetAsync(uri);
                if (result.IsSuccessStatusCode)
                {
                    string response = await result.Content.ReadAsStringAsync();
                    employees = Newtonsoft.Json.JsonConvert.DeserializeObject<IEnumerable<Employee>>(response);

                }
            }
            return employees;
        }
    }
}