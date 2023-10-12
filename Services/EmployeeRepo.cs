using EmployeeHub_MinimalAPI.Data;
using EmployeeHub_MinimalAPI.Models;
using EmployeeHub_MinimalAPI.Models.DTOs;
using EmployeeHub_MinimalAPI.Services.Password;
using Microsoft.EntityFrameworkCore;

namespace EmployeeHub_MinimalAPI.Services
{
	public class EmployeeRepo : IEmployee<Employee>
	{
		private readonly AppDbContext _appDbContext;
		public EmployeeRepo(AppDbContext appDbContext)
		{
			_appDbContext = appDbContext;
		}
		public async Task<Employee> CreateAsync(EmployeeCreateDTO dto, PasswordHashingService passwordHashingService)
		{
			// Create a new Employee entity and map data from the DTO
			var newEmployee = new Employee
			{
				Name = dto.Name,
				Email = dto.Email
			};

			// Generate a unique Salt for the new employee
			string salt = passwordHashingService.GenerateSalt();

			// Hash the employee's password using the generated Salt
			string hashedPassword = passwordHashingService.HashPassword(dto.Password, salt);

			// Store the Salt and the HashedPassword in the employee
			newEmployee.Salt = salt;
			newEmployee.Password = hashedPassword;

			// Add the new employee to the database
			await _appDbContext.AddAsync(newEmployee);
			await _appDbContext.SaveChangesAsync();

			// Return the new employee
			return newEmployee;
		}

		public async Task<Employee> DeleteAsync(int id)
		{
			var employeeToDelete=await _appDbContext.Employees.FirstOrDefaultAsync(x => x.Id == id);
            if (employeeToDelete != null)
			{
				_appDbContext.Employees.Remove(employeeToDelete);
				await _appDbContext.SaveChangesAsync();
			}
			return employeeToDelete;
		}

		public async Task<IEnumerable<Employee>> GetAllAsync()
		{
			return await _appDbContext.Employees.ToListAsync();
		}

		public Task<Employee> GetAsync(int id)
		{
			return _appDbContext.Employees.FirstOrDefaultAsync(x=>x.Id == id);
		}

		public async Task<Employee> UpdateAsync(Employee entity, PasswordHashingService passwordHashingService)
		{
			var newEmployee = await _appDbContext.Employees.FindAsync(entity.Id);
			if(newEmployee != null)
			{
				newEmployee.Name = entity.Name;
				newEmployee.Email = entity.Email;
				newEmployee.Password = passwordHashingService.HashPassword(entity.Password, newEmployee.Salt);
				newEmployee.VacationDays = entity.VacationDays;
				newEmployee.isAdmin = entity.isAdmin;
				await _appDbContext.SaveChangesAsync();
			}
			return newEmployee;
		}
		public async Task<Employee> Loggin(string UserName, string Password)
		{
			var userLoggin = await _appDbContext.Employees.FirstOrDefaultAsync(x=>x.Name == UserName && x.Password==Password);
			return userLoggin;
		}
	}
}
