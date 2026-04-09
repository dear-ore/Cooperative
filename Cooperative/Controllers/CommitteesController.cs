using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Cooperative.Data;
using Cooperative.DTOs;
using Cooperative.Models;

namespace Cooperative.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommitteesController : ControllerBase
    {
        private readonly CooperativeDbContext _context;

        public CommitteesController(CooperativeDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult> GetAllCommitteess()
        {
            var response = _context.Committees.Select(c => new CommitteeResponseDto
            {
                Name = c.Name,
                PhoneNumber = c.PhoneNumber,
                Factory = c.Factory,
                Email = c.Email,
            }).ToList();
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetCommitteeById(int id)
        {
            var executive = _context.Committees.FirstOrDefault(e=> e.Id == id);
            if(executive == null)
            {
                return NotFound();
            }

            var response = new CommitteeResponseDto
            {
                Name = executive.Name,
                Email =executive.Email,
                PhoneNumber = executive.PhoneNumber,
                Factory = executive.Factory,
            };         
            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult> AddNewCommittee([FromBody] AddCommitteeDto newCommittee)
        {
            var committeeObj = new Committee();
            committeeObj.Name = newCommittee.Name;
            committeeObj.PhoneNumber = newCommittee.PhoneNumber;
            committeeObj.Email = newCommittee.Email;
            committeeObj.Factory = newCommittee.Factory;
            await _context.AddAsync(committeeObj);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetCommitteeById), new { id = committeeObj }, committeeObj);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> EditCommittee(int id, [FromBody] AddCommitteeDto editedCommittee)
        {
            var executive = _context.Committees.FirstOrDefault(e => e.Id == id);
            if (executive == null)
            {
                return NotFound();
            }

            executive.PhoneNumber = editedCommittee.PhoneNumber;
            executive.Email = editedCommittee.Email;
            executive.Name = editedCommittee.Name;
            executive.Factory = editedCommittee.Factory;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCommittee(int id)
        {
            var executive = _context.Committees.FirstOrDefault(e => e.Id == id);
            if (executive == null)
            {
                return NotFound();
            }

            _context.Committees.Remove(executive);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}


