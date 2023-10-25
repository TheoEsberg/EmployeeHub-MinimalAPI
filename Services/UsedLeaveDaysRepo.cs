using EmployeeHub_MinimalAPI.Data;
using EmployeeHub_MinimalAPI.Models;
using EmployeeHub_MinimalAPI.Models.DTOs;

namespace EmployeeHub_MinimalAPI.Services
{
	public class UsedLeaveDaysRepo : IUsedLeaveDays<UsedLeaveDays>
	{
		private readonly AppDbContext _appDbContext;

        public UsedLeaveDaysRepo(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

		public Task<UsedLeaveDays> CreateAsync(UsedLeaveDaysCreateDTO dto)
		{
			throw new NotImplementedException();
		}

		public Task<UsedLeaveDays> DeleteAsync(int id)
		{
			throw new NotImplementedException();
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

		public Task<UsedLeaveDays> UpdateAsync(UsedLeaveDaysCreateDTO dto)
		{
			throw new NotImplementedException();
		}
	}
}
