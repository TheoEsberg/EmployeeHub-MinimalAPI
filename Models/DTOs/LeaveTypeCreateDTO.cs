using System.ComponentModel.DataAnnotations;

namespace EmployeeHub_MinimalAPI.Models.DTOs
{
	public class LeaveTypeCreateDTO
	{
		[Required]
		public string Name { get;set; }
		[Required]
		public int MaxDays { get;set; }
	}
}
