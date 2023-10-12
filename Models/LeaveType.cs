using System.ComponentModel.DataAnnotations;

namespace EmployeeHub_MinimalAPI.Models
{
	public class LeaveType
	{
		[Key]
		public int Id { get; set; }
		[Required]
		public string Name { get; set; }
		[Required]
		public int MaxDays { get; set; }

		public ICollection<LeaveRequest> LeaveRequest { get; set; }
	}
}
