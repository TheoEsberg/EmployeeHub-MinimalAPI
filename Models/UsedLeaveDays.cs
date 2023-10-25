using System.ComponentModel.DataAnnotations;

namespace EmployeeHub_MinimalAPI.Models
{
	public class UsedLeaveDays
	{
		[Key]
		public int Id { get; set; }

		[Required]
		public int EmployeeId { get; set; }
		public ICollection<Employee> Employees { get; set;}

		[Required]
		public int LeaveTypeId { get; set; }
		public ICollection<LeaveType> LeaveTypes { get; set; }

		[Required]
		public int Days { get; set; }
	}
}
