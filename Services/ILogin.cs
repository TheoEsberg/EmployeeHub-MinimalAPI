using EmployeeHub_MinimalAPI.Models.DTOs;
using EmployeeHub_MinimalAPI.Services.Password;

namespace EmployeeHub_MinimalAPI.Services
{
	public interface ILogin<T>
	{
		Task<T> Login(LoginDTO loginData, PasswordHashingService passwordHashingService);
	}
}
