using EmployeeHub_MinimalAPI.Models.DTOs;
using EmployeeHub_MinimalAPI.Services.Password;

namespace EmployeeHub_MinimalAPI.Services
{
	public interface IEmployee<T>
	{
		Task<IEnumerable<T>> GetAllAsync();
		Task<T> GetAsync(int id);
		Task<T> CreateAsync(EmployeeCreateDTO dto, PasswordHashingService passwordHashingService);
		Task<T> UpdateAsync(T entity, PasswordHashingService passwordHashingService);
		Task<T> DeleteAsync(int id);
	}
}
