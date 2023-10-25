using EmployeeHub_MinimalAPI.Models.DTOs.LeaveType;
using EmployeeHub_MinimalAPI.Models.DTOs.UsedLeaveDays;
using EmployeeHub_MinimalAPI.Models;
using EmployeeHub_MinimalAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeHub_MinimalAPI.Endpoints
{
	public static class LeaveType
	{
		public static void ConfigureLeaveType(this WebApplication app)
		{
			app.MapGet("api/leaveType", GetLeaveType).WithName("GetAllLeaveType").WithTags("Get").Produces(200);
			app.MapGet("api/leaveType/{id:int}", GetLeaveTypeById).WithName("GetLeaveTypeById").WithTags("Get").Produces(200).Produces(404);
			app.MapPost("api/leaveType", CreateLeaveType).WithName("CreateNewLeaveType").WithTags("Post").Produces(200).Produces(404);
			app.MapPut("api/leaveType", UpdateLeaveType).WithName("UpdateLeaveType").WithTags("Put").Produces(200).Produces(404);
			app.MapDelete("api/leaveType/{id:int}", DeleteLeaveType).WithName("DeleteLeaveType").WithTags("Delete").Produces(200).Produces(404);
		}

		private async static Task<IResult> GetLeaveType([FromServices] ILeaveType<Models.LeaveType> repository)
		{
			var result = await repository.GetAllAsync();
			if (result == null) { return Results.BadRequest(); }
			return Results.Ok(result);
		}

		private async static Task<IResult> GetLeaveTypeById([FromServices] ILeaveType<Models.LeaveType> repository, int id)
		{
			var result = await repository.GetAsync(id);
			if (result == null) { return Results.BadRequest(); }
			return Results.Ok(result);
		}

		private async static Task<IResult> CreateLeaveType([FromServices] ILeaveType<Models.LeaveType> repository, [FromServices] IEmployee<Models.Employee> employee, IUsedLeaveDays<Models.UsedLeaveDays> usedLeaveDays, LeaveTypeCreateDTO dto)
		{
			var result = await repository.CreateAsync(dto);
			if (result == null) { return Results.BadRequest(); }
			var allEmployee = await employee.GetAllAsync();
			foreach (var emp in allEmployee)
			{
				var newUsedLeaveDays = new UsedLeaveDaysCreateDTO
				{
					LeaveTypeId = result.Id,
					EmployeeId = emp.Id,
				};
				await usedLeaveDays.CreateAsync(newUsedLeaveDays);
			}
			return Results.Ok(result);
		}

		private async static Task<IResult> UpdateLeaveType([FromServices] ILeaveType<Models.LeaveType> repository, Models.LeaveType entity)
		{
			var result = await repository.UpdateAsync(entity);
			if (result == null) { return Results.BadRequest(); }
			return Results.Ok(result);
		}

		private async static Task<IResult> DeleteLeaveType([FromServices] ILeaveType<Models.LeaveType> repository, int id)
		{
			var result = await repository.DeleteAsync(id);
			if (result == null) { return Results.BadRequest(id); }
			return Results.Ok(result);
		}
	}
}
