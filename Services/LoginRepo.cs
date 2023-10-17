using EmployeeHub_MinimalAPI.Data;
using EmployeeHub_MinimalAPI.Models;
using EmployeeHub_MinimalAPI.Models.DTOs;
using EmployeeHub_MinimalAPI.Services.Password;
using Microsoft.EntityFrameworkCore;

namespace EmployeeHub_MinimalAPI.Services
{
	public class LoginRepo : ILogin<Employee>
	{
		private readonly AppDbContext _appDbContext;

        public LoginRepo(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

		public async Task<Employee> Login(LoginDTO loginData, PasswordHashingService passwordHashingService)
		{
			var employee = await _appDbContext.Employees.FirstOrDefaultAsync(e => e.Email == loginData.Email);

			if (employee == null)
			{
				// Handle the case where the emplyee with the provided email DOES NOT FUCKING EXIST!
				return null;
			}

			// Verify the emplyee's input password against the stored hash and salt
			if (passwordHashingService.VerifyPassword(loginData.Password, employee.Password))
			{
				return employee;
			}

			// When password does the fail.
			return null;
		}
	}
}
