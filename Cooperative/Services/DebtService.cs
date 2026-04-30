using Cooperative.Helpers;
using Cooperative.Data;
using Cooperative.Models;
using Microsoft.EntityFrameworkCore;

namespace Cooperative.Services
{
    public class DebtService : IDebtService
    {
        private readonly CooperativeDbContext _context;
        public DebtService(CooperativeDbContext context) 
        {
            _context = context; 
        }
        public async Task<ServiceResult> TakeLoan(decimal amount, int cooperatorId, TransactionType transactionType)
        {
            var cooperator = await _context.Cooperators.FirstOrDefaultAsync(c => c.Id == cooperatorId);
            if (cooperator == null)
            {
                return new ServiceResult
                {
                    IsSuccess = false,
                    Message = "Cooperator not found."
                };
            }

            if (cooperator.Status != CooperatorStatus.Active)
            {
                return new ServiceResult
                {
                    IsSuccess = false,
                    Message = "Only active cooperators can take loans."
                };
            }

            if(cooperator.MembershipCommencementDate > DateTime.Now.AddMonths(-6))
            {
                return new ServiceResult
                {
                    IsSuccess = false,
                    Message = "Cooperator must have been a member for at least 6 months to take a loan."
                };
            }

            if (await _context.Loans.AnyAsync(l => l.CooperatorId == cooperatorId && l.TotalRepayable > 0))
            {
                return new ServiceResult
                {
                    IsSuccess = false,
                    Message = "Cooperator already has an outstanding loan."
                };
            }

            decimal interest = amount * 0.1m;
            decimal totalRepayable = amount + interest;
            decimal monthlyRepayment = totalRepayable / 11;

            var loan = new Loan
            {
                CooperatorId = cooperatorId,
                PrincipalAmount = amount,
                TotalRepayable = totalRepayable,
                MonthlyInstallment = monthlyRepayment,
                DateTaken = DateTime.Now,
                InstallmentsRemaining = 11,
                LoanTransactionType = transactionType
            };
            cooperator.LoanBalance = -totalRepayable;
            await _context.Loans.AddAsync(loan);
            await _context.SaveChangesAsync();

            return new ServiceResult
            {
                IsSuccess = true,
                Message = "Loan taken successfully."
            };
        }
        public async Task<ServiceResult> TakeFood(decimal amount, int cooperatorId, int numberofinstallments, string description, int receiptNumber)
        {
            var cooperator = await _context.Cooperators.FirstOrDefaultAsync(c => c.Id == cooperatorId);
            if(cooperator == null)
            {
                return new ServiceResult
                {
                    IsSuccess = false,
                    Message = "Cooperator not found."
                };
            }

            if(cooperator.Status != CooperatorStatus.Active)
            {
                return new ServiceResult
                {
                    IsSuccess = false,
                    Message = "Only active cooperators can take food."
                };
            }

            decimal monthlyInstallment = amount / numberofinstallments;

            var food = new Food
            {
                CooperatorId = cooperatorId,
                Amount = amount,
                Description = description,
                NumberOfInstallments = numberofinstallments,
                DateTaken = DateTime.Now,
                MonthlyInstallment = monthlyInstallment,
                ReceiptNumber = receiptNumber
            };

            cooperator.FoodBalance = -amount;
            await _context.AddAsync(food);
            await _context.SaveChangesAsync();

            return new ServiceResult
            {
                IsSuccess = true,
                Message = "Food recorded successfully!"
            };
        }
        public async Task<ServiceResult> TakeSouvenir(decimal amount, int cooperatorId, int numberOfInstallments)
        {
            throw new NotImplementedException();
        }
    }
}
