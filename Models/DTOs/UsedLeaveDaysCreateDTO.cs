using System.ComponentModel.DataAnnotations;

namespace EmployeeHub_MinimalAPI.Models.DTOs
{
	public class UsedLeaveDaysCreateDTO
	{
		[Required]
		public int EmployeeId { get; set; }

		[Required]
		public int LeaveTypeId { get; set; }

		[Required]
		public int Days { get; set; }
	}
}
