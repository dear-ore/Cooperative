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
}
