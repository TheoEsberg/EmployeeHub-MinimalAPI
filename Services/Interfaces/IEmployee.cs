using EmployeeHub_MinimalAPI.Models.DTOs.Employee;
using EmployeeHub_MinimalAPI.Services.Password;

namespace EmployeeHub_MinimalAPI.Services.Interfaces
{
    public interface IEmployee<T>
	{
		Task<IEnumerable<T>> GetAllAsync();
		Task<T> GetAsync(int id);
		Task<T> CreateAsync(EmployeeCreateDTO dto, PasswordHashingService passwordHashingService);
		Task<T> UpdateAsync(EmployeeUpdateDTO dto, PasswordHashingService passwordHashingService);
		Task<T> DeleteAsync(int id);
	}
}
