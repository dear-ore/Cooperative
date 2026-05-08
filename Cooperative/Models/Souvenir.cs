using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cooperative.Models
{
    public class Souvenir
    {
        public int Id { get; set; }
        public int CooperatorId { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Amount { get; set; }
        [Required]
        public string Description { get; set; }
        public int NumberOfInstallments { get; set; }
        public DateTime DateTaken { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal MonthlyInstallment { get; set; }
    }
}
