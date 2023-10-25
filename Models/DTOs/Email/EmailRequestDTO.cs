namespace EmployeeHub_MinimalAPI.Models.DTOs.Email
{
    public class EmailRequestDTO
    {
        public string To { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
    }
}
