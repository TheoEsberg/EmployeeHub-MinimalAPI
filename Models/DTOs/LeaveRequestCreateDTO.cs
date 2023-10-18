using System.ComponentModel.DataAnnotations;

namespace EmployeeHub_MinimalAPI.Models.DTOs
{
	public class LeaveRequestCreateDTO
	{
		[Required]
		public int LeaveTypeId { get; set; }
		[Required]
		public DateTime StartDate { get; set; }
		[Required]
		public DateTime EndDate { get; set; }
	}
}
