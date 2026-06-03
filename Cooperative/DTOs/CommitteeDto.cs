using System.ComponentModel.DataAnnotations;

namespace Cooperative.DTOs
{
    public class AddCommitteeDto
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        [Required]
        public string Factory { get; set; }
    }

    public class CommitteeResponseDto
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Factory { get; set; }
    }
}
