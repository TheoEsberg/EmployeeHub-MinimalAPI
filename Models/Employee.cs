using Microsoft.Identity.Client;
using System.ComponentModel.DataAnnotations;

namespace EmployeeHub_MinimalAPI.Models
{
	public class Employee
	{
		[Key]
		public int Id { get; set; }
		[Required]
		public string Name { get; set; }
		[Required]
		public string Email { get; set; }
		[Required]
		public string Password { get; set; }
		public int VacationDays { get; set; }
		public Boolean isAdmin { get; set; }

		public ICollection<LeaveRequest> LeaveRequest { get; set; }

		public string Salt { get; set; }
	}
}
