using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cooperative.Models
{
    public class Cooperator
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string CoopNumber { get; set; }
        public string StaffNumber { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal BuildingFundBalance { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal InvestmentBalance { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal SharesBalance { get; set; }
        public decimal LoanBalance { get; set; }
        public decimal FoodBalance { get; set; }
        public decimal SouvenirBalance { get; set; }
        public decimal OtherBalance { get; set; }
    }
}

    

