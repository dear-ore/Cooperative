using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Cooperative.DTOs;
using Cooperative.Services;

namespace Cooperative.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContributionsController : ControllerBase
    {
        private readonly IContributionService _contributionService;
        public ContributionsController(IContributionService contributionService)
        {
            _contributionService = contributionService;
        }

        // POST: api/Contributions
        [HttpPost]
        public async Task<IActionResult> RecordContribution([FromBody] ContributionDto contributionDto)
        {
            var result = await _contributionService.RecordContribution(
                contributionDto.CooperatorId,
                contributionDto.BuildingAmount,
                contributionDto.SharesAmount,
                contributionDto.InvestmentAmount,
                contributionDto.SavingsAmount
            );

            return StatusCode(result.StatusCode, result.Message);
        }  
    }
}