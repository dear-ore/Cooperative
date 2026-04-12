using System.ComponentModel.DataAnnotations.Schema;

namespace Cooperative.DTOs
{
    public class AddCooperatorDto
    {
        public string Name { get; set; }
        public string CoopNumber { get; set; }
        public string StaffNumber { get; set; }
        public string Factory { get; set; }
    }
}
