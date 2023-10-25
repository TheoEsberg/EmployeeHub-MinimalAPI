﻿using EmployeeHub_MinimalAPI.Models.DTOs;

namespace EmployeeHub_MinimalAPI.Services
{
	public interface IUsedLeaveDays<T>
	{
		Task<IEnumerable<T>> GetAllAsync();
		Task<T> GetAsync(int id);
		Task<T> CreateAsync(UsedLeaveDaysCreateDTO dto);
		Task<T> UpdateDaysAsync(UsedLeaveDaysUpdateDTO dto);
		Task<T> DeleteAsync(int id);
		Task<IEnumerable<T>> GetByEmployeeId(int employeeId);
		Task<IEnumerable<T>> GetByLeaveTypeId(int leaveTypeId);
	}
}
