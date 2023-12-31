﻿using EmployeeHub_MinimalAPI.Models;
using EmployeeHub_MinimalAPI.Models.DTOs.UsedLeaveDays;
using EmployeeHub_MinimalAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeHub_MinimalAPI.Endpoints
{
	public static class UsedLeaveDays
	{
		public static void ConfigureUsedLeaveDays(this WebApplication app)
		{
			// Used Leave Days Endpoints
			app.MapGet("api/usedLeaveDays", GetAllUsedLeaveDays)
				.WithName("GetAllUsedLeaveDays")
				.WithTags("Used Leave Days")
				.Produces(200);

			app.MapGet("api/usedLeaveDays/{id:int}", GetUsedLeaveDaysById)
				.WithName("GetUsedLeaveDaysById")
				.WithTags("Used Leave Days")
				.Produces(200)
				.Produces(404);

			app.MapGet("api/usedLeaveDays/employee/{employeeId:int}", GetUsedLeaveDaysByEmployeeId)
				.WithName("GetUsedLeaveDaysByEmployeeId")
				.WithTags("Used Leave Days")
				.Produces(200)
				.Produces(404);

			app.MapGet("api/usedLeaveDays/leaveType/{leaveTypeId:int}", GetUsedLeaveDaysByLeaveTypeId)
				.WithName("GetUsedLeaveDaysByLeaveTypeId")
				.WithTags("Used Leave Days")
				.Produces(200)
				.Produces(404);

			app.MapGet("api/usedLeaveDays/{employeeId:int}/{leaveTypeId:int}", GetUsedLeaveDaysByEmployeeLeaveId)
				.WithName("GetUsedLeaveDaysByEmployeeLeaveTypeId")
				.WithTags("Used Leave Days")
				.Produces(200)
				.Produces(404);


			app.MapPost("api/usedLeaveDays", CreateUsedLeaveDays)
				.WithName("CreateUsedLeaveDays")
				.WithTags("Used Leave Days")
				.Produces(200)
				.Produces(404);

			app.MapPut("api/usedLeaveDays", UpdateUsedLeaveDays)
				.WithName("UpdateUsedLeaveDays")
				.WithTags("Used Leave Days")
				.Produces(200)
				.Produces(404);

			app.MapDelete("api/usedLeaveDays/{id:int}", DeleteUsedLeaveDays)
				.WithName("DeleteUsedLeaveDays")
				.WithTags("Used Leave Days")
				.Produces(200)
				.Produces(404);
		}

		private async static Task<IResult> CreateUsedLeaveDays([FromServices] IUsedLeaveDays<Models.UsedLeaveDays> repository, UsedLeaveDaysCreateDTO dto)
		{
			var result = await repository.CreateAsync(dto);
			if (result == null) { return Results.BadRequest(); }
			return Results.Ok(result);
		}

		private async static Task<IResult> UpdateUsedLeaveDays([FromServices] IUsedLeaveDays<Models.UsedLeaveDays> repository, UsedLeaveDaysUpdateDTO dto)
		{
			var result = await repository.UpdateDaysAsync(dto);
			if (result == null) { return Results.BadRequest(); }
			return Results.Ok(result);
		}

		private async static Task<IResult> GetAllUsedLeaveDays([FromServices] IUsedLeaveDays<Models.UsedLeaveDays> repository, [FromServices] ILeaveType<Models.LeaveType> leaveType)
		{
			var result = await repository.GetAllAsync();
			if (result == null) { return Results.BadRequest(); }
			var types = await leaveType.GetAllAsync();
			foreach (var item in result)
			{
				var type = types.FirstOrDefault(x => x.Id == item.LeaveTypeId);
				item.Days = (type.MaxDays - item.Days);
			}
			return Results.Ok(result);
		}

		private async static Task<IResult> GetUsedLeaveDaysById([FromServices] IUsedLeaveDays<Models.UsedLeaveDays> repository, int id)
		{
			var result = await repository.GetAsync(id);
			if (result == null) { return Results.BadRequest(); }
			return Results.Ok(result);
		}
		private async static Task<IResult> GetUsedLeaveDaysByEmployeeId([FromServices] IUsedLeaveDays<Models.UsedLeaveDays> repository, [FromServices] ILeaveType<Models.LeaveType> leaveType, int employeeId)
		{
			var result = await repository.GetByEmployeeId(employeeId);
			if (result == null) { return Results.BadRequest(); }
			var types = await leaveType.GetAllAsync();
			foreach (var item in result)
			{
				var type = types.FirstOrDefault(x => x.Id == item.LeaveTypeId);
				item.Days = (type.MaxDays - item.Days);
			}
			return Results.Ok(result);
		}
		private async static Task<IResult> GetUsedLeaveDaysByLeaveTypeId([FromServices] IUsedLeaveDays<Models.UsedLeaveDays> repository, int leaveTypeId)
		{
			var result = await repository.GetByLeaveTypeId(leaveTypeId);
			if (result == null) { return Results.BadRequest(); }
			return Results.Ok(result);
		}

		private async static Task<IResult> DeleteUsedLeaveDays([FromServices] IUsedLeaveDays<Models.UsedLeaveDays> repository, int id)
		{
			var result = await repository.DeleteAsync(id);
			if (result == null) { return Results.BadRequest(); }
			return Results.Ok(result);
		}

		private async static Task<IResult> GetUsedLeaveDaysByEmployeeLeaveId([FromServices] IUsedLeaveDays<Models.UsedLeaveDays> repository, int employeeId, int leaveTypeId)
		{
			var result = await repository.GetByEmployeeLeaveId(employeeId, leaveTypeId);
			if (result == null) { return Results.BadRequest(); };
			return Results.Ok(result);
		}
	}
}
