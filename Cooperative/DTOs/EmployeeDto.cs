namespace Cooperative.DTOs
{
    public class CreateEmployeeDto
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Factory { get; set; }
    }

    public class EditEmployeeDto
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Factory { get; set; }
    }

    public class EmployeeResponseDto
    {
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string Factory { get; set; }
    }
}

