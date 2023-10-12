using EmployeeHub_MinimalAPI.Data;
using EmployeeHub_MinimalAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeeHub_MinimalAPI.Services
{
	public class LeaveRequestRepo : IRepository<LeaveRequest>
	{
		private readonly AppDbContext _appDbContext;
		public LeaveRequestRepo(AppDbContext appDbContext)
		{
			_appDbContext = appDbContext;
		}
		public async Task<LeaveRequest> CreateAsync(LeaveRequest entity)
		{
			await _appDbContext.LeaveRequests.AddAsync(entity);
			await _appDbContext.SaveChangesAsync();
			return entity;
		}

		public async Task<LeaveRequest> DeleteAsync(int id)
		{
			var LeaveRequestToDelete=await _appDbContext.LeaveRequests.FirstOrDefaultAsync(x => x.Id == id);
			if(LeaveRequestToDelete != null)
			{
				_appDbContext.LeaveRequests.Remove(LeaveRequestToDelete);
				await _appDbContext.SaveChangesAsync();
			}
			return LeaveRequestToDelete;
		}

		public async Task<IEnumerable<LeaveRequest>> GetAllAsync()
		{
			return await _appDbContext.LeaveRequests.ToListAsync();
		}

		public async Task<LeaveRequest> GetAsync(int id)
		{
			return await _appDbContext.LeaveRequests.FirstOrDefaultAsync(x => x.Id == id);
		}

		public async Task<LeaveRequest> UpdateAsync(LeaveRequest entity)
		{
			var newLeaveRequest = await _appDbContext.LeaveRequests.FindAsync(entity.Id);
			if(newLeaveRequest != null)
			{
				newLeaveRequest.EmployeeId = entity.EmployeeId;
				newLeaveRequest.LeaveTypeId = entity.LeaveTypeId;
				newLeaveRequest.Pending=entity.Pending;
				newLeaveRequest.ResponseMessage = entity.ResponseMessage;
				newLeaveRequest.StartDate = entity.StartDate;
				newLeaveRequest.EndDate = entity.EndDate;
				newLeaveRequest.RequestDate = entity.RequestDate;
				await _appDbContext.SaveChangesAsync();
			}
			return newLeaveRequest;
		}
	}
}
