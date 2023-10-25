
namespace EmployeeHub_MinimalAPI.Models.DTOs.UsedLeaveDays
{
	public class UsedLeaveDaysUpdateDTO
	{
		public int EmployeeId { get; set; }
		public int LeaveTypeId { get;set; }
		public int Days { get; set; }
	}
}
