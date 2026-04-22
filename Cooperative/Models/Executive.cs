using System.ComponentModel.DataAnnotations;

namespace Cooperative.Models
{
    public class Executive
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        [Required]
        public string PostHeld { get; set; }
    }
}


