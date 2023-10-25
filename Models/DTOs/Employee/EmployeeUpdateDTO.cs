
namespace EmployeeHub_MinimalAPI.Models.DTOs.Employee
{
	public class EmployeeUpdateDTO
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Email { get; set; }
		public string Password { get; set; }
		public int VacationDays { get; set; }
		public Boolean isAdmin { get; set; }
		public string Salt { get; set; }
	}
}
