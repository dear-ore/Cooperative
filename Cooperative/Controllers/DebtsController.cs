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

        [HttpPost("Loan")]
        public async Task<ActionResult> TakeLoan([FromBody] TakeLoanDto request)
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

            return StatusCode(result.StatusCode, result.Message);
        }

        [HttpPost("Food")]
        public async Task<ActionResult> TakeFood([FromBody] TakeFoodDto request)
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

            return StatusCode(result.StatusCode, result.Message);
        }

        [HttpPost("Souvenir")]
        public async Task<ActionResult> TakeSouvenir([FromBody] TakeSouvenirDto request)
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

            return StatusCode(result.StatusCode, result.Message);
        }

        [HttpPost("Repayment")]
        public async Task<ActionResult> MakeRepayment([FromBody] MakeRepaymentDto request)
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

            return StatusCode(result.StatusCode, result.Message);
        }        
    }
}
