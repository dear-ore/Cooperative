using Cooperative.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cooperative.DTOs
{
    public class AddCooperatorDto
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public int CoopNumber { get; set; }

        [Required]
        public int StaffNumber { get; set; }

        [Required]
        public string Factory { get; set; }
    }
    public class UpdateCooperatorDto
    {
        public string? Name { get; set; }
        public string? CoopNumber { get; set; }
        public string? StaffNumber { get; set; }
        public string? Factory { get; set; }
        public CooperatorStatus? Status { get; set; }
        public string? PostHeld { get; set; }
        public string? Department { get; set; }
        public string? MaritalStatus { get; set; }
    }
    public class CooperatorResponseDto
    {
        public string Name { get; set; }
        public int CoopNumber { get; set; }
        public int StaffNumber { get; set; }
        public string Factory { get; set; }
        public CooperatorStatus Status { get; set; }
    }
}
