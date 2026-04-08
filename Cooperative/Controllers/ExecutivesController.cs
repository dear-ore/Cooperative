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
            var response = _context.Executives.Select(e => new ExecutiveResponseDto
            {
                Name = e.Name,
                Email = e.Email,
                PostHeld = e.PostHeld,
                PhoneNumber = e.PhoneNumber,

            }).ToList();
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

            var response = new ExecutiveResponseDto
            {
                Name = executive.Name,
                Email = executive.Email,
                PostHeld = executive.PostHeld,
                PhoneNumber = executive.PhoneNumber
            };

            return Ok(response);
        }

        [HttpPost]
        public ActionResult AddNewExecutives([FromBody] AddExecutivesDto newExecutive)
        {
            var executiveObj = new Executive();
            executiveObj.Name = newExecutive.Name;
            executiveObj.Email = newExecutive.Email;
            executiveObj.PhoneNumber = newExecutive.PhoneNumber;
            executiveObj.PostHeld = newExecutive.PostHeld;
            _context.Executives.Add(executiveObj);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetExecutivesById), new { id = executiveObj.Id }, executiveObj);
        }

        [HttpPut("{id}")]
        public ActionResult EditExecutive(int id, [FromBody] AddExecutivesDto editedExecutive)
        {
            var executive = _context.Executives.FirstOrDefault(e => e.Id == id);
            if(executive == null)
            {
                return NotFound();
            }

            executive.Name = editedExecutive.Name;
            executive.PhoneNumber = editedExecutive.PhoneNumber;
            executive.Email = editedExecutive.Email;
            executive.PostHeld = editedExecutive.PostHeld;
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
