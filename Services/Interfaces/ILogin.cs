using EmployeeHub_MinimalAPI.Models.DTOs.Login;
using EmployeeHub_MinimalAPI.Services.Password;

namespace EmployeeHub_MinimalAPI.Services.Interfaces
{
    public interface ILogin<T>
	{
		Task<T> Login(LoginDTO loginData, PasswordHashingService passwordHashingService);
	}
}
