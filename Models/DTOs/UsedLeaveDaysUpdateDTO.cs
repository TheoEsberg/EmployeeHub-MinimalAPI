using System.ComponentModel.DataAnnotations;

namespace EmployeeHub_MinimalAPI.Models.DTOs
{
	public class UsedLeaveDaysUpdateDTO
	{
		public int Id { get; set; }
		public int Days { get; set; }
	}
}
