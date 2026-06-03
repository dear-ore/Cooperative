using Cooperative.Models;

namespace Cooperative.DTOs
{
    public class TakeLoanDto
    {
        public int Id { get; set; }
        public decimal Amount{ get; set; }
        public TransactionType Transaction { get; set; }
    }
    public class TakeFoodDto
    {
        public decimal Amount { get; set; }
        public int Id { get; set; }
        public int NumberOfInstallments { get; set; }
        public string Description { get; set; }
        public int ReceiptNumber { get; set; }
    }
    public class TakeSouvenirDto
    {
        public decimal Amount { get; set; }
        public int Id { get; set; }
        public string Description { get; set; }
        public int NumberOfInstallments { get; set; }
    }

    public class MakeRepaymentDto
    {
        public decimal? LoanAmount { get; set; }
        public decimal? SouvenirAmount { get; set; }
        public decimal? FoodAmount { get; set; }
        public int Id { get; set; }
        public PaymentMethod PayMethod { get; set; }
        public int ReceiptNumber { get; set; }
    }
}
