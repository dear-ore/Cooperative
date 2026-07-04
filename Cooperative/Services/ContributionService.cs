using Cooperative.Data;
using Cooperative.Helpers;
using Cooperative.Models;
using Microsoft.EntityFrameworkCore;

namespace Cooperative.Services
{
    public class ContributionService : IContributionService
    {
        private readonly CooperativeDbContext _context;
        public ContributionService(CooperativeDbContext context)
        {
            _context = context;
        }
        private const decimal MaxShares = 30000m;
        private const decimal MaxInvestment = 80000m;
        private const decimal MaxBuildingFund = 30000m;
        private const decimal DevelopmentLevy = 500m;
        public async Task<ServiceResult> RecordContribution(int cooperatorId, decimal? buildingamount, decimal? sharesamount, decimal? investmentamount, decimal? savingsamount)
        {
            if ((buildingamount == null || buildingamount <= 0) &&
               (sharesamount == null || sharesamount <= 0) &&
               (investmentamount == null || investmentamount <= 0) &&
               (savingsamount == null || savingsamount == 0))
            {
                return new ServiceResult
                {
                    IsSuccess = false,
                    Message = "At least one payment amount must be provided.",
                    StatusCode = StatusCodes.Status400BadRequest,
                };
            }

            var cooperator = await _context.Cooperators.FirstOrDefaultAsync(c => c.Id == cooperatorId);
            if (cooperator == null)
            {
                return new ServiceResult
                {
                    IsSuccess = false,
                    Message = "Cooperator not found.",
                    StatusCode = StatusCodes.Status404NotFound
                };
            }

            if (cooperator.Status != CooperatorStatus.Active)
            {
                return new ServiceResult
                {
                    IsSuccess = false,
                    Message = "Only active cooperators make contribution.",
                    StatusCode = StatusCodes.Status400BadRequest
                };
            }
            
            if((buildingamount ?? 0) + (sharesamount ?? 0) + (investmentamount ?? 0) + (savingsamount ?? 0) + DevelopmentLevy != cooperator.MonthlyContribution)
            {
                return new ServiceResult
                {
                    IsSuccess = false,
                    Message = "Total contribution must be equal to monthly contribution limit.",
                    StatusCode = StatusCodes.Status400BadRequest
                };
            }

            decimal currentSavings = cooperator.SavingsBalance;

            if (cooperator.InvestmentBalance + (investmentamount ?? 0) > MaxInvestment)
            {
                currentSavings += (cooperator.InvestmentBalance + (investmentamount ?? 0)) - MaxInvestment;
                cooperator.InvestmentBalance = MaxInvestment;
            }
            else
            {
                cooperator.InvestmentBalance += investmentamount ?? 0;
            }

            if ((cooperator.BuildingFundBalance) + (buildingamount ?? 0) > MaxBuildingFund)
            {
                currentSavings += ((cooperator.BuildingFundBalance) + (buildingamount ?? 0)) - MaxBuildingFund;
                cooperator.BuildingFundBalance = MaxBuildingFund;
            }
            else
            {
                cooperator.BuildingFundBalance = (cooperator.BuildingFundBalance) + (buildingamount ?? 0);
            }

            if ((cooperator.SharesBalance) + (sharesamount ?? 0) > MaxShares)
            {
                currentSavings += ((cooperator.SharesBalance) + (sharesamount ?? 0)) - MaxShares;
                cooperator.SharesBalance = MaxShares;
            }
            else
            {
                cooperator.SharesBalance = (cooperator.SharesBalance    ) + (sharesamount ?? 0);
            }

            cooperator.SavingsBalance = currentSavings + (savingsamount ?? 0);

            await _context.SaveChangesAsync();

            return new ServiceResult
            {
                IsSuccess = true,
                Message = "Contribution recorded successfully.",
                StatusCode = StatusCodes.Status200OK
            };
        }
    }
}
