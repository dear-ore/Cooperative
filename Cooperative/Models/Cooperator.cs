using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cooperative.Models
{
    public enum CooperatorStatus
    {
        Pending,
        Active,
        Inactive,
        Withdrawn
    };
    public class Cooperator
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        [Required]
        public int CoopNumber { get; set; }

        [Required]
        public int StaffNumber { get; set; }

        [Required]
        public string Factory { get; set; }
        public DateTime DateOfEmployment { get; set; }
        public string PostHeld { get; set; }
        public string Department { get; set; }
        public bool IsMemberOfSimilarSociety { get; set; }
        public string? SimilarSocietyDetails { get; set; }
        public string MaritalStatus { get; set; }
        public DateTime MembershipCommencementDate { get; set; }
        public CooperatorStatus Status { get; set;  }

        [Column(TypeName = "decimal(18,2)")]
        public decimal MonthlyContribution { get; set; }

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

    

