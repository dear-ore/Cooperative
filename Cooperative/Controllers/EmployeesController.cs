using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Cooperative.Models;
using Cooperative.Data;

namespace Cooperative.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {        
        private readonly CooperativeDbContext _context;
        public EmployeesController(CooperativeDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult GetAllEmployees()
        {
            var response = _context.Employees;
            return Ok(response);
        }

        [HttpGet("{id}")]
        public ActionResult GetEmployeesById(int id)
        {
            var employee = _context.Employees.FirstOrDefault(e => e.Id == id);
            if (employee == null)
            {
                return NotFound();
            }
            return Ok(employee);
        }

        [HttpPost]
        public ActionResult AddEmployees([FromBody] Employees newEmployee)
        {
            _context.Employees.Add(newEmployee);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetEmployeesById), new { id = newEmployee.Id }, newEmployee);
        }

        [HttpPut("{id}")]
        public ActionResult UpdateEmployees(int id, [FromBody] Employees updatedEmployee)
        {
            var employee = _context.Employees.FirstOrDefault(e => e.Id == id);
            if (employee == null)
            {
                return NotFound();
            }

            employee.Name = updatedEmployee.Name;
            employee.Email = updatedEmployee.Email;
            employee.PhoneNumber = updatedEmployee.PhoneNumber;
            employee.Factory = updatedEmployee.Factory;
            _context.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteEmployees(int id)
        {
            var employee = _context.Employees.FirstOrDefault(e => e.Id == id);
            if (employee == null)
            {
                return NotFound();
            }
            _context.Employees.Remove(employee);
            _context.SaveChanges();
            return NoContent();
        }   
    }
}
