using EmployeeHub_MinimalAPI.Models.DTOs;
using EmployeeHub_MinimalAPI.Services.Password;

namespace EmployeeHub_MinimalAPI.Services
{
	public interface ILeaveRequest<T>
	{
		Task<IEnumerable<T>> GetAllAsync();
		Task<T> GetAsync(int id);
		Task<T> CreateAsync(LeaveRequestCreateDTO dto, int id);
		Task<T> UpdateAsync(LeaveRequestUpdateDTO dto);
		Task<T> DeleteAsync(int id);
	}
}
