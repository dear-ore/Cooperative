using Cooperative.Data;
using Cooperative.DTOs;
using Cooperative.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Cooperative.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CooperatorsController : ControllerBase
    {
        private readonly CooperativeDbContext _context;
        public CooperatorsController(CooperativeDbContext context)
        {
            _context = context;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetCooperatorById(int id)
        {
            var cooperator = _context.Cooperators.FirstOrDefault(c => c.Id == id);
            if (cooperator == null)
            {
                return NotFound("Cooperator not found.");
            }

            var response = new CooperatorResponseDto
            {
                Name = cooperator.Name,
                StaffNumber = cooperator.StaffNumber,
                CoopNumber = cooperator.CoopNumber,
                Factory = cooperator.Factory,
                Status = cooperator.Status
            };

            return Ok(response);
        }


        [HttpGet]
        public async Task<ActionResult> GetAllCooperators()
        {
            var response = _context.Cooperators.Select(c => new CooperatorResponseDto
            {
                Name = c.Name,
                Factory = c.Factory,
                StaffNumber = c.StaffNumber,
                CoopNumber = c.CoopNumber,
                Status = c.Status
            }).ToList();

            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult> AddNewCooperator([FromBody] AddCooperatorDto newCooperator)
        {
            var cooperatorObj = new Cooperator();
            cooperatorObj.Name = newCooperator.Name;
            cooperatorObj.CoopNumber = newCooperator.CoopNumber;
            cooperatorObj.StaffNumber = newCooperator.StaffNumber;
            cooperatorObj.Factory = newCooperator.Factory;
            cooperatorObj.SharesBalance = 0;
            cooperatorObj.FoodBalance = 0;
            cooperatorObj.SouvenirBalance = 0;
            cooperatorObj.BuildingFundBalance = 0;
            cooperatorObj.InvestmentBalance = 0;
            cooperatorObj.LoanBalance = 0;
            cooperatorObj.OtherBalance = 0;
            cooperatorObj.Status = CooperatorStatus.Active;
            await _context.Cooperators.AddAsync(cooperatorObj);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetCooperatorById), new { id = cooperatorObj.Id }, cooperatorObj);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateCooperator(int id, [FromBody] UpdateCooperatorDto updateCooperator)
        {
            var cooperator = _context.Cooperators.FirstOrDefault(c => c.Id == id);
            if (cooperator == null)
            {
                return NotFound("Cooperator not found.");
            }

            if (!string.IsNullOrEmpty(updateCooperator.Name))
                cooperator.Name = updateCooperator.Name;

            if (!string.IsNullOrEmpty(updateCooperator.CoopNumber) && int.TryParse(updateCooperator.CoopNumber, out var coopNumber))
                cooperator.CoopNumber = coopNumber;

            if (!string.IsNullOrEmpty(updateCooperator.StaffNumber) && int.TryParse(updateCooperator.StaffNumber, out var staffNumber))
                cooperator.StaffNumber = staffNumber;

            if (!string.IsNullOrEmpty(updateCooperator.Factory))
                cooperator.Factory = updateCooperator.Factory;

            if (!string.IsNullOrEmpty(updateCooperator.PostHeld))
                cooperator.PostHeld = updateCooperator.PostHeld;

            if (!string.IsNullOrEmpty(updateCooperator.Department))
                cooperator.Department = updateCooperator.Department;

            if (!string.IsNullOrEmpty(updateCooperator.MaritalStatus))
                cooperator.MaritalStatus = updateCooperator.MaritalStatus;

            if (updateCooperator.Status.HasValue)
                cooperator.Status = updateCooperator.Status.Value;

            _context.Cooperators.Update(cooperator);
            await _context.SaveChangesAsync();

            var response = new CooperatorResponseDto
            {
                Name = cooperator.Name,
                StaffNumber = cooperator.StaffNumber,
                CoopNumber = cooperator.CoopNumber,
                Factory = cooperator.Factory,
                Status = cooperator.Status
            };

            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCooperator(int id)
        {
            var cooperator = _context.Cooperators.FirstOrDefault(c => c.Id == id);
            if (cooperator == null)
            {
                return NotFound("Cooperator not found.");
            }

            _context.Cooperators.Remove(cooperator);
            await _context.SaveChangesAsync();

            return Ok("Cooperator deleted successfully.");
        }
    }
}
          
    

