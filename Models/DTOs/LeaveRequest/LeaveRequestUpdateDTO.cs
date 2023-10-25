
namespace EmployeeHub_MinimalAPI.Models.DTOs.LeaveRequest
{
	public class LeaveRequestUpdateDTO
	{
		public int Id { get; set; }
		public int Pending { get; set; }
		public string ResponseMessage { get; set; }
	}
}
