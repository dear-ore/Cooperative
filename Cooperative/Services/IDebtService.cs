using Cooperative.Helpers;
using Cooperative.Models;

namespace Cooperative.Services
{
    public interface IDebtService
    {
        public Task<ServiceResult> TakeLoan(decimal amount, int cooperatorId, TransactionType transactionType);
        public Task<ServiceResult> TakeFood(decimal amount, int cooperatorId);
        public Task<ServiceResult> TakeSouvenir(decimal amount, int cooperatorId, int numberOfInstallments);
    }
}
