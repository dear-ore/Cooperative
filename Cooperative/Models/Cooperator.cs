using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cooperative.Models
{
    public class Cooperator
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        [Required]
        public string CoopNumber { get; set; }

        [Required]
        public string StaffNumber { get; set; }

        [Required]
        public string Factory { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal BuildingFundBalance { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal InvestmentBalance { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal SharesBalance { get; set; }

        [Column(TypeName ="decimal(18,2)")]
        public decimal LoanBalance { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal FoodBalance { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal SouvenirBalance { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal OtherBalance { get; set; }
    }
}

    

