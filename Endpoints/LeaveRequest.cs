using EmployeeHub_MinimalAPI.Models.DTOs.LeaveRequest;
using EmployeeHub_MinimalAPI.Models.DTOs.UsedLeaveDays;
using EmployeeHub_MinimalAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeHub_MinimalAPI.Endpoints
{
	public static class LeaveRequest
	{
		public static void ConfigureLeaveRequest(this WebApplication app)
		{
			// Leave Request Endpoints
			app.MapGet("api/leaveRequest", GetLeaveRequest)
				.WithName("GetAllLeaveRequests")
				.WithTags("Leave Request")
				.Produces(200);

			app.MapGet("api/leaveRequest/employee/{id:int}", GetLeaveRequestByEmployeeId)
				.WithName("GetAllLeaveRequestsByEmployee")
				.WithTags("Leave Request")
				.Produces(200);

			app.MapGet("api/leaveRequest/{id:int}", GetLeaveRequestById)
				.WithName("GetLeaveRequestById")
				.WithTags("Leave Request")
				.Produces(200)
				.Produces(404);

			app.MapPost("api/leaveRequest", CreateLeaveRequest)
				.WithName("CreateNewLeaveRequest")
				.WithTags("Leave Request")
				.Produces(200)
				.Produces(404);

			app.MapPut("api/leaveRequest", UpdateLeaveRequest)
				.WithName("UpdateLeaveRequest")
				.WithTags("Leave Request")
				.Produces(200)
				.Produces(404);

			app.MapDelete("api/leaveRequest/{id:int}", DeleteLeaveRequest)
				.WithName("DeleteLeaveRequest")
				.WithTags("Leave Request")
				.Produces(200)
				.Produces(404);
		}

		private async static Task<IResult> GetLeaveRequest([FromServices] ILeaveRequest<Models.LeaveRequest> repository)
		{
			var result = await repository.GetAllAsync();
			if (result == null) { return Results.BadRequest(); }
			return Results.Ok(result);
		}

		private async static Task<IResult> GetLeaveRequestByEmployeeId([FromServices] ILeaveRequest<Models.LeaveRequest> repository, int id)
		{
			var result = await repository.GetAllEmployeeAsync(id);
			if (result == null) { return Results.BadRequest(); }
			return Results.Ok(result);
		}

		private async static Task<IResult> GetLeaveRequestById([FromServices] ILeaveRequest<Models.LeaveRequest> repository, int id)
		{
			var result = await repository.GetAsync(id);
			if (result == null) { return Results.BadRequest(); }
			return Results.Ok(result);
		}

		private async static Task<IResult> CreateLeaveRequest([FromServices] ILeaveRequest<Models.LeaveRequest> repository,[FromServices]IUsedLeaveDays<Models.UsedLeaveDays> usedLeaveDays,[FromServices]ILeaveType<Models.LeaveType> leaveType, LeaveRequestCreateDTO dto)
		{

			var daysUsed = await usedLeaveDays.GetByEmployeeLeaveId(dto.EmployeeId, dto.LeaveTypeId);

			var maxDays = await leaveType.GetAsync(dto.LeaveTypeId);

			var daysLeft = maxDays.MaxDays - daysUsed.Days;

			var daysUsing = (dto.EndDate - dto.StartDate).Days;

			if ( daysUsing > daysLeft) { return  Results.BadRequest(); }

			var result = await repository.CreateAsync(dto);
			if (result == null) { return Results.BadRequest(); }

			return Results.Ok(result);
		}

		private async static Task<IResult> UpdateLeaveRequest([FromServices] ILeaveRequest<Models.LeaveRequest> repository,[FromServices] IUsedLeaveDays<Models.UsedLeaveDays> usedLeaveDays, LeaveRequestUpdateDTO dto)
		{
			var test = await repository.GetAsync(dto.Id);
			int testingPending = test.Pending;
			int testingDays = ((test.EndDate - test.StartDate).Days) + 1;
			var result = await repository.UpdateAsync(dto);
			int totalDays = ((result.EndDate - result.StartDate).Days) + 1;

			if (result == null) { return Results.BadRequest(); }

			if(result.Pending==1 && result.Pending != testingPending)
			{
				var updatedUsedLeaveDays = new UsedLeaveDaysUpdateDTO
				{
					EmployeeId = result.EmployeeId,
					LeaveTypeId = result.LeaveTypeId,
					Days = totalDays
				};
				await usedLeaveDays.UpdateDaysAsync(updatedUsedLeaveDays);
			}
			else if(result.Pending==1 && result.Pending == testingPending && testingDays!=totalDays)
			{
				var updatedUsedLeaveDays = new UsedLeaveDaysUpdateDTO
				{
					EmployeeId = result.EmployeeId,
					LeaveTypeId = result.LeaveTypeId,
					Days = totalDays - testingDays
				};
				await usedLeaveDays.UpdateDaysAsync (updatedUsedLeaveDays);
			}
			else if(result.Pending!=1 && testingPending == 1)
			{
				var updatedUsedLeaveDays = new UsedLeaveDaysUpdateDTO
				{
					EmployeeId = result.EmployeeId,
					LeaveTypeId = result.LeaveTypeId,
					Days = -testingDays
				};
				await usedLeaveDays.UpdateDaysAsync(updatedUsedLeaveDays);
			}
			return Results.Ok(result);
		}

		private async static Task<IResult> DeleteLeaveRequest([FromServices] ILeaveRequest<Models.LeaveRequest> repository,[FromServices] IUsedLeaveDays<Models.UsedLeaveDays> usedLeaveDays, int id)
		{
			var result = await repository.DeleteAsync(id);
			if (result == null) { return Results.BadRequest(id); }

			if (result.Pending == 1)
			{
				var totalDays = ((result.EndDate - result.StartDate).Days) + 1;

				var updatedUsedLeaveDays = new UsedLeaveDaysUpdateDTO
				{
					EmployeeId = result.EmployeeId,
					LeaveTypeId = result.LeaveTypeId,
					Days = -totalDays
				};
				await usedLeaveDays.UpdateDaysAsync(updatedUsedLeaveDays);
			}
			return Results.Ok(result);
		}
	}
}
