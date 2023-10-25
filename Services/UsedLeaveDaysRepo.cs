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

		public Task<IEnumerable<UsedLeaveDays>> GetAllAsync()
		{
			throw new NotImplementedException();
		}

		public Task<UsedLeaveDays> GetAsync(int id)
		{
			throw new NotImplementedException();
		}

		public Task<UsedLeaveDays> GetByEmployeeId(int employeeId)
		{
			throw new NotImplementedException();
		}

		public Task<UsedLeaveDays> GetByLeaveTypeId(int leaveTypeId)
		{
			throw new NotImplementedException();
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
