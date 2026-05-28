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

            if (cooperator.LoanBalance < 0)
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
        public async Task<ServiceResult> TakeSouvenir(decimal amount, int cooperatorId, string description, int numberofInstallments)
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
                    Message = "Only active cooperators can take souvenir."
                };
            }

            decimal monthlyInstallment = amount / numberofInstallments;

            var souvenir = new Souvenir
            {
                CooperatorId = cooperatorId,
                Amount = amount,
                Description = description,
                NumberOfInstallments = numberofInstallments,
                DateTaken = DateTime.Now,
                MonthlyInstallment = monthlyInstallment,
            };

            cooperator.SouvenirBalance = -amount;
            await _context.AddAsync(souvenir);
            await _context.SaveChangesAsync();

            return new ServiceResult
            {
                IsSuccess = true,
                Message = "Souvenir recorded successfully!"
            };
        }
        public async Task<ServiceResult> MakeRepayment(decimal? loanamount, decimal? souveniramount, decimal? foodamount, int cooperatorId, PaymentMethod paymentmethod, int receiptNumber)
        {
            if ((loanamount == null || loanamount <= 0) &&
                (souveniramount == null || souveniramount <= 0) &&
                (foodamount == null || foodamount <= 0))
            {
                return new ServiceResult
                {
                    IsSuccess = false,
                    Message = "At least one repayment amount must be provided."
                };
            }

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
                    Message = "Only active cooperators can make repayments."
                };
            }
           
            if (loanamount != null && loanamount > 0)
            {
                if(loanamount > Math.Abs(cooperator.LoanBalance))
                {
                    return new ServiceResult
                    {
                        IsSuccess = false,
                        Message = "Repayment amount exceeds outstanding loan balance."
                    };
                }
                cooperator.LoanBalance += loanamount.Value;
            }

            if (souveniramount != null && souveniramount > 0)
            {
                if(souveniramount.Value > Math.Abs(cooperator.SouvenirBalance))
                {
                    return new ServiceResult
                    {
                        IsSuccess = false,
                        Message = "Repayment amount exceeds outstanding souvenir balance."
                    };
                }
                cooperator.SouvenirBalance += souveniramount.Value;
            }

            if (foodamount != null && foodamount > 0)
            {
                if(foodamount.Value > Math.Abs(cooperator.FoodBalance))
                {
                    return new ServiceResult
                    {
                        IsSuccess = false,
                        Message = "Repayment amount exceeds outstanding food balance."
                    };
                }
                cooperator.FoodBalance += foodamount.Value;
            }

            var repayment = new Repayment
            {
                CooperatorId = cooperatorId,
                ReceiptNumber = receiptNumber,
                DeductionMethod = paymentmethod,
                DateOfRepayment = paymentmethod == PaymentMethod.Bank ? new DateTime(DateTime.Now.Year, DateTime.Now.Month, 15) : new DateTime(DateTime.Now.Year, DateTime.Now.Month, 17),
                LoanRepaymentAmount = loanamount,
                SouvenirRepaymentAmount = souveniramount,
                FoodRepaymentAmount = foodamount
            };
            await _context.AddAsync(repayment);
            await _context.SaveChangesAsync();

            return new ServiceResult
            {
                IsSuccess = true,
                Message = "Repayment recorded successfully!"
            };
        }
    }
}
