using Cooperative.Services;
using Cooperative.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Cooperative.DTOs;

namespace Cooperative.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DebtsController : ControllerBase
    {
        private readonly IDebtService _service;
        
        public DebtsController(IDebtService service)
        {
            _service = service;
        }

        [HttpPost("Take-Loan")]
        public async Task<IActionResult> TakeLoan([FromBody] TakeLoanDto request)
        {
            var result = await _service.TakeLoan(
                request.Amount,
                request.Id,
                request.Transaction
            );

            if(result.IsSuccess)
            {
                return Ok(result.Message);
            }

            return BadRequest(result.Message);
        }

        [HttpPost("Take-Food")]
        public async Task<IActionResult> TakeFood([FromBody] TakeFoodDto request)
        {
            var result = await _service.TakeFood(
                request.Amount,
                request.Id,
                request.NumberOfInstallments,
                request.Description,
                request.ReceiptNumber
            );

            if (result.IsSuccess)
            {
                return Ok(result.Message);
            }

            return BadRequest(result.Message);
        }

        [HttpPost("Take-Souvenir")]
        public async Task<IActionResult> TakeSouvenir([FromBody] TakeSouvenirDto request)
        {
            var result = await _service.TakeSouvenir(
                request.Amount,
                request.Id,
                request.Description,
                request.NumberOfInstallments
            );

            if (result.IsSuccess)
            {
                return Ok(result.Message);
            }

            return BadRequest(result.Message);
        }

        [HttpPost("Make-Repayment")]
        public async Task<IActionResult> MakeRepayment([FromBody] MakeRepaymentDto request)
        {
            var result = await _service.MakeRepayment(
                request.LoanAmount,
                request.SouvenirAmount,
                request.FoodAmount,
                request.Id,
                request.PayMethod,
                request.ReceiptNumber

            );

            if (result.IsSuccess)
            {
                return Ok(result.Message);
            }

            return BadRequest(result.Message);
        }
            
    }
}
