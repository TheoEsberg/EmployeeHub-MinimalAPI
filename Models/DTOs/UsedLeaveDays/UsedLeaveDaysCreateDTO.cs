﻿using System.ComponentModel.DataAnnotations;

namespace EmployeeHub_MinimalAPI.Models.DTOs.UsedLeaveDays
{
	public class UsedLeaveDaysCreateDTO
	{
		[Required]
		public int EmployeeId { get; set; }

		[Required]
		public int LeaveTypeId { get; set; }
	}
}
