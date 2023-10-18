using EmployeeHub_MinimalAPI.Data;
using EmployeeHub_MinimalAPI.Models;
using EmployeeHub_MinimalAPI.Models.DTOs;
using Microsoft.EntityFrameworkCore;

namespace EmployeeHub_MinimalAPI.Services
{
	public class LeaveTypeRepo : ILeaveType<LeaveType>
	{
		private readonly AppDbContext _appDbContext;
		public LeaveTypeRepo(AppDbContext appDbContext)
		{
			_appDbContext = appDbContext;
		}

		public async Task<LeaveType> CreateAsync(LeaveTypeCreateDTO dto)
		{
			var newLeaveType = new LeaveType
			{
				Name=dto.Name,
				MaxDays=dto.MaxDays,
			};
			await _appDbContext.LeaveTypes.AddAsync(newLeaveType);
			await _appDbContext.SaveChangesAsync();
			return newLeaveType;
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
			var oldLeaveType = await _appDbContext.LeaveTypes.FindAsync(entity.Id);
			if (oldLeaveType != null)
			{
				if (entity.Name != "string") { oldLeaveType.Name = entity.Name; }
				oldLeaveType.MaxDays = entity.MaxDays;
				await _appDbContext.SaveChangesAsync();
			}
			return oldLeaveType;
		}
	}
}
