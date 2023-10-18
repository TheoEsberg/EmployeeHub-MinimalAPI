using System.ComponentModel.DataAnnotations;

namespace EmployeeHub_MinimalAPI.Models.DTOs
{
	public class LeaveRequestUpdateDTO
	{
		public int Id { get; set; }
		public int Pending { get; set; }
		public string ResponseMessage { get; set; }
	}
}
