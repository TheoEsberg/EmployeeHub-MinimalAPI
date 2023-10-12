using EmployeeHub_MinimalAPI.Data;
using EmployeeHub_MinimalAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeeHub_MinimalAPI.Services
{
	public class LeaveTypeRepo : IRepository<LeaveType>
	{
		private readonly AppDbContext _appDbContext;
		public LeaveTypeRepo(AppDbContext appDbContext)
		{
			_appDbContext = appDbContext;
		}

		public async Task<LeaveType> CreateAsync(LeaveType entity)
		{
			await _appDbContext.LeaveTypes.AddAsync(entity);
			await _appDbContext.SaveChangesAsync();
			return entity;
		}

		public async Task<LeaveType> DeleteAsync(int id)
		{
			var LeaveTypeToDelete=await _appDbContext.LeaveTypes.FirstOrDefaultAsync(x => x.Id == id);
			if(LeaveTypeToDelete != null)
			{
				_appDbContext.LeaveTypes.Remove(LeaveTypeToDelete);
				await _appDbContext.SaveChangesAsync();
			}
			return LeaveTypeToDelete;
		}

		public async Task<IEnumerable<LeaveType>> GetAllAsync()
		{
			return await _appDbContext.LeaveTypes.ToListAsync();
		}

		public async Task<LeaveType> GetAsync(int id)
		{
			return await _appDbContext.LeaveTypes.FirstOrDefaultAsync(x => x.Id == id);
		}

		public async Task<LeaveType> UpdateAsync(LeaveType entity)
		{
			var newLeaveType=await _appDbContext.LeaveTypes.FindAsync(entity.Id);
			if(newLeaveType != null)
			{
				newLeaveType.Name = entity.Name;
				newLeaveType.MaxDays = entity.MaxDays;
				await _appDbContext.SaveChangesAsync();
			}
			return newLeaveType;
		}
	}
}
