using System.ComponentModel.DataAnnotations;

namespace EmployeeHub_MinimalAPI.Models
{
	public class LeaveRequest
	{
		[Key]
		public int Id { get; set; }
		[Required]
		public int EmployeeId { get; set;}
		public Employee Employee { get; set;}
		[Required]
		public int LeaveTypeId { get; set;}
		public LeaveType LeaveType { get; set;}
		[Required]
		public int Pending { get; set; } // -1 Denied, 0 Still Pending, 1 Approved
		public string ResponseMessage { get; set; }
		[Required]
		public DateTime StartDate { get; set; }
		[Required]
		public DateTime EndDate { get; set; }
		[Required]
		public DateTime RequestDate { get; set; }

	}
}
