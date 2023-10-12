using EmployeeHub_MinimalAPI.Models;
using EmployeeHub_MinimalAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.Logging.Abstractions;

namespace EmployeeHub_MinimalAPI.Endpoints
{
	public static class Endpoints
	{
		public static void ConfigureEndpoints(this WebApplication app)
		{
			app.MapGet("api/employee", GetEmployee).WithName("GetAllEmployees").WithTags("Get").Produces(200);
			app.MapGet("api/employee{id:int}", GetEmployeeById).WithName("GetEmployeeById").WithTags("Get").Produces(200).Produces(404);
			app.MapPost("api/employee", CreateEmployee).WithName("CreateNewEmployee").WithTags("Create").Produces(200).Produces(404);
			app.MapPut("api/employee", UpdateEmployee).WithName("UpdateEmployee").WithTags("Update").Produces(200).Produces(404);
			app.MapDelete("api/employee{id:int}", DeleteEmployee).WithName("DeleteEmployee").WithTags("Delete").Produces(200).Produces(404);
			app.MapGet("api/login", Login).WithName("Login").WithTags("Login").Produces(200).Produces(404);
		}

		private async static Task<IResult> GetEmployee([FromServices] IRepository<Employee> repository)
		{
			var result = await repository.GetAllAsync();
			return Results.Ok(result);
		}

		private async static Task<IResult> GetEmployeeById([FromServices] IRepository<Employee> repository, int id)
		{
			var result = await repository.GetAsync(id);
			if (result == null) { return Results.BadRequest(); }
			return Results.Ok(result);
		}

		private async static Task<IResult> CreateEmployee([FromServices] IRepository<Employee> repository, Employee employee)
		{
			var result = await repository.CreateAsync(employee);
			if (result == null) { return Results.BadRequest(); }
			return Results.Ok(result);
		}

		private async static Task<IResult> UpdateEmployee([FromServices] IRepository<Employee> repository, Employee employee)
		{
			var result = await repository.UpdateAsync(employee);
			if (result == null) { return Results.BadRequest(); };
			return Results.Ok(result);
		}

		private async static Task<IResult> DeleteEmployee([FromServices] IRepository<Employee> repository, int id)
		{
			var result = await repository.DeleteAsync(id);
			if (result == null) { return Results.BadRequest(id); };
			return Results.Ok(result);
		}

		private async static Task<IResult> Login([FromServices] ILogin<Employee> repository, string email, string password)
		{
			var result = await repository.Login(email, password);
			if (result == null) { return Results.BadRequest(); }
			return Results.Ok(result);
		}
	}
}