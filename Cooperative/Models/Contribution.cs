using System.ComponentModel.DataAnnotations.Schema;

namespace Cooperative.Models
{
    public class Contribution
    {
        public int Id { get; set; }
        public int CooperatorId { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal? SavingsAmount { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal? InvestmentAmount { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal? SharesAmount { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal? BuildingFundAmount { get; set; }
        public DateTime DateRecorded { get; set; }
    }
}
