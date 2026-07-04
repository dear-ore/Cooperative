using Cooperative.Helpers;

namespace Cooperative.Services
{
    public interface IContributionService
    {
        public Task<ServiceResult> RecordContribution(int cooperatorId, decimal? buildingamount, decimal? sharesamount, decimal? investmentamount, decimal? savingsamount);
    }
}
