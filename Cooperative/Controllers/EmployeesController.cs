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
        public async Task<ActionResult> GetAllEmployees()
        {
            var response = _context.Employees.Select(e => new EmployeeResponseDto
            {
                Name = e.Name,
                PhoneNumber = e.PhoneNumber,
                Factory = e.Factory
            }).ToList();
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetEmployeesById(int id)
        {
            var employee = _context.Employees.FirstOrDefault(e => e.Id == id);
            if (employee == null)
            {
                return NotFound();
            }

            var response = new EmployeeResponseDto
            {
                Name = employee.Name,
                PhoneNumber = employee.PhoneNumber,
                Factory = employee.Factory
            };
            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult> AddEmployees([FromBody] CreateEmployeeDto newEmployee)
        {
            var employeeObj = new Employee();
            employeeObj.Name = newEmployee.Name;
            employeeObj.Email = newEmployee.Email;
            employeeObj.PhoneNumber = newEmployee.PhoneNumber;
            employeeObj.Factory = newEmployee.Factory;
            await _context.Employees.AddAsync(employeeObj);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetEmployeesById), new { id = employeeObj.Id }, employeeObj);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateEmployees(int id, [FromBody] EditEmployeeDto updatedEmployee)
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
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteEmployees(int id)
        {
            var employee = _context.Employees.FirstOrDefault(e => e.Id == id);
            if (employee == null)
            {
                return NotFound();
            }
            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();
            return NoContent();
        }   
    }
}
