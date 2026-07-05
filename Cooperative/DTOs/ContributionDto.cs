namespace Cooperative.DTOs
{
    public class ContributionDto
    {
          public int CooperatorId { get; set; }
          public decimal? BuildingAmount { get; set; }
          public decimal? SharesAmount { get; set; }
          public decimal? InvestmentAmount { get; set; }
          public decimal? SavingsAmount { get; set; }
    }
}
