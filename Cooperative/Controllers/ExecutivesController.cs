using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Cooperative.Models;
using Cooperative.Data;

namespace Cooperative.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExecutivesController : ControllerBase
    {
        private readonly CooperativeDbContext _context;
        public ExecutivesController(CooperativeDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult GetAllExecutives()
        {
            var response = _context.Executives.ToList();
            return Ok(response);
        }

        [HttpGet("{id}")]
        public ActionResult GetExecutivesById(int id)
        {
            var executive = _context.Executives.FirstOrDefault(e => e.Id == id);
            if (executive == null)
            {
                return NotFound();
            }
            return Ok(executive);
        }

        [HttpPost]

        public ActionResult AddNewExecutives([FromBody] Executive newExecutive)
        {
            var executiveObj = new Executive();
            executiveObj.Name = newExecutive.Name;
            executiveObj.Email = newExecutive.Email;
            executiveObj.PhoneNumber = newExecutive.PhoneNumber;
            _context.Executives.Add(executiveObj);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetExecutivesById), new { id = executiveObj.Id }, executiveObj);
        }
    }
}
