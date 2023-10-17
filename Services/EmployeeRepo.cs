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

		public async Task<Employee> UpdateAsync(EmployeeUpdateDTO dto, PasswordHashingService passwordHashingService)
		{
			var oldUser = await _appDbContext.Employees.FindAsync(dto.Id);

			if(oldUser != null)
			{
				if (dto.Name != "string") { oldUser.Name = dto.Name; }
				if (dto.Email != "string") { oldUser.Email = dto.Email; }
				if (dto.Password != "string") { oldUser.Password = passwordHashingService.HashPassword(dto.Password, oldUser.Salt); }
				oldUser.VacationDays = dto.VacationDays;
				oldUser.isAdmin = dto.isAdmin;
				await _appDbContext.SaveChangesAsync();
			}
			return oldUser;
		}
	}
}
