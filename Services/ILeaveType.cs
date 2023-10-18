using EmployeeHub_MinimalAPI.Models.DTOs;

namespace EmployeeHub_MinimalAPI.Services
{
	public interface ILeaveType<T>
	{
		Task<IEnumerable<T>> GetAllAsync();
		Task<T> GetAsync(int id);
		Task<T> CreateAsync(LeaveTypeCreateDTO dto);
		Task<T> UpdateAsync(T entity);
		Task<T> DeleteAsync(int id);
	}
}
