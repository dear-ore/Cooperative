using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Cooperative.Models;
using Cooperative.Data;
using Cooperative.DTOs;

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
        public ActionResult AddEmployees([FromBody] CreateEmployeeDto newEmployee)
        {
            var employeeObj = new Employee();
            employeeObj.Name = newEmployee.Name;
            employeeObj.Email = newEmployee.Email;
            employeeObj.PhoneNumber = newEmployee.PhoneNumber;
            employeeObj.Factory = newEmployee.Factory;
            _context.Employees.Add(employeeObj);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetEmployeesById), new { id = employeeObj.Id }, employeeObj);
        }

        [HttpPut("{id}")]
        public ActionResult UpdateEmployees(int id, [FromBody] EditEmployeeDto updatedEmployee)
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
