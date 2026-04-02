using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Cooperative.Models;

namespace Cooperative.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private static List<Employees> employees = new List<Employees>
        {
            new Employees { Id = 1, Name = "Afolabi Bukunmi", Email = "afolabi@company.com", PhoneNumber = "1234567890", Factory = "Yale 1" },
            new Employees { Id = 2, Name = "Desola Jane", Email = "desola@company.com", PhoneNumber = "0987654321", Factory = "Yale 2" },
            new Employees { Id = 3, Name = "Oluwaseun Ade", Email = "ade@company.com", PhoneNumber = "5551234567", Factory = "Yale 3" },
        };

        [HttpGet]
        public ActionResult GetAllEmployees()
        {
            return Ok(employees);
        }

        [HttpGet("{id}")]
        public ActionResult GetEmployeesById(int id)
        {
            var employee = employees.FirstOrDefault(e => e.Id == id);
            if (employee == null)
            {
                return NotFound();
            }
            return Ok(employee);
        }

        [HttpPost]
        public ActionResult AddEmployees([FromBody] Employees newEmployee)
        {
            newEmployee.Id = employees.Count + 1;
            employees.Add(newEmployee);
            return CreatedAtAction(nameof(GetEmployeesById), new { id = newEmployee.Id }, newEmployee);
        }

        [HttpPut("{id}")]
        public ActionResult UpdateEmployees(int id, [FromBody] Employees updatedEmployee)
        {
            var employee = employees.FirstOrDefault(e => e.Id == id);
            if (employee == null)
            {
                return NotFound();
            }

            employee.Name = updatedEmployee.Name;
            employee.Email = updatedEmployee.Email;
            employee.PhoneNumber = updatedEmployee.PhoneNumber;
            employee.Factory = updatedEmployee.Factory;
            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteEmployees(int id)
        {
            var employee = employees.FirstOrDefault(e => e.Id == id);
            if (employee == null)
            {
                return NotFound();
            }
            employees.Remove(employee);
            return NoContent();
        }   
    }
}
