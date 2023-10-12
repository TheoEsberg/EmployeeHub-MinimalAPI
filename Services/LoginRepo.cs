using EmployeeHub_MinimalAPI.Data;
using EmployeeHub_MinimalAPI.Models;
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

		public async Task<Employee> Login(string email, string password)
		{
			var result = await _appDbContext.Employees.FirstOrDefaultAsync(e => e.Email == email && e.Password == password);
			return result;
		}
	}
}
