namespace Cooperative.Models
{
    public class Loan
    {
        public int Id { get; set; }
        public int CooperatorId { get; set; }
        public decimal PrincipalAmount { get; set; }
        public decimal TotalRepayable{ get; set; }
        public int MonthlyInstallment { get; set; }
        public DateTime DateTaken { get; set; }
        public int InstallmentsRemaining { get; set; }
    }
}
