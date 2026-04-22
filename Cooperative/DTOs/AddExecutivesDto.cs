using System.ComponentModel.DataAnnotations;

namespace Cooperative.DTOs
{
        public class AddExecutivesDto
        {
            [Required]
            public string Name { get; set; }

            [Required]
            public string Email { get; set; }

            [Required]
            public string PhoneNumber { get; set; }

            [Required]
            public string PostHeld { get; set; }
        }

}
