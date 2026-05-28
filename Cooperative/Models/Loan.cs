using System.ComponentModel.DataAnnotations.Schema;

namespace Cooperative.Models
{
    public enum TransactionType
    {
        BankTransfer,
        Cheque
    }
    public class Loan
    {
        public int Id { get; set; }
        public TransactionType LoanTransactionType { get; set; }
        public int CooperatorId { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal PrincipalAmount { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalRepayable{ get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal MonthlyInstallment { get; set; }

        public DateTime DateTaken { get; set; }
        
    }
}
