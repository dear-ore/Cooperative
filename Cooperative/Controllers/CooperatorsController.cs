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
                return NotFound();
            }

            var response = new CooperatorResponseDto
            {
                Name = cooperator.Name,
                StaffNumber = cooperator.StaffNumber,
                CoopNumber = cooperator.CoopNumber,
                Factory = cooperator.Factory,
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

            await _context.Cooperators.AddAsync(cooperatorObj);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetCooperatorById), new { id = cooperatorObj.Id }, cooperatorObj);
        }
    
    }
}
          
    

