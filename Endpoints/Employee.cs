using EmployeeHub_MinimalAPI.Services.Interfaces;
using EmployeeHub_MinimalAPI.Models.DTOs.Employee;
using EmployeeHub_MinimalAPI.Models.DTOs.UsedLeaveDays;
using EmployeeHub_MinimalAPI.Models;
using EmployeeHub_MinimalAPI.Services.Password;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeHub_MinimalAPI.Endpoints
{
	public static class Employee
	{
		public static void ConfigureEmployee(this WebApplication app)
		{
			app.MapGet("api/employee", GetEmployee).WithName("GetAllEmployees").WithTags("Get").Produces(200);
			app.MapGet("api/employee/{id:int}", GetEmployeeById).WithName("GetEmployeeById").WithTags("Get").Produces(200).Produces(404);
			app.MapPost("api/employee", CreateEmployee).WithName("CreateNewEmployee").WithTags("Post").Produces(200).Produces(404);
			app.MapPut("api/employee", UpdateEmployee).WithName("UpdateEmployee").WithTags("Put").Produces(200).Produces(404);
			app.MapDelete("api/employee/{id:int}", DeleteEmployee).WithName("DeleteEmployee").WithTags("Delete").Produces(200).Produces(404);
		}

		private async static Task<IResult> GetEmployee([FromServices] IEmployee<Models.Employee> repository)
		{
			var result = await repository.GetAllAsync();
			return Results.Ok(result);
		}

		private async static Task<IResult> GetEmployeeById([FromServices] IEmployee<Models.Employee> repository, int id)
		{
			var result = await repository.GetAsync(id);
			if (result == null) { return Results.BadRequest(); }
			return Results.Ok(result);
		}

		private async static Task<IResult> CreateEmployee([FromServices] IEmployee<Models.Employee> repository, [FromServices] ILeaveType<Models.LeaveType> LeaveType, [FromServices] IUsedLeaveDays<Models.UsedLeaveDays> usedLeaveDays, EmployeeCreateDTO dto, PasswordHashingService passwordHashingService)
		{
			var result = await repository.CreateAsync(dto, passwordHashingService);
			if (result == null) { return Results.BadRequest(); }
			var leaveType = await LeaveType.GetAllAsync();
			foreach (var leave in leaveType)
			{
				var newUsedLeaveDays = new UsedLeaveDaysCreateDTO
				{
					EmployeeId = result.Id,
					LeaveTypeId = leave.Id,
				};
				await usedLeaveDays.CreateAsync(newUsedLeaveDays);
			}
			return Results.Ok(result);
		}

		private async static Task<IResult> UpdateEmployee([FromServices] IEmployee<Models.Employee> repository, EmployeeUpdateDTO dto, PasswordHashingService passwordHashingService)
		{
			var result = await repository.UpdateAsync(dto, passwordHashingService);
			if (result == null) { return Results.BadRequest(); }
			return Results.Ok(result);
		}

		private async static Task<IResult> DeleteEmployee([FromServices] IEmployee<Models.Employee> repository, int id)
		{
			var result = await repository.DeleteAsync(id);
			if (result == null) { return Results.BadRequest(id); }
			return Results.Ok(result);
		}
	}
}
