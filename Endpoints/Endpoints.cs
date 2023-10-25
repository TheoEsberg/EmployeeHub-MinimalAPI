using EmployeeHub_MinimalAPI.Models;
using EmployeeHub_MinimalAPI.Models.DTOs;
using EmployeeHub_MinimalAPI.Services;
using EmployeeHub_MinimalAPI.Services.Password;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.Extensions.Logging.Abstractions;

namespace EmployeeHub_MinimalAPI.Endpoints
{
	public static class Endpoints
	{
		public static void ConfigureEndpoints(this WebApplication app)
		{
			//Employee Endpoints
			app.MapGet("api/employee", GetEmployee).WithName("GetAllEmployees").WithTags("Get").Produces(200);
			app.MapGet("api/employee/{id:int}", GetEmployeeById).WithName("GetEmployeeById").WithTags("Get").Produces(200).Produces(404);
			app.MapPost("api/employee", CreateEmployee).WithName("CreateNewEmployee").WithTags("Create").Produces(200).Produces(404);
			app.MapPut("api/employee", UpdateEmployee).WithName("UpdateEmployee").WithTags("Update").Produces(200).Produces(404);
			app.MapDelete("api/employee/{id:int}", DeleteEmployee).WithName("DeleteEmployee").WithTags("Delete").Produces(200).Produces(404);

			//Login Endpoints
			app.MapPost("/api/Login", Login).WithName("Login").WithTags("Login").Produces(200).Produces(404);

			//Email Endpoints 
			app.MapPost("api/SendMail", SendEmail).WithName("SendEmail").WithTags("SendEmail").Produces(200).Produces(400);

			//LeaveRequest Endpoints
			app.MapGet("api/leaveRequest", GetLeaveRequest).WithName("GetAllLeaveRequests").WithTags("GetLeaveRequest").Produces(200);
			app.MapGet("api/leaveRequest/employee/{id:int}", GetLeaveRequestByEmployeeId).WithName("GetAllLeaveRequestsByEmployee").WithTags("GetLeaveRequest").Produces(200);
			app.MapGet("api/leaveRequest/{id:int}", GetLeaveRequestById).WithName("GetLeaveRequestById").WithTags("GetLeaveRequest").Produces(200).Produces(404);
			app.MapPost("api/leaveRequest", CreateLeaveRequest).WithName("CreateNewLeaveRequest").WithTags("CreateLeaveRequest").Produces(200).Produces(404);
			app.MapPut("api/leaveRequest", UpdateLeaveRequest).WithName("UpdateLeaveRequest").WithTags("UpdateLeaveRequest").Produces(200).Produces(404);
			app.MapDelete("api/leaveRequest/{id:int}", DeleteLeaveRequest).WithName("DeleteLeaveRequest").WithTags("DeleteLeaveRequest").Produces(200).Produces(404);

			//LeaveType Endpoints
			app.MapGet("api/leaveType", GetLeaveType).WithName("GetAllLeaveType").WithTags("GetLeaveType").Produces(200);
			app.MapGet("api/leaveType/{id:int}", GetLeaveTypeById).WithName("GetLeaveTypeById").WithTags("GetLeaveType").Produces(200).Produces(404);
			app.MapPost("api/leaveType", CreateLeaveType).WithName("CreateNewLeaveType").WithTags("CreateLeaveType").Produces(200).Produces(404);
			app.MapPut("api/leaveType", UpdateLeaveType).WithName("UpdateLeaveType").WithTags("UpdateLeaveType").Produces(200).Produces(404);
			app.MapDelete("api/leaveType/{id:int}", DeleteLeaveType).WithName("DeleteLeaveType").WithTags("DeleteLeaveType").Produces(200).Produces(404);

			// UsedLeaveDays Endpoints
			app.MapGet("api/usedLeaveDays", GetAllUsedLeaveDays).WithName("GetAllUsedLeaveDays").WithTags("GetUsedLeaveDays").Produces(200);
			app.MapGet("api/usedLeaveDays/{id:int}", GetUsedLeaveDaysById).WithName("GetUsedLeaveDaysById").WithTags("GetUsedLeaveDays").Produces(200).Produces(404);
			app.MapGet("api/usedLeaveDays/employee/{employeeId:int}", GetUsedLeaveDaysByEmployeeId).WithName("GetUsedLeaveDaysByEmployeeId").WithTags("GetUsedLeaveDays").Produces(200).Produces(404);
			app.MapGet("api/usedLeaveDays/leaveType/{leaveTypeId:int}", GetUsedLeaveDaysByLeaveTypeId).WithName("GetUsedLeaveDaysByLeaveTypeId").WithTags("GetUsedLeaveDays").Produces(200).Produces(404);
			app.MapPost("api/usedLeaveDays", CreateUsedLeaveDays).WithName("CreateUsedLeaveDays").WithTags("CreateUsedLeaveDays").Produces(200).Produces(404);
			app.MapPut("api/usedLeaveDays", UpdateUsedLeaveDays).WithName("UpdateUsedLeaveDays").WithTags("UpdateUsedLeaveDays").Produces(200).Produces(404);
			app.MapDelete("api/usedLeaveDays/{id:int}", DeleteUsedLeaveDays).WithName("DeleteUsedLeaveDays").WithTags("DeleteUsedLeaveDays").Produces(200).Produces(404);
		}

		//Employee Methods
		private async static Task<IResult> GetEmployee([FromServices] IEmployee<Employee> repository)
		{
			var result = await repository.GetAllAsync();
			return Results.Ok(result);
		}

		private async static Task<IResult> GetEmployeeById([FromServices] IEmployee<Employee> repository, int id)
		{
			var result = await repository.GetAsync(id);
			if (result == null) { return Results.BadRequest(); }
			return Results.Ok(result);
		}

		private async static Task<IResult> CreateEmployee([FromServices] IEmployee<Employee> repository, EmployeeCreateDTO dto, PasswordHashingService passwordHashingService)
		{
			var result = await repository.CreateAsync(dto, passwordHashingService);
			if (result == null) { return Results.BadRequest(); }
			return Results.Ok(result);
		}

		private async static Task<IResult> UpdateEmployee([FromServices] IEmployee<Employee> repository, EmployeeUpdateDTO dto, PasswordHashingService passwordHashingService)
		{
			var result = await repository.UpdateAsync(dto, passwordHashingService);
			if (result == null) { return Results.BadRequest(); }
			return Results.Ok(result);
		}

		private async static Task<IResult> DeleteEmployee([FromServices] IEmployee<Employee> repository, int id)
		{
			var result = await repository.DeleteAsync(id);
			if (result == null) { return Results.BadRequest(id); }
			return Results.Ok(result);
		}

		//Login Methods
		private async static Task<IResult> Login([FromServices] ILogin<Employee> repository, LoginDTO loginData, PasswordHashingService passwordHashingService)
		{
			var result = await repository.Login(loginData, passwordHashingService);

			if (result == null) { return Results.BadRequest(); }
			return Results.Ok(result);
		}

		//Email Methods
		private async static Task<IResult> SendEmail([FromServices] IEmail emailService, EmailRequestDTO emailRequest)
		{
			try
			{
				await emailService.SendEmailAsync(emailRequest.To, emailRequest.Subject, emailRequest.Body);
				return Results.Ok(new { message = "Email sent successfully" });
			}
			catch (Exception ex)
			{
				return Results.BadRequest(new {error = "Failed to send email", message = ex.Message });
			}
		}

		//LeaveRequest Methods
		private async static Task<IResult> GetLeaveRequest([FromServices] ILeaveRequest<LeaveRequest> repository)
		{
			var result = await repository.GetAllAsync();
			if (result == null) { return Results.BadRequest(); }
			return Results.Ok(result);
		}
		private async static Task<IResult> GetLeaveRequestByEmployeeId([FromServices] ILeaveRequest<LeaveRequest> repository,int id)
		{
			var result = await repository.GetAllEmployeeAsync(id);
			if (result == null) { return Results.BadRequest(); }
			return Results.Ok(result);
		}
		private async static Task<IResult> GetLeaveRequestById([FromServices] ILeaveRequest<LeaveRequest> repository, int id)
		{
			var result = await repository.GetAsync(id);
			if (result == null) { return Results.BadRequest(); }
			return Results.Ok(result);
		}
		private async static Task<IResult> CreateLeaveRequest([FromServices] ILeaveRequest<LeaveRequest> repository, LeaveRequestCreateDTO dto)
		{
			var result = await repository.CreateAsync(dto);
			if (result == null) { return Results.BadRequest(); }
			return Results.Ok(result);
		}
		private async static Task<IResult> UpdateLeaveRequest([FromServices] ILeaveRequest<LeaveRequest> repository, LeaveRequestUpdateDTO dto)
		{
			var result = await repository.UpdateAsync(dto);
			if (result == null) { return Results.BadRequest(); }
			return Results.Ok(result);
		}
		private async static Task<IResult> DeleteLeaveRequest([FromServices] ILeaveRequest<LeaveRequest> repository, int id)
		{
			var result = await repository.DeleteAsync(id);
			if (result == null) { return Results.BadRequest(id); }
			return Results.Ok(result);
		}

		//LeaveType Methods
		private async static Task<IResult> GetLeaveType([FromServices] ILeaveType<LeaveType> repository)
		{
			var result = await repository.GetAllAsync();
			if (result == null) { return Results.BadRequest(); }
			return Results.Ok(result);
		}
		private async static Task<IResult> GetLeaveTypeById([FromServices] ILeaveType<LeaveType> repository, int id)
		{
			var result = await repository.GetAsync(id);
			if (result == null) { return Results.BadRequest(); }
			return Results.Ok(result);
		}
		private async static Task<IResult> CreateLeaveType([FromServices] ILeaveType<LeaveType> repository, LeaveTypeCreateDTO dto)
		{
			var result = await repository.CreateAsync(dto);
			if (result == null) { return Results.BadRequest(); }
			return Results.Ok(result);
		}
		private async static Task<IResult> UpdateLeaveType([FromServices] ILeaveType<LeaveType> repository, LeaveType entity)
		{
			var result = await repository.UpdateAsync(entity);
			if (result == null) { return Results.BadRequest(); }
			return Results.Ok(result);
		}
		private async static Task<IResult> DeleteLeaveType([FromServices] ILeaveType<LeaveType> repository, int id)
		{
			var result = await repository.DeleteAsync(id);
			if (result == null) { return Results.BadRequest(id); }
			return Results.Ok(result);
		}

		// UsedLeaveDays Endpoints
		private async static Task<IResult> CreateUsedLeaveDays([FromServices] IUsedLeaveDays<UsedLeaveDays> repository, UsedLeaveDaysCreateDTO dto)
		{
			var result = await repository.CreateAsync(dto);
			if (result == null) { return Results.BadRequest(); }
			return Results.Ok(result);
		}

		private async static Task<IResult> UpdateUsedLeaveDays([FromServices] IUsedLeaveDays<UsedLeaveDays> repository, UsedLeaveDaysUpdateDTO dto)
		{
			var result = await repository.UpdateDaysAsync(dto);
			if (result == null) { return Results.BadRequest(); }
			return Results.Ok(result);
		}

		private async static Task<IResult> GetAllUsedLeaveDays([FromServices] IUsedLeaveDays<UsedLeaveDays> repository) 
		{
			var result = await repository.GetAllAsync();
			if (result == null) { return Results.BadRequest(); }
			return Results.Ok(result);
		}

		private async static Task<IResult> GetUsedLeaveDaysById([FromServices] IUsedLeaveDays<UsedLeaveDays> repository, int id)
		{
			var result = await repository.GetAsync(id);
			if (result == null) { return Results.BadRequest(); }
			return Results.Ok(result);
		}
		private async static Task<IResult> GetUsedLeaveDaysByEmployeeId([FromServices] IUsedLeaveDays<UsedLeaveDays> repository, int employeeId)
		{
			var result = await repository.GetByEmployeeId(employeeId);
			if (result == null) { return Results.BadRequest(); }
			return Results.Ok(result);
		}
		private async static Task<IResult> GetUsedLeaveDaysByLeaveTypeId([FromServices] IUsedLeaveDays<UsedLeaveDays> repository, int leaveTypeId)
		{
			var result = await repository.GetByLeaveTypeId(leaveTypeId);
			if (result == null) { return Results.BadRequest(); }
			return Results.Ok(result);
		}

		private async static Task<IResult> DeleteUsedLeaveDays([FromServices] IUsedLeaveDays<UsedLeaveDays> repository, int id)
		{
			var result = await repository.DeleteAsync(id);
			if (result == null) { return Results.BadRequest(); }
			return Results.Ok(result);
		}
	}
}