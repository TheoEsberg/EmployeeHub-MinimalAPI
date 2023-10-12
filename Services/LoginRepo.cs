using EmployeeHub_MinimalAPI.Data;
using EmployeeHub_MinimalAPI.Models;
using EmployeeHub_MinimalAPI.Services.Password;

namespace EmployeeHub_MinimalAPI.Services
{
	public class LoginRepo : ILogin<Employee>
	{
		private readonly AppDbContext _appDbContext;

        public LoginRepo(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

		public async Task<Employee> Login(string email, string password, PasswordHashingService passwordHashingService)
		{
			var employee = await _appDbContext.Employees.FirstOrDefaultAsync(e => e.Email == email);

			if (employee == null)
			{
				// Handle the case where the emplyee with the provided email DOES NOT FUCKING EXIST!
				return null;
			}

			// Verify the emplyee's input password against the stored hash and salt
			if (passwordHashingService.VerifyPassword(password, employee.Password))
			{
				return employee;
			}

			// When password does the fail.
			return null;
		}
	}
}
