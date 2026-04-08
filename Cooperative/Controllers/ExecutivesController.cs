using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Cooperative.Models;
using Cooperative.Data;
using Cooperative.DTOs;

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
        public ActionResult AddNewExecutives([FromBody] AddExecutivesDto newExecutive)
        {
            var executiveObj = new Executive();
            executiveObj.Name = newExecutive.Name;
            executiveObj.Email = newExecutive.Email;
            executiveObj.PhoneNumber = newExecutive.PhoneNumber;
            _context.Executives.Add(executiveObj);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetExecutivesById), new { id = executiveObj.Id }, executiveObj);
        }

        [HttpPut("{id}")]
        public ActionResult EditExecutive([FromBody] AddExecutivesDto editedExecutive, int id)
        {
            var executive = _context.Executives.FirstOrDefault(e => e.Id == id);
            if(executive == null)
            {
                return NotFound();
            }

            var executiveObj = new Executive();
            executiveObj.Name = editedExecutive.Name;
            executiveObj.PhoneNumber = editedExecutive.PhoneNumber;
            executiveObj.Email = editedExecutive.Email;
            executiveObj.PostHeld = editedExecutive.PostHeld;
            _context.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{id})")]
        public ActionResult DeleteExecutive(int id)
        {
            var executive = _context.Executives.FirstOrDefault(e => e.Id == id);
            if(executive == null)
            {
                return NotFound();
            }

            _context.Executives.Remove(executive);
            _context.SaveChanges();
            return NoContent();
        }

    }
}
