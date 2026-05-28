using Microsoft.Extensions.Options;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cooperative.Models
{
    public enum PaymentMethod
    {
        Bank,
        Management
    }
    public class Repayment
    {
        public int Id { get; set; }
        public int CooperatorId { get; set; }
        public int ReceiptNumber { get; set; }
        public PaymentMethod DeductionMethod { get; set; }
        public DateTime DateOfRepayment { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal? LoanRepaymentAmount { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal? FoodRepaymentAmount { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal? SouvenirRepaymentAmount { get; set; }
    }
}
