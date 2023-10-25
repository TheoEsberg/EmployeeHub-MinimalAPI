using EmployeeHub_MinimalAPI.Data;
using EmployeeHub_MinimalAPI.Models;
using EmployeeHub_MinimalAPI.Models.DTOs;
using Microsoft.EntityFrameworkCore;

namespace EmployeeHub_MinimalAPI.Services
{
	public class UsedLeaveDaysRepo : IUsedLeaveDays<UsedLeaveDays>
	{
		private readonly AppDbContext _appDbContext;

        public UsedLeaveDaysRepo(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

		public async Task<UsedLeaveDays> CreateAsync(UsedLeaveDaysCreateDTO dto)
		{
			var oldUsedLeaveDays = await _appDbContext.UsedLeaveDays.FindAsync(dto.EmployeeId, dto.LeaveTypeId);
			if (oldUsedLeaveDays == null)
			{
				var newUsedLeaveDays = new UsedLeaveDays
				{
					EmployeeId = dto.EmployeeId,
					LeaveTypeId = dto.LeaveTypeId,
					Days = 0
				};
				await _appDbContext.UsedLeaveDays.AddAsync(newUsedLeaveDays);
				await _appDbContext.SaveChangesAsync();
				return newUsedLeaveDays;
			}
			return null;
		}

		public async Task<UsedLeaveDays> DeleteAsync(int id)
		{
			var UsedLeaveDaysDelete = await _appDbContext.UsedLeaveDays.FirstOrDefaultAsync(x => x.Id == id);
			if (UsedLeaveDaysDelete != null)
			{
				_appDbContext.UsedLeaveDays.Remove(UsedLeaveDaysDelete);
				await _appDbContext.SaveChangesAsync();
			}
			return UsedLeaveDaysDelete;
		}

		public async Task<IEnumerable<UsedLeaveDays>> GetAllAsync()
		{
			return await _appDbContext.UsedLeaveDays.ToListAsync();
		}

		public async Task<UsedLeaveDays> GetAsync(int id)
		{
			return await _appDbContext.UsedLeaveDays.FirstOrDefaultAsync(e => e.Id == id);
		}

		public async Task<IEnumerable<UsedLeaveDays>> GetByEmployeeId(int employeeId)
		{
			return await _appDbContext.UsedLeaveDays.Where(e => e.EmployeeId == employeeId).ToListAsync();
		}

		public async Task<IEnumerable<UsedLeaveDays>> GetByLeaveTypeId(int leaveTypeId)
		{
			return await _appDbContext.UsedLeaveDays.Where(e => e.LeaveTypeId == leaveTypeId).ToListAsync();
		}

		public async Task<UsedLeaveDays> UpdateDaysAsync(UsedLeaveDaysUpdateDTO dto)
		{
			var oldUsedLeaveDays= await _appDbContext.UsedLeaveDays.FindAsync(dto.Id);
			if (oldUsedLeaveDays != null)
			{
				oldUsedLeaveDays.Days = oldUsedLeaveDays.Days + dto.Days;
				await _appDbContext.SaveChangesAsync();
			}
			return oldUsedLeaveDays;
		}
	}
}
