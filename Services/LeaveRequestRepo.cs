using EmployeeHub_MinimalAPI.Data;
using EmployeeHub_MinimalAPI.Models;

namespace EmployeeHub_MinimalAPI.Services
{
	public class LeaveRequestRepo : IRepository<LeaveRequest>
	{
		private readonly AppDbContext _appDbContext;
		public LeaveRequestRepo(AppDbContext appDbContext)
		{
			_appDbContext = appDbContext;
		}
		public Task<LeaveRequest> CreateAsync(LeaveRequest entity)
		{
			throw new NotImplementedException();
		}

		public Task<LeaveRequest> DeleteAsync(int id)
		{
			throw new NotImplementedException();
		}

		public Task<IEnumerable<LeaveRequest>> GetAllAsync()
		{
			throw new NotImplementedException();
		}

		public Task<LeaveRequest> GetAsync(int id)
		{
			throw new NotImplementedException();
		}

		public Task<LeaveRequest> UpdateAsync(LeaveRequest entity)
		{
			throw new NotImplementedException();
		}
	}
}
