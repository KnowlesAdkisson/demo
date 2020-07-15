using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Data.SqlClient;

namespace app06_web_api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        // [HttpGet]
        // public IEnumerable<WeatherForecast> Get()
        // {
        //     var rng = new Random();
        //     return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        //     {
        //         Date = DateTime.Now.AddDays(index),
        //         TemperatureC = rng.Next(-20, 55),
        //         Summary = Summaries[rng.Next(Summaries.Length)]
        //     })
        //     .ToArray();
        // }

        public class Employee
        {
            public int EmpID { get; set; }
            public string EmpFirstName { get; set; }
            public string EmpLastName { get; set; }
            public decimal EmpSalary { get; set; }
            public int DeptID { get; set; }
        }

        [HttpGet("{deptID}")]
        public IEnumerable<Employee> Get(int DeptID)
        {
            List<Employee> employees = new List<Employee>();

            string connectionString = @"Server=LAPTOP-ALPTJ9MA\SQLEXPRESS;Database=db01;Trusted_Connection=True;";

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText = "select * from Employees where DeptID = @DeptID";
                command.Parameters.AddWithValue("@DeptID", DeptID);

                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Employee employee = new Employee();
                    employee.EmpID = reader.GetInt32(reader.GetOrdinal("EmpID"));
                    employee.EmpFirstName = reader.GetString(reader.GetOrdinal("EmpFirstName"));
                    employee.EmpLastName = reader.GetString(reader.GetOrdinal("EmpLastName"));
                    if (!reader.IsDBNull(reader.GetOrdinal("EmpSalary")))
                    {
                        employee.EmpSalary = reader.GetDecimal(reader.GetOrdinal("EmpSalary"));
                    }
                    employee.DeptID = reader.GetInt32(reader.GetOrdinal("DeptID"));

                    employees.Add(employee);
                }
            }

            return employees;
        }
    }
}
