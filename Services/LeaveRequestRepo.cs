using EmployeeHub_MinimalAPI.Data;
using EmployeeHub_MinimalAPI.Models;
using EmployeeHub_MinimalAPI.Models.DTOs;
using EmployeeHub_MinimalAPI.Services.Password;
using Microsoft.EntityFrameworkCore;

namespace EmployeeHub_MinimalAPI.Services
{
	public class LeaveRequestRepo : ILeaveRequest<LeaveRequest>
	{
		private readonly AppDbContext _appDbContext;
		public LeaveRequestRepo(AppDbContext appDbContext)
		{
			_appDbContext = appDbContext;
		}
		public async Task<LeaveRequest> CreateAsync(LeaveRequestCreateDTO dto, int id)
		{
			var newLeaveRequest = new LeaveRequest
			{
				//Creates a new LeaveRequest for employee with used id
				EmployeeId = id,
				LeaveTypeId = dto.LeaveTypeId,
				Pending = 0,
				StartDate = dto.StartDate,
				EndDate = dto.EndDate,
				RequestDate = DateTime.Now
			};
			await _appDbContext.LeaveRequests.AddAsync(newLeaveRequest);
			await _appDbContext.SaveChangesAsync();
			return newLeaveRequest;
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

		public async Task<IEnumerable<LeaveRequest>> GetAllEmployeeAsync(int id)
		{
			return await _appDbContext.LeaveRequests.Where(x=>x.EmployeeId==id).ToListAsync();
		}

		public async Task<LeaveRequest> GetAsync(int id)
		{
			return await _appDbContext.LeaveRequests.FirstOrDefaultAsync(x => x.Id == id);
		}

		public async Task<LeaveRequest> UpdateAsync(LeaveRequestUpdateDTO dto)
		{
			var oldLeaveRequest = await _appDbContext.LeaveRequests.FindAsync(dto.Id);
			if (oldLeaveRequest != null)
			{
				oldLeaveRequest.Pending = dto.Pending;
				if (dto.ResponseMessage != "string") { oldLeaveRequest.ResponseMessage = dto.ResponseMessage; }
				await _appDbContext.SaveChangesAsync();
			}
			return oldLeaveRequest;
		}
	}
}
