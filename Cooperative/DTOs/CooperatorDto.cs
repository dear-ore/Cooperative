using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cooperative.DTOs
{
    public class AddCooperatorDto
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string CoopNumber { get; set; }

        [Required]
        public string StaffNumber { get; set; }

        [Required]
        public string Factory { get; set; }
    }
    public class CooperatorResponseDto
    {
        public string Name { get; set; }
        public string CoopNumber { get; set; }
        public string StaffNumber { get; set; }
        public string Factory { get; set; }
    }
}
