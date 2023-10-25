using EmployeeHub_MinimalAPI.Models.DTOs.LeaveRequest;
using EmployeeHub_MinimalAPI.Services.Password;

namespace EmployeeHub_MinimalAPI.Services.Interfaces
{
    public interface ILeaveRequest<T>
	{
		Task<IEnumerable<T>> GetAllAsync();
		Task<IEnumerable<T>> GetAllEmployeeAsync(int id);
		Task<T> GetAsync(int id);
		Task<T> CreateAsync(LeaveRequestCreateDTO dto);
		Task<T> UpdateAsync(LeaveRequestUpdateDTO dto);
		Task<T> DeleteAsync(int id);
	}
}
