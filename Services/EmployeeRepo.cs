using EmployeeHub_MinimalAPI.Data;
using EmployeeHub_MinimalAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeeHub_MinimalAPI.Services
{
	public class EmployeeRepo : IRepository<Employee>
	{
		private readonly AppDbContext _appDbContext;
		public EmployeeRepo(AppDbContext appDbContext)
		{
			_appDbContext = appDbContext;
		}
		public async Task<Employee> CreateAsync(Employee entity)
		{
			await _appDbContext.AddAsync(entity);
			await _appDbContext.SaveChangesAsync();
			return entity;
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

		public async Task<Employee> UpdateAsync(Employee entity)
		{
			var newEmployee = await _appDbContext.Employees.FindAsync(entity.Id);
			if(newEmployee != null)
			{
				newEmployee.Name = entity.Name;
				newEmployee.Email = entity.Email;
				newEmployee.Password = entity.Password;
				newEmployee.VacationDays = entity.VacationDays;
				newEmployee.isAdmin = entity.isAdmin;
				await _appDbContext.SaveChangesAsync();
			}
			return newEmployee;
		}
	}
}
