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
        public int CooperativeId { get; set; }
        public int ReceiptNumber { get; set; }
        public int MyProperty { get; set; }
        public PaymentMethod DeductionMethod { get; set; }
        public DateTime DateOfRepayment { get; set; }
        public decimal? LoanRepaymentAmount { get; set; }
        public decimal? FoodRepaymentAmount { get; set; }
        public decimal? SouvenirRepaymentAmount { get; set; }
    }
}
